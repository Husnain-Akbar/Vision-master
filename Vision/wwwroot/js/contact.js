var dataTable;

$(document).ready(function () {
    loadDataTable();
});

function loadDataTable() {

    dataTable = $('#tblData').DataTable({
        "ajax": {
            "url": "/customer/contact/GetAll",
            "type": "GET",
            "datatype": "json"
        },
        "columns": [
            { "data": "name", "width": "10%" },
            { "data": "email", "width": "10%" },
            { "data": "message", "width": "30%" },

            {
                "data": "id",
                "render": function (data) {
                    return `<div class="text-center">
                                <a href="/customer/contact/Upsert/${data}" class='btn btn-success text-white' style='cursor:pointer; width:100px;'>
                                    <i class='far fa-edit'></i> Edit
                                </a>
                                &nbsp;
                                <a href="/customer/contact/Delete/${data}" 
                                    class="btn btn-danger text-white" style="cursor:pointer">
                                    <i class="fas fa-trash-alt"></i> Delete 
                                </a>
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


