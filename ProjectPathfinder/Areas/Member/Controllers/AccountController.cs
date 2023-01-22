using NHibernate.Linq;
using ProjectPathfinder.Areas.Member.ViewModels;
using ProjectPathfinder.Infrastructure.Attributes;
using ProjectPathfinder.Infrastructure.Helpers;
using ProjectPathfinder.Infrastructure.Utilities;
using ProjectPathfinder.Models;
using System;
using System.IO;
using System.Linq;
using System.Web.Mvc;
using System.Web.Security;

namespace ProjectPathfinder.Areas.Member.Controllers
{
    [Authorize(Roles = "member")]
    [SelectedTab("dashboard")]
    public class AccountController : Controller
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        //-------------------------------------------------------------------------------------------

        public ActionResult Index()
        {
            return RedirectToAction("Dashboard");
        }

        //-------------------------------------------------------------------------------------------

        public ActionResult Dashboard()
        {
            return View();
        }

        //-------------------------------------------------------------------------------------------

        public ActionResult Personal_Details()
        {
            var user = Database.Session.Load<User>(CookieData.GetUserId(User.Identity.Name));
            var member = Database.Session.Query<MemberUser>().FirstOrDefault(u => u.User.Id == user.Id);
            if (member == null)
                HttpNotFound();

            return View(new AccountPersonalDetails
            {
                FirstName = member.FirstNames,
                LastName = member.Surname,
                BirthdayDay = member.DateOfBirth.Day.ToString(),
                BirthdayMonth = member.DateOfBirth.Month.ToString(),
                BirthdayYear = member.DateOfBirth.Year.ToString(),
                Grade = member.Grade.ToString(),
                ContactNumber = member.PrimaryNumber,
                School = member.School,
                Email = user.Email
            });
        }

        //-------------------------------------------------------------------------------------------

        [HandleError(ExceptionType = typeof(HttpAntiForgeryException))]
        [HttpPost,ValidateAntiForgeryToken]
        public ActionResult Personal_Details(AccountPersonalDetails form)
        {
            var user = Database.Session.Load<User>(CookieData.GetUserId(User.Identity.Name));
            var member = Database.Session.Query<MemberUser>().FirstOrDefault(u => u.User.Id == user.Id);

            // update flags
            bool updatePassword = (form.ExistingPassword != null || form.Password != null);
            bool updateEmail = (form.ConfirmEmail != null);

            // basic error checking
            if (Database.Session.Query<User>().Any(e => e.Email == form.Email && e.Email != user.Email))
                ModelState.AddModelError("Email", "Email must be unique");

            if (user.Email != form.Email && form.Email != form.ConfirmEmail)
                ModelState.AddModelError("ConfirmEmail", "The email you have entered does not match");

            if(updatePassword)
            {
                if (!user.CheckPassword(form.ExistingPassword))
                    ModelState.AddModelError("ExistingPassword", "Please ensure the existing password is correct");
                if (form.Password.Length < 6)
                    ModelState.AddModelError("Password", "New Password should contain atleast 6 characters");
                if(form.Password != form.ConfirmPassword)
                    ModelState.AddModelError("ConfirmPassword", "New Passwords do not match");

                //check password is strong enough
                if (PasswordEssentials.IsPasswordRequirementMet(form.Password) == false)
                {
                    ModelState.AddModelError("Password", "The chosen password is not strong enough. Please ensure it contains at least " +
                                                         "8 characters including a digit, uppercase or special character");
                }
            }



            if (!ModelState.IsValid)
            {
                return View(form);
            }

            // update the user fields
            if (updateEmail)
                user.Email = form.Email;
            if (updatePassword)
                user.SetPassword(form.Password);

            //update the member fields
            member.FirstNames = form.FirstName;
            member.Surname = form.LastName;
            member.PrimaryNumber = form.ContactNumber;
            member.School = form.School;
            member.Grade = Int32.Parse(form.Grade);
            member.DateOfBirth = new DateTime(int.Parse(form.BirthdayYear),
                                              int.Parse(form.BirthdayMonth),
                                              int.Parse(form.BirthdayDay));

            //It's pointless to call 'Database.Session.Update(member)' since nhibernate tracks changes as you update the variables
            TempData["DashboardMessage"] = "Awesome! Your details have been updated!";
            log.Info(member.User.Email + " updated their profile information");

            return RedirectToAction("Dashboard");
        }

