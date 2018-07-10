<template>
  <section v-if="ready" class="opaque-background">
    <b-row>
      <b-col>
        <h1 class="text-warning">Draft</h1>
      </b-col>
    </b-row>
    <b-row>
      <b-col>
        <p>Please select the heroes in the draft. Make sure you reorder then into the correct order has the appear in the draft. You can reorder them with drag and drop after selection.</p>
        <p>We have tried to speed up this process by giving you a number of options to select the hero. You can click on the hero icon, drag and drop it correct team, or search for the hero name and shortforms. Pressing Enter will select the 1st hero and reset the list.</p>
      </b-col>
    </b-row>
    <b-row>
      <b-col>
        <b-form-input v-model="filter" @keydown.enter.native="pickFromFilter" placeholder="Filter by Hero"  />
      </b-col>
    </b-row>
    <br />
    <b-row>
      <b-col class="text-center">
        <draggable v-model="heroPool" :options="{group:'heroes',sort:false}" @end="pickFromDragDrop" >
          <b-img v-for="item in heroPool" :key="item.id" :src="item.icon" :title="item.name" @click="pickFromClick(item)" class="moveable" />
        </draggable>
      </b-col>
    </b-row>
    <hr class="highlighted" />
    <b-row>
      <b-col cols="12" sm="6" class="text-center">
        <h3>Radiant</h3>
        <draggable v-model="radiant" :options="{group:'heroes'}"  class="border border-primary draggable">
          <b-img v-for="item in radiant" :key="item.id" :src="item.icon" :title="item.name" class="moveable" />
        </draggable>
      </b-col>
      <b-col cols="12" sm="6" class="text-center">
        <h3>Dire</h3>
        <draggable v-model="dire" :options="{group:'heroes'}" class="border border-primary draggable">
          <b-img v-for="item in dire" :key="item.id" :src="item.icon" :title="item.name" class="moveable" />
        </draggable>
      </b-col>
    </b-row>
    <hr class="highlighted" />
    <b-row>
      <b-col>
        <b-btn v-if="isStepComplete" variant="primary" @click="next" class="btn-block">Next</b-btn>
        <b-btn v-else variant="secondary" class="btn-block">Next</b-btn>
      </b-col>
      <b-col>
        <b-btn variant="danger" class="btn-block" @click="clear">Reset</b-btn>
      </b-col>
    </b-row>
  </section>
  <section v-else class="opaque-background text-center">
    <hgv-loader :color="'#ffc107'"></hgv-loader>
  </section>
</template>

<script>
import axios from 'axios'
import draggable from 'vuedraggable'
import {getHeroes, filterPool} from '@/assets/draft'

export default {
  name: 'DraftHeroes', 
  components: { draggable },
  methods: {
    clear () {
      this.filter = null
      this.radiant = []
      this.dire = []
      this.$router.replace({query: {}})
    },
    next () {
      let r = this.radiant.map(h => h.id).join(',')
      let d = this.dire.map(h => h.id).join(',')
      this.$router.push({name: 'DraftAbilities', query: {'radiant': r, 'dire': d}})
    },
    updateQueryString () {
      let r = this.radiant.map(h => h.id).join(',')
      let d = this.dire.map(h => h.id).join(',')
      this.$router.replace({query: {'radiant': r, 'dire': d}})
    },
    pickFromFilter () {
      let hero = this.heroPool[0]
      this.filter = null
      if (this.radiant.length < 6) {
        this.radiant.push(hero)
      } else if (this.dire.length < 6) {
        this.dire.push(hero)
      }
      this.updateQueryString()
    },
    pickFromClick (hero) {
      if (this.radiant.length < 6) {
        this.radiant.push(hero)
      } else if (this.dire.length < 6) {
        this.dire.push(hero)
      }

      this.updateQueryString()
    },
    pickFromDragDrop () {
      this.updateQueryString()
    }
  },
  computed: {
    heroPool: {
      get: function () {
        let collection = this.unSelectedHeroes

        if (this.filter) {
          let lower = this.filter.toLowerCase()
          collection = collection.filter((h) => { return h.name_lower.includes(lower) || h.acronym === lower })
        }

        // Sort by Name
        collection.sort((lhs, rhs) => { return lhs.name.localeCompare(rhs.name) })

        return collection
      },
      set: function () {}
    },
    unSelectedHeroes () {
      return this.heroes.filter(h => !this.radiant.includes(h)).filter(h => !this.dire.includes(h))
    },
    isStepComplete () {
      return this.radiant.length === 6 && this.dire.length === 6
    }
  },
  data () {
    return {
      'ready': false,
      'heroes': [],
      'filter': null,
      'radiant': [],
      'dire': []
    }
  },
  created () {
    const vm = this

    let p1 = axios.get('/static/data/draftpool.json').then((reponse) => { return reponse.data })
    Promise.all([p1]).then((values) => {
      const pool = values[0]

      let heroes = filterPool(pool)

      // Preset Values from Query String
      if (vm.$route.query.radiant && vm.$route.query.dire) {
        vm.radiant = getHeroes(vm.$route.query.radiant, heroes)
        vm.dire = getHeroes(vm.$route.query.dire, heroes)
        vm.updateQueryString()
      }

      vm.heroes = heroes
      vm.ready = true
    }).catch(function () {
      vm.$router.push('/error')
    })
  }
}
</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped>
.draggable {
  min-height: 55px; 
  padding:10px;
}
.moveable {
  cursor: move;
}
.icon {
  margin: 5px;
}
</style>
