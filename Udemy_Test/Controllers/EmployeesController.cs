using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Udemy_Test.Models;
using Udemy_Test;
using Udemy_Test.Filter;

namespace Udemy_Test.Controllers
{

    public class EmployeesController : Controller
    {

        private eShoppingCodiEntities db = new eShoppingCodiEntities();
        public UserRole role = new UserRole();

        [HttpGet]
        // GET: Employees


        
        public ActionResult Index()
        {

            var employees = db.Employees.Include(e => e.Department);
            return View(employees.ToList());


            //return View("Error");

        }

        // GET: Employees/Details/5

    }
}
