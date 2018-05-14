<template>
  <b-row>
    <b-col cols="12">
      <p>
        <span>* Why is data missing?</span>
        <button class="btn btn-link" v-on:click="more = !more">More</button>
        <br />
        <span v-if="more">This is because these triplets did not have high enough pick rate to be included in the results.</span>
      </p>
    </b-col>
  </b-row>
</template>

<script>

export default {
  name: 'MissingDataWarning',
  props: {
    open: {
      type: Boolean,
      default: false
    }
  },
  data () {
    return {
      'more': this.open
    }
  },
  methods: {
  }
}
</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped>
</style>
