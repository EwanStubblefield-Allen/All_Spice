<template>
  <div class="card-bg elevation-5 d-flex flex-column justify-content-between">
    <div class="d-flex justify-content-between align-items-start">
      <p class="category-bg fs-5 px-3 m-3">{{ recipeProp.category }}</p>
      <i v-if="isFav" @click="removeFavorite()" class="mdi mdi-heart fs-4 text-bg rounded-bottom mx-2 px-1 selectable"></i>
      <i v-else-if="account.id" @click="createFavorite()" class="mdi mdi-heart-outline fs-4 text-bg rounded-bottom mx-2 px-1 selectable"></i>
    </div>
    <div @click="setData()" class="text-bg m-2 p-1 rounded selectable" data-bs-toggle="modal" data-bs-target="#recipeDetails">
      <p class="fs-5 fw-bold">{{ recipeProp.title }}</p>
      <p>{{ recipeProp.instructions }}</p>
    </div>
  </div>
</template>

<script>
import { computed } from 'vue'
import { Recipe } from '../models/Recipe.js'
import { AppState } from '../AppState.js'
import { recipesService } from '../services/RecipesService.js'
import { ingredientsService } from '../services/IngredientsService.js'
import { favoritesService } from '../services/FavoritesService.js'
import Pop from '../utils/Pop.js'

export default {
  props: {
    recipeProp: {
      type: Recipe,
      required: true
    }
  },

  setup(props) {
    return {
      account: computed(() => AppState.account),
      isFav: computed(() => AppState.favorites.find(f => f.id == props.recipeProp.id)),
      backgroundImg: computed(() => `url("${props.recipeProp.img}")`),

      async setData() {
        try {
          recipesService.setActiveRecipe(props.recipeProp)
          await ingredientsService.getIngredients()
        } catch (error) {
          Pop.error(error.message, '[GETTING INGREDIENTS]')
        }
      },

      async createFavorite() {
        try {
          favoritesService.createFavorite(props.recipeProp.id)
        } catch (error) {
          Pop.error(error.message, '[CREATING FAVORITE]')
        }
      },

      async removeFavorite() {
        try {
          favoritesService.removeFavorite(props.recipeProp.id)
        } catch (error) {
          Pop.error(error.message, '[DELETING FAVORITE]')
        }
      }
    }
  }
}
</script>

<style lang="scss" scoped>
.card-bg {
  border-radius: 5px;
  background: v-bind(backgroundImg);
  background-position: center;
  background-size: cover;
  height: 40vh;
}
</style>