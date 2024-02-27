using MMTracker.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MMTracker.Abstract
{
    public interface IDepartment
    {
        Task<int> InsertUpdateDepartment(DepartmentDto model);
        Task DeleteDepartmentByKey(int id);
        Task<DepartmentDto> GetDepartmentByKey(int id);
        Task<List<DepartmentDto>> GetAllDepartments(int id = -1);
    }
}
