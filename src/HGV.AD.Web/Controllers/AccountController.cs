using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using HGV.AD.Web.Models;
using HGV.AD.Web.Services;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System.Net.Http;
using Newtonsoft.Json.Linq;
using Microsoft.Extensions.Options;
using HGV.AD.Web.Configuration;

namespace HGV.AD.Web.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ILogger _logger;

        private readonly IOptions<Site> _siteSettings;

        public AccountController(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            IOptions<Site> siteSettings,
            ILoggerFactory loggerFactory)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _siteSettings = siteSettings;
            _logger = loggerFactory.CreateLogger<AccountController>();
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Login(string returnUrl = null)
        {
            return View();
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult AccessDenied(string returnUrl = null)
        {
            return RedirectToAction("HandleStatusCode", "Main", new { id = 403 });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> LogOff()
        {
            await _signInManager.SignOutAsync();

            _logger.LogInformation(3, "User logged out.");

            return RedirectToAction(nameof(MainController.Index), "Main");
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public IActionResult ExternalLogin(string provider, string returnUrl = null)
        {
            // Request a redirect to the external login provider.
            var redirectUrl = Url.Action("ExternalLoginCallback", "Account", new { ReturnUrl = returnUrl });

            var properties = _signInManager.ConfigureExternalAuthenticationProperties(provider, redirectUrl);

            return Challenge(properties, provider);
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> ExternalLoginCallback(string returnUrl = null, string remoteError = null)
        {
            if (remoteError != null)
                throw new ApplicationException($"Error from external provider: {remoteError}");

            var info = await _signInManager.GetExternalLoginInfoAsync();
            if (info == null)
                throw new ApplicationException($"Error from external provider: No infomation found.");

            // Sign in the user with this external login provider if the user already has a login.
            var loginResult = await _signInManager.ExternalLoginSignInAsync(info.LoginProvider, info.ProviderKey, isPersistent: false);
            if (loginResult.Succeeded)
            {
                _logger.LogInformation(2, "User logged in with {Name} provider.", info.LoginProvider);

                return RedirectToLocal(returnUrl);
            }

            if (loginResult.IsLockedOut)
                return View("Lockout");

            if (loginResult.IsNotAllowed)
                return View("Lockout");

            var user = await CreateUserFromExternalProvider(info);

            var result = await _userManager.CreateAsync(user);
            if (!result.Succeeded)
                throw new ApplicationException($"Error from external provider: {remoteError}");

            result = await _userManager.AddLoginAsync(user, info);
            if (!result.Succeeded)
                throw new ApplicationException($"Error from external provider: Failed to login");

            await _signInManager.SignInAsync(user, isPersistent: false);

            _logger.LogInformation(1, "User created an account using {Name} provider.", info.LoginProvider);

            return RedirectToLocal(returnUrl);
        }

        private async Task<ApplicationUser> CreateUserFromExternalProvider(ExternalLoginInfo info)
        {
            var steam_id = info.ProviderKey.Split('/').LastOrDefault();
            
            var url = string.Format("http://api.steampowered.com/ISteamUser/GetPlayerSummaries/v0002/?key={0}&steamids={1}", _siteSettings.Value.DotaApiKey, steam_id);
            var client = new HttpClient();
            var json = await client.GetStringAsync(url);
            var data = JObject.Parse(json);
            var profile = data["response"]["players"][0];

            var account_id = (long.Parse(steam_id.Substring(3)) - 61197960265728).ToString();
            var user = new ApplicationUser() { UserName = account_id };

            var collection = new Dictionary<string, string>()
            {
                { "personaname", "steam:persona" },
                { "profileurl", "steam:profile" },
                { "avatar", "steam:avatar:small" },
                { "avatarmedium", "steam:avatar:medium" },
                { "avatarfull", "steam:avatar:large" },
                { "realname", "steam:name" },
                { "loccountrycode", "steam:country" },
                { "locstatecode", "steam:state" },
            };
            
            foreach (var item in collection)
            {
                var property = (string)profile[item.Key];
                if (!string.IsNullOrWhiteSpace(property))
                    user.Claims.Add(new IdentityUserClaim<string>() { ClaimType = item.Value, ClaimValue = property });
            }

            user.Claims.Add(new IdentityUserClaim<string>() { ClaimType = "steam:dota:id", ClaimValue = account_id });
            user.Claims.Add(new IdentityUserClaim<string>() { ClaimType = "steam:steam:id", ClaimValue = steam_id });

            return user;
        }

        private IActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction(nameof(MainController.Index), "Main");
            }
        }

    }
}
