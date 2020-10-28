using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DTOs;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;
using System.Threading;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace Roofstock01.Helpers
{
    public class PropertiesHelper
    {
        private readonly RoofStockDBContext _context;

        public PropertiesHelper(RoofStockDBContext context)
        {
            _context = context;
        }
        public PropertyServiceResponse GetProperties()
        {
            var serviceResponse = new PropertyServiceResponse();
            try
            {
                // Get the token for use the Integration API             
                using (HttpClient apiRequest = new HttpClient())
                {
                    apiRequest.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    // No specific resource needs to be set
                    Task<HttpResponseMessage> response = apiRequest.GetAsync("https://samplerspubcontent.blob.core.windows.net/public/properties.json");

                    // If response is Ok, set the authorization transaction
                    if (response.Result.IsSuccessStatusCode)
                    {
                        serviceResponse = JsonConvert.DeserializeObject<PropertyServiceResponse>(response.Result.Content.ReadAsStringAsync().Result);
                    }
                }
            }
            catch (Exception exp)
            {
                //Logger
            }
            return serviceResponse;
        }

        public async Task<GenericResponse> SaveProperty(string address, string yearBuilt, string listPrice, string monthlyPrice, string grossYield)
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
            }
            else
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
            }
            else
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
            }
            else
            {
                grossYieldD = 0;
            }

            int.TryParse(yearBuilt, out yearBuiltI);
            try
            {
                using (var context = _context)
                {
                    var property = new DTOs.Properties
                    {
                        Address = address,
                        YearBuilt = yearBuiltI,
                        GrossYield = grossYieldD,
                        ListPrice = listPriceD,
                        MonthlyRent = monthlyPrinceD
                    };

                    context.Properties.Add(property);
                    await context.SaveChangesAsync();
                }

                genericResponse.Success = true;
                genericResponse.Message = "Ok";
                return genericResponse;
            }
            catch (Exception exp)
            {
                genericResponse.Success = false;
                genericResponse.Message = "Internal Error";
                return genericResponse;
            }
        }

        private GenericResponse Json(GenericResponse genericResponse)
        {
            throw new NotImplementedException();
        }
    }
  
}
