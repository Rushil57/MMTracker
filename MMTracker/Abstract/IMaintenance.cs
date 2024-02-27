using MMTracker.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MMTracker.Abstract
{
    public interface IMaintenance
    {
        Task<int> InsertUpdateMaintenance(MaintenanceDto model);
        Task DeleteMaintenanceByKey(int id);
        Task<MaintenanceDto> GetMaintenanceByKey(int id);
        Task<List<MaintenanceDto>> GetAllMaintenance(int id = -1);
        Task<List<AlertListDto>> GetAlertList();
    }
}
