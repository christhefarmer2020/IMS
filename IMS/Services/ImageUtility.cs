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
                                 PAT_MRN = e.PAT_MRN_ID,
                                 Provider_1 = e.Provider_1,
                                 Provider_2 = e.Provider_2,
                                 Provider_3 = e.Provider_3,
                                 Provider_4 = e.Provider_4,
                                 Contact_Date = e.Contact_Date,
                                 Appointment_Time = e.Appointment_Time,
                                 First_Name = e.First_Name,
                                 Last_Name = e.Last_Name,
                                 Date_Of_Birth = e.Date_Of_Birth,
                                 Department_Name = e.Department_Name,
                                 Visit_Type = e.Visit_Type,
                                 Consent = x.Consent
                                 // ImageData = x.ImageData
                             };

                response.Data = result;
                response.IsSuccessful = true;
                int a = 3;
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

            try 
            {
                //var byteArray = new byte[createVM.Images.ContentLength];
                foreach (var item in createVM.Images)
                {
                    byte[] imageByte = new byte[item.ContentLength];
                    item.InputStream.Read(imageByte, 0, item.ContentLength);
                    var encounterImage = new EncounterImage();
                    encounterImage.Appointment_Time = createVM.Appointment_Time;
                    encounterImage.Consent = createVM.Consent.ToString();
                    encounterImage.PAT_MRN = createVM.PAT_MRN_NUM;
                    encounterImage.Image_Data = imageByte;
                    db.EImage.Add(encounterImage);
                    db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                response.IsSuccessful = false;
                response.ErrorMessage = ex.ToString();
            }


            response.IsSuccessful = true;
            response.Data = true;
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