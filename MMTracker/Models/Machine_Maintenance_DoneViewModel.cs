using MMTracker.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MMTracker.Models
{
    public class Machine_Maintenance_DoneViewModel
    {
        public int MachineId { get; set; }
        public int MaintenanceId { get; set; }
        public DateTime DoneOn { get; set; }

        //Extra
        public string DoneOnString { get; set; }
        public List<MachineDto> MachineList { get; set; }
        public List<MaintenanceDto> MaintenanceList { get; set; }
    }
}
