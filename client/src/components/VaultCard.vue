<template>
    <div @click="getVaultById(vault.id)" class="container">
        <img class="rounded" :src="vault.img" :alt="vault.name" style="width: 100%;" :title="vault.name">
        <div v-if="vault.isPrivate">
            <i class="mdi mdi-lock top-left"></i>
        </div>
        <div v-else>
            <i class="mdi mdi-key top-right"></i>
        </div>
        <div class="bottom-left fw-2">{{ vault.name }}</div>
        <div class="bottom-right">
            <img class="img-fluid rounded" :src="vault.creator.picture" :alt="vault.creator.name"
                :title="vault.creator.name">
            <div>
                <button @click="deleteVault(vault.id)" v-if="account && account.id == vault.creatorId"
                    class="btn btn-danger border shadow border-light text-dark mb-2"><i class="mdi mdi-delete"></i></button>
            </div>
        </div>
    </div>
</template>


<script>
import { vaultsService } from '../services/VaultService'
import { computed } from 'vue';
import { Vault } from '../models/Vault';
import { AppState } from '../AppState';
import Pop from '../utils/Pop';
import { router } from '../router'
export default {
    props: { vault: { type: Vault, required: true } },
    setup() {
        return {
            account: computed(() => AppState.account),
            async deleteVault(vaultId) {
                try {
                    if (await Pop.confirm('This vault will be discarded indefinitely') == true) {
                        await vaultsService.deleteVault(vaultId)
                        Pop.success('It is done')
                    }
                } catch (error) {
                    Pop.error(error)
                }
            },
            async getVaultById(vaultId) {
                try {
                    await vaultsService.getVaultById(vaultId)
                    router.push({ name: 'Vault', params: { vaultId: vaultId } })
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

.top-left {
    color: red;
    position: absolute;
    top: 8px;
    left: 16px;
    text-shadow: 0px 0px 10px rgb(84, 7, 7);
}

.top-right {
    color: rgb(5, 96, 22);
    position: absolute;
    top: 8px;
    right: 16px;
    text-shadow: 0px 0px 10px rgb(8, 123, 58);
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