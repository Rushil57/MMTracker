var tblList;
$(document).ready(function () {
    $('#txtDoneOn').datepicker();
    BindMachineMaintenanceDoneList();
});
function saveMachineMaintenanceDone() {
    if (ValidateForm()) {
        
        var modelData = {
            MachineId: $("#ddlMachine").val(),
            MaintenanceId: $("#ddlMaintenance").val(),
            DoneOnString: $("#txtDoneOn").val()
        };
        $.ajax({
            type: "POST",
            url: '/Machine/SaveMachine_Maintenance_Done',
            data: { model: modelData },
            dataType: "json",
            success: function (response) {

                if (response.isError == true) {
                    swal("Error!", response.errorMessage, "error");
                }
                else {
                    swal("Success!", response.successMessage, "success");
                    $('#myModal').modal('hide');
                    BindMachineMaintenanceDoneList();
                }
            },
            error: function () {
                swal("Error!", "Some error had occur!", "error");
            }
        });
    }
}
function DeleteData(machineId, maintenanceId) {
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
                    url: '/Machine/DeleteMaintenance_DoneByMachineIdAndMaintenanceId?machineId=' + machineId + "&maintenanceId=" + maintenanceId,
                    // data: id,
                    dataType: "json",
                    success: function (response) {
                        if (response.isError == true) {
                            swal("Error!", response.errorMessage, "error");
                        }
                        else {
                            swal("Success!", response.successMessage, "success");
                            BindMachineMaintenanceDoneList();
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
function EditData(machineId, maintenanceId) {

    $.ajax({
        type: "GET",
        url: '/Machine/GetMaintenance_DoneByMachineIdAndMaintenanceId?machineId=' + machineId + "&maintenanceId=" + maintenanceId,
        dataType: "json",
        success: function (response) {
            if (response.isError == true) {
                swal("Error!", response.errorMessage, "error");
            }
            else {
                response = response.responseDataList[0];
                $("#ddlMachine").val(response.machineId);
                $("#ddlMaintenance").val(response.maintenanceId);
                $("#ddlMachine").prop('disabled', true);
                $("#ddlMaintenance").prop('disabled', true);
                $("#txtDoneOn").val(response.doneOnDisplay);
                $('#myModal').modal('show');
            }
        },
        error: function () {
            //swal("Error!", "Some error had occur!", "error");
        }
    });
}
function BindMachineMaintenanceDoneList() {

    $.ajax({
        type: "Get",
        url: '/Machine/GetAllMachine_Maintenance_DoneList',
        dataType: "json",
        success: function (response) {
            if (response.isError == true) {
                swal("Error!", response.errorMessage, "error");
            }
            else {
                if ($.fn.dataTable.isDataTable('#machineMaintenanceDonetbl')) {
                    tblList.destroy();
                }
                var list = response.responseDataList[0];
                tblList = $('#machineMaintenanceDonetbl').DataTable({
                    data: list,
                    "columns": [
                        { "data": "machineId" },
                        { "data": "doneOnDisplay" },
                        {
                            "render": function (data, type, full, meta) { return "<a class='btn btn-info' onclick=EditData('" + full.machineId + "','" + full.maintenanceId + "'); ><i class='ft-edit'></i></a>"; }
                        },
                        {
                            data: null, render: function (data, type, row) {
                                return "<a class='btn btn-danger' onclick=DeleteData('" + row.machineId + "','" + row.maintenanceId + "'); ><i class='ft-trash'></i></a>";
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
        var MachineId = $("#ddlMachine").val();
        var MaintenanceId = $("#ddlMaintenance").val();
        var DoneOn = $("#txtDoneOn").val();
        

        if (MachineId == "" || MachineId == "-1") {
            swal("Error!", "Please Select Value For Machine", "error").then(function () {
                $("#ddlMachine").focus();
                $("#ddlMachine").val("-1");
            });
            return false;
        }

        if (MaintenanceId == "" || MaintenanceId == "-1") {
            swal("Error!", "Please Select Maintenance", "error").then(function () {
                $("#ddlMaintenance").focus();
                $("#ddlMaintenance").val("-1");
            });
            return false;
        }

        if (DoneOn == "") {
            swal("Error!", "Please Enter Value For Done On", "error").then(function () {
                $("#txtDoneOn").focus();
                $("#txtDoneOn").val("");
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
    $("#ddlMachine").prop('disabled', false);
    $("#ddlMaintenance").prop('disabled', false);
    $("#ddlMachine").val("-1");
    $("#ddlMaintenance").val("-1");
    $("#txtDoneOn").val("");
}
$('#myModal').on('shown.bs.modal', function () {
    $('#ddlMachine').focus();
})