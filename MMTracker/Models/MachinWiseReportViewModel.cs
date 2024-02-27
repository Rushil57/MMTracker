using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MMTracker.Models
{
    public class MachinWiseReportViewModel
    {
        public string MachineName { get; set; }
        public List<MachinWiseMaintenance> MachinWiseMaintenanceList { get; set; }

    }

    public class MachinWiseMaintenance
    {
        public string AssetID { get; set; }
        public string MachineName { get; set; }
        public string MaintenanceName { get; set; }
        public string DoneOnDisplay  { get; set; }
        public string NextDueOnDisplay { get; set; }

    }
}
