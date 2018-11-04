<template>
  <section v-if="ready" >
    <b-row>
      <b-col>
        <h2 class="text-center text-warning">Heroes</h2>
      </b-col>
    </b-row>
    <hr class="highlighted" />
    <b-row>
      <b-col class="text-center">
        <b-row>
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
        </b-row>
        <br />
        <b-row>
          <b-col>
            <h5><b-img src="https://hgv-hyperstone.azurewebsites.net/mics/type_melee.png" title="Melee" rounded class="ability-icon-sm" /> Melee</h5>
            <b-progress height="1.5rem" :value="groups.melee" :min="0" :max="1" :striped="true" show-progress></b-progress>
          </b-col>
          <b-col>
            <h5><b-img src="https://hgv-hyperstone.azurewebsites.net/mics/type_range.png" title="Range" rounded class="ability-icon-sm" /> Range</h5>
            <b-progress height="1.5rem" :value="groups.range" :min="0" :max="1" :striped="true" show-progress></b-progress>
          </b-col>
          <b-col>
          </b-col>
        </b-row>
      </b-col>
      <b-col cols="4">
        <div class="text-center">
          <b-alert variant="info" show>
            <strong>Cluckles Says</strong><br />
            <p>We have averaged each heroes' win rate for primary stats and attack capabilities</p>
          </b-alert>
          <b-img src="/static/images/cluckles-speach.png" class="cluckles-speach" />
        </div>
      </b-col>
    </b-row>
    <hr class="highlighted" />
    <b-row class="text-center">
      <b-col cols="2">
        <h4>Filter</h4>
      </b-col>
      <b-col cols="8">
        <b-form-input v-model="filter" placeholder="By Hero"  />
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
            <b-img :src="row.item.image_banner" class="hero-icon-banner-sm" />
          </template>
          <template slot="link" slot-scope="row">
            <b-link :to="'/hero/' + row.item.id">{{row.item.name}}</b-link>
          </template>
          <template slot="win_rate_progress" slot-scope="row">
            <b-progress height="2rem" :value="row.item.win_rate" :min="0" :max="1" :striped="true" show-progress></b-progress>
          </template>
          <template slot="wins" slot-scope="row">
            <b-progress height="2rem" variant="warning" :value="row.item.wins" :min="0" :max="1" :striped="true"></b-progress>
          </template>
          <template slot="picks" slot-scope="row">
            <b-progress height="2rem" variant="warning" :value="row.item.picks" :min="0" :max="1" :striped="true" ></b-progress>
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
  name: 'HeroesCollection',
  data () {
    const fields = [
      { key: 'icon', label: '', sortable: false },
      { key: 'link', label: 'Hero', sortable: true },
      // { key: 'wins', label: 'Wins', sortable: true },
      // { key: 'picks', label: 'Picks', sortable: true },
      { key: 'win_rate_progress', label: 'Win Rate', sortable: true }
    ]

    return {
      'ready': false,
      'currentPage': 1,
      'perPage': 10,
      'totalRows': 0,
      'sortBy': 'win_rate_progress',
      'sortDesc': true,
      'fields': fields,
      'items': [],
      'filter': null,
      'groups': {
        'str': 0,
        'int': 0,
        'agi': 0,
        'melee': 0,
        'range': 0
      }
    }
  },
  created: function () {
    const average = (arr, o) => arr.reduce((a, i) => a + i[o], 0) / arr.length
    
    const vm = this

    let web = [
      axios.get('/static/data/heroes/collection.json').then((reponse) => { return reponse.data })
    ]

    Promise.all(web).then((values) => {
      let items = values[0]

      let strheroes = items.filter(_ => _.attribute_primary === 'DOTA_ATTRIBUTE_STRENGTH')
      let intheroes = items.filter(_ => _.attribute_primary === 'DOTA_ATTRIBUTE_INTELLECT')
      let agiheroes = items.filter(_ => _.attribute_primary === 'DOTA_ATTRIBUTE_AGILITY')
      let meleeheroes = items.filter(_ => _.attack_capabilities === 'DOTA_UNIT_CAP_MELEE_ATTACK')
      let rangeheroes = items.filter(_ => _.attack_capabilities === 'DOTA_UNIT_CAP_RANGED_ATTACK')

      vm.groups.str = average(strheroes, 'win_rate')
      vm.groups.int = average(intheroes, 'win_rate')
      vm.groups.agi = average(agiheroes, 'win_rate')
      vm.groups.melee = average(meleeheroes, 'win_rate')
      vm.groups.range = average(rangeheroes, 'win_rate')
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
