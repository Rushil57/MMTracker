using MMTracker.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MMTracker.Abstract
{
    public interface IMachine_Running
    {
        Task<int> InsertUpdateMachine_Running(Machine_RunningDto model);
        Task DeleteMachine_RunningByMachineId(int machineId);
        Task<Machine_RunningDto> GetMachine_RunningByMachineId(int machineId);
        Task<List<Machine_RunningDto>> GetAllMachine_Running(int machineId = -1);
    }
}
