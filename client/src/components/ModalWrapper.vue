<template>
    <div class="modal fade" :id="modalId" tabindex="-1" role="dialog" aria-labelledby="modalTitleId" aria-hidden="true">
        <div :class="`modal-dialog modal-sm modal-${modalSize}`" role="document">
            <div class="modal-content">
                <slot>

                </slot>
            </div>
        </div>
    </div>
</template>


<script>
export default {
    props: { modalId: { type: String, required: true }, modalSize: { type: String, default: 'xl' } },
    setup() {
        return {}
    }
}
</script>


<style lang="scss" scoped></style>