<template>
  <div v-if="loading">
    <slot name="loading">
      <div class="w3-row w3-padding w3-border-orange w3-leftbar">
        <div class="w3-col s2">
          <b v-if="small" style="font-size:1.5em; color: #25a2c3;">Loading</b>
          <b v-else style="font-size:3em; color: #25a2c3;">Loading</b>
        </div>
        <div class="w3-col s10 w3-center">
          <i v-if="small" class="fas fa-spinner fa-spin" style="font-size:2.5em; color: #25a2c3;"></i>
          <i v-else class="fas fa-spinner fa-spin" style="font-size:5em; color: #25a2c3;"></i>
        </div>
      </div>
    </slot>
  </div>
  <div v-else>
     <slot></slot>
  </div>
</template>
  
<script>
export default {
 name: 'loader',
  props: {
    loading: Boolean,
    small: {
      type: Boolean,
      default: false,
    }
  }
}
</script>

<style>
</style>