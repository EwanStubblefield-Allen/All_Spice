import { AppState } from "../AppState.js"
import { Recipe } from "../models/Recipe.js"
import { api } from "./AxiosService.js"

class RecipesService {
  setActiveRecipe(recipe) {
    AppState.activeRecipe = recipe
  }

  async getRecipes(query) {
    let res

    if (query) {
      res = await api.get(`api/recipes?title=${query.trim()}`)
    } else {
      res = await api.get('api/recipes')
    }
    AppState.recipes = res.data.map(d => new Recipe(d))
  }

  async getRecipesByAccountId() {
    const res = await api.get('account/recipes')
    AppState.recipes = res.data.map(d => new Recipe(d))
  }

  async createRecipe(recipeData) {
    const res = await api.post('api/recipes', recipeData)
    AppState.recipes.push(new Recipe(res.data))
  }

  async updateRecipe(recipeData) {
    const res = await api.put(`api/recipes/${recipeData.id}`, recipeData)
    const foundIndex = AppState.recipes.findIndex(r => r.id == res.data.id)
    AppState.recipes.splice(foundIndex, 1, new Recipe(res.data))
  }

  async removeRecipe() {
    const res = await api.delete(`api/recipes/${AppState.activeRecipe.id}`)
    AppState.recipes = AppState.recipes.filter(r => r.id != res.data.id)
  }
}

export const recipesService = new RecipesService()