using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DTOs;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;

namespace Roofstock01.Helpers
{
    public class PropertiesHelper
    {
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
    }
  
}
