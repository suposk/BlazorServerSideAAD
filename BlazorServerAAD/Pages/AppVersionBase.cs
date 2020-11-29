using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace BlazorServerAAD.Pages
{
    public class AppVersionBase : ComponentBase
    {
        [Inject]
        public IConfiguration Configuration { get; set; }

        [Inject]
        IHttpClientFactory HttpClientFactory { get; set; }

        [Inject]
        Microsoft.Identity.Web.ITokenAcquisition TokenAcquisitionService { get; set; }

        public string ApiEndpoint
        {
            get
            {
                return this.Configuration.GetValue<string>("ApiEndpoint");
            }
        }

        private HttpClient _httpClient;
        protected async override Task OnInitializedAsync()
        {
            try
            {           

                if (_httpClient == null)
                    _httpClient = HttpClientFactory.CreateClient();

                //user_impersonation
                var apiToken = await TokenAcquisitionService.GetAccessTokenForUserAsync(new string[] { "https://jansupolikhotmail.onmicrosoft.com/WebApiNetCore3/user_impersonation" });

                _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", apiToken);
                var apiData = await _httpClient.GetAsync("https://localhost:5011/api/version/20000");
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };

                if (apiData.IsSuccessStatusCode)
                {
                    var content = await apiData.Content.ReadAsStringAsync();
                    var obj = JsonSerializer.Deserialize<VersionDto>(content, options);
                }
            }
            catch (Exception ex)
            {
 
            }
            await base.OnInitializedAsync();
        }
    }

    public class VersionDto
    {
        public int Version { get; set; }

        public string Link { get; set; }

        public string Details { get; set; }

        public DateTime CreatedAt { get; set; }
    }

}
