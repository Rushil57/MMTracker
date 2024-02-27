using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MMTracker.Dto
{
    public class Machine_MaintenanceDto
    {
        public int MachineId { get; set; }
        public int MaintenanceId { get; set; }
        public int MaintenanceFrequencyDays { get; set; }
        public TimeSpan MaintenanceFrequencyHours { get; set; }
        public decimal MaintenanceFrequencyQty { get; set; }
    }
}
