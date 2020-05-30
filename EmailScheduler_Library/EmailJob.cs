using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Quartz;

namespace EmailScheduler_Library
{
    public class EmailJob : IJob
    {
        public async Task Execute(IJobExecutionContext context)
        {
            
                try
                {
                    EmailGenerator emailGenerator = new EmailGenerator();
                    emailGenerator.StartEmailDelivery();
                await Console.Out.WriteLineAsync("Hello Job is done.");
            }
                catch (Exception ex)
                {
                    throw ex;
                }
            
        }
    }
}
