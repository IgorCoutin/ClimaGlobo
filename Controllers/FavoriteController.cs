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

    [HttpPost("add")]
    public IActionResult AddFavorite([FromBody] FavoriteDto favorite)
    {
        _favoriteService.AddFavorite(favorite);
        return Ok(new { Success = true, Message = "Cidade e país adicionados aos favoritos." });
    }

    [HttpPost("remove")]
    public IActionResult RemoveFavorite([FromBody] FavoriteDto favorite)
    {
        _favoriteService.RemoveFavorite(favorite);
        return Ok(new { Success = true, Message = "Cidade e país removidos dos favoritos." });
    }

    [HttpGet("list")]
    public IActionResult GetFavorites()
    {
        List<FavoriteDto> favorites = _favoriteService.GetAllFavorites();
        return Ok(new { Success = true, Favorites = favorites });
    }
}
