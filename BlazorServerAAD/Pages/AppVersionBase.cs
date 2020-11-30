using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace BlazorServerAAD.Pages
{
    public class AppVersionBase : ComponentBase
    {
        //const string apiPart = "https://localhost:5011/api/version/";
        const string apiPart = "api/version/";

        [Inject]
        public IConfiguration Configuration { get; set; }

        [Inject]
        IHttpClientFactory HttpClientFactory { get; set; }

        [Inject]
        Microsoft.Identity.Web.ITokenAcquisition TokenAcquisitionService { get; set; }

        //public string ApiEndpoint
        //{
        //    get
        //    {
        //        return this.Configuration.GetValue<string>("ApiEndpoint");
        //    }
        //}

        private HttpClient _httpClient;
        protected async override Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();            
            var v = await GetVersion();           
            if (v != null)
            {
                VersionDto add = new VersionDto { Link = "www.bing.com", Version = v.Version + 1, Details= $"Created in client at {DateTime.Now.ToShortTimeString()}" };
                var r = await AddVersion(add);
                if (r != null)
                {
                    //var v2 = await GetVersion(r.Version);
                    var all = await GetAllVersion();

                    //var deleted = await DeleteVersion(v.Version);
                }
            }
        }

        private async Task<VersionDto> GetVersion(int id = 1)
        {
            try
            {
                if (_httpClient == null)
                    _httpClient = HttpClientFactory.CreateClient("api");

                //user_impersonation
                var apiToken = await TokenAcquisitionService.GetAccessTokenForUserAsync(new string[] { "https://jansupolikhotmail.onmicrosoft.com/WebApiNetCore3/user_impersonation" });

                _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", apiToken);                
                var url = $"{apiPart}{id}";
                var apiData = await _httpClient.GetAsync(url).ConfigureAwait(false);
                var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

                if (apiData.IsSuccessStatusCode)
                {
                    var content = await apiData.Content.ReadAsStringAsync();
                    var version = JsonSerializer.Deserialize<VersionDto>(content, options);
                    return version;
                }
            }
            catch (Exception ex)
            {

            }
            return null;
        }

        private async Task<List<VersionDto>> GetAllVersion()
        {
            try
            {
                if (_httpClient == null)
                    _httpClient = HttpClientFactory.CreateClient("api");

                //user_impersonation
                var apiToken = await TokenAcquisitionService.GetAccessTokenForUserAsync(new string[] { "https://jansupolikhotmail.onmicrosoft.com/WebApiNetCore3/user_impersonation" });

                _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", apiToken);
                var url = $"{apiPart}";
                var apiData = await _httpClient.GetAsync(url).ConfigureAwait(false);
                var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

                if (apiData.IsSuccessStatusCode)
                {
                    var content = await apiData.Content.ReadAsStringAsync();
                    var version = JsonSerializer.Deserialize<List<VersionDto>>(content, options);
                    return version;
                }
            }
            catch (Exception ex)
            {

            }
            return null;
        }


        private async Task<VersionDto> AddVersion(VersionDto add)
        {
            try
            {
                if (_httpClient == null)
                    _httpClient = HttpClientFactory.CreateClient("api");

                //user_impersonation
                var apiToken = await TokenAcquisitionService.GetAccessTokenForUserAsync(new string[] { "https://jansupolikhotmail.onmicrosoft.com/WebApiNetCore3/user_impersonation" });

                _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", apiToken);
                var url = $"{apiPart}";
                var options = new JsonSerializerOptions {PropertyNameCaseInsensitive = true };
                var httpcontent = new StringContent(JsonSerializer.Serialize(add, options), Encoding.UTF8, "application/json");
                var apiData = await _httpClient.PostAsync(url, httpcontent).ConfigureAwait(false);

                if (apiData.IsSuccessStatusCode)
                {
                    var content = await apiData.Content.ReadAsStringAsync();
                    var version = JsonSerializer.Deserialize<VersionDto>(content, options);
                    return version;
                }
            }
            catch (Exception ex)
            {

            }
            return null;
        }

        private async Task<bool> DeleteVersion(int id)
        {
            try
            {
                if (_httpClient == null)
                    _httpClient = HttpClientFactory.CreateClient("api");

                //user_impersonation
                var apiToken = await TokenAcquisitionService.GetAccessTokenForUserAsync(new string[] { "https://jansupolikhotmail.onmicrosoft.com/WebApiNetCore3/user_impersonation" });

                _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", apiToken);
                var url = $"{apiPart}{id}";
                var apiData = await _httpClient.DeleteAsync(url).ConfigureAwait(false);                

                if (apiData.IsSuccessStatusCode)                
                    return true;                
            }
            catch (Exception ex)
            {

            }
            return false;
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
