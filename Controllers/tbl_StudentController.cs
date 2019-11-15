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
    public class tbl_StudentController : Controller
    {
        private readonly StudentRepositoryEntities1 db = new StudentRepositoryEntities1();


        // GET: Jobs/Edit/5
        public ActionResult Edit2(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbl_Student job = db.tbl_Student.Find(id);
            if (job == null)
            {
                return HttpNotFound();
            }
            ViewBag.GraduationID = new SelectList(db.tbl_Graduation, "GraduationID", "GraduationName", job.GraduationID);

            return View(job);
        }

        // POST: Jobs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit2([Bind(Include = "JobID,CompanyName,Details,Designation,LocationID,Number_of_Positions")] tbl_Student job)
        {
            if (ModelState.IsValid)
            {
                db.Entry(job).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.GraduationID = new SelectList(db.tbl_Graduation, "GraduationID", "GraduationName", job.GraduationID);

            return View(job);
        }

        public ActionResult Index()
        {
            try
            {
                if (Session["AdminID"] != null)
                {
                    var query = from students in db.tbl_Student select students;
                    return View(query);
                }
                else
                {
                    return RedirectToAction("AdminLogin", "Home");
                }
            }
            catch (Exception)
            {
                TempData["message"] = "Some error occured!";
                return View();
            }
        }

        [HttpPost]
        public ActionResult Index(int StudentID,string Dob,string FirstName, string LastName,string FatherName,string Contact,string Email,string City,string GraduationName)
        {
            try
            {
                if (Session["AdminID"] != null)
                {
                    if (StudentID != 0)
                   {
                       var query = from students in db.tbl_Student where students.StudentID == StudentID select students;
                      return View(query);
                    }     
                    else if (FirstName !=null )
                    {
                        var query = from students in db.tbl_Student where students.FirstName == FirstName select students;
                        return View(query);
                    }
                    else if (LastName == "")
                    {
                        var query = from students in db.tbl_Student where students.LastName == LastName select students;
                        return View(query);
                    }
                    else if (FatherName == "")
                    {
                        var query = from students in db.tbl_Student where students.FatherName == FatherName select students;
                        return View(query);
                    } 
                    else if (Contact=="")
                    {
                        var query = from students in db.tbl_Student where students.Contact == Contact select students;
                        return View(query);
                    }
                    else if (Dob == "")
                    {
                        var query = from students in db.tbl_Student where students.DateofBirth == Convert.ToDateTime(Dob) select students;
                        return View(query);
                    }   
                    else if(City=="")
                    {
                        var query = from students in db.tbl_Student where students.City == City  select students;
                        return View(query);

                    }
                    else
                    {
                        var tbl_Student = db.tbl_Student.Include(t => t.tbl_Graduation);

                        var query = from students in db.tbl_Student where students.tbl_Graduation.GraduationName == GraduationName select students;
                        return View(query);
                    }

                }
                else 
                {
                    return RedirectToAction("AdminLogin", "Home");
                }
            }
            catch (Exception)
            {
                TempData["message"] = "Some error occured!";
                return View();
            }
        }

        //public ActionResult Index()
        //{
            
        //    var tbl_Student = db.tbl_Student.Include(t => t.tbl_Graduation);
        //    return View(tbl_Student.ToList());
        //}

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbl_Student tbl_Student = db.tbl_Student.Find(id);
            if (tbl_Student == null)
            {
                return HttpNotFound();
            }
            return View(tbl_Student);
        }

        public ActionResult Create()
        { 
            ViewBag.GraduationID = new SelectList(db.tbl_Graduation, "GraduationID", "GraduationName");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "StudentID,FirstName,LastName,FatherName,DateofBirth,GraduationID,Percentage,Contact,Email,Password,City")] tbl_Student tbl_Student)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.tbl_Student.Add(tbl_Student);
                    db.SaveChanges();
                    //return RedirectToAction("StudentLogin", "Home");
                    //return Content("<html><body><script>alert('Successfully Registered');Window.location.href='/Home/StudentLogin'</script></body></html>");
                    return Content("<html><body><script>alert('Registerd successfully'); window.location.href = '/Home/StudentLogin';</script></body></html>");

                }
            }
            catch(Exception)
            {
                TempData["Message"] = "The email or phone number or password are already existed,Please give differnt one ";

            }

            ViewBag.GraduationID = new SelectList(db.tbl_Graduation, "GraduationID", "GraduationName", tbl_Student.GraduationID);

            return View(tbl_Student);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return View("NotFound");
            }
            tbl_Student tbl_Student = db.tbl_Student.Find(id);
            if (tbl_Student == null)
            {
                return View("NotFound");
            }
            ViewBag.GraduationID = new SelectList(db.tbl_Graduation, "GraduationID", "GraduationName", tbl_Student.GraduationID);
            return View(tbl_Student);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "FirstName,LastName,FatherName,GraduationID,Percentage,Contact,City")] tbl_Student tbl_Student)
        {
            //if (ModelState.IsValid)
            //{
                db.Entry(tbl_Student).State = EntityState.Modified;
            //tbl_Student.GraduationID = 1;
                db.SaveChanges();
                return RedirectToAction("Index");
            //}
            //ViewBag.GraduationID = new SelectList(db.tbl_Graduation, "GraduationID", "GraduationName", tbl_Student.GraduationID);
            //return View(tbl_Student);
        }
        //public ActionResult Edit(int? id)
        //{
        //    try
        //    {
        //        if (Session["AdminID"] != null)
        //        {
        //            tbl_Student student = new tbl_Student();
        //            student = db.tbl_Student.Find(id);
        //            ViewBag.GraduationID = new SelectList(db.tbl_Graduation, "GraduationID", "GraduationName", student.GraduationID);

        //            return View(student);


        //        }
        //        else
        //        {
        //            return RedirectToAction("AdminLogin", "Home");
        //        }
        //    }
        //    catch (Exception)
        //    {
        //        TempData["message"] = "Some error occured!";
        //        return View();
        //    }
        //}

        //[HttpPost]
        //public ActionResult Edit(tbl_Student student)
        //{
        //    try
        //    {
        //        if (ModelState.IsValid)
        //        {
        //            //if (Session["AdminID"] != null)
        //            //{
        //                var studentEdit = db.tbl_Student.Where(s => s.StudentID == student.StudentID).FirstOrDefault();
        //                studentEdit.FirstName = student.FirstName;
        //                studentEdit.LastName = student.LastName;
        //                studentEdit.FatherName = student.FatherName;
        //                studentEdit.DateofBirth = student.DateofBirth;
        //                studentEdit.GraduationID = student.GraduationID;
        //                studentEdit.Percentage = student.Percentage;
        //                studentEdit.Contact = student.Contact;
        //                studentEdit.Email = student.Email;

        //                studentEdit.Password = student.Password;
        //                studentEdit.City = student.City;


        //                if (TryUpdateModel(studentEdit))
        //                {

        //                    db.SaveChanges();
        //                    TempData["message"] = "Updated Successfully!!";
        //                    return RedirectToAction("Index");
        //                }
        //                ViewBag.GraduationID = new SelectList(db.tbl_Graduation, "GraduationID", "GraduationName", student.GraduationID);

        //                return RedirectToAction("Index");
        //            //}
        //            //else
        //            //{
        //            //    ViewBag.GraduationID = new SelectList(db.tbl_Graduation, "GraduationID", "GraduationName", student.GraduationID);

        //            //    return RedirectToAction("AdminLogin", "Home");
        //            //}
        //        }
        //        ViewBag.GraduationID = new SelectList(db.tbl_Graduation, "GraduationID", "GraduationName", student.GraduationID);

        //        return View();
        //    }
        //    catch (Exception)
        //    {
        //        TempData["message"] = "Some error occured!";
        //        return View();
        //    }
        //}

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbl_Student tbl_Student = db.tbl_Student.Find(id);
            if (tbl_Student == null)
            {
                return HttpNotFound();
            }
            return View(tbl_Student);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tbl_Student tbl_Student = db.tbl_Student.Find(id);
            db.tbl_Student.Remove(tbl_Student);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

    }
}
