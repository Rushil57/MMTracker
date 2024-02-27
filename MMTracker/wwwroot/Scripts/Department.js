var tblList;
$(document).ready(function () {
    BindDepartmentList();
});
function saveDepartment() {
    if (ValidateForm()) {
        var modelData = {
            DepartmentId: $("#hdnDepartmentId").val(),
            Name: $("#txtDepartmentName").val(),
            SupervisorName: $("#txtSupervisorName").val(),
            MobileNumber: $("#txtMobileNumber").val(),
            ExtensionNumber: $("#txtExtensionNumber").val()
        };
        $.ajax({
            type: "POST",
            url: '/Department/SaveDepartment',
            data: { model: modelData },
            dataType: "json",
            success: function (response) {

                if (response.isError == true) {
                    swal("Error!", response.errorMessage, "error");
                }
                else {
                    swal("Success!", response.successMessage, "success");
                    $('#myModal').modal('hide');
                    BindDepartmentList();
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
                    url: '/Department/DeleteDepartmentByKey?id=' + id,
                    // data: id,
                    dataType: "json",
                    success: function (response) {
                        if (response.isError == true) {
                            swal("Error!", response.errorMessage, "error");
                        }
                        else {
                            swal("Success!", response.successMessage, "success");
                            BindDepartmentList();
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
        url: '/Department/GetDepartmentByKey?id=' + id,
        dataType: "json",
        success: function (response) {
            if (response.isError == true) {
                swal("Error!", response.errorMessage, "error");
            }
            else {
                response = response.responseDataList[0];
                $("#hdnDepartmentId").val(response.departmentId);
                $("#txtDepartmentName").val(response.name);
                $("#txtSupervisorName").val(response.supervisorName);
                $("#txtMobileNumber").val(response.mobileNumber);
                $("#txtExtensionNumber").val(response.extensionNumber);
                $('#myModal').modal('show');
            }
        },
        error: function () {
            //swal("Error!", "Some error had occur!", "error");
        }
    });
}
function BindDepartmentList() {

    $.ajax({
        type: "Get",
        url: '/Department/GetAllDepartmentList',
        dataType: "json",
        success: function (response) {
            if (response.isError == true) {
                swal("Error!", response.errorMessage, "error");
            }
            else {
                if ($.fn.dataTable.isDataTable('#departmenttbl')) {
                    tblList.destroy();
                }
                var list = response.responseDataList[0];
                tblList = $('#departmenttbl').DataTable({
                    data: list,
                    "columns": [
                        { "data": "name" },
                        { "data": "supervisorName" },
                        { "data": "mobileNumber" },
                        {
                            "render": function (data, type, full, meta) { return "<a class='btn btn-info' onclick=EditData('" + full.departmentId + "'); ><i class='ft-edit'></i></a>"; }
                        },
                        {
                            data: null, render: function (data, type, row) {
                                return "<a class='btn btn-danger' onclick=DeleteData('" + row.departmentId + "'); ><i class='ft-trash'></i></a>";
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
        var departmentName = $("#txtDepartmentName").val().trim();
        if (departmentName == "") {
            swal("Error!", "Please Enter Department Name", "error").then(function () {
                $("#txtDepartmentName").focus();
                $("#txtDepartmentName").val("");
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

    $("#hdnDepartmentId").val("");
    $("#txtDepartmentName").val("");
    $("#txtSupervisorName").val("");
    $("#txtMobileNumber").val("");
    $("#txtExtensionNumber").val("");
}
$('#myModal').on('shown.bs.modal', function () {
    $('#txtDepartmentName').focus();
});