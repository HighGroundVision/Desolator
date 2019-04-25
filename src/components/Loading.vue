<template>
  <section>
    <div v-if="loading" class="text-center">
      <div class="spinner-border" role="status">
        <span class="sr-only">Loading...</span>
      </div>
    </div>
    <div v-if="error">
      <div class="alert alert-danger" role="alert">
        Error fetching data.
      </div>
    </div>
    <div v-if="error == false && loading == false">
      <slot></slot>
    </div>
  </section>
</template>

<script>
import axios from 'axios'

export default {
  name: 'Loading',
  props: {
    urls: Array,
  },
  data () {
    return {
      'loading': true,
      'error': false
    }
  },
  async mounted() {
    try {
      var data = [];

      // Load Data
      for (const url of this.urls) {
        var response = await axios.get(url);
        data.push(response.data);
      }

      this.loading = false;
      this.$emit('loaded', data); // Emit Event
    } catch (err) {
      this.loading = false;
      this.error = true;
      console.log(err);
    }
  }
}
</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped>
</style>
