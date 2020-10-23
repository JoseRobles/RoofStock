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

        public IActionResult Privacy()
        {
            return View();
        }

        [HttpGet]
        public IActionResult ListProperties()
        {
            var propertiesHelper = new PropertiesHelper();
            var propertyServiceResponse = propertiesHelper.GetProperties();


            return View(propertyServiceResponse);
        }

        [HttpPost]
        [Produces("application/json")]
        public async Task<IActionResult> SaveProperty(string address, string yearBuilt, string listPrice, string monthlyPrice, string grossYield)
        {
            
            var genericResponse = new GenericResponse();
            Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("en-US");
            Thread.CurrentThread.CurrentCulture.NumberFormat.NumberDecimalSeparator = ".";
            decimal listPriceD = 0;
            decimal monthlyPrinceD = 0;
            decimal grossYieldD = 0;
            int yearBuiltI = 0;

            var numberExpression = new Regex(@"\d+\.?\d*");
           
            if (listPrice != null)
            {
                var matchListPrice = numberExpression.Match(listPrice.Replace(',', '.'));
                if (matchListPrice.Success)
                {
                    decimal.TryParse(matchListPrice.Groups[0].Value, out listPriceD);
                }
            }else
            {
                listPriceD = 0;
            }

            if (monthlyPrice != null)
            {
                var matchMonthlyPrice = numberExpression.Match(monthlyPrice.Replace(',', '.'));
                if (matchMonthlyPrice.Success)
                {
                    decimal.TryParse(matchMonthlyPrice.Groups[0].Value, out monthlyPrinceD);
                }
            }else
            {
                monthlyPrinceD = 0;
            }

            if (grossYield != null)
            {
                var matchGrossYield = numberExpression.Match(grossYield.Replace(',', '.'));
                if (matchGrossYield.Success)
                {
                    decimal.TryParse(matchGrossYield.Groups[0].Value, out grossYieldD);
                }
            }else
            {
                grossYieldD = 0;
            }

            int.TryParse(yearBuilt, out yearBuiltI);

            try
            {
                await using (var context = _dbContext)
                {
                    var property = new DTOs.Properties { Address = address, YearBuilt = yearBuiltI, GrossYield = grossYieldD, ListPrice = listPriceD, MonthlyRent = monthlyPrinceD };
                    context.Properties.Add(property);
                    await context.SaveChangesAsync();
                }

                genericResponse.Success = true;
                genericResponse.Message = "Ok";

                
                return Json(genericResponse);

            }
            catch (Exception exp)
            {
                genericResponse.Success = false;
                genericResponse.Message = "Internal Error";

                var result = new JsonResult(genericResponse);
                return result;
            }
           

 
        }
 

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
