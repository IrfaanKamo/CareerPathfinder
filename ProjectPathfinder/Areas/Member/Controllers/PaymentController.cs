using NHibernate.Linq;
using ProjectPathfinder.Infrastructure.Attributes;
using ProjectPathfinder.Infrastructure.Helpers;
using ProjectPathfinder.Infrastructure.PaymentProvider;
using ProjectPathfinder.Infrastructure.Utilities;
using ProjectPathfinder.Models;
using System;
using System.Collections;
using System.Collections.Specialized;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace ProjectPathfinder.Areas.Member.Controllers
{
    [Authorize(Roles = "member")]
    public class PaymentController : Controller
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        //-------------------------------------------------------------------------------------------

        [NoDirectAccess]
        public ActionResult PurchaseTest()
        {
            return View();
        }

        //-------------------------------------------------------------------------------------------

        [HttpPost]
        [ActionName("PurchaseTest")]
        public async Task<ActionResult> PurchasingTest()
        {
            var member = Database.Session.Query<MemberUser>().FirstOrDefault(t => t.User.Id == CookieData.GetUserId(User.Identity.Name));
            var testProduct = Database.Session.Query<Product>().FirstOrDefault(t => t.Id == 1);

            //null error checking
            if(member == null)
            {
                log.Error("Test Purchase - Member returned null.");
            }
            if (testProduct == null)
            {
                log.Error("Test Purchase - Test Product returned null.");
            }

            string memberEmail = member.User.Email;

            //check invoice item already exists
            var existingInvoiceItem = Database.Session.Query<MemberInvoiceItem>()
                                            .FirstOrDefault(t => t.Product.Id == 1 && t.MemberInvoice.MemberUser.Id == member.Id);

            if(existingInvoiceItem != null )
            {
                if(existingInvoiceItem.MemberInvoice.PaymentReceived)
                {
                    //member has already paid for this item
                    log.Info("Test Purchase - " + memberEmail + " already paid for the item.");
                    return RedirectToRoute("home");
                }
                else
                {
                    //delete old invoice and create a new one afterwards
                    log.Info("Test Purchase - Deleting old invoice for " + memberEmail + "  and creating a new one.");
                    Database.Session.Delete(existingInvoiceItem.MemberInvoice);
                }                                  
            }

            //determine discount to be applied
            double discountPercent = 0.0;
            if(!Double.TryParse(ConfigurationManager.AppSettings["Test_DiscountPercent"], out discountPercent))
            {
                discountPercent = 0.0;
            }
            else if(discountPercent < 0.0 || discountPercent > 100.0)
            {
                discountPercent = 0.0;
            }
            log.Info("Test Purchase - Applied discount for " + memberEmail + ".");

            //final price after discount
            double finalPaymentPrice = testProduct.Price - (testProduct.Price * (discountPercent / 100));
            bool isTestFree = finalPaymentPrice == 0.0;

            //create invoice for payment into database
            var invoice = new MemberInvoice();
            invoice.DateOfPurchase = DateTime.Now;
            invoice.MemberUser = member;
            invoice.TotalAmount = 0.0;
            invoice.PaymentReceived = isTestFree; //test is considered paid if it's free otherwise payfast class will set this to true after payment is received.

            Database.Session.Save(invoice);
            log.Info("Test Purchase - Invoice saved for " + memberEmail + ".");

            //create items to be attached to the above invoice
            var invoiceItem = new MemberInvoiceItem();
            invoiceItem.MemberInvoice = invoice;
            invoiceItem.Product = testProduct;
            invoiceItem.Price = testProduct.Price;
            invoiceItem.DiscountApplied = discountPercent;

            Database.Session.Save(invoiceItem);
            log.Info("Test Purchase - Inovice item saved for " + memberEmail + ".");

            //update the invoice's total amount
            invoice.TotalAmount = finalPaymentPrice; //currently selling only one product
            
            if (isTestFree)
            {
                //return success if test is free
                return RedirectToAction("PaymentSuccess");
            }

            //create a local payment to send to PayFast if test is not free
            PaymentRequest paymentRequest = new PaymentRequest();
            paymentRequest.CreatePaymentRequest(testProduct.Name,
                                                testProduct.Description,
                                                finalPaymentPrice.ToString("0.00", System.Globalization.CultureInfo.InvariantCulture),
                                                CookieData.GetUserId(User.Identity.Name),
                                                memberEmail);
            log.Info("Test Purchase - Created local PayFast payment for " + memberEmail + ".");

            //-- send email to admin.
            string subject = "Potential Payment in Progress: " + finalPaymentPrice.ToString();
            string body = "A potential payment is about to be made by " + member.FirstNames + " " + member.Surname;
            MailMessage message = Mail.SendSimpleMailMessage("careerpathfindertest@gmail.com", subject, body);

            using (var smtp = new SmtpClient())
            {
                await smtp.SendMailAsync(message);
            }

            log.Info("Test Purchase - Sent payment email to admin.");
            //-- end send email to admin

            return Redirect(paymentRequest.PaymentUrl);
        }

        //-------------------------------------------------------------------------------------------

        public ActionResult PaymentSuccess()
        {
            return View();
        }

        //-------------------------------------------------------------------------------------------

        public ActionResult PaymentCancelled()
        {
            return View();
        }

        //-------------------------------------------------------------------------------------------
        [AllowAnonymous]
        public ActionResult PaymentPayFastNotify()
        {
            log.Info("PayFast - Entered notify method");
            string strPostedVariables = "";
            //stores our key value pairs to post to payfast
            NameValueCollection arrPostedVariables = new NameValueCollection();
            //stores all the variables from the query string.
            NameValueCollection payfastPostedVars = Request.Form;

            //--------------

            string key = "";
            string value = "";
            //cycle through all the key-value pairs
            for (int i = 0; i < payfastPostedVars.Count; i++)
            {
                key = payfastPostedVars.Keys[i]; //key name
                value = payfastPostedVars[i]; // value

                log.Info("Payfast - Query String: Key - " + key + " | Value - " + value);

                if (key != "signature") // do not add the signature when we post to pf
                {
                    strPostedVariables += key + "=" + value + "&";
                    arrPostedVariables.Add(key, value);
                }
            }
            //remove the last &
            strPostedVariables = strPostedVariables.TrimEnd(new char[] { '&' });

            //--------------

            //id given to us by payfast - use it keep track of the order
            string processorOrderId = payfastPostedVars["pf_payment_id"];

            //--------------

            //payment mode (need to make a static class that will give us this)
            string site = "";
            string merchant_id = "";
            string paymentMode = System.Configuration.ConfigurationManager.AppSettings["PayFast_PaymentMode"];

            if (paymentMode == "test")
            {
                log.Info("PayFast - Using Test mode");
                site = "https://sandbox.payfast.co.za/eng/query/validate";
                merchant_id = "10003370";
            }
            else if (paymentMode == "live")
            {
                site = "https://www.payfast.co.za/eng/query/validate";
                merchant_id = System.Configuration.ConfigurationManager.AppSettings["PayFast_MerchantID"];
            }
            else
            {
                throw new InvalidOperationException("Cannot process payment if PaymentMode (in web.config) value is unknown.");
            }

            //--------------

            //get the posted signature from the form.
            string postedSignature = payfastPostedVars["signature"];

            if (string.IsNullOrEmpty(postedSignature))
            {
                log.Error("Payfast - Signature from payfast is null.");
                throw new Exception("Signature parameter cannot be null");
            }                

            //--------------

            //check if this is a legitimate request from the payment processor
            //(are we correct merchant? is the host server a payfast server?)
            PerformSecurityChecks(arrPostedVariables, merchant_id);

            //The request is legitimate. Post back to payment processor to validate the data received
            ValidateData(site, arrPostedVariables);

            //all is valid, process the order
            ProcessOrder(arrPostedVariables);

            return new HttpStatusCodeResult(HttpStatusCode.OK);
        }

        //-------------------------------------------------------------------------------------------

        private void PerformSecurityChecks(NameValueCollection arrPostedVariables, string merchant_id)
        {
            log.Info("PayFast - Doing security checks");
            //verify that we are the intended merchant
            string receivedMerchant = arrPostedVariables["merchant_id"];

            if (receivedMerchant != merchant_id)
                throw new Exception("Mechant ID mismatch");

            //--------------

            //verify that the request comes from the payment processor's servers.
            //get all valid websites from payment processor
            string[] validSites = new string[] { "sandbox.payfast.co.za",
                                                 "www.payfast.co.za",
                                                 "w1w.payfast.co.za",
                                                 "w2w.payfast.co.za" };

            //get the requesting ip address
            string requestIp = Request.ServerVariables["REMOTE_ADDR"];
            if (string.IsNullOrEmpty(requestIp))
                throw new Exception("IP address cannot be null");

            //verify ip address is from payfast
            if (!IsIpAddressInList(validSites, requestIp))
                throw new Exception("IP address invalid");
        }

        //-------------------------------------------------------------------------------------------

        private bool IsIpAddressInList(string[] validSites, string ipAddress)
        {
            log.Info("PayFast - Checking valid IP");
            ArrayList validIps = new ArrayList();

            //cycle through all validSites and obtain their IP
            for (int i = 0; i < validSites.Length; i++)
            {
                validIps.AddRange(Dns.GetHostAddresses(validSites[i]));
            }

            //IP of the host that contacted us
            IPAddress ipObject = IPAddress.Parse(ipAddress);

            //verify that the host who contacted us is a payfast host.
            if (validIps.Contains(ipObject))
            {
                log.Info("PayFast - Host is verified as payfast");
                return true;
            }
            return false;
        }

        //-------------------------------------------------------------------------------------------

        private void ValidateData(string site, NameValueCollection arrPostedVariables)
        {
            log.Info("PayFast - Validating data");
            WebClient webClient = null;

            try
            {
                webClient = new WebClient();
                byte[] responseArray = webClient.UploadValues(site, arrPostedVariables);

                // Get the response and replace the line breaks with spaces
                string result = Encoding.ASCII.GetString(responseArray);
                result = result.Replace("\r\n", " ").Replace("\r", "").Replace("\n", " ");

                // Was the data valid?
                if (result == null || !result.StartsWith("VALID"))
                    throw new Exception("Data validation failed");
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (webClient != null)
                    webClient.Dispose();
            }
        }

        //-------------------------------------------------------------------------------------------

        private bool ProcessOrder(NameValueCollection arrPostedVariables)
        {
            log.Info("PayFast - Processing order");
            // Determine from payment status if we are supposed to credit or not
            string paymentStatus = arrPostedVariables["payment_status"];

            //get the user id from payfast
            int userId = Int32.Parse(arrPostedVariables["m_payment_id"]);

            //currently have only one product, so the invoice has to be for that - TODO: check product id
            var invoice = Database.Session.Query<MemberInvoice>()
                            .FirstOrDefault(user => user.MemberUser.User.Id == userId);

            try
            {
                if (paymentStatus == "COMPLETE")
                {
                    invoice.PaymentReceived = true;
                    log.Info("PayFast - Payment recieved from user id: " + userId);
                    return true;
                }
                else if (paymentStatus == "FAILED")
                {
                    invoice.PaymentReceived = false;
                    log.Info("PayFast - Payment failed for user id: " + userId);
                    return false;
                }
                else
                {
                    log.Warn("PayFast - Invalid payment status");
                    return false;
                }
            }
            catch (Exception ex)
            {
                invoice.PaymentReceived = false;
                log.Error("PayFast - error handling payment for user " + userId + " "
                          + "with message: " + ex.Message);
                return false;
            }
        }

        //-------------------------------------------------------------------------------------------
    }
}