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
    public class Machine_Maintenance_DoneService : BaseRepository, IMachine_Maintenance_Done
    {
        IConfiguration _configuration;

        public Machine_Maintenance_DoneService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task DeleteMaintenance_DoneByMachineIdAndMaintenanceId(int machineId, int MaintenanceId)
        {
            using (connection = Get_Connection(_configuration))
            {
                var param = new DynamicParameters();
                param.Add("v_machineid", machineId, DbType.Int64, ParameterDirection.Input);
                param.Add("v_maintenanceid", MaintenanceId, DbType.Int64, ParameterDirection.Input);
                await connection.QueryAsync("DeleteMachine_Maintenance_Done", param, commandType: CommandType.StoredProcedure);
            }
        }

        public async Task<List<Machine_Maintenance_DoneDto>> GetAllMachine_Maintenance_Done(int machineId = -1, int MaintenanceId = -1)
        {
            using (connection = Get_Connection(_configuration))
            {
                var param = new DynamicParameters();
                param.Add("v_machineid", machineId, DbType.Int64, ParameterDirection.Input);
                param.Add("v_maintenanceid", MaintenanceId, DbType.Int64, ParameterDirection.Input);
                var dataList = await connection.QueryAsync<Machine_Maintenance_DoneDto>("GetMachine_Maintenance_Done", param, commandType: CommandType.StoredProcedure);
                return dataList.ToList();
            }
        }

        public async Task<Machine_Maintenance_DoneDto> GetMaintenance_DoneByMachineIdAndMaintenanceId(int machineId, int MaintenanceId)
        {
            var dataList = await GetAllMachine_Maintenance_Done(machineId, MaintenanceId);
            return dataList.FirstOrDefault();
        }

        public async Task<int> InsertUpdateMachine_Maintenance_Done(Machine_Maintenance_DoneDto model)
        {
            using (connection = Get_Connection(_configuration))
            {
                var param = new DynamicParameters();
                param.Add("v_machineid", model.MachineId, DbType.Int32, ParameterDirection.Input);
                param.Add("v_maintenanceid", model.MaintenanceId, DbType.Int32, ParameterDirection.Input);
                param.Add("v_doneon", model.DoneOn, DbType.DateTime, ParameterDirection.Input);
                var lastInsertedId = await connection.QueryAsync<int>("InsertUpdateMachineMaintenanceDone", param, commandType: CommandType.StoredProcedure);
                return lastInsertedId.FirstOrDefault();
            }
        }
    }
}
