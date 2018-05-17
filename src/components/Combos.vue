<template>
  <section class="opaque-background">
    <b-row>
      <b-col>
        <h1 class="text-warning">Combo Stats</h1>
      </b-col>
    </b-row>
    <b-row>
      <b-col>
        <p>Lorem ipsum dolor sit amet, consectetur adipiscing elit. Maecenas eget porta velit. Etiam aliquam auctor nulla, vitae congue ligula rhoncus vel. Fusce porta imperdiet risus, ac maximus magna posuere in. Suspendisse quis sodales velit. Sed fringilla enim quis nibh congue efficitur. Proin ante lectus, rhoncus quis venenatis in, maximus malesuada ipsum. Cras hendrerit facilisis ante at molestie. Nullam ullamcorper diam vitae dolor placerat, nec euismod neque placerat.</p>
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
            <b-progress height="1.5rem" :value="round(stat.win_rate)" :min="0" :max="100" :striped="true" show-progress></b-progress>
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
            <b-progress height="1.5rem" :value="round(stat.win_rate)" :min="0" :max="100" :striped="true" show-progress></b-progress>
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
            <b-progress height="1.5rem" :value="round(stat.win_rate)" :min="0" :max="100" :striped="true" show-progress></b-progress>
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
            <b-progress height="1.5rem" :value="round(stat.win_rate)" :min="0" :max="100" :striped="true" show-progress></b-progress>
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
            <b-progress height="1.5rem" :value="round(stat.win_rate)" :min="0" :max="100" :striped="true" show-progress></b-progress>
          </b-col>
          <b-col cols="2">
            {{stat.wins}} / {{stat.picks}}
          </b-col>
        </b-row>
      </b-col>
    </b-row>

  </section>
</template>

<script>
import abilitiesDB from '@/data/abilities.json'
import statsDB from '@/data/stats-abilities.json'

export default {
  name: 'CombosList',
  data () {
    const limit = 10

    let melee = statsDB.filter(s => s.type === 1).slice(0, limit)
    let range = statsDB.filter(s => s.type === 2).slice(0, limit)
    let str = statsDB.filter(s => s.type === 3).slice(0, limit)
    let agi = statsDB.filter(s => s.type === 4).slice(0, limit) 
    let int = statsDB.filter(s => s.type === 5).slice(0, limit)
    let all = [melee, range, str, agi, int]
    
    for (let z = 0; z < all.length; z++) {
      let items = all[z]

      for (let i = 0; i < items.length; i++) {
        const keys = items[i].abilities.split('-')

        items[i].pair = []

        for (let x = 0; x < keys.length; x++) {
          const id = keys[x]
          const ability = abilitiesDB[id]
          items[i].pair.push(ability)
        }
      }
    }

    return {
      'topMelee': melee,
      'topRange': range,
      'topStr': str,
      'topAgi': agi,
      'topInt': int
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
</style>
