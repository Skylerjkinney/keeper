<template>
    <div class="container bg-img" v-if="activeVault">
        <div class="container m-2">
            <div class="masonry">
                <div class="my-2" v-for="keep in keeps">
                    <KeepCard :keep="keep" />
                </div>
            </div>
        </div>
    </div>
</template>


<script>
import { AppState } from '../AppState';
import { computed, ref, onMounted } from 'vue';
import { vaultsService } from '../services/VaultService';
import Pop from '../utils/Pop';
import { useRoute } from 'vue-router';
import { router } from '../router';
import KeepCard from '../components/KeepCard.vue';

export default {
    setup() {
        const route = useRoute();
        const watchableVaultId = computed(() => route.params.vaultId);
        onMounted(() => {
            getVaultKeeps()
            getVault()
        })
        async function getVaultKeeps() {
            try {
                AppState.keeps = [],
                    await vaultsService.getVaultKeeps(route.params.vaultId)
            } catch (error) {
                Pop.error(error)
                router.push({ name: 'Home' })
            }
        }
        async function getVault() {
            try {
                await vaultsService.getVaultById(watchableVaultId.value)
            } catch (error) {
                Pop.error(error)
                router.push({ name: 'Home' })
            }
        }
        return {
            activeVault: computed(() => AppState.activeVault),
            keeps: computed(() => AppState.keeps),
            bgImg: computed(() => `url(${AppState.activeVault?.img})`)
        }
    },
    components: { KeepCard }
};
</script>


<style lang="scss" scoped>
.masonry {
    columns: 250px;
}

.bg-img {
    background-position: center;
    background-size: cover;
    background-attachment: fixed;
    background-image: v-bind(bgImg);
    border-radius: 5%;
    height: 100dvh;
    width: 100dvh;
}
</style>