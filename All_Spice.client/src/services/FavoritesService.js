import { AppState } from "../AppState.js"
import { Recipe } from "../models/Recipe.js"
import { logger } from "../utils/Logger.js"
import { api } from "./AxiosService.js"

class FavoritesService {
  async getFavoritesByAccountId(isRecipe) {
    const res = await api.get('account/favorites')

    if (isRecipe) {
      AppState.recipes = res.data.map(d => new Recipe(d))
    } else {
      AppState.favorites = res.data.map(d => new Recipe(d))
    }
  }

  async createFavorite(recipeId) {
    const res = await api.post('api/favorites', { recipeId: recipeId })
    res.data.favoriteId = res.data.id
    res.data.id = recipeId
    AppState.favorites.push(res.data)
    logger.log(AppState.favorites.find(f => f.id == recipeId))
  }

  async removeFavorite(recipeId) {
    const favorites = AppState.favorites
    const foundFavorite = favorites.find(f => f.id == recipeId)
    const res = await api.delete(`api/favorites/${foundFavorite.favoriteId}`)
    logger.log(res.data)
    AppState.favorites = favorites.filter(f => f.id != res.data.recipeId)
  }
}

export const favoritesService = new FavoritesService()