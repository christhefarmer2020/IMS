using IMS.Models;
using IMS.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IMS.Services
{
    public class ImageUtility
    {
        private DatabaseConnection db = new DatabaseConnection();

        public ServiceMessage<IEnumerable<IndexVM>> getAll()
        {
            var response = new ServiceMessage<IEnumerable<IndexVM>>();
            try
            {
                var allEncounters = db.Encounters.ToList();
                var allEncounterImages = db.EImage.ToList();
                var result = from e in allEncounters
                             join x in allEncounterImages on e.PAT_MRN_ID equals x.PAT_MRN
                             select new IndexVM
                             {
                                 PAT_MRN = e?.PAT_MRN_ID,
                                 Provider_1 = e?.Provider_1,
                                 Provider_2 = e?.Provider_2,
                                 Provider_3 = e?.Provider_3,
                                 Provider_4 = e?.Provider_4,
                                 Contact_Date = e?.Contact_Date,
                                 Appointment_Time = e?.Appointment_Time,
                                 First_Name = e?.First_Name,
                                 Last_Name = e?.Last_Name,
                                 Date_Of_Birth = e?.Date_Of_Birth,
                                 Department_Name = e?.Department_Name,
                                 Visit_Type = e?.Visit_Type,
                                 Consent = x.Consent
                                 // ImageData = x.ImageData
                             };
                response.Data = result;
                response.IsSuccessful = true;
                int a = 1;
            }
            catch(Exception ex)
            {
                response.IsSuccessful = false;
                response.ErrorMessage = ex.ToString();
            }

            return response;
        }
        public ServiceMessage<bool> AddImage(CreateVM createVM)
        {
            var response = new ServiceMessage<bool>();

            // Insert Try Catch 
                // create private save method and call it here
                // convert the image to the correct format
            // proper Exception handeling here

            return response;
        }

        public ServiceMessage<bool> DeleteImage(int? id)
        {
            var response = new ServiceMessage<bool>();
            // Insert Try Catch
            // perform the delete
            // save the changes
            // The following code may be of some assistance 
                // EncounterImage encounterImage = db.EImage.Find(id);
                // db.EImage.Remove(encounterImage);
                // db.SaveChanges();
            return response;
        }

        public ServiceMessage<EncounterImage> FindImage(int? id){
            var response = new ServiceMessage<EncounterImage>();
            
            return response;
        }
        public void Dispose()
        {
            db.Dispose();
        }
        private bool SaveToDatabase()
        {
            return false;
        }

        
    }
}