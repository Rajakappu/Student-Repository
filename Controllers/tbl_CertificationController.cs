using System;    
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Repository.Models;

namespace Repository.Controllers
{
    public class tbl_CertificationController : Controller
    {
        private StudentRepositoryEntities1 db = new StudentRepositoryEntities1();

        public ActionResult Index()
        {
            return View(db.tbl_Certification.ToList());
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return View("Notfound");
            }
            tbl_Certification tbl_Certification = db.tbl_Certification.Find(id);
            if (tbl_Certification == null)
            {
                return View("Notfound");
            }
            return View(tbl_Certification);
        }

        public ActionResult Create()
        {
            return View();
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create( tbl_Certification tbl_Certification)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.tbl_Certification.Add(tbl_Certification);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch(Exception)
            {
                TempData["Message"] = "These details are already existed";

            }



            return View(tbl_Certification);
        }

        
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return View("Notfound");
            }
            tbl_Certification tbl_Certification = db.tbl_Certification.Find(id);
            if (tbl_Certification == null)
            {
                return View("Notfound");
            }
            return View(tbl_Certification);
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit( tbl_Certification tbl_Certification)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tbl_Certification).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tbl_Certification);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return View("Notfound");
            }
            tbl_Certification tbl_Certification = db.tbl_Certification.Find(id);
            if (tbl_Certification == null)
            {
                return View("Notfound");
            }
            return View(tbl_Certification);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tbl_Certification tbl_Certification = db.tbl_Certification.Find(id);
            db.tbl_Certification.Remove(tbl_Certification);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        
    }
}
