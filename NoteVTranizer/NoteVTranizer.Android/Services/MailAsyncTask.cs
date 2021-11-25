using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using MimeKit;
using MailKit.Net.Smtp;
namespace NoteVTranizer.Droid.Services
{
    class MailAsyncTask : AsyncTask
    {
        string username = "mail-id or username", password = "password", host = "smtp.gmail.com";
        int port = 25;
        MainActivity mainActivity;
        ProgressDialog progressDialog;
        public MailAsyncTask(MainActivity activity)
        {
            mainActivity = activity;
            progressDialog = new ProgressDialog(mainActivity);
            progressDialog.SetMessage("Sending...");
            progressDialog.SetCancelable(false);
        }
        protected override void OnPreExecute()
        {
            base.OnPreExecute();
            progressDialog.Show();
        }
        protected override Java.Lang.Object DoInBackground(params Java.Lang.Object[] @params)
        {
            try
            {
                var message = new MimeMessage();
                message.From.Add(new MailboxAddress("From", mainActivity.edtFrom.Text));
                message.To.Add(new MailboxAddress("To", mainActivity.edtTo.Text));
                message.Subject = mainActivity.edtSubject.Text;
                message.Body = new TextPart("plain")
                {
                    Text = mainActivity.edtMessage.Text
                };
                using (var client = new SmtpClient())
                {
                    // For demo-purposes, accept all SSL certificates (in case the server supports STARTTLS)  
                    client.ServerCertificateValidationCallback = (s, c, h, e) => true;
                    client.Connect(host, port, false);
                    // Note: only needed if the SMTP server requires authentication  
                    client.Authenticate(username, password);
                    client.Send(message);
                    client.Disconnect(true);
                }
                return "Successfully Sent";
            }
            catch (System.Exception ex)
            {
                return ex.Message;
            }
        }
        protected override void OnPostExecute(Java.Lang.Object result)
        {
            base.OnPostExecute(result);
            progressDialog.Dismiss();
            
            mainActivity.edtFrom.Text = null;
            mainActivity.edtTo.Text = null;
            mainActivity.edtSubject.Text = null;
            mainActivity.edtMessage.Text = null;
            Toast.MakeText(mainActivity, "Email Succesfully Sent...", ToastLength.Short).Show();
        }
    }
}