        //-------------------------------------------------------------------------------------------

        public ActionResult Invoice()
        {
            var invoiceTable = Database.Session.Query<MemberInvoice>().FirstOrDefault(t => t.MemberUser.User.Id == CookieData.GetUserId(User.Identity.Name));

            //No invoice generated if user has not attempted to pay
            if(invoiceTable == null)
            {
                return Content("No Invoice Available for Free Users");
            }

            var invoiceItem = Database.Session.Query<MemberInvoiceItem>().FirstOrDefault(t => t.MemberInvoice.Number == invoiceTable.Number);
            AccountInvoice invoiceData = new AccountInvoice();

            invoiceData.FirstName = invoiceTable.MemberUser.FirstNames;
            invoiceData.LastName = invoiceTable.MemberUser.Surname;
            invoiceData.ContactNo = invoiceTable.MemberUser.PrimaryNumber;
            invoiceData.Email = invoiceTable.MemberUser.User.Email;

            invoiceData.OrderNo = invoiceTable.Number.ToString();
            invoiceData.InvoiceDate = invoiceTable.DateOfPurchase.ToString("dd-MM-yyyy");

            double itemPrice = invoiceItem.Price;
            double itemDiscount = invoiceItem.DiscountApplied;
            double itemTotal = itemPrice - ((itemPrice) * (itemDiscount / 100));

            invoiceData.ItemName = invoiceItem.Product.Name;
            invoiceData.ItemPrice = itemPrice.ToString();
            invoiceData.ItemDiscount = itemDiscount.ToString();
            invoiceData.ItemTotal = itemTotal.ToString();

            invoiceData.FinalTotal = itemTotal.ToString();
            
            return View(invoiceData);
        }

        //-------------------------------------------------------------------------------------------

        public ActionResult Result_Status()
        {
            return View(new AccountResultStatus
            {
                RegisteredDate = Database.Session.Query<MemberUser>()
                                    .FirstOrDefault(u => u.User.Id == CookieData.GetUserId(User.Identity.Name)).DateOfRegistration,
                SubmittedTestDate = Database.Session.Query<MemberTest>()
                                    .FirstOrDefault(u => u.MemberUser.User.Id == CookieData.GetUserId(User.Identity.Name)).SubmittedDate,
                ResultsObtainedDate = Database.Session.Query<MemberResult>()
                                    .FirstOrDefault(u => u.MemberUser.User.Id == CookieData.GetUserId(User.Identity.Name)).SubmittedDate
            });
        }

        //-------------------------------------------------------------------------------------------

        public ActionResult Results()
        {
            return View();
        }

        //-------------------------------------------------------------------------------------------

        public ActionResult Delete_Account()
        {
            return View();
        }

        //-------------------------------------------------------------------------------------------

        [HandleError(ExceptionType = typeof(HttpAntiForgeryException))]
        [HttpPost, ValidateAntiForgeryToken]
        [ActionName("Delete_Account")]
        public ActionResult Deleted_Account()
        {
            var user = Database.Session.Load<User>(CookieData.GetUserId(User.Identity.Name));
            if (user == null)
                return HttpNotFound();

            log.Warn(user.Email + " is attempting to delete his/her account");
            Database.Session.Delete(user);
            FormsAuthentication.SignOut();            

            return RedirectToAction("Account_Deleted", "Account", new { area = "" });
        }

        //-------------------------------------------------------------------------------------------

        public ActionResult Resources()
        {
            var invoice = Database.Session.Query<MemberInvoice>().FirstOrDefault(t => t.MemberUser.User.Id == CookieData.GetUserId(User.Identity.Name));
            ViewBag.AllowPaidMemberResource = false;

            if(invoice != null)
            {
                ViewBag.AllowPaidMemberResource = invoice.PaymentReceived;
            }
            
            return View();
        }

        //-------------------------------------------------------------------------------------------

        public ActionResult Resource_Viewer(string fileName)
        {
            string path = Path.Combine(@"~\Assets\Documents", fileName);

            if(System.IO.File.Exists(Server.MapPath(path)) == false)
            {
                log.Error("PDF not found: " + path);
                return HttpNotFound();
            }
            return File(path, "application/pdf");            
        }
    }
}