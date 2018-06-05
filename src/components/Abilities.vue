<template>
  <section v-if="ready" class="opaque-background">
    <b-row>
      <b-col>
        <h1 class="text-warning">Ability Stats</h1>
      </b-col>
    </b-row>
    <b-row>
      <b-col>
        <p>
        We have compiled win rates since 7.07 patch using random sampling every day. 
        Ability pairs are created by combining all the skills with the 5 types (Str, Agi, Int, Melee, Range) to create ~2000 pairs.
        Below we highlight the 5 best abilities under each category by win rate.
        </p>
      </b-col>
    </b-row>
    <b-row class="text-center">
      <b-col v-for="item in top" :key="item.key">
        <b-img :src="item.img" :title="item.key" />
        <p>{{item.key}}</p>
        <ul class="list-unstyled">
          <li v-for="stat in item.stats" :key="stat.abilities">
            <b-img :src="stat.img" class="ability-icon-sm" />
            <br />
            <b-link :to="'/stats/ability/' + stat.id">
              {{stat.name}}
            </b-link>
          </li>
        </ul>
      </b-col>
    </b-row>
    <b-row>
      <b-col>
        <p>
          We have compiled the complete list of ability + type Pairs with the wins, picks, and win rate.
          You can filter by ability name and/or combination of type.
        </p>
        <p>
          You can click on the ability name to see more details about that ability.
          This includes the break down of stats by type. Also, the top combos with this ability by type. As well as, the ability rank and share these abilities with the community.
        </p>
      </b-col>
    </b-row>
    <b-row class="text-center">
      <b-col cols="2" lg="1">
        <h2>Filter</h2>
      </b-col>
      <b-col>
        <b-form-input v-model="filterByAbility" placeholder="By Ability"  />
      </b-col>
      <b-col>
        <b-form-checkbox-group v-model="filterByType" name="filterByType" :options="filterByTypeOptions">
        </b-form-checkbox-group>
      </b-col>
      <b-col cols="2" lg="1">
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
            <b-link :to="'/stats/ability/' + row.item.id">{{row.item.name}}</b-link>
          </template>
          <template slot="win_rate_progress" slot-scope="row">
            <b-progress height="2rem" :value="row.item.win_rate" :min="0" :max="100" :striped="true" show-progress></b-progress>
          </template>
          <template slot="type" slot-scope="row">
            <div v-if="row.item.type === 1">
              <b-img src="https://hgv-hyperstone.azurewebsites.net/mics/type_melee.png" title="Melee" /> Melee
            </div>
            <div v-if="row.item.type === 2">
              <b-img src="https://hgv-hyperstone.azurewebsites.net/mics/type_range.png" title="Range" /> Range
            </div>
            <div v-if="row.item.type === 3">
              <b-img src="https://hgv-hyperstone.azurewebsites.net/mics/primary_str.png" title="Str" /> Str &nbsp; &nbsp;
            </div>
            <div v-if="row.item.type === 4">
              <b-img src="https://hgv-hyperstone.azurewebsites.net/mics/primary_agi.png" title="Agi" /> Agi &nbsp; &nbsp;
            </div>
            <div v-if="row.item.type === 5">
              <b-img src="https://hgv-hyperstone.azurewebsites.net/mics/primary_int.png" title="Int"  /> Int &nbsp; &nbsp;
            </div>
          </template>
          <template slot="ranking" slot-scope="row">
            <span v-if="row.item.ranking">{{row.item.ranking}}</span>
            <span v-else>--</span>
          </template>
          <template slot="votes" slot-scope="row">
            <span v-if="row.item.votes">{{row.item.votes}}</span>
            <span v-else>--</span>
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
  <section v-else class="opaque-background text-center">
    <hgv-loader :color="'#ffc107'"></hgv-loader>
  </section>
</template>

<script>
import axios from 'axios'

