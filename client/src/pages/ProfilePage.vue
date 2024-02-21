<template>
    <div v-if="profile" class="container-fluid">
        <section class="row text-center">
            <h1>{{ profile.name }}</h1>
            <img :src="profile.picture" :alt="profile.name">
            <img :src="profile.coverImg" :alt="profile.name">
        </section>
    </div>
    <section class="row">
        <div class="my-2" v-for="keep in keeps" :key="profile.i">
            <KeepCard :keep="keep" />
        </div>
    </section>
</template>

<script>
import { AppState } from '../AppState';
import { computed, ref, onMounted, watch } from 'vue';
import { useRoute } from 'vue-router';
import Pop from '../utils/Pop';
import { logger } from '../utils/Logger';
import { profileService } from '../services/ProfileService.js'
import KeepCard from '../components/KeepCard.vue';
export default {
    setup() {
        const route = useRoute();
        const watchableProfileId = computed(() => route.params.profileId);
        async function getProfileById() {
            try {
                const profileId = route.params.profileId;
                await profileService.getProfileById(profileId);
            }
            catch (error) {
                Pop.error(error);
            }
        }
        watch(watchableProfileId, () => {
            logger.log(route);
            getProfileById();
        }, { immediate: true });
        return {
            profile: computed(() => AppState.activeProfile),
            keeps: computed(() => AppState.keeps)
        };
    },
    components: { KeepCard }
};
</script>


<style lang="scss" scoped></style>