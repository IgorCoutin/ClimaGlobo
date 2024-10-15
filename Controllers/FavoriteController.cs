using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

[ApiController]
[Route("api/[controller]")]
public class FavoritesController : ControllerBase
{
    private readonly FavoriteService _favoriteService;

    public FavoritesController(FavoriteService favoriteService)
    {
        _favoriteService = favoriteService;
    }

    // Endpoint para adicionar uma cidade/país aos favoritos
    [HttpPost("add")]
    public IActionResult AddFavorite([FromBody] FavoriteDto favorite)
    {
        _favoriteService.AddFavorite(favorite);
        return Ok(new { Success = true, Message = "Cidade e país adicionados aos favoritos." });
    }

    // Endpoint para remover uma cidade/país dos favoritos
    [HttpPost("remove")]
    public IActionResult RemoveFavorite([FromBody] FavoriteDto favorite)
    {
        _favoriteService.RemoveFavorite(favorite);
        return Ok(new { Success = true, Message = "Cidade e país removidos dos favoritos." });
    }

    // Endpoint para listar todas as cidades/países favoritos
    [HttpGet("list")]
    public IActionResult GetFavorites()
    {
        List<FavoriteDto> favorites = _favoriteService.GetAllFavorites();
        return Ok(new { Success = true, Favorites = favorites });
    }
}
