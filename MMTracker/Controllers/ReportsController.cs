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
    public class ReportsController : Controller
    {
        private readonly IMaintenance _maintenance;
        private readonly IMachine_Running _machine_Running;
        private readonly IMachine_Maintenance_Done _machine_Maintenance_Done;
        private readonly IMachine _machine;
        private readonly IMachine_Maintenance _machine_Maintenance;

        public ReportsController(IMaintenance maintenance, IMachine_Running machine_Running, IMachine_Maintenance_Done machine_Maintenance_Done, IMachine machine, IMachine_Maintenance machine_Maintenance)
        {
            _maintenance = maintenance;
            _machine_Running = machine_Running;
            _machine_Maintenance_Done = machine_Maintenance_Done;
            _machine = machine;
            _machine_Maintenance = machine_Maintenance;
        }
        public IActionResult AlertList()
        {
            return View();
        }
        public async Task<JsonResult> GetAllAlertList()
        {
            JsonResponseData resData = new JsonResponseData();
            try
            {
                List<string> alertList = new List<string>();
                List<AlertListDto> finalAlertListData = new List<AlertListDto>();
                var alertListData = await _maintenance.GetAlertList();
                var machine_runningData = await _machine_Running.GetAllMachine_Running();
                var machineDoneData = await _machine_Maintenance_Done.GetAllMachine_Maintenance_Done();

                // 15 days in advance of maintenance due date 
                foreach (var item in machineDoneData)
                {
                    var singleObj = alertListData.Where(x => x.MachineId.Equals(item.MachineId) &&
                                                    x.MaintenanceId.Equals(item.MaintenanceId)).FirstOrDefault();
                    if (singleObj != null)
                    {
                        var duedate = item.DoneOn.AddDays(singleObj.MaintenanceFrequencyDays);
                        var diffDays = (duedate - DateTime.Now).TotalDays;
                        if (diffDays <= 15)
                        {
                            finalAlertListData.Add(singleObj);
                            alertList.Add("Machine: " + singleObj.MachineName + "  Maintenance : " + singleObj.MaintenanceName + " will be due in next " + (int)Math.Ceiling(diffDays) + " day(s).");
                        }
                    }
                }

                //300 hours in advance of next running hours
                foreach (var item in alertListData)
                {
                    var runningData = machine_runningData.Where(x => x.MachineId.Equals(item.MachineId)).FirstOrDefault();
                    if (runningData != null)
                    {
                        var diffHours = item.MaintenanceFrequencyHours - runningData.HoursRun;
                        if (diffHours.TotalHours <= 300)
                        {
                            finalAlertListData.Add(item);
                            alertList.Add("Machine: " + item.MachineName + " machine running hours has reached " + runningData.HoursRun.ToString());
                        }
                    }
                }

                //Around 20000 pieces before the target quantity
                foreach (var item in alertListData)
                {
                    var runningData = machine_runningData.Where(x => x.MachineId.Equals(item.MachineId)).FirstOrDefault();
                    if (runningData != null)
                    {
                        var diffQty = item.MaintenanceFrequencyQty - runningData.QtyProduced;
                        if (diffQty <= 20000)
                        {
                            finalAlertListData.Add(item);
                            alertList.Add("Machine: " + item.MachineName + " machine running Qantity has reached " + runningData.QtyProduced.ToString());
                        }
                    }
                }
                AlertListViewModel model = new AlertListViewModel();
                model.AlertListData = finalAlertListData;
                model.AlertDataCollection = alertList;
                resData.ResponseDataList.Add(model);
                return Json(resData);
            }
            catch (Exception ex)
            {
                resData.IsError = true;
                resData.ErrorMessage = ex.Message;
                return Json(resData);
            }
            
        }

        public IActionResult ReportMaintenance()
        {
            return View();
        }
        public async Task<JsonResult> ReportMaintenanceList()
        {
            JsonResponseData resData = new JsonResponseData();
            try
            {
                List<AlertListDto> finalAlertListData = new List<AlertListDto>();
                var alertListData = await _maintenance.GetAlertList();
                var machineDoneData = await _machine_Maintenance_Done.GetAllMachine_Maintenance_Done();

                // 10 days in advance of maintenance due date 
                foreach (var item in machineDoneData)
                {
                    var singleObj = alertListData.Where(x => x.MachineId.Equals(item.MachineId) &&
                                                    x.MaintenanceId.Equals(item.MaintenanceId)).FirstOrDefault();
                    if (singleObj != null)
                    {
                        var duedate = item.DoneOn.AddDays(singleObj.MaintenanceFrequencyDays);
                        var diffDays = (duedate - DateTime.Now).TotalDays;
                        if (diffDays <= 10)
                        {
                            finalAlertListData.Add(singleObj);
                        }
                    }
                }

                AlertListViewModel model = new AlertListViewModel();
                model.AlertListData = finalAlertListData;
                resData.ResponseDataList.Add(model);
                return Json(resData);
            }
            catch (Exception ex)
            {
                resData.IsError = true;
                resData.ErrorMessage = ex.Message;
                return Json(resData);
            }
        }

        public IActionResult MachineWiseMaintenance()
        {
            return View();
        }
        public async Task<JsonResult> MachinWiseMaintenanceList()
        {
            JsonResponseData resData = new JsonResponseData();
            try
            {
                List<MachinWiseReportViewModel> finalList = new List<MachinWiseReportViewModel>();
                var machineList = await _machine.GetAllMachine();
                var machine_maintenance_doneList = await _machine_Maintenance_Done.GetAllMachine_Maintenance_Done();
                var machine_maintenanceList = await _machine_Maintenance.GetAllMachine_Maintenance();
                var maintenanceList = await _maintenance.GetAllMaintenance();
                foreach (var machine in machineList)
                {
                    MachinWiseReportViewModel model = new MachinWiseReportViewModel();
                    model.MachinWiseMaintenanceList = new List<MachinWiseMaintenance>();
                    model.MachineName = machine.MachineOwner;
                    var mmList = machine_maintenanceList.Where(x => x.MachineId.Equals(machine.MachineId)).ToList();

                    foreach (var item in mmList)
                    {
                        MachinWiseMaintenance mwm = new MachinWiseMaintenance();
                        mwm.AssetID = machine.AssetId;
                        mwm.MachineName = machine.MachineOwner;
                        var mmDone = machine_maintenance_doneList.Where(x => x.MachineId.Equals(item.MachineId) && x.MaintenanceId.Equals(item.MaintenanceId)).FirstOrDefault();
                        var mt = maintenanceList.Where(x => x.MaintenanceId.Equals(item.MaintenanceId)).FirstOrDefault();
                        mwm.MaintenanceName = mt != null ? mt.MaintenanceName : string.Empty;
                        if (mmDone != null)
                        {
                            mwm.DoneOnDisplay = mmDone.DoneOn.ToShortDateString();
                            mwm.NextDueOnDisplay = mmDone.DoneOn.AddDays(item.MaintenanceFrequencyDays).ToShortDateString();
                        }
                        else
                        {
                            mwm.DoneOnDisplay = string.Empty;
                            mwm.NextDueOnDisplay = string.Empty;
                        }
                        model.MachinWiseMaintenanceList.Add(mwm);
                    }

                    finalList.Add(model);
                }
                resData.ResponseDataList.Add(finalList);
                return Json(resData);
            }
            catch (Exception ex)
            {
                resData.IsError = true;
                resData.ErrorMessage = ex.Message;
                return Json(resData);
            }
        }
    }
}
