<template>
  <header class="container-fluid px-0">
    <Navbar />
  </header>

  <main class="container-fluid">
    <router-view />
  </main>

  <div v-if="appState.account.id" @click="isEditing()" class="d-flex justify-content-center align-items-center add-position selectable" data-bs-toggle="modal" data-bs-target="#recipeForm">
    <i class="mdi mdi-plus fs-1"></i>
  </div>

  <ModalComponent id="recipeDetails" class="modal-xl">
    <RecipeDetails />
  </ModalComponent>

  <ModalComponent id="recipeForm">
    <RecipeForm />
  </ModalComponent>
</template>

<script>
import { computed } from 'vue'
import { AppState } from './AppState'
import Navbar from './components/Navbar.vue'
import ModalComponent from './components/ModalComponent.vue'
import RecipeDetails from './components/RecipeDetails.vue'

export default {
  setup() {
    return {
      appState: computed(() => AppState),

      isEditing() {
        AppState.isEditing = false
      }
    }
  },
  components: { Navbar, ModalComponent, RecipeDetails }
}
</script>

<style lang="scss">
  @import "./assets/scss/main.scss";

  :root {
    --main-height: calc(100vh - 32px - 64px);
  }

  body {
    color: white;
    font-family: 'Sahitya', serif;
  }

  p {
    margin-bottom: 0;
  }

  .text-green {
    color: #219653;
  }

  .category-bg {
    border-radius: 20px
  }

  .category-bg,
  .text-bg {
    border: 0.5px solid #BBB;
    background: rgba(126, 126, 126, 0.60);
    backdrop-filter: blur(10px);
  }

  .add-position {
    position: fixed;
    bottom: 20px;
    right: 20px;
    height: 60px;
    width: 60px;
    background: #527360;
    border-radius: 50%;
  }

  .title {
    border-radius: 5px 5px 0px 0px;
    background: #527360;
  }
</style>