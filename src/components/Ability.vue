<template>
  <section v-if="ready" >
    <b-row>
      <b-col>
        <h2 class="text-warning">Ability Details</h2>
      </b-col>
    </b-row>
    <b-row>
      <b-col>
        <p>
           We have provided details about a ability, including it's description, standard attributes (like cooldown and mana cost) and custom attributes (unique to the ability).
           We have included the abilities pairs to can compare abiltiy combos by Wins, Picks, WinRate.
           We have included the heroes pairs to identify which heroes are strongest with this ability.
           We have included the most picked drafts for this abilitiy to identify which drafts are picked often and how well they fair. We will not include drafts that have a zero win rate. (even if they are picked alot) 
        </p>
      </b-col>
    </b-row>
    <b-row>
      <b-col cols="6">
        <h2>{{ability.name}}</h2>
      </b-col>
      <b-col>
          <b-img :src="ability.image" rounded class="ability-icon-md"></b-img>
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
      <b-col>
        <b-card>
          <b-card-body>
            {{ability.description}}
          </b-card-body>
        </b-card>
      </b-col>
      <b-col>
        <ul class="list-group">
            <li class="list-group-item">
              <b-row>
                <b-col>
                  <span>Spell Immunity</span>
                </b-col>
                <b-col>
                  <span v-if="ability.spell_immunity_type === 'SPELL_IMMUNITY_ENEMIES_YES'">Enemies Yes</span>
                  <span v-if="ability.spell_immunity_type === 'SPELL_IMMUNITY_ENEMIES_NO'">Enemies No</span>
                  <span v-if="ability.spell_immunity_type === 'SPELL_IMMUNITY_ALLIES_NO'">Allies No</span>
                  <span v-if="ability.spell_immunity_type === 'SPELL_IMMUNITY_ALLIES_YES'">Allies Yes</span>
                </b-col>
              </b-row>
            </li>
            <li class="list-group-item">
              <b-row>
                <b-col>
                  <span>Spell Dispellable</span>
                </b-col>
                <b-col>
                  <span></span>
                  <span v-if="ability.spell_dispellable_type === 'SPELL_DISPELLABLE_YES'">Yes</span>
                  <span v-if="ability.spell_dispellable_type === 'SPELL_DISPELLABLE_NO'">No</span>
                  <span v-if="ability.spell_dispellable_type === 'SPELL_DISPELLABLE_YES_STRONG'">Strong</span>
                </b-col>
              </b-row>
            </li>
            <li class="list-group-item">
              <b-row>
                <b-col>
                  <span>Ultimate</span>
                </b-col>
                <b-col>
                  <span v-if="ability.is_ultimate === true">Yes</span>
                  <span v-else>No</span>
                </b-col>
              </b-row>
            </li>
            <li class="list-group-item">
              <b-row>
                <b-col>
                  <span>Aghanim's Scepter</span>
                </b-col>
                <b-col>
                  <span v-if="ability.has_scepter_upgrade === true">Yes</span>
                  <span v-else>No</span>
                </b-col>
              </b-row>
            </li>
            <li class="list-group-item">
              <b-row>
                <b-col>
                  <span>Cooldown</span>
                </b-col>
                <b-col>
                  <span>{{format(ability.ability_cooldown)}}</span>
                </b-col>
              </b-row>
            </li>
            <li class="list-group-item">
              <b-row>
                <b-col>
                  <span>Mana Cost</span>
                </b-col>
                <b-col>
                  <span>{{format(ability.ability_mana_cost)}}</span>
                </b-col>
              </b-row>
            </li>
         </ul>
      </b-col>
      <b-col>
        <ul class="list-group">
          <template v-for="(item, key) in ability.ability_special">
            <li :key="key" class="list-group-item">
              <b-row>
                <b-col>
                  <span>
                    {{humanize(key)}}
                  </span>
                </b-col>
                <b-col>
                  <span>{{format(item)}}</span>
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
        <h4 class="text-center">Ability Combos</h4>
        <b-table 
          :fields="combos.fields" :items="computedCombos" 
          :sort-by.sync="combos.sortBy" :sort-desc.sync="combos.sortDesc"
          :current-page="combos.currentPage" :per-page="combos.perPage">
          <template slot="icon" slot-scope="row">
              <b-img :src="row.item.img" class="ability-icon-sm" />
          </template>
          <template slot="link" slot-scope="row">
              <b-link :to="'/ability/' + row.item.id" target="_blank">{{row.item.name}}</b-link>
          </template>
          <template slot="win_rate_progress" slot-scope="row">
              <b-progress x height="1.5rem" :value="row.item.win_rate" :min="0" :max="1" :striped="true" show-progress></b-progress>
          </template>
          <template slot="wins" slot-scope="row">
            <b-progress variant="warning" height="1.5rem" :value="row.item.wins" :min="0" :max="1" :striped="true" ></b-progress>
          </template>
          <template slot="picks" slot-scope="row">
            <b-progress variant="info" height="1.5rem" :value="row.item.picks" :min="0" :max="1" :striped="true" ></b-progress>
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
        <b-pagination align="center" :total-rows="combos.totalRows" :per-page="combos.perPage" v-model="combos.currentPage" />
      </b-col>
    </b-row>
    <hr class="highlighted" />
    <b-row>
      <b-col>
        <h4 class="text-center">Hero Stats</h4>
        <b-row class="text-center">
          <b-col>
            <h5>&nbsp;</h5>
            <span>Win Rate</span>
          </b-col>
          <b-col>
            <h5><b-img src="https://hgv-hyperstone.azurewebsites.net/mics/primary_str.png" title="Str" rounded class="ability-icon-sm" /> Str</h5>
            <b-progress height="1.5rem" :value="groups.str" :min="0" :max="1" :striped="true" show-progress></b-progress>
          </b-col>
          <b-col>
            <h5><b-img src="https://hgv-hyperstone.azurewebsites.net/mics/primary_agi.png" title="Agi" rounded class="ability-icon-sm" /> Agi</h5>
            <b-progress height="1.5rem" :value="groups.agi" :min="0" :max="1" :striped="true" show-progress></b-progress>
          </b-col>
          <b-col>
            <h5><b-img src="https://hgv-hyperstone.azurewebsites.net/mics/primary_int.png" title="Int" rounded class="ability-icon-sm" /> Int</h5>
            <b-progress height="1.5rem" :value="groups.int" :min="0" :max="1" :striped="true" show-progress></b-progress>
          </b-col>
          <b-col>
            <h5><b-img src="https://hgv-hyperstone.azurewebsites.net/mics/type_melee.png" title="Melee" rounded class="ability-icon-sm" /> Melee</h5>
            <b-progress height="1.5rem" :value="groups.melee" :min="0" :max="1" :striped="true" show-progress></b-progress>
          </b-col>
          <b-col>
            <h5><b-img src="https://hgv-hyperstone.azurewebsites.net/mics/type_range.png" title="Range" rounded class="ability-icon-sm" /> Range</h5>
            <b-progress height="1.5rem" :value="groups.range" :min="0" :max="1" :striped="true" show-progress></b-progress>
          </b-col>
        </b-row>
      </b-col>
    </b-row>
    <br />
    <b-row>
      <b-col>
        <h4 class="text-center">Hero Pairs</h4>
        <b-table 
          :fields="heroes.fields" :items="computedHeroes" 
          :sort-by.sync="heroes.sortBy" :sort-desc.sync="heroes.sortDesc" 
          :current-page="heroes.currentPage" :per-page="heroes.perPage">
          <template slot="icon" slot-scope="row">
              <b-img :src="row.item.image_banner" class="hero-icon-banner-sm" />
              <b-img v-if="row.item.attribute_primary === 'DOTA_ATTRIBUTE_STRENGTH'" src="https://hgv-hyperstone.azurewebsites.net/mics/primary_str.png" title="Str" rounded class="ability-icon-xs" />
              <b-img v-if="row.item.attribute_primary === 'DOTA_ATTRIBUTE_INTELLECT'" src="https://hgv-hyperstone.azurewebsites.net/mics/primary_int.png" title="Int" rounded class="ability-icon-xs" />
              <b-img v-if="row.item.attribute_primary === 'DOTA_ATTRIBUTE_AGILITY'" src="https://hgv-hyperstone.azurewebsites.net/mics/primary_agi.png" title="Agi" rounded class="ability-icon-xs" />
              <b-img v-if="row.item.attack_capabilities === 'DOTA_UNIT_CAP_MELEE_ATTACK'" src="https://hgv-hyperstone.azurewebsites.net/mics/type_melee.png" title="Melee" rounded class="ability-icon-xs" />
              <b-img v-if="row.item.attack_capabilities === 'DOTA_UNIT_CAP_RANGED_ATTACK'" src="https://hgv-hyperstone.azurewebsites.net/mics/type_range.png" title="Range" rounded class="ability-icon-xs" />
          </template>
          <template slot="link" slot-scope="row">
              <b-link :to="'/hero/' + row.item.id" target="_blank">{{row.item.name}}</b-link>
          </template>
          <template slot="win_rate_progress" slot-scope="row">
              <b-progress height="1.5rem" :value="row.item.win_rate" :min="0" :max="1" :striped="true" show-progress></b-progress>
          </template>
          <template slot="wins" slot-scope="row">
              <b-progress variant="warning" height="1.5rem" :value="row.item.wins" :min="0" :max="1" :striped="true" ></b-progress>
          </template>
          <template slot="picks" slot-scope="row">
              <b-progress variant="info" height="1.5rem" :value="row.item.picks" :min="0" :max="1" :striped="true"></b-progress>
          </template>
        </b-table>
        <b-pagination align="center" :total-rows="heroes.totalRows" :per-page="heroes.perPage" v-model="heroes.currentPage" />
      </b-col>
    </b-row>
    <hr class="highlighted" />
    <b-row>
      <b-col>
        <h4 class="text-center">Drafts</h4>
        <b-table 
          :fields="drafts.fields" :items="drafts.items" 
          :sort-by.sync="drafts.sortBy" :sort-desc.sync="drafts.sortDesc">
          <template slot="images" slot-scope="row">
            <b-img :src="row.item.images[0]" class="hero-icon-banner-sm" />
            <b-img :src="row.item.images[1]" class="hero-icon-banner-sm" />
            <b-img :src="row.item.images[2]" class="hero-icon-banner-sm" />
            <b-img :src="row.item.images[3]" class="hero-icon-banner-sm" />
          </template>
          <template slot="names" slot-scope="row">
            <span>{{row.item.abilties}}</span>
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

