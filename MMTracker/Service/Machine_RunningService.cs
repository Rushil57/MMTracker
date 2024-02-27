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
    public class Machine_RunningService : BaseRepository, IMachine_Running
    {
        IConfiguration _configuration;

        public Machine_RunningService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task DeleteMachine_RunningByMachineId(int machineId)
        {
            using (connection = Get_Connection(_configuration))
            {
                var param = new DynamicParameters();
                param.Add("v_machineid", machineId, DbType.Int64, ParameterDirection.Input);
                await connection.QueryAsync("DeleteMachine_Running", param, commandType: CommandType.StoredProcedure);
            }
        }

        public async Task<List<Machine_RunningDto>> GetAllMachine_Running(int machineId = -1)
        {
            using (connection = Get_Connection(_configuration))
            {
                var param = new DynamicParameters();
                param.Add("v_machineid", machineId, DbType.Int64, ParameterDirection.Input);
                var dataList = await connection.QueryAsync<Machine_RunningDto>("GetMachine_Running", param, commandType: CommandType.StoredProcedure);
                return dataList.ToList();
            }
        }

        public async Task<Machine_RunningDto> GetMachine_RunningByMachineId(int machineId)
        {
            var dataList = await GetAllMachine_Running(machineId);
            return dataList.FirstOrDefault();
        }

        public async Task<int> InsertUpdateMachine_Running(Machine_RunningDto model)
        {
            using (connection = Get_Connection(_configuration))
            {
                var param = new DynamicParameters();
                param.Add("v_machineid", model.MachineId, DbType.Int64, ParameterDirection.Input);
                param.Add("v_daysrun", model.DaysRun, DbType.Int64, ParameterDirection.Input);
                param.Add("v_hoursrun", model.HoursRun, DbType.Time, ParameterDirection.Input);
                param.Add("v_qtyproduced", model.QtyProduced, DbType.Decimal, ParameterDirection.Input);
                var lastInsertedId = await connection.QueryAsync<int>("InsertUpdateMachineRunning", param, commandType: CommandType.StoredProcedure);
                return lastInsertedId.FirstOrDefault();
            }
        }
    }
}
