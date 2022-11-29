using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Udemy_Project.Models;
using Udemy_Project;
using System.Web.Security;

namespace Udemy_Project.Controllers
{
    public class AccountController : Controller
    {
        UdemyEntities1 context = new UdemyEntities1();
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

        public ActionResult Login(User model)
        {
            string localusername = model.UserName;
            model.UserPassword = PasswordEncrypt.Encrypt(model.UserPassword);
            bool isvalid = context.Users.Any(x => x.UserName == model.UserName && x.UserPassword == model.UserPassword);

            if (isvalid)
            {
                FormsAuthentication.SetAuthCookie(model.UserName, false);

                var roleCheck = (from user in context.Users
                                 join rolemapping in context.RoleMappings on user.UserId equals rolemapping.UserId
                                 join roles in context.Roles on rolemapping.RoleId equals roles.RoleId
                                 where user.UserName == localusername
                                 select roles.RoleName).FirstOrDefault();

                var userId = (from user in context.Users
                              where user.UserName == localusername
                              select user.UserId).FirstOrDefault();

                if (roleCheck == "Trainer")
                {
                    TempData["UserName"] = model.UserName;
                    TempData["UserId"] = userId;
                    return RedirectToAction("TrainerHomePage", "Trainer");
                }

                else if (roleCheck == "User")
                {
                    TempData["UserName"] = model.UserName;
                    TempData["UserId"] = userId;
                    return RedirectToAction("UserHomePage", "User");
                }

                else
                {
                    TempData["UserName"] = model.UserName;
                    return RedirectToAction("AdminHomePage", "Admin");
                }
            }
            else
                return View();
           // TempData.Keep();
            //return RedirectToAction("UserHomePage", "User");
        }

        [HttpPost]
        public ActionResult Signup(User model,RoleMapping roleMapping, Role role1)
        {
            model.UserPassword = PasswordEncrypt.Encrypt(model.UserPassword);

            //role.UserId = model.UserId;
            context.Users.Add(model);
            context.SaveChanges();

            var roleId = (from role in context.Roles
                         where role.RoleName == role1.RoleName
                         select role.RoleId).First();
            //role.user_id = model.id;
            roleMapping.UserId = model.UserId;
            roleMapping.RoleId = roleId;

            context.RoleMappings.Add(roleMapping);
            context.SaveChanges();
            //context.UserRoles.Add(role);
            //context.SaveChanges();

            return RedirectToAction("login");
        }
    }
}