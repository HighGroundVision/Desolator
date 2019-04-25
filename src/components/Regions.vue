<template>
  <b-form>
    <b-form-group label="Selected Region">
      <b-form-select v-model="selection" :options="regions" @change="onChanged">
      </b-form-select>
    </b-form-group>
  </b-form>
</template>

<script>
import regionData from '@/assets/data/regions.js'

export default {
  name: 'Regions',
  data () {
    var regions = [];
    for (const key in regionData) {
      if (regionData.hasOwnProperty(key)) {
        const element = regionData[key];
        regions.push({value: key, text: element })
      }
    }

    return {
      'selection': '2',
      'regions': regions
    }
  },
   methods: {
    onChanged() {
      this.$emit("change", this.selection);
    }
  }
}
</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped>
</style>
