using MMTracker.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MMTracker.Models
{
    public class MachineViewModel
    {
        public int MachineId { get; set; }
        public string AssetId { get; set; }
        public int CategoryId { get; set; }
        public int DepartmentId { get; set; }
        public string MachineOwner { get; set; }
        public decimal ProductionRatePerDay { get; set; }

        //Extra
        public List<DepartmentDto> DepartmentList { get; set; }
        public List<CategoryDto> CategoryList { get; set; }
    }
}
