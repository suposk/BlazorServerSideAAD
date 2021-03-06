﻿using Client.Services.Dto;
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
        const string scope = "https://jansupolikhotmail.onmicrosoft.com/WebApiNetCore3/user_impersonation";
        JsonSerializerOptions _options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ITokenAcquisition _tokenAcquisition;
        private HttpClient _httpClient;

        public VersionService(IHttpClientFactory httpClientFactory, ITokenAcquisition tokenAcquisition)
        {
            _httpClientFactory = httpClientFactory;
            _tokenAcquisition = tokenAcquisition;
        }

        public async Task<VersionDto> GetVersion(string version = "0")
        {
            try
            {
                if (_httpClient == null)
                    _httpClient = _httpClientFactory.CreateClient("api");

                //user_impersonation
                var apiToken = await _tokenAcquisition.GetAccessTokenForUserAsync(new string[] { scope });

                _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", apiToken);
                var url = $"{apiPart}{version}";
                var apiData = await _httpClient.GetAsync(url).ConfigureAwait(false);                

                if (apiData.IsSuccessStatusCode)
                {
                    var content = await apiData.Content.ReadAsStringAsync();
                    var result = JsonSerializer.Deserialize<VersionDto>(content, _options);
                    return result;
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
                var apiToken = await _tokenAcquisition.GetAccessTokenForUserAsync(new string[] { scope });

                _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", apiToken);
                var url = $"{apiPart}";
                var apiData = await _httpClient.GetAsync(url).ConfigureAwait(false);                

                if (apiData.IsSuccessStatusCode)
                {
                    var content = await apiData.Content.ReadAsStringAsync();
                    var version = JsonSerializer.Deserialize<List<VersionDto>>(content, _options);
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
                var apiToken = await _tokenAcquisition.GetAccessTokenForUserAsync(new string[] { scope });

                _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", apiToken);
                var url = $"{apiPart}";                
                var httpcontent = new StringContent(JsonSerializer.Serialize(add, _options), Encoding.UTF8, "application/json");
                var apiData = await _httpClient.PostAsync(url, httpcontent).ConfigureAwait(false);

                if (apiData.IsSuccessStatusCode)
                {
                    var content = await apiData.Content.ReadAsStringAsync();
                    var version = JsonSerializer.Deserialize<VersionDto>(content, _options);
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
                var apiToken = await _tokenAcquisition.GetAccessTokenForUserAsync(new string[] { scope });

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
