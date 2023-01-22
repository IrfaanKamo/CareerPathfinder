using NHibernate.Linq;
using ProjectPathfinder.Areas.Member.ViewModels;
using ProjectPathfinder.Infrastructure.Attributes;
using ProjectPathfinder.Infrastructure.Helpers;
using ProjectPathfinder.Infrastructure.Utilities;
using ProjectPathfinder.Models;
using ProjectPathfinder.Test;
using ProjectPathfinder.Test.TestObjects;
using ProjectPathfinder.Test.Utilities;
using System;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace ProjectPathfinder.Areas.Member.Controllers
{
    [Authorize(Roles = "member")]
    public class TestController : Controller
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        //-------------------------------------------------------------------------------------------

        public ActionResult Index()
        {
            var testTable = Database.Session.Query<MemberTest>().FirstOrDefault(t => t.MemberUser.User.Id == CookieData.GetUserId(User.Identity.Name));
            int currentStep = 1;

            //member has already submitted the test and is not required to resubmit, then don't allow him to access it.
            if (testTable.SubmittedDate.Year > 2000 && !testTable.TestToResubmit)
            {
                log.Info(testTable.MemberUser.User.Email + "attempted to access a submitted test");
                return RedirectToAction("Test_Already_Submitted");
            }

            //let the member start/resume the test
            if (testTable.TestObject == null)
            {
                Session["TestObject"] = new CareerPathfinderTest();
                log.Info(testTable.MemberUser.User.Email + " has started a new test.");
            }                
            else
            {
                CareerPathfinderTest test = TestSerializer.DeserializeTest(testTable.TestObject);
                currentStep = testTable.TestToResubmit ? 1 : test.CurrentStep;
                Session["TestObject"] = test;
                log.Info(testTable.MemberUser.User.Email + " has resumed a stored test from step " + currentStep);
            }

            //jump to the step that user was on when he saved and exited
            switch(currentStep)
            {
                case 1: return RedirectToAction("Step_01");
                case 2: return RedirectToAction("Step_02");
                case 3: return RedirectToAction("Step_03");
                case 4: return RedirectToAction("Step_04");
                case 5: return RedirectToAction("Step_05_1");
                case 6: return RedirectToAction("Step_06");
                case 7: return RedirectToAction("Step_07");
                case 8: return RedirectToAction("Step_08");
                case 9: return RedirectToAction("Step_09");
                case 10: return RedirectToAction("Step_10");
                case 11: return RedirectToAction("Test_Complete");
            }                

            return RedirectToAction("Step_01");
        }

        //-------------------------------------------------------------------------------------------

        public ActionResult Appendix(string id)
        {
            var dir = Server.MapPath("~/Assets/Images/appendix");
            var path = Path.Combine(dir, "Appendix_" + id + ".jpg");
            return base.File(path, "image/jpeg");
        }

        //-------------------------------------------------------------------------------------------

        [HttpPost, NoDirectAccess]
        [MultipleButton(Name = "action", Argument = "SaveAndExit")]
        public ActionResult SaveAndExit()
        {
            var testTable = Database.Session.Query<MemberTest>().FirstOrDefault(t => t.MemberUser.User.Id == CookieData.GetUserId(User.Identity.Name));
            CareerPathfinderTest test = Session["TestObject"] as CareerPathfinderTest;
            testTable.TestObject = TestSerializer.SerializeTest(test);

            TempData["DashboardMessage"] = "Awesome! Your test has been saved. You can return at anytime to continue.";
            log.Info(testTable.MemberUser.User.Email + "'s test has been saved");

            return RedirectToAction("Dashboard", "Account", new { area = "member" });
        }

        //-------------------------------------------------------------------------------------------
        // STEP ONE
        //-------------------------------------------------------------------------------------------

        [NoDirectAccess]
        public ActionResult Step_01()
        {
            ViewBag.StepNumber = 1;

            CareerPathfinderTest test = Session["TestObject"] as CareerPathfinderTest;
            test.CurrentStep = 2;
            return View();
        }

        //-------------------------------------------------------------------------------------------
        // STEP TWO
        //-------------------------------------------------------------------------------------------

        [NoDirectAccess]
        public ActionResult Step_02()
        {
            ViewBag.StepNumber = 2;

            CareerPathfinderTest test = Session["TestObject"] as CareerPathfinderTest;
            TestStepTwo form = test.stepTwo;

            return View(form);
        }

        //-------------------------------------------------------------------------------------------

        [HttpPost, NoDirectAccess]
        [MultipleButton(Name = "action", Argument = "Step_02Next")]
        public ActionResult Step_02Next(TestStepTwo form)
        {
            if(!ModelState.IsValid)
            {
                return View(form);
            }
            
            CareerPathfinderTest test = Session["TestObject"] as CareerPathfinderTest;
            test.CurrentStep = 3;
            test.stepTwo = form;

            return RedirectToAction("Step_03");
        }

        //-------------------------------------------------------------------------------------------

        [HttpPost, NoDirectAccess]
        [MultipleButton(Name = "action", Argument = "Step_02Previous")]
        public ActionResult Step_02Previous(TestStepTwo form)
        {
            CareerPathfinderTest test = Session["TestObject"] as CareerPathfinderTest;
            test.stepTwo = form;

            return RedirectToAction("Step_01");
        }

        //-------------------------------------------------------------------------------------------
        // STEP THREE
        //-------------------------------------------------------------------------------------------

        [NoDirectAccess]
        public ActionResult Step_03()
        {
            ViewBag.StepNumber = 3;

            CareerPathfinderTest test = Session["TestObject"] as CareerPathfinderTest;
            TestStepThree form = test.stepThree;

            return View(form);
        }

        //-------------------------------------------------------------------------------------------

        [HttpPost, NoDirectAccess]
        [MultipleButton(Name = "action", Argument = "Step_03Next")]
        public ActionResult Step_03Next(TestStepThree form)
        {
            if (!ModelState.IsValid)
            {
                return View(form);
            }

            CareerPathfinderTest test = Session["TestObject"] as CareerPathfinderTest;
            test.CurrentStep = 4;
            test.stepThree = form;

            return RedirectToAction("Step_04");
        }

        //-------------------------------------------------------------------------------------------

        [HttpPost, NoDirectAccess]
        [MultipleButton(Name = "action", Argument = "Step_03Previous")]
        public ActionResult Step_03Previous(TestStepThree form)
        {
            CareerPathfinderTest test = Session["TestObject"] as CareerPathfinderTest;
            test.stepThree = form;

            return RedirectToAction("Step_02");
        }

        //-------------------------------------------------------------------------------------------
        // STEP FOUR
        //-------------------------------------------------------------------------------------------

        [NoDirectAccess]
        public ActionResult Step_04()
        {
            ViewBag.StepNumber = 4;
            
            CareerPathfinderTest test = Session["TestObject"] as CareerPathfinderTest;
            TestStepFour form = test.stepFour;
            form.Grade = Database.Session.Query<MemberUser>().FirstOrDefault(t => t.User.Id == CookieData.GetUserId(User.Identity.Name)).Grade;

            return View(form);
        }

        //-------------------------------------------------------------------------------------------

        [HttpPost, NoDirectAccess]
        [MultipleButton(Name = "action", Argument = "Step_04Next")]
        public ActionResult Step_04Next(TestStepFour form)
        {
            if (!ModelState.IsValid)
            {
                return View(form);
            }

            if (HasMemberPaidForTest() == false) //user has not paid
            {
                var testTable = Database.Session.Query<MemberTest>().FirstOrDefault(t => t.MemberUser.User.Id == CookieData.GetUserId(User.Identity.Name));
                CareerPathfinderTest freeTest = Session["TestObject"] as CareerPathfinderTest;

                freeTest.CurrentStep = 4; //let the user return to this step so he does not get locked out of the test.
                freeTest.stepFour = form;

                testTable.TestObject = TestSerializer.SerializeTest(freeTest);
                return RedirectToAction("PurchaseTest", "Payment", new { area = "member" });
            }

            CareerPathfinderTest test = Session["TestObject"] as CareerPathfinderTest;
            test.CurrentStep = 5;
            test.stepFour = form;

            return RedirectToAction("Step_05_1");
        }

        //-------------------------------------------------------------------------------------------

        [HttpPost, NoDirectAccess]
        [MultipleButton(Name = "action", Argument = "Step_04Previous")]
        public ActionResult Step_04Previous(TestStepFour form)
        {
            CareerPathfinderTest test = Session["TestObject"] as CareerPathfinderTest;
            test.stepFour = form;

            return RedirectToAction("Step_03");
        }

        //-------------------------------------------------------------------------------------------
        // STEP FIVE
        //-------------------------------------------------------------------------------------------

        [NoDirectAccess]
        public ActionResult Step_05_1()
        {
            var invoice = Database.Session.Query<MemberInvoice>().FirstOrDefault(t => t.MemberUser.User.Id == CookieData.GetUserId(User.Identity.Name));
            bool userHasPaid = false;

            if (invoice != null)
            {
                userHasPaid = invoice.PaymentReceived;
            }

            if (userHasPaid == false)
            {
                return RedirectToAction("PurchaseTest", "Payment", new { area = "member" });
            }

            ViewBag.StepNumber = 5;

            CareerPathfinderTest test = Session["TestObject"] as CareerPathfinderTest;
            TestStepFive form = test.stepFive;

            return View(form);
        }

        //-------------------------------------------------------------------------------------------

        [HttpPost, NoDirectAccess]
        [MultipleButton(Name = "action", Argument = "Step_05_1Next")]
        public ActionResult Step_05_1Next(TestStepFive form)
        {
            if (!ModelState.IsValid)
            {
                return View(form);
            }

            CareerPathfinderTest test = Session["TestObject"] as CareerPathfinderTest;
            test.CurrentStep = 5;
            test.stepFive = form;

            return RedirectToAction("Step_05_2");
        }

        //-------------------------------------------------------------------------------------------

        [HttpPost, NoDirectAccess]
        [MultipleButton(Name = "action", Argument = "Step_05_1Previous")]
        public ActionResult Step_05_1Previous(TestStepFive form)
        {
            CareerPathfinderTest test = Session["TestObject"] as CareerPathfinderTest;
            test.stepFive = form;

            return RedirectToAction("Step_04");
        }

        //-------------------------------------------------------------------------------------------
        // STEP FIVE CONTINUATION
        //-------------------------------------------------------------------------------------------

        [NoDirectAccess]
        public ActionResult Step_05_2()
        {
            ViewBag.StepNumber = 5;

            CareerPathfinderTest test = Session["TestObject"] as CareerPathfinderTest;
            TestStepFiveContinuation form = test.stepFiveContinuation;

            return View(form);
        }

        //-------------------------------------------------------------------------------------------

        [HttpPost, NoDirectAccess]
        [MultipleButton(Name = "action", Argument = "Step_05_2Next")]
        public ActionResult Step_05_2Next(TestStepFiveContinuation form)
        {
            if (!ModelState.IsValid)
            {
                return View(form);
            }

            CareerPathfinderTest test = Session["TestObject"] as CareerPathfinderTest;
            test.CurrentStep = 6;
            test.stepFiveContinuation = form;

            return RedirectToAction("Step_06");
        }

        //-------------------------------------------------------------------------------------------

        [HttpPost, NoDirectAccess]
        [MultipleButton(Name = "action", Argument = "Step_05_2Previous")]
        public ActionResult Step_05_2Previous(TestStepFiveContinuation form)
        {
            CareerPathfinderTest test = Session["TestObject"] as CareerPathfinderTest;
            test.stepFiveContinuation = form;

            return RedirectToAction("Step_05_1");
        }

        //-------------------------------------------------------------------------------------------
        // STEP SIX
        //-------------------------------------------------------------------------------------------

        [NoDirectAccess]
        public ActionResult Step_06()
        {
            ViewBag.StepNumber = 6;

            CareerPathfinderTest test = Session["TestObject"] as CareerPathfinderTest;
            TestStepSix form = test.stepSix;

            return View(form);
        }

        //-------------------------------------------------------------------------------------------

        [HttpPost, NoDirectAccess]
        [MultipleButton(Name = "action", Argument = "Step_06Next")]
        public ActionResult Step_06Next(TestStepSix form)
        {
            if(form.Interest_1.Equals(form.Interest_2))
            {
                ModelState.AddModelError("Interest_2", "Primary and Secondary Interests cannot be the same.");
            }

            if (!ModelState.IsValid)
            {
                return View(form);
            }

            CareerPathfinderTest test = Session["TestObject"] as CareerPathfinderTest;
            test.CurrentStep = 7;
            test.stepSix = form;

            return RedirectToAction("Step_07");
        }

        //-------------------------------------------------------------------------------------------

        [HttpPost, NoDirectAccess]
        [MultipleButton(Name = "action", Argument = "Step_06Previous")]
        public ActionResult Step_06Previous(TestStepSix form)
        {
            CareerPathfinderTest test = Session["TestObject"] as CareerPathfinderTest;
            test.stepSix = form;

            return RedirectToAction("Step_05_2");
        }

        //-------------------------------------------------------------------------------------------
        // STEP SEVEN
        //-------------------------------------------------------------------------------------------

        [NoDirectAccess]
        public ActionResult Step_07()
        {
            ViewBag.StepNumber = 7;

            CareerPathfinderTest test = Session["TestObject"] as CareerPathfinderTest;
            TestStepSeven form = test.stepSeven;

            form.Interest_1 = Interests.Instance.interestsList[Int32.Parse(test.stepSix.Interest_1)];
            form.Interest_2 = Interests.Instance.interestsList[Int32.Parse(test.stepSix.Interest_2)];

            return View(form);
        }

        //-------------------------------------------------------------------------------------------

        [HttpPost, NoDirectAccess]
        [MultipleButton(Name = "action", Argument = "Step_07Next")]
        public ActionResult Step_07Next(TestStepSeven form)
        {
            if (form.Ability_1.Equals(form.Ability_2))
            {
                ModelState.AddModelError("Ability_2", "Primary and Secondary Abilities/Skills cannot be the same.");
            }

            if (!ModelState.IsValid)
            {
                return View(form);
            }

            CareerPathfinderTest test = Session["TestObject"] as CareerPathfinderTest;
            test.CurrentStep = 8;
            test.stepSeven = form;

            return RedirectToAction("Step_08");
        }

        //-------------------------------------------------------------------------------------------

        [HttpPost, NoDirectAccess]
        [MultipleButton(Name = "action", Argument = "Step_07Previous")]
        public ActionResult Step_07Previous(TestStepSeven form)
        {
            CareerPathfinderTest test = Session["TestObject"] as CareerPathfinderTest;
            test.stepSeven = form;

            return RedirectToAction("Step_06");
        }

        //-------------------------------------------------------------------------------------------
        // STEP EIGHT
        //-------------------------------------------------------------------------------------------

        [NoDirectAccess]
        public ActionResult Step_08()
        {
            ViewBag.StepNumber = 8;

            CareerPathfinderTest test = Session["TestObject"] as CareerPathfinderTest;
            TestStepEight form = test.stepEight;
            
            return View(form);
        }

        //-------------------------------------------------------------------------------------------

        [HttpPost, NoDirectAccess]
        [MultipleButton(Name = "action", Argument = "Step_08Next")]
        public ActionResult Step_08Next(TestStepEight form)
        {
            if (form.CareerChoice_1.Equals(form.CareerChoice_2))
                ModelState.AddModelError("CareerChoice_2", "Primary and Secondary Career Choice cannot be the same.");
            if (form.CareerGroup_1.Equals(form.CareerGroup_2))
                ModelState.AddModelError("CareerGroup_2", "Primary and Secondary Career Group cannot be the same.");
            if (form.CareerValue_1.Equals(form.CareerValue_2))
                ModelState.AddModelError("CareerValue_2", "Primary and Secondary Career Value cannot be the same.");

            if (!ModelState.IsValid)
            {
                return View(form);
            }

            CareerPathfinderTest test = Session["TestObject"] as CareerPathfinderTest;
            test.CurrentStep = 9;
            test.stepEight = form;

            return RedirectToAction("Step_09");
        }

        //-------------------------------------------------------------------------------------------

        [HttpPost, NoDirectAccess]
        [MultipleButton(Name = "action", Argument = "Step_08Previous")]
        public ActionResult Step_08Previous(TestStepEight form)
        {
            CareerPathfinderTest test = Session["TestObject"] as CareerPathfinderTest;
            test.stepEight = form;

            return RedirectToAction("Step_07");
        }

        //-------------------------------------------------------------------------------------------
        // STEP NINE
        //-------------------------------------------------------------------------------------------

        [NoDirectAccess]
        public ActionResult Step_09()
        {
            ViewBag.StepNumber = 9;

            CareerPathfinderTest test = Session["TestObject"] as CareerPathfinderTest;
            TestStepNine form = test.stepNine;

            return View(form);
        }

        //-------------------------------------------------------------------------------------------

        [HttpPost, NoDirectAccess]
        [MultipleButton(Name = "action", Argument = "Step_09Next")]
        public ActionResult Step_09Next(TestStepNine form)
        {
            if (!ModelState.IsValid)
            {
                return View(form);
            }

            CareerPathfinderTest test = Session["TestObject"] as CareerPathfinderTest;
            test.CurrentStep = 10;
            test.stepNine = form;

            return RedirectToAction("Step_10");
        }

        //-------------------------------------------------------------------------------------------

        [HttpPost, NoDirectAccess]
        [MultipleButton(Name = "action", Argument = "Step_09Previous")]
        public ActionResult Step_09Previous(TestStepNine form)
        {
            CareerPathfinderTest test = Session["TestObject"] as CareerPathfinderTest;
            test.stepNine = form;

            return RedirectToAction("Step_08");
        }

        //-------------------------------------------------------------------------------------------
        // STEP TEN
        //-------------------------------------------------------------------------------------------

        [NoDirectAccess]
        public ActionResult Step_10()
        {
            ViewBag.StepNumber = 10;

            CareerPathfinderTest test = Session["TestObject"] as CareerPathfinderTest;
            TestStepTen form = test.stepTen;

            return View(form);
        }

        //-------------------------------------------------------------------------------------------

        [HttpPost, NoDirectAccess]
        [MultipleButton(Name = "action", Argument = "Step_10Next")]
        public ActionResult Step_10Next(TestStepTen form)
        {
            if (!ModelState.IsValid)
            {
                return View(form);
            }

            CareerPathfinderTest test = Session["TestObject"] as CareerPathfinderTest;
            test.CurrentStep = 11;
            test.stepTen = form;

            return RedirectToAction("Test_Complete");
        }

        //-------------------------------------------------------------------------------------------

        [HttpPost, NoDirectAccess]
        [MultipleButton(Name = "action", Argument = "Step_10Previous")]
        public ActionResult Step_10Previous(TestStepTen form)
        {
            CareerPathfinderTest test = Session["TestObject"] as CareerPathfinderTest;
            test.stepTen = form;

            return RedirectToAction("Step_09");
        }        
        //-------------------------------------------------------------------------------------------
        // TEST COMPLETE
        //-------------------------------------------------------------------------------------------

        [NoDirectAccess]
        public ActionResult Test_Complete()
        {
            ViewBag.StepNumber = 11;

            var testTable = Database.Session.Query<MemberTest>().FirstOrDefault(t => t.MemberUser.User.Id == CookieData.GetUserId(User.Identity.Name));
            CareerPathfinderTest test = Session["TestObject"] as CareerPathfinderTest;
            testTable.TestObject = TestSerializer.SerializeTest(test);

            return View();
        }

        //-------------------------------------------------------------------------------------------
        // SUBMIT TEST
        //-------------------------------------------------------------------------------------------

        [NoDirectAccess]
        public async Task<ActionResult> Submit_Test()
        {
            var memberTest = Database.Session.Query<MemberTest>().FirstOrDefault(t => t.MemberUser.User.Id == CookieData.GetUserId(User.Identity.Name));
            memberTest.SubmittedDate = DateTime.Now;
            memberTest.TestToResubmit = false;

            //-- send email to admin.
            string subject = "New Test Submitted - " + memberTest.MemberUser.FirstNames + " " + memberTest.MemberUser.Surname;
            string body = "A new completed test has been submitted by: " + memberTest.MemberUser.User.Email;
            MailMessage message = Mail.SendSimpleMailMessage("careerpathfindertest@gmail.com", subject, body); 

            using (var smtp = new SmtpClient())
            {
                await smtp.SendMailAsync(message);
            }
            //-- end send email to admin

            TempData["DashboardMessage"] = "Congratulations! Your test has been submitted for reviewing!";
            log.Info(memberTest.MemberUser.User.Email + " has submitted their test");

            return RedirectToAction("Dashboard", "Account", new { area = "member" });
        }

        //-------------------------------------------------------------------------------------------

        [NoDirectAccess]
        public ActionResult Submit_Later()
        {
            TempData["DashboardMessage"] = "Test Complete! Don't forget to submit your test once you're ready!";
            return RedirectToAction("Dashboard", "Account", new { area = "member" });
        }

        //-------------------------------------------------------------------------------------------

        [NoDirectAccess]
        public ActionResult Test_Already_Submitted()
        {
            ViewBag.StepNumber = 11;
            return View();
        }

        //-------------------------------------------------------------------------------------------

        private bool HasMemberPaidForTest()
        {
            var invoice = Database.Session.Query<MemberInvoice>().FirstOrDefault(t => t.MemberUser.User.Id == CookieData.GetUserId(User.Identity.Name));

            if(invoice == null)
            {
                return false;
            }
            return invoice.PaymentReceived;
        }

        //-------------------------------------------------------------------------------------------
    }
}