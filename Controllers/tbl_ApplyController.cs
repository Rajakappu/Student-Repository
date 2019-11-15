using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Repository.Models;

namespace Repository.Controllers
{
    public class tbl_ApplyController : Controller
    {
        private readonly StudentRepositoryEntities1 db = new StudentRepositoryEntities1();

        // GET: tbl_Apply
        public ActionResult Index()
        {
            ViewBag.StatusID = new SelectList(db.tbl_Status, "StatusID", "Status");

            var a = db.tbl_Apply.Include(t => t.tbl_Certification).Include(t => t.tbl_Status).Include(t => t.tbl_Student);
            return View(a.ToList());
        }
        [HttpPost]
        public ActionResult Index(int StatusID)
        {
            ViewBag.StatusID = new SelectList(db.tbl_Status, "StatusID", "Status");

            var a = db.tbl_Apply.Include(t => t.tbl_Certification).Include(t => t.tbl_Status).Where(t=>t.StatusID== StatusID).Include(t => t.tbl_Student);
            return View(a.ToList());
        }

        public ActionResult MyIndex()
        {
            ViewBag.StatusID = new SelectList(db.tbl_Status, "StatusID", "Status");

            int a = Convert.ToInt32(Session["StudentID"]);
            var t = db.tbl_Apply.Where(b => b.StudentID == a).ToList();
            return View(t);



        }

        [HttpPost]
        public ActionResult MyIndex(int StatusID)
        {
            ViewBag.StatusID = new SelectList(db.tbl_Status, "StatusID", "Status");

            var a = db.tbl_Apply.Include(t => t.tbl_Certification).Include(t => t.tbl_Status).Where(t => t.StatusID == StatusID).Include(t => t.tbl_Student);
            return View(a.ToList());
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return View("Notfound");
            }
            tbl_Apply tbl_Apply = db.tbl_Apply.Find(id);
            if (tbl_Apply == null)
            {
                return View("Notfound");
            }
            return View(tbl_Apply);
        }


        public ActionResult Create()
        {
            ViewBag.CertificationID = new SelectList(db.tbl_Certification, "CertificationID", "CertificationName");
            ViewBag.StatusID = new SelectList(db.tbl_Status, "StatusID", "Status");
            ViewBag.StudentID = new SelectList(db.tbl_Student, "StudentID", "FirstName");
            return View();
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ApplyId,CertificationID,StudentID,Comments,StatusID")] tbl_Apply tbl_Apply)
        {
            if (ModelState.IsValid)
            {
                tbl_Apply.StatusID = 1;
                db.tbl_Apply.Add(tbl_Apply);
                db.SaveChanges();

                    return Content("<html><body><script>alert('Applied successfully'); window.location.href = '/tbl_Apply/MyIndex';</script></body></html>");

            
            }

          ViewBag.CertificationID = new SelectList(db.tbl_Certification, "CertificationID", "CertificationName", tbl_Apply.CertificationID);
            ViewBag.StatusID = new SelectList(db.tbl_Status, "StatusID", "Status",1, tbl_Apply.tbl_Status.Status);
            ViewBag.StudentID = new SelectList(db.tbl_Student, "StudentID", "FirstName", tbl_Apply.StudentID);
            return View(tbl_Apply);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbl_Apply tbl_Apply = db.tbl_Apply.Find(id);
            if (tbl_Apply == null)
            {
                return View("Notfound");
            }
            ViewBag.CertificationID = new SelectList(db.tbl_Certification, "CertificationID", "CertificationName", tbl_Apply.CertificationID);
            ViewBag.StatusID = new SelectList(db.tbl_Status, "StatusID", "Status", tbl_Apply.StatusID);
            ViewBag.StudentID = new SelectList(db.tbl_Student, "StudentID", "FirstName", tbl_Apply.StudentID);
            return View(tbl_Apply);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ApplyId,CertificationID,StudentID,Comments,StatusID")] tbl_Apply tbl_Apply)
        {
            if (ModelState.IsValid)
            {

                db.Entry(tbl_Apply).State = EntityState.Modified;
                db.SaveChanges();

                return RedirectToAction("Index");
            }
            List<SelectListItem> li = new List<SelectListItem>
            {
                new SelectListItem() { Text = "Request", Value = "1" },
                new SelectListItem() { Text = "Approved", Value = "2" },
                new SelectListItem() { Text = "Cancelled", Value = "3" }
            };

            ViewBag.CertificationID = new SelectList(db.tbl_Certification, "CertificationID", "CertificationName", tbl_Apply.CertificationID);
            //ViewBag.StatusID = new SelectList(db.tbl_Status, "StatusID", "Status", tbl_Apply.StatusID);
            ViewBag.StatusID = new SelectList(li, "Value", "Text");
            ViewBag.StudentID = new SelectList(db.tbl_Student, "StudentID", "FirstName", tbl_Apply.StudentID);
            return View(tbl_Apply);
        }

      

                
        // GET: tbl_Apply/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return View("Notfound");
            }
            tbl_Apply tbl_Apply = db.tbl_Apply.Find(id);
            if (tbl_Apply == null)
            {
                return HttpNotFound();
            }
            return View(tbl_Apply);
        }

     
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tbl_Apply tbl_Apply = db.tbl_Apply.Find(id);
            db.tbl_Apply.Remove(tbl_Apply);
            db.SaveChanges();
            int a = Convert.ToInt32(Session["UserID"]);
            if (a ==0)
            {
                return RedirectToAction("MyIndex");
            }
            else
            {
                return RedirectToAction("Index");


            }
        }

        


    }
}
