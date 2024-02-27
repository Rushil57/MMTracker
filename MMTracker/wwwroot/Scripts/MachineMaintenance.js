var tblList;
$(document).ready(function () {
    BindMachineMaintenanceList();
});
function saveMachineMaintenance() {
    if (ValidateForm()) {
        
        var modelData = {
            MachineId: $("#ddlMachine").val(),
            MaintenanceId: $("#ddlMaintenance").val(),
            MaintenanceFrequencyDays: $("#txtMaintenanceFrequencyDays").val(),
            MaintenanceFrequencyHours: $("#txtMaintenanceFrequencyHours").val(),
            MaintenanceFrequencyQty: $("#txtMaintenanceFrequencyQty").val()
        };
        $.ajax({
            type: "POST",
            url: '/Machine/SaveMachine_Maintenance',
            data: { model: modelData },
            dataType: "json",
            success: function (response) {

                if (response.isError == true) {
                    swal("Error!", response.errorMessage, "error");
                }
                else {
                    swal("Success!", response.successMessage, "success");
                    $('#myModal').modal('hide');
                    BindMachineMaintenanceList();
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
                    url: '/Machine/DeleteMachineMaintenanceByMachineIdAndMaintenanceId?machineId=' + machineId + "&maintenanceId=" + maintenanceId,
                    // data: id,
                    dataType: "json",
                    success: function (response) {
                        if (response.isError == true) {
                            swal("Error!", response.errorMessage, "error");
                        }
                        else {
                            swal("Success!", response.successMessage, "success");
                            BindMachineMaintenanceList();
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
        url: '/Machine/GetMachineMaintenanceByMachineIdAndMaintenanceId?machineId=' + machineId + "&maintenanceId=" + maintenanceId,
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
                $("#txtMaintenanceFrequencyDays").val(response.maintenanceFrequencyDays);
                $("#txtMaintenanceFrequencyHours").val(response.maintenanceFrequencyHours);
                $("#txtMaintenanceFrequencyQty").val(response.maintenanceFrequencyQty);
                $('#myModal').modal('show');
            }
        },
        error: function () {
            //swal("Error!", "Some error had occur!", "error");
        }
    });
}
function BindMachineMaintenanceList() {

    $.ajax({
        type: "Get",
        url: '/Machine/GetAllMachine_MaintenanceList',
        dataType: "json",
        success: function (response) {
            if (response.isError == true) {
                swal("Error!", response.errorMessage, "error");
            }
            else {
                if ($.fn.dataTable.isDataTable('#machineMaintenancetbl')) {
                    tblList.destroy();
                }
                var list = response.responseDataList[0];
                tblList = $('#machineMaintenancetbl').DataTable({
                    data: list,
                    "columns": [
                        { "data": "machineId" },
                        { "data": "maintenanceFrequencyDays" },
                        //{ "data": "maintenanceFrequencyHours" },
                        { "data": "maintenanceFrequencyQty" },
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
        var MaintenanceFrequencyDays = $("#txtMaintenanceFrequencyDays").val();
        var MaintenanceFrequencyHours = $("#txtMaintenanceFrequencyHours").val();
        var MaintenanceFrequencyQty = $("#txtMaintenanceFrequencyQty").val();

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

        if (MaintenanceFrequencyDays == "") {
            swal("Error!", "Please Enter Maintenance Frequency Days", "error").then(function () {
                $("#txtMaintenanceFrequencyDays").focus();
                $("#txtMaintenanceFrequencyDays").val("");
            });
            return false;
        }
        if (isNaN(MaintenanceFrequencyDays)) {
            swal("Error!", "Please Enter Valid Value For Maintenance Frequency Days", "error").then(function () {
                $("#txtMaintenanceFrequencyDays").focus();
                $("#txtMaintenanceFrequencyDays").val("");
            });
            return false;
        }
        
        if (MaintenanceFrequencyHours == "") {
            swal("Error!", "Please Enter Maintenance Frequency Hours", "error").then(function () {
                $("#txtMaintenanceFrequencyHours").focus();
                $("#txtMaintenanceFrequencyHours").val("");
            });
            return false;
        }
        
        if (MaintenanceFrequencyQty == "") {
            swal("Error!", "Please Enter Maintenance Frequency Qty", "error").then(function () {
                $("#txtMaintenanceFrequencyQty").focus();
                $("#txtMaintenanceFrequencyQty").val("");
            });
            return false;
        }
        if (isNaN(MaintenanceFrequencyQty)) {
            swal("Error!", "Please Enter Valid Value For Maintenance Frequency Qty", "error").then(function () {
                $("#txtMaintenanceFrequencyQty").focus();
                $("#txtMaintenanceFrequencyQty").val("");
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
    $("#txtMaintenanceFrequencyDays").val("");
    $("#txtMaintenanceFrequencyHours").val("");
    $("#txtMaintenanceFrequencyQty").val("");
    


}
$('#myModal').on('shown.bs.modal', function () {
    $('#ddlMachine').focus();
})