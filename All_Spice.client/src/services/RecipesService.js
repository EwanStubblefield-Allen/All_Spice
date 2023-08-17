import { AppState } from "../AppState.js"
import { Recipe } from "../models/Recipe.js"
import { api } from "./AxiosService.js"

class RecipesService {
  setActiveRecipe(recipe) {
    AppState.activeRecipe = recipe
  }

  async getRecipes() {
    const res = await api.get('api/recipes')
    AppState.recipes = res.data.map(d => new Recipe(d))
  }

  async getRecipesByAccountId() {
    const res = await api.get('account/recipes')
    AppState.recipes = res.data.map(d => new Recipe(d))
  }
}

export const recipesService = new RecipesService()