using System.Text.Json;
using PetAndOwner.Domain.Entities;

namespace PetAndOwner.Application.Api;

public class ApiService
{
    private readonly HttpClient _httpClient;

    public ApiService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<List<Country>> GetCountriesAsync()
    {
        List<Country> countries = new List<Country>();
        var response = await _httpClient.GetAsync("/countries");
        if (response.IsSuccessStatusCode)
        {
            var json = await response.Content.ReadAsStringAsync();
            ResponseDto dto = JsonSerializer.Deserialize<ResponseDto>(json);

            foreach (var countryDto in dto.Data)
            {
                var country = new Country()
                {
                    Id = Guid.NewGuid(),
                    Name = countryDto.Country,
                };

                country.Cities = countryDto.Cities.Select(cityName => new City()
                {
                    Id = Guid.NewGuid(),
                    Name = cityName,
                    Country = country,
                }).ToList();

                countries.Add(country);
            }
        }
        else
        {
            throw new HttpRequestException("Something went wrong");
        }
        return countries;
    }
}