// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.


// Write your JavaScript code.

$(function () {

    var table = $('#EmployeeTable').DataTable({
        "ajax": {
            "url": "/Employee/GetEmployees",
            "type": "GET",
            "datatype": "json"
        },
        "columns": [
            { "data": "name" },
            { "data": "surname" },
            { "data": "position" },
            { "data": "workplace"},
            {
                "data": null,
                "render": function (data, type, row) {
                    return `
                        <button class="btn btn-warning editEmployeeBtn" data-id="${data.id}" data-bs-toggle="modal" data-bs-target="#addEmployeeModal"><i class="bi bi-pencil-square text-white"></i></button> |
                        <a href="/Employee/Details/${data.id}">Details</a> |
                        <a href="/Employee/Delete/${data.id}">Delete</a>
                    `;
                }
            }

        ],

        "columnDefs": [
            { "orderable": false, "target": 4 }
        ],

        "language": {
            "search": "Wyszukaj",
            "info": "Wyświetlanie _START_ do _END_ z _TOTAL_ wierszy",
            "lengthMenu": "Wyświetl _MENU_ wierszy na stronę",
        }
    });

    $('#addEmployeeBtn').on("click", function () {
        clearModalFields();
        clearErrorMessages();

        $('#modal-employee-title').html("Dodaj pracownika");
        $('#submit-btn').html("Dodaj");

        var method = "CreateEmployee";
        var message = "Pomyślnie dodano pracownika";

        submitEditOrCreate(method, message);
    });

    $(document).on("click", ".editEmployeeBtn", function () {
        clearModalFields();
        clearErrorMessages();

        var employeeId = $(this).data("id");

        $('#modal-employee-title').html("Edytuj pracownika");
        $('#submit-btn').html("Zapisz zmiany");

        $.ajax({
            url: "/Employee/GetEmployee",
            type: "GET",
            data: {
                "id": employeeId
            },
            success: function (response) {

                $('#Name').val(response.data.name);
                $('#Surname').val(response.data.surname);
                $('#Position').val(response.data.position);
                $('#Workplace').val(response.data.workplace);

            },
            error: function () {
                alert("Wystąpił błąd")
            }


        });

        var method = "EditEmployee";
        var message = "Pomyślnie zmodyfikowano pracownika";

        submitEditOrCreate(method, message, employeeId);
    });

    function submitEditOrCreate(method, alertMessage, employeeId = null) {
        $('#AddOrEditForm').off('submit').on('submit', function (e) {
            e.preventDefault();

            if (employeeId == null) {
                var formData = {
                    "Name": $('#Name').val(),
                    "Surname": $('#Surname').val(),
                    "Position": $('#Position').val(),
                    "Workplace": $('#Workplace').val(),
                };
            }
            else {
                var formData = {
                    "Id": employeeId,
                    "Name": $('#Name').val(),
                    "Surname": $('#Surname').val(),
                    "Position": $('#Position').val(),
                    "Workplace": $('#Workplace').val(),
                };
            }

            

            $.ajax({
                url: "/Employee/" + method,
                type: "POST",
                data: formData,
                success: function (response) {
                    if (response.success) {
                        $('#addEmployeeModal').modal('hide');
                        table.ajax.reload(null, false);
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
                $("#" + errorId).html(errors[key])
            }
        }
    }

    function clearErrorMessages() {
        $('#NameError').html('');
        $('#SurnameError').html('');
        $('#PositionError').html('');
        $('#WorkplaceError').html('');
    }

    function clearModalFields() {
        $('#Name').val('');
        $('#Surname').val('');
        $('#Position').val('');
        $('#Workplace').val('');
    }

});
