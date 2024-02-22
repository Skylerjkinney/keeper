<template>
    <form @submit.prevent="createVault()" class="row my-3 text-center">
        <div class="col-11 my-2 ms-5">
            <img v-if="vaultData.img" class="img-fluid" :src="vaultData.img" alt="preview">
            <div v-else class="place-holder">Preview</div>
        </div>
        <div class="col-12 my-2">
            <label for="insert-image">Image</label>
            <input v-model="vaultData.img" class="form-control" type="url" required maxlength="255" name="insert-image"
                id="insert-image">
        </div>
        <div class="col-12 my-2">
            <label for="create-vault-name">Name</label>
            <input v-model="vaultData.name" class="form-control" type="text" minlength="5" required maxlength="100"
                name="create-vault-name" id="create-vault-name">
        </div>
        <div class="col-12 my-2">
            <label for="create-vault-description">Description</label>
            <input v-model="vaultData.description" class="form-control" type="text" minlength="25" required
                maxlength="1000">
        </div>
        <div>
            <label for="create-vault-private">Private?</label>
            <input v-model="vaultData.isPrivate" type="checkbox" name="create-vault-private" id="create-vault-private">
        </div>
        <div class="col-12 text-start m-2">
            <button class="btn btn-primary" type="submit">Create vault</button>
        </div>
    </form>
</template>


<script>
import { AppState } from '../AppState';
import { computed, ref, onMounted } from 'vue';
import { vaultsService } from '../services/VaultService';
import { logger } from '../utils/Logger';
import { Modal } from 'bootstrap';
import Pop from '../utils/Pop';
export default {
    setup() {
        const vaultData = ref({})
        return {
            vaultData,
            async createVault() {
                try {
                    logger.log('Making vault', vaultData.value)
                    await vaultsService.createVault(vaultData.value)
                    Modal.getOrCreateInstance("#vault-form-modal").hide()
                    Pop.success('vault fresh out of oven 👍')
                } catch (error) {
                    Pop.error(error)
                }
            }
        }
    }
};
</script>


<style lang="scss" scoped>
.place-holder {
    height: 10vh;
    border: 2px var(--bs-info) dashed;
}
</style>