using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IMS.ViewModels
{
    public class CreateVM
    {
        public string PAT_MRN_NUM { get; set; }
        public DateTime Appointment_Time { get; set; }
        public string Consent { get; set; }
        public List<HttpPostedFileBase> Images { get; set; }  
    }
}