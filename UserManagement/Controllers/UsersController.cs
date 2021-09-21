using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BussinessLayer;
using Entities;
using Entities.ResponseObject;
using UserManagement.Models;

namespace UserManagement.Controllers
{
    public class UsersController : Controller
    {
        private UserManager userManager = new UserManager();
        // GET: Users
        public ActionResult Index()
        {
            var activeUser = CurrentSession.ActiveUser();
            if (activeUser == null)
            {
                return RedirectToAction("Login", "Login");
            }
            return View(userManager.GetUsers());
        }

        // GET: Users/Details/5
        public ActionResult Details(int? Id)
        {
            var activeUser = CurrentSession.ActiveUser();
            if (activeUser == null)
            {
                return RedirectToAction("Login", "Login");
            }
            if (Id == null)
            {
                return RedirectToAction("Index", "Users");
            }
            UserManager um = new UserManager();
            var res = um.GetUserById(Id.Value);
            res.Result.UserRoles = Enum.GetNames(typeof(UserRoles)).ToHashSet();
            if (res.Errors.Count > 0)
            {
                //TODO: Kullanıcıyı oluşturulacak hata sayfasına yönlendir.
                return RedirectToAction("Index", "Users");
            }
            return View(res.Result);
        }

        // GET: Users/Create
        public ActionResult Create()
        {
            var activeUser = CurrentSession.ActiveUser();
            if (activeUser == null)
            {
                return RedirectToAction("Login", "Login");
            }
            var userModel = new UserModel()
            {
                UserRoles = Enum.GetNames(typeof(UserRoles)).ToHashSet(),
            };
            return View(userModel);
        }

        // POST: Users/Create        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(UserModel model)
        {
            var activeUser = CurrentSession.ActiveUser();
            if (activeUser == null)
            {
                return RedirectToAction("Login", "Login");
            }
            model.UserRoles = Enum.GetNames(typeof(UserRoles)).ToHashSet();
            if (ModelState.IsValid)
            {
                UserManager um = new UserManager();
                var res = um.CreateUser(model);
                if (res.Errors.Count > 0)
                {
                    res.Errors.ForEach(g => ModelState.AddModelError("", g));
                    return View(model);
                }
                else
                {
                    return RedirectToAction("Index", "Users");
                }
            }
            return View(model);
        }

        // GET: Users/Edit/5
        public ActionResult Edit(int? Id)
        {
            var activeUser = CurrentSession.ActiveUser();
            if (activeUser == null)
            {
                return RedirectToAction("Login", "Login");
            }
            if (Id == null)
            {
                return RedirectToAction("Index", "Users");
            }
            UserManager um = new UserManager();
            var res = um.GetUserById(Id.Value);
            if (res.Result == null)
            {
                return RedirectToAction("Index", "Users");
            }
            res.Result.UserRoles = Enum.GetNames(typeof(UserRoles)).ToHashSet();
            if (res.Errors.Count > 0)
            {
                return RedirectToAction("Index", "Users");
            }            
            return View(res.Result);
        }

        // POST: Users/Edit/5    
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(UserModel model)
        {
            var activeUser = CurrentSession.ActiveUser();
            if (activeUser == null)
            {
                return RedirectToAction("Login", "Login");
            }
            if (ModelState.IsValid)
            {
                UserManager um = new UserManager();
                var res = um.UpdateUser(model, activeUser);
                if (res.Errors.Count > 0)
                {
                    res.Errors.ForEach(g => ModelState.AddModelError("", g));
                    return View(model);
                }
                else
                {
                    CurrentSession.Set<User>("activeUser", um.GetUserUserBy(g => g.Id == activeUser.Id).Result);
                    return RedirectToAction("Edit/" + model.Id + "", "Users");
                }
            }
            return View(model);
        }

        // GET: Users/Delete/5
        public ActionResult Delete(int? Id)
        {
            var activeUser = CurrentSession.ActiveUser();
            if (activeUser == null)
            {
                return RedirectToAction("Login", "Login");
            }
            if (Id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserManager um = new UserManager();
            var res = um.DeleteUser(Id.Value, activeUser);
            if (res.Errors.Count > 0)
            {
                res.Errors.ForEach(g => ModelState.AddModelError("", g));
            }
            return RedirectToAction("Index", "Users");
        }

        public JsonResult DeleteItem(int Id)
        {
            var activeUser = CurrentSession.ActiveUser();
            if (activeUser == null)
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }
            UserManager um = new UserManager();
            var res = um.DeleteUser(Id, activeUser);
            if (res.Errors.Count > 0)
            {
                res.Errors.ForEach(g => ModelState.AddModelError("", g));
            }
            return Json(res.Errors, JsonRequestBehavior.AllowGet);
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            return RedirectToAction("Index");
        }

    }
}
