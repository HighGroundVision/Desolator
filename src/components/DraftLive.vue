<template>
  <section v-if="ready" class="opaque-background">
    <b-row>
      <b-col>
        <h1 class="text-warning">Draft</h1>
      </b-col>
      <b-col cols="1">
        <b-btn v-b-modal.modalHelp title="Help"><i class="far fa-question-circle"></i></b-btn>
      </b-col>
    </b-row>

    <hr class="highlighted" />

    <!-- Hero -->
    <b-row>
      <b-col>
        <b-img :src="hero.icon" :title="hero.name" class="hero-icon-sm"></b-img>
      </b-col>
      <b-col>
        <div style="line-height: 32px;">
          <span>{{hero.name}}</span>
        </div>
      </b-col>
      <b-col>
        <div style="line-height: 32px;">
          <i title="Pick Order" class="fa-lg fas fa-sort-amount-down"></i>
          <span>{{pickOrder}}</span>
        </div>
      </b-col>
      <b-col>
        <div v-if="hero.attack === 1">
          <b-img src="https://hgv-hyperstone.azurewebsites.net/mics/type_melee.png" title="Attack Type" /> Melee
        </div>
        <div v-if="hero.attack === 2">
          <b-img src="https://hgv-hyperstone.azurewebsites.net/mics/type_range.png" title="Attack Type" /> Range
        </div>
      </b-col>
      <b-col>
        <div v-if="hero.primary === 3">
          <b-img src="https://hgv-hyperstone.azurewebsites.net/mics/primary_str.png" title="Primary Stat" /> Str
        </div>
        <div v-if="hero.primary=== 4">
          <b-img src="https://hgv-hyperstone.azurewebsites.net/mics/primary_agi.png" title="Primary Stat" /> Agi
        </div>
        <div v-if="hero.primary === 5">
          <b-img src="https://hgv-hyperstone.azurewebsites.net/mics/primary_int.png" title="Primary Stat"  /> Int
        </div>
      </b-col>
    </b-row>

    <hr class="highlighted" />

    <!-- Pool -->
    <b-row>
      <b-col cols="8">
        <b-form inline>
          <span>Filter</span>
          &nbsp;&nbsp;
          <b-button-group>
            <b-button v-for="f in filter" @click="onFilter(f)" :pressed.sync="f.state" variant="outline-info" :key="f.caption">
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
    <br />
    <b-row>
      <template v-for="item in abilities" >
        <b-col :key="item.ability.id" cols="1">
          <i v-if="item.filtered" class="rank fas fa-certificate fa-stack-2x text-info"></i>
          <b-img :src="item.ability.img" :title="item.ability.dname"  @click.left.exact="pick(item.ability.id)" @click.right.exact="draft(item.ability.id)" oncontextmenu="return false" v-bind:class="getAbilityClass(item)" />
        </b-col>
      </template>
    </b-row>

    <hr class="highlighted" />

    <!-- Drafting Field -->
    <div class="drafting-field ">
      <b-row>
        <b-col>
          <h5>Combos For</h5>
        </b-col>
      </b-row>
      <b-row>
        <b-col cols="2">
          <div>
            <b-img :src="hero.img"  class="hero-icon-lg"></b-img>
          </div>
        </b-col>
        <template v-for="item in combos" >
          <b-col :key="item.abilities">
            <template v-for="skill in item.skills" >
              <div :key="skill.id">
                <b-img :title="skill.dname" :src="skill.img" @click.left.exact="pick(skill.id)" @click.right.exact="draft(skill.id)" oncontextmenu="return false" v-bind:class="getComboClass(item)"></b-img>
              </div>
            </template>
          </b-col>
        </template>
      </b-row>
      <template v-for="skill in selectedCombos">
        <div :key="skill.id" v-if="skill.combos.length > 0">
          <b-row>
            <b-col cols="2">
            <div>
              <b-img :src="skill.img" :title="skill.dname"  @click.left.exact="draft(skill.id)" class="ability-icon-lg"></b-img>
            </div>
          </b-col>
          <template v-for="item in skill.combos" >
            <b-col :key="item.abilities">
              <template v-for="skill in item.skills.filter(s => s.id != skill.id)">
                <div :key="skill.id">
                  <b-img :title="skill.dname" :src="skill.img" @click.left.exact="pick(skill.id)" @click.right.exact="draft(skill.id)" oncontextmenu="return false" v-bind:class="getComboClass(item)"></b-img>
                </div>
              </template>
            </b-col>
          </template>
          </b-row>
        </div>
      </template>
    </div>

    <!--Dialog - Help -->
    <b-modal id="modalHelp" title="Help" size="lg" :hide-footer="true" header-bg-variant="dark" header-text-variant="light" body-bg-variant="dark" footer-bg-variant="dark">
      <p>You can remove ability from the pool by clicking on them. This will grey them out in the pool and filter them out of combos.</p>
      <p>You can pick abilties by right clicking. We will automaticly filter out the other Ultimates after you select one.</p>
      <p>The 1st row contains the Hero details. Which her you selected. What pick order you are. What is your hero's Attack Type and Primary Stat. It will also display any abilties you have selected.</p>
      <p>The 2nd row contains the filter and sort controls. You can sort the Pool and Combos by Win Rate, Picks, or Wins. You can Filter the reults to highlight abilties match that filter and filter out combos do not match.</p>
      <p>The 3rd row contains the pool of abilties in the draft. The order is controled by the sort (see above). The borders are Green for greater then 50% win rate, Blue for equal to 50% win rate, and Red for below 50%</p>
      <p>The 4th row contains the combos for the hero based on the attack type / primary stat and selected the abilities.</p>
    </b-modal>
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
    openMenu (e) {
      e.preventDefault()
    },
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
    onFilter (selected) {
      let clear = this.filter.filter(f => f.caption !== selected.caption)
      for (let i = 0; i < clear.length; i++) {
        const element = clear[i]
        element.state = false
      }

      let set = this.filter.filter(f => f.caption === selected.caption && selected.stat === false)
      for (let i = 0; i < set.length; i++) {
        const element = set[i]
        element.state = true
      }
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
        'icon-ability ability-icon-lg': true,
        'picked': picks.includes(item.ability.id),
        'drafted': drafts.includes(item.ability.id),
        'skill border border-danger': item.stats.win_rate < 50,
        'skill border border-primary': item.stats.win_rate === 50,
        'skill border border-success': item.stats.win_rate > 50
      }
      return data
    },
    getComboClass (stats) {
      let data = { 
        'icon-combo ability-icon-lg': true,
        'skill border border-danger': stats.win_rate < 50,
        'skill border border-primary': stats.win_rate === 50,
        'skill border border-success': stats.win_rate > 50
      }
      return data
    },
    pick (id) {
      if (this.drafted.includes(id)) {
        return
      }

      if (this.picked.includes(id) === true) {
        this.picked = this.picked.filter(p => p !== id)
      } else {
        this.picked.push(id)
      }
    },
    pickUltimates (id) {
      let item = this._pool.filter(p => p.ability.id === id)[0]
      if (item.ability.ultimate) {
        let others = this._pool.filter(p => p.ability.id !== id && p.ability.ultimate === true)
        for (let i = 0; i < others.length; i++) {
          const other = others[i]
          this.picked.push(other.ability.id)
        }
      }
    },
    draft (id) {
      if (this.picked.includes(id)) {
        return
      }

      if (this.drafted.includes(id) === true) {
        this.drafted = this.drafted.filter(p => p !== id)

        let others = this._pool.filter(p => p.ability.ultimate === true)
        for (let i = 0; i < others.length; i++) {
          const other = others[i]
          this.picked = this.picked.filter(p => p !== other.ability.id)
        }
      } else {
        this.drafted.push(id)
        this.pickUltimates(id)
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
      } else if (d === 5) {
        return 'Last'
      } else if (d >= 0) {
        return (d + 5) + 'th'
      } else {
        return '#'
      }
    },
    abilities () {
      let pool = this._pool

      let filters = this.filter.filter(s => s.state === true)
      for (let i = 0; i < pool.length; i++) {
        const item = pool[i]
        item.filtered = false
        for (let z = 0; z < filters.length; z++) {
          const f = filters[z]
          if (item.ability.keywords.includes(f.caption)) {
            item.filtered = true
          }
        }
      }

      let sorts = this.sort.filter(s => s.state === true)
      for (let i = 0; i < sorts.length; i++) {
        const item = sorts[i]
        pool.sort((lhs, rhs) => {
          let delta = rhs.stats[item.option] - lhs.stats[item.option]
          let inic = rhs.ability.id - lhs.ability.id
          return delta === 0 ? inic : delta
        })
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

      let sorts = this.sort.filter(s => s.state === true)
      for (let i = 0; i < sorts.length; i++) {
        const item = sorts[i]
        combos.sort((lhs, rhs) => {
          let delta = rhs[item.option] - lhs[item.option]
          let inic = rhs.abilities.localeCompare(lhs.abilities)
          return delta === 0 ? inic : delta
        })
      }

      combos = combos.slice(0, 8)
      return combos
    },
    selectedAbilities () {
      let abilities = []

      for (let i = 0; i < 4; i++) {
        const id = this.drafted[i]
        if (id) {
          let item = this._pool.filter(item => item.ability.id === id)[0]
          if (item) {
            abilities.push(item.ability)
          }
        } else {
          abilities.push({ id: i, dname: 'Empty', img: 'https://hgv-hyperstone.azurewebsites.net/abilities/empty.png', ultimate: false })
        }
      }

      abilities.sort((lhs, rhs) => lhs.ultimate - rhs.ultimate)

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

      let skills = []
      for (let i = 0; i < 4; i++) {
        const id = this.drafted[i]
        if (id) {
          let collection = combos.filter(s => s.abilities.includes(id))
          
          let sorts = this.sort.filter(s => s.state === true)
          for (let i = 0; i < sorts.length; i++) {
            const item = sorts[i]
            collection = collection.sort((lhs, rhs) => {
              let delta = rhs[item.option] - lhs[item.option]
              let inic = rhs.abilities.localeCompare(lhs.abilities)
              return delta === 0 ? inic : delta
            })
          }
          collection = collection.slice(0, 8)

          let item = this._pool.filter(s => s.ability.id === id)[0]
          if (item) {
            skills.push({ id: item.ability.id, dname: item.ability.dname, img: item.ability.img, ultimate: item.ability.ultimate, combos: collection })
          }
        } else {
          skills.push({ id: i, dname: 'Empty', img: 'https://hgv-hyperstone.azurewebsites.net/abilities/empty.png', ultimate: false, combos: [] })
        }
      }

      skills.sort((lhs, rhs) => lhs.ultimate - rhs.ultimate)

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
      { caption: 'Heal', state: false },
      { caption: 'Aghanims', state: false }
    ]

    this.sort = [
      { option: 'win_rate', caption: 'Win Rate', state: true },
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
        let winRate = stats.reduce((lhs, rhs) => (lhs.win_rate + rhs.win_rate)) / stats.length
        let wins = stats.reduce((lhs, rhs) => (lhs.wins + rhs.wins)) / stats.length
        let picks = stats.reduce((lhs, rhs) => (lhs.picks + rhs.picks)) / stats.length

        // Get Combos
        let combos = comboStats.filter(s => s.abilities.includes(id))
        
        let data = {
          'ability': ability,
          'stats': {
            'win_rate': winRate,
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
.icon-ability {
  margin-bottom: 10px;
}
.icon-combo {
  margin: 5px;
}
.rank {
  position: absolute;
  top: -10px;
  left: -25px;
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
    opacity: 0.2;
}
.drafted {
    opacity: 0.4;
}
.drafting-field {
  min-width: 600px;
}
</style>
