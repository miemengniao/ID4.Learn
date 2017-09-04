using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Authentication;

namespace ID4.MvcClient.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        [Authorize]
        public IActionResult Auth()
        {
            return View();
        }

        [Authorize]
        public async Task<IActionResult> WebApi()
        {   
            HttpClient client = new HttpClient();

            var accessToken = await HttpContext.Authentication.GetTokenAsync("access_token");
            client.SetBearerToken(accessToken);

            var response = await client.GetAsync("http://localhost:5004/api/Values");
            if (!response.IsSuccessStatusCode)
            {
                ViewData.Add("value",response.StatusCode);
            }
            else
            {
                var content = response.Content.ReadAsStringAsync().Result;
                ViewData.Add("value", content);
            }
            return View();
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
