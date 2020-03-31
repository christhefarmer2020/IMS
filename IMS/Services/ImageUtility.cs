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
                                 image_id = x.Image_Id,
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
                                 Consent = x.Consent,
                                 ImageData = x.Image_Data
                             };

                response.Data = result;
                response.IsSuccessful = true;
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

        public ServiceMessage<bool> DeleteImage(int? image_id)
        {
            var response = new ServiceMessage<bool>();
            try
            {
                var imageObj = db.EImage.Find(image_id);
                db.EImage.Remove(imageObj);
                db.SaveChanges();
                response.IsSuccessful = true;
            }
           catch(Exception e)
            {
                response.IsSuccessful = false;
                response.ErrorMessage = e.ToString();
            }
            return response;
        }

        public EncounterImage FindImage(int? id){
            var response = new ServiceMessage<EncounterImage>();
            var imageObj = db.EImage.Find(id);
            response.Data = imageObj;
            return imageObj;
        }
        public void Dispose()
        {
            db.Dispose();
        } 
    }
}