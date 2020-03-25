using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace IMS.ViewModels
{
    public class CreateVM
    {
        public string PAT_MRN_NUM { get; set; }

        [Display(Name = "Appointment Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime Appointment_Time { get; set; }

        public string Consent { get; set; }

        public List<HttpPostedFileBase> Images { get; set; }  
    }
}