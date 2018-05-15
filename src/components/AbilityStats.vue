<template>
  <section>
    <div v-if="found">
     <b-row>
        <b-col cols="4" sm="2" md="1" lg="1" xl="1">
          <b-img :src="ability.img" :title="ability.dname" fluid class="ability-icon" />
        </b-col>
        <b-col>
          <b-row>
            <b-col>
              <p class="header">{{ability.dname}}</p>
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
        <b-col cols="6" xs="4" sm="2" md="1" lg="1" xl="1"></b-col>
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
      <p class="header">Individual Stats</p>
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
      <hr />
      <missing-data></missing-data>
    </div>
    <div v-else>
      <b-row>
        <b-col>
          <p class="header">Missing!</p>
          <p>Could not find Ability {{abilityKey}}</p>
        </b-col>
      </b-row>
    </div>
  </section>
</template>

<script>
import abilitiesDB from '@/data/abilities.json'
import statsDB from '@/data/stats-ability.json'

export default {
  name: 'AbilityStats',
  props: {
    abilityKey: {
      type: String,
      default: '0'
    }
  },
  data () {
    let key = parseInt(this.abilityKey)

    let ability = abilitiesDB[key]
    if (ability === undefined) {
      return { 'found': false }
    }

    let results = statsDB.filter(stat => stat.abilities === this.abilityKey)
    results.sort(function (lhs, rhs) { return rhs.win_rate - lhs.win_rate })

    return {
      'found': true,
      'ability': ability,
      'stats': results
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
.header {
  font-size: 1.6em;
  font-weight: bold;
}
.ability-icon {

    box-shadow: 5px 4px #000000;
}
</style>
