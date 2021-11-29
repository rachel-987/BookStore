var dataTable;

$(document).ready(function () {
    loadDataTable();
});

function loadDataTable() {
    //Load data and send it to Product table.
    dataTable = $('#tblData').DataTable({
        "ajax": {
            "url": "/Admin/Company/GetAll"
        },
        "columns": [
            //There are columns in table (sequence)
            { "data": "name", "width": "15%" }, 
            { "data": "streetAddress", "width": "15%" },
            { "data": "city", "width": "15%" },
            { "data": "state", "width": "15%" },
            { "data": "postCode", "width": "15%" },
            { "data": "phoneNumber", "width": "15%" },
            {
                "data": "id",
                "render": function (data) {
                    return `
                        <div class="h-50" role="group">
                            <a href="/Admin/Company/Upsert?id=${data}" style="text-decoration:none">
                                <i class="bi bi-pencil-square"></i>&nbsp; Edit
                            </a>
                            <br/>
                            <a onClick=Delete('/Admin/Company/Delete/${data}')>
                                <i class="bi bi-x-circle"></i>&nbsp; Delete
                            </a> 
                        </div>
                    `
                },
                "width": "10%"
            }
        ]
    });
}

function Delete(url) {
    Swal.fire({
        title: 'Are you sure?',
        text: "You won't be able to revert this!",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Yes, delete it!'
    }).then((result) => {
        if (result.isConfirmed) {
            $.ajax({
                url: url,
                type: 'DELETE',
                success: function (data) {
                    if (data.success) {
                        dataTable.ajax.reload();
                        toastr.success(data.message);
                    }
                    else {
                        toastr.error(data.message);
                    }
                }
            })
        }
    })
}