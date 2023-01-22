using NHibernate.Linq;
using ProjectPathfinder.Areas.Admin.ViewModels;
using ProjectPathfinder.Infrastructure.Attributes;
using ProjectPathfinder.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Web.Security;

namespace ProjectPathfinder.Areas.Admin.Controllers
{
    [Authorize(Roles = "admin")]
    [SelectedTab("adminPanel")]
    public class AccountController : Controller
    {
        //-------------------------------------------------------------------------------------------

        public ActionResult Index()
        {
            var totalMembersCount = Database.Session.Query<MemberUser>().Count();
            int totalResultsPending = 0;
            List<MemberResult> possiblePendingResults = Database.Session.Query<MemberResult>().Where(r => r.SubmittedDate.Year < 2000).ToList();

            //For each possiblePendingResults, check if the member has submitted their test.
            //That then means a result is pending for them, so increment the count.
            for (int r = 0; r < possiblePendingResults.Count; r++)
            {
                var test = Database.Session.Query<MemberTest>().FirstOrDefault(t => t.MemberUser.Id == possiblePendingResults[r].MemberUser.Id);
                if (test.SubmittedDate.Year > 2000) { totalResultsPending++; }
            }     

            return View(new AccountIndex
            {
                NumberOfMembers = totalMembersCount,
                NumberOfPendingResults = totalResultsPending
            });
        }

        //-------------------------------------------------------------------------------------------

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToRoute("home");
        }

        //-------------------------------------------------------------------------------------------

    }
}