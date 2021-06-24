using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using GLogin2.Common;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using GLogin2.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace GLogin2.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Authenticate(string state) 
        {
            return RedirectPermanent(GoogleApiHelper.GetOauthUri(state));
        }
  
     
        public async Task<IActionResult> OauthCallback(string code,string error, string state)
        {
            try
            {
                if (!string.IsNullOrEmpty(code))
                {
                    using (var http = new HttpClient())
                    {
                        var request = new GoogleTokenModel()
                        {
                            client_id =  GoogleApiHelper.ClientId,
                            client_secret = GoogleApiHelper.ClientSecret,
                            code = code,
                            redirect_uri = GoogleApiHelper.RedirectUri
                        }; 
                        var response = await http.PostAsJsonAsync(GoogleApiHelper.TokenUri,request);
                        var responseStr = response.Content.ReadAsStringAsync().Result;
                        var resultModel = JsonConvert.DeserializeObject<GoogleTokenResponseModel>(responseStr);
                        ViewBag.Message = "Id_Token: " + resultModel.id_token;
                    }
                }
                if (!string.IsNullOrEmpty(error)) 
                {
                    ViewBag.Message = "Error: " + error;
                }
                if (!string.IsNullOrEmpty(state)) 
                {
                    ViewBag.RedirectUrl = state;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel {RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier});
        }
    }
}