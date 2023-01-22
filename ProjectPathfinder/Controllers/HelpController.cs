using System.Linq;
using System.Web.Mvc;

namespace ProjectPathfinder.Controllers
{
    public class HelpController : Controller
    {
        public ActionResult Index()
        {
            return View("Faqs");
        }

        public ActionResult Faqs()
        {
            return View();
        }

        public ActionResult Privacy_Policy()
        {
            return View();
        }
    }
}