using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using HGV.AD.Web.Services;
using Hangfire;

namespace HGV.AD.Web.Controllers
{
    public class AdminController : Controller
    {
        public ActionResult RegisterRecurringServices()
        {
#if DEBUG
            RecurringJob.AddOrUpdate<CollectorService>(_ => _.Collect(), Cron.MinuteInterval(1));
            RecurringJob.AddOrUpdate<TransferService>(_ => _.Transfer(), Cron.Daily);
#else
            RecurringJob.AddOrUpdate<CollectorService>(_ => _.Collect(), Cron.MinuteInterval(5));
            RecurringJob.AddOrUpdate<TransferService>(_ => _.Transfer(), Cron.Weekly);
#endif
            return Redirect("/hangfire/recurring");
        }

        public ActionResult SeedHeroes()
        {
			var id = BackgroundJob.Enqueue<SeedService>(_ => _.SeedHeroes());

			return Redirect("/hangfire/");
        }

		public ActionResult SeedAbilities()
		{
			var id = BackgroundJob.Enqueue<SeedService>(_ => _.SeedAbilities());

			return Redirect("/hangfire/");
		}

		public ActionResult SeedTrends()
		{
			var id = BackgroundJob.Enqueue<SeedService>(_ => _.SeedTrends());

			return Redirect("/hangfire/");
		}
    }
}