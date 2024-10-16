
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
            { "data": "employeeId" },
            {
                "data": null,
                "render": function (data, type, row) {
                    return `
                        <button class="btn btn-warning editAssetBtn" data-id="${data.id}" data-bs-toggle="modal" data-bs-target="#addAssetModal"><i class="bi bi-pencil-square text-white"></i></button> 
                        <a class="btn btn-success" href="/FixedAsset/Details/${data.id}"><i class="bi bi-person-square text-white"></i></a> 
                        <button class="btn btn-danger" data-id="${data.id}"><i class="bi bi-trash3 text-white"></i></button>
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


    function getAsset(assetId) {
        return $.ajax({
            url: "/FixedAsset/GetAsset",
            type: "GET",
            data: { "id": assetId }
        });
    }

    function submitEditOrCreateAsset(method, alertMessage, assetId = null) {
        $('#AddOrEditAssetForm').off('submit').on('submit', function (e) {
            e.preventDefault();

            if (assetId == null) {
                var formData = {
                    "Name": $('#asset-name').val(),
                    "Model": $('#asset-model').val(),
                    "SerialNumber": $('#asset-serial-number').val(),
                    "AssetCode": $('#asset-code').val(),
                };
            }
            else {
                var formData = {
                    "Id": assetId,
                    "Name": $('#asset-name').val(),
                    "Model": $('#asset-model').val(),
                    "SerialNumber": $('#asset-serial-number').val(),
                    "AssetCode": $('#asset-code').val(),
                };
            }



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

        for (var key in errors) {
            if (errors.hasOwnProperty(key)) {
                var errorId = key + "Error";
                $("#" + errorId).html(errors[key].join('</br>'));
            }
        }
    }

    function clearErrorMessages() {
        $('#NameError').html('');
        $('#ModelError').html('');
        $('#SerialNumberError').html('');
        $('#AssetCodeError').html('');
    }

    function clearModalFields() {
        $('#asset-name').val('');
        $('#asset-model').val('');
        $('#asset-serial-number').val('');
        $('#asset-code').val('');
    }
});