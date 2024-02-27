var tblList;
$(document).ready(function () {
    BindMachinWiseMaintenanceList(); 
});
function BindMachinWiseMaintenanceList() {

    $.ajax({
        type: "Get",
        url: '/Reports/MachinWiseMaintenanceList',
        dataType: "json",
        success: function (response) {
            if (response.isError == true) {
                swal("Error!", response.errorMessage, "error");
            }
            else {
                if ($.fn.dataTable.isDataTable('#machinWiseMaintenanceTbl')) {
                    tblList.destroy();
                }
                var list = response.responseDataList[0];
                tblList = $('#machinWiseMaintenanceTbl').DataTable({

                    data: list,
                    "columns": [
                        {
                            "className": 'details-control',
                            "orderable": false,
                            "data": null,
                            "defaultContent": ''
                        },
                        //{
                        //    data: "machineName", render: function (data, type, row) {
                        //        return "<span class='details-control'>t</span><span> " + row.machineName+"</span>";
                        //        }

                        //},
                        { "data": "machineName", "className": 'm-name' }
                        //{ "data": "maintenanceName" },
                        //{
                        //    "render": function (data, type, full, meta) { return "<a class='btn btn-info' onclick=EditData('" + full.categoryId + "'); ><i class='ft-edit'></i></a>"; }
                        //},
                        //{
                        //    data: null, render: function (data, type, row) {
                        //        return "<a class='btn btn-danger' onclick=DeleteData('" + row.categoryId + "'); ><i class='ft-trash'></i></a>";
                        //    }
                        //},
                    ]
                });

                // Add event listener for opening and closing details
                $('#machinWiseMaintenanceTbl tbody').on('click', 'td.details-control', function () {
                    var tr = $(this).closest('tr');
                    var row = tblList.row(tr);

                    if (row.child.isShown()) {
                        // This row is already open - close it
                        row.child.hide();
                        tr.removeClass('shown');
                    }
                    else {
                        // Open this row
                        row.child(formatDetailsData(row.data())).show();
                        tr.addClass('shown');
                    }
                });
            }
        },
        error: function () {
            swal("Error!", "Some error had occur!", "error");
        }
    });
}

function formatDetailsData(rowData) {
    var detailsHtml = "<table style='width:100%;'><tr> <th>Maintenance Name</th> <th> Done On</th> <th>Next Due on</th> </tr>";
    for (var i = 0; i < rowData.machinWiseMaintenanceList.length; i++) {
        detailsHtml += "<tr> <td>" + rowData.machinWiseMaintenanceList[i].maintenanceName + "</td> <td> " + rowData.machinWiseMaintenanceList[i].doneOnDisplay + "</td> <td>" + rowData.machinWiseMaintenanceList[i].nextDueOnDisplay + "</td> </tr>";
    }
    if (rowData.machinWiseMaintenanceList.length == 0) {
        detailsHtml += "<tr> <td colspan='3'> No Data </td></tr>";
    }
    detailsHtml += "</table>";
    



    return detailsHtml;
}

