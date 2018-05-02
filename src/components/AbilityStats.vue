<template>
  <section class="opaque-background">
    <div class="row">
        <div class="col-12">
            <h1>{{ability.dname}}</h1>
        </div>
    </div>
    <hr />
    <div class="row">
      <div class="col-2">
        <b-img :src="ability.img" :title="ability.dname" fluid class="ability-icon" />
      </div>
      <div class="col-10">
        {{ability.desc}}
      </div>
    </div>
    <hr />
    <div class="row">
        <div class="col-6">
            <h2>Melee Heroes</h2>
            <div class="row">
                <div class="col-4">
                    Picks
                </div>
                <div class="col-2">
                    {{melee.picks}}
                </div>
                <div class="col-6">
                </div>
            </div>
            <div class="row">
                <div class="col-4">
                    Pick Rate
                </div>
                <div class="col-2">
                    {{round(melee.pickVsTotal)}} %
                </div>
                <div class="col-6">
                    <b-progress :value="round(melee.pickVsTotal)" :min="0" :max="100"></b-progress>
                </div>
            </div>
            <div class="row">
                <div class="col-4">
                    Wins
                </div>
                <div class="col-2">
                    {{melee.wins}}
                </div>
                <div class="col-6">
                </div>
            </div>
            <div class="row">
                <div class="col-4">
                    Win Rate (Total)
                </div>
                <div class="col-2">
                    {{round(melee.winsVsTotal)}} %
                </div>
                <div class="col-6">
                    <b-progress :value="round(melee.winsVsTotal)" :min="0" :max="100"></b-progress>
                </div>
            </div>
            <div class="row">
                <div class="col-4">
                    Win Rate (Picks)
                </div>
                <div class="col-2">
                    {{round(melee.winsVsPicks)}} %
                </div>
                <div class="col-6">
                    <b-progress :value="round(melee.winsVsPicks)" :min="0" :max="100"></b-progress>
                </div>
            </div>
        </div>
        <div class="col-6">
            <h2>Range Heroes</h2>
            <div class="row">
                <div class="col-4">
                    Picks
                </div>
                <div class="col-2">
                    {{range.picks}}
                </div>
                <div class="col-6">
                </div>
            </div>
            <div class="row">
                <div class="col-4">
                    Pick Rate
                </div>
                <div class="col-2">
                    {{round(range.pickVsTotal)}} %
                </div>
                <div class="col-6">
                    <b-progress :value="round(range.pickVsTotal)" :min="0" :max="100"></b-progress>
                </div>
            </div>
            <div class="row">
                <div class="col-4">
                    Wins
                </div>
                <div class="col-2">
                    {{range.wins}}
                </div>
                <div class="col-6">
                </div>
            </div>
            <div class="row">
                <div class="col-4">
                    Win Rate (Total)
                </div>
                <div class="col-2">
                    {{round(range.winsVsTotal)}} %
                </div>
                <div class="col-6">
                    <b-progress :value="round(range.winsVsTotal)" :min="0" :max="100"></b-progress>
                </div>
            </div>
            <div class="row">
                <div class="col-4">
                    Win Rate (Picks)
                </div>
                <div class="col-2">
                    {{round(range.winsVsPicks)}} %
                </div>
                <div class="col-6">
                    <b-progress :value="round(range.winsVsPicks)" :min="0" :max="100"></b-progress>
                </div>
            </div>
        </div>
    </div>
  </section>
</template>

<script>
import abilities from '../data/abilities.json'

export default {
  name: 'AbilityStats',
  created () {
    var url = 'http://localhost:7071/api/RequestStats?key=' + this.$route.params.key
    this.$http.get(url).then((response) => {
      if (!response.ok) {
        return
      }

      var data = response.body
      this.ability = abilities[data.abilities[0]]
      this.melee = data.melee
      this.range = data.range
      this.loading = false
    })
  },
  data () {
    return {
      'loading': true,
      'ability': {},
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
.ability-icon {
    height: 120px;
}
</style>
