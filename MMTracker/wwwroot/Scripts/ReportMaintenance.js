var tblList;
$(document).ready(function () {
    BindReportMaintenanceList(); 
});
function BindReportMaintenanceList() {

    $.ajax({
        type: "Get",
        url: '/Reports/ReportMaintenanceList',
        dataType: "json",
        success: function (response) {
            if (response.isError == true) {
                swal("Error!", response.errorMessage, "error");
            }
            else {
                if ($.fn.dataTable.isDataTable('#alertTbl')) {
                    tblList.destroy();
                }
                var list = response.responseDataList[0];
                var gridData = list.alertListData;
                tblList = $('#alertTbl').DataTable({
                    data: gridData,
                    "columns": [
                        { "data": "machineName" },
                        { "data": "maintenanceName" },
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
            }
        },
        error: function () {
            swal("Error!", "Some error had occur!", "error");
        }
    });
}

