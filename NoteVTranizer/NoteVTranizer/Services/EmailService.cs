using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace NoteVTranizer.Services
{
    public class EmailService
    {
        public bool EmailMessage(string to, string subject, string message)
        {
            bool bRes = false;

            try
            {
                if (!String.IsNullOrEmpty(to))
                {
                    MailMessage mail = new MailMessage();
                    SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");
                    //ApplicationAdmin@ils.tlv.com
                    mail.From = new MailAddress("trandvinh@gmail.com");
                    //mail.From = new MailAddress("ApplicationAdmin@ils.tlv.com");
                    mail.To.Add(to);
                    mail.Subject = subject;
                    mail.Body = message;
                    mail.Priority = MailPriority.High;
                    mail.SubjectEncoding = mail.BodyEncoding = Encoding.UTF8;

                    SmtpServer.Port = 587;
                    //SmtpServer.Host = "smtp.gmail.com";
                    SmtpServer.EnableSsl = true;
                    //SmtpServer.UseDefaultCredentials = false; Tas425@2901
                    //SmtpServer.Credentials = new System.Net.NetworkCredential("ApplicationAdmin@ils.tlv.com", "admin123");
                    SmtpServer.Credentials = new System.Net.NetworkCredential("trandvinh@gmail.com", "Jamm132000");
                    //SmtpServer.SendAsync("trandvinh@gmail.com", message, subject, message, "");
                    //ServicePointManager.ServerCertificateValidationCallback = delegate (object sender, X509Certificate certificate, X509Chain chain, 
                    //     System.Net.Security.SslPolicyErrors sslPolicyErrors) {
                    //    return true;
                    //};
                    SmtpServer.Send(mail);
                }
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
                throw;
            }
           

            return bRes;
        }
        //private void EmailSendCompleted(object sender, IAsyncResult e)
        //{

        //}
    }
}
