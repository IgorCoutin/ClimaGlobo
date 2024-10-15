using System.Net.Http;
using System.Threading.Tasks;
using ClimaGloboApi.Dtos;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

public class CallService
{
    private readonly HttpClient _httpClient;
    private readonly string _apiKey;

    public CallService(HttpClient httpClient, IConfiguration configuration)
    {
        _httpClient = httpClient;
        _apiKey = configuration["OpenWeatherMap:ApiKey"];  
    }

    public async Task<(WeatherDto, CountryDto)> GetWeatherDataByCityAsync(string city)
    {
        
        var weatherUrl = $"https://api.openweathermap.org/data/2.5/weather?q={city}&appid={_apiKey}&units=metric";
        
        try
        {
            var weatherResponse = await _httpClient.GetAsync(weatherUrl);
            if (!weatherResponse.IsSuccessStatusCode)
            {
                return (null, null);  
            }

            var weatherContent = await weatherResponse.Content.ReadAsStringAsync();
            if (string.IsNullOrWhiteSpace(weatherContent))
            {
                return (null, null);  
            }

           
            var weatherData = JsonConvert.DeserializeObject<WeatherDto>(weatherContent);

            
            var countryCode = weatherData.Sys.Country;
            var countryUrl = $"https://restcountries.com/v3.1/alpha/{countryCode}";

            var countryResponse = await _httpClient.GetAsync(countryUrl);
            if (!countryResponse.IsSuccessStatusCode)
            {
                return (weatherData, null);  
            }

            var countryContent = await countryResponse.Content.ReadAsStringAsync();
            if (string.IsNullOrWhiteSpace(countryContent))
            {
                return (weatherData, null); 
            }

            
            var countryData = JsonConvert.DeserializeObject<CountryDto[]>(countryContent);

            
            return (weatherData, countryData[0]);
        }
        catch (HttpRequestException ex)
        {
            Console.WriteLine($"Erro na requisição: {ex.Message}");
            return (null, null);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Erro inesperado: {ex.Message}");
            return (null, null);
        }
    }
}
