using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace IMS.Models
{
    public class Encounter
    {
        [Key]
        public string PAT_ENC_CSN_ID { get; set; }
        public string PAT_MRN_ID { get; set; }
        public string DX_Codes { get; set; }
        public string DX_Names { get; set; }
        public string Provider_1 { get; set; }
        public string Provider_2 { get; set; }
        public string Provider_3 { get; set; }
        public string Provider_4 { get; set; }
        public DateTime Encouter_CloseTime { get; set; }
        public DateTime Encounter_Entry_Time { get; set; }
        public string Encounter_Closed { get; set; }
        public string Encounter_Type { get; set; }
        public DateTime Contact_Date { get; set; }
        public DateTime Appointment_Time { get; set; }
        public string Appointment_Status { get; set; }
        public DateTime Checkin_Time { get; set; }
        public DateTime Check_Out_Time { get; set; }
        public string First_Name { get; set; }
        public string Middle_Name { get; set; }
        public string Last_Name { get; set; }
        public string SSN { get; set; }
        public DateTime Date_Of_Birth { get; set; }
        public string Gender { get; set; }
        public string Race { get; set; }
        public string Ethnic_Group { get; set; }
        public string Email_Address { get; set; }
        public string Nurse_Wait_Time { get; set; }
        public string PAT_ID { get; set; }
        public string Phys_Wait_Time { get; set; }
        public string Phys_Time_W_Paitent { get; set; }
        public string Post_Visit_Charting { get; set; }
        public string Pre_Visit_Charting { get; set; } 
        public string Referal_ID { get; set; }
        public DateTime Cancelation_Date { get; set; }
        public DateTime Last_Audit_Date { get; set; }
        public string Department_Name { get; set; }
        public string Address_Line_1 { get; set; }
        public string Address_Line_2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip_Code { get; set; }
        public string Living_Status { get; set; }
        public string Visit_Type { get; set; }
        public string Visit_Wait_Time { get; set; }
    }
}