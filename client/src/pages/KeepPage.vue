<template>
  <div class="container">
    <div class="masonry">
      <div class="my-2" v-for="keep in keeps">
        <KeepCard :keep="keep" />
      </div>
    </div>
  </div>
</template>

<script>
import Navbar from '../components/Navbar.vue'
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
  components: { Login, KeepCard, Navbar }
}
</script>

<style lang="scss" scoped>
.masonry {
  columns: 250px;
}
</style>
