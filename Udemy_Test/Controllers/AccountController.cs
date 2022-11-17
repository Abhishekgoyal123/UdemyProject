using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Udemy_Test.Models;
using Udemy_Test;

namespace Udemy_Test.Controllers
{
    public class AccountController : Controller
    {
        eShoppingCodiEntities context = new eShoppingCodiEntities();
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

        public ActionResult Login(User model, UserRole role)
        {
            eShoppingCodiEntities db = new eShoppingCodiEntities();
            string username = model.UserName;
            model.Password = PasswordEncrypt.Encrypt(model.Password);
            bool isvalid = db.Users.Any(x => x.UserName == model.UserName && x.Password == model.Password );
            //int id = model.id;
            if (isvalid)
            {
                var abc = from user in context.Users
                          join userRole in context.UserRoles on user.id equals userRole.user_id
                          where user.UserName == username
                          select userRole;




                //var isValid1 = (from user in context.Users.ToList()
                //              join roles in context.UserRoles.ToList()
                //              on user.id equals roles.user_id
                //              where user.id == model.id && roles.role_name = "trainer"





                //if (abc == "trainer")
                //{
                //    return RedirectToAction("Index", "Employees");
                //}

            }
            return View();
        }

        public ActionResult Signup()
        {

            return View();
        }

        [HttpPost]

        public ActionResult Signup(User model)
        {
             eShoppingCodiEntities db = new eShoppingCodiEntities();
            model.Password = PasswordEncrypt.Encrypt(model.Password);
            db.Users.Add(model);
            db.SaveChanges();
            
            return RedirectToAction("login");
        }
    }
} 