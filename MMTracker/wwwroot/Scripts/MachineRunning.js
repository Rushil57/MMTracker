var tblList;
$(document).ready(function () {
    BindMachineRunningList();
});
function saveMachineRunning() {
    if (ValidateForm()) {
        
        var modelData = {
            MachineId: $("#ddlMachine").val(),
            DaysRun: $("#txtDaysRun").val(),
            HoursRun: $("#txtHoursRun").val(),
            QtyProduced: $("#txtQtyProduced").val()
        };
        $.ajax({
            type: "POST",
            url: '/Machine/SaveMachine_Running',
            data: { model: modelData },
            dataType: "json",
            success: function (response) {

                if (response.isError == true) {
                    swal("Error!", response.errorMessage, "error");
                }
                else {
                    swal("Success!", response.successMessage, "success");
                    $('#myModal').modal('hide');
                    BindMachineRunningList();
                }
            },
            error: function () {
                swal("Error!", "Some error had occur!", "error");
            }
        });
    }
}
function DeleteData(machineId) {
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
                    url: '/Machine/DeleteMachine_RunningByMachineId?machineId=' + machineId ,
                    // data: id,
                    dataType: "json",
                    success: function (response) {
                        if (response.isError == true) {
                            swal("Error!", response.errorMessage, "error");
                        }
                        else {
                            swal("Success!", response.successMessage, "success");
                            BindMachineRunningList();
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
function EditData(machineId) {

    $.ajax({
        type: "GET",
        url: '/Machine/GetMachine_RunningByMachineId?machineId=' + machineId,
        dataType: "json",
        success: function (response) {
            if (response.isError == true) {
                swal("Error!", response.errorMessage, "error");
            }
            else {
                response = response.responseDataList[0];
                $("#ddlMachine").val(response.machineId);
                $("#txtDaysRun").val(response.daysRun);
                $("#ddlMachine").prop('disabled', true);
                $("#txtHoursRun").val(response.hoursRun);
                $("#txtQtyProduced").val(response.qtyProduced);
                $('#myModal').modal('show');
            }
        },
        error: function () {
            //swal("Error!", "Some error had occur!", "error");
        }
    });
}
function BindMachineRunningList() {

    $.ajax({
        type: "Get",
        url: '/Machine/GetAllMachine_RunningList',
        dataType: "json",
        success: function (response) {
            if (response.isError == true) {
                swal("Error!", response.errorMessage, "error");
            }
            else {
                if ($.fn.dataTable.isDataTable('#machineRunningtbl')) {
                    tblList.destroy();
                }
                var list = response.responseDataList[0];
                tblList = $('#machineRunningtbl').DataTable({
                    data: list,
                    "columns": [
                        { "data": "machineId" },
                        { "data": "daysRun" },
                        { "data": "hoursRun" },
                        { "data": "qtyProduced" },
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
        var MachineId = $("#ddlMachine").val();
        var daysRun = $("#txtDaysRun").val();
        var hoursRun = $("#txtHoursRun").val();
        var qtyProduced = $("#txtQtyProduced").val();

        if (MachineId == "" || MachineId == "-1") {
            swal("Error!", "Please Select Value For Machine", "error").then(function () {
                $("#ddlMachine").focus();
                $("#ddlMachine").val("-1");
            });
            return false;
        }

        if (daysRun == "") {
            swal("Error!", "Please Enter Day Run", "error").then(function () {
                $("#txtDaysRun").focus();
                $("#txtDaysRun").val("-1");
            });
            return false;
        }

        if (hoursRun == "") {
            swal("Error!", "Please Enter Hours Run", "error").then(function () {
                $("#txtHoursRun").focus();
                $("#txtHoursRun").val("");
            });
            return false;
        }
        if (qtyProduced == "") {
            swal("Error!", "Please Enter Qty Produced", "error").then(function () {
                $("#txtQtyProduced").focus();
                $("#txtQtyProduced").val("");
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
    $("#ddlMachine").val("-1");
    $("#txtDaysRun").val("");
    $("#txtHoursRun").val("");
    $("#txtQtyProduced").val("");
    
}
$('#myModal').on('shown.bs.modal', function () {
    $('#ddlMachine').focus();
})