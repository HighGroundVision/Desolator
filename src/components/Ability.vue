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
      <b-col>
        <div>
          <h2>{{ability.name}}</h2>
        </div>
        <div>
          <b-img :src="ability.image" rounded class="ability-icon-lg"></b-img>
        </div>
      </b-col>
      <b-col>
         {{ability.description}}
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
    <br />
    <b-row>
      <b-col>
         <ul class="list-group">
            <li class="list-group-item">
              <b-row>
                <b-col>
                  <span></span>
                </b-col>
                <b-col cols="2">
                  <span></span>
                </b-col>
              </b-row>
            </li>
         </ul>
      </b-col>
    </b-row>
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
    </b-row>
  </section>
  <section v-else class="text-center">
    <hgv-loader :color="'#ffc107'"></hgv-loader>
  </section>
</template>

<script>
import axios from 'axios'

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
    resetModel () {
    }
  }
}
</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped>
</style>

