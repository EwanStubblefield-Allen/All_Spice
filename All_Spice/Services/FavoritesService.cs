using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace All_Spice.Services;

public class FavoritesService
{
  private readonly FavoritesRepository _favoritesRepository;

  public FavoritesService(FavoritesRepository favoritesRepository)
  {
    _favoritesRepository = favoritesRepository;
  }

  internal Favorite GetFavoriteById(int favoriteId)
  {
    Favorite favorite = _favoritesRepository.GetFavoriteById(favoriteId);
    return favorite ?? throw new Exception($"[NO FAVORITE MATCHES THE ID: {favoriteId}]");
  }

  internal List<RecipeFav> GetFavoritesByAccountId(string accountId)
  {
    return _favoritesRepository.GetFavoritesByAccountId(accountId);
  }

  internal Favorite CreateFavorite(Favorite favoriteData)
  {
    return _favoritesRepository.CreateFavorite(favoriteData);
  }

  internal Favorite RemoveFavorite(int favoriteId, string userId)
  {
    Favorite favorite = GetFavoriteById(favoriteId);
    if (favorite.AccountId != userId)
    {
      throw new Exception("[YOU ARE NOT THE CREATOR OF THIS FAVORITE!]");
    }
    _favoritesRepository.RemoveFavorite(favoriteId);
    return favorite;
  }
}
