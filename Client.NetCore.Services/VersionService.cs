using Client.Services.Dto;
using Microsoft.Identity.Web;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Client.NetCore.Services
{
    public class VersionService : IVersionService
    {
        //const string apiPart = "https://localhost:5011/api/version/";
        const string apiPart = "api/version/";

        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ITokenAcquisition _tokenAcquisition;
        private HttpClient _httpClient;

        public VersionService(IHttpClientFactory httpClientFactory, ITokenAcquisition tokenAcquisition)
        {
            _httpClientFactory = httpClientFactory;
            _tokenAcquisition = tokenAcquisition;
        }

        public async Task<VersionDto> GetVersion(int id = 1)
        {
            try
            {
                if (_httpClient == null)
                    _httpClient = _httpClientFactory.CreateClient("api");

                //user_impersonation
                var apiToken = await _tokenAcquisition.GetAccessTokenForUserAsync(new string[] { "https://jansupolikhotmail.onmicrosoft.com/WebApiNetCore3/user_impersonation" });

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

        public async Task<List<VersionDto>> GetAllVersion()
        {
            try
            {
                if (_httpClient == null)
                    _httpClient = _httpClientFactory.CreateClient("api");

                //user_impersonation
                var apiToken = await _tokenAcquisition.GetAccessTokenForUserAsync(new string[] { "https://jansupolikhotmail.onmicrosoft.com/WebApiNetCore3/user_impersonation" });

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


        public async Task<VersionDto> AddVersion(VersionDto add)
        {
            try
            {
                if (_httpClient == null)
                    _httpClient = _httpClientFactory.CreateClient("api");

                //user_impersonation
                var apiToken = await _tokenAcquisition.GetAccessTokenForUserAsync(new string[] { "https://jansupolikhotmail.onmicrosoft.com/WebApiNetCore3/user_impersonation" });

                _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", apiToken);
                var url = $"{apiPart}";
                var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
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

        public async Task<bool> DeleteVersion(int id)
        {
            try
            {
                if (_httpClient == null)
                    _httpClient = _httpClientFactory.CreateClient("api");

                //user_impersonation
                var apiToken = await _tokenAcquisition.GetAccessTokenForUserAsync(new string[] { "https://jansupolikhotmail.onmicrosoft.com/WebApiNetCore3/user_impersonation" });

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
}
