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

   
    public async Task<CallDto> GetWeatherDataByCityAsync(string city)
    {
        
        var weatherUrl = $"https://api.openweathermap.org/data/2.5/weather?q={city}&appid={_apiKey}&units=metric";
        
        try
        {
            var weatherResponse = await _httpClient.GetAsync(weatherUrl);
            if (!weatherResponse.IsSuccessStatusCode)
            {
                return null;  
            }

            var weatherContent = await weatherResponse.Content.ReadAsStringAsync();
            if (string.IsNullOrWhiteSpace(weatherContent))
            {
                return null; 
            }

            
            var weatherData = JsonConvert.DeserializeObject<WeatherDto>(weatherContent);

            
            var oneCallUrl = $"https://api.openweathermap.org/data/3.0/onecall?lat={weatherData.Coord.Lat}&lon={weatherData.Coord.Lon}&exclude=minutely&appid={_apiKey}&units=metric";

            var oneCallResponse = await _httpClient.GetAsync(oneCallUrl);
            if (!oneCallResponse.IsSuccessStatusCode)
            {
                return null;
            }

            var oneCallContent = await oneCallResponse.Content.ReadAsStringAsync();
            if (string.IsNullOrWhiteSpace(oneCallContent))
            {
                return null;
            }

            // Desserializar a resposta do OneCall para o DTO de clima
            return JsonConvert.DeserializeObject<CallDto>(oneCallContent);
        }
        catch (HttpRequestException ex)
        {
            
            Console.WriteLine($"Erro na requisição: {ex.Message}");
            return null;
        }
        catch (Exception ex)
        {
            
            Console.WriteLine($"Erro inesperado: {ex.Message}");
            return null;
        }
    }
}
