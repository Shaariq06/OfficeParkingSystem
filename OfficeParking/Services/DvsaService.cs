using System.Net.Http.Headers;
using System.Text.Json;
using Microsoft.Extensions.Options;
using OfficeParkingSystem.Models.DTO;

namespace OfficeParkingSystem.Services
{
    public class DvsaService : IDvsaService
    {
        private readonly HttpClient _httpClient;
        private readonly DvsaApiSettings _settings;
        private string? _accessToken;
        private DateTime _tokenExpiryTime;

        public DvsaService(HttpClient httpClient, IOptions<DvsaApiSettings> settings)
        {
            _httpClient = httpClient;
            _settings = settings.Value;
            _tokenExpiryTime = DateTime.UtcNow.AddMinutes(-1);
        }

        private async Task<string> GetAccessTokenAsync()
        {
            if (!string.IsNullOrEmpty(_accessToken) && _tokenExpiryTime > DateTime.UtcNow)
            {
                return _accessToken;
            }

            var request = new HttpRequestMessage(HttpMethod.Post, _settings.TokenUrl);
            var formData = new List<KeyValuePair<string, string>>
            {
                new("grant_type", "client_credentials"),
                new("client_id", _settings.ClientId),
                new("client_secret", _settings.ClientSecret),
                new("scope", _settings.ScopeUrl)
            };
            request.Content = new FormUrlEncodedContent(formData);

            var response = await _httpClient.SendAsync(request);
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            var tokenResponse = JsonSerializer.Deserialize<TokenResponseDTO>(content);

            _accessToken = tokenResponse?.access_token;
            _tokenExpiryTime = DateTime.UtcNow.AddSeconds(tokenResponse?.expires_in - 60 ?? 3540);

            return _accessToken ?? throw new InvalidOperationException("Could not retrieve a valid access token from DVSA.");
        }

        public async Task<DvsaVehicleDetailsDTO?> GetVehicleDetailsAsync(string registration)
        {
            var token = await GetAccessTokenAsync();

            var request = new HttpRequestMessage(
                HttpMethod.Get,
                $"https://history.mot.api.gov.uk/v1/trade/vehicles/registration/{registration}"
            );

            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
            request.Headers.Add("x-api-key", _settings.ApiKey);
            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var response = await _httpClient.SendAsync(request);

            if (!response.IsSuccessStatusCode)
            {
                return null;
            }

            var content = await response.Content.ReadAsStringAsync();
            var vehicleData = JsonSerializer.Deserialize<DvsaVehicleDetailsDTO>(content);

            return vehicleData;
        }
    }
}