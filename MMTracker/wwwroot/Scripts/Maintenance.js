var tblList;
$(document).ready(function () {
    BindMaintenanceList();
});
function saveMaintenance() {
    if (ValidateForm()) {
        var modelData = {
            MaintenanceId: $("#hdnMaintenanceId").val(),
            MaintenanceName: $("#txtMaintenanceName").val(),
            Maintenance: $("#txtMaintenance").val()
        };
        $.ajax({
            type: "POST",
            url: '/Maintenance/SaveMaintenance',
            data: { model: modelData },
            dataType: "json",
            success: function (response) {

                if (response.isError == true) {
                    swal("Error!", response.errorMessage, "error");
                }
                else {
                    swal("Success!", response.successMessage, "success");
                    $('#myModal').modal('hide');
                    BindMaintenanceList();
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
                    url: '/Maintenance/DeleteMaintenanceByKey?id=' + id,
                    // data: id,
                    dataType: "json",
                    success: function (response) {
                        if (response.isError == true) {
                            swal("Error!", response.errorMessage, "error");
                        }
                        else {
                            swal("Success!", response.successMessage, "success");
                            BindMaintenanceList();
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
        url: '/Maintenance/GetMaintenanceByKey?id=' + id,
        dataType: "json",
        success: function (response) {
            if (response.isError == true) {
                swal("Error!", response.errorMessage, "error");
            }
            else {
                response = response.responseDataList[0];
                $("#hdnMaintenanceId").val(response.maintenanceId);
                $("#txtMaintenanceName").val(response.maintenanceName);
                $("#txtMaintenance").val(response.maintenance);
                $('#myModal').modal('show');
            }
        },
        error: function () {
            //swal("Error!", "Some error had occur!", "error");
        }
    });
}
function BindMaintenanceList() {

    $.ajax({
        type: "Get",
        url: '/Maintenance/GetAllMaintenanceList',
        dataType: "json",
        success: function (response) {
            if (response.isError == true) {
                swal("Error!", response.errorMessage, "error");
            }
            else {
                if ($.fn.dataTable.isDataTable('#maintenancetbl')) {
                    tblList.destroy();
                }
                var list = response.responseDataList[0];
                tblList = $('#maintenancetbl').DataTable({
                    data: list,
                    "columns": [
                        { "data": "maintenanceName" },
                        {
                            "render": function (data, type, full, meta) { return "<a class='btn btn-info' onclick=EditData('" + full.maintenanceId + "'); ><i class='ft-edit'></i></a>"; }
                        },
                        {
                            data: null, render: function (data, type, row) {
                                return "<a class='btn btn-danger' onclick=DeleteData('" + row.maintenanceId + "'); ><i class='ft-trash'></i></a>";
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
        var maintenanceName = $("#txtMaintenanceName").val().trim();
        if (maintenanceName == "") {
            swal("Error!", "Please Enter Maintenance Name", "error").then(function () {
                $("#txtMaintenanceName").focus();
                $("#txtMaintenanceName").val("");
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

    $("#hdnMaintenanceId").val("");
    $("#txtMaintenanceName").val("");
    $("#txtMaintenance").val("");
}
$('#myModal').on('shown.bs.modal', function () {
    $('#txtMaintenanceName').focus();
});