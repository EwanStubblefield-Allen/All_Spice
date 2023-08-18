<template>
  <section class="row">
    <div class="col-12 title">
      <p class="fs-1">New Recipe</p>
    </div>

    <div class="col-12">
      <form @submit.prevent="handleSubmit()" class="row text-dark py-3">
        <div class="col-7 form-group">
          <label for="title" class="px-2 fs-5">Title</label>
          <input v-model="editable.title" id="title" class="form-control" type="text" placeholder="Title..." required>
        </div>

        <div class="col-5 form-group">
          <label for="category" class="px-2 fs-5">Category</label>
          <select v-model="editable.category" id="category" class="form-select" required>
            <option selected>Category...</option>
            <option v-for="c in category" :key="c" :value="c">{{ c }}</option>
          </select>
        </div>

        <div class="col-12 form-group py-2">
          <label for="instructions" class="px-2 fs-5">Instructions</label>
          <input v-model="editable.instructions" id="instructions" class="form-control" type="text" placeholder="Instructions..." required>
        </div>

        <div class="col-12 form-group pb-2">
          <label for="img" class="px-2 fs-5">Image</label>
          <input v-model="editable.img" id="img" class="form-control" type="url" placeholder="Url..." required>
        </div>

        <div class="text-end pt-3">
          <button @click="resetEditable()" class="btn mx-3" type="reset" data-bs-dismiss="modal">Cancel</button>
          <button class="btn btn-success" type="submit">Submit</button>
        </div>
      </form>
    </div>
  </section>
</template>

<script>
import { ref, watchEffect } from 'vue'
import { AppState } from '../AppState.js'
import { recipesService } from '../services/RecipesService.js'
import { Modal } from 'bootstrap'
import Pop from '../utils/Pop.js'

export default {
  setup() {
    watchEffect(() => {
      if (AppState.isEditing) {
        editable.value = { ...AppState.activeRecipe }
      }
    })

    const editable = ref({})
    const category = ['Soup', 'Pasta', 'Fish', 'Salads', 'Beef', 'Burgers', 'Cheese', 'Italian', 'Mexican', 'Specialty Coffee']

    async function createRecipe() {
      try {
        recipesService.createRecipe(editable.value)
      } catch (error) {
        Pop.error(error.message, '[CREATING RECIPE]')
      }
    }

    async function updateRecipe() {
      try {
        recipesService.updateRecipe(editable.value)
      } catch (error) {
        Pop.error(error.message, '[UPDATING RECIPE]')
      }
    }

    return {
      editable,
      category,

      resetEditable() {
        editable.value = {}
      },

      handleSubmit() {
        if (editable.value.category == 'Category...') {
          return
        }

        if (AppState.isEditing) {
          updateRecipe()
        } else {
          createRecipe()
        }
        this.resetEditable()
        Modal.getOrCreateInstance('#recipeForm').hide()
      }
    }
  }
}
</script>

<style lang="scss" scoped></style>