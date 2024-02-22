<template>
    <form @submit.prevent="createKeep()" class="row my-3 text-center">
        <div class="col-11 my-2 ms-5">
            <img v-if="keepData.img" class="img-fluid" :src="keepData.img" alt="preview">
            <div v-else class="place-holder">Preview</div>
        </div>
        <div class="col-12 my-2">
            <label for="insert-image">Image</label>
            <input v-model="keepData.img" class="form-control" type="url" required maxlength="255" name="insert-image"
                id="insert-image">
        </div>
        <div class="col-12 my-2">
            <label for="create-keep-name">Name</label>
            <input v-model="keepData.name" class="form-control" type="text" minlength="5" required maxlength="100"
                name="create-keep-name" id="create-keep-name">
        </div>
        <div class="col-12 my-2">
            <label for="create-keep-description">Description</label>
            <input v-model="keepData.description" class="form-control" type="text" minlength="25" required maxlength="1000">
        </div>
        <div class="col-12 text-start m-2">
            <button class="btn btn-primary" type="submit">Create Keep</button>
        </div>
    </form>
</template>


<script>
import { AppState } from '../AppState';
import { computed, ref, onMounted } from 'vue';
import { keepsService } from '../services/KeepService';
import { logger } from '../utils/Logger';
import { Modal } from 'bootstrap';
import Pop from '../utils/Pop';
export default {
    setup() {
        const keepData = ref({})
        return {
            keepData,
            async createKeep() {
                try {
                    logger.log('Making Keep', keepData.value)
                    await keepsService.createKeep(keepData.value)
                    Modal.getOrCreateInstance("#keep-form-modal").hide()
                    Pop.success('Keep fresh out of oven 👍')
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