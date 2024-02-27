using MMTracker.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MMTracker.Models
{
    public class Machine_RunningViewModel
    {
        public int MachineId { get; set; }
        public int DaysRun { get; set; }
        public TimeSpan HoursRun { get; set; }
        public decimal QtyProduced { get; set; }

        //Extra
        public List<MachineDto> MachineList { get; set; }
    }
}
