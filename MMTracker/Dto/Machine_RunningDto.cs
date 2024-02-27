using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MMTracker.Dto
{
    public class Machine_RunningDto
    {
        public int MachineId { get; set; }
        public int DaysRun { get; set; }
        public TimeSpan HoursRun { get; set; }
        public decimal QtyProduced { get; set; }
    }
}
