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
      <b-col>
        <h2>{{hero.name}}</h2>
      </b-col>
    </b-row>
    <b-row>
      <b-col>
        <b-img :src="hero.img" :title="hero.name" class="hero-icon-lg"></b-img>
      </b-col>
      <b-col>
        <div>
          <b-badge pill variant="secondary">
            <span style="font-size: 1.5em;">{{pickOrder}} Pick</span>
          </b-badge>
        </div>
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
      <template v-for="ability in selectedAbilities">
        <b-col :key="ability.id">
          <b-img :src="ability.img" class="ability-icon-lg"></b-img>
        </b-col>
      </template>
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
          <b-img :src="item.ability.img" :title="item.ability.name" @click.exact="pick(item.ability.id)" @click.alt.exact="draft(item.ability.id)" v-bind:class="getAbilityClass(item)" />
        </b-col>
      </template>
    </b-row>
    <hr class="highlighted" />
    <b-row>
      <b-col class="text-center">
        <div>
          <h5>Combos For</h5>
          <b-img :src="hero.icon" class="ability-icon-sm"></b-img>
        </div>
        <br />
        <ul class="list-unstyled">
          <template v-for="item in combos" >
            <li :key="item.abilities">
              <template v-for="skill in item.skills" >
                <b-img :key="skill.id" :src="skill.img" @click.exact="pick(skill.id)" @click.alt.exact="draft(skill.id)"  class="icon ability-icon-lg"></b-img>
              </template>
            </li>
          </template>
        </ul>
      </b-col>
      <template v-for="skill in selectedCombos">
        <b-col :key="skill.id" class="text-center">
          <div>
            <h5>Combos For</h5>
            <b-img :src="skill.img" @click.exact="draft(skill.id)" class="ability-icon-sm"></b-img>
          </div>
          <br />
          <ul class="list-unstyled">
            <template v-for="item in skill.combos" >
              <li :key="item.abilities" >
                <template v-for="skill in item.skills.filter(s => s.id != skill.id)" >
                  <b-img :key="skill.id" :src="skill.img" @click.exact="pick(skill.id)" @click.alt.exact="draft(skill.id)" class="icon ability-icon-lg"></b-img>
                </template>
              </li>
            </template>
          </ul>
        </b-col>
      </template>
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
    onSort (selected) {
      for (let i = 0; i < this.sort.length; i++) {
        const item = this.sort[i]
        item.state = false
      }
      selected.state = true
    },
    getAbilityClass (item) {
      let picks = this.picked
      let drafts = this.drafted

      let data = { 
        'icon ability-icon-lg': true,
        'picked': picks.includes(item.ability.id),
        'drafted': drafts.includes(item.ability.id),
        'skill border border-danger': item.stats.rate < 50,
        'skill border border-primary': item.stats.rate === 50,
        'skill border border-success': item.stats.rate > 50
      }
      return data
    },
    getComboClass (stats) {
      let data = { 
        // 'icon ability-icon-lg': true,
        'skill border border-danger': stats.win_rate < 50,
        'skill border border-primary': stats.win_rate === 50,
        'skill border border-success': stats.win_rate > 50
      }
      return data
    },
    pick (id) {
      if (this.picked.includes(id) === true) {
        this.picked = this.picked.filter(p => p !== id)
      } else {
        this.picked.push(id)
      }
    },
    draft (id) {
      if (this.drafted.includes(id) === true) {
        this.drafted = this.drafted.filter(p => p !== id)
      } else {
        this.drafted.push(id)
      }
    }
  },
  computed: {
    pickOrder () {
      let r = this.radiant.indexOf(this.heroId)
      let d = this.dire.indexOf(this.heroId)

      if (r >= 0) {
        if (r === 1) {
          return '1st'
        } else if (r === 2) {
          return '2nd'
        } else if (r === 3) {
          return '3rd'
        } else {
          return r + 'th'
        }
      } else if (d >= 0) {
        return (d + 5) + 'th'
      } else {
        return ''
      }
    },
    abilities () {
      let pool = this._pool

      let filters = this.filter.filter(s => s.state === true)
      for (let i = 0; i < filters.length; i++) {
        const item = filters[i]
        pool = pool.filter(p => p.ability.keywords.includes(item.caption))
      }

      let sorts = this.sort.filter(s => s.state === true)
      for (let i = 0; i < sorts.length; i++) {
        const item = sorts[i]
        pool.sort((lhs, rhs) => rhs.stats[item.option] - lhs.stats[item.option])
      }

      return pool
    },
    combos () {
      let combos = this._combos
      let picks = this.picked
      let drafts = this.drafted
      let unavailable = [].concat(picks).concat(drafts)

      combos = combos.filter(s => {
        for (let i = 0; i < unavailable.length; i++) {
          const id = unavailable[i]
          if (s.abilities.includes(id) === true) {
            return false
          }
        }
        return true
      })

      let filters = this.filter.filter(s => s.state === true)
      for (let i = 0; i < filters.length; i++) {
        const item = filters[i]
        combos = combos.filter(p => p.skills[0].keywords.includes(item.caption) || p.skills[1].keywords.includes(item.caption))
      }

      combos.sort((lhs, rhs) => rhs.win_rate - lhs.win_rate)

      combos = combos.slice(0, 12)
      return combos
    },
    selectedAbilities () {
      let abilities = []

      for (let i = 0; i < 4; i++) {
        const id = this.drafted[i]
        if (id) {
          let item = this.abilities.filter(item => item.ability.id === id)[0]
          abilities.push(item.ability)
        } else {
          abilities.push({ id: i, name: 'empty', img: 'https://hgv-hyperstone.azurewebsites.net/abilities/empty.png' })
        }
      }

      return abilities
    },
    selectedCombos () {
      let combos = this._combos
      let picks = this.picked
      let unavailable = [].concat(picks)

      combos = combos.filter(s => {
        for (let i = 0; i < unavailable.length; i++) {
          const id = unavailable[i]
          if (s.abilities.includes(id) === true) {
            return false
          }
        }
        return true
      })

      let filters = this.filter.filter(s => s.state === true)
      for (let i = 0; i < filters.length; i++) {
        const item = filters[i]
        combos = combos.filter(p => p.skills[0].keywords.includes(item.caption) || p.skills[1].keywords.includes(item.caption))
      }

      combos.sort((lhs, rhs) => rhs.win_rate - lhs.win_rate)

      let skills = []
      for (let i = 0; i < 4; i++) {
        const id = this.drafted[i]
        if (id) {
          let collection = combos.filter(s => s.abilities.includes(id)).slice(0, 12)
          let item = this.abilities.filter(s => s.ability.id === id)[0]
          skills.push({ id: item.ability.id, name: item.ability.name, img: item.ability.img, combos: collection })
        } else {
          skills.push({ id: i, name: 'empty', img: 'https://hgv-hyperstone.azurewebsites.net/abilities/empty.png', combos: [] })
        }
      }

      return skills
    }
  },
  data () {
    return {
      'ready': false,
      // Needed for QueryString (if going back a step)
      'radiant': [],
      'dire': [],
      'skills': [],
      'ultimates': [],
      'heroId': 0,
      // Pools of Abilities
      '_pool': [],
      '_combos': [],
      // Selected Hero Details
      'hero': null,
      'filter': [],
      'sort': [],
      // Draft Status
      'picked': [],
      'drafted': []
    }
  },
  created () {
    const vm = this

    this.filter = [
      { caption: 'Stun', state: false },
      { caption: 'Nuke', state: false },
      { caption: 'Disable', state: false },
      { caption: 'Slow', state: false },
      { caption: 'Speed', state: false },
      { caption: 'Attack', state: false },
      { caption: 'Tank', state: false },
      { caption: 'Heal', state: false }
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
      let comboStats = values.shift()

      // Combined ultimates & skills
      let indexes = [].concat(vm.ultimates).concat(vm.skills)

      // Get Hero
      let hero = heroes[vm.heroId]
      hero.profile = hero.img.replace('/banner/', '/profile/npc_dota_hero_')
      hero.icon = hero.img.replace('/banner/', '/icons/')

      // Filter Stats By Hero types
      abilityStats = abilityStats.filter(s => s.type === hero.attack || s.type === hero.primary)
      comboStats = comboStats.filter(s => s.type === hero.attack)

      // Filter by Abilties
      comboStats = comboStats.filter(s => {
        let keys = s.abilities.split('-')
        return (indexes.includes(parseInt(keys[0])) && indexes.includes(parseInt(keys[1])))
      })

      // Get Ability Details
      for (let c = 0; c < comboStats.length; c++) {
        let element = comboStats[c]
        element.skills = []

        let ids = element.abilities.split('-')
        for (let i = 0; i < ids.length; i++) {
          const id = ids[i]
          let ability = abilities[id]
          element.skills.push(ability) 
        }
      }

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

        // Get Combos
        let combos = comboStats.filter(s => s.abilities.includes(id))
        
        let data = {
          'ability': ability,
          'stats': {
            'rate': rate,
            'wins': wins,
            'picks': picks
          },
          'combos': combos
        }
        pool.push(data)
      }

      vm.hero = hero
      vm._pool = pool
      vm._combos = comboStats
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
  margin-bottom: 10px;
}
.rank {
  position: absolute;
  top: -10px;
  left: 0px;
  z-index: 999;
}
.commands {
  position: absolute;
  top: 60px;
  left: 20px;
}
.skill {
  border-width: 2px !important;
}
.picked {
    opacity: 0.4;
}
.drafted {
    opacity: 0.6;
}
</style>
