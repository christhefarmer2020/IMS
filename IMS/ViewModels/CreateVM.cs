using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace IMS.ViewModels
{
    public class CreateVM
    {
        [Display(Name = "Appointment Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Required]
        public string Appointment_Time { get; set; }

        [Required]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Display(Name = "Date of birth")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Required]
        public string DOB { get; set; }

        [Required]
        public string Consent { get; set; }

        [Required]
        [FileExtensions(Extensions = "JPEG,JPG,PNG", ErrorMessage = "The file type is not allowed")]
        public List<HttpPostedFileBase> Images { get; set;}  
    }
}