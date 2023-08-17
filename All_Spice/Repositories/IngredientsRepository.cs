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
    SELECT *
    FROM ingredients
    WHERE id = @ingredientId
    ;";
    return _db.QueryFirstOrDefault<Ingredient>(sql, new { ingredientId });
  }

  internal List<Ingredient> GetIngredientsByRecipeId(int recipeId)
  {
    string sql = @"
    SELECT *
    FROM ingredients
    WHERE recipeId = @recipeId
    ;";
    return _db.Query<Ingredient>(sql, new { recipeId }).ToList();
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

  internal void RemoveIngredient(int ingredientId)
  {
    string sql = "DELETE FROM ingredients WHERE id = @ingredientId LIMIT 1;";
    _db.Execute(sql, new { ingredientId });
  }
}
