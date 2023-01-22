using NHibernate.Linq;
using ProjectPathfinder.Areas.Member.ViewModels;
using ProjectPathfinder.Infrastructure.Helpers;
using ProjectPathfinder.Models;
using ProjectPathfinder.Result;
using ProjectPathfinder.Result.Utilities;
using System.Linq;
using System.Web.Mvc;

namespace ProjectPathfinder.Areas.Member.Controllers
{
    [Authorize(Roles = "member")]
    public class ResultController : Controller
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        //-------------------------------------------------------------------------------------------

        public ActionResult Index()
        {
            var resultTable = Database.Session.Query<MemberResult>().FirstOrDefault(t => t.MemberUser.User.Id == CookieData.GetUserId(User.Identity.Name));
            
            //display approriate pages for memeber if no results available
            if(resultTable.SubmittedDate.Year < 2000)
            {
                return RedirectToAction("Result_Status", "Account", new { area = "member" });
            }
            else if(resultTable.ResultObject == null)
            {
                log.Error("Result object cannot be found for user " + User.Identity.Name);
                return HttpNotFound();
            }

            //populate the results
            ResultIndex form = new ResultIndex();
            form.result = ResultSerializer.DeserializeResult(resultTable.ResultObject);

            return View(form);
        }
    }
}