using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Roofstock01.Models;
using Roofstock01.Helpers;
using DTOs;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System.Threading;
using System.Globalization;
using System.Text.RegularExpressions;

namespace Roofstock01.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private RoofStockDBContext _dbContext = null;

        public HomeController(ILogger<HomeController> logger, RoofStockDBContext context)
        {
            _logger = logger;
            this._dbContext = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult ListProperties()
        {
            var propertiesHelper = new PropertiesHelper(_dbContext);
            var propertyServiceResponse = propertiesHelper.GetProperties();
            return View(propertyServiceResponse);
        }

        [HttpPost]
        [Produces("application/json")]
        public async Task<IActionResult> SaveProperty(string address, string yearBuilt, string listPrice, string monthlyPrice, string grossYield)
        {
            var propertiesHelper = new PropertiesHelper(_dbContext);
            var genericResponse = await propertiesHelper.SaveProperty(address, yearBuilt, listPrice, monthlyPrice, grossYield);

            return Json(genericResponse);
        }
 
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
