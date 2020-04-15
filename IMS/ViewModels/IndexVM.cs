using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace IMS.ViewModels
{
    public class IndexVM
    {
        public int image_id { get; set; }
        [DisplayName("MRN Number")]
        public string PAT_MRN { get; set; }
        [DisplayName("Provider")]
        public string Providers { get; set; }
        [DisplayName("Contact Date")]
        public DateTime? Contact_Date { get; set; }
        [DisplayName("Appointment Time")]
        public DateTime? Appointment_Time { get; set; }
        [DisplayName("First Name")]
        public string First_Name { get; set; }
        [DisplayName("Last Name")]
        public string Last_Name { get; set; }
        [DisplayName("Date of Birth")]
        public DateTime? Date_Of_Birth { get; set; }
        [DisplayName("Department Name")]
        public string Department_Name { get; set; }
        [DisplayName("Visit Type")]
        public string Visit_Type { get; set; }
        public string Consent { get; set; }
        public string Gender { get; set; }
        public byte[] ImageData { get; set; }
    }
}