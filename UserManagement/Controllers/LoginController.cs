using BussinessLayer;
using Entities.ResponseObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using UserManagement.Models;

namespace UserManagement.Controllers
{
    public class LoginController : Controller
    {

        [AllowAnonymous]
        public ActionResult Login()
        {
            if (string.IsNullOrEmpty(HttpContext.User.Identity.Name))
            {
                CurrentSession.Remove("activeUser");
                FormsAuthentication.SignOut();
                return View();
            }
            //return Redirect("~/User/UserList");
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        public ActionResult Login(LoginModel user)
        {
            if (ModelState.IsValid)
            {
                UserManager um = new UserManager();
                var res = um.LoginUser(user);
                if (res.Errors.Count > 0)
                {
                    res.Errors.ForEach(g => ModelState.AddModelError("", g));
                    return View(user);
                }
                else
                {                    
                    var activeUser= um.GetUserUserBy(g => g.Email == user.Email && g.Password==user.Password);
                    Session["activeUser"] = activeUser.Result;
                    FormsAuthentication.SetAuthCookie(user.Email, false);
                    return RedirectToAction("Index", "Users");
                }
            }
            return View(user);
        }

        public ActionResult LogOff()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login", "Login");
        }
    }
}