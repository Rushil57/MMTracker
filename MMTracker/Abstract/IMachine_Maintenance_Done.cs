using MMTracker.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MMTracker.Abstract
{
    public interface IMachine_Maintenance_Done
    {
        Task<int> InsertUpdateMachine_Maintenance_Done(Machine_Maintenance_DoneDto model);
        Task DeleteMaintenance_DoneByMachineIdAndMaintenanceId(int machineId, int MaintenanceId);
        Task<Machine_Maintenance_DoneDto> GetMaintenance_DoneByMachineIdAndMaintenanceId(int machineId, int MaintenanceId);
        Task<List<Machine_Maintenance_DoneDto>> GetAllMachine_Maintenance_Done(int machineId = -1, int MaintenanceId =-1);
    }
}
