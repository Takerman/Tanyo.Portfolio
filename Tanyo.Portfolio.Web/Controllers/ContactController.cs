﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using Tanyo.Portfolio.Web.Models;
using Tanyo.Portfolio.Web.Models.Services;

namespace Tanyo.Portfolio.Web.Areas.Tanyo.Controllers
{
    public class ContactController : BaseController
    {
        public ContactController(ILogger<BaseController> logger,
            NavLinksService navLinksService) : base(logger, navLinksService)
        {
        }

        public IActionResult Index()
        {
            Layout.Head.Title = "Contact | " + Layout.Head.Title + " | .NET Developer";
            Layout.Banner.Title = "Contact";
            Layout.Banner.NavLinks = new List<NavLink>()
            {
                new NavLink(){ Action = "Index", Controller = "Home", Label = "Home" },
                new NavLink(){ Action = "Index", Controller = "Contact", Label = "Contact" },
            };
            return View();
        }
    }
}