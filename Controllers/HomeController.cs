using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Repository.Models;
using Repository.Models.Extended;

namespace Repository.Controllers
{
    public class HomeController : Controller
    {
       public ActionResult LoginCenter()
        {
            Session.Abandon();
            return View();


        }
        public ActionResult StudentLogin()
        {
            return View();

        }
        [HttpPost]
        public ActionResult StudentLogin(Login objUser)
        {
            if (!ModelState.IsValid)
            {
                return View(objUser);
            }
            else
            {
                using (StudentRepositoryEntities1 db = new StudentRepositoryEntities1())
                {

                    var obj = db.tbl_Student.Where(a => a.Email.Equals(objUser.Email) && a.Password.Equals(objUser.Password)).FirstOrDefault();

                    if (obj != null)
                    {

                        //Session["UserID"] = obj.UserID.ToString();
                        //Session["UserName"] = obj.UserName.ToString();

                        Session["StudentID"] = obj.StudentID.ToString();
                        Session["FirstName"] = obj.FirstName.ToString();
                        Session["Email"] = obj.Email.ToString();
                        Session["GraduationName"]= obj.tbl_Graduation.GraduationName.ToString();

                        Session["Contact"] = obj.Contact.ToString();
                        Session["City"] = obj.City.ToString();




                        return Content("<html><body><script>alert('Login successfully'); window.location.href = '/Home/Studentroles';</script></body></html>");


                    }
                    else
                    {
                        TempData["Message"] = "InValid Credentials ";

                    }
                    return View(obj);
                }
            }
        }

        public ActionResult AdminLogin()
        {

            return View();

        }
        [HttpPost]
        public ActionResult AdminLogin(tbl_Admin objUser)
        {
            if (!ModelState.IsValid)
            {
                return View(objUser);
            }
            else
            {
                using (StudentRepositoryEntities1 db = new StudentRepositoryEntities1())
                {

                    var obj = db.tbl_Admin.Where(a => a.Username.Equals(objUser.Username) && a.Password.Equals(objUser.Password)).FirstOrDefault();
                    

                    if (obj != null)
                    {

                        //Session["UserID"] = obj.UserID.ToString();
                        //Session["UserName"] = obj.UserName.ToString();
                        Session["Email"] = obj.Email.ToString();

                        Session["Username"] = obj.Username.ToString();
                        Session["FirstName"] = obj.FirstName.ToString();
                        Session["AdminID"] = obj.AdminID.ToString();
                        Session["Password"] = obj.Password.ToString();





                        if (obj.AdminID!=0)
                        {

                            return Content("<html><body><script>alert('Login successfully'); window.location.href = '/Home/Adminroles';</script></body></html>");


                        }


                    }
                    else
                    {


                        TempData["Message"] = "InValid Credentials ";

                    }

                    return View(obj);
                }
            }
        }
        public ActionResult Adminroles()
        {

            return View();
        }
        public ActionResult Studentroles()
        {
            return View();
        }

        public ActionResult ResetPassword()
        {
            return View();
        }
        //GET:ResetPassword
        [HttpPost]
        public ActionResult ResetPassword(tbl_Student obj)
        {
            int a = Convert.ToInt32(Session["StudentID"]);
            using (StudentRepositoryEntities1 db = new StudentRepositoryEntities1())
            {
                tbl_Student u = db.tbl_Student.Find(a);
                var b = db.tbl_Student.Where(e => e.Email.Equals(obj.Email) && e.FatherName.Equals(obj.FatherName)).FirstOrDefault();
                if (b != null)
                {
                    b.Password = obj.Password;
                    db.Entry(b).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                }
                return Content("<html><body><script>alert('Password Changed successfully'); window.location.href = '/Home/StudentLogin';</script></body></html>");


                //return RedirectToAction("StudentLogin");

            }
        }
        public ActionResult ResetPasswordforAdmin()
        {
            return View();
        }
        //GET:ResetPassword
        [HttpPost]
        public ActionResult ResetPasswordforAdmin(tbl_Student obj)
        {
            int a = Convert.ToInt32(Session["AdminID"]);
            using (StudentRepositoryEntities1 db = new StudentRepositoryEntities1())
            {
                tbl_Admin u = db.tbl_Admin.Find(a);
                var b = db.tbl_Admin.Where(e => e.Email.Equals(obj.Email) && e.LastName.Equals(obj.LastName)&& e.Contact.Equals(obj.Contact)).FirstOrDefault();
                if (b != null)
                {
                    b.Password = obj.Password;
                    db.Entry(b).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                }
                return Content("<html><body><script>alert('Password Changed successfully'); window.location.href = '/Home/StudentLogin';</script></body></html>");


                //return RedirectToAction("StudentLogin");

            }
        }
        [HttpGet]
        public ActionResult MyAccount()
        {
            int a = Convert.ToInt32(Session["StudentID"]);

            using (StudentRepositoryEntities1 db = new StudentRepositoryEntities1())
            {

                tbl_Student std = db.tbl_Student.Find(a);
                if (std == null)
                {

                    return View("Details not Found");

                }

                return View();
            }
        }
            
            public ActionResult Logout()
            {
            FormsAuthentication.SignOut();
            Session.Clear();
            return RedirectToAction("AdminLogin","Home");

        }
    }
}
