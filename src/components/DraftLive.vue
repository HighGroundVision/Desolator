<template>
  <section v-if="ready" class="opaque-background">
    <b-row>
      <b-col>
        <h1 class="text-warning">Draft</h1>
      </b-col>
    </b-row>
    <b-row>
      <b-col>
        <p>Lorem ipsum dolor sit amet, consectetur adipiscing elit. Suspendisse a risus vel nunc ullamcorper vestibulum in sed quam. Suspendisse facilisis lacinia semper. Nulla facilisi. Orci varius natoque penatibus et magnis dis parturient montes, nascetur ridiculus mus. Morbi pretium, tellus eget auctor faucibus, lacus mauris feugiat elit, at bibendum elit arcu sed ipsum. Donec dapibus semper ante id dapibus.</p>
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
  name: 'DraftLive', 
  methods: {
    validateAndExtractQuery () {
      if (this.$route.query === undefined) {
        this.clear()
      }
      if (this.$route.query.radiant !== undefined) {
        this.radiant = this.$route.query.radiant.split(',').map(lhs => parseInt(lhs))
      }
      if (this.$route.query.dire !== undefined) {
        this.dire = this.$route.query.dire.split(',').map(lhs => parseInt(lhs))
      }
      if (this.$route.query.ultimates !== undefined) {
        this.ultimates = this.$route.query.ultimates.split(',').map(lhs => parseInt(lhs))
      }
      if (this.$route.query.skills !== undefined) {
        this.skills = this.$route.query.skills.split(',').map(lhs => parseInt(lhs))
      }
      if (this.$route.query.hero !== undefined) {
        this.heroId = this.heroId = parseInt(this.$route.query.hero)
      }

      let hasData = 
        this.radiant.length === 6 && 
        this.dire.length === 6 && 
        this.skills.length === 36 && 
        this.ultimates.length === 12

      if (hasData && this.heroId === 0) {
        this.back()
      } else if (hasData) {
        return
      } else {
        this.clear()
      }
    },
    packageData () {
      return {
        'radiant': this.radiant.join(','),
        'dire': this.dire.join(','),
        'ultimates': this.ultimates.join(','),
        'skills': this.skills.join(',')
      }
    },
    clear () {
      this.$router.push({name: 'Draft'})
    },
    back () {
      let data = this.packageData()
      this.$router.push({name: 'DraftAbilities', query: data})
    },
    isRadiant (ability) {
      return this.radiant.includes(ability.id)
    },
    isDire (ability) {
      return this.dire.includes(ability.id)
    }
  },
  computed: {
    isTest () {
      return true
    }
  },
  data () {
    return {
      'ready': false,
      // Database
      // 'heroes': [],
      // 'abilities': [],
      // 'abilityStats': [],
      // 'comboStats': [],
      // Needed for QueryString (if going back a step)
      'radiant': [],
      'dire': [],
      'skills': [],
      'ultimates': [],
      'heroId': 0,
      // Pools of Abilities
      'pool': [],
      // Selected Hero Details
      'hero': null
    }
  },
  created () {
    const vm = this
    
    vm.validateAndExtractQuery()

    let web = [
      axios.get('/static/data/heroes.json').then((reponse) => { return reponse.data }),
      axios.get('/static/data/abilities.json').then((reponse) => { return reponse.data }),
      axios.get('/static/data/stats-abilities.json').then((reponse) => { return reponse.data }),
      axios.get('/static/data/stats-combos.json').then((reponse) => { return reponse.data })
    ]
    Promise.all(web).then((values) => {
      let heroes = values.shift()
      let abilities = values.shift()
      let abilityStats = values.shift()
      // let comboStats = values.shift()

      // Combined ultimates & skills
      let indexes = [].concat(vm.ultimates).concat(vm.skills)

      // Get Hero
      let hero = heroes[vm.heroId]

      // Filter Stats By Hero types
      abilityStats = abilityStats.filter(s => s.type === hero.attack || s.type === hero.primary)
      // comboStats = comboStats.filter(s => s.type === hero.attack || s.type === hero.primary)

      // Get Abilities
      let pool = []
      for (let i = 0; i < indexes.length; i++) {
        // Get ID
        const id = indexes[i]

        // Get Ability Details
        let ability = abilities[id]
        
        // Get Stats
        let stats = abilityStats.filter(s => s.abilities.includes(id))
        let rate = stats.reduce((lhs, rhs) => (lhs.win_rate + rhs.win_rate)) / stats.length
        let wins = stats.reduce((lhs, rhs) => (lhs.wins + rhs.wins)) / stats.length
        let picks = stats.reduce((lhs, rhs) => (lhs.picks + rhs.picks)) / stats.length

        let stat = {
          'rate': rate,
          'wins': wins,
          'picks': picks
        }
        
        let data = {
          'ability': ability,
          'stats': stat
        }
        pool.push(data)
      }

      console.log(pool)

      vm.hero = hero
      vm.pool = pool
      vm.ready = true
    }).catch(function (err) {
      console.log(err)
      // vm.$router.push('/error')
    })
  }
}
</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped>
</style>
