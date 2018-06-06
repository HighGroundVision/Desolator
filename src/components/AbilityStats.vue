<template>
  <section v-if="ready" class="opaque-background">
    <b-row>
      <b-col>
        <h1 class="text-warning">Ability Details</h1>
      </b-col>
    </b-row>
    <b-row>
      <b-col>
        <b-row>
          <b-col>          
            <p>
              Abilities are the heart and soul of Ability Draft (and dare we say Dota itself!) so geting to know your abilities is key to enjoying AD.
              We have collected the most usefull information about abilities into one place including the ability attributes, stats, and combos.
            </p>
          </b-col>
        </b-row>
        <b-row>
          <b-col cols="4" md="2">
            <b-img :src="ability.img" :title="ability.dname" fluid class="ability-icon ability-icon-lg" />
          </b-col>
          <b-col>
            <b-row>
              <b-col>
                <h2>{{ability.dname}}</h2>
              </b-col>
            </b-row>
            <b-row>
              <b-col>
                <p>{{ability.desc}}</p>
              </b-col>
            </b-row>
            <b-row>
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
          </b-col>
        </b-row>
      </b-col>
      <b-col cols="3">
        <hgv-social-share :msg="socialMessage"></hgv-social-share>
      </b-col>
    </b-row>
    <hr class="highlighted" />
    <b-row>
      <b-col>
        <h3>Stats</h3>
        <p>
          An ability win rate by self is not that valuable as it can generaly effected by the type of hero that try and use those abilities.
          We broke out the picks, wins, and win rate by Melee, Range, Str, Agi, Int to give you a better understanding of how that ability will align with your hero.
          We also encourage the community to get involved and help us rank each ability to generate a Karma score.
        </p>
        <div class="text-center">
          <b-row>
            <b-col>Type</b-col>
            <b-col>Win Rate</b-col>
            <b-col>Wins / Picks</b-col>
          </b-row>
          <b-row v-for="stat in stats" :key="stats.indexOf(stat)">
            <b-col v-if="stat.type === 1">
              <b-img src="https://hgv-hyperstone.azurewebsites.net/mics/type_melee.png" title="Melee" class="ability-icon-sm" /> Melee
            </b-col>
            <b-col v-if="stat.type === 2">
              <b-img src="https://hgv-hyperstone.azurewebsites.net/mics/type_range.png" title="Range" class="ability-icon-sm" /> Range
            </b-col>
            <b-col v-if="stat.type === 3">
              <b-img src="https://hgv-hyperstone.azurewebsites.net/mics/primary_str.png" title="Str" class="ability-icon-sm" /> Str &nbsp; &nbsp;
            </b-col>
            <b-col v-if="stat.type === 4">
              <b-img src="https://hgv-hyperstone.azurewebsites.net/mics/primary_agi.png" title="Agi" class="ability-icon-sm" /> Agi &nbsp; &nbsp;
            </b-col>
            <b-col v-if="stat.type === 5">
              <b-img src="https://hgv-hyperstone.azurewebsites.net/mics/primary_int.png" title="Int" class="ability-icon-sm" /> Int &nbsp; &nbsp;
            </b-col>
            <b-col>
              <b-progress :value="stat.win_rate" :min="0" :max="100" :striped="true" show-progress></b-progress>
            </b-col>
            <b-col>{{stat.wins}} / {{stat.picks}}</b-col>
          </b-row>
        </div>
      </b-col>
      <b-col cols="3">
        <hgv-karma :type="1"></hgv-karma>
      </b-col>
    </b-row>
    <hr />
    <b-row>
      <b-col>
        <h3>Combos</h3>
        <p>
          An Ability is just a start. 
          A good combo can go a long way to help you win the match. 
          Like abilities we have taken the pair of abilties to from a combo and add the type to form triplets with a wins, picks, and win rate for each of these combos.
          These are the top combos with this ability by win rate.
        </p>
      </b-col>
    </b-row>
    <b-row class="text-center">
      <b-col cols="12">
        <b-row v-if="combos.length > 0">
          <b-col>Icon</b-col>
          <b-col>Ability</b-col>
          <b-col>Type</b-col>
          <b-col>Win Rate</b-col>
          <b-col>Wins / Picks</b-col>
        </b-row>
        <b-row v-for="stat in combos" :key="combos.indexOf(stat)">
          <b-col>
            <b-img :src="stat.img" class="ability-icon-sm" /> 
          </b-col>
          <b-col>
            <b-link :to="'/stats/abilities/' + stat.abilities">{{stat.name}}</b-link>
          </b-col>
          <b-col>
            <div v-if="stat.type === 1">
              <b-img src="https://hgv-hyperstone.azurewebsites.net/mics/type_melee.png" title="Melee" class="ability-icon-sm" /> Melee
            </div>
            <div v-if="stat.type === 2">
              <b-img src="https://hgv-hyperstone.azurewebsites.net/mics/type_range.png" title="Range" class="ability-icon-sm" /> Range
            </div>
            <div v-if="stat.type === 3">
              <b-img src="https://hgv-hyperstone.azurewebsites.net/mics/primary_str.png" title="Str" class="ability-icon-sm" /> Str &nbsp; &nbsp;
            </div>
            <div v-if="stat.type === 4">
              <b-img src="https://hgv-hyperstone.azurewebsites.net/mics/primary_agi.png" title="Agi" class="ability-icon-sm" /> Agi &nbsp; &nbsp;
            </div>
            <div v-if="stat.type === 5">
              <b-img src="https://hgv-hyperstone.azurewebsites.net/mics/primary_int.png" title="Int" class="ability-icon-sm"  /> Int &nbsp; &nbsp;
            </div>
          </b-col>
          <b-col>
            <b-progress :value="stat.win_rate" :min="0" :max="100" :striped="true" show-progress></b-progress>
          </b-col>
          <b-col>{{stat.wins}} / {{stat.picks}}</b-col>
        </b-row>
      </b-col>
    </b-row>
  </section>
  <section v-else class="opaque-background text-center">
    <hgv-loader :color="'#ffc107'"></hgv-loader>
  </section>
