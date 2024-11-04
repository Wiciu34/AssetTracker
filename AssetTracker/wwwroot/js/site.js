// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.


//import { getAsset } from "./assets.js";
// Write your JavaScript code.

const toastMessage = $('.toast-body');
const toastShow = bootstrap.Toast.getOrCreateInstance($('#Toast'));

$(function () {
 
    //Get all
    var table = $('#EmployeeTable').DataTable({
        "ajax": {
            "url": "/Employee/GetEmployees",
            "type": "GET",
            "datatype": "json",
        },

        "columns": [
            { "data": "name" },
            { "data": "surname" },
            { "data": "position" },
            { "data": "workplace" },
            { "data": "email" },
            {
                "data": null,
                "render": function (data, type, row) {
                    return `
                        <button class="btn btn-warning editEmployeeBtn" data-id="${data.id}" data-bs-toggle="modal" data-bs-target="#addEmployeeModal"><i class="bi bi-pencil-square text-white"></i></button> 
                        <a class="btn btn-success" href="/Employee/Details/${data.id}"><i class="bi bi-person-square text-white"></i></a> 
                        <button class="btn btn-danger deleteEmployeeBtn" data-id="${data.id}" data-bs-toggle="modal" data-bs-target="#deleteEmployeeModal"><i class="bi bi-trash3 text-white"></i></button>
                    `;
                }
            }

        ],

        "columnDefs": [
            { "orderable": false, "target": [4,5] }
        ],

        "language": {
            "search": "Wyszukaj",
            "info": "Wyświetlanie _START_ do _END_ z _TOTAL_ wierszy",
            "lengthMenu": "Wyświetl _MENU_ wierszy na stronę",
            "infoFiltered": "(przefiltrowano z _MAX_ wszystkich rekordów)",

            "loadingRecords": ` <div class="spinner-border" role="status">
                                    <span class="visually-hidden">Ładowanie...</span>
                                </div>
                              `,
            "emptyTable": "Brak danych do wyświetlenia",
        },
    });

    //Add
    $('#addEmployeeBtn').on("click", function () {
        clearModalFields();
        clearErrorMessages();

        $('#modal-employee-title').html("Dodaj pracownika");
        $('#submit-btn').html("Dodaj");

        var method = "CreateEmployee";
        var message = "Pomyślnie dodano pracownika";

        submitEditOrCreate(method, message);
    });

    //Edit
    $(document).on("click", ".editEmployeeBtn", function () {
        clearModalFields();
        clearErrorMessages();

        var employeeId = $(this).data("id");

        $('#modal-employee-title').html("Edytuj pracownika");
        $('#submit-btn').html("Zapisz zmiany");

        getEmployee(employeeId).done(function (response) {
            $('#Name').val(response.data.name);
            $('#Surname').val(response.data.surname);
            $('#Position').val(response.data.position);
            $('#Workplace').val(response.data.workplace);
            $('#Email').val(response.data.email);
        }).fail(function () {
            alert("Wystąpił błąd podczas pobierania danych.");
        });

        var method = "EditEmployee";
        var message = "Pomyślnie zmodyfikowano pracownika";

        submitEditOrCreate(method, message, employeeId);
    });

    //Delete
    $(document).on("click", ".deleteEmployeeBtn", function () {

        var employeeId = $(this).data("id");

        getEmployee(employeeId).done(function (response) {
            $('#employee-data').html(response.data.name + " " + response.data.surname);
        }).fail(function () {
            alert("Wystąpił błąd podczas pobierania danych.");
        });

        $('#submit-delete').on("click", function () {
            $.ajax({
                url: "/Employee/DeleteEmployee",
                type: "POST",
                data: { "id": employeeId },
                success: function () {
                    $('#deleteEmployeeModal').modal('hide');
                    if (window.location.pathname.includes("/Employee/Details/")) {
                        window.location.href = "/Employee/Index";
                    }
                    else {
                        table.ajax.reload(null, false);
                    }

                    toastMessage.html(alertMessage);
                    toastShow.show();
                },
                error: function () {
                    alert("Wystąpił błąd!");
                }
            })
        });
    });

    function getEmployee(emplyeeId) {
        return $.ajax({
            url: "/Employee/GetEmployee",
            type: "GET",
            data: {
                "id": emplyeeId,
            }
        });
    };

    function refreshEmployeePartialView(employeeId) {
        $.ajax({
            url: "/Employee/GetEmployeePartialView/" + employeeId,
            type: "GET",
            success: function (response) {
                $('#employeeDetailsContainer').html(response);
            },
            error: function () {
                alert("Wystąpił błąd podczas odświeżania widoku częściowego");
            }
        })
    }

    function submitEditOrCreate(method, alertMessage, employeeId = null) {
        $('#AddOrEditForm').off('submit').on('submit', function (e) {
            e.preventDefault();

            var formData = {
                "Name": $('#Name').val(),
                "Surname": $('#Surname').val(),
                "Position": $('#Position').val(),
                "Workplace": $('#Workplace').val(),
                "Email": $('#Email').val()
            };

            $.ajax({
                url: "/Employee/" + method,
                type: "POST",
                data: {
                    "employeeDto": formData,
                    "employeeId": employeeId
                },
                success: function (response) {
                    if (response.success) {

                        $('#addEmployeeModal').modal('hide');

                        if (window.location.pathname.includes("/Employee/Details/")) {
                            refreshEmployeePartialView(employeeId);
                        }
                        else {
                            table.ajax.reload(null, false);
                        }
                        toastMessage.html(alertMessage);
                        toastShow.show();
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
                let errorKey = key.replace('employeeDto.', '');
                let errorId = errorKey + "Error";
                $("#" + errorId).html(errors[key])
            }
        }
    }

    function clearErrorMessages() {
        $('#NameError').html('');
        $('#SurnameError').html('');
        $('#PositionError').html('');
        $('#WorkplaceError').html('');
        $('#EmailError').html('');
    }

    function clearModalFields() {
        $('#Name').val('');
        $('#Surname').val('');
        $('#Position').val('');
        $('#Workplace').val('');
        $('#Email').val('');
    }
    ////////////////////////////////////////////////////////////////////

    //Details page
    let freeAssets = true
    let addAssetToEmployeeTable = $('#addAssetToEmployeeTable').DataTable({
        
        "ajax": {
            "url": "/FixedAsset/GetAssets",
            "type": "GET",
            "datatype": "json",
            "data": {
                freeAssets: freeAssets
            }
        },
        "columns": [
            { "data": "name" },
            { "data": "model" },
            { "data": "serialNumber" },
            { "data": "assetCode" },
            {
                "data": null,
                "render": function (data, type, row) {
                    return `
                        <div class="form-check">
                            <input class="form-check-input" type="checkbox" value="${data.id}">
                        </div>
                    `;           
                    
                },
                
            },
        ],

        "info": false,
        "ordering": false,
        "lengthChange": false,
        "pageLength": 10,
       

        "language": {
            "search": "Wyszukaj",
        },
    });

    $('#AddAssetsToEmployeeForm').off('submit').on('submit', function (e) {
        e.preventDefault();

        let selectedIds = [];

        $('#addAssetToEmployeeTable input.form-check-input:checked').each(function () {
            selectedIds.push($(this).val());
        });

        $.ajax({
            url: "/FixedAsset/AddAssetsToEmployee",
            type: "POST",
            data: {
                assetsIds: selectedIds,
                employeeId: $('#employeeId').val()
                
            },
            success: function (response) {
                if (response.success) {
                    $("#addAssetToEmployeeModal").modal("hide");
                    addAssetToEmployeeTable.ajax.reload(null, false);
                    refreshEmployeePartialView(response.data);
                    toastMessage.html("Pomyślnie dodano zasoby do pracownika");
                    toastShow.show();
                }
                else {
                    alert("Nie udało się dodać zasobów do pracownika");
                }
            },
            error: function () {
                alert("Coś poszło nie tak!");
            }
        })
    });

    $(document).on("click", ".deleteAssetFromEmployee", function () {

        let assetId = $(this).data("id");
        getAsset(assetId).done(function (response) {
            $('#employee-asset-data').html(response.data.name + " " + response.data.model)
        }).fail(function () {
            alert("Wystąpił błąd podczas pobierania zasobu")
        });

        $("#submit-delete-asset-from-employee").on("click", function () {

            $.ajax({
                url: "/FixedAsset/RemoveAssetFromEmployee",
                type: "POST",
                data: {
                    assetId: assetId,
                    employeeId: $('#employeeId').val()
                },
                success: function (response) {
                    if (response.success) {
                        $("#deleteAssetFromEmployeeModal").modal("hide");
                        addAssetToEmployeeTable.ajax.reload(null, false);
                        refreshEmployeePartialView(response.data);
                        toastMessage.html("Pomyślnie odebran zasób do pracownika");
                        toastShow.show();
                    }
                    else {
                        alert("Nie udało się odebrać zasobu od pracownika");
                    }
                },
                error: function () {
                    alert("Coś poszło nie tak!");
                }
            })
        });
        
    });
});
