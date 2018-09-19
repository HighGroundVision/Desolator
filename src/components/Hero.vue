<template>
  <section v-if="ready" >
    <b-row>
      <b-col>
        <h2 class="text-warning">Hero Details</h2>
      </b-col>
    </b-row>
    <b-row>
      <b-col>
        <p>
            Lorem ipsum dolor sit amet, consectetur adipiscing elit. Donec eget tempus quam, quis posuere massa. Phasellus ultricies iaculis sapien. Suspendisse tristique odio ullamcorper laoreet vehicula. Duis a ex ut orci pretium blandit efficitur vitae sem. Nullam interdum lobortis mauris ut lacinia. Sed eros ligula, porta sed sapien sed, tempor dapibus nisl. Phasellus turpis massa, rhoncus non ligula sit amet, posuere ultrices elit. Aliquam erat volutpat.
        </p>
      </b-col>
    </b-row>
    <b-row class="text-center">
        <b-col>
            <h3>{{hero.name}}</h3>
        </b-col>
        <b-col>
            <b-img :src="hero.image_banner" rounded class="hero-icon-banner"></b-img>
        </b-col>
        <b-col>
            <div>Wins</div>
            <span>{{stats.wins}}</span>
        </b-col>
        <b-col>
            <div>Picks</div>
            <span>{{stats.picks}}</span>
        </b-col>
        <b-col>
            <div>Win Rate</div>
            <b-progress height="2rem" :value="stats.win_rate" :min="0" :max="1" :striped="true" show-progress></b-progress>
        </b-col>
    </b-row>
    <hr class="highlighted" />
    <b-row>
        <b-col cols="4">
             <h4 class="text-center">Hero Stats</h4>
             <ul class="list-group">
                <li class="list-group-item">
                    <b-row>
                        <b-col>
                            <span class="invert">Primary Attribute</span>
                        </b-col>
                        <b-col cols="2">
                            <b-img v-if="hero.attribute_primary === 'DOTA_ATTRIBUTE_STRENGTH'" src="https://hgv-hyperstone.azurewebsites.net/mics/primary_str.png" rounded class="ability-icon-sm" />
                            <b-img v-if="hero.attribute_primary === 'DOTA_ATTRIBUTE_AGILITY'" src="https://hgv-hyperstone.azurewebsites.net/mics/primary_agi.png" rounded class="ability-icon-sm" />
                            <b-img v-if="hero.attribute_primary === 'DOTA_ATTRIBUTE_INTELLIGENCE'" src="https://hgv-hyperstone.azurewebsites.net/mics/primary_int.png" rounded class="ability-icon-sm" />
                        </b-col>
                    </b-row>
                </li>
                <li class="list-group-item">
                    <b-row>
                        <b-col>
                            <b-img src="https://hgv-hyperstone.azurewebsites.net/mics/primary_str.png" rounded class="ability-icon-sm" />
                        </b-col>
                        <b-col cols="2">
                            <span class="invert" title="Strength Base">{{hero.attribute_base_strength}}</span>
                        </b-col>
                        <b-col cols="2">
                            <span class="invert" title="Strength Base">{{hero.attribute_strength_gain}}</span>
                        </b-col>
                    </b-row>
                </li>
                <li class="list-group-item">
                    <b-row>
                        <b-col>
                            <b-img src="https://hgv-hyperstone.azurewebsites.net/mics/primary_agi.png" rounded class="ability-icon-sm" />
                        </b-col>
                        <b-col cols="2">
                            <span class="invert" title="Agility Base">{{hero.attribute_base_agility}}</span>
                        </b-col>
                        <b-col cols="2">
                            <span class="invert" title="Agility Base">{{hero.attribute_agility_gain}}</span>
                        </b-col>
                    </b-row>
                </li>
                <li class="list-group-item">
                    <b-row>
                        <b-col>
                            <b-img src="https://hgv-hyperstone.azurewebsites.net/mics/primary_int.png" rounded class="ability-icon-sm" />
                        </b-col>
                        <b-col cols="2">
                            <span class="invert" title="Intelligence Base">{{hero.attribute_base_intelligence}}</span>
                        </b-col>
                        <b-col cols="2">
                            <span class="invert" title="Intelligence Base">{{hero.attribute_intelligence_gain}}</span>
                        </b-col>
                    </b-row>
                </li>
                <li class="list-group-item">
                    <b-row>
                        <b-col>
                            <span class="invert">Health</span>
                        </b-col>
                        <b-col cols="2">
                            <span class="invert" title="Health Base">{{hero.status_health}}</span>
                        </b-col>
                        <b-col cols="2">
                            <span class="invert" title="Health Base">{{hero.status_health_regen}}</span>
                        </b-col>
                    </b-row>
                </li>
                <li class="list-group-item">
                    <b-row>
                        <b-col>
                            <span class="invert">Mana</span>
                        </b-col>
                        <b-col cols="2">
                            <span class="invert" title="Mana Base">{{hero.status_mana}}</span>
                        </b-col>
                        <b-col cols="2">
                            <span class="invert" title="Mana Regen">{{hero.status_mana_regen}}</span>
                        </b-col>
                    </b-row>
                </li>
                <li class="list-group-item">
                    <b-row>
                        <b-col>
                            <span class="invert">Attack Damage</span>
                        </b-col>
                        <b-col cols="2">
                            <span class="invert" title="Damage Min">{{hero.attack_damage_min}}</span>
                        </b-col>
                        <b-col cols="2">
                            <span class="invert" title="Damage Max">{{hero.attack_damage_max}}</span>
                        </b-col>
                    </b-row>
                </li>
                <li class="list-group-item">
                    <b-row>
                        <b-col>
                            <span class="invert">Attack Range</span>
                        </b-col>
                        <b-col cols="2">
                            <span class="invert" v-if="hero.attack_capabilities === 'DOTA_UNIT_CAP_MELEE_ATTACK'" title="Attack Type">Melee</span>
                            <span class="invert" v-if="hero.attack_capabilities === 'DOTA_UNIT_CAP_RANGE_ATTACK'" title="Attack Type">Ranged</span>
                        </b-col>
                        <b-col cols="2">
                            <span class="invert" title="Attack Distance">{{hero.attack_range}}</span>
                        </b-col>
                    </b-row>
                </li>
                <li class="list-group-item">
                    <b-row>
                        <b-col>
                            <span class="invert">Move Speed</span>
                        </b-col>
                        <b-col cols="2">
                            <span class="invert" title="Movement Rate">{{hero.movement_speed}}</span>
                        </b-col>
                        <b-col cols="2">
                            <span class="invert" title="Turn Rate">{{hero.movement_turn_rate}}</span>
                        </b-col>
                    </b-row>
                </li>
                <li class="list-group-item">
                    <b-row>
                        <b-col>
                            <span class="invert">Vision</span>
                        </b-col>
                        <b-col cols="2">
                            <span class="invert" title="Daytime Range">{{hero.vision_daytime_range}}</span>
                        </b-col>
                        <b-col cols="2">
                            <span class="invert" title="Nighttime Range">{{hero.vision_nighttime_range}}</span>
                        </b-col>
                    </b-row>
                </li>
             </ul>
        </b-col>
        <b-col cols="4">
            <h4 class="text-center">Default Abilities</h4>
            <ul class="list-group">
                <template v-for="ability in hero.abilities">
                    <li v-if="ability.ability_draft_enabled" :key="ability.id" class="list-group-item">
                        <b-row>
                            <b-col cols="2">
                                <b-img :src="ability.image" rounded class="ability-icon-sm" />
                            </b-col>
                            <b-col>
                                <b-link :to="'/ability/' + ability.id" target="_blank">
                                    <span class="invert">{{ability.name}}</span>
                                </b-link>
                            </b-col>
                        </b-row>
                    </li>
                </template>
            </ul>
        </b-col>
        <b-col cols="4">
            <h4 class="text-center">Talents</h4>
            <ul class="list-group">
                <template v-for="talent in talents">
                    <li :key="talent.level" class="list-group-item">
                        <b-row style="line-height: 32px;">
                            <b-col cols="5">
                                <span class="invert" :title="talent.option1.description">{{talent.option1.name}}</span>
                            </b-col>
                            <b-col cols="2" class="text-center">
                                <span class="badge badge-pill badge-info">{{talent.level}}</span>
                            </b-col>
                            <b-col cols="5">
                                <span class="invert" :title="talent.option2.description">{{talent.option2.name}}</span>
                            </b-col>
                        </b-row>
                    </li>
                </template>
            </ul>
        </b-col>
    </b-row>
    <br />
    <h3 class="text-center">Top Abilities</h3>
    <b-row>
        <b-col>
            <b-card>
                <b-card-body>
                    <b-table :fields="abilities.fields" :items="computedAbilities" :sort-by.sync="abilities.sortBy" :sort-desc.sync="abilities.sortDesc" >
                        <template slot="icon" slot-scope="row">
                            <b-img :src="row.item.img" class="ability-icon-sm" />
                        </template>
                        <template slot="link" slot-scope="row">
                            <b-link :to="'/ability/' + row.item.id" target="_blank">{{row.item.name}}</b-link>
                        </template>
                        <template slot="win_rate_progress" slot-scope="row">
                            <b-progress height="2rem" :value="row.item.win_rate" :min="0" :max="1" :striped="true" show-progress></b-progress>
                        </template>
                        <template slot="wins" slot-scope="row">
                            <span>{{row.item.wins}}</span>
                        </template>
                        <template slot="picks" slot-scope="row">
                            <span>{{row.item.picks}}</span>
                        </template>
                        <template slot="ultimate" slot-scope="row">
                            <span v-if="row.item.is_ultimate" class="badge badge-success">Yes</span>
                            <span v-else class="badge badge-warning">No</span>
                        </template>
                        <template slot="upgrade" slot-scope="row">
                            <span v-if="row.item.has_upgrade" class="badge badge-success">Yes</span>
                            <span v-else class="badge badge-warning">No</span>
                        </template>
                    </b-table>
                </b-card-body>
            </b-card>
            
        </b-col>
    </b-row>
  </section>
  <section v-else class="text-center">
    <hgv-loader :color="'#ffc107'"></hgv-loader>
  </section>
