using System;
using System.Collections.Generic;
using System.Text;
using Quartz;
using Quartz.Impl;
//using Quartz.Impl.Matchers;

namespace EmailScheduler_Library
{
    public class EmailJobScheduler
    {
        public static void Start()
        {
            try
            {
                //IScheduler scheduler = StdSchedulerFactory.GetDefaultScheduler();
                // construct a scheduler factory
                ISchedulerFactory schedFact = new StdSchedulerFactory();

                // get a scheduler, start the schedular before triggers or anything else
                IScheduler sched = schedFact.GetScheduler().GetAwaiter().GetResult();
                sched.Start();
                // create job
                IJobDetail EmailJob = JobBuilder.Create<EmailJob>()
                                  .WithIdentity("EmailJob", "EmailJobGroup")
                                  .Build();
                // create trigger
                ITrigger EmailTrigger = TriggerBuilder.Create()
                    .WithIdentity("EmailJob", "EmailJobGroup")
                    .WithCronSchedule("0 0 0 1 / 1 * ? *")//ConfigurationManager.AppSettings["HPSABonusFormat"])
                    .Build();
                
                sched.ScheduleJob(EmailJob, EmailTrigger);


            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
