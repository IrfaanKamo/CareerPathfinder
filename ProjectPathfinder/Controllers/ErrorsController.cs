using System.Web.Mvc;

namespace ProjectPathfinder.Controllers
{
    public class ErrorsController : Controller
    {
        public ActionResult NotFound()
        {
            return View();
        }

        public ActionResult Error()
        {
            return View();
        }
    }
}