using System;
using System.Configuration;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;

namespace ProjectPathfinder.Infrastructure.PaymentProvider
{
    public class PaymentRequest
    {
        private string m_site = "";
        private string m_merchant_id = "";
        private string m_merchant_key = "";

        public string PaymentUrl { get; private set; }

        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        //-------------------------------------------------------------------------------------------
        // CONSTRUCTOR
        //-------------------------------------------------------------------------------------------

        public PaymentRequest()
        {
        }

        //-------------------------------------------------------------------------------------------
        // PUBLIC METHODS
        //-------------------------------------------------------------------------------------------

        public bool CreatePaymentRequest(string name, string description, string amount, int userId, string emailAddress)
        {
            try
            {
                ConfigurePaymentMode();
                PaymentUrl = m_site + BuildQueryString(name, description, amount, userId, emailAddress);

                log.Debug("PayFast - Redirect url set to: " + PaymentUrl);
                return true;
            }
            catch (Exception e)
            {
                log.Error("PayFast - Error initiating payment request: " + e.Message);
                return false;
            }
        }

        //-------------------------------------------------------------------------------------------
        // PRIVATE METHODS
        //-------------------------------------------------------------------------------------------

        private bool ConfigurePaymentMode()
        {
            string paymentMode = ConfigurationManager.AppSettings["PayFast_PaymentMode"];

            if (paymentMode == "test")
            {
                m_site = "https://sandbox.payfast.co.za/eng/process?";
                m_merchant_id = "10003370";
                m_merchant_key = "x5b2dgv8ehui3";

                log.Debug("PayFast - Using sandbox payment mode.");
                return true;
            }
            else if (paymentMode == "live")
            {
                m_site = "https://www.payfast.co.za/eng/process?";
                m_merchant_id = ConfigurationManager.AppSettings["PayFast_MerchantID"];
                m_merchant_key = ConfigurationManager.AppSettings["PayFast_MerchantKey"];

                log.Debug("PayFast - Using live payment mode.");
                return true;
            }

            log.Error("PayFast - Unrecognised web.config PaymentMode: " + paymentMode);
            throw new InvalidOperationException("Unrecognised Payment Mode set in web.config.");
        }

        //-------------------------------------------------------------------------------------------

        private string BuildQueryString(string orderName, string orderDesc, string orderAmount, int userId, string emailAddress)
        {
            //order of variables are important. 
            //Please view "www.payfast.co.za/documentation/confirm-page" for more information.
            //there are more recommended and optional variables available to us on the site if we need.

            StringBuilder str = new StringBuilder();
            str.Append("merchant_id=" + UrlEncodeUppercase(m_merchant_id));
            str.Append("&merchant_key=" + UrlEncodeUppercase(m_merchant_key));
            str.Append("&return_url=" + UrlEncodeUppercase(ConfigurationManager.AppSettings["PayFast_ReturnURL"]));
            str.Append("&cancel_url=" + UrlEncodeUppercase(ConfigurationManager.AppSettings["PayFast_CancelURL"]));
            str.Append("&notify_url=" + UrlEncodeUppercase(ConfigurationManager.AppSettings["PayFast_NotifyURL"]));

            str.Append("&m_payment_id=" + UrlEncodeUppercase(userId.ToString()));
            str.Append("&amount=" + UrlEncodeUppercase(orderAmount));
            str.Append("&item_name=" + UrlEncodeUppercase(orderName));
            str.Append("&item_description=" + UrlEncodeUppercase(orderDesc));
            str.Append("&email_address=" + UrlEncodeUppercase(emailAddress));

            return str.ToString();
        }

        //-------------------------------------------------------------------------------------------
        /// <summary>
        /// Encodes the html url but also makes sure the escaped characters are uppercase.
        /// </summary>
        /// <param name="s">Raw Html Url</param>
        /// <returns>Url string with uppercased escape characters</returns>
        private string UrlEncodeUppercase(string s)
        {
            string lower = HttpUtility.UrlEncode(s);

            Regex reg = new Regex(@"%[a-f0-9]{2}");
            string upper = reg.Replace(lower, m => m.Value.ToUpperInvariant());

            return upper;
        }

        //-------------------------------------------------------------------------------------------
    }
}