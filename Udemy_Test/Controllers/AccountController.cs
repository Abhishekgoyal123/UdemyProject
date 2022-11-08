﻿using System;
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
            eShoppingCodiEntities db = new eShoppingCodiEntities();
            model.Password = PasswordEncrypt.Encrypt(model.Password);
            bool isvalid = db.Users.Any(x => x.UserName == model.UserName && x.Password == model.Password );
            if (isvalid)
            {
                return RedirectToAction("Index", "Employees");
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