const humanizeFn = require('humanize-string')

export default {
  name: 'AbilityDetails',
  data () {
    const heroFields = [
      { key: 'icon', label: '', sortable: false },
      { key: 'link', label: 'Hero', sortable: true },
      { key: 'wins', label: 'Most Wins', sortable: true },
      { key: 'picks', label: 'Most Picks', sortable: true },
      { key: 'win_rate_progress', label: 'Win Rate', sortable: true }
    ]

    const abilityFields = [
      { key: 'icon', label: '', sortable: false },
      { key: 'link', label: 'Ability', sortable: true },
      { key: 'ultimate', label: 'Ultimate', sortable: true },
      { key: 'upgrade', label: 'Aghanims', sortable: true },
      { key: 'wins', label: 'Most Wins', sortable: true },
      { key: 'picks', label: 'Most Picks', sortable: true },
      { key: 'win_rate_progress', label: 'Win Rate', sortable: true }
    ]

    const draftsFields = [
      { key: 'images', label: '', sortable: false },
      { key: 'names', label: 'Abilities', sortable: false },
      // { key: 'wins', label: 'Wins', sortable: true },
      { key: 'picks', label: 'Picks', sortable: false },
      { key: 'win_rate_progress', label: 'Win Rate', sortable: false }
    ]

    return {
      'ready': false,
      'ability': null,
      'stats': null,
      'groups': {
        'str': 0,
        'int': 0,
        'agi': 0,
        'melee': 0,
        'range': 0
      },
      'heroes': {
        'items': [],
        'fields': heroFields,
        'sortBy': 'win_rate_progress',
        'sortDesc': true,
        'currentPage': 1,
        'perPage': 10,
        'totalRows': 0
      },
      'combos': {
        'items': [],
        'fields': abilityFields,
        'sortBy': 'win_rate_progress',
        'sortDesc': true,
        'currentPage': 1,
        'perPage': 10,
        'totalRows': 0
      },
      'drafts': {
        'items': [],
        'fields': draftsFields,
        'sortBy': 'picks',
        'sortDesc': true,
        'currentPage': 1,
        'perPage': 10,
        'totalRows': 0
      }
    }
  },
  created: function () {
    const average = (arr, o) => arr.reduce((a, i) => a + i[o], 0) / arr.length

    const vm = this

    let id = this.$route.params.id

    let web = [
      axios.get('/static/data/abilities/' + id + '/ability.json').then((reponse) => { return reponse.data }),
      axios.get('/static/data/abilities/' + id + '/stats.json').then((reponse) => { return reponse.data }),
      axios.get('/static/data/abilities/' + id + '/heroes.json').then((reponse) => { return reponse.data }),
      axios.get('/static/data/abilities/' + id + '/combos.json').then((reponse) => { return reponse.data }),
      axios.get('/static/data/abilities/' + id + '/drafts.json').then((reponse) => { return reponse.data })
    ]

    Promise.all(web).then((values) => {
      let details = values[0]
      let stats = values[1]
      let heroes = values[2]
      let combos = values[3]
      let drafts = values[4]

      let strheroes = heroes.filter(_ => _.attribute_primary === 'DOTA_ATTRIBUTE_STRENGTH')
      let intheroes = heroes.filter(_ => _.attribute_primary === 'DOTA_ATTRIBUTE_INTELLECT')
      let agiheroes = heroes.filter(_ => _.attribute_primary === 'DOTA_ATTRIBUTE_AGILITY')
      let meleeheroes = heroes.filter(_ => _.attack_capabilities === 'DOTA_UNIT_CAP_MELEE_ATTACK')
      let rangeheroes = heroes.filter(_ => _.attack_capabilities === 'DOTA_UNIT_CAP_RANGED_ATTACK')

      for (let i = 0; i < drafts.length; i++) {
        const draft = drafts[i]
        draft.images.sort((lhs, rhs) => { return lhs.includes(details.key) ? -1 : 1 })
      }

      let avgPicks = combos.reduce((ammulator, element) => ammulator + element.picks, 0) / combos.length
      let mostCombos = combos.filter(_ => _.picks >= avgPicks)

      vm.ability = details
      vm.stats = stats
      vm.heroes.items = heroes
      vm.heroes.totalRows = heroes.length
      vm.combos.items = mostCombos
      vm.combos.totalRows = mostCombos.length
      vm.drafts.items = drafts
      vm.drafts.totalRows = drafts.length
      vm.groups.str = average(strheroes, 'win_rate')
      vm.groups.int = average(intheroes, 'win_rate')
      vm.groups.agi = average(agiheroes, 'win_rate')
      vm.groups.melee = average(meleeheroes, 'win_rate')
      vm.groups.range = average(rangeheroes, 'win_rate')
      vm.ready = true
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
        } else if (this.combos.sortBy === 'picks') {
          return this.combos.sortDesc ? rhs.picks - lhs.picks : lhs.picks - rhs.picks
        } else if (this.combos.sortBy === 'wins') {
          return this.combos.sortDesc ? rhs.wins - lhs.wins : lhs.wins - rhs.wins
        } else if (this.combos.sortBy === 'ultimate') {
          return this.combos.sortDesc ? lhs.is_ultimate - rhs.is_ultimate : rhs.is_ultimate - lhs.is_ultimate
        } else if (this.combos.sortBy === 'upgrade') {
          return this.combos.sortDesc ? lhs.has_upgrade - rhs.has_upgrade : rhs.has_upgrade - lhs.has_upgrade 
        } else {
          return 0
        }
      })

      return items
    },
    computedHeroes: function () {
      let items = this.heroes.items

      items = items.sort((lhs, rhs) => {
        if (this.heroes.sortBy === 'link') {
          return this.heroes.sortDesc ? lhs.name.localeCompare(rhs.name) : rhs.name.localeCompare(lhs.name)
        } else if (this.heroes.sortBy === 'win_rate_progress') {
          return this.heroes.sortDesc ? rhs.win_rate - lhs.win_rate : lhs.win_rate - rhs.win_rate
        } else if (this.sortBy === 'picks') {
          return this.heroes.sortDesc ? rhs.picks - lhs.picks : lhs.picks - rhs.picks
        } else if (this.sortBy === 'wins') {
          return this.heroes.sortDesc ? rhs.wins - lhs.wins : lhs.wins - rhs.wins
        } else {
          return 0
        }
      })

      return items
    }
  },
  methods: {
    format (items) {
      let result = items.every((item) => item === items[0]) 
      if (result) {
        return items[0]
      } else {
        return items.join(' / ')
      }
    },
    humanize (value) {
      return humanizeFn(value).replace('tooltip', '')
    }
  }
}
</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped>
</style>

