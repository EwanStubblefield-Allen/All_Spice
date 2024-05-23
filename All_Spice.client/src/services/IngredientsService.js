import { AppState } from '../AppState.js'
import { Ingredient } from '../models/Ingredient.js'
import { api } from './AxiosService.js'

class IngredientsService {
  async getIngredients() {
    const res = await api.get(`api/recipes/${AppState.activeRecipe.id}/ingredients`)
    AppState.ingredients = res.data.map(d => new Ingredient(d))
  }

  async createIngredient(ingredientData) {
    const res = await api.post('api/ingredients', ingredientData)
    AppState.ingredients.push(new Ingredient(res.data))
  }

  async removeIngredient(ingredientId) {
    await api.delete(`api/ingredients/${ingredientId}`)
    AppState.ingredients = AppState.ingredients.filter(i => i.id != ingredientId)
  }
}

export const ingredientsService = new IngredientsService()
