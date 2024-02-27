using Dapper;
using Microsoft.Extensions.Configuration;
using MMTracker.Abstract;
using MMTracker.Dto;
using MMTracker.Helper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace MMTracker.Service
{
    public class DepartmentService : BaseRepository, IDepartment
    {
        IConfiguration _configuration;

        public DepartmentService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task DeleteDepartmentByKey(int id)
        {
            using (connection = Get_Connection(_configuration))
            {
                var param = new DynamicParameters();
                param.Add("v_departmentid", id, DbType.Int64, ParameterDirection.Input);
                await connection.QueryAsync("DeleteDepartment", param, commandType: CommandType.StoredProcedure);
            }
        }

        public async Task<List<DepartmentDto>> GetAllDepartments(int id = -1)
        {
            using (connection = Get_Connection(_configuration))
            {
                var param = new DynamicParameters();
                param.Add("v_departmentid", id, DbType.Int64, ParameterDirection.Input);
                var dataList = await connection.QueryAsync<DepartmentDto>("GetDepartment", param, commandType: CommandType.StoredProcedure);
                return dataList.ToList();
            }
        }

        public async Task<DepartmentDto> GetDepartmentByKey(int id)
        {
            var dataList = await GetAllDepartments(id);
            return dataList.FirstOrDefault();
        }

        public async Task<int> InsertUpdateDepartment(DepartmentDto model)
        {
            using (connection = Get_Connection(_configuration))
            {
                var param = new DynamicParameters();
                param.Add("v_departmentId", model.DepartmentId, DbType.Int64, ParameterDirection.Input);
                param.Add("v_name", model.Name, DbType.String, ParameterDirection.Input);
                param.Add("v_supervisorname", model.SupervisorName, DbType.String, ParameterDirection.Input);
                param.Add("v_mobilenumber", model.MobileNumber, DbType.String, ParameterDirection.Input);
                param.Add("v_extensionnumber", model.ExtensionNumber, DbType.String, ParameterDirection.Input);
                var lastInsertedId = await connection.QueryAsync<int>("InsertUpdateDepartment", param, commandType: CommandType.StoredProcedure);
                return lastInsertedId.FirstOrDefault();
            }
        }
    }
}
