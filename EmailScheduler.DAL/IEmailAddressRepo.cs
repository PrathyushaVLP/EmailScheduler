using EmailScheduler.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace EmailScheduler.DAL
{
    public interface IEmailAddressRepo
    {
         List<EmailAddresses> GetEmailAddresses();
    }
}
