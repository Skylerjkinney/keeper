<template>
    <div class="container" v-if="activeVault">
        <img :src="activeVault.img" :alt="activeVault.name">
    </div>
    <div class="container">
        <div class="masonry">
            <div class="my-2" v-for="keep in keeps">
                <KeepCard :keep="keep" />
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

        }
    }
};
</script>


<style lang="scss" scoped>
.masonry {
    columns: 250px;
}
</style>