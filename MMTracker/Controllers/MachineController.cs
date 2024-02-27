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
    public class MachineController : Controller
    {
        private readonly IMachine _machine;
        private readonly ICategory _category;
        private readonly IDepartment _department;
        private readonly IMaintenance _maintenance;
        private readonly IMachine_Maintenance _machine_Maintenance;
        private readonly IMachine_Maintenance_Done _machine_Maintenance_Done;
        private readonly IMachine_Running _machine_Running;

        public MachineController(IMachine machine, ICategory category, IDepartment department, IMaintenance maintenance,
            IMachine_Maintenance machine_Maintenance, IMachine_Maintenance_Done machine_Maintenance_Done, IMachine_Running machine_Running)
        {
            _machine = machine;
            _category = category;
            _department = department;
            _maintenance = maintenance;
            _machine_Maintenance = machine_Maintenance;
            _machine_Maintenance_Done = machine_Maintenance_Done;
            _machine_Running = machine_Running;
        }

        #region Machine Master
        public async Task<IActionResult> MachineList()
        {
            MachineViewModel model = new MachineViewModel();
            model.CategoryList = await _category.GetAllCategories();
            model.DepartmentList = await _department.GetAllDepartments();
            return View(model);
        }
        public async Task<JsonResult> GetAllMachineList()
        {
            JsonResponseData resData = new JsonResponseData();
            try
            {
                var machineList = await _machine.GetAllMachine();
                resData.ResponseDataList.Add(machineList);
                return Json(resData);
            }
            catch (Exception ex)
            {
                resData.IsError = true;
                resData.ErrorMessage = ex.Message;
                return Json(resData);
            }
        }
        public async Task<JsonResult> GetMachineByKey(int id)
        {
            JsonResponseData resData = new JsonResponseData();
            try
            {
                var machine = await _machine.GetMachineByKey(id);
                resData.ResponseDataList.Add(machine);
                return Json(resData);
            }
            catch (Exception ex)
            {
                resData.IsError = true;
                resData.ErrorMessage = ex.Message;
                return Json(resData);
            }
        }
        public async Task<JsonResult> DeleteMachineByKey(int id)
        {
            JsonResponseData resData = new JsonResponseData();
            try
            {
                await _machine.DeleteMachineByKey(id);
                resData.SuccessMessage = "Record Deleted Successfully";
            }
            catch (Exception ex)
            {
                resData.IsError = true;
                resData.ErrorMessage = ex.Message;
            }
            return Json(resData);
        }
        public async Task<JsonResult> SaveMachine(MachineViewModel model)
        {
            JsonResponseData resData = new JsonResponseData();
            try
            {
                MachineDto machine = new MachineDto()
                {
                    AssetId = model.AssetId,
                    CategoryId = model.CategoryId,
                    DepartmentId = model.DepartmentId,
                    MachineId = model.MachineId,
                    MachineOwner = model.MachineOwner,
                    ProductionRatePerDay = model.ProductionRatePerDay
                };

                var id = await _machine.InsertUpdateMachine(machine);
                if (model.MachineId > 0)
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
        #endregion


        #region Machine_Maintenance
        public async Task<IActionResult> MachineMaintenanceList()
        {
            Machine_MaintenanceViewModel model = new Machine_MaintenanceViewModel();
            model.MachineList = await _machine.GetAllMachine();
            model.MaintenanceList = await _maintenance.GetAllMaintenance();
            return View(model);
        }
        public async Task<JsonResult> GetAllMachine_MaintenanceList()
        {
            JsonResponseData resData = new JsonResponseData();
            try
            {
                var machineList = await _machine_Maintenance.GetAllMachine_Maintenance();
                resData.ResponseDataList.Add(machineList);
                return Json(resData);
            }
            catch (Exception ex)
            {
                resData.IsError = true;
                resData.ErrorMessage = ex.Message;
                return Json(resData);
            }
        }
        public async Task<JsonResult> GetMachineMaintenanceByMachineIdAndMaintenanceId(int machineId, int maintenanceId)
        {
            JsonResponseData resData = new JsonResponseData();
            try
            {
                var machine = await _machine_Maintenance.GetMachineMaintenanceByMachineIdAndMaintenanceId(machineId, maintenanceId);
                resData.ResponseDataList.Add(machine);
                return Json(resData);
            }
            catch (Exception ex)
            {
                resData.IsError = true;
                resData.ErrorMessage = ex.Message;
                return Json(resData);
            }
        }
        public async Task<JsonResult> DeleteMachineMaintenanceByMachineIdAndMaintenanceId(int machineId, int maintenanceId)
        {
            JsonResponseData resData = new JsonResponseData();
            try
            {
                await _machine_Maintenance.DeleteMachineMaintenanceByMachineIdAndMaintenanceId(machineId, maintenanceId);
                resData.SuccessMessage = "Record Deleted Successfully";
            }
            catch (Exception ex)
            {
                resData.IsError = true;
                resData.ErrorMessage = ex.Message;
            }
            return Json(resData);
        }
        public async Task<JsonResult> SaveMachine_Maintenance(Machine_MaintenanceViewModel model)
        {
            JsonResponseData resData = new JsonResponseData();
            try
            {
                Machine_MaintenanceDto machine_Maintenance = new Machine_MaintenanceDto()
                {
                    MachineId = model.MachineId,
                    MaintenanceFrequencyDays = model.MaintenanceFrequencyDays,
                    MaintenanceId = model.MaintenanceId,
                    MaintenanceFrequencyHours = model.MaintenanceFrequencyHours,
                    MaintenanceFrequencyQty = model.MaintenanceFrequencyQty

                };

                var id = await _machine_Maintenance.InsertUpdateMachine_Maintenance(machine_Maintenance);
                if (model.MachineId > 0)
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
        #endregion


        #region Machine_Maintenance_Done
        public async Task<IActionResult> MachineMaintenanceDoneList()
        {
            Machine_Maintenance_DoneViewModel model = new Machine_Maintenance_DoneViewModel();
            model.MachineList = await _machine.GetAllMachine();
            model.MaintenanceList = await _maintenance.GetAllMaintenance();
            return View(model);
        }
        public async Task<JsonResult> GetAllMachine_Maintenance_DoneList()
        {
            JsonResponseData resData = new JsonResponseData();
            try
            {
                var machineList = await _machine_Maintenance_Done.GetAllMachine_Maintenance_Done();
                foreach (var item in machineList)
                {
                    item.DoneOnDisplay = item.DoneOn.ToShortDateString();
                }
                resData.ResponseDataList.Add(machineList);
                return Json(resData);
            }
            catch (Exception ex)
            {
                resData.IsError = true;
                resData.ErrorMessage = ex.Message;
                return Json(resData);
            }
        }
        public async Task<JsonResult> GetMaintenance_DoneByMachineIdAndMaintenanceId(int machineId, int maintenanceId)
        {
            JsonResponseData resData = new JsonResponseData();
            try
            {
                var machine = await _machine_Maintenance_Done.GetMaintenance_DoneByMachineIdAndMaintenanceId(machineId, maintenanceId);
                machine.DoneOnDisplay = machine.DoneOn.ToShortDateString();
                resData.ResponseDataList.Add(machine);
                return Json(resData);
            }
            catch (Exception ex)
            {
                resData.IsError = true;
                resData.ErrorMessage = ex.Message;
                return Json(resData);
            }
        }
        public async Task<JsonResult> DeleteMaintenance_DoneByMachineIdAndMaintenanceId(int machineId, int maintenanceId)
        {
            JsonResponseData resData = new JsonResponseData();
            try
            {
                await _machine_Maintenance_Done.DeleteMaintenance_DoneByMachineIdAndMaintenanceId(machineId, maintenanceId);
                resData.SuccessMessage = "Record Deleted Successfully";
            }
            catch (Exception ex)
            {
                resData.IsError = true;
                resData.ErrorMessage = ex.Message;
            }
            return Json(resData);
        }
        public async Task<JsonResult> SaveMachine_Maintenance_Done(Machine_Maintenance_DoneViewModel model)
        {
            JsonResponseData resData = new JsonResponseData();
            try
            {
                Machine_Maintenance_DoneDto machine_Maintenance_done = new Machine_Maintenance_DoneDto()
                {
                    DoneOn = Convert.ToDateTime(model.DoneOnString),
                    MachineId = model.MachineId,
                    MaintenanceId = model.MaintenanceId
                };

                var id = await _machine_Maintenance_Done.InsertUpdateMachine_Maintenance_Done(machine_Maintenance_done);
                if (model.MachineId > 0)
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
        #endregion

        #region Machine_Running
        public async Task<IActionResult> MachineRunningList()
        {
            Machine_RunningViewModel model = new Machine_RunningViewModel();
            model.MachineList = await _machine.GetAllMachine();
            return View(model);
        }
        public async Task<JsonResult> GetAllMachine_RunningList()
        {
            JsonResponseData resData = new JsonResponseData();
            try
            {
                var machineList = await _machine_Running.GetAllMachine_Running();
                resData.ResponseDataList.Add(machineList);
                return Json(resData);
            }
            catch (Exception ex)
            {
                resData.IsError = true;
                resData.ErrorMessage = ex.Message;
                return Json(resData);
            }
        }
        public async Task<JsonResult> GetMachine_RunningByMachineId(int machineId)
        {
            JsonResponseData resData = new JsonResponseData();
            try
            {
                var machine = await _machine_Running.GetMachine_RunningByMachineId(machineId);
                resData.ResponseDataList.Add(machine);
                return Json(resData);
            }
            catch (Exception ex)
            {
                resData.IsError = true;
                resData.ErrorMessage = ex.Message;
                return Json(resData);
            }
        }
        public async Task<JsonResult> DeleteMachine_RunningByMachineId(int machineId)
        {
            JsonResponseData resData = new JsonResponseData();
            try
            {
                await _machine_Running.DeleteMachine_RunningByMachineId(machineId);
                resData.SuccessMessage = "Record Deleted Successfully";
            }
            catch (Exception ex)
            {
                resData.IsError = true;
                resData.ErrorMessage = ex.Message;
            }
            return Json(resData);
        }
        public async Task<JsonResult> SaveMachine_Running(Machine_RunningViewModel model)
        {
            JsonResponseData resData = new JsonResponseData();
            try
            {
                Machine_RunningDto machine_Maintenance_done = new Machine_RunningDto()
                {
                    DaysRun = model.DaysRun,
                    HoursRun = model.HoursRun,
                    MachineId = model.MachineId,
                    QtyProduced = model.QtyProduced
                };

                var id = await _machine_Running.InsertUpdateMachine_Running(machine_Maintenance_done);
                if (model.MachineId > 0)
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
        #endregion


    }
}
