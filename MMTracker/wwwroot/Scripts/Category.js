var tblList;
$(document).ready(function () {
    BindCategoryList();
});
function saveCategory() {
    if (ValidateForm()) {
        var modelData = {
            CategoryId: $("#hdnCategoryId").val(),
            Name: $("#txtCategoryName").val(),
            Description: $("#txtDescription").val()
        };
        $.ajax({
            type: "POST",
            url: '/Category/SaveCategory',
            data: { model: modelData },
            dataType: "json",
            success: function (response) {

                if (response.isError == true) {
                    swal("Error!", response.errorMessage, "error");
                }
                else {
                    swal("Success!", response.successMessage, "success");
                    $('#myModal').modal('hide');
                    BindCategoryList();
                }
            },
            error: function () {
                swal("Error!", "Some error had occur!", "error");
            }
        });
    }
}
function DeleteData(id) {
    swal({
        title: "Are you sure?",
        text: "Once deleted, you will not be able to recover this data!",
        icon: "warning",
        buttons: true,
        dangerMode: true,
    })
        .then((willDelete) => {
            if (willDelete) {
                $.ajax({
                    type: "GET",
                    url: '/Category/DeleteCategoryByKey?id=' + id,
                    // data: id,
                    dataType: "json",
                    success: function (response) {
                        if (response.isError == true) {
                            swal("Error!", response.errorMessage, "error");
                        }
                        else {
                            swal("Success!", response.successMessage, "success");
                            BindCategoryList();
                        }
                    },
                    error: function () {
                        //swal("Error!", "Some error had occur!", "error");
                    }
                });

            } else {
                //swal("Your data is safe!");
            }
        });

}
function EditData(id) {

    $.ajax({
        type: "GET",
        url: '/Category/GetCategoryByKey?id=' + id,
        dataType: "json",
        success: function (response) {
            if (response.isError == true) {
                swal("Error!", response.errorMessage, "error");
            }
            else {
                response = response.responseDataList[0];
                $("#hdnCategoryId").val(response.categoryId);
                $("#txtCategoryName").val(response.name);
                $("#txtDescription").val(response.description);
                $('#myModal').modal('show');
            }
        },
        error: function () {
            //swal("Error!", "Some error had occur!", "error");
        }
    });
}
function BindCategoryList() {

    $.ajax({
        type: "Get",
        url: '/Category/GetAllCategoryList',
        dataType: "json",
        success: function (response) {
            if (response.isError == true) {
                swal("Error!", response.errorMessage, "error");
            }
            else {
                if ($.fn.dataTable.isDataTable('#categorytbl')) {
                    tblList.destroy();
                }
                var list = response.responseDataList[0];
                tblList = $('#categorytbl').DataTable({
                    data: list,
                    "columns": [
                        { "data": "name" },
                        {
                            "render": function (data, type, full, meta) { return "<a class='btn btn-info' onclick=EditData('" + full.categoryId + "'); ><i class='ft-edit'></i></a>"; }
                        },
                        {
                            data: null, render: function (data, type, row) {
                                return "<a class='btn btn-danger' onclick=DeleteData('" + row.categoryId + "'); ><i class='ft-trash'></i></a>";
                            }
                        },
                    ]
                });
            }
        },
        error: function () {
            swal("Error!", "Some error had occur!", "error");
        }
    });
}
function ValidateForm() {
    var isValid = true;
    try {
        var categoryName = $("#txtCategoryName").val().trim();
        if (categoryName == "") {
            swal("Error!", "Please Enter Category Name", "error").then(function () {
                $("#txtCategoryName").focus();
                $("#txtCategoryName").val("");
            });
            return false;
        }
    } catch (err) {
        alert(err);
        return false;
    }
    return isValid;
}


function clearData() {
    $("#txtCategoryName").val("");
    $("#txtDescription").val("");
    $("#hdnCategoryId").val("");
}
$('#myModal').on('shown.bs.modal', function () {
    $('#txtCategoryName').focus();
});