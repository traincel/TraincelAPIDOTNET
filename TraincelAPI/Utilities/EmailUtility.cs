using Microsoft.Extensions.Configuration;
using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.Configuration.Internal;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;
using TraincelAPI.Models.VM;

namespace TraincelAPI.Utilities
{
    public static class EmailUtility
    {

        public static async Task<Response> SendEmail(CommonEmailVM commonEmail)
        {
            try
            {
                var apiKey = commonEmail.ApiKey;
                if (apiKey == null)
                {
                    apiKey = Environment.GetEnvironmentVariable("SEND_GRID_KEY");
                }
                var client = new SendGridClient(apiKey);
                var from = new EmailAddress("muzamil.kichloo@outlook.com", "Traincel");
                PrepareEmail(commonEmail, out string subject, out EmailAddress to, out string plainTextContent, out string htmlContent);
                var mail = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
                return await client.SendEmailAsync(mail);

            }
            catch (SmtpFailedRecipientException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private static void PrepareEmail(CommonEmailVM commonEmail, out string subject, out EmailAddress to, out string plainTextContent, out string htmlContent)
        {
            subject = "";
            to = new EmailAddress("muzamil.kichloo@outlook.com", "Traincel");
            //to = new EmailAddress("skharpor17@gmail.com", "Traincel");
            plainTextContent = "";
            htmlContent = "";

            switch (commonEmail.NotificationType.ToLower())
            {
                case "registrationsuccessful":
                    {
                        subject = "Registration Successful";
                        to = new EmailAddress(commonEmail.EmailAddress, commonEmail.Name);
                        htmlContent = "<div><p> Hi" + commonEmail.Name + ",</p><p> Congratulation! you have successfully registered with Traincel.</p></div> ";
                    }
                    break;
                case "changepassword":
                    {
                        subject = "Change Password";
                        to = new EmailAddress(commonEmail.EmailAddress, commonEmail.Name);
                        htmlContent = "<div><p> Hi" + commonEmail.Name + ",</p><p>Your code for changing the password is </p></div> ";
                    }
                    break;
                case "forgotpassword":
                    {
                        subject = "Forgot Password";
                        to = new EmailAddress(commonEmail.EmailAddress, commonEmail.Name);
                        htmlContent = "<div><p> Hi " + commonEmail.Name + ",</p><p>Your code for changing the password is " + commonEmail.ChangePasswordCode + "</p></div> ";
                    }
                    break;
                case "contactus":
                    {
                        subject = "Contact Us";
                        htmlContent = "<div><p> Hi Muzamil, </p><p>Please find below the Contact Details</p>" +
                           "<p><span>Name:</span><span>" + commonEmail.Name + "</span></p>" +
                           "<p><span>Email Address:</span><span>" + commonEmail.EmailAddress + "</span></p>" +
                           "<p><span>Contact Number:</span><span>" + commonEmail.ContactNumber + "</span></p>" +
                           "<p><span>Subject:</span><span>" + commonEmail.Subject + "</span></p>" +
                           "<p><span>Message:</span><span>" + commonEmail.Message + "</span></p>" +
                           "</ div> ";
                    }
                    break;
                case "feedback":
                    {
                        subject = "Feedback";
                        htmlContent = "<div><p> Hi Muzamil, </p><p>Please find below the feedback</p>" +
                           "<p><span>Name:</span><span>" + commonEmail.Name + "</span></p>" +
                           "<p><span>Email Address:</span><span>" + commonEmail.EmailAddress + "</span></p>" +
                           "<p><span>Contact Number:</span><span>" + commonEmail.ContactNumber + "</span></p>" +
                           "<p><span>Subject:</span><span>" + commonEmail.Subject + "</span></p>" +
                           "<p><span>Message:</span><span>" + commonEmail.Message + "</span></p>" +
                           "</ div> ";
                    }
                    break;
                case "speakingopportunity":
                    {
                        subject = commonEmail.NotificationType;
                        htmlContent = "<div><p> Hi Muzamil, </p><p>Please find below the details of an interested speaker/p>" +
                           "<p><span>Name:</p><p>" + commonEmail.Name + "</p>" +
                           "<p><span>Email Address:</span><span>" + commonEmail.EmailAddress + "</span></p>" +
                           "<p><span>Contact Number:</span><span>" + commonEmail.ContactNumber + "</span></p>" +
                           "<p><span>Industry:</span><span>" + commonEmail.Industry + "</span></p>" +
                           "<p><span>Website:</span><span>" + commonEmail.Website + "</span></p>" +
                           "<p><span>Bio:</span><span>" + commonEmail.Bio + "</span></p>" +
                           "</ div> ";
                    }
                    break;
                case "customizedtraining":
                    {
                        subject = "Customized Training";
                        htmlContent = "<div><p> Hi Muzamil, </p><p>Please find below the details for Customized Training.</p>" +
                           "<p><span>Name:</p><p>" + commonEmail.Name + "</p>" +
                           "<p><span>Email Address:</span><span>" + commonEmail.EmailAddress + "</span></p>" +
                           "<p><span>Contact Number:</span><span>" + commonEmail.ContactNumber + "</span></p>" +
                           "<p><span>Industry:</span><span>" + commonEmail.Industry + "</span></p>" +
                           "<p><span>Website:</span><span>" + commonEmail.Website + "</span></p>" +
                           "<p><span>Bio:</span><span>" + commonEmail.Bio + "</span></p>" +
                           "</ div> ";
                    }
                    break;
                case "inHouseTraining":
                    {
                        subject = "In House Training";
                        htmlContent = "<div><p> Hi Muzamil, </p><p>Please find below the details for In House Training</p>" +
                           "<p><span>Name:</p><p>" + commonEmail.Name + "</p>" +
                           "<p><span>Email Address:</span><span>" + commonEmail.EmailAddress + "</span></p>" +
                           "<p><span>Contact Number:</span><span>" + commonEmail.ContactNumber + "</span></p>" +
                           "<p><span>Industry:</span><span>" + commonEmail.Industry + "</span></p>" +
                           "<p><span>Website:</span><span>" + commonEmail.Website + "</span></p>" +
                           "<p><span>Bio:</span><span>" + commonEmail.Bio + "</span></p>" +
                           "</ div> ";
                    }
                    break;
                case "emailverification":
                    {
                        subject = "Email Verification";
                        to = new EmailAddress(commonEmail.EmailAddress, commonEmail.Name);
                        htmlContent = "<div><p> Hi" + commonEmail.Name + ",</p><p>Your code for verifying the password is </p></div> ";
                    }
                    break;
                case "newsLetter":
                    {
                        subject = commonEmail.NotificationType;
                        htmlContent = "<div><p> Hi Muzamil, </p><p>Please find below the email address for newsletter subscription</p>" +
                           "<p><span>Email Address:</span><span>" + commonEmail.EmailAddress + "</span></p>" +
                           "</ div> ";
                    }
                    break;
            };
        }
    }
}
