using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace All_Spice.Services;

public class IngredientsService
{
  private readonly IngredientsRepository _ingredientsRepository;

  public IngredientsService(IngredientsRepository ingredientsRepository)
  {
    _ingredientsRepository = ingredientsRepository;
  }

  internal Ingredient GetIngredientById(int ingredientId)
  {
    Ingredient ingredient = _ingredientsRepository.GetIngredientById(ingredientId);
    return ingredient ?? throw new Exception($"[NO INGREDIENT MATCHES THE ID: {ingredientId}]");
  }

  internal List<Ingredient> GetIngredientsByRecipeId(int recipeId)
  {
    return _ingredientsRepository.GetIngredientsByRecipeId(recipeId);
  }

  internal Ingredient CreateIngredient(Ingredient ingredientData)
  {
    int ingredientId = _ingredientsRepository.CreateIngredient(ingredientData);
    return GetIngredientById(ingredientId);
  }

  internal Ingredient RemoveIngredient(int ingredientId)
  {
    Ingredient ingredient = GetIngredientById(ingredientId);
    _ingredientsRepository.RemoveIngredient(ingredientId);
    return ingredient;
  }
}