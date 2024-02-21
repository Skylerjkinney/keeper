<template>
    <section v-if="activeKeep" class="row">
        <div class="col-4">
            <img class="m-2 img-fluid rounded" :src="activeKeep.img" :alt="activeKeep.name">
        </div>
        <div class="col-8 text-center">
            <router-link :to="{ name: 'Profile', params: { profileId: activeKeep.creatorId } }">
                <img :title="`Look at ${activeKeep.creator.name}'s Vaults!`" :src="activeKeep.creator.picture"
                    :alt="activeKeep.creator.name" class="my-2 tiny-img">
            </router-link>
            <div class="btn-group">
                <div class="mx-1"><i class="mdi mdi-eye"></i> {{ activeKeep.views }}</div>
                <div class="mx-1"><i class="mdi mdi-lock">{{ activeKeep.kept }}</i></div>
            </div>
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
    height: 50vh;
    width: 80vh;
    object-fit: cover;
    object-position: center;
}

.tiny-img {
    height: 30px;
    width: 30px;
}
</style>