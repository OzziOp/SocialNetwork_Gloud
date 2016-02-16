using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SocialNetwork.Models;
using System.Data.Entity;
using System.Data;
using System.Web.Security;

namespace SocialNetwork.Controllers
{
    public class AccountController : Controller
    {
        DataContext db = new DataContext();
        [HttpGet]
        public ActionResult LoginView()
        {
            return View();
        }
        [HttpPost]
        public ActionResult LoginView(LoginClass model, string returnUrl)
        {
            
            if (ModelState.IsValid)
            {
                using (DataContext db = new DataContext())
                {
                    string userEmail = model.Login;
                    string Password = model.Password;
                    bool userValid = db.Users.Any(u => u.Login == userEmail && u.Password == Password);
                    if (userValid)
                    {
                        FormsAuthentication.SetAuthCookie(userEmail, false);
                        if (Url.IsLocalUrl(returnUrl) && returnUrl.Length > 1 && returnUrl.StartsWith("/")
                    && !returnUrl.StartsWith("//") && !returnUrl.StartsWith("/\\"))
                        {
                            return Redirect(returnUrl);
                        }
                        else
                        {
                            return RedirectToAction("PersonalOffice", "Account",model);
                        }
                    }
                    else
                    {
                        ModelState.AddModelError("", "Пользователя с таким логином и паролем нет");
                    }
                }
            }

            return View(model);
        }
        [HttpGet]
        public ActionResult RegisterView()
        {
            return View();
        }
        [HttpPost]
        public ActionResult RegisterView(RegisterationClass model)
        {
            if (ModelState.IsValid)
            {
                User user = null;
                using (DataContext db = new DataContext())
                {
                    user = db.Users.FirstOrDefault(u => u.Login == model.Login);
                }
                if (user == null)
                {
                    db.Users.Add(new User { Login = model.Login, Password = model.Password });
                        db.SaveChanges();

                        user = db.Users.Where(u => u.Login == model.Login && u.Password == model.Password).FirstOrDefault();
                    if (user != null)
                    {
                        FormsAuthentication.SetAuthCookie(model.Login, true);
                        return RedirectToAction("PersonalOffice", "Account",user);
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Пользователь с таким логином уже существует");
                }
            }
            return View(model);
        }
        [HttpGet]
        public ActionResult PersonalOffice(User user)
        {
            return View(user);
        }
        [HttpGet]
        public ActionResult EditUser(EditUser user)
        {
            return View(user);
        }
        [HttpPost]
        public ActionResult EditUser(User user)
        {
            return View();
        }
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();

            return RedirectToAction("LoginView", "Account");
        }
        
    }
}
