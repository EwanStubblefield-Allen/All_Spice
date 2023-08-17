using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace All_Spice.Repositories;

public class FavoritesRepository
{
  private readonly IDbConnection _db;

  public FavoritesRepository(IDbConnection db)
  {
    _db = db;
  }

  internal Favorite GetFavoriteById(int favoriteId)
  {
    string sql = "SELECT * FROM favorites WHERE id = @favoriteId;";
    return _db.QueryFirstOrDefault<Favorite>(sql, new { favoriteId });
  }

  internal List<ProfileFav> GetFavoritesByAccountId(string accountId)
  {
    string sql = @"
    SELECT f.*, a.*
    FROM favorites f 
    JOIN accounts a ON a.id = f.accountId
    WHERE accountId = @accountId
    ;";
    return _db.Query<Favorite, ProfileFav, ProfileFav>(
    sql,
    (favorite, profile) =>
    {
      profile.FavoriteId = favorite.Id;
      return profile;
    },
    new { accountId }).ToList();
  }

  internal Favorite CreateFavorite(Favorite favoriteData)
  {
    string sql = @"
    INSERT INTO
    favorites(accountId, recipeId)
    VALUES(@accountId, @RecipeId);
    SELECT LAST_INSERT_ID()
    ;";
    int favoriteId = _db.ExecuteScalar<int>(sql, favoriteData);
    favoriteData.Id = favoriteId;
    return favoriteData;
  }

  internal void RemoveFavorite(int favoriteId)
  {
    string sql = "DELETE FROM favorites WHERE id = @favoriteId LIMIT 1;";
    _db.Execute(sql, new { favoriteId });
  }
}
