
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
                        <button class="btn btn-warning" data-id="${data.id}"><i class="bi bi-pencil-square text-white"></i></button> 
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
})