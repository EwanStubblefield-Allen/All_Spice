<template>
  <nav class="row navbar navbar-dark bg-img px-3 align-items-start px-0 position-relative">
    <div class="d-flex justify-content-end align-items-center">
      <form @submit.prevent="getRecipes()" id="form" class="px-3">
        <div class="form-group d-flex align-items-center">
          <div class="input-group">
            <input v-model="editable" id="search" class="form-control" type="text" minlength="3" maxlength="50" placeholder="Search..." required>
            <button type="submit" class="input-group-text" id="comment" title="Post Comment"><i class="mdi mdi-magnify"></i></button>
          </div>
        </div>
      </form>
      <!-- LOGIN COMPONENT HERE -->
      <Login />
    </div>

    <div class="offset-1 col-10 navbar-brand me-0 d-flex justify-content-center">
      <div class="d-flex flex-column align-items-center mb-5 pb-2">
        <p class="fs-1">All-Spice</p>
        <p class="sub-title">Cherish Your Family <br> And Their Cooking</p>
      </div>
    </div>

    <div class="col-8 col-md-6 col-lg-4 d-flex align-items-center text-center bg-light text-green filter px-0">
      <p @click="getRecipes()" class="flex-grow-1 selectable py-2">Home</p>
      <p @click="getRecipesByAccountId()" class="flex-grow-1 selectable py-2">My Recipes</p>
      <p @click="getFavoritesByAccountId()" class="flex-grow-1 selectable py-2">Favorites</p>
    </div>
  </nav>
</template>

<script>
import { ref } from 'vue'
import { recipesService } from '../services/RecipesService.js'
import { favoritesService } from '../services/FavoritesService.js'
import Pop from '../utils/Pop.js'
import Login from './Login.vue'

export default {
  setup() {
    const editable = ref('')

    return {
      editable,

      async getRecipes() {
        try {
          await recipesService.getRecipes(editable.value)
          editable.value = ''
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

  @media screen and (min-width: 770px) {
    .filter {
      left: 25%;
    }
  }

  @media screen and (min-width: 992px) {
    .filter {
      left: 33%;
    }
  }
</style>