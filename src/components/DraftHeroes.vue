<template>
  <section v-if="ready" class="opaque-background">
    <b-row>
      <b-col>
        <h1 class="text-warning">Draft</h1>
      </b-col>
    </b-row>
    <b-row>
      <b-col>
        <p>Lorem ipsum dolor sit amet, consectetur adipiscing elit. Suspendisse a risus vel nunc ullamcorper vestibulum in sed quam. Suspendisse facilisis lacinia semper. Nulla facilisi. Orci varius natoque penatibus et magnis dis parturient montes, nascetur ridiculus mus. Morbi pretium, tellus eget auctor faucibus, lacus mauris feugiat elit, at bibendum elit arcu sed ipsum. Donec dapibus semper ante id dapibus. In auctor mollis neque quis hendrerit. Aenean pharetra tortor orci, ut porttitor lectus cursus a. Nulla gravida rhoncus dolor vitae blandit. Pellentesque porta placerat justo, in efficitur felis aliquam ut. Pellentesque ullamcorper vestibulum magna eget maximus. In at suscipit erat. Maecenas rhoncus dolor ante, at consectetur metus dictum sit amet. Vivamus enim tortor, semper non mollis sed, vehicula sit amet nisl. Mauris lacinia ante sit amet erat dapibus sollicitudin sed quis tortor.</p>
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
        <draggable v-model="heroPool" :options="{group:'heroes',sort:false}"  >
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
        <b-btn variant="danger" class="btn-block" @click="clear">Clear</b-btn>
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
import {getCollectionFromQueryString} from '@/assets/draft'

export default {
  name: 'DraftHeroes', 
  components: { draggable },
  methods: {
    clear () {
      // Reset Data
      this.step = 1
      this.filter = null
      this.radiant = []
      this.dire = []
      // Reset query
      this.$router.replace({query: {}})
    },
    next () {
      let r = this.radiant.map(h => h.id).join(',')
      let d = this.dire.map(h => h.id).join(',')
      this.$router.push({name: 'DraftAbilities', query: {'radiant': r, 'dire': d}})
    },
    pickFromFilter () {
      if (this.heroPool.length === 1) {
        let hero = this.heroPool[0]
        if (this.radiant.length < 6) {
          this.radiant.push(hero)
        } else if (this.dire.length < 6) {
          this.dire.push(hero)
        }

        this.filter = null
      }
    },
    pickFromClick (hero) {
      if (this.radiant.length < 6) {
        this.radiant.push(hero)
      } else if (this.dire.length < 6) {
        this.dire.push(hero)
      }
    }
  },
  computed: {
    heroPool: {
      get: function () {
        let collection = this.unSelectedHeroes

        if (this.filter) {
          let lower = this.filter.toLowerCase()
          if (lower.length === 2) {
            // Filter by acronym (if 2 letters)
            collection = collection.filter((h) => { return h.acronym === lower })
          } else {
            // Filter by Name
            collection = collection.filter((h) => { return h.name_lower.includes(lower) })
          }
        }

        // Sort by Name
        collection.sort((lhs, rhs) => { return lhs.name.localeCompare(rhs.name) })

        return collection
      },
      set: function () {}
    },
    radiant: {
      get: function () {
        return this.team_radiant
      },
      set: function (value) {
        this.team_radiant = value
      }
    },
    dire: {
      get: function () {
        return this.team_dire
      },
      set: function (value) {
        this.team_dire = value
      }
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
      'team_radiant': [],
      'team_dire': []
    }
  },
  created () {
    const vm = this

    let p1 = axios.get('/static/data/draftpool.json').then((reponse) => { return reponse.data })
    Promise.all([p1]).then((values) => {
      const pool = values[0]

      let heroes = pool.filter(h => h.enabled === true)
      for (let i = 0; i < heroes.length; i++) {
        const hero = heroes[i]
        // Hero
        hero.name_lower = hero.name.toLowerCase()
        hero.acronym = hero.acronym.toLowerCase()
        hero.icon = hero.img.replace('/banner/', '/icons/')
      }

      // Preset Values from Query String
      if (vm.$route.query.radiant) {
        vm.radiant = getCollectionFromQueryString(vm.$route.query.radiant, heroes)
      }
      if (vm.$route.query.dire) {
        vm.dire = getCollectionFromQueryString(vm.$route.query.radiant, heroes)
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
