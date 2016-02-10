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
            var cookie2 = Request.Cookies["222"];
            var value = cookie2.Value;
            var value1 = cookie2.Name;
            var value2 = cookie2.Expires;
            return View();
        }
        [HttpPost]
        public ActionResult LoginView(LoginClass model)
        {
            
            if (ModelState.IsValid)
            {
                // поиск пользователя в бд
                User user = null;
                using (DataContext db = new DataContext())
                {
                    user = db.Users.FirstOrDefault(u => u.Email == model.Email && u.Password == model.Password);

                }
                if (user != null)
                {
                    FormsAuthentication.SetAuthCookie(model.Email, true);
                    return RedirectToAction("PersonalOffice", "Account",user);
                }
                else
                {
                    ModelState.AddModelError("", "Пользователя с таким логином и паролем нет");
                }
            }

            return View(model);
        }
        public string Index()
        {
            string result = "Вы не авторизованы";
            if (User.Identity.IsAuthenticated)
            {
                result = "Ваш логин: " + User.Identity.Name;
            }
            return result;
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
                    user = db.Users.FirstOrDefault(u => u.Email == model.Email);
                }
                if (user == null)
                {
                    // создаем нового пользователя
                        db.Users.Add(new User {Email = model.Email, Password = model.Password});
                        db.SaveChanges();

                        user = db.Users.Where(u => u.Email == model.Email && u.Password == model.Password).FirstOrDefault();
                    // если пользователь удачно добавлен в бд
                    if (user != null)
                    {
                        FormsAuthentication.SetAuthCookie(model.Email, true);
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
        
    }
}
