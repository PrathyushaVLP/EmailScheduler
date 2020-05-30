using EmailScheduler.DTO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;

namespace EmailScheduler_Library
{
    public class EmailSender
    {
        public static Task<bool> DispatchMail(Email obj)
        {
            TaskCompletionSource<bool> tcs = new TaskCompletionSource<bool>();
            try
            {
                var client = GetDefaultClientSettings(new NetworkCredential(obj.From, obj.Password));
                var mail = new MailMessage
                {
                    From = new MailAddress(obj.From),
                    IsBodyHtml = true,
                    Priority = obj.Priority,
                    Subject = obj.Subject,
                    Body = obj.Body
                };
                // Create  the file attachment for this email message.
                foreach (string filename in obj.Attachments)
                {
                    if (File.Exists(filename))
                    {
                        //mail.Attachments.Add(new Attachment(filename, MediaTypeNames.Application.Octet));
                        Attachment data = new Attachment(filename, MediaTypeNames.Application.Octet);
                        //Attachment data = new Attachment(filename, "application/vnd.ms-excel");
                        //Add time stamp information for the file.

                        ContentDisposition disposition = data.ContentDisposition;
                        disposition.CreationDate = System.IO.File.GetCreationTime(filename);
                        disposition.ModificationDate = System.IO.File.GetLastWriteTime(filename);
                        disposition.ReadDate = System.IO.File.GetLastAccessTime(filename);
                        //Add the file attachment to this email message.
                        mail.Attachments.Add(data);
                    }
                    //using (Attachment data = new Attachment(filename, MediaTypeNames.Application.Octet))
                    //{
                    //    mail.Attachments.Add(data);
                    //}
                }


                foreach (var address in obj.To.Split(new[] { ";" }, StringSplitOptions.RemoveEmptyEntries))
                    mail.To.Add(address);
                foreach (var address in obj.CC.Split(new[] { ";" }, StringSplitOptions.RemoveEmptyEntries))
                    mail.CC.Add(address);
                client.Send(mail);
                tcs.SetResult(true);
                return tcs.Task;
            }
            catch (Exception ex)
            {
                //throw ex;
                //Logger logger = LogManager.GetLogger("databaseLogger");

                //logger.Error(ex, "Email Error " + obj.ReportName);
                //tcs.SetException(ex);
                return tcs.Task;
            }
        }

        /// <summary>
        /// Method to Create mail body if there is error in dispatch
        /// </summary>
        /// <param name="message">Exception Message</param>
        /// <param name="fileName">The file name which got failed to dispatch</param>
        /// <returns>The mail body</returns>
        public static string DispatchErrorMailBody(string message, string fileName)
        {
            StringBuilder stringBuilder = new StringBuilder();

            stringBuilder.Append("<html><body>");
            stringBuilder.Append(" <pre> Dear Team,</pre> ");
            stringBuilder.Append("<br />");
            stringBuilder.Append(" <pre>The following 837 batch <b>").Append(fileName).Append("</b> have not been Dispatched.Please Find the Error Details below</pre> ");
            stringBuilder.Append("<br />");
            stringBuilder.Append("<b>Error Message</b>:");
            stringBuilder.Append("<pre>").Append(message).Append("</pre>");
            stringBuilder.Append("<br />");
            stringBuilder.Append("<pre>This is system generated Email Please do not reply.</pre>");
            stringBuilder.Append("</body>");
            stringBuilder.Append("</html>");
            return stringBuilder.ToString();
        }

        /// <summary>
        /// Method to Create mail body if there is error in Generation
        /// </summary>
        /// <param name="message">Exception Message</param>
        /// <param name="fileName">The file name which got failed to dispatch</param>
        /// <returns>Mail Body</returns>
        public static string GenerationErrorMailBody(string message, int[] claimIDs)
        {
            int index = 0;
            StringBuilder stringBuilder = new StringBuilder();

            stringBuilder.Append("<html><body>");
            stringBuilder.Append(" <p> Dear Team,</p> ");
            stringBuilder.Append("<br />");
            stringBuilder.Append(" <p>The following 837 batch have failed to Generate.Please Find the Error Details below</p> ");
            stringBuilder.Append("<br />");
            stringBuilder.Append("<b>Error Message</b>:");
            stringBuilder.Append("<p>").Append(message).Append("</p>");
            stringBuilder.Append("<br />");

            stringBuilder.Append("<p>The Following are the Claim ID which have failed to generate</p>");
            foreach (var id in claimIDs)
            {
                index++;
                stringBuilder.Append("<p>" + index).Append("." + id).Append("</p>");
            }
            stringBuilder.Append("<pre>This is system generated Email Please do not reply.</pre>");
            stringBuilder.Append("</body>");
            stringBuilder.Append("</html>");
            return stringBuilder.ToString();
        }

        public static SmtpClient GetDefaultClientSettings(NetworkCredential credentials)
        {
            return new SmtpClient("smtp.gmail.com")
            {
                Port = 587,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                EnableSsl = false,               
                Credentials = credentials,
            };
        }

        /// <summary>
        /// Method to send the Email if there is any error in the Dispatching of the File.
        /// </summary>
        /// <param name="errormessage">Exception Message</param>
        /// <param name="fileName">File which got failed to be dispatched</param>
       
    }
}
