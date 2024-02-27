using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MMTracker.Dto
{
    public class AlertListDto
    {
        public int MachineId { get; set; }
        public int MaintenanceId { get; set; }
        public int MaintenanceFrequencyDays { get; set; }
        public TimeSpan MaintenanceFrequencyHours { get; set; }
        public decimal MaintenanceFrequencyQty { get; set; }
        public string MachineName { get; set; }
        public string MaintenanceName { get; set; }
    }
}
