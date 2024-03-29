﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Filters;

namespace Udemy_Test.Filter
{
    public class CustomFilter : ActionFilterAttribute, IAuthenticationFilter
    {
        public void OnAuthentication(AuthenticationContext filterContext)
        {
            ////if(filterContext.HttpContext.User.IsInRole(Roles) Identity.IsAuthenticated)
            ////{
               
            ////    filterContext.Result = new HttpUnauthorizedResult(); 
            ////}
        }

        public void OnAuthenticationChallenge(AuthenticationChallengeContext filterContext)
        {
             if(filterContext.Result == null || filterContext.Result is HttpUnauthorizedResult)
            {
                filterContext.Result = new ViewResult()
                {
                    ViewName = "Error"
                };
            }  
        }
    }
}