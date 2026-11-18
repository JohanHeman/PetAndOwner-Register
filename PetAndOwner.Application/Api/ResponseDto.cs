using System.Text.Json.Serialization;

namespace PetAndOwner.Application.Api;

public class ResponseDto
{
    [JsonPropertyName("data")]
    public List<CountryDto> Data { get; set; }
}