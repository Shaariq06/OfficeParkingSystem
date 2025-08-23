using System.Text;
using System.Text.Json;
using OfficeParkingSystem.Models.DTO;
using OfficeParkingSystem.Services;

public class DvlaService : IDvlaService
{
    private readonly HttpClient _httpClient;
    private readonly IConfiguration _configuration;

    public DvlaService(HttpClient httpClient, IConfiguration configuration)
    {
        _httpClient = httpClient;
        _configuration = configuration;
    }

    public async Task<DvlaVehicleResponseDTO?> GetVehicleDetailsAsync(string regNo)
    {
        var apiUrl = "https://driver-vehicle-licensing.api.gov.uk/vehicle-enquiry/v1/vehicles";
        var apiKey = _configuration["Dvla:ApiKey"];

        var requestBody = new
        {
            registrationNumber = regNo.ToUpper().Replace(" ", string.Empty)
        };

        var json = JsonSerializer.Serialize(requestBody);
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        _httpClient.DefaultRequestHeaders.Clear();
        _httpClient.DefaultRequestHeaders.Add("x-api-key", apiKey);

        var response = await _httpClient.PostAsync(apiUrl, content);
        if (!response.IsSuccessStatusCode)
        {
            // This can happen if the API key is wrong or the service is down.
            return null;
        }

        var dvlaRaw = await response.Content.ReadFromJsonAsync<JsonElement>();

        // Safely check for the presence of a key before trying to access it.
        // The DVLA API can return an 'errors' object even with a 200 OK status.
        if (!dvlaRaw.TryGetProperty("make", out _))
        {
            return null; // The response is not a valid vehicle, so we return null.
        }

        var dto = new DvlaVehicleResponseDTO();

        dto.Make = dvlaRaw.GetProperty("make").GetString() ?? string.Empty;
        dto.Colour = dvlaRaw.GetProperty("colour").GetString() ?? string.Empty;
        dto.Year = dvlaRaw.GetProperty("yearOfManufacture").GetInt32();

        return dto;
    }
}