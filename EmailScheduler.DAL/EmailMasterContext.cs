using EmailScheduler.DTO;
using System;
using System.Data.Entity;
using System.Threading;

namespace EmailScheduler.DAL
{
    public class EmailMasterContext : DbContext
    {
        public EmailMasterContext() : base("EmailMaster")
        {
        }
            public DbSet<EmailAddresses> EmailAddresses { get; set; }
            
        
    }
}
