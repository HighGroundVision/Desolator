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
          Lorem ipsum dolor sit amet, consectetur adipiscing elit. Donec eget tempus quam, quis posuere massa. Phasellus ultricies iaculis sapien. Suspendisse tristique odio ullamcorper laoreet vehicula. Duis a ex ut orci pretium blandit efficitur vitae sem. Nullam interdum lobortis mauris ut lacinia. Sed eros ligula, porta sed sapien sed, tempor dapibus nisl. Phasellus turpis massa, rhoncus non ligula sit amet, posuere ultrices elit. Aliquam erat volutpat.
        </p>
      </b-col>
    </b-row>
    <b-row>
      <b-col cols="4">
        <h2>{{ability.name}}</h2>
      </b-col>
      <b-col>
          <b-img :src="ability.image" rounded class="ability-icon-md"></b-img>
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
    <br />
    <b-row>
      <b-col>
        <h4 class="text-center">Top Combos</h4>
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
        </b-table>
      </b-col>
      <b-col>
        <h4 class="text-center">Top Heroes</h4>
        <b-table :fields="heroes.fields" :items="computedHeroes" :sort-by.sync="heroes.sortBy" :sort-desc.sync="heroes.sortDesc" >
          <template slot="icon" slot-scope="row">
              <b-img :src="row.item.img" class="hero-icon-banner-sm" />
          </template>
          <template slot="link" slot-scope="row">
              <b-link :to="'/hero/' + row.item.id" target="_blank">{{row.item.name}}</b-link>
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
      { key: 'icon', label: 'Icon', sortable: false, variant: 'light' },
      { key: 'link', label: 'Hero', sortable: true, variant: 'light' },
      { key: 'wins', label: 'Wins', sortable: true, variant: 'light' },
      { key: 'picks', label: 'Picks', sortable: true, variant: 'light' },
      { key: 'win_rate_progress', label: 'Win Rate', sortable: true, variant: 'light' }
    ]

    const abilityFields = [
      { key: 'icon', label: 'Icon', sortable: false, variant: 'light' },
      { key: 'link', label: 'Ability', sortable: true, variant: 'light' },
      { key: 'wins', label: 'Wins', sortable: true, variant: 'light' },
      { key: 'picks', label: 'Picks', sortable: true, variant: 'light' },
      { key: 'win_rate_progress', label: 'Win Rate', sortable: true, variant: 'light' }
    ]

    return {
      'ready': false,
      'ability': null,
      'stats': null,
      'heroes': {
        'items': [],
        'fields': heroFields,
        'sortBy': 'win_rate_progress',
        'sortDesc': true
      },
      'combos': {
        'items': [],
        'fields': abilityFields,
        'sortBy': 'win_rate_progress',
        'sortDesc': true
      }
    }
  },
  created: function () {
    const vm = this

    let id = this.$route.params.id

    let web = [
      axios.get('/static/data/abilities/' + id + '/ability.json').then((reponse) => { return reponse.data }),
      axios.get('/static/data/abilities/' + id + '/stats.json').then((reponse) => { return reponse.data }),
      axios.get('/static/data/abilities/' + id + '/heroes.json').then((reponse) => { return reponse.data }),
      axios.get('/static/data/abilities/' + id + '/combos.json').then((reponse) => { return reponse.data })
    ]

    Promise.all(web).then((values) => {
      let details = values[0]
      let stats = values[1]
      let heroes = values[2]
      let combos = values[3]
      
      vm.ability = details
      vm.stats = stats
      vm.heroes.items = heroes
      vm.combos.items = combos
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

