<template>
  <header class="mb-5">
    <Login />
  </header>
  <div class="container">
    <div class="row">
      <div class="col-3" v-for="keep in keeps">
        <KeepCard :keep="keep" />
      </div>
    </div>
  </div>
</template>

<script>
import { keepsService } from '../services/KeepService.js'
import { computed, onMounted } from 'vue';
import KeepCard from '../components/KeepCard.vue';
import Login from '../components/Login.vue';
import Pop from '../utils/Pop';
import { AppState } from '../AppState';
export default {
  setup() {
    onMounted(() => {
      getKeeps()
    })
    async function getKeeps() {
      try {
        await keepsService.getKeeps()
      } catch (error) {
        Pop.error(error)
      }
    }
    return {
      keeps: computed(() => AppState.keeps),
    }
  },
  components: { Login, KeepCard }
}
</script>
