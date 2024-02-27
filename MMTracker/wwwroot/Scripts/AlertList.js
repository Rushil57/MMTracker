var tblList;
$(document).ready(function () {
    BindAlertList(); 
    setInterval(function () {
        BindAlertList(); 
    }, 60000)
    
});
function BindAlertList() {

    $.ajax({
        type: "Get",
        url: '/Reports/GetAllAlertList',
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
                var girdData = list.alertListData;
                tblList = $('#alertTbl').DataTable({
                    data: girdData,
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

                setTimeout(function () {
                    var alertList = list.alertDataCollection;
                    for (var i = 0; i < alertList.length; i++) {
                        alert(alertList[i]);
                    }
                }, 500);
            }
        },
        error: function () {
            swal("Error!", "Some error had occur!", "error");
        }
    });
}