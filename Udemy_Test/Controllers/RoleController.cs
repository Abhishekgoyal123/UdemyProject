using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Udemy_Test.Models;
using Udemy_Test;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;


namespace Udemy_Test.Controllers
{
    public class RoleController : Controller
    {
        // GET: Role
       

        RoleManager<IdentityRole> roleManager;

       
        public ActionResult Index()
        {
            var roles = roleManager.Roles.ToList();
            return View(roles);
        }

        public ActionResult Create()
        {
            return View(new IdentityRole());
        }

        

    }
}