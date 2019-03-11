<template>
  <b-row>
    <b-col>
      <h4>{{hero.summary.name}}</h4>
    </b-col>
    <b-col cols="4" lg="3">
      <div class="text-center">
        <b-alert variant="info" show>   
          <p>
            Every hero is unique, like a snowflake.
          </p>
        </b-alert>
        <img src="@/assets/imgs/cluckles-speach.png" class="cluckles-speach" />
      </div>
    </b-col>
  </b-row>
</template>

<script>
import numeral from 'numeral'
import heroes from '@/assets/data/hero-details.json'

export default {
  name: 'hero',
  data () {
    return {
      'hero': heroes[this.$route.params.id]
    }
  },
  methods: {
    formatPercentage(value) {
      return numeral(value).format('0%');
    }
  },
  watch:{
    $route (to, from){
      this.hero = heroes[to.params.id];
    }
  } 
}
</script>