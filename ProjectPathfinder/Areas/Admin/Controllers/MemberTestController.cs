using NHibernate.Linq;
using ProjectPathfinder.Areas.Member.ViewModels;
using ProjectPathfinder.Infrastructure.Attributes;
using ProjectPathfinder.Models;
using ProjectPathfinder.Test;
using ProjectPathfinder.Test.Utilities;
using System;
using System.Linq;
using System.Web.Mvc;

namespace ProjectPathfinder.Areas.Admin.Controllers
{
    [Authorize(Roles = "admin")]
    [SelectedTab("adminMemberTest")]
    public class MemberTestController : Controller
    {
        public ActionResult Index(int? id)
        {
            if(id == null)
            {
                return RedirectToAction("Index", "Member", new { area = "admin" });
            }

            var testTable = Database.Session.Query<MemberTest>().FirstOrDefault(t => t.MemberUser.Id == id);

            ViewBag.CurrentMemberId = id;
            ViewBag.CurrentMemberName = Database.Session.Query<MemberUser>().FirstOrDefault(t => t.Id == id).FirstNames + " " +
                                        Database.Session.Query<MemberUser>().FirstOrDefault(t => t.Id == id).Surname;
            Session["AdminTestObject"] = TestSerializer.DeserializeTest(testTable.TestObject);

            return RedirectToAction("Step_01", new { id = id });
        }        

        //-------------------------------------------------------------------------------------------
        // STEP ONE
        //-------------------------------------------------------------------------------------------

        public ActionResult Step_01(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index", "Member", new { area = "admin" });
            }

            ViewBag.CurrentMemberId = id;
            ViewBag.CurrentMemberName = Database.Session.Query<MemberUser>().FirstOrDefault(t => t.Id == id).FirstNames + " " +
                                        Database.Session.Query<MemberUser>().FirstOrDefault(t => t.Id == id).Surname;

            CareerPathfinderTest test = Session["AdminTestObject"] as CareerPathfinderTest;
            return View();
        }

        //-------------------------------------------------------------------------------------------
        // STEP TWO
        //-------------------------------------------------------------------------------------------

        public ActionResult Step_02(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index", "Member", new { area = "admin" });
            }

            ViewBag.CurrentMemberId = id;
            ViewBag.CurrentMemberName = Database.Session.Query<MemberUser>().FirstOrDefault(t => t.Id == id).FirstNames + " " +
                                        Database.Session.Query<MemberUser>().FirstOrDefault(t => t.Id == id).Surname;

            CareerPathfinderTest test = Session["AdminTestObject"] as CareerPathfinderTest;
            TestStepTwo form = test.stepTwo;

