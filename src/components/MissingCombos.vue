<template>
  <b-row>
    <b-col cols="12">
      <p>
        <span>* Why are there no results?</span>
        <button class="btn btn-link" v-on:click="more = !more">More</button>
        <br />
        <span v-if="more">This is because this combo has the highest win rate. There is nothing better!</span>
      </p>
    </b-col>
  </b-row>
</template>

<script>

export default {
  name: 'MissingCombos',
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
