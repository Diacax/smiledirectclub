using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Linq;
using SpaceXApi.Models;

namespace SpaceXApi.Controllers
{
    [Route("api/[controller]")]
    public class ValuesController : Controller
    {
        static HttpClient client = new HttpClient();
        private readonly IConfiguration _configuration;

        public ValuesController(IConfiguration configuration) => _configuration = configuration;

        // GET api/values
        [HttpGet]
        public async Task<IEnumerable<LaunchpadModel>> GetAllLaunchPadData()
        {
            string launchpadData = null;
            HttpResponseMessage response = await client.GetAsync(_configuration["SiteConfig"]);
            if (response.IsSuccessStatusCode)
            {
                launchpadData = await response.Content.ReadAsStringAsync();
                var productJObject = JArray.Parse(launchpadData).Select(x => new LaunchpadModel
                {
                    FullName = x["full_name"].ToString(),
                    Id = x["id"].ToString(),
                    Status = x["status"].ToString()
                });
                return productJObject;
            }
            return new List<LaunchpadModel>();
        }

        // GET api/values/id
        [HttpGet("{id}")]
        public async Task<object> GetById(string id)
        {
            string launchpadData = null;
            HttpResponseMessage response = await client.GetAsync(_configuration["SiteConfig"] + "/" + id);
            if (response.IsSuccessStatusCode)
            {
                launchpadData = await response.Content.ReadAsStringAsync();
                JObject productJObject = JObject.Parse(launchpadData);
                
                LaunchpadModel launchpad = new LaunchpadModel
                {
                    FullName = productJObject["full_name"].ToString(),
                    Id = productJObject["id"].ToString(),
                    Status = productJObject["status"].ToString()
                };
                return launchpad;
            }
            return new List<LaunchpadModel>();
        }
        // GET api/values/status
        [HttpGet("bystatus/{status}")]
        public async Task<IEnumerable<LaunchpadModel>> GetAllLaunchPadByStatus(string status)
        {
            string launchpadData = null;
            HttpResponseMessage response = await client.GetAsync(_configuration["SiteConfig"]);
            if (response.IsSuccessStatusCode)
            {
                launchpadData = await response.Content.ReadAsStringAsync();
                var productJObject = JArray.Parse(launchpadData).Where(y => (string)y["status"] == status).Select(x => new LaunchpadModel
                {
                    FullName = x["full_name"].ToString(),
                    Id = x["id"].ToString(),
                    Status = x["status"].ToString()
                });
                return productJObject;
            }
            return new List<LaunchpadModel>();
        }
    }
}
