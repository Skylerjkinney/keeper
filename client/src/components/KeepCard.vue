<template>
    <div class="container">
        <img :src="keep.img" alt="keep.name" style="width: 100%;" :title="keep.name">
        <div class="bottom-left">🏠</div>
        <div class="bottom-right">🚓</div>
    </div>
</template>


<script>
import { Keep } from '../models/Keep';
export default {
    props: { keep: { type: Keep, required: true } },
    setup() {
        return {}
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
}
</style>