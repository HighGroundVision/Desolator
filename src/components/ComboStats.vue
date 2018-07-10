<template>
  <section v-if="ready" class="opaque-background">
    <b-row>
      <b-col>
        <h1 class="text-warning">Combo Details</h1>
      </b-col>
    </b-row>
    <b-row>
      <b-col>
        <p>
          A good combo can win the game single handed.
          Knowing which combos, a high impact have is import as your team is constructing the draft.
          Making sure the right abilities goes to right heroes based on their type, while at the same time trying to deny the enemy team is key to successfull draft.
          We have collected the most useful information about combos into one place including the abilities attributes, combined and individual stats, and better combos for each ability.
        </p>
      </b-col>
    </b-row>
    <b-row>
      <b-col cols="6" v-for="ability in abilities" :key="ability.id">
        <b-row>
          <b-col md="2">
            <b-img :src="ability.img" :title="ability.dname" fluid class="ability-icon" />
          </b-col>
          <b-col>
            <b-row>
              <b-col>
                <h2><b-link :to="'/stats/ability/' + ability.id">{{ability.dname}}</b-link></h2>
              </b-col>
            </b-row>
            <b-row>
              <b-col>
                <p>{{ability.desc}}</p>
              </b-col>
            </b-row>
          </b-col>
        </b-row>
        <div>
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
    <hr class="highlighted" />
    <b-row>
      <b-col>
        <b-row>
          <b-col>
            <h3>Combo Stats</h3>
            <p>
              A combo win rate by self is not that valuable as it can generaly be effected by the type of hero that tries to use that combo.
              We broke out the picks, wins, and win rate by Melee, Range, Str, Agi, Int to give you a better understanding of how that combo will align with your hero.
            </p>
            <p>
              We also encourage the community to get involved and help us rank each ability to generate a Karma score.
            </p>
          </b-col>
        </b-row>
        <b-row class="text-center">
          <b-col cols="12">
            <b-row v-if="combind.length > 0">
              <b-col>Type</b-col>
              <b-col>Win Rate</b-col>
              <b-col>Wins / Picks</b-col>
            </b-row>
            <b-row v-for="stat in combind" :key="combind.indexOf(stat)">
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
            <hgv-missing-data></hgv-missing-data>
          </b-col>
        </b-row>
      </b-col>
      <b-col cols="3">
        <hgv-karma :type="2"></hgv-karma>
      </b-col>
    </b-row>
    <hr class="highlighted" />
    <b-row>
      <b-col>
        <h3>Abilities Stats</h3>
        <p>
          We have included the ability stat information here as well to give you view of not just how the combo affects your hero but also how the individual abilities affect your hero, to make sure you are getting the most out of this combo.
        </p>
      </b-col>
    </b-row>
    <b-row>
      <b-col v-for="item in singles" :key="item.ability.id" md="6">
        <b-row>
          <b-col cols="1" sm="2">
            <b-img :src="item.ability.img" :title="item.ability.dname" fluid class="ability-icon" />
          </b-col>
          <b-col>
            <h2>{{item.ability.dname}}</h2>
          </b-col>
        </b-row>
        <br />
        <b-row>
          <b-col class="text-center">
            <b-row v-if="item.stats.length > 0">
              <b-col>Type</b-col>
              <b-col>Win Rate</b-col>
              <b-col>Wins / Picks</b-col>
            </b-row>
            <b-row v-for="stat in item.stats" :key="item.stats.indexOf(stat)">
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
        <br />
      </b-col>
    </b-row>
    <hr class="highlighted" />
    <b-row>
      <b-col>
        <h3>Better Combos</h3>
        <p>
          This is the best combo for these abilties; Maybe it is for one but not the other; How far off is it?
          We will display the 10 combos with a win rate better then this combo.
          If there are less then this you know that this combo is with in the best for that ability.
        </p>
      </b-col>
    </b-row>
    <b-row v-for="item in combos" :key="item.ability.id">
      <b-col>
        <b-row>
          <b-col cols="1" sm="1">
            <b-img :src="item.ability.img" :title="item.ability.dname" fluid class="ability-icon" />
          </b-col>
          <b-col>
            <h2>{{item.ability.dname}}</h2>
          </b-col>
        </b-row>
        <br />
        <b-row>
          <b-col>
            <b-row v-if="item.stats.length === 0">
              <b-col class="text-center">
                <hgv-missing-combos></hgv-missing-combos>
              </b-col>
            </b-row>
            <b-row v-if="item.stats.length > 0">
              <b-col>Icon</b-col>
              <b-col>Ability</b-col>
              <b-col>Type</b-col>
              <b-col>Win Rate</b-col>
              <b-col>Wins / Picks</b-col>
            </b-row>
            <b-row v-for="stat in item.stats" :key="item.stats.indexOf(stat)">
              <b-col>
                <b-img :src="stat.img" :title="stat.name" fluid class="ability-icon-sm" />
              </b-col>
              <b-col>
                <b-link :to="'/stats/abilities/' + stat.abilities" target="_blank">{{stat.name}}</b-link>
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
        <br />
      </b-col>
    </b-row>
    <hr class="highlighted" />
    <b-row>
      <b-col cols="4" offset="4">
        <hgv-social-share :msg="socialMessage"></hgv-social-share>
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
  name: 'ComboStats',
  data () {
    return {
      'ready': false,
      'socialMessage': 'Cluckles says check out this combo',
      'abilities': [],
      'combind': [],
      'singles': [],
      'combos': []
    }
  },
  created: function () {
    const vm = this

    let p1 = axios.get('/static/data/abilities.json').then((reponse) => { return reponse.data })
    let p2 = axios.get('/static/data/stats-abilities.json').then((reponse) => { return reponse.data })
    let p3 = axios.get('/static/data/stats-combos.json').then((reponse) => { return reponse.data })
    
    Promise.all([p1, p2, p3]).then((values) => {
      const abilitiesDB = values[0]
      const statsAbilityDB = values[1]
      const statsAbilitiesDB = values[2]

      let key = vm.$route.params.key
      let abilityKeys = key.split('-')

      let names = []
      let abilities = []
      let singles = []
      let combos = []

      abilityKeys.forEach(id => {
        const ability = abilitiesDB[id]

        abilities.push(ability)
        names.push(ability.dname)

        const singleStats = statsAbilityDB.filter(stat => stat.abilities === id)
        singles.push({'ability': ability, 'stats': singleStats})

        let stats = []
        let comboStats = statsAbilitiesDB.filter(stat => stat.abilities.includes(id))
        for (let index = 0; index < comboStats.length; index++) {
          let keys = comboStats[index].abilities
          if (keys === key) {
            break
          }

          let otherId = keys.split('-').filter(z => z !== id)[0]
          const otherAbility = abilitiesDB[otherId]
          
          let data = {
            'abilities': keys,
            'name': otherAbility.dname, 
            'img': otherAbility.img, 
            'type': comboStats[index].type, 
            'win_rate': comboStats[index].win_rate, 
            'wins': comboStats[index].wins, 
            'picks': comboStats[index].picks
          }
          stats.push(data)
        }

        // Limit the results to 10
        let topStats = stats.slice(0, 10)
        combos.push({ 'ability': ability, 'stats': topStats })
      })

      let combind = statsAbilitiesDB.filter(stat => stat.abilities === key)
      combind.sort(function (lhs, rhs) { return rhs.win_rate - lhs.win_rate })
      singles.sort(function (lhs, rhs) { return rhs.win_rate - lhs.win_rate })
      combos.sort(function (lhs, rhs) { return rhs.win_rate - lhs.win_rate })

      vm.socialMessage = 'Cluckles says check out this combo ' + names.join(' & ')
      vm.abilities = abilities
      vm.combind = combind
      vm.singles = singles
      vm.combos = combos
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
