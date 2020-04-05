﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Localization;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Reflection;
using Tanyo.Portfolio.Web.Models;
using Tanyo.Portfolio.Web.Models.Filters;
using Tanyo.Portfolio.Web.Models.Services;
using Tanyo.Portfolio.Web.Resources;

namespace Tanyo.Portfolio.Web.Areas.Tanyo.Controllers
{
    [TanyoLayoutActionFilter]
    public abstract class BaseController : Controller
    {
        public Layout Layout { get; set; }

        protected ILogger<BaseController> _logger;

        public string Area { get; set; }

        public BaseController(ILogger<BaseController> logger,
            NavLinksService navLinksService)
        {
            var type = typeof(SharedResource);
            var assemblyName = new AssemblyName(type.GetTypeInfo().Assembly.FullName);

            this.Area = "Tanyo";

            Layout = new Layout();
            Layout.Head.Title = "Tanyo Ivanov";
            Layout.Header.ImageUrl = "/img/profile/logo.png";
            Layout.Header.NavigationLinks = navLinksService.GetNavLinks().ToList();

            Layout.Footer.ImageUrl = "/img/profile/logo2.png";
            Layout.Footer.NavigationLinks = Layout.Header.NavigationLinks;
            Layout.Footer.SocialLinks = navLinksService.GetSocialLinks().ToList();
            Layout.Footer.CopyLink = navLinksService.GetCopyLinks().FirstOrDefault();

            Layout.Brands = navLinksService.GetBrands().ToList();

            _logger = logger;
        }

        [HttpPost]
        public IActionResult SetLanguage(string culture, string returnUrl)
        {
            Response.Cookies.Append(
                CookieRequestCultureProvider.DefaultCookieName,
                CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)),
                new CookieOptions { Expires = DateTimeOffset.UtcNow.AddYears(1) }
            );

            return LocalRedirect(returnUrl);
        }
    }
}