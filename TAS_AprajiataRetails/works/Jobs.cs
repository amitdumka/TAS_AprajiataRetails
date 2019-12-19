using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using TAS_AprajiataRetails.Models.Helpers;

namespace TAS_AprajiataRetails.works
{
    public sealed class Jobs
    {
        public Jobs() { }

        public static void DailyJob(object state)
        {
            //Your dream goes here.
            Utils.JobOpeningClosingBalance();
            Utils.CashInHandCorrectionForMonth(DateTime.Today);
        }

        public static void HalfDayJob(object state)
        {
            //Your dream goes here.
            Utils.JobOpeningClosingBalance();
            Utils.CashInHandCorrectionForMonth(DateTime.Today);
        }

        public static void HourlyJob(object state)
        {
            //Your dream goes here.
            // Utils.CashInHandCorrectionForMonth(DateTime.Today);
        }
        public static void TwoHourlyJob(object state)
        {
            //Your dream goes here.
            Utils.JobOpeningClosingBalance();


        }
    }
    public sealed class Scheduler
    {
        public Scheduler() { }

        public void Scheduler_Start()
        {
            TimerCallback callbackDaily = new TimerCallback(Jobs.DailyJob);
            Timer dailyTimer = new Timer(callbackDaily, null, TimeSpan.Zero, TimeSpan.FromHours(24.0));

            TimerCallback callbackHalfDay = new TimerCallback(Jobs.DailyJob);
            Timer halfDayTimer = new Timer(callbackHalfDay, null, TimeSpan.Zero, TimeSpan.FromHours(14.0));

            TimerCallback callbackHourly = new TimerCallback(Jobs.HourlyJob);
            Timer hourlyTimer = new Timer(callbackHourly, null, TimeSpan.Zero, TimeSpan.FromHours(1.0));

            TimerCallback callbackTwoHourly = new TimerCallback(Jobs.HourlyJob);
            Timer twoHourlyTimer = new Timer(callbackTwoHourly, null, TimeSpan.Zero, TimeSpan.FromHours(2.0));
        }
    }
}