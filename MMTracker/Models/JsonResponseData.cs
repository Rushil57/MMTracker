using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MMTracker.Models
{
    public class JsonResponseData
    {
        public JsonResponseData()
        {
            ResponseDataList = new ArrayList();
        }
        public bool IsError { get; set; }
        public string ErrorMessage { get; set; }
        public string SuccessMessage { get; set; }
        public ArrayList ResponseDataList { get; set; }
    }
}
