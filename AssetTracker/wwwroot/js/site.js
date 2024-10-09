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
            {
                "data": "workplace",
                //"render": function (data, type, row) {
                //    if (data.workplace == 0) return "Warszawa";
                //    else {
                //        return "Lublin";
                //    }
                //}
            },
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

    //new DataTable('#EmployeeTable', {
    //    "columnDefs": [
    //        { "orderable": false, "target": 4 }
    //    ],
    //    "language": {
    //        "search": "Wyszukaj",
    //        "info": "Wyświetlanie _START_ do _END_ z _TOTAL_ wierszy",
    //        "lengthMenu": "Wyświetl _MENU_ wierszy na stronę",
    //    },
    //});

});
