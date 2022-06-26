// using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;


namespace TudoFarmaRep.CrossCutting.Helper
{
    public class LogHelper
    {
        // public class EmailLog : IDisposable
        // {
        //     public string? Smtp { get; set; }
        //     public string? UserName { get; set; }
        //     public string? Password { get; set; }
        //     public string? Subject { get; set; }
        //     public string? From { get; set; }
        //     public string[]? To { get; set; }
        //     public string? Message { get; set; }            
        //     public Exception? Exception { get; set; }

        //     public SmtpClient SmtpClient { 
        //         get {
        //             return new SmtpClient(Smtp)
        //             {
        //                 Credentials = new NetworkCredential(UserName, Password)
        //             };
        //         }
        //     }

        //     public MailMessage MailMessage
        //     {
        //         get
        //         {
        //             var mail = new MailMessage(From ?? string.Empty, To?.FirstOrDefault() ?? string.Empty);
                    
        //             if(To?.Length > 1)                    
        //                 foreach (var to in To.Skip(1))                        
        //                     mail.CC.Add(to);

        //             mail.IsBodyHtml = true;
        //             return mail;
        //         }
        //     }

        //     public void Dispose()
        //     {
        //         this.SmtpClient.Dispose();
        //         this.MailMessage.Dispose();
        //     }
        // }

        // public static void RegisterLog(string action, string path)
        // {             

        //     if (string.IsNullOrEmpty(action.Replace("\n","")))
        //         return;
                       
        //     try
        //     {                
        //         var file = $"{DateTime.Now.ToShortDateString().Replace("/", "_")}_Log.txt";                
        //         var text = $"[{ DateTime.Now}]\t{action}\n\n";
        //         FileHelper.RecordFile(path, text, file);                
        //     }
        //     catch (Exception ex)
        //     {
        //         throw new Exception($"Erro: {ex.Message}");
        //     }       
        // }

        // public static void SendEmail(EmailLog emailLog)
        // {
        //     using (emailLog)
        //     {                
        //         var email = emailLog.MailMessage;
        //         var body = new StringBuilder();
                
        //         if(emailLog.Exception != null)
        //         {
        //             var br = "<br />";

        //             body.Append($"Erro {DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss")}: {br + br}");

        //             if(!string.IsNullOrEmpty(emailLog.Message))
        //                 body.Append($"<strong>Information:</strong> {emailLog.Message} {br + br}");

        //             body.Append($"<strong>Message:</strong> {emailLog.Exception.Message + br + br}<strong>StackTrace:</strong> {emailLog.Exception.StackTrace + br + br}<strong>Source:</strong> {emailLog.Exception.Source + br + br}");

        //             if (emailLog.Exception.Data?.Keys?.Count > 0)
        //             {
        //                 foreach (var key in emailLog.Exception.Data.Keys)
        //                 {
        //                     if (emailLog.Exception.Data[key] == null)
        //                         continue;
        //                     if (emailLog.Exception.Data[key].ToString().ToLower().StartsWith("http") || string.IsNullOrEmpty(emailLog.Exception.Data[key]?.ToString()))
        //                         continue;
        //                     body.Append($"<strong>{key}:</strong> {emailLog.Exception.Data[key]?.ToString() + br + br}");
        //                 }
        //             }
        //         }
        //         else
        //         {
        //             body.Append($"Erro desconhecido {DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss")}");
        //         }

        //         email.Body = body.ToString();
        //         emailLog.SmtpClient.Send(email);
        //     }            
        // }
    }
}
