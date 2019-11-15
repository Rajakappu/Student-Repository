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
    public class ApplyjobsController : Controller
    {
        private StudentRepositoryEntities1 db = new StudentRepositoryEntities1();

        // GET: Applyjobs
        public ActionResult Index()
        {
            var applyjobs = db.ApplyJobs.Include(a => a.Job).Include(a => a.tbl_Student);
            return View(applyjobs.ToList());
        }
        public ActionResult MyIndex()
        {

            int a = Convert.ToInt32(Session["StudentID"]);
            var t = db.ApplyJobs.Where(b => b.StudentID == a).ToList();
            return View(t);



        }


        // GET: Applyjobs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ApplyJob applyjob = db.ApplyJobs.Find(id);
            if (applyjob == null)
            {
                return HttpNotFound();
            }
            return View(applyjob);
        }

        // GET: Applyjobs/Create
        public ActionResult Create()
        {
            ViewBag.JobID = new SelectList(db.Jobs, "JobID", "CompanyName");
            ViewBag.StudentID = new SelectList(db.tbl_Student, "StudentID", "FirstName");
            return View();
        }

        // POST: Applyjobs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ApplyId,JobID,StudentID,Skills")] ApplyJob applyjob)
        {
            if (ModelState.IsValid)
            {
                db.ApplyJobs.Add(applyjob);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.JobID = new SelectList(db.Jobs, "JobID", "CompanyName", applyjob.JobID);
            ViewBag.StudentID = new SelectList(db.tbl_Student, "StudentID", "FirstName", applyjob.StudentID);
            return View(applyjob);
        }

        // GET: Applyjobs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ApplyJob applyjob = db.ApplyJobs.Find(id);
            if (applyjob == null)
            {
                return HttpNotFound();
            }
            ViewBag.JobID = new SelectList(db.Jobs, "JobID", "CompanyName", applyjob.JobID);
            ViewBag.StudentID = new SelectList(db.tbl_Student, "StudentID", "FirstName", applyjob.StudentID);
            return View(applyjob);
        }

        // POST: Applyjobs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ApplyId,JobID,StudentID,Skills")] ApplyJob applyjob)
        {
            if (ModelState.IsValid)
            {
                db.Entry(applyjob).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.JobID = new SelectList(db.Jobs, "JobID", "CompanyName", applyjob.JobID);
            ViewBag.StudentID = new SelectList(db.tbl_Student, "StudentID", "FirstName", applyjob.StudentID);
            return View(applyjob);
        }

        // GET: Applyjobs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ApplyJob applyjob = db.ApplyJobs.Find(id);
            if (applyjob == null)
            {
                return HttpNotFound();
            }
            return View(applyjob);
        }

        // POST: Applyjobs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ApplyJob applyjob = db.ApplyJobs.Find(id);
            db.ApplyJobs.Remove(applyjob);
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
