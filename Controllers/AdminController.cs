using Repository.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Repository.Controllers
{
    public class AdminController : Controller
    {
        private readonly StudentRepositoryEntities1 Context = new StudentRepositoryEntities1();
        //Admin Can See the Student List
        public ActionResult StudentIndex()
        {
            var data = Context.tbl_Student.ToList();
            return View(data);
        }








        [HttpGet]
        public ActionResult PersonalInfo()
        {
            int a = Convert.ToInt32(Session["StudentID"]);

           

            tbl_Student user = Context.tbl_Student.Find(a);

            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        [HttpGet]
        public ActionResult AdminPersonalInfo()
        {
            int a = Convert.ToInt32(Session["AdminID"]);



            tbl_Admin user = Context.tbl_Admin.Find(a);

            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        public ActionResult AdminEdit(int? id)
        {
            var result = Context.tbl_Admin.SingleOrDefault(a => a.AdminID == id);
            Session["Password"] = result.Password;
            tbl_Admin std = new tbl_Admin();

            std = Context.tbl_Admin.Find(id);
            return View(std);
        }
        [HttpPost]
        public ActionResult AdminEdit(tbl_Admin std)
        {

            var d = Context.tbl_Admin.Where(x => x.AdminID == std.AdminID).FirstOrDefault();
            d.AdminID = std.AdminID;
            d.FirstName = std.FirstName;
            d.LastName = std.LastName;
            d.Password = Convert.ToString(Session["Password"]);
            d.Username = std.Username;
            d.Contact = std.Contact;
            d.Email = std.Email;

            if (TryUpdateModel(d))
            {
                Context.SaveChanges();

                ModelState.Clear();

                return RedirectToAction("AdminPersonalInfo");
            }


            return View();
        }

        public ActionResult Edit(int? id)
        {
            var result = Context.tbl_Student.SingleOrDefault(a => a.StudentID == id);
            Session["Password"] = result.Password;
            Session["DOB"] = result.DateofBirth;
            tbl_Student std = new tbl_Student();
            ViewBag.GraduationID = new SelectList(Context.tbl_Graduation, "GraduationID", "GraduationName");

            std = Context.tbl_Student.Find(id);
            return View(std);
        }
        [HttpPost]
        public ActionResult Edit(tbl_Student std)
        {

            var d = Context.tbl_Student.Where(x => x.StudentID == std.StudentID).FirstOrDefault();
            d.StudentID = std.StudentID;
            d.FirstName = std.FirstName;
            d.LastName = std.LastName;
            d.FatherName = std.FatherName;
            d.DateofBirth = Convert.ToDateTime(Session["DOB"]);
            d.Password = Convert.ToString(Session["Password"]);
            d.GraduationID = std.GraduationID;
            d.Percentage = std.Percentage;
            d.Contact = std.Contact;
            d.Email = std.Email;
            d.City = std.City;

            if (TryUpdateModel(d))
            {
                Context.SaveChanges();

                ModelState.Clear();

                return RedirectToAction("PersonalInfo");
            }

            ViewBag.GraduationID = new SelectList(Context.tbl_Graduation, "GraduationID", "GraduationName", std.GraduationID);

            return View();
        }


        
        //public ActionResult CertificationList()
        //{
        //    var data = Context.tbl_Certification.ToList();
        //    return View(data);
        //}


        public ActionResult CertificationLis2()
        {
            var data = Context.tbl_Certification.ToList();
            return View(data);
        }



        public ActionResult ApplyCertification()
        {
            //int a = Convert.ToInt32(Session["StudentID"]);

            ViewBag.CertificationID = new SelectList(Context.tbl_Certification, "CertificationID", "CertificationName");
            ViewBag.StudentID = new SelectList(Context.tbl_Student, "a", "FirstName");
            ViewBag.StatusID = new SelectList(Context.tbl_Status, "Status", "Status");



            return View();
        }

        [HttpPost]
        public ActionResult ApplyCertification(tbl_Apply u)
        {
            try
            {

                if (u.ApplyId != 0)
                {
                    if (ModelState.IsValid)
                    {
                        tbl_Apply user = new tbl_Apply
                        {
                            ApplyId = u.ApplyId,
                            CertificationID = u.CertificationID,
                            StudentID = u.StudentID,
                            StatusID = u.StatusID,
                        };

                        Context.tbl_Apply.Add(user);

                        Context.SaveChanges();
                    }
                    else
                    {
                        return View();
                    }
                }
                else
                {
                    return View();
                }
                ViewBag.Message = "Successfully Added";
                return RedirectToAction("ApplysList");
            }
            catch (Exception)
            {
                return RedirectToAction("CertificationLis2");
            }
        }


        public ActionResult ApplyList()
        {
            var data = Context.tbl_Apply.ToList();
            return View(data);
        }
    }

}