var dataTable;

$(document).ready(function () {
    loadDataTable();
});

function loadDataTable() {

    dataTable = $('#tblData').DataTable({
        "ajax": {
            "url": "/admin/order/GetAll",
            "type": "GET",
            "datatype": "json"
        },
        "columns": [
            { "data": "name", "width": "20%" },
            { "data": "phone", "width": "20%" },
            { "data": "email", "width": "15%" },
            { "data": "serviceCount", "width": "15%" },
            { "data": "status", "width": "15%" },
           {
                "data": "id",
                "render": function (data) {
                    return `<div class="text-center">
                                 <a href="/Admin/order/Details/${data}" class='btn btn-success text-white' style='cursor:pointer; width:100px;'>
                                    <i class='far fa-edit'></i> Details
                                </a>
                               &nbsp;
                               
                            </div>
                            `;
                }, "width": "30%"
            }
        ],
        "language": {
            "emptyTable": "No records found."
        },
        "width": "100%"
    });
}


