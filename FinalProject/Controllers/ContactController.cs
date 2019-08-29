using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using FinalProject.Models;

namespace FinalProject.Controllers
{
    public class ContactController : Controller
    {
        public ContactController() : base()
        {
            ViewData["ContactIsActive"] = "active";
        }

        // GET: Contact
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public string Index(ContactMesssage msg)
        {
            HttpContext.Response.AddHeader("Content-Type", "application/json; charset=utf-8");

            dynamic result = SendContactMessage(msg);
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            return serializer.Serialize(result);
        }

        private dynamic SendContactMessage(ContactMesssage msg)
        {
            // Command line argument must the the SMTP host.
            SmtpClient client = new SmtpClient
            {
                Port = 26,
                Host = "mail.jasonallen.dev",
                EnableSsl = false,
                Timeout = 10000,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential("csproject@jasonallen.dev", "KdF7[4?70k+T")
            };

            MailMessage mm = new MailMessage
            {
                From = new MailAddress("csproject@jasonallen.dev", msg.Name),
                Sender = new MailAddress("csproject@jasonallen.dev"),
                Subject = msg.Subject,
                SubjectEncoding = Encoding.UTF8,
                Body = msg.Message,
                BodyEncoding = Encoding.UTF8,
                IsBodyHtml = false,
                DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure
            };
            mm.To.Add("contact@jasonallen.dev");
            mm.ReplyToList.Add(new MailAddress(msg.Email, msg.Name));
            mm.Headers.Add("Disposition-Notification-To", "csproject@jasonallen.dev");

            try {
                client.Send(mm);
            } catch (Exception e) {
                return new {
                    success = false,
                    error = e.Message
                };
            } finally {
                mm.Dispose();
                client.Dispose();
            }

            return new {
                success = true,
                error = (string)null
            };
        }
    }
}
