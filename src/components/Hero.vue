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
      <b-col cols="6">
        <h3>{{hero.name}}</h3>
      </b-col>
      <b-col>
        <b-img :src="hero.image_banner" rounded class="hero-icon-banner"></b-img>
      </b-col>
      <!--
      <b-col>
        <div>Wins</div>
        <span>{{stats.wins}}</span>
      </b-col>
      <b-col>
        <div>Picks</div>
        <span>{{stats.picks}}</span>
      </b-col>
      -->
      <b-col>
        <div>Win Rate</div>
        <b-progress height="2rem" :value="stats.win_rate" :min="0" :max="1" :striped="true" show-progress></b-progress>
      </b-col>
    </b-row>
    <hr class="highlighted" />
    <b-row>
        <b-col cols="4">
          <h4 class="text-center">Attributes</h4>
          <ul class="list-group">
            <li class="list-group-item">
                <b-row>
                    <b-col>
                      <h5>Primary</h5>
                    </b-col>
                    <b-col cols="2">
                      <b-img v-if="hero.attribute_primary === 'DOTA_ATTRIBUTE_STRENGTH'" src="https://hgv-hyperstone.azurewebsites.net/mics/primary_str.png" rounded class="ability-icon-sm" />
                      <b-img v-if="hero.attribute_primary === 'DOTA_ATTRIBUTE_AGILITY'" src="https://hgv-hyperstone.azurewebsites.net/mics/primary_agi.png" rounded class="ability-icon-sm" />
                      <b-img v-if="hero.attribute_primary === 'DOTA_ATTRIBUTE_INTELLECT'" src="https://hgv-hyperstone.azurewebsites.net/mics/primary_int.png" rounded class="ability-icon-sm" />
                    </b-col>
                </b-row>
            </li>
            <li class="list-group-item">
                <b-row>
                    <b-col cols="2">
                        <b-img src="https://hgv-hyperstone.azurewebsites.net/mics/primary_str.png" title="Strength" rounded class="ability-icon-sm" />
                    </b-col>
                    <b-col>
                        <b-row>
                          <b-col>
                            <span>Base</span>
                          </b-col>
                          <b-col>
                            <span>{{attributes.attribute_base_strength}}</span>
                          </b-col>
                          <b-col>
                            <b-progress :value="attributes.ranking_base_strength" :min="0" :max="1" :striped="true" ></b-progress>
                          </b-col>
                        </b-row>
                        <b-row>
                          <b-col>
                            <span>Gain</span>
                          </b-col>
                          <b-col>
                            <span>{{attributes.attribute_strength_gain}}</span>
                          </b-col>
                          <b-col>
                            <b-progress :value="attributes.ranking_strength_gain" :min="0" :max="1" :striped="true" ></b-progress>
                          </b-col>
                        </b-row>
                    </b-col>
                </b-row>
            </li>
            <li class="list-group-item">
                <b-row>
                    <b-col cols="2">
                        <b-img src="https://hgv-hyperstone.azurewebsites.net/mics/primary_agi.png" title="Agility" rounded class="ability-icon-sm" />
                    </b-col>
                    <b-col>
                        <b-row>
                          <b-col>
                            <span>Base</span>
                          </b-col>
                          <b-col>
                            <span >{{attributes.attribute_base_agility}}</span>
                          </b-col>
                          <b-col>
                            <b-progress :value="attributes.ranking_base_agility" :min="0" :max="1" :striped="true"></b-progress>
                          </b-col>
                        </b-row>
                        <b-row>
                          <b-col>
                            <span>Gain</span>
                          </b-col>
                          <b-col>
                            <span>{{attributes.attribute_agility_gain}}</span>
                          </b-col>
                          <b-col>
                            <b-progress :value="attributes.ranking_agility_gain" :min="0" :max="1" :striped="true"></b-progress>
                          </b-col>
                        </b-row>
                    </b-col>
                </b-row>
            </li>
            <li class="list-group-item">
                <b-row>
                    <b-col cols="2">
                      <b-img src="https://hgv-hyperstone.azurewebsites.net/mics/primary_int.png" title="Intelligence" rounded class="ability-icon-sm" />
                    </b-col>
                    <b-col>
                        <b-row>
                          <b-col>
                            <span>Base</span>
                          </b-col>
                          <b-col>
                            <span >{{attributes.attribute_base_intelligence}}</span>
                          </b-col>
                          <b-col>
                            <b-progress :value="attributes.ranking_base_intelligence" :min="0" :max="1" :striped="true"></b-progress>
                          </b-col>
                        </b-row>
                        <b-row>
                          <b-col>
                            <span>Gain</span>
                          </b-col>
                          <b-col>
                            <span>{{attributes.attribute_intelligence_gain}}</span>
                          </b-col>
                          <b-col>
                            <b-progress :value="attributes.ranking_intelligence_gain" :min="0" :max="1" :striped="true"></b-progress>
                          </b-col>
                        </b-row>
                    </b-col>
                </b-row>
            </li>
            <li class="list-group-item">
                <b-row>
                    <b-col cols="2">
                      <b-img src="https://hgv-hyperstone.azurewebsites.net/mics/health.png" title="Health" rounded class="ability-icon-sm" />
                    </b-col>
                    <b-col>
                        <b-row>
                          <b-col>
                            <span>Base</span>
                          </b-col>
                          <b-col>
                            <span >{{attributes.status_health}}</span>
                          </b-col>
                          <b-col>
                            <b-progress :value="attributes.ranking_health" :min="0" :max="1" :striped="true"></b-progress>
                          </b-col>
                        </b-row>
                        <b-row>
                          <b-col>
                            <span>Regen</span>
                          </b-col>
                          <b-col>
                            <span>{{attributes.status_health_regen}}</span>
                          </b-col>
                          <b-col>
                            <b-progress :value="attributes.ranking_health_regen" :min="0" :max="1" :striped="true"></b-progress>
                          </b-col>
                        </b-row>
                    </b-col>
                </b-row>
            </li>
            <li class="list-group-item">
                <b-row>
                    <b-col cols="2">
                      <b-img src="https://hgv-hyperstone.azurewebsites.net/mics/mana.png" title="Mana" rounded class="ability-icon-sm" />
                    </b-col>
                    <b-col>
                        <b-row>
                          <b-col>
                            <span>Base</span>
                          </b-col>
                          <b-col>
                            <span >{{attributes.status_mana}}</span>
                          </b-col>
                          <b-col>
                            <b-progress :value="attributes.ranking_mana" :min="0" :max="1" :striped="true"></b-progress>
                          </b-col>
                        </b-row>
                        <b-row>
                          <b-col>
                            <span>Regen</span>
                          </b-col>
                          <b-col>
                            <span>{{attributes.status_mana_regen}}</span>
                          </b-col>
                          <b-col>
                            <b-progress :value="attributes.ranking_mana_regen" :min="0" :max="1" :striped="true"></b-progress>
                          </b-col>
                        </b-row>
                    </b-col>
                </b-row>
            </li>
            <li class="list-group-item">
                <b-row>
                    <b-col cols="2">
                      <b-img src="https://hgv-hyperstone.azurewebsites.net/mics/damage.png" title="Attack" rounded class="ability-icon-sm" />
                    </b-col>
                    <b-col>
                        <b-row>
                          <b-col>
                            <span>Type</span>
                          </b-col>
                          <b-col>
                            <span class="invert" v-if="hero.attack_capabilities === 'DOTA_UNIT_CAP_MELEE_ATTACK'" title="Attack Type">Melee</span>
                            <span class="invert" v-if="hero.attack_capabilities === 'DOTA_UNIT_CAP_RANGED_ATTACK'" title="Attack Type">Ranged</span>
                          </b-col>
                          <b-col>
                          </b-col>
                        </b-row>
                        <b-row>
                          <b-col>
                            <span>Distance</span>
                          </b-col>
                          <b-col>
                            <span>{{attributes.attack_range}}</span>
                          </b-col>
                          <b-col>
                            <b-progress :value="attributes.ranking_attack_range" :min="0" :max="1" :striped="true"></b-progress>
                          </b-col>
                        </b-row>
                        <b-row>
                          <b-col>
                            <span>Damage</span>
                          </b-col>
                          <b-col>
                            <span >{{attributes.attack_damage_min}} - {{attributes.attack_damage_max}}</span>
                          </b-col>
                          <b-col>
                            <b-progress :value="attributes.ranking_attack_damage" :min="0" :max="1" :striped="true"></b-progress>
                          </b-col>
                        </b-row>
                    </b-col>
                </b-row>
            </li>
            <li class="list-group-item">
                <b-row>
                    <b-col cols="2">
                      <b-img src="https://hgv-hyperstone.azurewebsites.net/mics/movment.png" title="Movement" rounded class="ability-icon-sm" />
                    </b-col>
                    <b-col>
                        <b-row>
                          <b-col>
                            <span>Speed</span>
                          </b-col>
                          <b-col>
                            <span>{{attributes.movement_speed}}</span>
                          </b-col>
                          <b-col>
                            <b-progress :value="attributes.ranking_movement_speed" :min="0" :max="1" :striped="true"></b-progress>
                          </b-col>
                        </b-row>
                        <b-row>
                          <b-col>
                            <span>Turn</span>
                          </b-col>
                          <b-col>
                            <span>{{attributes.movement_turn_rate}}</span>
                          </b-col>
                          <b-col>
                            <b-progress :value="attributes.ranking_movement_turn_rate" :min="0" :max="1" :striped="true"></b-progress>
                          </b-col>
                        </b-row>
                    </b-col>
                </b-row>
            </li>
            <li class="list-group-item">
                <b-row>
                    <b-col cols="2">
                      <b-img src="https://hgv-hyperstone.azurewebsites.net/mics/vision.png" title="Vision" rounded class="ability-icon-sm" />
                    </b-col>
                    <b-col>
                        <b-row>
                          <b-col>
                            <span>Day</span>
                          </b-col>
                          <b-col>
                            <span>{{attributes.vision_daytime_range}}</span>
                          </b-col>
                          <b-col>
                            <b-progress :value="attributes.ranking_vision_daytime_range" :min="0" :max="1" :striped="true"></b-progress>
                          </b-col>
                        </b-row>
                        <b-row>
                          <b-col>
                            <span>Night</span>
                          </b-col>
                          <b-col>
                            <span>{{attributes.vision_nighttime_range}}</span>
                          </b-col>
                          <b-col>
                            <b-progress :value="attributes.ranking_vision_nighttime_range" :min="0" :max="1" :striped="true" ></b-progress>
                          </b-col>
                        </b-row>
                    </b-col>
                </b-row>
            </li>
          </ul>
        </b-col>
        <b-col cols="4">
            <h4 class="text-center">Abilities</h4>
            <ul class="list-group">
                <template v-for="ability in abilities">
                    <li :key="ability.id" class="list-group-item">
                        <b-row>
                            <b-col cols="2">
                                <b-img :src="ability.img" rounded class="ability-icon-sm" />
                            </b-col>
                            <b-col>
                                <b-link :to="'/ability/' + ability.id" target="_blank">
                                    <span class="invert">{{ability.name}}</span>
                                </b-link>
                            </b-col>
                            <b-col>
                              <b-progress height="1.5rem" :value="ability.win_rate" :min="0" :max="1" :striped="true" show-progress></b-progress>
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
          <b-table :fields="combos.fields" :items="computedCombos" :sort-by.sync="combos.sortBy" :sort-desc.sync="combos.sortDesc" >
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
              <span v-else class="badge badge-secondary">No</span>
            </template>
            <template slot="upgrade" slot-scope="row">
              <span v-if="row.item.has_upgrade" class="badge badge-success">Yes</span>
              <span v-else class="badge badge-secondary">No</span>
            </template>
          </b-table>
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
      // { key: 'wins', label: 'Wins', sortable: true },
      // { key: 'picks', label: 'Picks', sortable: true },
      { key: 'win_rate_progress', label: 'Win Rate', sortable: true }
    ]

    return {
      'ready': false,
      'hero': null,
      'attributes': null,
      'abilities': [],
      'talents': [],
      'stats': null,
      'combos': {
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

    let web1 = [
      axios.get('/static/data/heroes/' + id + '/hero.json').then((reponse) => { return reponse.data }),
      axios.get('/static/data/heroes/' + id + '/stats.json').then((reponse) => { return reponse.data }),
      axios.get('/static/data/heroes/' + id + '/abilities.json').then((reponse) => { return reponse.data }),
      axios.get('/static/data/heroes/' + id + '/attributes.json').then((reponse) => { return reponse.data })
    ]

    Promise.all(web1).then((values1) => {
      let hero = values1[0] 
      let stat = values1[1] 
      let combos = values1[2]
      let attributes = values1[3]
      let talents = []

      talents.push({ level: 10, option1: hero.talents[0], option2: hero.talents[1] })
      talents.push({ level: 15, option1: hero.talents[2], option2: hero.talents[3] })
      talents.push({ level: 20, option1: hero.talents[4], option2: hero.talents[5] })
      talents.push({ level: 25, option1: hero.talents[6], option2: hero.talents[7] })

      let web2 = []
      for (let i = 0; i < hero.abilities.length; i++) {
        const ability = hero.abilities[i]
        if (ability.ability_draft_enabled) {
          web2.push(axios.get('/static/data/abilities/' + ability.id + '/stats.json').then((reponse) => { return reponse.data }))
        }
      }

      Promise.all(web2).then((values2) => {
        vm.abilities = values2
        vm.talents = talents
        vm.attributes = attributes
        vm.hero = hero
        vm.stats = stat
        vm.combos.items = combos
        vm.ready = true
      })
    }).catch(function () {
      vm.$router.push('/error')
    })
  },
  computed: {
    computedCombos: function () {
      let items = this.combos.items

      items = items.sort((lhs, rhs) => {
        if (this.combos.sortBy === 'link') {
          return this.combos.sortDesc ? lhs.name.localeCompare(rhs.name) : rhs.name.localeCompare(lhs.name)
        } else if (this.combos.sortBy === 'win_rate_progress') {
          return this.combos.sortDesc ? rhs.win_rate - lhs.win_rate : lhs.win_rate - rhs.win_rate
        } else if (this.combos.sortBy === 'ultimate') {
          return this.combos.sortDesc ? lhs.is_ultimate > rhs.is_ultimate : lhs.is_ultimate < rhs.is_ultimate 
        } else if (this.combos.sortBy === 'upgrade') {
          return this.combos.sortDesc ? lhs.has_upgrade > rhs.has_upgrade : lhs.has_upgrade < rhs.has_upgrade 
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
