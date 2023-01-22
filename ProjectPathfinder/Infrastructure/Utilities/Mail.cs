using System.IO;
using System.Net.Mail;

namespace ProjectPathfinder.Infrastructure.Utilities
{
    public static class Mail
    {
        //-------------------------------------------------------------------------------------------

        public static string GetLinkButtonMessageBody(string template, string link)
        {
            string body = string.Empty;
            using (StreamReader reader = new StreamReader(template))
            {
                body = reader.ReadToEnd();
            }
            
            return body.Replace("{link}", link);
        }
        
        //-------------------------------------------------------------------------------------------

        public static MailMessage SendSimpleMailMessage(string toAddress, string subject, string body)
        {
            MailMessage message = new MailMessage();
            message.To.Add(new MailAddress(toAddress));
            message.Subject = subject;
            message.Body = body;
            message.IsBodyHtml = true;

            return message;
        }

        //-------------------------------------------------------------------------------------------


        public static MailMessage SendToAndBccMailMessage(string toAddress, string bccAddress, string subject, string body)
        {
            MailMessage message = new MailMessage();
            message.To.Add(new MailAddress(toAddress));
            message.Bcc.Add(new MailAddress(bccAddress));
            message.Subject = subject;
            message.Body = body;
            message.IsBodyHtml = true;

            return message;
        }

        //-------------------------------------------------------------------------------------------

        public static MailMessage SendMultiMailMessage(MailAddressCollection toAddresses, MailAddressCollection bccAddresses, string subject, string body)
        {
            MailMessage message = new MailMessage();

            foreach(MailAddress to in toAddresses)
            {
                message.To.Add(to);
            }
            foreach (MailAddress bcc in bccAddresses)
            {
                message.Bcc.Add(bcc);
            }

            message.Subject = subject;
            message.Body = body;
            message.IsBodyHtml = true;

            return message;
        }

        //-------------------------------------------------------------------------------------------
    }
}