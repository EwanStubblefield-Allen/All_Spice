<template>
  <nav class="row navbar navbar-dark bg-img px-3 align-items-start px-0 position-relative">
    <div class="offset-1 col-10 navbar-brand me-0 d-flex justify-content-center">
      <div class="d-flex flex-column align-items-center my-5 py-2">
        <p class="fs-1">All-Spice</p>
        <p class="sub-title">Cherish Your Family <br> And Their Cooking</p>
      </div>
    </div>

    <div class="col-1 d-flex justify-content-end">
      <!-- LOGIN COMPONENT HERE -->
      <Login />
    </div>

    <div class="col-8 col-md-6 col-lg-4 d-flex align-items-center text-center bg-light text-green filter px-0">
      <p @click="getRecipes()" class="flex-grow-1 selectable py-2">Home</p>
      <p @click="getRecipesByAccountId()" class="flex-grow-1 selectable py-2">My Recipes</p>
      <p @click="getFavoritesByAccountId()" class="flex-grow-1 selectable py-2">Favorites</p>
    </div>
  </nav>
</template>

<script>
import { recipesService } from '../services/RecipesService.js'
import { favoritesService } from '../services/FavoritesService.js'
import Pop from '../utils/Pop.js'
import Login from './Login.vue'

export default {
  setup() {
    return {
      async getRecipes() {
        try {
          await recipesService.getRecipes()
        } catch (error) {
          Pop.error(error.message, '[GETTING RECIPES]')
        }
      },

      async getRecipesByAccountId() {
        try {
          await recipesService.getRecipesByAccountId()
        } catch (error) {
          Pop.error(error.message, '[GETTING RECIPES BY ACCOUNT ID]')
        }
      },

      async getFavoritesByAccountId() {
        try {
          await favoritesService.getFavoritesByAccountId(true)
        } catch (error) {
          Pop.error(error.message, '[GETTING FAVORITES BY ACCOUNT ID]')
        }
      }
    }
  },
  components: { Login }
}
</script>

<style scoped>
  a:hover {
    text-decoration: none;
  }

  .nav-link {
    text-transform: uppercase;
  }

  .navbar-nav .router-link-exact-active {
    border-bottom: 2px solid var(--bs-success);
    border-bottom-left-radius: 0;
    border-bottom-right-radius: 0;
  }

  .bg-img {
    background-image: url(../assets/img/unsplash_pqJ21tErTgI.png);
    background-position: center;
    background-size: cover;
    background-repeat: none;
    margin: 3vh;
    border-radius: 5px;
    box-shadow: 0px 4px 15px rgba(0, 0, 0, 0.25);
  }

  .sub-title {
    font-size: 1em;
    text-shadow: 0px 2px 3px #000;
  }

  .filter {
    position: absolute;
    bottom: -20px;
    left: 16%;
    box-shadow: 0px 4px 15px rgba(0, 0, 0, 0.25);
  }

  @media screen and (min-width: 760px) {
    .filter {
      left: 25%;
    }
  }

  @media screen and (min-width: 960px) {
    .filter {
      left: 33%;
    }
  }
</style>