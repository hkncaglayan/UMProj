using BussinessLayer;
using Common;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UserManagement.Models
{
    public class WebCommon : ICommon
    {
        public User GetActiveUser()
        {
            if (HttpContext.Current.Session["activeUser"] != null)
            {                
                return HttpContext.Current.Session["activeUser"] as User;                
            }
            else
            {
                return new User() { Id = -1, Name = "SYSTEM" };
            }
        }       
    }
}