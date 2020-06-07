var dataTable;

$(document).ready(function () {
    loadDataTable();
});


function loadDataTable() {
    dataTable = $('#tblData').DataTable({
        "ajax": {
            "url": "/admin/service/GetAll",
            "type": "GET",
            "datatype": "json"
        },
        "columns": [
            { "data": "name", "width": "20%" },
            { "data": "category.name", "width": "20%" },
            { "data": "price", "width": "15%" },
            { "data": "frequency.frequencyCount", "width": "15%" },
            {
                "data": "id",
                "render": function (data) {

                    return `<div class="text-center"> 
                            <a href="/Admin/Service/Upsert/${data}" class='btn btn-success text-white' style='cursor:pointer; width:100px;' >
                                <i class='far fa-edit'></i> Edit
                            </a>
                            &nbsp;
                            <a href="/Admin/Service/Delete/${data}" 
                                    class="btn btn-danger text-white" style="cursor:pointer">
                                    <i class="fas fa-trash-alt"></i> Delete 
                                </a>
                          </div>
                        `;
                }, "width": "30%"
            }


        ],
        "language": {
            "emptyTable": "no data found."
        },
        "width": "100%"
    });
}

