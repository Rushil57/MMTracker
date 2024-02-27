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
    public class MachineService : BaseRepository, IMachine
    {
        IConfiguration _configuration;

        public MachineService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task DeleteMachineByKey(int id)
        {
            using (connection = Get_Connection(_configuration))
            {
                var param = new DynamicParameters();
                param.Add("v_machineid", id, DbType.Int64, ParameterDirection.Input);
                await connection.QueryAsync("DeleteMachine", param, commandType: CommandType.StoredProcedure);
            }
        }

        public async Task<List<MachineDto>> GetAllMachine(int id = -1)
        {
            using (connection = Get_Connection(_configuration))
            {
                var param = new DynamicParameters();
                param.Add("v_machineid", id, DbType.Int64, ParameterDirection.Input);
                var dataList = await connection.QueryAsync<MachineDto>("GetMachine", param, commandType: CommandType.StoredProcedure);
                return dataList.ToList();
            }
        }

        public async Task<MachineDto> GetMachineByKey(int id)
        {
            var dataList = await GetAllMachine(id);
            return dataList.FirstOrDefault();
        }

        public async Task<int> InsertUpdateMachine(MachineDto model)
        {
            using (connection = Get_Connection(_configuration))
            {
                var param = new DynamicParameters();
                param.Add("v_machineid", model.MachineId, DbType.Int64, ParameterDirection.Input);
                param.Add("v_assetid", model.AssetId, DbType.String, ParameterDirection.Input);
                param.Add("v_catergoryid", model.CategoryId, DbType.Int16, ParameterDirection.Input);
                param.Add("v_departmentid", model.DepartmentId, DbType.Int64, ParameterDirection.Input);
                param.Add("v_machineowner", model.MachineOwner, DbType.String, ParameterDirection.Input);
                param.Add("v_productionrateperday", model.ProductionRatePerDay, DbType.Decimal, ParameterDirection.Input);

                var lastInsertedId = await connection.QueryAsync<int>("InsertUpdateMachine", param, commandType: CommandType.StoredProcedure);
                return lastInsertedId.FirstOrDefault();
            }
        }
    }
}
