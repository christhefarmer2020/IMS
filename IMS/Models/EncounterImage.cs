using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace IMS.Models
{
    public class EncounterImage
    {
        [Key]
        public int Image_Id { get; set; }
        public byte[] Image_Data { get; set; }
        public string Consent { get; set; }
        public DateTime Appointment_Time { get; set; }
        public string PAT_MRN { get; set; }
        public string FilePath { get; set; }
    }
}