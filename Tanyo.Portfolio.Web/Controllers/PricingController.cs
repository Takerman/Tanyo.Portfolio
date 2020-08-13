﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using Tanyo.Portfolio.Web.Models;
using Tanyo.Portfolio.Web.Models.Partials;
using Tanyo.Portfolio.Web.Models.Services;

namespace Tanyo.Portfolio.Web.Areas.Tanyo.Controllers
{
    public class PricingController : BaseController
    {
        private readonly SkillsService _skillsService;
        private readonly PricingService _pricingService;

        public PricingController(ILogger<BaseController> logger,
            NavLinksService navLinksService,
            SkillsService skillsService,
            PricingService pricingService,
            IStringLocalizerFactory factory) : base(logger, navLinksService, factory)
        {
            _skillsService = skillsService;
            _pricingService = pricingService;
        }

        public IActionResult GetTable(int employmentTypeId, int locationId)
        {
            var prices = _pricingService.GetPrices().Where(x => x.Type == employmentTypeId && x.Location == locationId).ToList();

            var model = new PricingTableModel(employmentTypeId)
            {
                Prices = prices
            };

            return PartialView("PricingTable", model);
        }

        public IActionResult Index()
        {
            Layout.Head.Title = _sharedLocalizer["Pricing | " + Layout.Head.Title + " | .NET Developer"];
            Layout.Banner.Title = _sharedLocalizer["Pricing"];
            Layout.Banner.NavLinks = new List<NavLink>()
            {
                new NavLink(){ Action = "Index", Controller = "Home", Label = _sharedLocalizer["Home"] },
                new NavLink(){ Action = "Index", Controller = "Pricing", Label = _sharedLocalizer["Pricing"] },
            };

            var skills = _skillsService.GetSkills().ToList();
            var prices = _pricingService.GetPrices().ToList();

            var model = new PricingModel()
            {
                Skills = skills,
                Prices = prices
            };

            return View(model);
        }
    }
}