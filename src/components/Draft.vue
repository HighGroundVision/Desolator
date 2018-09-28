<template>
  <section v-if="ready" class="text-center">
    <b-row>
      <b-col>
        <h2 class="text-warning">Drafting</h2>
      </b-col>
    </b-row>
    <hr class="highlighted" />
    <b-row>
      <b-col>
        <h4>Pool</h4>
        <p>Select the Roster (the 10 heroes displayed at the top of the drafting window) from the hero Pool.</p>
      </b-col>
    </b-row>
    <b-row>
      <b-col cols="2">
        <h4>Filter</h4>
      </b-col>
      <b-col cols="8">
        <b-form-input v-model="filter" placeholder="By Hero" />
      </b-col>
      <b-col cols="2">
        <b-btn @click="clear">Clear</b-btn>
      </b-col>
    </b-row>
    <b-row>
      <b-col>
        <template v-for="(item) in computedPool">
          <img @click="selectPool(item)" :key="item.id" :src="item.image_profile" class="hero-icon-profile-md m-1" />
        </template>
      </b-col>
    </b-row>
    <hr class="highlighted" />
    <b-row>
      <b-col>
        <h4>Roster</h4>
         <p v-if="fullRoster">Select your hero to proced to draft.</p>
      </b-col>
    </b-row>
    <b-row>
      <b-col>
        <template v-for="(item) in roster">
          <img @click="selectRoster(item)" :key="item.id" :src="item.image_profile" v-bind:class="{ 'border border-primary border-selected': selection === item.id }" class="hero-icon-profile-md m-1" />
        </template>
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
  name: 'Draft',
  data () {
    return {
      'ready': false,
      'filter': null,
      'pool': [],
      'roster': [],
      'selection': null
    }
  },
  created: function () {
    const vm = this

    let web = [
      axios.get('/static/data/heroes/collection.json').then((reponse) => { return reponse.data })
    ]

    Promise.all(web).then((values) => {
      let items = values[0]

      vm.pool = items
      vm.roster = []
      vm.ready = true
    }).catch(function () {
      vm.$router.push('/error')
    })
  },
  computed: {
    computedPool: function () {
      let items = this.pool

      if (this.filter) {
        items = items.filter((lhs) => {
          return lhs.name.toLowerCase().includes(this.filter.toLowerCase())
        })
      }

      items.sort((lhs, rhs) => {
        if (lhs.name < rhs.name) return -1
        if (lhs.name > rhs.name) return 1
        return 0
      })

      return items
    },
    fullRoster: function () {
      return this.roster.length === 10
    }
  },
  methods: {
    clear () {
      this.filter = null
      this.roster = []
    },
    selectPool (item) {
      if (this.roster.includes(item)) {
        let index = this.roster.indexOf(item)
        this.roster.splice(index, 1)
      } else {
        if (this.roster.length < 10) {
          this.roster.push(item)
        }
      }
    },
    selectRoster (item) {
      if (this.fullRoster) {
        this.selection = item.id
        this.draft()
      }
    },
    draft () {
      let web = []
      for (let i = 0; i < this.roster.length; i++) {
        const hero = this.roster[i]
        web.push(axios.get('/static/data/heroes/' + hero.id + '/hero.json').then((reponse) => { return reponse.data }))
      }

      Promise.all(web).then((values) => {
        let collection = []
        for (let i = 0; i < this.roster.length; i++) {
          let hero = values[i]
          let data = hero.abilities.filter(_ => _.ability_behaviors.includes('DOTA_ABILITY_BEHAVIOR_HIDDEN') === false).map(_ => _.id)
          collection = collection.concat(data)
        }
        
        let heroes = this.roster.map(_ => _.id).join()
        let abilities = collection.join()
        this.$router.push('/draft/live/?hero=' + this.selection + '&roster=' + heroes + '&skills=' + abilities)
      })
    }
  }
}
</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped>
.border-selected {
  border-width: medium !important;
}
</style>
