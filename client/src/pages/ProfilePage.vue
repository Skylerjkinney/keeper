<template>
    <header class="mb-1">
        <Navbar />
    </header>
    <div class="container-fluid bg-img">
        <div v-if="profile" class="text-center my-2">
            <h1>{{ profile.name }}</h1>
            <img class="profile-img" :src="profile.picture" :alt="profile.name">
            <h2 class="text-center my-2">Vaults</h2>
        </div>
    </div>
    <div class="container">
        <div class="masonry">
            <div class="my-2" v-for="vault in vaults">
                <VaultCard :vault="vault" />
            </div>
        </div>
    </div>
    <section class="my-5">
        <h3 class="text-center my-2">Keeps</h3>
    </section>
    <div class="container">
        <div class="masonry">
            <div class="my-2" v-for="keep in keeps">
                <KeepCard :keep="keep" />
            </div>
        </div>
    </div>
</template>

<script>
import Navbar from '../components/Navbar.vue';
import { AppState } from '../AppState';
import { computed, ref, onMounted, watch } from 'vue';
import { useRoute } from 'vue-router';
import Pop from '../utils/Pop';
import { logger } from '../utils/Logger';
import { profileService } from '../services/ProfileService.js'
import KeepCard from '../components/KeepCard.vue';
import VaultCard from '../components/VaultCard.vue';
export default {
    setup() {
        const route = useRoute();
        const watchableProfileId = computed(() => route.params.profileId);
        onMounted(() => {
            getProfileKeeps()
            getProfileVaults()
        })
        async function getProfileKeeps() {
            try {
                AppState.keeps = [],
                    await profileService.getProfileKeeps(route.params.profileId)
            } catch (error) {
                Pop.error(error)
            }
        }
        async function getProfileVaults() {
            try {
                AppState.vaults = [],
                    await profileService.getProfileVaults(route.params.profileId)
            } catch (error) {
                Pop.error(error)
            }
        }
        async function getProfileById() {
            try {
                const profileId = route.params.profileId;
                logger.log(profileId, "getting profile")
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
            keeps: computed(() => AppState.keeps),
            vaults: computed(() => AppState.vaults),
            bgImg: computed(() => `url(${AppState.activeProfile?.coverImg})`)
        };
    },
    components: { KeepCard, VaultCard }
};
</script>


<style lang="scss" scoped>
.bg-img {
    background-position: center;
    background-size: cover;
    background-attachment: fixed;
    background-image: v-bind(bgImg);
}

.profile-img {
    height: 80px;
    width: 80px;
    object-fit: contain;
    object-position: center;
    border-radius: 50%;
}

.masonry {
    columns: 250px;
}

h1 {
    font-size: 72px;
    background: -webkit-linear-gradient(#0c0756, #da0f3b);
    -webkit-background-clip: text;
    -webkit-text-fill-color: transparent;
}

h2 {
    font-size: 72px;
    background: -webkit-linear-gradient(#034f3e, #d2731a);
    -webkit-background-clip: text;
    -webkit-text-fill-color: transparent;
}

h3 {
    font-size: 72px;
    background: -webkit-linear-gradient(#114b5c, #ffe208);
    -webkit-background-clip: text;
    -webkit-text-fill-color: transparent;
}
</style>