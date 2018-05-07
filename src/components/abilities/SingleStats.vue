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
        <b-col v-for="stat in stats" :key="stat.type" cols="12">
          <p class="stat-header">{{stat.type}}</p>
          <b-row>
            <b-col>Pick Rate</b-col>
            <b-col>{{round(stat.pickRate)}} %</b-col>
            <b-col>
              <b-progress :value="round(stat.pickRate)" :min="0" :max="100"></b-progress>
            </b-col>
            <b-col>{{stat.picks}} / {{stat.total}}</b-col>
          </b-row>
          <b-row>
            <b-col>Win Rate</b-col>
            <b-col>{{round(stat.winRate)}} %</b-col>
            <b-col>
              <b-progress :value="round(stat.winRate)" :min="0" :max="100"></b-progress>
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
import { stateMerge } from 'vue-object-merge'

export default {
  name: 'SingleStats',
  created () {
    var url = process.env.API_BASE_URL + '/RequestStats?key=' + this.$route.params.key
    this.$http.get(url).then((response) => {
      if (!response.ok) return
      var data = response.body

      // if (data.abilities.length > 0) return
      this.ability = abilities[data.abilities[0]]

      if (data.melee) {
        stateMerge(data.melee, {type: 'Melee Heroes'})
        this.stats.push(data.melee)
      }

      if (data.range) {
        stateMerge(data.range, {type: 'Range Heroes'})
        this.stats.push(data.range)
      }

      this.loading = false
    })
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
.stat-header {
  font-size: 1.6em;
}
</style>
