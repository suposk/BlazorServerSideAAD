using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
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
                var apiData = await _httpClient.GetAsync("https://localhost:5011/api/version/21");
                if (apiData.IsSuccessStatusCode)
                {
                    var api = await apiData.Content.ReadAsStreamAsync();
                    // AADSTS65001: The user or administrator has not consented to use the application with ID '8ff7755f-f65f-41ca-9e88-f70215f06fb4' named 'BlazorServerAAD'.Send an interactive authorization request for this user and resource.
                }
            }
            catch (Exception ex)
            {
 
            }
            await base.OnInitializedAsync();
        }
    }
}
