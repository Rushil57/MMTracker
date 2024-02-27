using MMTracker.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MMTracker.Models
{
    public class AlertListViewModel
    {
        public List<AlertListDto> AlertListData { get; set; }
        public List<string> AlertDataCollection { get; set; }
    }
}