</template>

<script>
import axios from 'axios'

export default {
  name: 'AbilityStats',
  data () {
    return {
      'ready': false,
      'socialMessage': 'Cluckles says to check out stats for',
      'ability': [],
      'stats': [],
      'combos': []
    }
  },
  created: function () {
    const vm = this

    let web = [
      axios.get('/static/data/abilities.json').then((reponse) => { return reponse.data }),
      axios.get('/static/data/stats-abilities.json').then((reponse) => { return reponse.data }),
      axios.get('/static/data/stats-combos.json').then((reponse) => { return reponse.data })
    ]

    Promise.all(web).then((values) => {
      const abilitiesDB = values[0]
      const statsAbilitiesDB = values[1]
      const statsComboDB = values[2]
      
      let abilityKey = vm.$route.params.key
      let key = parseInt(abilityKey)

      let ability = abilitiesDB[key]

      let results = statsAbilitiesDB.filter(stat => stat.abilities === abilityKey)
      results.sort(function (lhs, rhs) { return rhs.win_rate - lhs.win_rate })

      let combos = statsComboDB.filter((lhs) => lhs.abilities.includes(abilityKey))
      combos.sort(function (lhs, rhs) { return rhs.win_rate - lhs.win_rate })
      combos = combos.slice(0, 10)

      let topCombos = []
      combos.forEach(combo => {
        let parts = combo.abilities.split('-')
        const otherId = parts.filter(z => z !== abilityKey)[0]
        const ability = abilitiesDB[otherId]

        let data = {
          'abilities': combo.abilities,
          'name': ability.dname,
          'img': ability.img,
          'type': combo.type,
          'win_rate': combo.win_rate,
          'wins': combo.wins,
          'picks': combo.picks
        }
        topCombos.push(data)
      })

      vm.socialMessage = 'Cluckles says to check out stats for ' + ability.dname
      vm.key = abilityKey
      vm.ability = ability
      vm.stats = results
      vm.combos = topCombos
      vm.ready = true
    }).catch(function () {
      vm.$router.push('/error')
    })
  },
  methods: {
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
</style>
