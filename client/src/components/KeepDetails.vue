<template>
    <section class="row">
        <div v-if="activeKeep" class="col-6">
            <img class="m-2 img-fluid rounded" :src="activeKeep.img" :alt="activeKeep.name">
        </div>
        <div v-if="activeKeep" class="col-6 text-center">
            <span><i class="mdi mdi-eye"></i> {{ activeKeep.views }}</span>
            <span><i class="mdi mdi-lock">{{ activeKeep.kept }}</i></span>
            <h1>{{ activeKeep.name }}</h1>
            <p>{{ activeKeep.description }}</p>
        </div>
    </section>
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


<style lang="scss" scoped>
img {
    height: 97%;
    width: 80%;
}
</style>