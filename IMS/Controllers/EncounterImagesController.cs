using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using IMS.Models;
using IMS.Services;

namespace IMS.Controllers
{
    public class EncounterImagesController : Controller
    {
        private ImageUtility ServiceCall = new ImageUtility();
        // The db Connection will be moved to the Service folder
        private DatabaseConnection db = new DatabaseConnection();

        // GET: EncounterImages
        public ActionResult Index()
        {
            return View(db.EImage.ToList());
        }

        public ActionResult Create()
        {
            return View();
        }

        // POST: EncounterImages/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(EncounterImage encounterImage)
        {
            if (ModelState.IsValid)
            {
                db.EImage.Add(encounterImage);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(encounterImage);
        }

        // GET: EncounterImages/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EncounterImage encounterImage = db.EImage.Find(id);
            if (encounterImage == null)
            {
                return HttpNotFound();
            }
            return View(encounterImage);
        }

        // POST: EncounterImages/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            EncounterImage encounterImage = db.EImage.Find(id);
            db.EImage.Remove(encounterImage);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
