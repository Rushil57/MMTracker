using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MMTracker.Dto
{
    public class MachineDto
    {
        public int MachineId { get; set; }
        public string AssetId { get; set; }
        public int CategoryId { get; set; }
        public int DepartmentId { get; set; }
        public string MachineOwner { get; set; }
        public decimal ProductionRatePerDay { get; set; }

        //Extra
        public string CategoryName { get; set; }
        public string DepartmentName { get; set; }
    }
}
