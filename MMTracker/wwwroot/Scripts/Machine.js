var tblList;
$(document).ready(function () {
    BindMachineList();
});
function saveMachine() {
    if (ValidateForm()) {
        var modelData = {
            MachineId: $("#hdnMachineId").val(),
            AssetId: $("#txtAssetId").val(),
            CategoryId: $("#ddlCategory").val(),
            DepartmentId: $("#ddlDepartment").val(),
            MachineOwner: $("#txtMachineOwner").val(),
            ProductionRatePerDay: $("#txtProductionRate").val()
        };
        $.ajax({
            type: "POST",
            url: '/Machine/SaveMachine',
            data: { model: modelData },
            dataType: "json",
            success: function (response) {

                if (response.isError == true) {
                    swal("Error!", response.errorMessage, "error");
                }
                else {
                    swal("Success!", response.successMessage, "success");
                    $('#myModal').modal('hide');
                    BindMachineList();
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
                    url: '/Machine/DeleteMachineByKey?id=' + id,
                    // data: id,
                    dataType: "json",
                    success: function (response) {
                        if (response.isError == true) {
                            swal("Error!", response.errorMessage, "error");
                        }
                        else {
                            swal("Success!", response.successMessage, "success");
                            BindMachineList();
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
        url: '/Machine/GetMachineByKey?id=' + id,
        dataType: "json",
        success: function (response) {
            if (response.isError == true) {
                swal("Error!", response.errorMessage, "error");
            }
            else {
                response = response.responseDataList[0];
                $("#hdnMachineId").val(response.machineId);
                $("#txtAssetId").val(response.assetId);
                $("#ddlCategory").val(response.categoryId);
                $("#ddlDepartment").val(response.departmentId);
                $("#txtMachineOwner").val(response.machineOwner);
                $("#txtProductionRate").val(response.productionRatePerDay);
                $('#myModal').modal('show');
            }
        },
        error: function () {
            //swal("Error!", "Some error had occur!", "error");
        }
    });
}
function BindMachineList() {

    $.ajax({
        type: "Get",
        url: '/Machine/GetAllMachineList',
        dataType: "json",
        success: function (response) {
            if (response.isError == true) {
                swal("Error!", response.errorMessage, "error");
            }
            else {
                if ($.fn.dataTable.isDataTable('#machinetbl')) {
                    tblList.destroy();
                }
                var list = response.responseDataList[0];
                tblList = $('#machinetbl').DataTable({
                    data: list,
                    "columns": [
                        { "data": "machineOwner" },
                        { "data": "categoryName" },
                        { "data": "departmentName" },
                        { "data": "productionRatePerDay" },
                        {
                            "render": function (data, type, full, meta) { return "<a class='btn btn-info' onclick=EditData('" + full.machineId + "'); ><i class='ft-edit'></i></a>"; }
                        },
                        {
                            data: null, render: function (data, type, row) {
                                return "<a class='btn btn-danger' onclick=DeleteData('" + row.machineId + "'); ><i class='ft-trash'></i></a>";
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
        var category = $("#ddlCategory").val().trim();
        var department = $("#ddlDepartment").val().trim();
        var machineOwner = $("#txtMachineOwner").val().trim();

        if (category == "" || category == "-1") {
            swal("Error!", "Please Select Category", "error").then(function () {
                $("#ddlCategory").focus();
                $("#ddlCategory").val("-1");
            });
            return false;
        }

        if (department == "" || department == "-1") {
            swal("Error!", "Please Select Department", "error").then(function () {
                $("#ddlDepartment").focus();
                $("#ddlDepartment").val("-1");
            });
            return false;
        }

        if (machineOwner == "") {
            swal("Error!", "Please Enter Machine Owner", "error").then(function () {
                $("#txtMachineOwner").focus();
                $("#txtMachineOwner").val("");
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

    $("#hdnMachineId").val("");
    $("#txtAssetId").val("");
    $("#ddlCategory").val("-1");
    $("#ddlDepartment").val("-1");
    $("#txtMachineOwner").val("");
    $("#txtProductionRate").val("");


}
$('#myModal').on('shown.bs.modal', function () {
    $('#txtAssetId').focus();
})