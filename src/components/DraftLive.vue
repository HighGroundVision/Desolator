<template>
  <section v-if="ready" class="opaque-background">
    <b-row>
      <b-col>
        <h1 class="text-warning">Draft</h1>
      </b-col>
    </b-row>
    <b-row>
      <b-col>
        <p>Et excepteur nulla ea occaecat pariatur sunt nulla sunt. Consequat esse ipsum quis exercitation cillum voluptate non sint eu eiusmod officia mollit deserunt aliqua. Sint ipsum minim aliqua consectetur eiusmod cillum reprehenderit commodo non. Sint cillum dolor fugiat ut nostrud. Aliquip nisi irure consectetur consectetur eiusmod eu ullamco aute laboris eiusmod nostrud. Nulla ipsum ad sunt id officia minim sunt.</p>
      </b-col>
    </b-row>
    <b-row>
      <b-col>
        <draggable v-model="pool" :options="{group:'heroes'}" >
          <b-img v-for="item in pool" :key="item.id" :src="item.img" :title="item.name" />
        </draggable>
      </b-col>
    </b-row>
    <hr class="highlighted" />
    <b-row>
      <b-col>
        <h3>Radiant</h3>
        <draggable v-model="radiant" :options="{group:'heroes'}"  class="border border-primary" style="min-height: 100px;">
           <b-img v-for="item in radiant" :key="item.id" :src="item.img" :title="item.name" />
        </draggable>
      </b-col>
      <b-col>
        <h3>Dire</h3>
        <draggable v-model="dire" :options="{group:'heroes'}" class="border border-primary" style="min-height: 100px;">
           <b-img v-for="item in dire" :key="item.id" :src="item.img" :title="item.name" />
        </draggable>
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

export default {
  name: 'DraftLive', 
  components: { draggable },
  data () {
    return {
      'ready': false,
      'pool': [],
      'radiant': [],
      'dire': []
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
        hero.img = hero.img.replace('/banner/', '/icons/')
      }
      // let abilities = heroes.map(h => h.abilities).reduce((acc, val) => acc.concat(val), []).filter(a => a.enabled === true)
      // console.log(abilities)

      heroes.sort((lhs, rhs) => lhs.name.localeCompare(rhs.name))

      vm.pool = heroes
      vm.ready = true
    }).catch(function () {
      vm.$router.push('/error')
    })
  }
}
</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped>

</style>
