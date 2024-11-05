function getAsset(assetId) {
    return $.ajax({
        url: "/FixedAsset/GetAsset",
        type: "GET",
        data: { "id": assetId }
    });
}

$(function () {
    //Get All
    var assetTable = $('#AssetTable').DataTable({
        "ajax": {
            "url": "/FixedAsset/GetAssets",
            "type": "GET",
            "datatype": "json"
        },
        "columns": [
            { "data": "name" },
            { "data": "model" },
            { "data": "serialNumber" },
            { "data": "assetCode" },
            {
                "data": "employeeId",
                "render": function (data, type, row) {
                    return data != null ? "Zasób w użyciu" : "";
                }
            },
            {
                "data": null,
                "render": function (data, type, row) {
                    return `
                         <button class="btn btn-warning editAssetBtn" data-id="${data.id}" data-bs-toggle="modal" data-bs-target="#addAssetModal"><i class="bi bi-pencil-square text-white"></i></button> 
                         <a class="btn btn-success" href="/FixedAsset/Details/${data.id}"><i class="bi bi-person-square text-white"></i></a> 
                         <button class="btn btn-danger deleteAssetBtn" data-id="${data.id}"  data-bs-toggle="modal" data-bs-target="#deleteAssetModal"><i class="bi bi-trash3 text-white"></i></button>
                    `;
                }
            },
        ],

        "columnDefs": [
            { "orderable": false, "target": 4 },
            { "orderable": false, "target": 5 },
            { "searchable": false, "targets": 4 }
        ],

        "language": {
            "search": "Wyszukaj",
            "info": "Wyświetlanie _START_ do _END_ z _TOTAL_ wierszy",
            "lengthMenu": "Wyświetl _MENU_ wierszy na stronę",
        }
    });

    //Add Asset
    $('#addAssetBtn').on('click', function () {
        clearModalFields();
        clearErrorMessages();

        $('#modal-asset-title').html("Dodaj zasób");
        $('#submit-asset-btn').html("Dodaj");

        var method = "CreateAsset";
        var message = "Pomyślnie dodano zasób";

        submitEditOrCreateAsset(method, message);
    });

    //Update Asset
    $(document).on('click', ".editAssetBtn", function () {
        clearModalFields();
        clearErrorMessages();

        var assetId = $(this).data("id");

        $('#modal-asset-title').html("Edytuj zasób");
        $('#submit-asset-btn').html("Zapisz zmiany");

        getAsset(assetId).done(function (response) {
            $('#asset-name').val(response.data.name);
            $('#asset-model').val(response.data.model);
            $('#asset-serial-number').val(response.data.serialNumber);
            $('#asset-code').val(response.data.assetCode);
        }).fail(function () {
            alert("Wystąpił błąd podczas pobierania danych.");
        });

        submitEditOrCreateAsset("EditAsset", "Pomyślnie zmodyfikowano zasób", assetId);
    });

    //Delete Asset
    $(document).on('click', ".deleteAssetBtn", function () {

        var assetId = $(this).data("id");
        console.log(assetId);
        getAsset(assetId).done(function (response) {
            $('#asset-data').html(response.data.assetCode);
        }).fail(function () {
            alert("Wystąpił błąd podczas pobierania danych.");
        });

        $('#submit-asset-delete').on("click", function () {
            $.ajax({
                url: "/FixedAsset/DeleteAsset",
                type: "POST",
                data: { "id": assetId },
                success: function () {
                    $('#deleteAssetModal').modal('hide');
                    assetTable.ajax.reload(null, false);
                    alert("Pomyślnie usunięto zasób");
                },
                error: function (response) {
                    alert(response.error);
                }
            })
        });
    });

    function refreshFixedAssetPartialView(assetId) {
        $.ajax({
            url: "/FixedAsset/GetAssetPartialView/" + assetId,
            type: "GET",
            success: function (response) {
                $('#fixedAssetDetailsContainer').html(response);
            },
            error: function () {
                alert("Wystąpił błąd podczas odświeżania widoku częściowego")
            }
        })
    }

    function submitEditOrCreateAsset(method, alertMessage, assetId = null) {
        $('#AddOrEditAssetForm').off('submit').on('submit', function (e) {
            e.preventDefault();

            var formData = {
                "Name": $('#asset-name').val(),
                "Model": $('#asset-model').val(),
                "SerialNumber": $('#asset-serial-number').val(),
                "AssetCode": $('#asset-code').val(),
                "ExpirationDate": $('#asset-expiration-date').val()
            };

            $.ajax({
                url: "/FixedAsset/" + method,
                type: "POST",
                data: formData,
                success: function (response) {
                    if (response.success) {
                        $('#addAssetModal').modal('hide');
                        assetTable.ajax.reload(null, false);
                        alert(alertMessage);
                    }
                    else {
                        displayValidationErrors(response.errors);
                    }
                },
                error: function () {
                    alert("Wystąpił błąd podczas wysyłania danych.");
                }
            })
        })
    }

    function displayValidationErrors(errors) {
        clearErrorMessages();

        for (let key in errors) {
            if (errors.hasOwnProperty(key)) {
                let errorKey = key.replace('assetDto', '');
                let errorId = errorKey + "Error";
                $("#" + errorId).html(errors[key]);
            }
        }
    }

    function clearErrorMessages() {
        $('#NameError').html('');
        $('#ModelError').html('');
        $('#SerialNumberError').html('');
        $('#AssetCodeError').html('');
        $('#ExpirationDateError').html('');

    }

    function clearModalFields() {
        $('#asset-name').val('');
        $('#asset-model').val('');
        $('#asset-serial-number').val('');
        $('#asset-code').val('');
        $('#asset-expiration-date').val('');
    }
});