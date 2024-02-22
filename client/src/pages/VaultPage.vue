<template>
    <div class="container">
        <h1>HEY BRO UR AT THIS VAULTS PAGE</h1>


    </div>
</template>


<script>
import { AppState } from '../AppState';
import { computed, ref, onMounted } from 'vue';
export default {
    setup() {
        return {
            activeVault: computed(() => AppState.activeVault)
        }
    }
};
</script>


<style lang="scss" scoped></style>