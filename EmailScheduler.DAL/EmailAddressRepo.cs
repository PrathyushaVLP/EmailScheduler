using EmailScheduler.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EmailScheduler.DAL
{
    public class EmailAddressRepo : IEmailAddressRepo
    {
        EmailMasterContext db = new EmailMasterContext();

        public List<EmailAddresses> GetEmailAddresses()
        {
            //List<EmailAddresses> ea = db.EmailAddresses.ToList();
            return db.EmailAddresses.ToList();
        }
    }
}
