<template>
  <section class="opaque-background">
    <b-row>
      <b-col md="2" lg="1" >
        <p class="header">Filter</p>
      </b-col>
      <b-col  md="4" lg="5">
        <b-form-input v-model="filterByAbility" placeholder="By Ability" />
      </b-col>
      <b-col md="4" lg="5">
        <b-form-select v-model="filterByType" :options="typeOptions">
          <option slot="first" :value="null">By Type</option>
        </b-form-select>
      </b-col>
      <b-col md="2" lg="1">
        <b-btn @click="resetModel">Clear</b-btn>
      </b-col>
    </b-row>
    <br />
    <b-row>
      <b-col>
        <b-table :fields="fields" :items="computedItems" :sort-by.sync="sortBy" :sort-desc.sync="sortDesc" :current-page="currentPage" :per-page="perPage">
          <template slot="icon" slot-scope="row">
            <b-img :src="row.item.img" fluid />
          </template>
          <template slot="win_rate_progress" slot-scope="row">
            <b-progress height="2rem" :value="round(row.item.win_rate)" :min="0" :max="100" :striped="true" show-progress></b-progress>
          </template>
          <template slot="link" slot-scope="row">
            <b-link :to="'/stats/' + row.item.id">{{row.item.name}}</b-link>
          </template>
          <template slot="type" slot-scope="row">
            <div v-if="row.item.type === 1">
              <b-img src="/static/images/type_melee.png" title="Melee" /> Melee
            </div>
            <div v-if="row.item.type === 2">
              <b-img src="/static/images/type_range.png" title="Range" /> Range
            </div>
            <div v-if="row.item.type === 3">
              <b-img src="/static/images/primary_str.png" title="Str" /> Str
            </div>
            <div v-if="row.item.type === 4">
              <b-img src="/static/images/primary_agi.png" title="Agi" /> Agi
            </div>
            <div v-if="row.item.type === 5">
              <b-img src="/static/images/primary_int.png" title="Int"  /> Int
            </div>
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
</template>

<script>
import abilitiesDB from '@/data/abilities.json'
import statsDB from '@/data/stats-ability.json'

export default {
  name: 'AbilityList',
  data () {
    let items = statsDB

    const fields = [
      { key: 'icon', label: '', sortable: false },
      { key: 'link', label: 'Ability', sortable: true },
      { key: 'type', label: 'Type', sortable: true },
      { key: 'win_rate_progress', label: 'Win Rate', sortable: true },
      { key: 'win_vs_picks', label: 'Win / Pick', sortable: true }
    ]

    for (let index = 0; index < items.length; index++) {
      const abilityId = parseInt(items[index].abilities)
      const ability = abilitiesDB[abilityId]
      items[index].id = abilityId
      items[index].name = ability.dname
      items[index].img = ability.img
      items[index].win_vs_picks = items[index].wins + ' / ' + items[index].picks
    }

    const typeOptions = [
      {text: 'Melee', value: 1},
      {text: 'Range', value: 2},
      {text: 'Str', value: 3},
      {text: 'Agi', value: 4},
      {text: 'Int', value: 5}
    ]

    return {
      'currentPage': 1,
      'perPage': 10,
      'totalRows': items.length,
      'pageOptions': [ 10, 50, 100 ],
      'sortBy': 'win_rate',
      'sortDesc': true,
      'fields': fields,
      'items': items,
      'filterByAbility': null,
      'filterByType': null,
      'typeOptions': typeOptions
    }
  },
  computed: {
    computedItems: function () {
      let items = this.items

      if (this.filterByAbility) {
        items = items.filter((lhs) => {
          return lhs.name.toLowerCase().includes(this.filterByAbility.toLowerCase())
        })
      }

      if (this.filterByType) {
        items = items.filter((lhs) => {
          return lhs.type === this.filterByType
        })
      }

      items = items.sort((lhs, rhs) => {
        if (this.sortBy === 'link') {
          return this.sortDesc ? lhs.name.localeCompare(rhs.name) : rhs.name.localeCompare(lhs.name)
        } else if (this.sortBy === 'type') {
          return this.sortDesc ? rhs.type - lhs.type : lhs.type - rhs.type
        } else if (this.sortBy === 'win_rate_progress') {
          return this.sortDesc ? rhs.win_rate - lhs.win_rate : lhs.win_rate - rhs.win_rate
        } else if (this.sortBy === 'win_vs_picks') {
          return this.sortDesc ? rhs.picks - lhs.picks : lhs.picks - rhs.picks
        } else {
          return 0
        }
      })

      return items
    }
  },
  methods: {
    resetModel: function () {
      this.filterByAbility = null
      this.filterByType = null
    },
    round: function (stat) {
      return Math.round(stat * 100)
    },
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
.header {
  font-size: 1.6em;
  font-weight: bold;
}
</style>
