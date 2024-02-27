using MMTracker.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MMTracker.Abstract
{
    public interface IMachine_Maintenance
    {
        Task<int> InsertUpdateMachine_Maintenance(Machine_MaintenanceDto model);
        Task DeleteMachineMaintenanceByMachineIdAndMaintenanceId(int machineId, int MaintenanceId);
        Task<Machine_MaintenanceDto> GetMachineMaintenanceByMachineIdAndMaintenanceId(int machineId, int MaintenanceId);
        Task<List<Machine_MaintenanceDto>> GetAllMachine_Maintenance(int machineId = -1, int MaintenanceId = -1);
    }
}
