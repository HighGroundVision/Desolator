<template>
  <section v-if="ready" >
    <b-row>
      <b-col>
        <h2 class="text-warning">Drafting</h2>
      </b-col>
    </b-row>
    <b-row>
      <b-col>
        <p>Working on it...</p>
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
      <b-col cols="5">
        <h5 class="text-center">Attributes</h5>
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
                    <b-progress variant="info" :value="attributes.ranking_base_strength" :min="0" :max="1" :striped="true" ></b-progress>
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
                    <b-progress variant="info" :value="attributes.ranking_strength_gain" :min="0" :max="1" :striped="true" ></b-progress>
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
                    <b-progress variant="info" :value="attributes.ranking_base_agility" :min="0" :max="1" :striped="true"></b-progress>
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
                    <b-progress variant="info" :value="attributes.ranking_agility_gain" :min="0" :max="1" :striped="true"></b-progress>
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
                    <b-progress variant="info" :value="attributes.ranking_base_intelligence" :min="0" :max="1" :striped="true"></b-progress>
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
                    <b-progress variant="info" :value="attributes.ranking_intelligence_gain" :min="0" :max="1" :striped="true"></b-progress>
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
                    <b-progress variant="info" :value="attributes.ranking_health" :min="0" :max="1" :striped="true"></b-progress>
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
                    <b-progress variant="info" :value="attributes.ranking_health_regen" :min="0" :max="1" :striped="true"></b-progress>
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
                    <b-progress variant="info" :value="attributes.ranking_mana" :min="0" :max="1" :striped="true"></b-progress>
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
                    <b-progress variant="info" :value="attributes.ranking_mana_regen" :min="0" :max="1" :striped="true"></b-progress>
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
                    <b-progress variant="info" :value="attributes.ranking_attack_range" :min="0" :max="1" :striped="true"></b-progress>
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
                    <b-progress variant="info" :value="attributes.ranking_attack_damage" :min="0" :max="1" :striped="true"></b-progress>
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
                    <b-progress variant="info" :value="attributes.ranking_movement_speed" :min="0" :max="1" :striped="true"></b-progress>
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
                    <b-progress variant="info" :value="attributes.ranking_movement_turn_rate" :min="0" :max="1" :striped="true"></b-progress>
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
                    <b-progress variant="info" :value="attributes.ranking_vision_daytime_range" :min="0" :max="1" :striped="true"></b-progress>
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
                    <b-progress variant="info" :value="attributes.ranking_vision_nighttime_range" :min="0" :max="1" :striped="true" ></b-progress>
                  </b-col>
                </b-row>
              </b-col>
            </b-row>
          </li>
        </ul>
      </b-col>
      <b-col>
        <h5 class="text-center">Hero Talents</h5>
        <ul class="list-group">
          <template v-for="talent in talents">
            <li :key="talent.level" class="list-group-item">
              <b-row style="line-height: 32px;">
                <b-col cols="5">
                  <span class="invert" :title="talent.option1.description">{{talent.option1.name}}</span>
                </b-col>
                <b-col cols="2" class="text-center">
                  <h5><span class="badge badge-pill badge-secondary">{{talent.level}}</span></h5>
                </b-col>
                <b-col cols="5">
                  <span class="invert" :title="talent.option2.description">{{talent.option2.name}}</span>
                </b-col>
              </b-row>
            </li>
          </template>
        </ul>
        <br />
        <h5 class="text-center">Ability Talents</h5>
        <ul class="list-group">
          <template v-for="talent in unique">
            <li :key="talent.key" class="list-group-item">
              <b-row>
                <b-col>
                  <span class="invert" :title="talent.description">{{talent.name}}</span>
                </b-col>
                <b-col>
                  <div class="text-right">
                    <b-img src="https://hgv-hyperstone.azurewebsites.net/abilities/empty.png" class="ability-icon-sm" />
                  </div>
                </b-col>
              </b-row>
            </li>
          </template>
        </ul>       
      </b-col>
    </b-row>
    <hr class="highlighted" />
    <b-row>
      <b-col>
        <b-form-input v-model="tables.search" placeholder="Search for Ability"></b-form-input>
      </b-col>
      <b-col>
        <b-form-select v-model="tables.filter">
          <option :value="null">Filter</option>
          <optgroup label="General">
            <option value="aghanims">Aghanims</option>
            <option value="ultimate">Ultimate</option>
          </optgroup>
          <optgroup label="Disables">
            <option value="stun,root,sleep,hex,taunt,fear">Stun</option>
            <option value="slow">Slow</option>
            <option value="silence">Silence</option>
            <option value="disarm,blind,ethereal">Disarm</option>
          </optgroup>          
        </b-form-select>
      </b-col>
      <b-col>
        <b-form-select v-model="tables.filter">
          <option :value="null">Sort</option>
          <optgroup label="Ascending">
            <option value="1">Win Rate</option>
            <option value="3">Wins</option>
            <option value="5">Picks</option>
          </optgroup>
          <optgroup label="Descending">
            <option value="2">Win Rate</option>
            <option value="4">Wins</option>
            <option value="6">Picks</option>
          </optgroup>
        </b-form-select>
      </b-col>
      <b-col>
        <vue-slider ref="slider" v-model="tables.limit" v-bind="{min: 40, max: 60}" ></vue-slider>
      </b-col>
    </b-row>
    <br />
    <b-row>
      <template v-for="item in computedTopPicksHero">
        <b-col md="1" v-bind:key="item.id">
          <div style="min-height: 100px;">
            <b-img :src="item.img" :title="item.name" class="ability-icon-md" />
            <!--<b-link :to="'/ability/' + item.id" target="_blank">{{item.name}}</b-link>-->
            <br />
            <b-progress height="0.4rem" :value="item.win_rate" :min="0" :max="1"></b-progress>
            <b-progress variant="warning" height="0.4rem" :value="item.wins" :min="0" :max="1"></b-progress>
            <b-progress variant="info" height="0.4rem" :value="item.picks" :min="0" :max="1"></b-progress>
            <br />
          </div>
        </b-col>
      </template>
    </b-row>
    <!--
    <hr class="highlighted" />
    <br />
    <b-row>
      <b-col md="6">
        <h5 class="text-center">Top Picks <small>For this Hero</small></h5>
        <b-table 
          :fields="topPicksHero.fields" 
          :items="computedTopPicksHero" 
          :sort-by.sync="topPicksHero.sortBy" :sort-desc.sync="topPicksHero.sortDesc" 
          :current-page="topPicksHero.currentPage" :per-page="topPicksHero.perPage"
          @filtered="onTopPicksHeroFiltered" >
          <template slot="actions" slot-scope="row">
            <b-button-group>
              <b-button variant="secondary" @click="removeAbility(row.item.id)">X</b-button>
              <b-button variant="success" @click="draftAbility(row.item.id)">✔</b-button>
            </b-button-group>
          </template>
          <template slot="icon" slot-scope="row">
            <b-img :src="row.item.img" class="ability-icon-sm" />
          </template>
          <template slot="link" slot-scope="row">
            <b-link :to="'/ability/' + row.item.id" target="_blank">{{row.item.name}}</b-link>
          </template>
          <template slot="win_rate" slot-scope="row">
            <b-progress height="1.5rem" :value="row.item.win_rate" :min="0" :max="1" :striped="true" show-progress></b-progress>
          </template>
          <template slot="wins" slot-scope="row">
            <b-progress variant="warning" height="1.5rem" :value="row.item.wins" :min="0" :max="1" :striped="true"></b-progress>
          </template>
          <template slot="picks" slot-scope="row">
            <b-progress variant="warning" height="1.5rem" :value="row.item.picks" :min="0" :max="1" :striped="true"></b-progress>
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
        <b-pagination align="center" :total-rows="topPicksHero.totalRows" :per-page="topPicksHero.perPage" v-model="topPicksHero.currentPage" />
      </b-col>
      <b-col md="6">
        <h5 class="text-center">Top Abilities <small>In General</small></h5>
        <b-table 
          :fields="topPicksAbilities.fields" 
          :items="computedTopPicksAbilities" 
          :sort-by.sync="topPicksAbilities.sortBy" :sort-desc.sync="topPicksAbilities.sortDesc" 
          :current-page="topPicksAbilities.currentPage" :per-page="topPicksAbilities.perPage"
          @filtered="onTopPicksAbilitiesFiltered" >
          <template slot="actions" slot-scope="row">
            <b-button-group>
              <b-button variant="secondary" @click="removeAbility(row.item.id)">X</b-button>
              <b-button variant="success" @click="draftAbility(row.item.id)">✔</b-button>
            </b-button-group>
          </template>
          <template slot="icon" slot-scope="row">
            <b-img :src="row.item.img" class="ability-icon-sm" />
          </template>
          <template slot="link" slot-scope="row">
            <b-link :to="'/ability/' + row.item.id" target="_blank">{{row.item.name}}</b-link>
          </template>
          <template slot="win_rate" slot-scope="row">
            <b-progress height="1.5rem" :value="row.item.win_rate" :min="0" :max="1" :striped="true" show-progress></b-progress>
          </template>
          <template slot="wins" slot-scope="row">
            <b-progress variant="warning" height="1.5rem" :value="row.item.wins" :min="0" :max="1" :striped="true"></b-progress>
          </template>
          <template slot="picks" slot-scope="row">
            <b-progress variant="warning" height="1.5rem" :value="row.item.picks" :min="0" :max="1" :striped="true"></b-progress>
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
        <b-pagination align="center" :total-rows="topPicksAbilities.totalRows" :per-page="topPicksAbilities.perPage" v-model="topPicksAbilities.currentPage" />
      </b-col>
    </b-row>
    -->
  </section>
  <section v-else class="text-center">
    <hgv-loader :color="'#ffc107'"></hgv-loader>
  </section>
