using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace IMS.ViewModels
{
    public class CreateVM
    {
        [Required]
        [RegularExpression(@"[123456789]{9}$", ErrorMessage = "Must be 9 digits with no special characters.")]
        public string PAT_MRN_NUM { get; set; }

        [Display(Name = "Appointment Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Required]

        public DateTime Appointment_Time { get; set; }

        [Required]
        public string Consent { get; set; }
        
        [FileExtensions(Extensions = "JPEG,JPG,PNG,GIF,TIFF")]
        [Required]
        public List<HttpPostedFileBase> Images { get; set;}  
    }
}