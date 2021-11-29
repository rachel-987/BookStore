var dataTable;

$(document).ready(function () {
    loadDataTable();
});

function loadDataTable() {
    //Load data and send it to Product table.
    dataTable = $('#tblData').DataTable({
        "ajax": {
            "url": "/admin/product/getall"
        },
        "columns": [
            //There are columns in table (sequence)
            { "data": "title", "width": "15%" },
            {
                "data": "imageURL",
                "render": function (data) {
                    return `<img src="${data}" width="150" height="150" />`
                },
                "width": "15%"
            },
            { "data": "isbn", "width": "15%" },
            { "data": "price", "width": "15%" },
            { "data": "author", "width": "15%" },
            { "data": "category.name", "width": "15%" },
            {
                "data": "id",
                "render": function (data) {
                    return `
                        <div class="h-50" role="group">
                            <a href="/Admin/Product/Upsert?id=${data}" style="text-decoration:none">
                                <i class="bi bi-pencil-square"></i>&nbsp; Edit
                            </a>
                            <br/>
                            <a onClick=Delete('/Admin/Product/Delete/${data}')>
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