using NHibernate.Linq;
using ProjectPathfinder.Infrastructure.Utilities;
using ProjectPathfinder.Models;
using System.Linq;

namespace ProjectPathfinder.Areas.Admin.ViewModels
{
    //-------------------------------------------------------------------------------------------
    // Index
    //-------------------------------------------------------------------------------------------

    public class MemberIndex
    {
        public PagedData<MemberUser> Members { get; set; }
        public int NumberOfMembers { get; set; }

        //-------------------------------------------------------------------------------------------

        public bool IsTestSubmitted(MemberUser member)
        {
            var memberTest = Database.Session.Query<MemberTest>().FirstOrDefault(u => u.MemberUser.Id == member.Id);

            if(memberTest == null)
            {
                return false;
            }
            else if (memberTest.SubmittedDate.Year > 2000)
            {
                return true;
            }
            return false;
        }

        //-------------------------------------------------------------------------------------------

        public bool IsResultSubmitted(MemberUser member)
        {
            var memberResult = Database.Session.Query<MemberResult>().FirstOrDefault(u => u.MemberUser.Id == member.Id);

            if (memberResult == null)
            {
                return false;
            }
            else if (memberResult.SubmittedDate.Year > 2000)
            {
                return true;
            }
            return false;
        }        
    }

    //-------------------------------------------------------------------------------------------
    // Pending Results
    //-------------------------------------------------------------------------------------------

    public class MemberPendingResults
    {
        public PagedData<MemberUser> Members { get; set; }
        public int NumberOfPendingResults { get; set; }

        //-------------------------------------------------------------------------------------------

        public bool IsTestSubmitted(MemberUser member)
        {
            var memberTest = Database.Session.Query<MemberTest>().FirstOrDefault(u => u.MemberUser.Id == member.Id);

            if (memberTest == null)
            {
                return false;
            }
            else if (memberTest.SubmittedDate.Year > 2000)
            {
                return true;
            }
            return false;
        }

        //-------------------------------------------------------------------------------------------

        public bool IsResultSubmitted(MemberUser member)
        {
            var memberResult = Database.Session.Query<MemberResult>().FirstOrDefault(u => u.MemberUser.Id == member.Id);

            if (memberResult == null)
            {
                return false;
            }
            else if (memberResult.SubmittedDate.Year > 2000)
            {
                return true;
            }
            return false;
        }        
    }
}