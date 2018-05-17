<template>
  <section  class="opaque-background">
    <b-row>
      <b-col>
        <h1 class="text-warning">Ability Details</h1>
      </b-col>
    </b-row>
    <b-row>
      <b-col>
        <p>Lorem ipsum dolor sit amet, consectetur adipiscing elit. Maecenas eget porta velit. Etiam aliquam auctor nulla, vitae congue ligula rhoncus vel. Fusce porta imperdiet risus, ac maximus magna posuere in. Suspendisse quis sodales velit. Sed fringilla enim quis nibh congue efficitur. Proin ante lectus, rhoncus quis venenatis in, maximus malesuada ipsum. Cras hendrerit facilisis ante at molestie. Nullam ullamcorper diam vitae dolor placerat, nec euismod neque placerat.</p>
      </b-col>
    </b-row>
    <hr />
    <b-row>
      <b-col cols="4" sm="2" md="1" lg="1" xl="1">
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
    <b-row>
      <b-col>
        <h3>Individual Stats</h3>
        <p>Lorem ipsum dolor sit amet, consectetur adipiscing elit. Maecenas eget porta velit. Etiam aliquam auctor nulla, vitae congue ligula rhoncus vel. Fusce porta imperdiet risus, ac maximus magna posuere in. Suspendisse quis sodales velit. Sed fringilla enim quis nibh congue efficitur. Proin ante lectus, rhoncus quis venenatis in, maximus malesuada ipsum. Cras hendrerit facilisis ante at molestie. Nullam ullamcorper diam vitae dolor placerat, nec euismod neque placerat.</p>
      </b-col>
    </b-row>
    <b-row class="text-center">
      <b-col cols="12">
        <b-row>
          <b-col>Type</b-col>
          <b-col>Win Rate</b-col>
          <b-col>Wins / Picks</b-col>
        </b-row>
        <b-row v-for="stat in stats" :key="stats.indexOf(stat)">
          <b-col v-if="stat.type === 1">
            <b-img src="/static/images/type_melee.png" title="Melee" class="ability-icon-sm" /> Melee
          </b-col>
          <b-col v-if="stat.type === 2">
            <b-img src="/static/images/type_range.png" title="Range" class="ability-icon-sm" /> Range
          </b-col>
          <b-col v-if="stat.type === 3">
            <b-img src="/static/images/primary_str.png" title="Str" class="ability-icon-sm" /> Str &nbsp; &nbsp;
          </b-col>
          <b-col v-if="stat.type === 4">
            <b-img src="/static/images/primary_agi.png" title="Agi" class="ability-icon-sm" /> Agi &nbsp; &nbsp;
          </b-col>
          <b-col v-if="stat.type === 5">
            <b-img src="/static/images/primary_int.png" title="Int" class="ability-icon-sm" /> Int &nbsp; &nbsp;
          </b-col>
          <b-col>
            <b-progress :value="round(stat.win_rate)" :min="0" :max="100" :striped="true" show-progress></b-progress>
          </b-col>
          <b-col>{{stat.wins}} / {{stat.picks}}</b-col>
        </b-row>
      </b-col>
    </b-row>
    <hr />
    <b-row>
      <b-col>
        <h3>Top Combos</h3>
        <p>Lorem ipsum dolor sit amet, consectetur adipiscing elit. Maecenas eget porta velit. Etiam aliquam auctor nulla, vitae congue ligula rhoncus vel. Fusce porta imperdiet risus, ac maximus magna posuere in. Suspendisse quis sodales velit. Sed fringilla enim quis nibh congue efficitur. Proin ante lectus, rhoncus quis venenatis in, maximus malesuada ipsum. Cras hendrerit facilisis ante at molestie. Nullam ullamcorper diam vitae dolor placerat, nec euismod neque placerat.</p>
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
              <b-img src="/static/images/type_melee.png" title="Melee" class="ability-icon-sm" /> Melee
            </div>
            <div v-if="stat.type === 2">
              <b-img src="/static/images/type_range.png" title="Range" class="ability-icon-sm" /> Range
            </div>
            <div v-if="stat.type === 3">
              <b-img src="/static/images/primary_str.png" title="Str" class="ability-icon-sm" /> Str &nbsp; &nbsp;
            </div>
            <div v-if="stat.type === 4">
              <b-img src="/static/images/primary_agi.png" title="Agi" class="ability-icon-sm" /> Agi &nbsp; &nbsp;
            </div>
            <div v-if="stat.type === 5">
              <b-img src="/static/images/primary_int.png" title="Int" class="ability-icon-sm"  /> Int &nbsp; &nbsp;
            </div>
          </b-col>
          <b-col>
            <b-progress :value="round(stat.win_rate)" :min="0" :max="100" :striped="true" show-progress></b-progress>
          </b-col>
          <b-col>{{stat.wins}} / {{stat.picks}}</b-col>
        </b-row>
        <b-row>
          <b-col>
            <b-btn variant="link" @click="comboLimit+=10">Show More</b-btn>
          </b-col>
        </b-row>
      </b-col>
    </b-row>

  </section>
</template>

<script>
import abilitiesDB from '@/data/abilities.json'
import statsAbilityDB from '@/data/stats-ability.json'
import statsAbilitieDB from '@/data/stats-abilities.json'

export default {
  name: 'AbilityStats',
  data () {
    let abilityKey = this.$route.params.key
    let key = parseInt(abilityKey)

    let ability = abilitiesDB[key]

    let results = statsAbilityDB.filter(stat => stat.abilities === abilityKey)
    results.sort(function (lhs, rhs) { return rhs.win_rate - lhs.win_rate })

    return {
      'key': abilityKey,
      'ability': ability,
      'stats': results,
      'comboLimit': 10
    }
  },
  computed: {
    combos: function () {
      let combos = statsAbilitieDB.filter((lhs) => lhs.abilities.includes(this.key))
      combos.sort(function (lhs, rhs) { return rhs.win_rate - lhs.win_rate })
      const topCombos = combos.slice(0, this.comboLimit)

      let results = []
      topCombos.forEach(combo => {
        let parts = combo.abilities.split('-')
        const otherId = parts.filter(z => z !== this.key)[0]
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
        results.push(data)
      })

      return results
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
