<template>
  <section class="opaque-background">
    <div class="row">
        <div class="col-6">
            <h2>Melee</h2>
            <div class="row">
                <div class="col-4">
                    Pick Rate
                </div>
                <div class="col-6">
                    <b-progress :value="round(melee.pickVsTotal)" :min="0" :max="100"></b-progress>
                </div>
                <div class="col-2">
                    {{round(melee.pickVsTotal)}} %
                </div>
            </div>
            <div class="row">
                <div class="col-4">
                    Win Rate (Total)
                </div>
                <div class="col-6">
                    <b-progress :value="round(melee.winsVsTotal)" :min="0" :max="100"></b-progress>
                </div>
                <div class="col-2">
                    {{round(melee.winsVsTotal)}} %
                </div>
            </div>
            <div class="row">
                <div class="col-4">
                    Win Rate (Picks)
                </div>
                <div class="col-6">
                    <b-progress :value="round(melee.winsVsPicks)" :min="0" :max="100"></b-progress>
                </div>
                <div class="col-2">
                    {{round(melee.winsVsPicks)}} %
                </div>
            </div>

        </div>
        <div class="col-6">
            <h2>Range</h2>
            <div class="row">
                <div class="col-4">
                    Pick Rate
                </div>
                <div class="col-6">
                    <b-progress :value="round(range.pickVsTotal)" :min="0" :max="100"></b-progress>
                </div>
                <div class="col-2">
                    {{round(range.pickVsTotal)}} %
                </div>
            </div>
            <div class="row">
                <div class="col-4">
                    Win Rate (Total)
                </div>
                <div class="col-6">
                    <b-progress :value="round(range.winsVsTotal)" :min="0" :max="100"></b-progress>
                </div>
                <div class="col-2">
                    {{round(range.winsVsTotal)}} %
                </div>
            </div>
            <div class="row">
                <div class="col-4">
                    Win Rate (Picks)
                </div>
                <div class="col-6">
                    <b-progress :value="round(range.winsVsPicks)" :min="0" :max="100"></b-progress>
                </div>
                <div class="col-2">
                    {{round(range.winsVsPicks)}} %
                </div>
            </div>

        </div>
    </div>
  </section>
</template>

<script>
import pool from '../data/draftpool.json'

export default {
  name: 'AbilityStats',
  created () {
    console.log(pool)

    var url = 'http://localhost:7071/api/RequestStats?key=' + this.$route.params.key
    this.$http.get(url).then((response) => {
      if (response.ok) {
        var data = response.body
        this.melee = data.melee
        this.range = data.range
      }
    })
  },
  data () {
    return {
      'melee': {},
      'range': {}
    }
  },
  methods: {
    round: function (stat) {
      return Math.round(stat * 100)
    }
  }
}
</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped>
</style>
