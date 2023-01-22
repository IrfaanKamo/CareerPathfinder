using NHibernate.Linq;
using ProjectPathfinder.Areas.Admin.ViewModels;
using ProjectPathfinder.Infrastructure.Utilities;
using ProjectPathfinder.Models;
using ProjectPathfinder.Result;
using ProjectPathfinder.Result.Utilities;
using ProjectPathfinder.Test;
using ProjectPathfinder.Test.TestObjects;
using ProjectPathfinder.Test.Utilities;
using System;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace ProjectPathfinder.Areas.Admin.Controllers
{
    [Authorize(Roles = "admin")]
    public class MemberResultController : Controller
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        //-------------------------------------------------------------------------------------------

        public ActionResult Index(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index", "Member", new { area = "admin" });
            }

            var resultTable = Database.Session.Query<MemberResult>().FirstOrDefault(t => t.MemberUser.Id == id);            

            //create or restore a result object
            if (resultTable.ResultObject == null)
            {
                Session["ResultObject"] = new CareerPathfinderResult();
                log.Info("Admin has created a new result object for member id: " + id);
            }
            else
            {
                //TODO: Make sure that if a result has been submitted, the admin cannot resubmit one, in the code somewhere below.
                CareerPathfinderResult result = ResultSerializer.DeserializeResult(resultTable.ResultObject);
                Session["ResultObject"] = result;
                log.Info("Admin has reviewed the result for member id: " + id);
            }

            return RedirectToAction("Feedback", new { id = id });
        }

        //-------------------------------------------------------------------------------------------
        // RESULT FEEDBACK
        //-------------------------------------------------------------------------------------------

        public ActionResult Feedback(int? id)
        {
            CareerPathfinderResult result = Session["ResultObject"] as CareerPathfinderResult;
            ResultFeedback form = result.resultFeedback;           

            var student = Database.Session.Query<MemberUser>().FirstOrDefault(t => t.Id == id);
            ViewBag.StudentDetails = student.FirstNames + " " + student.Surname +
                                     " | Grade: " + student.Grade +
                                     " | School: " + student.School;

            //We need the member id to upload the results to the right member in POST 
            int memberId = Convert.ToInt32(id);
            form.memberId = memberId;

            var testTable = Database.Session.Query<MemberTest>().FirstOrDefault(t => t.MemberUser.Id == memberId);
            CareerPathfinderTest memberTest = TestSerializer.DeserializeTest(testTable.TestObject);
            var resultTable = Database.Session.Query<MemberResult>().FirstOrDefault(t => t.MemberUser.Id == memberId);

            // uploading a new result?
            if(resultTable.SubmittedDate.Year < 2000)
            {
                //auto-fill fields from test object
                form.personality_01 = memberTest.stepFive.Word_Aesthetic;
                form.personality_02 = memberTest.stepFive.Word_Conventional;
                form.personality_03 = memberTest.stepFive.Word_Enterprising;
                form.personality_04 = memberTest.stepFive.Word_Investigative;
                form.personality_05 = memberTest.stepFive.Word_Realistic;
                form.personality_06 = memberTest.stepFive.Word_Social;
                form.interest_01 = Interests.Instance.interestsList[Convert.ToInt32(memberTest.stepSix.Interest_1)];
                form.interest_02 = Interests.Instance.interestsList[Convert.ToInt32(memberTest.stepSix.Interest_2)];
                form.abilities_01 = Abilities.Instance.abilitiesList[Convert.ToInt32(memberTest.stepSeven.Ability_1)];
                form.abilities_02 = Abilities.Instance.abilitiesList[Convert.ToInt32(memberTest.stepSeven.Ability_2)];
                form.careerValues_01 = CareerValues.Instance.careerList[Convert.ToInt32(memberTest.stepEight.CareerValue_1)];
                form.careerValues_02 = CareerValues.Instance.careerList[Convert.ToInt32(memberTest.stepEight.CareerValue_2)];
                form.preferredSubject_01 = memberTest.stepFour.mostLikedSubject1;
                form.preferredSubject_02 = memberTest.stepFour.mostLikedSubject2;
                form.careerGroup_01 = CareerGroups.Instance.careerList[Convert.ToInt32(memberTest.stepEight.CareerGroup_1)];
                form.careerGroup_02 = CareerGroups.Instance.careerList[Convert.ToInt32(memberTest.stepEight.CareerGroup_2)];
                form.suggestedCareer_01 = memberTest.stepEight.CareerChoice_1;
                form.suggestedCareer_02 = memberTest.stepEight.CareerChoice_2;
            }

            return View(form);
        }

        //-------------------------------------------------------------------------------------------

        [HttpPost]
        public async Task<ActionResult> Feedback(ResultFeedback form)
        {
            int value;
            if(int.TryParse(form.interestAbilitiesMatch_01, out value) == false ||
               int.TryParse(form.interestAbilitiesMatch_02, out value) == false)
            {
                ModelState.AddModelError("interestAbilitiesMatch_01", "Final percentages need to be in a numerical format.");
            }

            if (!ModelState.IsValid)
            {
                return View(form);
            }

            var resultTable = Database.Session.Query<MemberResult>().FirstOrDefault(t => t.MemberUser.Id == form.memberId);
            CareerPathfinderResult result = Session["ResultObject"] as CareerPathfinderResult;
            result.resultFeedback = form;

            resultTable.ResultObject = ResultSerializer.SerializeResult(result);
            resultTable.SubmittedDate = DateTime.Now;
            //Hard-coded submitter, until we create a list of admins. TODO: update email
            resultTable.Submitter = Database.Session.Query<User>().FirstOrDefault(t => t.Email == "careerpathfindertest@gmail.com");

            //-- send email to user.
            string body = Mail.GetLinkButtonMessageBody(Server.MapPath(@"~/Assets/EmailTemplates/result_uploaded.html"), "http://www.careerpathfinder.co.za/account/login");
            MailMessage message = Mail.SendSimpleMailMessage(resultTable.MemberUser.User.Email, "CareerPathfinder - Your Results are Available", body);

            using (var smtp = new SmtpClient())
            {
                await smtp.SendMailAsync(message);
            }
            //-- end send email to user.

            TempData["AdminResultMessage"] = "The result for " + resultTable.MemberUser.FirstNames + " " + resultTable.MemberUser.Surname + 
                                             " has been uploaded into their account.";
            log.Info(resultTable.MemberUser.User.Email + "'s result has been saved");

            return RedirectToAction("Index", "Account", new { area = "admin" });
        }

        //-------------------------------------------------------------------------------------------
    }
}