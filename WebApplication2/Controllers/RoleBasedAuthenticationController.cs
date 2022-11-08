using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
//using System.Web.Mvc;

namespace WebApplication2.Controllers
{
    public class RoleBasedAuthenticationController : Controller
    {
        [Authorize(Roles ="trainer")]
        public IActionResult Index()
        {
            return View();
        }

        
       
    }
}
