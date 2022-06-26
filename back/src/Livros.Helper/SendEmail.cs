// using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;


namespace Livros.Helper.EmailLog
{
    public class LogHelper
    {
        public class EmailLog : IDisposable
        {
            public string? Smtp { get; set; }
            public int Port { get; set; }
            public string? UserName { get; set; }
            public string? Password { get; set; }
            public string? Subject { get; set; }
            public string? From { get; set; }
            public string? To { get; set; }
            public string? Message { get; set; }            
            public Exception? Exception { get; set; }

            public SmtpClient SmtpClient { 
                get {
                    return new SmtpClient(Smtp)
                    {
                        Port = 587,
                        Credentials = new NetworkCredential(UserName, Password),
                        EnableSsl = true,
                    };
                }
            }

            public MailMessage? MailMessage
            {
                get
                {
                    var mail = new MailMessage(From ?? "user@gmail.com", To ?? "user@gmail.com");

                    mail.IsBodyHtml = true;
                    return mail;
                }
            }

            public void Dispose()
            {
                this.SmtpClient.Dispose();
                this.MailMessage?.Dispose();
            }
        }

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

        public static void SendEmail(EmailLog emailLog, int id)
        {
            using (emailLog)
            {              
                var email = emailLog.MailMessage ?? new MailMessage();
                email.Subject = "Recuperação de senha";
                var body = new StringBuilder();
                
                var br = "<br />";

                body.Append($"Você solicitou um e-mail de recuperação de senha as {DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss")}: {br + br}");

            
                body.Append($"<strong>Seu link para recuperação é</strong> <a href=\'http://localhost:4200/forget/{id}'>Clique aqui para redefinir</a>");

                body.Append($"{br}<strong>Caso não tenha sido você, nos avise!");

                email.Body = body.ToString();
                
                emailLog.SmtpClient.Send(email);
            }            
        }
    }
}