            return View(form);
        }

        //-------------------------------------------------------------------------------------------
        // STEP THREE
        //-------------------------------------------------------------------------------------------

        public ActionResult Step_03(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index", "Member", new { area = "admin" });
            }

            ViewBag.CurrentMemberId = id;
            ViewBag.CurrentMemberName = Database.Session.Query<MemberUser>().FirstOrDefault(t => t.Id == id).FirstNames + " " +
                                        Database.Session.Query<MemberUser>().FirstOrDefault(t => t.Id == id).Surname;

            CareerPathfinderTest test = Session["AdminTestObject"] as CareerPathfinderTest;
            TestStepThree form = test.stepThree;

            return View(form);
        }

        //-------------------------------------------------------------------------------------------
        // STEP FOUR
        //-------------------------------------------------------------------------------------------

        public ActionResult Step_04(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index", "Member", new { area = "admin" });
            }

            ViewBag.CurrentMemberId = id;
            ViewBag.CurrentMemberName = Database.Session.Query<MemberUser>().FirstOrDefault(t => t.Id == id).FirstNames + " " +
                                        Database.Session.Query<MemberUser>().FirstOrDefault(t => t.Id == id).Surname;

            CareerPathfinderTest test = Session["AdminTestObject"] as CareerPathfinderTest;
            TestStepFour form = test.stepFour;
            form.Grade = Database.Session.Query<MemberUser>().FirstOrDefault(t => t.Id == id).Grade;

            return View(form);
        }

        //-------------------------------------------------------------------------------------------
        // STEP FIVE ONE
        //-------------------------------------------------------------------------------------------

        public ActionResult Step_05_1(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index", "Member", new { area = "admin" });
            }

            ViewBag.CurrentMemberId = id;
            ViewBag.CurrentMemberName = Database.Session.Query<MemberUser>().FirstOrDefault(t => t.Id == id).FirstNames + " " +
                                        Database.Session.Query<MemberUser>().FirstOrDefault(t => t.Id == id).Surname;

            CareerPathfinderTest test = Session["AdminTestObject"] as CareerPathfinderTest;
            TestStepFive form = test.stepFive;

            return View(form);
        }

        //-------------------------------------------------------------------------------------------
        // STEP FVIE TWO
        //-------------------------------------------------------------------------------------------

        public ActionResult Step_05_2(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index", "Member", new { area = "admin" });
            }

            ViewBag.CurrentMemberId = id;
            ViewBag.CurrentMemberName = Database.Session.Query<MemberUser>().FirstOrDefault(t => t.Id == id).FirstNames + " " +
                                        Database.Session.Query<MemberUser>().FirstOrDefault(t => t.Id == id).Surname;

            CareerPathfinderTest test = Session["AdminTestObject"] as CareerPathfinderTest;
            TestStepFiveContinuation form = test.stepFiveContinuation;

            return View(form);
        }

        //-------------------------------------------------------------------------------------------
        // STEP SIX
        //-------------------------------------------------------------------------------------------

        public ActionResult Step_06(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index", "Member", new { area = "admin" });
            }

            ViewBag.CurrentMemberId = id;
            ViewBag.CurrentMemberName = Database.Session.Query<MemberUser>().FirstOrDefault(t => t.Id == id).FirstNames + " " +
                                        Database.Session.Query<MemberUser>().FirstOrDefault(t => t.Id == id).Surname;

            CareerPathfinderTest test = Session["AdminTestObject"] as CareerPathfinderTest;
            TestStepSix form = test.stepSix;

            return View(form);
        }

        //-------------------------------------------------------------------------------------------
        // STEP SEVEN
        //-------------------------------------------------------------------------------------------

        public ActionResult Step_07(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index", "Member", new { area = "admin" });
            }

            ViewBag.CurrentMemberId = id;
            ViewBag.CurrentMemberName = Database.Session.Query<MemberUser>().FirstOrDefault(t => t.Id == id).FirstNames + " " +
                                        Database.Session.Query<MemberUser>().FirstOrDefault(t => t.Id == id).Surname;

            CareerPathfinderTest test = Session["AdminTestObject"] as CareerPathfinderTest;
            TestStepSeven form = test.stepSeven;

            return View(form);
        }

        //-------------------------------------------------------------------------------------------
        // STEP EIGHT
        //-------------------------------------------------------------------------------------------

        public ActionResult Step_08(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index", "Member", new { area = "admin" });
            }

            ViewBag.CurrentMemberId = id;
            ViewBag.CurrentMemberName = Database.Session.Query<MemberUser>().FirstOrDefault(t => t.Id == id).FirstNames + " " +
                                        Database.Session.Query<MemberUser>().FirstOrDefault(t => t.Id == id).Surname;

            CareerPathfinderTest test = Session["AdminTestObject"] as CareerPathfinderTest;
            TestStepEight form = test.stepEight;

            return View(form);
        }

        //-------------------------------------------------------------------------------------------
        // STEP NINE
        //-------------------------------------------------------------------------------------------

        public ActionResult Step_09(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index", "Member", new { area = "admin" });
            }

            ViewBag.CurrentMemberId = id;
            ViewBag.CurrentMemberName = Database.Session.Query<MemberUser>().FirstOrDefault(t => t.Id == id).FirstNames + " " +
                                        Database.Session.Query<MemberUser>().FirstOrDefault(t => t.Id == id).Surname;

            CareerPathfinderTest test = Session["AdminTestObject"] as CareerPathfinderTest;
            TestStepNine form = test.stepNine;

            return View(form);
        }

        //-------------------------------------------------------------------------------------------
        // STEP TEN
        //-------------------------------------------------------------------------------------------

        public ActionResult Step_10(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index", "Member", new { area = "admin" });
            }

            ViewBag.CurrentMemberId = id;
            ViewBag.CurrentMemberName = Database.Session.Query<MemberUser>().FirstOrDefault(t => t.Id == id).FirstNames + " " +
                                        Database.Session.Query<MemberUser>().FirstOrDefault(t => t.Id == id).Surname;

            CareerPathfinderTest test = Session["AdminTestObject"] as CareerPathfinderTest;
            TestStepTen form = test.stepTen;

            return View(form);
        }

        //-------------------------------------------------------------------------------------------
        // STEP TEN
        //-------------------------------------------------------------------------------------------

        public ActionResult Require_Resubmision(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index", "Member", new { area = "admin" });
            }

            var testTable = Database.Session.Query<MemberTest>().FirstOrDefault(t => t.MemberUser.Id == id);
            testTable.TestToResubmit = true;
            testTable.SubmittedDate = DateTime.MinValue;

            TempData["AdminResultMessage"] = "The test for " + testTable.MemberUser.FirstNames + " " + testTable.MemberUser.Surname +
                                             " has been sent back for resubmission";

            return RedirectToAction("Index", "Account", new { area = "admin" });
        }
    }
}