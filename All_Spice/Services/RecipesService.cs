using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace All_Spice.Services;

public class RecipesService
{
  private readonly RecipesRepository _recipesRepository;

  public RecipesService(RecipesRepository recipesRepository)
  {
    _recipesRepository = recipesRepository;
  }

  internal List<Recipe> GetRecipe()
  {
    return _recipesRepository.GetRecipes();
  }

  internal Recipe GetRecipeById(int recipeId)
  {
    Recipe recipe = _recipesRepository.GetRecipeById(recipeId);
    return recipe ?? throw new Exception($"[NO RECIPE MATCHES THE ID: {recipeId}]");
  }

  internal Recipe CreateRecipe(Recipe recipeData)
  {
    int recipeId = _recipesRepository.CreateRecipe(recipeData);
    return GetRecipeById(recipeId);
  }

  internal Recipe UpdateRecipe(Recipe recipeData)
  {
    Recipe originalRecipe = CheckData(recipeData.Id, recipeData.CreatorId);
    originalRecipe.Title = recipeData.Title ?? originalRecipe.Title;
    originalRecipe.Instructions = recipeData.Instructions ?? originalRecipe.Instructions;
    originalRecipe.Img = recipeData.Img ?? originalRecipe.Img;
    originalRecipe.Category = recipeData.Category ?? originalRecipe.Category;
    return _recipesRepository.UpdateRecipe(originalRecipe);
  }

  internal Recipe RemoveRecipe(int recipeId, string userId)
  {
    Recipe recipeToRemove = CheckData(recipeId, userId);
    _recipesRepository.RemoveRecipe(recipeId);
    return recipeToRemove;
  }

  private Recipe CheckData(int recipeId, string userId)
  {
    Recipe recipe = GetRecipeById(recipeId);
    if (recipe.CreatorId != userId)
    {
      throw new Exception($"[YOU ARE NOT THE CREATOR OF {recipe.Title}]");
    }
    return recipe;
  }
}