</template>

<script>
import axios from 'axios'
import VueSlider from 'vue-slider-component'

export default {
  name: 'DraftLive',
  components: {
    VueSlider
  },
  data () {
    const fields = [
      // { key: 'actions', label: '', sortable: false },
      { key: 'icon', label: 'Icon', sortable: false },
      // { key: 'link', label: 'Ability', sortable: true },
      { key: 'ultimate', label: 'Ultimate', sortable: true },
      { key: 'upgrade', label: 'Aghanims', sortable: true },
      { key: 'picks', label: 'Most Picks', sortable: true },
      { key: 'wins', label: 'Most Wins', sortable: true },
      { key: 'win_rate', label: 'Win Rate', sortable: true }
    ]

    return {
      'ready': false,
      'hero': null,
      'attributes': null,
      'abilities': [],
      'pool': [],
      'filter': [],
      'talents': [],
      'unique': [],
      'stats': null,
      'tables': {
        'search': null,
        'filter': null,
        'limit': 50
      },
      'topPicksHero': {
        'items': [],
        'fields': fields,
        'sortBy': 'win_rate',
        'sortDesc': true,
        'totalRows': 0,
        'currentPage': 1,
        'perPage': 10
      }
    }
  },
  created: function () {
    // let roster = this.$route.query.roster
    // let heroes = roster.split(',').map(_ => parseInt(_))
    
    // console.log('Hero', heroId)
    // console.log('Roster', heroes)
    // console.log('Skills', abilities)

    const vm = this

    let id = parseInt(this.$route.query.hero)
    let pool = this.$route.query.skills.split(',').map(_ => parseInt(_))

    let web1 = [
      axios.get('/static/data/heroes/' + id + '/hero.json').then((reponse) => { return reponse.data }),
      axios.get('/static/data/heroes/' + id + '/stats.json').then((reponse) => { return reponse.data }),
      axios.get('/static/data/heroes/' + id + '/abilities.json').then((reponse) => { return reponse.data }),
      axios.get('/static/data/heroes/' + id + '/attributes.json').then((reponse) => { return reponse.data }),
      axios.get('/static/data/abilities/collection.json').then((reponse) => { return reponse.data })
    ]

    Promise.all(web1).then((values) => {
      let hero = values[0] 
      let stat = values[1] 
      let heroAbilities = values[2]
      let attributes = values[3]
      // let abilities = values[4]
      // abilities = abilities.filter(_ => pool.includes(_.id))

      let talents = []

      talents.push({ level: 10, option1: hero.talents[0], option2: hero.talents[1] })
      talents.push({ level: 15, option1: hero.talents[2], option2: hero.talents[3] })
      talents.push({ level: 20, option1: hero.talents[4], option2: hero.talents[5] })
      talents.push({ level: 25, option1: hero.talents[6], option2: hero.talents[7] })

      for (let i = 0; i < hero.talents.length; i++) {
        const talent = hero.talents[i]
        if (talent.key.includes('unique')) {
          vm.unique.push(talent)
        }
      }

      vm.pool = pool
      vm.talents = talents
      vm.attributes = attributes
      vm.hero = hero
      vm.stats = stat
      vm.topPicksHero.items = heroAbilities
      vm.topPicksHero.totalRows = heroAbilities.length
      vm.ready = true
    }).catch(function (err) {
      console.log('Error', err)
      // vm.$router.push('/error')
    })
  },
  computed: {
    computedTopPicksHero: function () {
      let items = this.topPicksHero.items.filter(_ => this.pool.includes(_.id))

      if (this.tables.search) {
        items = items.filter(_ => _.name.toLowerCase().includes(this.tables.search.toLowerCase()))
      } else {
        items = items
          .filter(_ => _.win_rate > (this.tables.limit / 100))
          .filter(_ => this.filter.includes(_.id) === false)

        if (this.tables.filter) {
          if (this.tables.filter === 'aghanims') {
            items = items.filter(_ => _.has_upgrade)
          } else if (this.tables.filter === 'ultimate') {
            items = items.filter(_ => _.is_ultimate)
          } else {
            let filter = this.tables.filter.split(',')
            items = items.filter(_ => filter.map(f => _.desc.includes(f)).reduce((acc, val) => acc || val))
          }
        }
      }

      items = items.sort((lhs, rhs) => {
        if (this.topPicksHero.sortBy === 'link') {
          return this.topPicksHero.sortDesc ? lhs.name.localeCompare(rhs.name) : rhs.name.localeCompare(lhs.name)
        } else if (this.topPicksHero.sortBy === 'win_rate') {
          return this.topPicksHero.sortDesc ? rhs.win_rate - lhs.win_rate : lhs.win_rate - rhs.win_rate
        } else if (this.topPicksHero.sortBy === 'picks') {
          return this.topPicksHero.sortDesc ? rhs.picks - lhs.picks : lhs.picks - rhs.picks
        } else if (this.topPicksHero.sortBy === 'wins') {
          return this.topPicksHero.sortDesc ? rhs.wins - lhs.wins : lhs.wins - rhs.wins
        } else if (this.topPicksHero.sortBy === 'ultimate') {
          return this.topPicksHero.sortDesc ? rhs.win_rate - lhs.win_rate : lhs.win_rate - rhs.win_rate
        } else if (this.topPicksHero.sortBy === 'upgrade') {
          return this.topPicksHero.sortDesc ? rhs.win_rate - lhs.win_rate : lhs.win_rate - rhs.win_rate
        } else {
          return 0
        }
      })

      return items
    }
  },
  methods: {
    onTopPicksHeroFiltered (filteredItems) {
      this.topPicksHero.totalRows = filteredItems.length
      this.topPicksHero.currentPage = 1
    },
    removeAbility (id) {
      this.filter.push(id)
    },
    draftAbility (id) {
      console.log('Ability', id)
    }
  }
}
</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped>
.sticky-abilities{
  padding:0px;
  margin:0px;
  position:fixed;
  top:230px;
  width:210px;
  z-index: 1100;
}
</style>