</template>

<script>
import axios from 'axios'

export default {
  name: 'HeroDetails',
  data () {
    const fields = [
      { key: 'icon', label: 'Icon', sortable: false },
      { key: 'link', label: 'Ability', sortable: true },
      { key: 'ultimate', label: 'Ultimate', sortable: true },
      { key: 'upgrade', label: 'Upgradable', sortable: true },
      { key: 'wins', label: 'Wins', sortable: true },
      { key: 'picks', label: 'Picks', sortable: true },
      { key: 'win_rate_progress', label: 'Win Rate', sortable: true }
    ]

    return {
      'ready': false,
      'hero': null,
      'talents': [],
      'stats': null,
      'abilities': {
        'items': [],
        'fields': fields,
        'sortBy': 'win_rate_progress',
        'sortDesc': true
      }
    }
  },
  created: function () {
    const vm = this

    let id = this.$route.params.id

    let web = [
      axios.get('/static/data/heroes/' + id + '/hero.json').then((reponse) => { return reponse.data }),
      axios.get('/static/data/heroes/' + id + '/stats.json').then((reponse) => { return reponse.data }),
      axios.get('/static/data/heroes/' + id + '/abilities.json').then((reponse) => { return reponse.data })
    ]

    Promise.all(web).then((values) => {
      let hero = values[0] 
      let stat = values[1] 
      let abilities = values[2]
      let talents = []

      talents.push({ level: 10, option1: hero.talents[0], option2: hero.talents[1] })
      talents.push({ level: 15, option1: hero.talents[2], option2: hero.talents[3] })
      talents.push({ level: 20, option1: hero.talents[4], option2: hero.talents[5] })
      talents.push({ level: 25, option1: hero.talents[6], option2: hero.talents[7] })

      vm.talents = talents
      vm.hero = hero
      vm.stats = stat
      vm.abilities.items = abilities
      vm.ready = true
    }).catch(function () {
      vm.$router.push('/error')
    })
  },
  computed: {
    computedAbilities: function () {
      let items = this.abilities.items

      items = items.sort((lhs, rhs) => {
        if (this.abilities.sortBy === 'link') {
          return this.abilities.sortDesc ? lhs.name.localeCompare(rhs.name) : rhs.name.localeCompare(lhs.name)
        } else if (this.abilities.sortBy === 'win_rate_progress') {
          return this.abilities.sortDesc ? rhs.win_rate - lhs.win_rate : lhs.win_rate - rhs.win_rate
        } else if (this.abilities.sortBy === 'ultimate') {
          return this.abilities.sortDesc ? lhs.is_ultimate > rhs.is_ultimate : lhs.is_ultimate < rhs.is_ultimate 
        } else if (this.abilities.sortBy === 'upgrade') {
          return this.abilities.sortDesc ? lhs.has_upgrade > rhs.has_upgrade : lhs.has_upgrade < rhs.has_upgrade 
        } else {
          return 0
        }
      })

      return items
    }
  },
  methods: {
    resetModel () {
    }
  }
}
</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped>
</style>
