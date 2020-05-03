using IMS.Models;
using IMS.ViewModels;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Web;

namespace IMS.Services
{
    public class ImageUtility
    {
        private DatabaseConnection db = new DatabaseConnection();


        public ServiceMessage<IEnumerable<searchEncounterVM>> getAllEncounters()
        {
            var response = new ServiceMessage<IEnumerable<searchEncounterVM>>();
            try
            {
                var date1 = DateTime.Today.AddMonths(-1);
                var date2 = DateTime.Today.AddMonths(1);
                var allEncounters = db.Encounters.ToList();
                var result = from all in allEncounters
                             select new searchEncounterVM
                             {
                                 PAT_ENC_CSN_ID = all.PAT_ENC_CSN_ID,
                                 PAT_MRN = all.PAT_MRN_ID,
                                 Providers = all.Provider_1 + ", " + all.Provider_2 + ", " + all.Provider_3 + ", " + all.Provider_4,
                                 Contact_Date = all.Contact_Date,
                                 First_Name = all.First_Name,
                                 Last_Name = all.Last_Name,
                                 Date_Of_Birth = all.Date_Of_Birth.ToShortDateString(),
                                 Department_Name = all.Department_Name,
                                 Visit_Type = all.Visit_Type,
                                 Gender = all.Gender
                             };
                result = result.Where(d => d.Contact_Date >= date1 && d.Contact_Date <= date2);

                response.Data = result;
            }
            catch (Exception ex)
            {
                response.IsSuccessful = false;
                response.ErrorMessage = ex.ToString();
            }
            return response;
        }

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
                                  Contact_Date = imageTable.Appointment_Time.ToShortDateString(),
                                  First_Name = joined?.First_Name,
                                  Last_Name = joined?.Last_Name,
                                  Date_Of_Birth = joined?.Date_Of_Birth.ToShortDateString(),
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
            int counter = 1;
            try 
            {
                foreach (var item in createVM.Images)
                {
                    byte[] imageByte = new byte[item.ContentLength];
                    item.InputStream.Read(imageByte, 0, item.ContentLength);
                    string consent = createVM.Consent.ToString();
                    string MRN = createVM.PAT_MRN;
                    //create DOV for file name
                    string DOV = createVM.Appointment_Time.ToString().Replace('/', '.');
                    //adding name and DOV for the file extension
                    string saveTo = CreateFolders(createVM.LastName.ToString() + ", " + createVM.FirstName.ToString() + " DOV " + DOV + " (" +counter.ToString() + ")" + ".jpeg", createVM);                    
                    FileStream writeStream = new FileStream(saveTo, FileMode.Create, FileAccess.Write);
                    counter++;
                    writeStream.Write(imageByte, 0, item.ContentLength);
                    writeStream.Close();

                    var encounterImage = new EncounterImage();  
                    encounterImage.Appointment_Time = DateTime.Parse(createVM.Appointment_Time);
                    encounterImage.Consent = createVM.Consent.ToString();
                    encounterImage.PAT_MRN = MRN;
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
        
        private string CreateFolders(string FileName, CreateVM createVM)
        {
            // Specify a name for your top-level folder.
            string folderName= System.Configuration.ConfigurationManager.AppSettings["source"];

            // To create a string that specifies the path to a subfolder under your 
            // top-level folder, add a name for the subfolder to folderName.
            string DOB = createVM.DOB.ToString().Replace('/', '.');
            string pathString = System.IO.Path.Combine(folderName, createVM.LastName.ToString() + ", " + createVM.FirstName.ToString() + " DOB " + DOB);

            // Create the subfolder. You can verify in File Explorer that you have this
            // structure in the C: drive.
            //    Local Disk (C:)
            //        folderName
            //            pathString
            System.IO.Directory.CreateDirectory(pathString);

            // Use Combine again to add the file name to the path.
            pathString = System.IO.Path.Combine(pathString, FileName);

            // Verify the path that you have constructed.
            Console.WriteLine("Path to my file: {0}\n", pathString);

            // Check that the file doesn't already exist. If it doesn't exist, create
            // the file and write integers 0 - 99 to it.
            // DANGER: System.IO.File.Create will overwrite the file if it already exists.
            // This could happen even with random file names, although it is unlikely.
            if (!System.IO.File.Exists(pathString))
            {
                using (System.IO.FileStream fs = System.IO.File.Create(pathString))
                {
                    for (byte i = 0; i < 100; i++)
                    {
                        fs.WriteByte(i);
                    }
                }
            }
            else
            {
                Console.WriteLine("File \"{0}\" already exists.", FileName);
            }

            // Read and display the data from your file.
            try
            {
                byte[] readBuffer = System.IO.File.ReadAllBytes(pathString);
                foreach (byte b in readBuffer)
                {
                    Console.Write(b + " ");
                }
                Console.WriteLine();
            }
            catch (System.IO.IOException e)
            {
                Console.WriteLine(e.Message);
            }            

            return pathString;
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
        public Encounter GetEcnounter(string PAT_ENC_CSN_ID)
        {
            var obj = db.Encounters.Find(PAT_ENC_CSN_ID);
            return obj;
        }
        public void Dispose()
        {
            db.Dispose();
        } 
    }
}