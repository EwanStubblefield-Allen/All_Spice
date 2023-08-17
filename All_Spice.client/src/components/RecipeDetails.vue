<template>
  <section v-if="activeRecipe" class="row">
    <img class="col-12 col-lg-4 img-fluid px-0 rounded" :src="activeRecipe.img" :alt="activeRecipe.title">
    <div class="col-12 col-lg-8 d-flex flex-column justify-content-between">
      <div>
        <div class="d-flex justify-content-between align-items-start">
          <div class="my-3 p-3">
            <div class="d-flex align-items-center">
              <p class="fs-3 text-green">{{ activeRecipe.title }}</p>
              <p class="category-bg fs-5 px-3 m-3">{{ activeRecipe.category }}</p>
            </div>
            <p class="text-secondary fs-5">{{ activeRecipe.instructions }}</p>
          </div>
          <button type="button" class="btn mdi mdi-close text-danger fs-3" data-bs-dismiss="modal" aria-label="Close"></button>
        </div>
        <section class="row">
          <div class="col-12 col-lg-6 px-3">
            <div class="food-card">
              <p class="title fs-4 text-center p-2">Ingredients</p>
              <div class=" d-flex flex-column justify-content-between p-3 text-dark">
                <div v-if="ingredients.length">
                  <div v-for="i in ingredients" :key="i.id" class="d-flex justify-content-between">
                    <p>{{ i.quantity }} {{ i.name }}</p>
                    <i v-if="activeRecipe.creatorId == account.id" @click="removeIngredient(i.id)" class="mdi mdi-trash-can text-danger selectable" title="Remove Ingredient"></i>
                  </div>
                </div>
                <form @submit.prevent="createIngredient()" v-if="activeRecipe.creatorId == account.id">
                  <div class="form-group">
                    <label for="ingredient"></label>
                    <div class="input-group">
                      <input v-model="editable.quantity" class="form-control" type="text" placeholder="Quantity..." required>
                      <input v-model="editable.name" class="form-control w-25" type="text" placeholder="Add Ingredient..." required>
                      <button type="submit" class="mdi mdi-plus input-group-text" title="Add Ingredient"></button>
                    </div>
                  </div>
                </form>
              </div>
            </div>
          </div>
        </section>
      </div>
      <div class="text-end p-3">
        <p class="text-secondary">published by: {{ activeRecipe.creator.name }}</p>
      </div>
    </div>
  </section>
</template>

<script>
import { AppState } from '../AppState.js'
import { computed, onMounted, ref } from 'vue'
import { ingredientsService } from '../services/IngredientsService.js'
import Pop from '../utils/Pop.js'

export default {
  setup() {
    const editable = ref({})

    onMounted(() => {
      const recipeModal = document.getElementById('recipeDetails')
      recipeModal.addEventListener('hidden.bs.modal', () => {
        editable.value = {}
      })
    })

    return {
      editable,
      account: computed(() => AppState.account),
      activeRecipe: computed(() => AppState.activeRecipe),
      ingredients: computed(() => AppState.ingredients),

      async createIngredient() {
        try {
          editable.value.recipeId = AppState.activeRecipe.id
          await ingredientsService.createIngredient(editable.value)
          editable.value = {}
        } catch (error) {
          Pop.error(error.message, '[CREATING INGREDIENT]')
        }
      },

      async removeIngredient(ingredientId) {
        try {
          const isSure = await Pop.confirm('Are you sure you want to remove this ingredient?')

          if (!isSure) {
            return
          }
          await ingredientsService.removeIngredient(ingredientId)
        } catch (error) {
          Pop.error(error.message, '[DELETING INGREDIENT]')
        }
      }
    }
  }
}
</script>

<style lang="scss" scoped>
  img {
    object-fit: cover;
    object-position: center;
  }

  .title {
    border-radius: 5px 5px 0px 0px;
    background: var(--all-green, #527360);
  }

  .food-card {
    border-radius: 5px;
    background: #F0F4F2;
    box-shadow: 0px 4px 4px 0px rgba(0, 0, 0, 0.25);
  }

  @media screen and (min-width: 960px) {
    img {
      height: 90vh;
    }
  }
</style>