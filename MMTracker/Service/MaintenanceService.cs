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
    public class MaintenanceService : BaseRepository, IMaintenance
    {
        IConfiguration _configuration;

        public MaintenanceService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task DeleteMaintenanceByKey(int id)
        {
            using (connection = Get_Connection(_configuration))
            {
                var param = new DynamicParameters();
                param.Add("v_maintenanceid", id, DbType.Int64, ParameterDirection.Input);
                await connection.QueryAsync("DeleteMaintenance", param, commandType: CommandType.StoredProcedure);
            }
        }

        public async Task<List<MaintenanceDto>> GetAllMaintenance(int id = -1)
        {
            using (connection = Get_Connection(_configuration))
            {
                var param = new DynamicParameters();
                param.Add("v_maintenanceid", id, DbType.Int64, ParameterDirection.Input);
                var dataList = await connection.QueryAsync<MaintenanceDto>("GetMaintenance", param, commandType: CommandType.StoredProcedure);
                return dataList.ToList();
            }
        }

        public async Task<MaintenanceDto> GetMaintenanceByKey(int id)
        {
            var dataList = await GetAllMaintenance(id);
            return dataList.FirstOrDefault();
        }

        public async Task<int> InsertUpdateMaintenance(MaintenanceDto model)
        {
            using (connection = Get_Connection(_configuration))
            {
                var param = new DynamicParameters();
                param.Add("v_maintenanceId", model.MaintenanceId, DbType.Int64, ParameterDirection.Input);
                param.Add("v_maintenanceName", model.MaintenanceName, DbType.String, ParameterDirection.Input);
                param.Add("v_maintenance", model.Maintenance, DbType.String, ParameterDirection.Input);
                var lastInsertedId = await connection.QueryAsync<int>("InsertUpdateMaintenance", param, commandType: CommandType.StoredProcedure);
                return lastInsertedId.FirstOrDefault();
            }
        }

        public async Task<List<AlertListDto>> GetAlertList()
        {
            using (connection = Get_Connection(_configuration))
            {
                var dataList = await connection.QueryAsync<AlertListDto>("GetAlertListData", commandType: CommandType.StoredProcedure);
                return dataList.ToList();
            }
        }
    }
}
