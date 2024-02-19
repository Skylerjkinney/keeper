<template>
    <div @click="setActiveKeep(keep.id)" data-bs-toggle="modal" data-bs-target="#keep-details-modal" class="container">
        <img :src="keep.img" :alt="keep.name" style="width: 100%;" :title="keep.name">
        <div class="bottom-left">{{ keep.name }}</div>
        <div class="bottom-right">
            <img class="img-fluid rounded" :src="keep.creator.picture" :alt="keep.creator.name" :title="keep.creator.name">
        </div>
    </div>
</template>


<script>
import { Keep } from '../models/Keep';
import { keepsService } from '../services/KeepService';
export default {
    props: { keep: { type: Keep, required: true } },
    setup() {
        return {
            setActiveKeep(keepId) {
                keepsService.setActiveKeep(keepId)
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