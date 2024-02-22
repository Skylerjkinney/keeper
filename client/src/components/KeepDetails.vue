<template>
    <section v-if="activeKeep" class="row">
        <div class="col-4">
            <img class="m-2 img-fluid rounded" :src="activeKeep.img" :alt="activeKeep.name">
        </div>
        <div class="col-8 text-center">
            <router-link :to="{ name: 'Profile', params: { profileId: activeKeep.creatorId } }">
                <img @click="dismissModal()" :title="`Look at ${activeKeep.creator.name}'s Vaults!`"
                    :src="activeKeep.creator.picture" :alt="activeKeep.creator.name" class="my-2 tiny-img">
            </router-link>
            <h1>{{ activeKeep.name }}</h1>
            <p>{{ activeKeep.description }}</p>
        </div>
        <div class="btn-group">
            <div class="mx-1"><i class="mdi mdi-eye"></i> {{ activeKeep.views }}</div>
            <div class="mx-1"><i class="mdi mdi-lock">{{ activeKeep.kept }}</i></div>
        </div>
        <div class="mb-3 col-6">
            <label for="vault-select">Select Vault</label>
            <select v-model="vaultData.vaultId" class="form-control" name="vault-select" required id="vault-select">
                <option v-for="vault in vaults" :key="vault.id" :value="vault.id">{{ vault.name }}</option>
            </select>
        </div>
    </section>
</template>


<script>
import { AppState } from '../AppState';
import { computed, ref, onMounted } from 'vue';
import { Keep } from '../models/Keep';
import { Modal } from 'bootstrap';
import Pop from '../utils/Pop';
import { vaultKeepService } from '../services/VaultKeepService.js'
export default {

    setup() {
        const vaultData = ref({})
        return {
            vaultData,
            activeKeep: computed(() => AppState.activeKeep),
            vaults: computed(() => AppState.vaults),
            selectedVaults: computed(() => AppState.vaults.find(vault => vault.id == vaultData.value.vaultId)),
            async vaultKeep() {
                try {
                    await vaultKeepService.createVaultKeep(vaultData.value)
                    this.dismissModal()
                    vaultData.value = {}
                    Pop.success("Vault is Keeped")
                } catch (error) {
                    Pop.error(error)
                }
            },
            dismissModal() {
                Modal.getOrCreateInstance("#keep-details-modal").hide()
            }
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