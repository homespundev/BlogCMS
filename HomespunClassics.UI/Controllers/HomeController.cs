using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using HomespunClassics.UI.Models;
using HomespunClassics.DATA;

namespace HomespunClassics.UI.Controllers
{
    public class HomeController : Controller
    {
        private HomespunClassicsDbEntities db = new HomespunClassicsDbEntities();
        public ActionResult Index()
        {
            var posts = db.Posts;
            ViewBag.MostRecentPost = (from p in posts where p.DateCreated <= DateTime.Now && p.Published orderby p.DateCreated descending select p.PostId).First();
            return View();
        }

        public ActionResult About()
        {
            return View();
        }

        public ActionResult Contact()
        {
            return View();
        }
        //[.NET Attribute] provides metadata (data about data)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Contact(ContactViewModel contact)
        {
            //Create the body for the email
            //This is just the content that is going to be added to the body
            //of the email that we send. 
            //We MUST add the Email field as content because we can't use it
            //as the from address so this is the ONLY place that it will be listed
            string body = string.Format("Name: {0}<br/>Email: {1}<br/>Subject: {2}<br/>Message: {3}",
                contact.Name,
                contact.Email,
                contact.Subject,
                contact.Message);

            //Create and Configure the Mail Message
            //We are going to create and address the envelope
            MailMessage msg = new MailMessage(
                "no-reply@homespundev.com"//From address (this MUST be an email
                                          //on your hosting account)
                , "mfrey2011@gmail.com",//To (what email want sent to
                contact.Subject,//email subject
                body);
            //Configure the Mail Message object
            msg.IsBodyHtml = true; //default is false, if there is html in the
            //body this must be set to true or the html tags will be shown in the message
            msg.CC.Add("another@email.com");//adds cc email address
            msg.Bcc.Add("blindcopy@email.com");//adds bcc email address
            msg.Priority = MailPriority.High;//an enumeration with values of high, normal, low

            //create and configure the smtp client
            //build the post office and send the email
            SmtpClient client = new SmtpClient("mail.homespundev.com");//this the mail server that 
            //the email will be sent throught
            client.Credentials = new NetworkCredential("no-reply@homespundev.com", "P@ssw0rd");//we must
            //add our email credentials so that the mail server knows that we are authorized
            //to send mail through the server

            //send the message
            //try catch method for handling exceptions
            //an exception is anything that interrupts the normal execution of an assembly (Project)
            //try - used to encapsulate 'potentially dangerous code'
            using (client)//using will destroy the client object after it is done
                          //which frees up memory
            {
                try//test to see if you can send, if so do it
                {
                    client.Send(msg);//use SmtpClient send() to actually send MailMessage
                }
                catch//if the test fails, show the error message
                {
                    ViewBag.ErrorMessage = "There was an error sending your email. Please try again.";
                    return View();
                }
            }

            //Send the user to the ContactConfirmation view
            //and pass through the object with it if the email
            //successfully sent

            return View("ContactConfirmation", contact);
        }
    }
}