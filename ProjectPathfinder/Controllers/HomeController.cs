using ProjectPathfinder.Infrastructure.Attributes;
using ProjectPathfinder.ViewModels;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace ProjectPathfinder.Controllers
{
    public class HomeController : Controller
    {
        //-------------------------------------------------------------------------------------------

        [SelectedTab("home")]
        public ActionResult Index()
        {
            return View();
        }

        //-------------------------------------------------------------------------------------------

        [SelectedTab("pricing")]
        public ActionResult Pricing()
        {
            return View();
        }

        //-------------------------------------------------------------------------------------------

        [SelectedTab("about")]
        public ActionResult About_Us()
        {
            return View(new AboutUs()
            {
            });
        }

        //-------------------------------------------------------------------------------------------

        [HttpPost]
        public async Task<ActionResult> About_Us(AboutUs form)
        {
            try
            {
                //-- send email to admin.
                var message = new MailMessage();
                message.To.Add(new MailAddress("careerpathfindertest@gmail.com"));
                message.ReplyToList.Add(new MailAddress(form.Email));
                message.Subject = "Query from - " + form.Name + " (" + form.Email + ")";
                message.Body = form.Message;
                message.IsBodyHtml = true;

                using (var smtp = new SmtpClient())
                {
                    await smtp.SendMailAsync(message);
                }
                //-- end send email to admin
            }
            catch
            {
                TempData["AboutEmailMessage"] = "Oh no! Your email could not be sent, please ensure your email is correct.";
                return RedirectToAction("About_Us");
            }

            TempData["AboutEmailMessage"] = "Awesome! Your email has been sent to us, we will get back to you shortly.";
            return RedirectToAction("About_Us");
        }

        //-------------------------------------------------------------------------------------------
    }
}