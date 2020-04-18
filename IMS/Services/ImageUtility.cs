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
                var result1 = from imageTable in allEncounterImages
                              join encounters in allEncounters on imageTable.PAT_MRN equals encounters.PAT_MRN_ID into joinedList
                              from joined in joinedList.DefaultIfEmpty()
                              select new IndexVM
                              {
                                  image_id = imageTable.Image_Id,
                                  PAT_MRN = imageTable.PAT_MRN,
                                  Providers = joined?.Provider_1 + ", " + joined?.Provider_2 + ", " + joined?.Provider_3 + ", " + joined?.Provider_4,
                                  Contact_Date = imageTable.Appointment_Time,
                                  First_Name = joined?.First_Name,
                                  Last_Name = joined?.Last_Name,
                                  Date_Of_Birth = joined?.Date_Of_Birth,
                                  Department_Name = joined?.Department_Name,
                                  Visit_Type = joined?.Visit_Type,
                                  Consent = imageTable.Consent,
                                  ImageData = imageTable.Image_Data,
                                  Gender = joined?.Gender
                              };

                response.Data = result1;
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
                    encounterImage.Appointment_Time = DateTime.Parse(createVM.Appointment_Time);
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