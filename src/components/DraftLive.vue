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
    <b-row>
      <b-col cols="2">
        <b-img :src="hero.img" :title="hero.name" class="hero-icon-profile "></b-img>
      </b-col>
      <b-col>
        <b-row>
          <b-col>
            <h2>{{hero.name}}</h2>
          </b-col>
        </b-row>
        <b-row>
          <b-col>
            <b-badge pill variant="secondary">
              <span style="font-size: 1.5em;">1st Pick</span>
            </b-badge>
          </b-col>
          <b-col>
            <div v-if="hero.attack === 1">
              <b-img src="https://hgv-hyperstone.azurewebsites.net/mics/type_melee.png" title="Melee" /> Melee
            </div>
            <div v-if="hero.attack === 2">
              <b-img src="https://hgv-hyperstone.azurewebsites.net/mics/type_range.png" title="Range" /> Range
            </div>
            <div v-if="hero.primary === 3">
              <b-img src="https://hgv-hyperstone.azurewebsites.net/mics/primary_str.png" title="Str" /> Str
            </div>
            <div v-if="hero.primary=== 4">
              <b-img src="https://hgv-hyperstone.azurewebsites.net/mics/primary_agi.png" title="Agi" /> Agi
            </div>
            <div v-if="hero.primary === 5">
              <b-img src="https://hgv-hyperstone.azurewebsites.net/mics/primary_int.png" title="Int"  /> Int
            </div>
          </b-col>
          <b-col>
            <b-img src="https://hgv-hyperstone.azurewebsites.net/abilities/empty.png" class="ability-icon-lg"></b-img>
          </b-col>
          <b-col>
            <b-img src="https://hgv-hyperstone.azurewebsites.net/abilities/empty.png" class="ability-icon-lg"></b-img>
          </b-col>
          <b-col>
            <b-img src="https://hgv-hyperstone.azurewebsites.net/abilities/empty.png" class="ability-icon-lg"></b-img>    
          </b-col>
          <b-col>
            <b-img src="https://hgv-hyperstone.azurewebsites.net/abilities/empty.png" class="ability-icon-lg"></b-img>
          </b-col>
        </b-row>
      </b-col>
    </b-row>
    <hr class="highlighted" />
    <b-row>
      <b-col cols="8">
        <b-form inline>
          <span>Filter</span>
          &nbsp;&nbsp;
          <b-button-group>
            <b-button v-for="f in filter" :pressed.sync="f.state" variant="outline-info" :key="f.caption">
              {{ f.caption }}
            </b-button>
          </b-button-group>
         </b-form>
      </b-col>
      <b-col cols="4">
        <b-form inline>
          <span>Sort</span>
          &nbsp;&nbsp;
          <b-button-group>
            <b-button v-for="s in sort" @click="onSort(s)" :pressed.sync="s.state" variant="outline-info" :key="s.caption">
              {{ s.caption }}
            </b-button>
          </b-button-group>
        </b-form>
      </b-col>
    </b-row>
    <hr class="highlighted" />
    <b-row>
      <template v-for="item in abilities" >
        <b-col :key="item.ability.id" cols="1">
          <b-img :src="item.ability.img" :title="item.ability.name" v-bind:class="getAbilityClass(item)" />
        </b-col>
      </template>
    </b-row>
    <hr class="highlighted" />
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
    /*
    isRadiant (ability) {
      return this.radiant.includes(ability.id)
    },
    isDire (ability) {
      return this.dire.includes(ability.id)
    },
    */
    onSort (selected) {
      for (let i = 0; i < this.sort.length; i++) {
        const item = this.sort[i]
        item.state = false
      }
      selected.state = true
    },
    getAbilityClass (item) {
      let data = { 
        'icon ability-icon-lg': true, 
        'border border-success': item.stats.rate >= 55,
        'border border-danger': item.stats.rate < 50
        // 'border-primary': item.stats.rate < 55 && item.stats.rate >= 50,
      }
      return data
    }
  },
  computed: {
    abilities () {
      let pool = this._pool

      let sort = this.sort.filter(s => s.state === true)[0]
      if (sort) {
        pool.sort((lhs, rhs) => rhs.stats[sort.option] - lhs.stats[sort.option])
      }
      
      return pool
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
      'hero': null,
      'filter': [],
      'sort': []
    }
  },
  created () {
    const vm = this

    this.filter = [
      { caption: 'Stun', state: true },
      { caption: 'Nuke', state: true },
      { caption: 'Disable', state: true },
      { caption: 'Slow', state: true },
      { caption: 'Speed', state: true },
      { caption: 'Attack', state: true },
      { caption: 'Tank', state: true },
      { caption: 'Heal', state: true }
    ]

    this.sort = [
      { option: 'rate', caption: 'Win Rate', state: true },
      { option: 'wins', caption: 'Wins', state: false },
      { option: 'picks', caption: 'Picks', state: false }
    ]

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
      hero.img = hero.img.replace('/banner/', '/profile/npc_dota_hero_')

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

      vm.hero = hero
      vm._pool = pool
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
.icon {
  border-width: 3px !important;
  margin-bottom: 10px;
}
</style>
