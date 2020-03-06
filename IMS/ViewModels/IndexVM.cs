using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IMS.ViewModels
{
    public class IndexVM
    {
        public string PAT_MRN { get; set; }
        public string Provider_1 { get; set; }
        public string Provider_2 { get; set; }
        public string Provider_3 { get; set; }
        public string Provider_4 { get; set; }
        public DateTime? Contact_Date { get; set; }
        public DateTime? Appointment_Time { get; set; }
        public string First_Name { get; set; }
        public string Last_Name { get; set; }
        public DateTime? Date_Of_Birth { get; set; }
        public string Department_Name { get; set; }
        public string Visit_Type { get; set; }
        public string Consent { get; set; }
        // Images
    }
}