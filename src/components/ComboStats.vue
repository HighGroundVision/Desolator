<template>
  <section  class="opaque-background">
    <b-row>
      <b-col>
        <h1 class="text-warning">Combo Details</h1>
      </b-col>
    </b-row>
    <b-row>
      <b-col>
        <p>Lorem ipsum dolor sit amet, consectetur adipiscing elit. Maecenas eget porta velit. Etiam aliquam auctor nulla, vitae congue ligula rhoncus vel. Fusce porta imperdiet risus, ac maximus magna posuere in. Suspendisse quis sodales velit. Sed fringilla enim quis nibh congue efficitur. Proin ante lectus, rhoncus quis venenatis in, maximus malesuada ipsum. Cras hendrerit facilisis ante at molestie. Nullam ullamcorper diam vitae dolor placerat, nec euismod neque placerat.</p>
      </b-col>
    </b-row>
    <hr />
    <b-row>
      <b-col cols="6" v-for="ability in abilities" :key="ability.id">
        <b-row>
          <b-col md="2">
            <b-img :src="ability.img" :title="ability.dname" fluid class="ability-icon" />
          </b-col>
          <b-col>
            <b-row>
              <b-col>
                <b-link :to="'/stats/ability/' + ability.id">
                  <h2>{{ability.dname}}</h2>
                </b-link>
              </b-col>
            </b-row>
            <b-row>
              <b-col>
                <p>{{ability.desc}}</p>
                <p>
                  <b-link v-on:click="attributes = !attributes">Attributes</b-link>
                </p>
              </b-col>
            </b-row>
          </b-col>
        </b-row>
        <div v-if="attributes">
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
        </div>
        
      </b-col>
    </b-row>
    <hr />
    <b-row>
      <b-col>
        <h3>Combind Stats</h3>
        <p>Lorem ipsum dolor sit amet, consectetur adipiscing elit. Maecenas eget porta velit. Etiam aliquam auctor nulla, vitae congue ligula rhoncus vel. Fusce porta imperdiet risus, ac maximus magna posuere in. Suspendisse quis sodales velit. Sed fringilla enim quis nibh congue efficitur. Proin ante lectus, rhoncus quis venenatis in, maximus malesuada ipsum. Cras hendrerit facilisis ante at molestie. Nullam ullamcorper diam vitae dolor placerat, nec euismod neque placerat.</p>
      </b-col>
    </b-row>
    <b-row class="text-center">
      <b-col cols="12">
        <b-row v-if="combindStats.length > 0">
          <b-col>Type</b-col>
          <b-col>Win Rate</b-col>
          <b-col>Wins / Picks</b-col>
        </b-row>
        <b-row v-for="stat in combindStats" :key="combindStats.indexOf(stat)">
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
        <h3>Individual Stats</h3>
        <p>Lorem ipsum dolor sit amet, consectetur adipiscing elit. Maecenas eget porta velit. Etiam aliquam auctor nulla, vitae congue ligula rhoncus vel. Fusce porta imperdiet risus, ac maximus magna posuere in. Suspendisse quis sodales velit. Sed fringilla enim quis nibh congue efficitur. Proin ante lectus, rhoncus quis venenatis in, maximus malesuada ipsum. Cras hendrerit facilisis ante at molestie. Nullam ullamcorper diam vitae dolor placerat, nec euismod neque placerat.</p>
      </b-col>
    </b-row>
    <b-row class="text-center">
      <b-col cols="12">
        <b-row v-if="individualStats.length > 0">
          <b-col>Ability</b-col>
          <b-col>Icon</b-col>
          <b-col>Type</b-col>
          <b-col>Win Rate</b-col>
          <b-col>Wins / Picks</b-col>
        </b-row>
        <b-row v-for="stat in individualStats" :key="individualStats.indexOf(stat)">
          <b-col>
            {{stat.name}}
          </b-col>
          <b-col>
            <b-img :src="stat.img" class="ability-icon-sm" />
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
  data () {
    let key = this.$route.params.key
    let abilityKeys = key.split('-')

    let individual = []
    let abilities = []

    abilityKeys.forEach(id => {
      const abilityId = parseInt(id)
      const ability = abilitiesDB[abilityId]
      abilities.push(ability)

      let stats = statsAbilityDB.filter(stat => stat.abilities === id)
      for (let index = 0; index < stats.length; index++) {
        stats[index].img = ability.img
        stats[index].name = ability.dname
      }
      individual = individual.concat(stats)
    })

    let combind = statsAbilitiesDB.filter(stat => stat.abilities === key)
    combind.sort(function (lhs, rhs) { return rhs.win_rate - lhs.win_rate })
    individual.sort(function (lhs, rhs) { return rhs.win_rate - lhs.win_rate })

    return {
      'attributes': false,
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
</style>
