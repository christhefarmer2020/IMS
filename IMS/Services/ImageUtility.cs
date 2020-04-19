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
                                  Providers = joined?.Provider_1 + ", " + joined?.Provider_2 + ", " + joined?.Provider_3 + " , " + joined?.Provider_4,
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
                //var result = from e in allEncounters
                //             join x in allEncounterImages on e.PAT_MRN_ID equals x.PAT_MRN into j
                //             from x in j.DefaultIfEmpty()
                //             select new IndexVM
                //             {
                //                 image_id = x.Image_Id,
                //                 PAT_MRN = e.PAT_MRN_ID,
                //                 Provider_1 = e.Provider_1,
                //                 Provider_2 = e.Provider_2,
                //                 Provider_3 = e.Provider_3,
                //                 Provider_4 = e.Provider_4,
                //                 Contact_Date = e.Contact_Date,
                //                 Appointment_Time = e.Appointment_Time,
                //                 First_Name = e.First_Name,
                //                 Last_Name = e.Last_Name,
                //                 Date_Of_Birth = e.Date_Of_Birth,
                //                 Department_Name = e.Department_Name,
                //                 Visit_Type = e.Visit_Type,
                //                 Consent = x.Consent,
                //                 ImageData = x.Image_Data
                //             };

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
                foreach (var item in createVM.Images)
                {
                    byte[] imageByte = new byte[item.ContentLength];
                    item.InputStream.Read(imageByte, 0, item.ContentLength);
                    string consent = createVM.Consent.ToString();

                    string DOV = createVM.Appointment_Time.ToString().Replace('/', '.');
                    DOV = DOV.Substring(0, DOV.IndexOf(" "));
                    //string saveTo = CreateFolders(createVM);
                    string saveTo = CreateFolders(createVM.LastName.ToString() + ", " + createVM.FirstName.ToString() + " DOV " + DOV + ".jpeg", createVM);
                    //string saveTo = CreateFolders(createVM.LastName.ToString() + ", " + createVM.FirstName.ToString() + " DOV " + DOV);
                    //CreateFolders("Last, First DOV 4.8.2020");
                    FileStream writeStream = new FileStream(saveTo, FileMode.Create, FileAccess.Write);
                    writeStream.Write(imageByte, 0, item.ContentLength);
                    writeStream.Close();

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
        
        private string CreateFolders(string FileName, CreateVM createVM)
        {
            // Specify a name for your top-level folder.
            string folderName= System.Configuration.ConfigurationManager.AppSettings["source"];

            //string folderName = "c://Users//Ryan//Desktop//";
            //string folderName = ConfigurationManager.AppSettings["filePath"].ToString();
            //string folderName = System.Web.Configuration.WebConfigurationManager.AppSettings["filePath"].ToString();

            // To create a string that specifies the path to a subfolder under your 
            // top-level folder, add a name for the subfolder to folderName.
            string DOB = createVM.DOB.ToString().Replace('/', '.');
            DOB = DOB.Substring(0, DOB.IndexOf(" "));
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
                //return;
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
        public void Dispose()
        {
            db.Dispose();
        } 
    }
}