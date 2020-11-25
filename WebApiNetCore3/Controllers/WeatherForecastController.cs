using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Identity.Client;
using Microsoft.Identity.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;

namespace WebApiNetCore3.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;
        private readonly ITokenAcquisition _tokenAcquisition;
        private HttpClient _httpClient;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, ITokenAcquisition tokenAcquisition)
        {
            _logger = logger;
            _tokenAcquisition = tokenAcquisition;
            //_httpClient = new HttpClient();
        }

        [HttpGet]
        public async Task<IEnumerable<WeatherForecast>> Get()
        {
            ////HttpContext.VerifyUserHasAnyAcceptedScope(scopeRequiredByAPI);
            //string[] scopes = new[] { "user.read" };
            //try
            //{
            //    var autRes = await _tokenAcquisition.GetAuthenticationResultForUserAsync(scopes);
            //    string token = await _tokenAcquisition.GetAccessTokenForUserAsync(scopes);
            //    // call the downstream API with the bearer token in the Authorize header

            //    // make API call
            //    _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            //    var dataRequest = await _httpClient.GetAsync("https://graph.microsoft.com/beta/me");

            //    if (dataRequest.IsSuccessStatusCode)
            //    {
            //        var userData = System.Text.Json.JsonDocument.Parse(await dataRequest.Content.ReadAsStreamAsync());
            //        var userDisplayName = userData.RootElement.GetProperty("displayName").GetString();
            //    }
            //}
            //catch (MsalUiRequiredException ex)
            //{
            //    //await _tokenAcquisition.ReplyForbiddenWithWwwAuthenticateHeaderAsync(HttpContext, scopes, ex);
            //}
            //catch(Exception ex)
            //{
            //    Console.WriteLine($"{ex.Message}");
            //}


            var ident = HttpContext.User.Identity;
            string owner = (User?.FindFirst(ClaimTypes.NameIdentifier))?.Value;

            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            })
            .ToArray();
        }
    }
}
