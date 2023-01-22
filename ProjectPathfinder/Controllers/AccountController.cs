using NHibernate.Linq;
using ProjectPathfinder.Infrastructure.Attributes;
using ProjectPathfinder.Infrastructure.Helpers;
using ProjectPathfinder.Infrastructure.Utilities;
using ProjectPathfinder.Models;
using ProjectPathfinder.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Security;

namespace ProjectPathfinder.Controllers
{
    public class AccountController : Controller
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        //-------------------------------------------------------------------------------------------

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToRoute("home");
        }

        //-------------------------------------------------------------------------------------------

        public ActionResult Login()
        {
            return View(new AccountLogin()
            {
            });
        }

        //-------------------------------------------------------------------------------------------

        [HandleError(ExceptionType = typeof(HttpAntiForgeryException))]
        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Login(AccountLogin form, string returnUrl)
        {
            var user = Database.Session.Query<User>().FirstOrDefault(u => u.Email == form.Email);

            if (user == null)
                Models.User.FakeHash();

            if(user == null || !user.CheckPassword(form.Password))
                ModelState.AddModelError("Email", "Username or password is incorrect");

            if (!ModelState.IsValid)
            {
                form.Password = "";
                return View(form);
            }                

            var member = Database.Session.Query<MemberUser>().FirstOrDefault(m => m.User.Id == user.Id);
            bool isAdmin = member == null;

            if (isAdmin == false)
            {
                //user is a member
                string cookieUsername = CookieData.SetCookieName(user.Id.ToString(), member.FirstNames);
                FormsAuthentication.SetAuthCookie(cookieUsername, true);
            }            
            else
            {
                //user is an admin
                string cookieUsername = CookieData.SetCookieName(user.Id.ToString(), user.Email);
                FormsAuthentication.SetAuthCookie(cookieUsername, true);                
            }

            log.Info(user.Email + " logged in successfully");

            //-- return to respective page
            if(isAdmin)
            {
                return RedirectToAction("Index", "Account", new { area = "admin" });
            }

            // TODO: check the return url is only for our site, for security purposes
            if (!string.IsNullOrWhiteSpace(returnUrl))
            {
                TempData["DashboardMessage"] = "Brilliant! You are now logged in!";
                return Redirect(returnUrl);
            }            

            return RedirectToRoute("home");
        }

        //-------------------------------------------------------------------------------------------

        public ActionResult Sign_Up()
        {  
            //don't allow a logged in user to sign up
            if(User.Identity.IsAuthenticated)
            {
                return RedirectToRoute("home");
            }          

            return View(new AccountSignUp()
            {
            });
        }

        //-------------------------------------------------------------------------------------------

        [HandleError(ExceptionType = typeof(HttpAntiForgeryException))]
        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Sign_Up(AccountSignUp form)
        {
            //check email is unique
            if (Database.Session.Query<User>().Any(u => u.Email == form.Email))
            {
                ModelState.AddModelError("Email", "The email address specified already exists");
            }

            //check password is strong enough
            if (PasswordEssentials.IsPasswordRequirementMet(form.Password) == false)
            {
                ModelState.AddModelError("Password", "The chosen password is not strong enough. Please ensure it contains at least " + 
                                                     "8 characters, a digit and an uppercase or special character");
            }

            if (!ModelState.IsValid)
            {
                return View(form);
            }

            try
            {
                var user = new User();
                user.Email = form.Email;
                user.SetPassword(form.Password);
                user.Role = Database.Session.Query<Role>().FirstOrDefault(u => u.Name == "member");

                var member = new MemberUser();
                member.User = user;
                member.FirstNames = form.FirstName;
                member.Surname = form.LastName;
                member.DateOfBirth = new DateTime(int.Parse(form.BirthdayYear),
                                                  int.Parse(form.BirthdayMonth),
                                                  int.Parse(form.BirthdayDay));
                member.Grade = int.Parse(form.Grade);
                member.PrimaryNumber = form.ContactNumber;
                member.School = form.School;
                member.DateOfRegistration = DateTime.Now;

                var test = new MemberTest();
                test.MemberUser = member;

                var result = new MemberResult();
                result.MemberUser = member;

                Database.Session.Save(user);
                Database.Session.Save(member);
                Database.Session.Save(test);
                Database.Session.Save(result);

                //TODO: Send email to user for successfull registration

                string cookieUsername = CookieData.SetCookieName(user.Id.ToString(), member.FirstNames);
                FormsAuthentication.SetAuthCookie(cookieUsername, true);
            }
            catch(Exception e)
            {
                log.Error("Sign up failed: " + e.Message);
            }          

            return RedirectToRoute("home");
        }

        //-------------------------------------------------------------------------------------------

        [NoDirectAccess]
        public ActionResult Forgot_Password()
        {
            return View();
        }

        //-------------------------------------------------------------------------------------------

        [HandleError(ExceptionType = typeof(HttpAntiForgeryException))]
        [HttpPost, ValidateAntiForgeryToken]
        public async Task<ActionResult> Forgot_Password(AccountForgotPassword form)
        {
            //check that there isn't already a password reset request pending
            var passwordReset = Database.Session.Query<PasswordForgotRequest>().FirstOrDefault(u => u.Email == form.Email);
            if (passwordReset != null && passwordReset.ExpiryDate > DateTime.Now)
            {
                ModelState.AddModelError("Email", "A password change request was recently submitted for this email");
            }

            if (!ModelState.IsValid)
            {
                return View(form);
            }

            // create the password reset request
            var passwordForgot = new PasswordForgotRequest();

            string guid = Guid.NewGuid().ToString();
            var checkConflictGuid = Database.Session.Query<PasswordForgotRequest>().FirstOrDefault(u => u.Id == guid);

            while(checkConflictGuid != null)
            {
                guid = Guid.NewGuid().ToString();
                checkConflictGuid = Database.Session.Query<PasswordForgotRequest>().FirstOrDefault(u => u.Id == guid);
            }

            passwordForgot.Id = guid;
            passwordForgot.Email = form.Email;
            passwordForgot.ExpiryDate = DateTime.Now.AddDays(1);

            Database.Session.Save(passwordForgot);           

            //gets the url address for the password reset using the action and controller name
            string link = this.Url.Action("Reset_Password", "Account", new { id = guid, area = "" }, this.Request.Url.Scheme);

            //-- send email to user.
            string body = Mail.GetLinkButtonMessageBody(Server.MapPath(@"~/Assets/EmailTemplates/password_reset.html"), link);
            MailMessage message = Mail.SendSimpleMailMessage(form.Email, "CareerPathfinder - Password Reset Request", body);

            using (var smtp = new SmtpClient())
            {                
                await smtp.SendMailAsync(message);
            }
            //-- end send email to user

            TempData["ResetPasswordMessage"] = "An email has been sent to you with a link to reset your password." +
                                               " If the email does not show up in your inbox, please check your spam folder. ";
            return RedirectToAction("Reset_Password_Notice");
        }

        //-------------------------------------------------------------------------------------------

        [NoDirectAccess]
        public ActionResult Reset_Password_Notice()
        {
            return View();
        }

        //-------------------------------------------------------------------------------------------

        public ActionResult Reset_Password(string id)
        {
            //check if the id exists
            var passwordReset = Database.Session.Query<PasswordForgotRequest>().FirstOrDefault(u => u.Id == id);
            if (passwordReset == null)
            {
                return HttpNotFound();
            }

            //check if a user with the submitted email is registered
            var user = Database.Session.Query<User>().FirstOrDefault(u => u.Email == passwordReset.Email);
            if (user == null)
            {
                return HttpNotFound();
            }

            //check if the password reset request is still active
            if(passwordReset.ExpiryDate < DateTime.Now)
            {
                TempData["ResetPasswordMessage"] = "The password reset for this account has expired, please resubmit a new reset request.";
                return RedirectToAction("Reset_Password_Notice");
            }

            //once we reach here, the user has successfully requested a password reset
            return View(new AccountResetPassword
            {
                Id = id
            });
        }

        //-------------------------------------------------------------------------------------------

        [HandleError(ExceptionType = typeof(HttpAntiForgeryException))]
        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Reset_Password(AccountResetPassword form)
        {
            if (PasswordEssentials.IsPasswordRequirementMet(form.Password) == false)
            {
                ModelState.AddModelError("Password", "The chosen password is not strong enough. Please ensure it contains at least " +
                                                     "8 characters including a digit, uppercase or special character");
            }

            if (!ModelState.IsValid)
            {
                return View(form);
            }

            var passwordReset = Database.Session.Query<PasswordForgotRequest>().FirstOrDefault(u => u.Id == form.Id);
            var user = Database.Session.Query<User>().FirstOrDefault(u => u.Email == passwordReset.Email);

            // set the user password and disable the current password reset
            user.SetPassword(form.Password);
            passwordReset.ExpiryDate = DateTime.Now;
            log.Info( user.Email + " has reset their password");

            TempData["ResetPasswordMessage"] = "Password for " + user.Email + " has been successfully reset.";
            return RedirectToAction("Reset_Password_Notice");
        }

        //-------------------------------------------------------------------------------------------

        [NoDirectAccess]
        public ActionResult Account_Deleted()
        {
            return View();
        }

        //-------------------------------------------------------------------------------------------
    }
}