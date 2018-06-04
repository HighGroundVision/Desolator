<template>
  <section v-if="ready" class="opaque-background">
    <b-row>
      <b-col>
        <h1 class="text-warning">Combo Stats</h1>
      </b-col>
    </b-row>
    <b-row>
      <b-col>
        <p>
        We have compiled win rates since 7.07 patch using random sampling every day. 
        Combos are created by combining all active skills with each other (creating Set A) then all skills are combind with each active ultimate (creating Set B). 
        Set A is then merge with Set B creating all posible ability combos this is then paired with each type (Str, Agi, Int, Melee, Range) to create ~1M triplets
        This also means the chance that one of these triplets show up in a match is very low. 
        To counter act this offset we have filter out of results triplets that do not meet the minimum number of picks. 
        Which is implemented an average of whole set. 
        After this filter we are left with ~120,000 triplets.
        </p>
        <p>Below we highlight the top 10 combos under each category.</p>
      </b-col>
    </b-row>
    <b-row>
      <b-col class="text-center">
        <b-img src="https://hgv-hyperstone.azurewebsites.net/mics/type_melee.png" title="Melee" class="ability-icon-sm" /> 
        <h3>Melee</h3>
      </b-col>
    </b-row>
    <br />
    <b-row>
      <b-col>
        <b-row v-for="stat in topMelee" :key="topMelee.indexOf(stat)">
          <b-col cols="2">
            <b-img :src="stat.pair[0].img" class="ability-icon-sm" />
            <b-img :src="stat.pair[1].img" class="ability-icon-sm" />
          </b-col>
          <b-col>
            <b-link :to="'/stats/abilities/' + stat.abilities">{{stat.pair[0].dname}} | {{stat.pair[1].dname}}</b-link>
          </b-col>
          <b-col>
            <b-progress height="1.5rem" :value="stat.win_rate" :min="0" :max="100" :striped="true" show-progress></b-progress>
          </b-col>
          <b-col cols="2">
            {{stat.wins}} / {{stat.picks}}
          </b-col>
        </b-row>
      </b-col>
    </b-row>
    <hr />
    <b-row>
      <b-col class="text-center">
        <b-img src="https://hgv-hyperstone.azurewebsites.net/mics/type_range.png" title="Range" class="ability-icon-sm" /> 
        <h3>Range</h3>
      </b-col>
    </b-row>
    <br />
    <b-row>
      <b-col>
        <b-row v-for="stat in topRange" :key="topRange.indexOf(stat)">
          <b-col cols="2">
            <b-img :src="stat.pair[0].img" class="ability-icon-sm" />
            <b-img :src="stat.pair[1].img" class="ability-icon-sm" />
          </b-col>
          <b-col>
            <b-link :to="'/stats/abilities/' + stat.abilities">{{stat.pair[0].dname}} | {{stat.pair[1].dname}}</b-link>
          </b-col>
          <b-col>
            <b-progress height="1.5rem" :value="stat.win_rate" :min="0" :max="100" :striped="true" show-progress></b-progress>
          </b-col>
          <b-col cols="2">
            {{stat.wins}} / {{stat.picks}}
          </b-col>
        </b-row>
      </b-col>
    </b-row>
    <hr />
    <b-row>
      <b-col class="text-center">
        <b-img src="https://hgv-hyperstone.azurewebsites.net/mics/primary_str.png" title="Str" class="ability-icon-sm" /> 
        <h3>Str</h3>
      </b-col>
    </b-row>
    <br />
    <b-row>
      <b-col>
        <b-row v-for="stat in topStr" :key="topStr.indexOf(stat)">
          <b-col cols="2">
            <b-img :src="stat.pair[0].img" class="ability-icon-sm" />
            <b-img :src="stat.pair[1].img" class="ability-icon-sm" />
          </b-col>
          <b-col>
            <b-link :to="'/stats/abilities/' + stat.abilities">{{stat.pair[0].dname}} | {{stat.pair[1].dname}}</b-link>
          </b-col>
          <b-col>
            <b-progress height="1.5rem" :value="stat.win_rate" :min="0" :max="100" :striped="true" show-progress></b-progress>
          </b-col>
          <b-col cols="2">
            {{stat.wins}} / {{stat.picks}}
          </b-col>
        </b-row>
      </b-col>
    </b-row>
    <hr />
    <b-row>
      <b-col class="text-center">
        <b-img src="https://hgv-hyperstone.azurewebsites.net/mics/primary_agi.png" title="Agi" class="ability-icon-sm" /> 
        <h3>Agi</h3>
      </b-col>
    </b-row>
    <br />
    <b-row>
      <b-col>
        <b-row v-for="stat in topAgi" :key="topAgi.indexOf(stat)">
          <b-col cols="2">
            <b-img :src="stat.pair[0].img" class="ability-icon-sm" />
            <b-img :src="stat.pair[1].img" class="ability-icon-sm" />
          </b-col>
          <b-col>
            <b-link :to="'/stats/abilities/' + stat.abilities">{{stat.pair[0].dname}} | {{stat.pair[1].dname}}</b-link>
          </b-col>
          <b-col>
            <b-progress height="1.5rem" :value="stat.win_rate" :min="0" :max="100" :striped="true" show-progress></b-progress>
          </b-col>
          <b-col cols="2">
            {{stat.wins}} / {{stat.picks}}
          </b-col>
        </b-row>
      </b-col>
    </b-row>
    <hr />
    <b-row>
      <b-col class="text-center">
        <b-img src="https://hgv-hyperstone.azurewebsites.net/mics/primary_int.png" title="Int" class="ability-icon-sm" /> 
        <h3>Int</h3>
      </b-col>
    </b-row>
    <br />
    <b-row>
      <b-col>
        <b-row v-for="stat in topInt" :key="topInt.indexOf(stat)">
          <b-col cols="2">
            <b-img :src="stat.pair[0].img" class="ability-icon-sm" />
            <b-img :src="stat.pair[1].img" class="ability-icon-sm" />
          </b-col>
          <b-col>
            <b-link :to="'/stats/abilities/' + stat.abilities">{{stat.pair[0].dname}} | {{stat.pair[1].dname}}</b-link>
          </b-col>
          <b-col>
            <b-progress height="1.5rem" :value="stat.win_rate" :min="0" :max="100" :striped="true" show-progress></b-progress>
          </b-col>
          <b-col cols="2">
            {{stat.wins}} / {{stat.picks}}
          </b-col>
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
  name: 'CombosList',
  data () {
    return {
      'ready': false,
      'topMelee': [],
      'topRange': [],
      'topStr': [],
      'topAgi': [],
      'topInt': []
    }
  },
  created: function () {
    const vm = this

    let p1 = axios.get('/static/data/abilities.json').then((reponse) => { return reponse.data })
    let p2 = axios.get('/static/data/stats-abilities.json').then((reponse) => { return reponse.data })
    
    Promise.all([p1, p2]).then((values) => {
      const abilitiesDB = values[0]
      const statsDB = values[1]

      let melee = []
      let range = []
      let str = []
      let agi = []
      let int = []

      for (let i = 0; i < statsDB.length; i++) {
        let stat = statsDB[i]
        if (stat.picks < 10) {
          continue
        }

        stat.pair = []

        const keys = stat.abilities.split('-')
        for (let x = 0; x < keys.length; x++) {
          const id = keys[x]
          const ability = abilitiesDB[id]
          stat.pair.push(ability)
        }

        if (stat.type === 1 && melee.length < 10) {
          melee.push(stat)
        } else if (stat.type === 2 && range.length < 10) {
          range.push(stat)
        } else if (stat.type === 3 && str.length < 10) {
          str.push(stat)
        } else if (stat.type === 4 && agi.length < 10) {
          agi.push(stat)
        } else if (stat.type === 5 && int.length < 10) {
          int.push(stat)
        }

        if (melee.length === 10 && 
          range.length === 10 && 
          str.length === 10 && 
          agi.length === 10 && 
          int.length === 10) {
          break
        }
      }

      vm.topMelee = melee
      vm.topRange = range
      vm.topStr = str
      vm.topAgi = agi
      vm.topInt = int
      vm.ready = true
    }).catch(function (error) {
      console.log(error)
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
