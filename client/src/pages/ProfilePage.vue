<template>
    <div v-if="profile" class="container-fluid">
        <section class="row text-center">
            <h1>{{ profile.name }}</h1>
            <h2>{{ profile.class }}</h2>
            <p>{{ profile.bio }}</p>
        </section>
    </div>
</template>

<script>
import { AppState } from '../AppState';
import { computed, ref, onMounted, watch } from 'vue';
import { useRoute } from 'vue-router';
import Pop from '../utils/Pop';
import { logger } from '../utils/Logger';
import { profileService } from '../services/ProfileService.js'
export default {
    setup() {
        const route = useRoute()
        const watchableProfileId = computed(() => route.params.profileId)
        async function getProfileById() {
            try {
                const profileId = route.params.profileId;
                await profileService.getProfileById(profileId)
            } catch (error) {
                Pop.error(error)
            }
        }
        watch(
            watchableProfileId, () => {
                logger.log(route);
                getProfileById();
            },
            { immediate: true }
        )
        return {
            profile: computed(() => AppState.activeProfile),
        }
    }
};
</script>


<style lang="scss" scoped></style>