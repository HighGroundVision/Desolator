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
            RecurringJob.AddOrUpdate<CollectorService>(_ => _.Collect(), Cron.MinuteInterval(10));
            RecurringJob.AddOrUpdate<TransferService>(_ => _.Transfer(), Cron.Weekly);

            return Redirect("/hangfire/recurring");
        }

        
        public ActionResult ApplyMigrations()
        {
            var id = BackgroundJob.Enqueue<SeedService>(_ => _.MigrateDB());

            return Redirect("/hangfire/jobs/processing");
        }

        
        public ActionResult SeedHeroes()
        {
			var id = BackgroundJob.Enqueue<SeedService>(_ => _.SeedHeroes());

			return Redirect("/hangfire/jobs/processing");
        }

        
        public ActionResult SeedAbilities()
		{
			var id = BackgroundJob.Enqueue<SeedService>(_ => _.SeedAbilities());

			return Redirect("/hangfire/jobs/processing");
		}

        public ActionResult SeedTrends()
		{
			var id = BackgroundJob.Enqueue<SeedService>(_ => _.SeedTrends());

			return Redirect("/hangfire/jobs/processing");
		}
    }
}