using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Net;
using System.Net.Mail;
using System.Configuration;

namespace TAS_AprajiataRetails.Ops.TAS
{
    public class EMail
    {
        public string FromAddress { get; set; }
        public string FromName { get; set; }
        public string[] ToAddress { get; set; }
        public string[] ToNames { get; set; }
        public string BCCAddress { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }

    }
    public class EmailConfig
    {
        public const string Host = "smtp.gmail.com";
        public const int Port = 587;
        public const bool SSL = true;
        public const bool DefaultCredentials = false;
    }
    public class EmailHelper
    {
        public void SendTestEmail() { }
        public void SendErrorEmail() { }
        public bool SendStatusEmail(string message)
        {
            EMail eMail = new EMail
            {
                Subject = "AprajitaRetails: Status Message",
                Body = message,
                FromAddress = "",
                FromName = "AprajitaRetails",
                ToNames = new string[] { "Amit Kumar", "Alok Kumar" },
                ToAddress = new string[] { "amitnarayansah@gmail.com", "thearvindstoredumka@gmail.com" },
                BCCAddress = "aprajitaretailsdumka@gmail.com"

            };
            return SendEmail(eMail, "PASSWORD");


        }

        private bool SendEmail(EMail eMail, string fromPassword)
        {
            try
            {
                string senderName = ConfigurationManager.AppSettings["From"].ToString();

                //string mailServer = ConfigurationManager.AppSettings["SMTPServer"].ToString(); ;

                string senderEmailId = ConfigurationManager.AppSettings["SMTPUserName"].ToString();
                string password = ConfigurationManager.AppSettings["SMTPPasssword"].ToString();


                var fromAddress = new MailAddress(eMail.FromAddress, eMail.FromName);

                var toAddress = new MailAddress(eMail.ToAddress[0], eMail.ToNames[0]);


                var smtp = new SmtpClient
                {
                    Host = EmailConfig.Host,
                    Port = EmailConfig.Port,
                    EnableSsl = EmailConfig.SSL,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = EmailConfig.DefaultCredentials,
                    Credentials = new NetworkCredential(fromAddress.Address, fromPassword)
                };



                using (var message = new MailMessage(fromAddress, toAddress)
                {
                    Subject = eMail.Subject,
                    Body = eMail.Body
                })
                {
                    // create attachment and set media Type
                    //      see http://msdn.microsoft.com/de-de/library/system.net.mime.mediatypenames.application.aspx
                    /*Attachment data = new Attachment(
                                             "PATH_TO_YOUR_FILE",
                                             MediaTypeNames.Application.Octet);
                                        // your path may look like Server.MapPath("~/file.ABC")
                                        message.Attachments.Add(data);*/
                    smtp.Send(message);
                    return true;
                }



            }
            catch (Exception)
            {

                //TODO: Handel Expetion
                return false;
            }



        }
    }
}