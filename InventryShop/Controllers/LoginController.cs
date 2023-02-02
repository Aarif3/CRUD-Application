using InventryShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;
using System.Web.Security;

namespace InventryShop.Controllers
{
    public class LoginController : Controller
    {
        ProductContext db =new ProductContext();

        // GET: Login
        public ActionResult SignUP()
        {
            return View();
        }

        [HttpPost]
        public ActionResult SignUP(SignUp sp)
        {
            SignUp s = new SignUp();
            s.UserName= sp.UserName;
            s.UserEmail = sp.UserEmail;
            s.Password = sp.Password;
            s.ConfirmPassword = sp.ConfirmPassword;
            db.SignupTbl.Add(s);
            db.SaveChanges();
            return View("Login");
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(SignUp sp,string ReturnUrl)
        {
            var s = db.SignupTbl.FirstOrDefault(model => model.UserName == sp.UserName && model.Password == sp.Password);
            if(s != null)
            {
                FormsAuthentication.SetAuthCookie(sp.UserName, false);
                if (Url.IsLocalUrl(ReturnUrl) && ReturnUrl.Length > 1 && ReturnUrl.StartsWith("/"))
                {
                    return Redirect(ReturnUrl);
                        
                }
                return RedirectToAction("Index", "Home");

            }
            else
            {
                ViewBag.Message = "InValid Email or Password";
            }
            return View();
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login");
        }
    }
}