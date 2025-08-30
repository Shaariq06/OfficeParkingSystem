using System.Text.Json.Serialization;

namespace OfficeParkingSystem.Models.DTO
{
    public class DvsaApiSettings
    {
        public required string TokenUrl { get; set; }
        public required string ScopeUrl { get; set; }
        public required string ClientId { get; set; }
        public required string ClientSecret { get; set; }
        public required string ApiKey { get; set; }
    }

    public class TokenResponseDTO
    {
        [JsonPropertyName("access_token")]
        public string? access_token { get; set; }

        [JsonPropertyName("expires_in")]
        public int expires_in { get; set; }
    }

    public class DvsaVehicleDetailsDTO
    {
        [JsonPropertyName("make")]
        public string? Make { get; set; }

        [JsonPropertyName("model")]
        public string? Model { get; set; }

        [JsonPropertyName("primaryColour")]
        public string? Colour { get; set; }

        [JsonPropertyName("registrationDate")]
        public string? RegistrationDate { get; set; }

        public int? Year
        {
            get
            {
                if (DateTime.TryParse(RegistrationDate, out DateTime parsedDate))
                {
                    return parsedDate.Year;
                }
                return null;
            }
        }
    }
}
