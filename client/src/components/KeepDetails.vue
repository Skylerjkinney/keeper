<template>
    <div v-if="activeKeep" class="text-center text-light">
        <h1>{{ activeKeep.name }}</h1>
        <h3>{{ activeKeep.description }}</h3>
    </div>
</template>


<script>
import { AppState } from '../AppState';
import { computed, ref, onMounted } from 'vue';
import { Keep } from '../models/Keep';

export default {

    setup() {
        return {
            activeKeep: computed(() => AppState.activeKeep),
        }
    }
};
</script>


<style lang="scss" scoped></style>