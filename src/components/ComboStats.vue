<template>
  <section>
    <b-row>
      <b-col cols="6" v-for="ability in abilities" :key="ability.id">
        <b-row>
          <b-col md="2">
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
          <b-col md="2"></b-col>
          <b-col>
            <ul class="list-unstyled">
              <li v-if="ability.mc" class="text-info">MANA COST: {{format(ability.mc)}}</li>
              <li v-if="ability.cd" class="text-warning">COOLDOWN: {{format(ability.cd)}}</li>
              <li v-if="ability.behavior">BEHAVIOR: {{format(ability.behavior)}}</li>
              <li v-if="ability.dmg_type">DAMAGE TYPE: {{ability.dmg_type}}</li>
              <li v-if="ability.bkbpierce">PIERCING: {{ability.bkbpierce}}</li>
              <li v-for="attr in ability.attrib" :key="attr.key">
                {{attr.header}} {{format(attr.value)}}
              </li>
            </ul>
          </b-col>
        </b-row>
        
      </b-col>
    </b-row>
    <hr />
    <p class="header">Combind Stats</p>
    <b-row>
      <b-col cols="12">
        <b-row v-if="combindStats.length > 0">
          <b-col>Attack Type</b-col>
          <b-col>Primary Stat</b-col>
          <b-col>Win Rate</b-col>
          <b-col></b-col>
          <b-col>Wins / Picks</b-col>
        </b-row>
        <b-row v-for="stat in combindStats" :key="combindStats.indexOf(stat)">
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
    <p class="header">Individual Stats</p>
    <b-row>
      <b-col cols="12">
        <b-row v-if="individualStats.length > 0">
          <b-col>Ability</b-col>
          <b-col>Attack Type</b-col>
          <b-col>Primary Stat</b-col>
          <b-col>Win Rate</b-col>
          <b-col></b-col>
          <b-col>Wins / Picks</b-col>
        </b-row>
        <b-row v-for="stat in individualStats" :key="individualStats.indexOf(stat)">
          <b-col>{{stat.abilities}}</b-col>
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
  </section>
</template>

<script>
import abilitiesDB from '@/data/abilities.json'
import statsAbilitiesDB from '@/data/stats-abilities.json'
import statsAbilityDB from '@/data/stats-ability.json'

export default {
  name: 'ComboStats',
  props: {
    abilityKeys: {
      type: Array,
      default: []
    }
  },
  data () {
    let key = this.abilityKeys.join('-')

    let combind = statsAbilitiesDB.filter(stat => stat.abilities === key)
    let individual = []

    let abilities = []
    this.abilityKeys.forEach(id => {
      const abilityId = parseInt(id)
      abilities.push(abilitiesDB[abilityId])

      let stats = statsAbilityDB.filter(stat => stat.abilities === abilityId)
      individual = individual.concat(stats)
    })

    combind.sort(function (lhs, rhs) { return rhs.win_rate - lhs.win_rate })
    individual.sort(function (lhs, rhs) { return rhs.win_rate - lhs.win_rate })

    return {
      'abilities': abilities,
      'combindStats': combind,
      'individualStats': individual
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
