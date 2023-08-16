using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace All_Spice.Repositories;

public class IngredientsRepository
{
  private readonly IDbConnection _db;

  public IngredientsRepository(IDbConnection db)
  {
    _db = db;
  }

  internal Ingredient GetIngredientById(int ingredientId)
  {
    string sql = @"
    SELECT i.*, r.*
    FROM ingredients i
    JOIN recipes r ON i.recipeId = r.id
    WHERE i.id = @ingredientId
    ;";
    return _db.QueryFirstOrDefault<Ingredient>(sql, new { ingredientId });
  }

  internal int CreateIngredient(Ingredient ingredientData)
  {
    string sql = @"
    INSERT INTO
    ingredients (name, quantity, recipeId)
    VALUES (@Name, @Quantity, @RecipeId);
    SELECT LAST_INSERT_ID()
    ;";
    return _db.ExecuteScalar<int>(sql, ingredientData);
  }
}
