<template>
    <div @click="getKeepById(keep.id)" data-bs-toggle="modal" data-bs-target="#keep-details-modal" class="container">
        <img class="rounded" :src="keep.img" :alt="keep.name" style="width: 100%;" :title="keep.name">
        <div class="bottom-left fw-2">{{ keep.name }}</div>
        <div class="bottom-right">
            <img class="img-fluid rounded" :src="keep.creator.picture" :alt="keep.creator.name" :title="keep.creator.name">
            <div>
                <button @click="deleteKeep(keep.id)" v-if="account && account.id == keep.creatorId"
                    class="btn btn-danger border shadow border-light text-dark mb-2"><i class="mdi mdi-delete"></i></button>
                <button @click="removeKeep(keep.vaultKeepId)"
                    v-if="activeVault?.creatorId && account?.id == activeVault?.creatorId"
                    class="btn btn-warning border shadow border-dark text-danger mb-2"><i
                        class="mdi mdi-file-remove"></i></button>
            </div>
        </div>
    </div>
</template>


<script>
import { AppState } from '../AppState';
import { computed } from 'vue';
import { Keep } from '../models/Keep';
import { keepsService } from '../services/KeepService';
import Pop from '../utils/Pop';
import { Modal } from 'bootstrap';
import { accountService } from '../services/AccountService';
import { router } from '../router';
import { vaultKeepService } from '../services/VaultKeepService';
export default {
    props: { keep: { type: Keep, required: true } },
    setup() {
        return {
            account: computed(() => AppState.account),
            activeVault: computed(() => AppState.activeVault),
            async deleteKeep(keepId) {
                try {
                    if (await Pop.confirm('This keep will be discarded indefinitely') == true) {
                        Modal.getOrCreateInstance("#keep-details-modal").hide()
                        await keepsService.deleteKeep(keepId)
                        Pop.success('It is done')
                    }
                } catch (error) {
                    Pop.error(error)
                }
            },
            async removeKeep(vaultKeepId) {
                try {
                    if (await Pop.confirm('Remove this keep from your vault?') == true) {
                        await vaultKeepService.deleteVaultKeep(vaultKeepId)
                        Modal.getOrCreateInstance("#keep-details-modal").hide()
                    } Pop.success('Keep removed from vault')
                } catch (error) {
                    Pop.error(error)
                    router.push({ name: 'Home' })
                }
            },
            async getKeepById(keepId) {
                try {
                    await keepsService.getKeepById(keepId)
                    await accountService.getMyVaults()
                } catch (error) {
                    Pop.error(error)
                }
            }
        }
    }
};
</script>


<style lang="scss" scoped>
.container {
    position: relative;
    text-align: center;
    color: white;
}

.bottom-left {
    position: absolute;
    bottom: 8px;
    left: 16px;
    text-shadow: 0px 0px 10px black;
}

.bottom-right {
    position: absolute;
    bottom: 8px;
    right: 16px;

    >img {
        height: 30px;
        width: 30px;
    }
}
</style>