<template>
  <section v-if="ready" >
    <b-row>
      <b-col>
        <h2 class="text-center text-warning">Abilities</h2>
      </b-col>
    </b-row>
    <hr class="highlighted" />
    <b-row class="text-center">
      <b-col cols="2">
        <h4>Filter</h4>
      </b-col>
      <b-col cols="8">
        <b-form-input v-model="filter" placeholder="By Ability"  />
      </b-col>
      <b-col cols="2">
        <b-btn @click="resetModel">Clear</b-btn>
      </b-col>
    </b-row>
    <br />
    <b-row class="text-center">
      <b-col>
        <b-table :fields="fields" :items="computedItems" 
            :sort-by.sync="sortBy" :sort-desc.sync="sortDesc" 
            :current-page="currentPage" :per-page="perPage"
            @filtered="onFiltered">
          <template slot="icon" slot-scope="row">
            <b-img :src="row.item.img" class="ability-icon-sm" />
          </template>
          <template slot="link" slot-scope="row">
            <b-link :to="'/ability/' + row.item.id">{{row.item.name}}</b-link>
          </template>
          <template slot="win_rate_progress" slot-scope="row">
            <b-progress height="2rem" :value="row.item.win_rate" :min="0" :max="1" :striped="true" show-progress></b-progress>
          </template>
          <template slot="wins" slot-scope="row">
            <b-progress height="2rem" variant="warning" :value="row.item.wins" :min="0" :max="1" :striped="true" ></b-progress>
          </template>
          <template slot="picks" slot-scope="row">
            <b-progress height="2rem" variant="info" :value="row.item.picks" :min="0" :max="1" :striped="true" ></b-progress>
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
    <b-row>
      <b-col>
        <b-pagination align="center" :total-rows="totalRows" :per-page="perPage" v-model="currentPage" />
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
  name: 'AbilitiesCollection',
  data () {
    const fields = [
      { key: 'icon', label: '', sortable: false },
      { key: 'link', label: 'Ability', sortable: true },
      { key: 'ultimate', label: 'Ultimate', sortable: true },
      { key: 'upgrade', label: 'Aghanims', sortable: true },
      { key: 'wins', label: 'Wins', sortable: true },
      { key: 'picks', label: 'Picks', sortable: true },
      { key: 'win_rate_progress', label: 'Win Rate', sortable: true }
    ]

    return {
      'ready': false,
      'currentPage': 1,
      'perPage': 10,
      'totalRows': 0,
      'pageOptions': [ 10, 50, 100 ],
      'sortBy': 'win_rate_progress',
      'sortDesc': true,
      'fields': fields,
      'items': [],
      'filter': null
    }
  },
  created: function () {
    const vm = this

    let web = [
      axios.get('/static/data/abilities/collection.json').then((reponse) => { return reponse.data })
    ]

    Promise.all(web).then((values) => {
      let items = values[0]

      vm.totalRows = items.length
      vm.items = items
      vm.ready = true
    }).catch(function () {
      vm.$router.push('/error')
    })
  },
  computed: {
    computedItems: function () {
      let items = this.items

      if (this.filter) {
        items = items.filter((lhs) => {
          return lhs.name.toLowerCase().includes(this.filter.toLowerCase())
        })
      }

      items = items.sort((lhs, rhs) => {
        if (this.sortBy === 'link') {
          return this.sortDesc ? lhs.name.localeCompare(rhs.name) : rhs.name.localeCompare(lhs.name)
        } else if (this.sortBy === 'win_rate_progress') {
          return this.sortDesc ? rhs.win_rate - lhs.win_rate : lhs.win_rate - rhs.win_rate
        } else if (this.sortBy === 'picks') {
          return this.sortDesc ? rhs.picks - lhs.picks : lhs.picks - rhs.picks
        } else if (this.sortBy === 'wins') {
          return this.sortDesc ? rhs.wins - lhs.wins : lhs.wins - rhs.wins
        } else if (this.sortBy === 'ultimate') {
          return this.sortDesc ? lhs.is_ultimate - rhs.is_ultimate : rhs.is_ultimate - lhs.is_ultimate
        } else if (this.sortBy === 'upgrade') {
          return this.sortDesc ? lhs.has_upgrade - rhs.has_upgrade : rhs.has_upgrade - lhs.has_upgrade 
        } else {
          return 0
        }
      })

      return items
    }
  },
  methods: {
    onFiltered (filteredItems) {
      this.totalRows = filteredItems.length
      this.currentPage = 1
    },
    resetModel () {
      this.filter = null
    }
  }
}
</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped>
</style>
