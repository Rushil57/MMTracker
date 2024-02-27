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
    public class CategoryController : Controller
    {
        private readonly ICategory _category;

        public CategoryController(ICategory category)
        {
            _category = category;
        }
        public IActionResult CategoryList()
        {
            return View();
        }
        public async Task<JsonResult> GetAllCategoryList()
        {
            JsonResponseData resData = new JsonResponseData();
            try
            {
                var categoryList = await _category.GetAllCategories();
                resData.ResponseDataList.Add(categoryList);
                return Json(resData);
            }
            catch (Exception ex)
            {
                resData.IsError = true;
                resData.ErrorMessage = ex.Message;
                return Json(resData);
            }
            
        }
        public async Task<JsonResult> GetCategoryByKey(int id)
        {
            JsonResponseData resData = new JsonResponseData();
            try
            {
                var category = await _category.GetCategoryByKey(id);
                resData.ResponseDataList.Add(category);
                return Json(resData);
            }
            catch (Exception ex)
            {
                resData.IsError = true;
                resData.ErrorMessage = ex.Message;
                return Json(resData);
            }
            
        }
        public async Task<JsonResult> DeleteCategoryByKey(int id)
        {
            JsonResponseData resData = new JsonResponseData();
            try
            {
                await _category.DeleteCategoryByKey(id);
                resData.SuccessMessage = "Record Deleted Successfully";
            }
            catch (Exception ex)
            {
                resData.IsError = true;
                resData.ErrorMessage = ex.Message;
            }
            return Json(resData);
        }
        public async Task<JsonResult> SaveCategory(CategoryViewModel model)
        {
            JsonResponseData resData = new JsonResponseData();
            try
            {
                CategoryDto category = new CategoryDto()
                {
                    CategoryId = model.CategoryId,
                    Description = model.Description,
                    Name = model.Name
                };
                var id = await _category.InsertUpdateCategory(category);
                if (model.CategoryId > 0)
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
