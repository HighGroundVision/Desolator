<template>
  <div v-if="loading">
    <slot name="loading">
      <div class="w3-row w3-padding w3-border-orange w3-leftbar">
        <div class="w3-col s2">
          <h1>Loading</h1>
        </div>
        <div class="w3-col s10 w3-center">
          <i class="fas fa-spinner fa-spin" style="font-size:5em; color: #25a2c3;"></i>
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
  }
}
</script>

<style>
</style>