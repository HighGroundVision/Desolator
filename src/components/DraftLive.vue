<template>
  <section v-if="ready" >
    <b-row>
      <b-col>
        <h2 class="text-warning">Drafting</h2>
      </b-col>
    </b-row>
    <b-row>
      <b-col>
        <p>
          We have provided details about the draft. 
          We have included the <span class="rounded bg-primary text-white"> Win Rate </span> , <span class="rounded bg-warning text-white"> Wins </span> and <span class="rounded bg-info text-white"> Picks </span> for the abilties in the draft pool. 
          Along with search, filter, and sort capabilities on the draft pool. We also include a threshold to hide ability that are below the threshold, it is compared against the current sort.    
          We have included the stats for the your hero as well as the stats for the general abilities.
          You can <span class="rounded bg-danger text-white"> ✖ </span> to remove an ability from the pool. You can also <span class="rounded bg-success text-white"> ✔ </span> to draft an ability to see its combos. You can use <span class="rounded bg-info text-white"> ? </span> to open a new page with details about this ability.
        </p>
      </b-col>
    </b-row>
    <b-row class="text-center">
      <b-col cols="6">
        <h3>{{hero.details.name}}</h3>
      </b-col>
      <b-col cols="3">
        <b-img :src="hero.details.image_banner" rounded class="hero-icon-banner"></b-img>
      </b-col>
      <b-col cols="3">
        <div>Win Rate</div>
        <b-progress height="2rem" :value="hero.stats.win_rate" :min="0" :max="1" striped show-progress></b-progress>
      </b-col>
    </b-row>
    <hr class="highlighted" />
    <b-row>
      <b-col md="3" class="p-1">
        <b-button-group>
          <b-button variant="primary" @click="switchWhich(true)" :pressed="abilities.which" title="">Hero</b-button>
          <b-button variant="primary" @click="switchWhich(false)" :pressed="!abilities.which" title="">Abilities</b-button>
          <!--<b-button variant="primary" :pressed="false" title="View Combos">Combos</b-button>-->
        </b-button-group>
      </b-col>
      <b-col md="3" class="p-1">
        <b-form-input v-model="abilities.search" placeholder="Search for Ability"></b-form-input>
      </b-col>
      <b-col md="2" class="p-1">
        <b-input-group prepend="Filter">
          <b-form-select v-model="abilities.filter">
            <template slot="first">
              <option :value="null">None</option>
            </template>
            <optgroup label="General">
              <option value="illusion,reflection">Illusions</option>
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
         </b-input-group>
      </b-col>
      <b-col md="2" class="p-1">
        <b-input-group prepend="Sort">
          <b-form-select v-model="abilities.sort">
            <option value="1">Win Rate</option>
            <option value="2">Wins</option>
            <option value="3">Picks</option>
          </b-form-select>
        </b-input-group>
      </b-col>
      <b-col md="2">
        <span>Threshold</span>
        <vue-slider ref="slider" v-model="abilities.limit" v-bind="{min: 0, max: 100}" ></vue-slider>
      </b-col>
    </b-row>
    <br />
    <b-row>
      <template v-for="item in computedAbilities">
        <b-col cols="12" sm="4" md="2" class="border border-light" v-bind:key="item.id">
          <div class="overlay-container" :title="item.name">
            <b-row>
              <b-col cols="2" md="4" class="p-0">
                <div class="p-1">
                  <b-img :src="item.img" class="rounded img-fluid" />
                </div>
              </b-col>
              <b-col cols="10" md="8" class="p-0">
                <div class="p-1">
                  <b-progress class="m-1" height="0.3rem" :value="item.win_rate" :min="0" :max="1" ></b-progress>
                  <b-progress class="m-1" height="0.3rem" variant="warning" :value="item.wins"  :min="0" :max="1"></b-progress>
                  <b-progress class="m-1" height="0.3rem" variant="info" :value="item.picks"  :min="0" :max="1"></b-progress>
                </div>
              </b-col>
            </b-row>
            <div class="overlay">
              <b-button-group class="text">
                <b-button variant="success" size="sm" @click="draftAbility(item.id)" title="Draft this ability for yourself">✔</b-button>
                <b-button variant="danger" size="sm" @click="removeAbility(item.id)" title="Remove this ability from the pool">✖</b-button>
                <b-button  :to="'/ability/' + item.id" target="_blank" variant="info" size="sm" title="More details about ability">?</b-button>                          
              </b-button-group>
            </div>
          </div>
        </b-col>
      </template>
    </b-row>
    <hr class="highlighted" />
    <h5 class="">Drafted Abilities <small>and their top 10 combos</small></h5>
    <b-row>
      <b-col>
        <template v-for="item in computedSelection">
          <b-row v-bind:key="item.id" class="p-1 text-center">
            <b-col cols="4" sm="2" md="1" class="bg-secondary border border-secondary">
              <img class="rounded img-fluid" :src="item.ability.image" :alt="item.ability.name" @click="rePoolAbility(item.ability.id)">
            </b-col>
            <template v-for="combo in item.combos">
              <b-col cols="4" sm="2" md="1" v-bind:key="combo.id">
                <div class="overlay-container">
                  <div>
                    <img class="img-fluid" :src="combo.img" :alt="combo.name">
                  </div>
                  <div class="overlay">
                    <b-button-group class="text">
                      <b-button variant="success" size="sm" @click="draftAbility(combo.id)" title="Draft this ability for yourself">✔</b-button>
                      <b-button variant="danger" size="sm" @click="removeAbility(combo.id)" title="Remove this ability from the pool">✖</b-button>
                      <b-button :to="'/ability/' + combo.id" target="_blank" variant="info" size="sm" title="More details about ability">?</b-button>
                    </b-button-group>
                  </div>
                </div>
              </b-col>
            </template>
          </b-row>
        </template>
      </b-col>
    </b-row>
    <hr class="highlighted" />
    <b-row>
      <b-col md="5">
        <h5 class="text-center">Attributes</h5>
        <ul class="list-group">
          <li class="list-group-item">
            <b-row>
              <b-col>
                <h5>Primary</h5>
              </b-col>
              <b-col cols="2">
                <b-img v-if="hero.details.attribute_primary === 'DOTA_ATTRIBUTE_STRENGTH'" src="https://hgv-hyperstone.azurewebsites.net/mics/primary_str.png" rounded class="ability-icon-sm" />
                <b-img v-if="hero.details.attribute_primary === 'DOTA_ATTRIBUTE_AGILITY'" src="https://hgv-hyperstone.azurewebsites.net/mics/primary_agi.png" rounded class="ability-icon-sm" />
                <b-img v-if="hero.details.attribute_primary === 'DOTA_ATTRIBUTE_INTELLECT'" src="https://hgv-hyperstone.azurewebsites.net/mics/primary_int.png" rounded class="ability-icon-sm" />
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
                    <span>{{hero.attributes.attribute_base_strength}}</span>
                  </b-col>
                  <b-col>
                    <b-progress variant="dark" :value="hero.attributes.ranking_base_strength" :min="0" :max="1" :striped="true" ></b-progress>
                  </b-col>
                </b-row>
                <b-row>
                  <b-col>
                    <span>Gain</span>
                  </b-col>
                  <b-col>
                    <span>{{hero.attributes.attribute_strength_gain}}</span>
                  </b-col>
                  <b-col>
                    <b-progress variant="dark" :value="hero.attributes.ranking_strength_gain" :min="0" :max="1" :striped="true" ></b-progress>
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
                    <span >{{hero.attributes.attribute_base_agility}}</span>
                  </b-col>
                  <b-col>
                    <b-progress variant="dark" :value="hero.attributes.ranking_base_agility" :min="0" :max="1" :striped="true"></b-progress>
                  </b-col>
                </b-row>
                <b-row>
                  <b-col>
                    <span>Gain</span>
                  </b-col>
                  <b-col>
                    <span>{{hero.attributes.attribute_agility_gain}}</span>
                  </b-col>
                  <b-col>
                    <b-progress variant="dark" :value="hero.attributes.ranking_agility_gain" :min="0" :max="1" :striped="true"></b-progress>
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
                    <span >{{hero.attributes.attribute_base_intelligence}}</span>
                  </b-col>
                  <b-col>
                    <b-progress variant="dark" :value="hero.attributes.ranking_base_intelligence" :min="0" :max="1" :striped="true"></b-progress>
                  </b-col>
                </b-row>
                <b-row>
                  <b-col>
                    <span>Gain</span>
                  </b-col>
                  <b-col>
                    <span>{{hero.attributes.attribute_intelligence_gain}}</span>
                  </b-col>
                  <b-col>
                    <b-progress variant="dark" :value="hero.attributes.ranking_intelligence_gain" :min="0" :max="1" :striped="true"></b-progress>
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
                    <span >{{hero.attributes.status_health}}</span>
                  </b-col>
                  <b-col>
                    <b-progress variant="dark" :value="hero.attributes.ranking_health" :min="0" :max="1" :striped="true"></b-progress>
                  </b-col>
                </b-row>
                <b-row>
                  <b-col>
                    <span>Regen</span>
                  </b-col>
                  <b-col>
                    <span>{{hero.attributes.status_health_regen}}</span>
                  </b-col>
                  <b-col>
                    <b-progress variant="dark" :value="hero.attributes.ranking_health_regen" :min="0" :max="1" :striped="true"></b-progress>
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
                    <span >{{hero.attributes.status_mana}}</span>
                  </b-col>
                  <b-col>
                    <b-progress variant="dark" :value="hero.attributes.ranking_mana" :min="0" :max="1" :striped="true"></b-progress>
                  </b-col>
                </b-row>
                <b-row>
                  <b-col>
                    <span>Regen</span>
                  </b-col>
                  <b-col>
                    <span>{{hero.attributes.status_mana_regen}}</span>
                  </b-col>
                  <b-col>
                    <b-progress variant="dark" :value="hero.attributes.ranking_mana_regen" :min="0" :max="1" :striped="true"></b-progress>
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
                    <span>{{hero.attributes.attack_range}}</span>
                  </b-col>
                  <b-col>
                    <b-progress variant="dark" :value="hero.attributes.ranking_attack_range" :min="0" :max="1" :striped="true"></b-progress>
                  </b-col>
                </b-row>
                <b-row>
                  <b-col>
                    <span>Damage</span>
                  </b-col>
                  <b-col>
                    <span >{{hero.attributes.attack_damage_min}} - {{hero.attributes.attack_damage_max}}</span>
                  </b-col>
                  <b-col>
                    <b-progress variant="dark" :value="hero.attributes.ranking_attack_damage" :min="0" :max="1" :striped="true"></b-progress>
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
                    <span>{{hero.attributes.movement_speed}}</span>
                  </b-col>
                  <b-col>
                    <b-progress variant="dark" :value="hero.attributes.ranking_movement_speed" :min="0" :max="1" :striped="true"></b-progress>
                  </b-col>
                </b-row>
                <b-row>
                  <b-col>
                    <span>Turn</span>
                  </b-col>
                  <b-col>
                    <span>{{hero.attributes.movement_turn_rate}}</span>
                  </b-col>
                  <b-col>
                    <b-progress variant="dark" :value="hero.attributes.ranking_movement_turn_rate" :min="0" :max="1" :striped="true"></b-progress>
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
                    <span>{{hero.attributes.vision_daytime_range}}</span>
                  </b-col>
                  <b-col>
                    <b-progress variant="dark" :value="hero.attributes.ranking_vision_daytime_range" :min="0" :max="1" :striped="true"></b-progress>
                  </b-col>
                </b-row>
                <b-row>
                  <b-col>
                    <span>Night</span>
                  </b-col>
                  <b-col>
                    <span>{{hero.attributes.vision_nighttime_range}}</span>
                  </b-col>
                  <b-col>
                    <b-progress variant="dark" :value="hero.attributes.ranking_vision_nighttime_range" :min="0" :max="1" :striped="true" ></b-progress>
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
          <template v-for="talent in hero.talents">
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
          <template v-for="talent in hero.unique">
            <li :key="talent.key" class="list-group-item">
              <b-row>
                <b-col>
                  <span class="invert" :title="talent.description">{{talent.name}}</span>
                </b-col>
                <b-col>
                  <div class="text-right">
                    <b-img :src="talent.img" class="ability-icon-sm" />
                  </div>
                </b-col>
                <b-col>
                  <b-progress height="2rem" :value="talent.win_rate" :min="0" :max="1" striped show-progress ></b-progress>
                </b-col>
              </b-row>
            </li>
          </template>
        </ul>       
      </b-col>
    </b-row>
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
    return {
      'ready': false,
      'skills': [],
      'selection': [],
      'hero': {
        'stats': null,
        'details': null,
        'attributes': null,
        'talents': [],
        'unique': []
      },
      'abilities': {
        'items': {
          'general': [],
          'hero': []
        },
        'which': true,
        'search': null,
        'filter': null,        
        'sort': '1',
        'limit': 45,
        'taken': []
      }
    }
  },
  created: function () {
    const vm = this

    let id = parseInt(this.$route.query.hero)
    let skills = this.$route.query.skills.split(',').map(_ => parseInt(_))

    let web1 = [
      axios.get('/static/data/heroes/' + id + '/hero.json').then((reponse) => { return reponse.data }),
      axios.get('/static/data/heroes/' + id + '/stats.json').then((reponse) => { return reponse.data }),
      axios.get('/static/data/heroes/' + id + '/attributes.json').then((reponse) => { return reponse.data }),
      axios.get('/static/data/heroes/' + id + '/abilities.json').then((reponse) => { return reponse.data }),
      axios.get('/static/data/abilities/collection.json').then((reponse) => { return reponse.data })
    ]

    Promise.all(web1).then((values) => {
      let hero = values[0] 
      let stat = values[1] 
      let attributes = values[2]
      let abilitiesHero = values[3]
      let abilitiesGeneral = values[4]

      let heroesAbilities = hero.abilities.filter(_ => _.ability_draft_enabled).map(_ => _.id)
      let abilities = abilitiesHero.filter(_ => heroesAbilities.includes(_.id))
      
      let talentsHero = []
      let talentsUnique = []
      talentsHero.push({ level: 10, option1: hero.talents[0], option2: hero.talents[1] })
      talentsHero.push({ level: 15, option1: hero.talents[2], option2: hero.talents[3] })
      talentsHero.push({ level: 20, option1: hero.talents[4], option2: hero.talents[5] })
      talentsHero.push({ level: 25, option1: hero.talents[6], option2: hero.talents[7] })

      for (let i = 0; i < hero.talents.length; i++) {
        const talent = hero.talents[i]
        if (talent.key.includes('unique')) {
          talent.img = 'https://hgv-hyperstone.azurewebsites.net/abilities/empty.png'
          talent.win_rate = 0

          for (const a of abilities) {
            if (talent.name.includes(a.name)) {
              talent.img = a.img
              talent.win_rate = a.win_rate
            }
          }
          talentsUnique.push(talent)
        }
      }

      vm.skills = skills
      vm.hero.talents = talentsHero
      vm.hero.unique = talentsUnique
      vm.hero.attributes = attributes
      vm.hero.details = hero
      vm.hero.stats = stat
      vm.abilities.items.hero = abilitiesHero.filter(_ => vm.skills.includes(_.id))
      vm.abilities.items.general = abilitiesGeneral.filter(_ => vm.skills.includes(_.id))
      vm.ready = true
    }).catch(function (err) {
      console.log('Error', err)
      // vm.$router.push('/error')
    })
  },
  computed: {
    computedAbilities: function () {
      let collection = this.abilities.which ? this.abilities.items.hero : this.abilities.items.general

      if (this.abilities.search) {
        collection = collection.filter(_ => _.name.toLowerCase().includes(this.abilities.search.toLowerCase()))
      } else {
        let property = (this.abilities.sort === '1') ? 'win_rate' : (this.abilities.sort === '2') ? 'wins' : (this.abilities.sort === '3') ? 'picks' : ''
        collection = collection
          .filter(_ => _[property] > (this.abilities.limit / 100))
          .filter(_ => this.abilities.taken.includes(_.id) === false)

        if (this.abilities.filter) {
          if (this.abilities.filter === 'aghanims') {
            collection = collection.filter(_ => _.has_upgrade)
          } else if (this.abilities.filter === 'ultimate') {
            collection = collection.filter(_ => _.is_ultimate)
          } else {
            let filter = this.abilities.filter.split(',')
            collection = collection.filter(_ => filter.map(f => _.desc.includes(f)).reduce((acc, val) => acc || val))
          }
        }
      }

      collection.sort((lhs, rhs) => {
        if (this.abilities.sort === '1') {
          return rhs.win_rate - lhs.win_rate
        } else if (this.abilities.sort === '2') {
          return rhs.wins - lhs.wins
        } else if (this.abilities.sort === '3') {   
          return rhs.picks - lhs.picks
        } else {
          return 0
        }
      })

      return collection
    },
    computedSelection: function () {
      let collection = []

      let drafted = this.selection.map(_ => _.ability.id)

      for (const item of this.selection) {
        let combos = item.combos
            .filter(_ => this.abilities.taken.includes(_.id) === false)
            .filter(_ => drafted.includes(_.id) === false)
            .slice(0, 10)

        if (this.abilities.filter) {
          if (this.abilities.filter === 'aghanims') {
            combos = combos.filter(_ => _.has_upgrade)
          } else if (this.abilities.filter === 'ultimate') {
            combos = combos.filter(_ => _.is_ultimate)
          } else {
            let filter = this.abilities.filter.split(',')
            combos = combos.filter(_ => filter.map(f => _.desc.includes(f)).reduce((acc, val) => acc || val))
          }
        }

        combos.sort((lhs, rhs) => {
          if (this.abilities.sort === '1') {
            return rhs.win_rate - lhs.win_rate
          } else if (this.abilities.sort === '2') {
            return rhs.wins - lhs.wins
          } else if (this.abilities.sort === '3') {   
            return rhs.picks - lhs.picks
          } else {
            return 0
          }
        })

        let data = {
          ability: item.ability,
          combos: combos
        }
        collection.push(data)
      }

      return collection
    }
  },
  methods: {
    switchWhich (value) {
      this.abilities.which = value
    },
    removeAbility (id) {
      this.abilities.taken.push(id)
    },
    draftAbility (id) {
      const vm = this

      let web2 = [
        axios.get('/static/data/abilities/' + id + '/ability.json').then((reponse) => { return reponse.data }),
        axios.get('/static/data/abilities/' + id + '/combos.json').then((reponse) => { return reponse.data })
      ]

      Promise.all(web2).then((values) => {
        let ability = values[0]
        let combos = values[1]

        combos = combos.filter(_ => this.skills.includes(_.id))
        combos.sort((lhs, rhs) => {
          return rhs.wins - lhs.wins
        })

        let data = {
          ability: ability,
          combos: combos
        }
        vm.selection.push(data)
        vm.abilities.taken.push(id)
      })
    },
    rePoolAbility (id) {
      this.abilities.taken = this.abilities.taken.filter(_ => _ !== id)
      this.selection = this.selection.filter(_ => _.ability.id !== id)
    }
  }
}
</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped>
.border-3 {
  border-width: 3px !important;
}
.overlay {
  position: absolute;
  bottom: 100%;
  left: 0;
  right: 0;
  background-color: rgba(128,128,128, 0.5);
  overflow: hidden;
  width: 100%;
  height:0;
  transition: .2s ease;
}

.overlay-container:hover > .overlay {
  bottom: 0;
  height: 100%;
}

.text {
  color: white;
  font-size: 20px;
  position: absolute;
  top: 50%;
  left: 50%;
  -webkit-transform: translate(-50%, -50%);
  -ms-transform: translate(-50%, -50%);
  transform: translate(-50%, -50%);
  text-align: center;
}
</style>
