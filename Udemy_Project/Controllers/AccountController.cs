using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Udemy_Project.Models;
using Udemy_Project;

namespace Udemy_Project.Controllers
{
    public class AccountController : Controller
    {
        UdemyEntities context = new UdemyEntities();
        // GET: Account
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Login()
        {
            return View();
        }

        public ActionResult Signup()
        {
            List<Role> rolelist = context.Roles.ToList();

            rolelist.RemoveAt(0);
            ViewData["RoleName"] = new SelectList(rolelist, "RoleName", "RoleName");
            
            return View();
        }

        [HttpPost]

        public ActionResult Login(User model, Role role)
        {
          
            string localusername = model.UserName;
            model.UserPassword = PasswordEncrypt.Encrypt(model.UserPassword);
            bool isvalid = context.Users.Any(x => x.UserName == model.UserName && x.UserPassword == model.UserPassword);

            if (isvalid)
            {

                var roleCheck = (from user in context.Users
                           join rolemapping in context.RoleMappings on user.UserId equals rolemapping.UserId
                           join roles in context.Roles on rolemapping.RoleId equals roles.RoleId
                           where user.UserName == localusername
                           select roles.RoleName).First();

                if (roleCheck == "Trainer")
                {
                    return RedirectToAction("TrainerHomePage", "Trainer");
                }

                else if(roleCheck == "User")
                {
                    return RedirectToAction("UserHomePage", "User");
                }

               else
                {
                    return RedirectToAction("AdminHomePage", "Admin");
                }
            }
            else
                return View("Error");
            
        }

        [HttpPost]
        public ActionResult Signup(User model,Role role)
        {
            
            model.UserPassword = PasswordEncrypt.Encrypt(model.UserPassword);

            // role.user_id = model.id;
            context.Users.Add(model);
            context.SaveChanges();
            //role.user_id = model.id;
            //context.UserRoles.Add(role);
            //context.SaveChanges();

            return RedirectToAction("login");
        }
    }
}