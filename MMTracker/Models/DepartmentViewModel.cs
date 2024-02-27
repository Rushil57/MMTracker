using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MMTracker.Models
{
    public class DepartmentViewModel
    {
        public int DepartmentId { get; set; }
        public string Name { get; set; }
        public string SupervisorName { get; set; }
        public string MobileNumber { get; set; }
        public string ExtensionNumber { get; set; }
    }
}
