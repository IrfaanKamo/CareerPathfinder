using NHibernate.Linq;
using ProjectPathfinder.Areas.Admin.ViewModels;
using ProjectPathfinder.Infrastructure.Attributes;
using ProjectPathfinder.Infrastructure.Utilities;
using ProjectPathfinder.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace ProjectPathfinder.Areas.Admin.Controllers
{
    [Authorize(Roles = "admin")]
    [SelectedTab("adminMembers")]
    public class MemberController : Controller
    {
        private const int MembersPerPage = 20;

        //-------------------------------------------------------------------------------------------

        public ActionResult Index(int page = 1)
        {
            var totalMembersCount = Database.Session.Query<MemberUser>().Count();

            var baseQuery = Database.Session.Query<MemberUser>().OrderBy(f => f.Surname);

            var membersIds = baseQuery
                .Skip((page - 1) * MembersPerPage) //e.g if we on page 2, we skip (2-1)*5 members and then
                .Take(MembersPerPage)              //show the next 5 members on that page
                .Select(p => p.Id)
                .ToArray();

            var currentMemberPage = baseQuery
                .Where(p => membersIds.Contains(p.Id))
                .ToList();

            return View(new MemberIndex
            {
                Members = new PagedData<MemberUser>(currentMemberPage, totalMembersCount, page, MembersPerPage),
                NumberOfMembers = totalMembersCount
            });
        }

        //-------------------------------------------------------------------------------------------

        public ActionResult PendingResults(int page = 1)
        {
            List<MemberResult> possiblePendingResults = Database.Session.Query<MemberResult>().Where(r => r.SubmittedDate.Year < 2000).ToList();
            List<MemberUser> membersRequiringResults = new List<MemberUser>();

            for (int r = 0; r < possiblePendingResults.Count; r++)
            {
                var test = Database.Session.Query<MemberTest>().FirstOrDefault(t => t.MemberUser.Id == possiblePendingResults[r].MemberUser.Id);
                //check if member has submitted their test, that means he needs a result
                if (test.SubmittedDate.Year > 2000)
                {
                    //populate the list of members that require results
                    membersRequiringResults.Add(Database.Session.Query<MemberUser>().FirstOrDefault(t => t.Id == possiblePendingResults[r].MemberUser.Id));
                }
            }            

            var membersIds = membersRequiringResults
                .Skip((page - 1) * MembersPerPage)
                .Take(MembersPerPage)
                .Select(p => p.Id)
                .ToArray();

            var currentMemberPage = membersRequiringResults
                .Where(p => membersIds.Contains(p.Id))
                .ToList();

            return View(new MemberPendingResults
            {
                Members = new PagedData<MemberUser>(currentMemberPage, membersRequiringResults.Count, page, MembersPerPage),
                NumberOfPendingResults = membersRequiringResults.Count
            });
        }

        //-------------------------------------------------------------------------------------------
    }
}