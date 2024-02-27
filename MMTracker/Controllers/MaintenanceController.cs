using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MMTracker.Abstract;
using MMTracker.Dto;
using MMTracker.Models;

namespace MMTracker.Controllers
{
    public class MaintenanceController : Controller
    {
        private readonly IMaintenance _maintenance;

        public MaintenanceController(IMaintenance maintenance)
        {
            _maintenance = maintenance;
        }
        public IActionResult MaintenanceList()
        {
            return View();
        }
        public async Task<JsonResult> GetAllMaintenanceList()
        {
            JsonResponseData resData = new JsonResponseData();
            try
            {
                var mList = await _maintenance.GetAllMaintenance();
                resData.ResponseDataList.Add(mList);
                return Json(resData);
            }
            catch (Exception ex)
            {
                resData.IsError = true;
                resData.ErrorMessage = ex.Message;
                return Json(resData);
            }
        }
        public async Task<JsonResult> GetMaintenanceByKey(int id)
        {
            JsonResponseData resData = new JsonResponseData();
            try
            {
                var maintenance = await _maintenance.GetMaintenanceByKey(id);
                resData.ResponseDataList.Add(maintenance);
                return Json(resData);
            }
            catch (Exception ex)
            {
                resData.IsError = true;
                resData.ErrorMessage = ex.Message;
                return Json(resData);
            }
            
        }
        public async Task<JsonResult> DeleteMaintenanceByKey(int id)
        {
            JsonResponseData resData = new JsonResponseData();
            try
            {
                await _maintenance.DeleteMaintenanceByKey(id);
                resData.SuccessMessage = "Record Deleted Successfully";
            }
            catch (Exception ex)
            {
                resData.IsError = true;
                resData.ErrorMessage = ex.Message;
            }
            return Json(resData);
        }
        public async Task<JsonResult> SaveMaintenance(MaintenanceViewModel model)
        {
            JsonResponseData resData = new JsonResponseData();
            try
            {
                MaintenanceDto maintenance = new MaintenanceDto() {
                    Maintenance = model.Maintenance,
                    MaintenanceId = model.MaintenanceId,
                    MaintenanceName = model.MaintenanceName
                };

                var id = await _maintenance.InsertUpdateMaintenance(maintenance);
                if (model.MaintenanceId > 0)
                {
                    resData.SuccessMessage = "Record Updated Successfully.";
                }
                else
                {
                    resData.SuccessMessage = "Record Inserted Successfully.";
                }

            }
            catch (Exception ex)
            {
                resData.IsError = true;
                resData.ErrorMessage = ex.Message;
            }


            return Json(resData);
        }
    }
}
