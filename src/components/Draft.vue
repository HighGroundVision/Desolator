<template>
  <section class="text-center">
    <b-row>
      <b-col>
        <h2 class="text-left text-warning">Drafting</h2>
      </b-col>
    </b-row>
    <hr class="highlighted" />
    <b-row>
      <b-col>
        <h4>Pool</h4>
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
        <b-btn @click="resetModel">Clear</b-btn>
      </b-col>
    </b-row>
    <b-row>
      <b-col>
        <template v-for="(item) in computedPool">
          <img @click="draftItem(item)" :key="item.id" :src="item.img" class="hero-icon-profile-md m-1" />
        </template>
      </b-col>
    </b-row>
    <hr class="highlighted" />
    <b-row>
      <b-col>
        <h4>Roster</h4>
      </b-col>
    </b-row>
    <b-row>
      <b-col>
        <template v-for="(item) in roster">
          <img @click="returnItem(item)" :key="item.id" :src="item.img" v-bind:class="{ 'border border-primary border-selected': selected === item }" class="hero-icon-profile-md m-1" />
        </template>
        <br />
        <b-btn v-if="fullRoster" @click="proceed" variant="success">Draft</b-btn>
      </b-col>
    </b-row>
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
      'roster': []
    }
  },
  created: function () {
    const vm = this

    let web = [
      axios.get('/static/data/heroes/collection.json').then((reponse) => { return reponse.data })
    ]

    Promise.all(web).then((values) => {
      let items = values[0]

      for (let i = 0; i < items.length; i++) {
        const item = items[i]
        item.img = item.img.replace('/banner/', '/profile/')
      }

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
    resetModel () {
      this.filter = null
      this.roster = []
    },
    draftItem (item) {
      if (this.roster.length < 10) {
        if (this.roster.includes(item) === false) {
          this.roster.push(item)
        }
      }
    },
    returnItem (item) {
      let index = this.roster.indexOf(item)
      this.roster.splice(index, 1)
    },
    proceed () {
      var roster = this.roster.map(_ => _.id).join()
      this.$router.push('/draft/handler/?heroes=' + roster)
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
