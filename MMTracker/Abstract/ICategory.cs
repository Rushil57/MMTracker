using MMTracker.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MMTracker.Abstract
{
    public interface ICategory
    {
        Task<int> InsertUpdateCategory(CategoryDto model);
        Task DeleteCategoryByKey(int id);
        Task<CategoryDto> GetCategoryByKey(int id);
        Task<List<CategoryDto>> GetAllCategories(int id = -1);
    }
}
