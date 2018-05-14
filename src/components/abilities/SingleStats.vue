<template>
  <section class="opaque-background">
    <div v-if="loading">
      <b-row>
        <b-col>
          <pacman-loader :loading="loading" :color="'#007bff'" ></pacman-loader>
        </b-col>
      </b-row>
    </div>
    <div v-else>
      <b-row>
        <b-col cols="2" >
          <b-img :src="ability.img" :title="ability.dname" fluid class="ability-icon" />
        </b-col>
        <b-col>
          <b-row>
            <b-col>
              <p class="ability-name">{{ability.dname}}</p>
            </b-col>
          </b-row>
          <b-row>
            <b-col>
              <p>{{ability.desc}}</p>
            </b-col>
          </b-row>
        </b-col>
      </b-row>
      <b-row>
        <b-col cols="2" />
        <b-col>
          <ul class="list-unstyled">
            <li v-if="ability.mc" class="text-info">MANA COST: {{format(ability.mc)}}</li>
            <li v-if="ability.cd" class="text-warning">COOLDOWN: {{format(ability.cd)}}</li>
            <li v-if="ability.behavior">BEHAVIOR: {{format(ability.behavior)}}</li>
            <li v-if="ability.dmg_type">DAMAGE TYPE: {{ability.dmg_type}}</li>
            <li v-if="ability.bkbpierce">PIERCING: {{ability.bkbpierce}}</li>
          </ul>
        </b-col>
        <b-col>
          <ul class="list-unstyled">
            <li class="ability-stats" v-for="attr in ability.attrib" :key="attr.key">
              {{attr.header}} {{format(attr.value)}}
            </li>
          </ul>
        </b-col>
      </b-row>
      <hr />
      <b-row>
        <b-col cols="12">
          <b-row>
            <b-col>Attack Type</b-col>
            <b-col>Primary Stat</b-col>
            <b-col>Win Rate</b-col>
            <b-col></b-col>
            <b-col>Wins / Picks</b-col>
          </b-row>
          <b-row v-for="stat in stats" :key="stats.indexOf(stat)">
            <b-col v-if="stat.type === 1">
              <b-img src="/static/images/type_melee.png" title="Melee" />
            </b-col>
            <b-col v-if="stat.type === 2">
              <b-img src="/static/images/type_range.png" title="Range" />
            </b-col>
            <b-col v-if="stat.primary === 1">
              <b-img src="/static/images/primary_str.png" title="Str" />
            </b-col>
            <b-col v-if="stat.primary === 2">
              <b-img src="/static/images/primary_agi.png" title="Agi" />
            </b-col>
            <b-col v-if="stat.primary === 3">
              <b-img src="/static/images/primary_int.png" title="Int"  />
            </b-col>
            <b-col>{{round(stat.win_rate)}} %</b-col>
            <b-col>
              <b-progress :value="round(stat.win_rate)" :min="0" :max="100"></b-progress>
            </b-col>
            <b-col>{{stat.wins}} / {{stat.picks}}</b-col>
          </b-row>
        </b-col>
      </b-row>
    </div>
  </section>
</template>

<script>
import abilities from '@/data/abilities.json'
import db from '@/data/db-extract-ability.json'

export default {
  name: 'SingleStats',
  created () {
    // var url = process.env.API_BASE_URL + '/RequestStats?key=' + this.$route.params.key

    const id = parseInt(this.$route.params.key)

    this.ability = abilities[id]

    let results = db.filter(stat => stat.picks > 10 && stat.abilities === id)
    results.sort(function (lhs, rhs) { return rhs.win_rate - lhs.win_rate })
    this.stats = results

    this.loading = false
  },
  data () {
    return {
      'loading': true,
      'ability': {},
      'stats': []
    }
  },
  methods: {
    round: function (stat) {
      return Math.round(stat * 100)
    },
    format: function (stat) {
      if (Array.isArray(stat)) {
        return stat.join(' / ')
      } else {
        return stat
      }
    }
  }
}
</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped>
.ability-name {
  font-size: 1.6em;
  font-weight: bold;
}
.ability-icon {
    height: 120px;
    box-shadow: 5px 4px #000000;
}
</style>
