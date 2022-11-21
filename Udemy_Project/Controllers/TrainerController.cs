using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Udemy_Project.Controllers
{
    public class TrainerController : Controller
    {
        // GET: Trainer
        public ActionResult TrainerHomePage()
        {
            ViewBag.Message = "Welcome to Trainer Home Page";

            return View();
        }
    }
}