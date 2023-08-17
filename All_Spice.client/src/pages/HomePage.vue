<template>
  <section class="row my-3">
    <div v-for="r in recipes" :key="r.id" class="col-12 col-md-6 col-lg-4 px-5 py-3">
      <RecipeCard :recipeProp="r" />
    </div>
  </section>
</template>

<script>
import { computed, onMounted } from 'vue'
import { recipesService } from '../services/RecipesService.js'
import { AppState } from '../AppState.js'
import RecipeCard from '../components/RecipeCard.vue'
import Pop from '../utils/Pop.js'

export default {
  setup() {
    onMounted(() => {
      getRecipes()
    })

    async function getRecipes() {
      try {
        await recipesService.getRecipes()
      } catch (error) {
        Pop.error(error.message, '[GETTING RECIPES]')
      }
    }

    return {
      recipes: computed(() => AppState.recipes)
    }
  },
  components: { RecipeCard }
}
</script>

<style scoped lang="scss"></style>