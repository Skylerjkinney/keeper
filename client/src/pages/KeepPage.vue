<template>
  <header class="justify-content-center">
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
import { onMounted } from 'vue';
import KeepCard from '../components/KeepCard.vue';
import Login from '../components/Login.vue';
import Pop from '../utils/Pop';
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

    }
  },
  components: { Login, KeepCard }
}
</script>
