using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

[ApiController]
[Route("api/[controller]")]
public class CallController : ControllerBase
{
    private readonly CallService _callService;

    public CallController(CallService callService)
    {
        _callService = callService;
    }

    [HttpGet("{city}")]
    public async Task<IActionResult> GetWeatherAndCountryInfo(string city)
    {
        var (weatherData, countryData) = await _callService.GetWeatherDataByCityAsync(city);

        if (weatherData == null)
        {
            return NotFound(new { Success = false, Message = $"Não foi possível obter dados do clima para a cidade: {city}" });
        }

        if (countryData == null)
        {
            return Ok(new
            {
                Success = true,
                Weather = weatherData,
                Message = "Dados do país não disponíveis."
            });
        }

        return Ok(new
        {
            Success = true,
            Weather = weatherData,
            Country = countryData
        });
    }
}
