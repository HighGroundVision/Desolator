<template>
  <section v-if="ready" >
    <b-row>
      <b-col>
        <h2 class="text-warning">Drafting</h2>
      </b-col>
    </b-row>
  </section>
  <section v-else class="text-center">
    <hgv-loader :color="'#ffc107'"></hgv-loader>
  </section>
</template>

<script>
// import axios from 'axios'

export default {
  name: 'DraftLive',
  data () {
    return {
      'ready': false
    }
  },
  created: function () {
    
  }
}
</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped>
</style>
