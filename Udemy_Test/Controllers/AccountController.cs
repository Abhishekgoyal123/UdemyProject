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
            string localusername = model.UserName;
            model.Password = PasswordEncrypt.Encrypt(model.Password);
            bool isvalid = db.Users.Any(x => x.UserName == model.UserName && x.Password == model.Password );
           // int id = 
            if (isvalid)
            {

                var abc = (from user in context.Users
                           join userRole in context.UserRoles on user.id equals userRole.user_id
                           where user.UserName == localusername
                           select userRole.role_name).First();

                if (abc == "Trainer")
                {
                    return RedirectToAction("Index", "Employees");
                }

            }
            return View();
        }

        public ActionResult Signup()
        {
            List<UserRole> rolelist = context.UserRoles.ToList();
            
            rolelist.RemoveAt(1);
            ViewData["role_name"] = new SelectList(rolelist, "role_name", "role_name");
            return View();
        }

        [HttpPost]

        public ActionResult Signup(User model, UserRole role)
        {
             eShoppingCodiEntities db = new eShoppingCodiEntities();
            model.Password = PasswordEncrypt.Encrypt(model.Password);

           // role.user_id = model.id;
            db.Users.Add(model);
            db.SaveChanges();
            role.user_id = model.id;
            db.UserRoles.Add(role);
            db.SaveChanges();

            return RedirectToAction("login");
        }
    }
} 