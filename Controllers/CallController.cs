using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

[ApiController]
[Route("api/[controller]")]
public class CallController : ControllerBase
{
    private readonly CallService _callService;

    public CallController(CallService callService)
    {
        _callService = callService;
    }

    // Endpoint para obter dados climáticos por nome da cidade
    [HttpGet("city/{cityName}")]
    public async Task<IActionResult> GetWeatherByCity(string cityName)
    {
        if (string.IsNullOrWhiteSpace(cityName))
        {
            return BadRequest(new
            {
                Success = false,
                Message = "O nome da cidade não pode ser vazio."
            });
        }

        try
        {
            // Chamar o serviço para obter os dados climáticos
            var callData = await _callService.GetWeatherDataByCityAsync(cityName);

            if (callData == null)
            {
                return NotFound(new
                {
                    Success = false,
                    Message = $"Não foi possível obter dados do clima para a cidade: {cityName}"
                });
            }

            return Ok(new
            {
                Success = true,
                Data = callData
            });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new
            {
                Success = false,
                Message = "Ocorreu um erro ao processar a solicitação.",
                Details = ex.Message
            });
        }
    }
}
