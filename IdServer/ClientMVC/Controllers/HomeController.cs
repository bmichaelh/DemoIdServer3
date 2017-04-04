using System.Security.Claims;
using System.Web;
using System.Web.Mvc;
using Thinktecture.IdentityModel.Mvc;

namespace ClientMVC.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        [ResourceAuthorize("Read", "ContactDetails")]
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        [HandleForbidden]
        public ActionResult UpdateContact()
        {
            if (!HttpContext.CheckAccess("Write", "ContactDetails", "some more data"))
            {
                return this.AccessDenied();
            }
            ViewBag.Message = "Update your contact details";

            return View();
        }

        [Authorize]
        public ActionResult ShowClaims()
        {
            return View((User as ClaimsPrincipal).Claims);
        }

        public ActionResult Logout()
        {
            Request.GetOwinContext().Authentication.SignOut();
            return RedirectToAction("/");
        }
    }
}