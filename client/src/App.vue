<template>
  <main>
    <ModalWrapper modalId="keep-details-modal">
      <KeepDetails />
    </ModalWrapper>
    <router-view />
  </main>
</template>

<script>
import ModalWrapper from './components/ModalWrapper.vue'
import { computed } from 'vue'
import { AppState } from './AppState'
import KeepDetails from './components/KeepDetails.vue'


export default {
  setup() {
    return {
      appState: computed(() => AppState)
    }
  },
  components: { ModalWrapper, KeepDetails }
}
</script>
<style lang="scss">
@import "./assets/scss/main.scss";

:root {
  --main-height: calc(100vh - 32px - 64px);
}


footer {
  display: grid;
  place-content: center;
  height: 32px;
}
</style>
