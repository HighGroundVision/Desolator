using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using HGV.AD.Web.Services;
using Hangfire;
using Microsoft.AspNetCore.Authorization;

namespace HGV.AD.Web.Controllers
{
    [Authorize(Policy = "Founder")]
    public class AdminController : Controller
    {
        public ActionResult ApplyMigrations()
        {
            var id = BackgroundJob.Enqueue<SeedService>(_ => _.MigrateDB());

            return Redirect("/hangfire/jobs/processing");
        }

        public ActionResult RegisterRecurringServices()
        {
            RecurringJob.AddOrUpdate<CollectorService>("75A37A85-0F57-460F-9837-F8F295408F4D", _ => _.CalucateBatch(), Cron.MinuteInterval(5));
            RecurringJob.AddOrUpdate<TransferService>("C534FFB9-DED0-4BCF-A7CF-5E67B781DA1B", _ => _.Transfer(), Cron.Monthly());

            return Redirect("/hangfire/recurring");
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