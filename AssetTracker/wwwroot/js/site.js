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
                        <a href="/Employee/Edit/${data.id}">Edit</a> |
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
    });

    $('#AddEmployeeBtn').on('click', function () {

        var name = $('#Name').val();
        var Surname = $('#Surname').val();

        console.log(name);
        console.log(Surname);

        var formData = {
            "Name": name,
            "Surname": Surname,
            "Position": $('#Position').val(),
            "Workplace": $('#Workplace').val()
        };

        $.ajax({
            url: "/Employee/CreateEmployee",
            type: "POST",
            data: formData,
            success: function (response) {
                if (response.success) {
                    $('#addEmployeeModal').modal('hide');
                    table.ajax.reload(null, false);
                    alert("Nowy pracownik został dodany!");
                    console.log(response.employee)
                }
                else {
                    displayValidationErrors(response.errors);
                }
            },
            error: function () {
                alert("Wystąpił błąd podczas wysyłania danych.");
            }
        });


    });

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
        $('#PositionameError').html('');
        $('#WorkplaceError').html('');
    }

    function clearModalFields() {
        $('#Name').val('');
        $('#Surname').val('');
        $('#Positioname').val('');
        $('#Workplace').val('');
    }

});
