using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace All_Spice.Repositories;

public class RecipesRepository
{
  private readonly IDbConnection _db;

  public RecipesRepository(IDbConnection db)
  {
    _db = db;
  }

  internal List<Recipe> GetRecipes(Recipe recipeData)
  {
    recipeData.Title = $"%{recipeData.Title}%";
    string sql = @"
    SELECT r.*, a.*
    FROM recipes r
    JOIN accounts a ON r.creatorId = a.id
    WHERE title LIKE @Title
    ;";
    return _db.Query<Recipe, Profile, Recipe>(
      sql,
      (recipe, profile) =>
      {
        recipe.Creator = profile;
        return recipe;
      },
      recipeData).ToList();
  }

  internal Recipe GetRecipeById(int recipeId)
  {
    string sql = @"
    SELECT r.*, a.*
    FROM recipes r
    JOIN accounts a ON r.creatorId = a.id
    WHERE r.id = @recipeId LIMIT 1
    ;";
    return _db.Query<Recipe, Profile, Recipe>(
    sql,
    (recipe, profile) =>
    {
      recipe.Creator = profile;
      return recipe;
    },
    new { recipeId }).FirstOrDefault();
  }

  internal List<Recipe> GetRecipesByAccountId(string accountId)
  {
    string sql = @"
    SELECT r.*, a.*
    FROM recipes r
    JOIN accounts a ON r.creatorId = a.id
    WHERE r.creatorId = @accountId
    ;";
    return _db.Query<Recipe, Profile, Recipe>(
      sql,
      (recipe, profile) =>
      {
        recipe.Creator = profile;
        return recipe;
      },
      new { accountId }).ToList();
  }

  internal int CreateRecipe(Recipe recipeData)
  {
    string sql = @"
    INSERT INTO
    recipes (title, instructions, img, category, creatorId)
    VALUES (@Title, @Instructions, @Img, @Category, @CreatorId);
    SELECT LAST_INSERT_ID()
    ;";
    return _db.ExecuteScalar<int>(sql, recipeData);
  }

  internal Recipe UpdateRecipe(Recipe recipeData)
  {
    string sql = @"
    UPDATE recipes r SET
    title = @Title,
    instructions = @Instructions,
    img = @Img,
    category = @Category
    WHERE r.id = @Id LIMIT 1;
    SELECT r.*, a.*
    FROM recipes r
    JOIN accounts a ON r.creatorId = a.id
    WHERE r.id = @Id
    ;";
    return _db.Query<Recipe, Profile, Recipe>(
      sql,
      (recipe, profile) =>
      {
        recipe.Creator = profile;
        return recipe;
      },
      recipeData
    ).SingleOrDefault();
  }

  internal void RemoveRecipe(int recipeId)
  {
    string sql = "DELETE FROM recipes WHERE id = @recipeId LIMIT 1";
    _db.Execute(sql, new { recipeId });
  }
}
