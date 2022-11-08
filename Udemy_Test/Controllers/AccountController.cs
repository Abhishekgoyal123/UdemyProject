using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Udemy_Test.Models;

namespace Udemy_Test.Controllers
{
    
    public class AccountController : Controller
    {
        // GET: Account
        public ActionResult Index()
        {

            return View();
        }

        public ActionResult Login()
        {

            return View();
        }

        [HttpPost]

        public ActionResult Login(Membership model)
        {
            return View();
        }

        public ActionResult Signup()
        {

            return View();
        }

        [HttpPost]

        public ActionResult Signup(Membership model)
        {
            return View();
        }
    }
} 