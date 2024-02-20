<template>
  <img :src="account.coverImg" :alt="account.name" class="cover-img">
  <section class="">
    <div class="container-fluid centered">
      <section class="row justify-content-center">
        <div class="col-12 text-center list-card d-flex flex-column align-items-center w-50 mt-4">
          <div class="about text-center text-info ">
            <h1>Welcome {{ account.name }}</h1>
            <img class="rounded bg-img" :src="account.picture" :alt="account.name" />
            <h3>{{ account.email }}</h3>
          </div>
          <form @submit.prevent="updateAccount()">
            <div class="mb-3">
              <label for="name">Name</label>
              <input v-model="update.name" class="form-control" id="name" type="text" required>
            </div>
            <div class="mb-3">
              <label for="picture">Picture</label>
              <input v-model="update.picture" class="form-control" id="picture" type="url" required>
            </div>
            <div class="mb-3">
              <label for="cover">Cover Image</label>
              <input v-model="update.coverImg" class="form-control" id="cover" type="url">
            </div>
            <div class="text-center mb-3">
              <button class="btn btn-outline-success" type="submit">Update</button>
            </div>
          </form>
          <router-link :to="{ name: 'Home' }">
            <Button class="btn btn-outline-success">HOME</Button>
          </router-link>
        </div>
      </section>
    </div>
  </section>
</template>

<script>
import { computed, watch, ref } from 'vue';
import { AppState } from '../AppState';
import { accountService } from '../services/AccountService';
import Pop from '../utils/Pop';
export default {
  setup() {
    const update = ref({})
    const account = computed(() => AppState.account)
    watch(
      account,
      () => {
        update.value = { ...AppState.account }
      },
      { immediate: true }
    )
    return {
      update,
      account,
      async updateAccount() {
        try {
          const accountData = update.value
          await accountService.updateAccount(accountData)
          Pop.success('Updated Account!')
        } catch (error) {
          Pop.error(error)
        }
      }
    }
  }
}
</script>

<style scoped>
label {
  color: #bc0c0c;
}

.list-card {
  outline: solid 2px #bc0c0c;
  border-radius: 10px;
  padding: 60px;
  background-color: #212529;
  box-shadow: 5px 8px 5px rgba(0, 0, 0, 0.54);
}

.bg-img {
  height: 80px;
  width: 80px;
  object-fit: contain;
  object-position: center;
}

.cover-img {
  height: 100dvh;
  width: 100dvw;
  object-position: center;
  object-fit: cover;
  position: fixed;
}

.container {
  position: relative;
  text-align: center;
  color: white;
}

.centered {
  position: absolute;
  top: 50%;
  left: 50%;
  transform: translate(-50%, -50%);
}
</style>
