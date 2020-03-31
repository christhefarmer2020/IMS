﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using IMS.Models;
using IMS.Services;
using IMS.ViewModels;

namespace IMS.Controllers
{
    public class EncounterImagesController : Controller
    {
        private static string createAlert = "";
        public static string deleteAlert = "";

        private readonly ImageUtility ServiceCall = new ImageUtility();

        public ActionResult Index()
        {
            var response = ServiceCall.getAll();
            IEnumerable<IndexVM> IndexVM = response.Data;
            ViewBag.CreateAlert = createAlert;
            createAlert = "";
            ViewBag.DeleteAlert = deleteAlert;
            deleteAlert = "";
            return View(IndexVM);
        }

        public ActionResult Create()
        {
            var vm = new CreateVM();
            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CreateVM createVM)
        {
            if (ModelState.IsValid)
            {
                var response = ServiceCall.AddImage(createVM);
                createAlert = response.IsSuccessful.ToString();
            }
            return RedirectToAction("Index");
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var vm = ServiceCall.FindImage(id);
            if (vm == null)
            {
                return HttpNotFound();
            }
            return View(vm);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var response = ServiceCall.DeleteImage(id);
            deleteAlert = response.IsSuccessful.ToString();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                ServiceCall.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
