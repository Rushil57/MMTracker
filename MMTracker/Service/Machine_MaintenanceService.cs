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
    public class Machine_MaintenanceService : BaseRepository, IMachine_Maintenance
    {
        IConfiguration _configuration;

        public Machine_MaintenanceService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task DeleteMachineMaintenanceByMachineIdAndMaintenanceId(int machineId, int MaintenanceId)
        {
            using (connection = Get_Connection(_configuration))
            {
                var param = new DynamicParameters();
                param.Add("v_machineid", machineId, DbType.Int64, ParameterDirection.Input);
                param.Add("v_maintenanceid", MaintenanceId, DbType.Int64, ParameterDirection.Input);
                await connection.QueryAsync("DeleteMachine_Maintenance", param, commandType: CommandType.StoredProcedure);
            }
        }

        public async Task<List<Machine_MaintenanceDto>> GetAllMachine_Maintenance(int machineId = -1, int MaintenanceId = -1)
        {
            using (connection = Get_Connection(_configuration))
            {
                var param = new DynamicParameters();
                param.Add("v_machineid", machineId, DbType.Int64, ParameterDirection.Input);
                param.Add("v_maintenanceid", MaintenanceId, DbType.Int64, ParameterDirection.Input);
                var dataList = await connection.QueryAsync<Machine_MaintenanceDto>("GetMachine_Maintenance", param, commandType: CommandType.StoredProcedure);
                return dataList.ToList();
            }
        }

        public async Task<Machine_MaintenanceDto> GetMachineMaintenanceByMachineIdAndMaintenanceId(int machineId, int MaintenanceId)
        {
            var dataList = await GetAllMachine_Maintenance(machineId, MaintenanceId);
            return dataList.FirstOrDefault();
        }

        public async Task<int> InsertUpdateMachine_Maintenance(Machine_MaintenanceDto model)
        {
            using (connection = Get_Connection(_configuration))
            {
                var param = new DynamicParameters();
                param.Add("v_machineid", model.MachineId, DbType.Int64, ParameterDirection.Input);
                param.Add("v_maintenanceid", model.MaintenanceId, DbType.Int64, ParameterDirection.Input);
                param.Add("v_maintenancefrequencydays", model.MaintenanceFrequencyDays, DbType.Int64, ParameterDirection.Input);
                param.Add("v_maintenancefrequencyhours", model.MaintenanceFrequencyHours, DbType.Time, ParameterDirection.Input);
                param.Add("v_maintenancefrequencyqty", model.MaintenanceFrequencyQty, DbType.Decimal, ParameterDirection.Input);
                var lastInsertedId = await connection.QueryAsync<int>("InsertUpdateMachineMaintenance", param, commandType: CommandType.StoredProcedure);
                return lastInsertedId.FirstOrDefault();
            }
        }
    }
}
