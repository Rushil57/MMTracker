using MMTracker.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MMTracker.Models
{
    public class Machine_MaintenanceViewModel
    {
        public int MachineId { get; set; }
        public int MaintenanceId { get; set; }
        public int MaintenanceFrequencyDays { get; set; }
        public TimeSpan MaintenanceFrequencyHours { get; set; }
        public decimal MaintenanceFrequencyQty { get; set; }

        // Extra
        public List<MachineDto> MachineList { get; set; }
        public List<MaintenanceDto> MaintenanceList { get; set; }
    }
}
