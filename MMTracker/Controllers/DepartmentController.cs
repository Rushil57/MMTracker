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
    public class DepartmentController : Controller
    {
        private readonly IDepartment _department;

        public DepartmentController(IDepartment department)
        {
            _department = department;
        }
        public IActionResult DepartmentList()
        {
            return View();
        }
        public async Task<JsonResult> GetAllDepartmentList()
        {
            JsonResponseData resData = new JsonResponseData();
            try
            {
                var deptList = await _department.GetAllDepartments();
                resData.ResponseDataList.Add(deptList);
                return Json(resData);
            }
            catch (Exception ex)
            {
                resData.IsError = true;
                resData.ErrorMessage = ex.Message;
                return Json(resData);
            }
        }
        public async Task<JsonResult> GetDepartmentByKey(int id)
        {
            JsonResponseData resData = new JsonResponseData();
            try
            {
                var department = await _department.GetDepartmentByKey(id);
                resData.ResponseDataList.Add(department);
                return Json(resData);
            }
            catch (Exception ex)
            {
                resData.IsError = true;
                resData.ErrorMessage = ex.Message;
                return Json(resData);
            }
            
        }
        public async Task<JsonResult> DeleteDepartmentByKey(int id)
        {
            JsonResponseData resData = new JsonResponseData();
            try
            {
                await _department.DeleteDepartmentByKey(id);
                resData.SuccessMessage = "Record Deleted Successfully";
            }
            catch (Exception ex)
            {
                resData.IsError = true;
                resData.ErrorMessage = ex.Message;
            }
            return Json(resData);
        }
        public async Task<JsonResult> SaveDepartment(DepartmentViewModel model)
        {
            JsonResponseData resData = new JsonResponseData();
            try
            {
                DepartmentDto department = new DepartmentDto() {
                    DepartmentId = model.DepartmentId,
                    ExtensionNumber = model.ExtensionNumber,
                    MobileNumber = model.MobileNumber,
                    Name = model.Name,
                    SupervisorName = model.SupervisorName
                };

                var id = await _department.InsertUpdateDepartment(department);
                if (model.DepartmentId > 0)
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
