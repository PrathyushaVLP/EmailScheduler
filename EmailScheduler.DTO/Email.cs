using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Net.Mail;
using System.Text;

namespace EmailScheduler.DTO
{
    public class Email
    {
        public Email()
        {
            Attachments = new List<string>();
        }
        public int EmailId { get; set; }
        public string From { get; set; }

        public string Password { get; set; }

        public string To { get; set; }
        public string CC { get; set; }
        public MailPriority Priority { get; set; }

        public string Subject { get; set; }

        public string Body { get; set; }
        [NotMapped]

        public List<string> Attachments { get; set; }
    }
}
