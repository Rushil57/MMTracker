using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MMTracker.Dto
{
    public class Machine_Maintenance_DoneDto
    {
        public int MachineId { get; set; }
        public int MaintenanceId { get; set; }
        public DateTime DoneOn { get; set; }
        public string DoneOnDisplay { get; set; }
    }
}
