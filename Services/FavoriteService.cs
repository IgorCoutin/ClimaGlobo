using System.Collections.Generic;
using System.Linq;

public class FavoriteService
{
    private readonly List<FavoriteDto> _favorites = new List<FavoriteDto>();

    public void AddFavorite(FavoriteDto favorite)
    {
        if (!_favorites.Any(f => f.City == favorite.City && f.Country == favorite.Country))
        {
            _favorites.Add(favorite);
        }
    }

    public void RemoveFavorite(FavoriteDto favorite)
    {
        var existingFavorite = _favorites.FirstOrDefault(f => f.City == favorite.City && f.Country == favorite.Country);
        if (existingFavorite != null)
        {
            _favorites.Remove(existingFavorite);
        }
    }

    public List<FavoriteDto> GetAllFavorites()
    {
        return _favorites;
    }
}