export default {
  name: 'AbilityList',
  data () {
    const fields = [
      { key: 'icon', label: 'Icon', sortable: false },
      { key: 'link', label: 'Ability', sortable: true },
      { key: 'type', label: 'Type', sortable: true },
      { key: 'win_rate_progress', label: 'Win Rate', sortable: true },
      { key: 'win_vs_picks', label: 'Win / Pick', sortable: false },
      { key: 'ranking', label: 'Karma', sortable: true },
      { key: 'votes', label: 'Votes (+/-)', sortable: false }
    ]

    const filterByTypeOptions = [
      {text: 'Melee', value: 1},
      {text: 'Range', value: 2},
      {text: 'Str', value: 3},
      {text: 'Agi', value: 4},
      {text: 'Int', value: 5}
    ]

    return {
      'ready': false,
      'top': [],
      'currentPage': 1,
      'perPage': 10,
      'totalRows': 0,
      'pageOptions': [ 10, 50, 100 ],
      'sortBy': 'win_rate_progress',
      'sortDesc': true,
      'fields': fields,
      'items': [],
      'filterByAbility': null,
      'filterByType': [1, 2, 3, 4, 5],
      'filterByTypeOptions': filterByTypeOptions
    }
  },
  created: function () {
    const vm = this

    let p1 = axios.get('/static/data/stats-abilities.json').then((reponse) => { return reponse.data })
    let p2 = axios.get('/static/data/abilities.json').then((reponse) => { return reponse.data })
    let p3 = axios.get(process.env.API_BASE_URL + 'AbilityRanking').then((reponse) => { return reponse.data })

    Promise.all([p1, p2, p3]).then((values) => {
      let items = values[0]
      let abilitiesDB = values[1]
      let rankingAbilityDB = values[2]

      for (let index = 0; index < items.length; index++) {
        const abilityId = parseInt(items[index].abilities)
        const ability = abilitiesDB[abilityId]
        items[index].id = abilityId
        items[index].name = ability.dname
        items[index].img = ability.img
        items[index].win_vs_picks = items[index].wins + ' / ' + items[index].picks

        const ranking = rankingAbilityDB[abilityId]
        items[index].ranking = ranking ? ranking.total : ''
        items[index].votes = ranking ? ranking.up + ' / ' + ranking.down : ''
      }

      const imageUrl = 'https://hgv-hyperstone.azurewebsites.net/mics/'
      let top = []

      items.sort(function (lhs, rhs) { return rhs.win_rate - lhs.win_rate })
      top.push({ 'key': 'Melee', 'img': imageUrl + 'type_melee.png', 'stats': items.filter(s => s.type === 1).slice(0, 5) })
      top.push({ 'key': 'Range', 'img': imageUrl + 'type_range.png', 'stats': items.filter(s => s.type === 2).slice(0, 5) })
      top.push({ 'key': 'Str', 'img': imageUrl + 'primary_str.png', 'stats': items.filter(s => s.type === 3).slice(0, 5) })
      top.push({ 'key': 'Agi', 'img': imageUrl + 'primary_agi.png', 'stats': items.filter(s => s.type === 4).slice(0, 5) })
      top.push({ 'key': 'Int', 'img': imageUrl + 'primary_int.png', 'stats': items.filter(s => s.type === 5).slice(0, 5) })

      // NOTE: The type (type === 1) dose not really matter just filter the items down to single set
      let rankings = items.filter(s => s.type === 1 && s.ranking).sort(function (lhs, rhs) { return rhs.ranking - lhs.ranking })
      top.push({ 'key': 'Karma', 'img': imageUrl + 'attribute_bonus.png', 'stats': rankings.slice(0, 5) })

      vm.totalRows = items.length
      vm.items = items
      vm.top = top
      vm.ready = true
    }).catch(function () {
      vm.$router.push('/error')
    })
  },
  computed: {
    computedItems: function () {
      let items = this.items

      if (this.filterByAbility) {
        items = items.filter((lhs) => {
          return lhs.name.toLowerCase().includes(this.filterByAbility.toLowerCase())
        })
      }

      items = items.filter((lhs) => {
        return this.filterByType.includes(lhs.type)
      })

      items = items.sort((lhs, rhs) => {
        if (this.sortBy === 'link') {
          return this.sortDesc ? lhs.name.localeCompare(rhs.name) : rhs.name.localeCompare(lhs.name)
        } else if (this.sortBy === 'type') {
          return this.sortDesc ? rhs.type - lhs.type : lhs.type - rhs.type
        } else if (this.sortBy === 'win_rate_progress') {
          return this.sortDesc ? rhs.win_rate - lhs.win_rate : lhs.win_rate - rhs.win_rate
        } else if (this.sortBy === 'ranking') {
          return this.sortDesc ? rhs.ranking - lhs.ranking : lhs.ranking - rhs.ranking
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
      this.filterByAbility = null
      this.filterByType = [1, 2, 3, 4, 5]
    },
    format (stat) {
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
</style>
