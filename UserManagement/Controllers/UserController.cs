using BussinessLayer;
using Common;
using Entities;
using Entities.ResponseObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using UserManagement.Models;

namespace UserManagement.Controllers
{
    public class UserController : Controller
    {
        // GET: User
        [Authorize]
        public ActionResult UserDetail(int? Id)
        {
            if (Id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);
            }
            else
            {
                UserManager um = new UserManager();
                var res = um.GetUserById(Id.Value);
                if (res.Errors.Count > 0)
                {
                    //TODO: Kullanıcıyı oluşturulacak hata sayfasına yönlendir.
                    return new HttpStatusCodeResult(HttpStatusCode.NotFound);
                }
                else
                {
                    return View(res.Result);
                }
            }
        }
        [Authorize]
        public ActionResult CreateUser()
        {
            var userModel = new UserModel()
            {
                UserRoles = Enum.GetNames(typeof(UserRoles)).ToHashSet(),
                DateOfBirth = DateTime.Today.AddYears(-20),
            };           
            return View(userModel);
        }       

        [HttpPost]
        public ActionResult UserDetail(UserModel model)
        {          
            if (ModelState.IsValid)
            {
                UserManager um = new UserManager();
                var res = um.UpdateUser(model,CurrentSession.ActiveUser());
                if (res.Errors.Count > 0)
                {
                    res.Errors.ForEach(g => ModelState.AddModelError("", g));
                    return View(model);
                }
                else
                {
                    return RedirectToAction("UserDetail/"+model.Id+"", "User");
                }
            }
            return View(model);
        }
        [HttpPost]
        public ActionResult CreateUser(UserModel model)
        {            
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
                    //TODO: Hata için ara sayfaya yönlendir. (sweetalert olabilir)
                    return View(model);
                }
            }            
            return View(model);
        }
        
      [Authorize]
        public ActionResult UserList()
        {
            UserManager um = new UserManager();
            List<User> list = um.GetUsers();
            return View(list);
        }
        [Authorize]       
        public MvcHtmlString DeleteUser(int id)
        { 
            UserManager um = new UserManager();
            var res=um.DeleteUser(id,CurrentSession.ActiveUser());
            if (res.Errors.Count>0)
            {
                res.Errors.ForEach(g => ModelState.AddModelError("", g));              
            }
            return MvcHtmlString.Empty;
        }
              
    }
}