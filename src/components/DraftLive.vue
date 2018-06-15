<template>
  <section v-if="ready" class="opaque-background">
    <div>
      <b-row>
        <b-col>
          <h1 class="text-warning">Draft</h1>
        </b-col>
      </b-row>
    </div>
    <!-- STEP # 1 -->
    <div v-if="step === 1">
      <b-row>
        <b-col>
          <p>Lorem ipsum dolor sit amet, consectetur adipiscing elit. Suspendisse a risus vel nunc ullamcorper vestibulum in sed quam. Suspendisse facilisis lacinia semper. Nulla facilisi. Orci varius natoque penatibus et magnis dis parturient montes, nascetur ridiculus mus. Morbi pretium, tellus eget auctor faucibus, lacus mauris feugiat elit, at bibendum elit arcu sed ipsum. Donec dapibus semper ante id dapibus. In auctor mollis neque quis hendrerit. Aenean pharetra tortor orci, ut porttitor lectus cursus a. Nulla gravida rhoncus dolor vitae blandit. Pellentesque porta placerat justo, in efficitur felis aliquam ut. Pellentesque ullamcorper vestibulum magna eget maximus. In at suscipit erat. Maecenas rhoncus dolor ante, at consectetur metus dictum sit amet. Vivamus enim tortor, semper non mollis sed, vehicula sit amet nisl. Mauris lacinia ante sit amet erat dapibus sollicitudin sed quis tortor.</p>
        </b-col>
      </b-row>
      <b-row>
        <b-col>
          <b-form-input v-model="filter" @keydown.enter.native="quickPick" placeholder="Filter by Hero"  />
        </b-col>
      </b-row>
      <br />
      <b-row>
        <b-col class="text-center">
          <draggable v-model="poolHeroes" :options="{group:'heroes',sort:false}"  >
            <b-img v-for="item in poolHeroes" :key="item.id" :src="item.icon" :title="item.name" @click="pick(item)" class="moveable" />
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
          <b-btn v-if="step1Complete" variant="primary" @click="heroesSelected" class="btn-block">Next</b-btn>
          <b-btn v-else variant="secondary" class="btn-block">Next</b-btn>
        </b-col>
        <b-col>
          <b-btn variant="danger" class="btn-block" @click="clear">Clear</b-btn>
        </b-col>
      </b-row>
    </div>
    <!-- STEP # 2 -->
    <div v-if="step === 2">
      <b-row>
        <b-col>
          <p>Lorem ipsum dolor sit amet, consectetur adipiscing elit. Suspendisse a risus vel nunc ullamcorper vestibulum in sed quam. Suspendisse facilisis lacinia semper. Nulla facilisi. Orci varius natoque penatibus et magnis dis parturient montes, nascetur ridiculus mus. Morbi pretium, tellus eget auctor faucibus, lacus mauris feugiat elit, at bibendum elit arcu sed ipsum. Donec dapibus semper ante id dapibus.</p>
        </b-col>
      </b-row>
      <b-row>
        <b-col cols="3" class="text-center">
          <template v-for="item in teamRadiant">
            <b-row :key="item.id">
              <b-col>
                <b-img :src="item.img" class="icon hero-icon-lg" @click="selectHero(item)" v-bind:class="{'border border-warning hero-selected': selectedHero ? selectedHero.id === item.id : false }" />
              </b-col>
            </b-row>
          </template>
        </b-col>
        <b-col cols="6" class="text-center">         
          <b-row>
            <b-col >
              <b-row>
                <b-col>
                  <b-img :src="radiant[0].ultimate.img" class="icon ability-icon-lg" @click="selectUltimate(radiant[0], radiant[0].ultimate)" />
                  <b-img :src="radiant[1].ultimate.img" class="icon ability-icon-lg" @click="selectUltimate(radiant[1], radiant[1].ultimate)" />
                  <b-img :src="radiant[2].ultimate.img" class="icon ability-icon-lg" @click="selectUltimate(radiant[2], radiant[2].ultimate)" />
                </b-col>
              </b-row>
              <b-row>
                <b-col>
                  <b-img :src="radiant[3].ultimate.img" class="icon ability-icon-lg" @click="selectUltimate(radiant[3], radiant[3].ultimate)" />
                  <b-img :src="radiant[4].ultimate.img" class="icon ability-icon-lg" @click="selectUltimate(radiant[4], radiant[4].ultimate)" />
                  <b-img :src="radiant[5].ultimate.img" class="icon ability-icon-lg" @click="selectUltimate(radiant[5], radiant[5].ultimate)" />
                </b-col>
              </b-row>
            </b-col>
            <b-col>
              <b-row>
                <b-col>
                  <b-img :src="dire[2].ultimate.img" class="icon ability-icon-lg" @click="selectUltimate(dire[2], dire[2].ultimate)" />
                  <b-img :src="dire[1].ultimate.img" class="icon ability-icon-lg" @click="selectUltimate(dire[1], dire[1].ultimate)" />
                  <b-img :src="dire[0].ultimate.img" class="icon ability-icon-lg" @click="selectUltimate(dire[0], dire[0].ultimate)" />
                </b-col>
              </b-row>
              <b-row>
                <b-col>
                  <b-img :src="dire[5].ultimate.img" class="icon ability-icon-lg" @click="selectUltimate(dire[5], dire[5].ultimate)" />
                  <b-img :src="dire[4].ultimate.img" class="icon ability-icon-lg" @click="selectUltimate(dire[4], dire[4].ultimate)" />
                  <b-img :src="dire[3].ultimate.img" class="icon ability-icon-lg" @click="selectUltimate(dire[3], dire[3].ultimate)" />
                </b-col>
              </b-row>
            </b-col>
          </b-row>
          <br />
          <b-row>
            <b-col>
              <template v-for="item in radiant">
                <b-row :key="item.id">
                  <b-col>
                    <template v-for="skill in item.skills">
                      <b-img :key="skill.id" :src="skill.img" class="icon ability-icon-lg" @click="selectAbility(item, skill)" />
                    </template>
                  </b-col>
                </b-row>
              </template>
            </b-col>
            <b-col>
              <template v-for="item in dire">
                <b-row :key="item.id">
                  <b-col>
                    <template v-for="skill in item.skills">
                      <b-img :key="skill.id" :src="skill.img" class="icon ability-icon-lg"  @click="selectAbility(item, skill)" />
                    </template>
                  </b-col>
                </b-row>
              </template>
            </b-col>
          </b-row>
        </b-col>
        <b-col cols="3" class="text-center">
          <template v-for="item in teamDire">
            <b-row :key="item.id">
              <b-col>
                <b-img :src="item.img" class="icon hero-icon-lg" @click="selectHero(item)" v-bind:class="{'border border-warning hero-selected': selectedHero ? selectedHero.id === item.id : false }" />
              </b-col>
            </b-row>
          </template>
        </b-col>
      </b-row>  
      <hr class="highlighted" />
      <b-row>
        <b-col>
          <b-btn variant="primary" @click="back" class="btn-block">Back</b-btn>
        </b-col>
        <b-col>
          <b-btn v-if="step2Complete" variant="primary" @click="abilitiesSelected" class="btn-block">Next</b-btn>
          <b-btn v-else variant="secondary" class="btn-block">Next</b-btn>
        </b-col>
        <b-col>
          <b-btn variant="danger" @click="clear" class="btn-block">Clear</b-btn>
        </b-col>
      </b-row>
    </div>
    <!-- STEP # 3 -->
    <div v-if="step === 3">
      <b-row>
        <b-col>
          <p>Suspendisse facilisis lacinia semper. Nulla facilisi. Orci varius natoque penatibus et magnis dis parturient montes, nascetur ridiculus mus. Morbi pretium, tellus eget auctor faucibus, lacus mauris feugiat elit, at bibendum elit arcu sed ipsum. Donec dapibus semper ante id dapibus. In auctor mollis neque quis hendrerit. Aenean pharetra tortor orci, ut porttitor lectus cursus a. Nulla gravida rhoncus dolor vitae blandit.</p>
        </b-col>
      </b-row>
      <hr class="highlighted" />
      <b-row>
        <b-col>
          <p>Stuff</p>
        </b-col>
      </b-row>
      <hr class="highlighted" />
      <b-row>
        <b-col>
          <b-btn variant="secondary" @click="clear" class="btn-block">Clear</b-btn>
        </b-col>
      </b-row>
    </div>
    <!-- Modal Component -->
    <b-modal ref="RefSelectAbility" title="Select Missing Ability" size="lg" :hide-footer="true" header-bg-variant="dark" header-text-variant="light" body-bg-variant="dark" footer-bg-variant="dark">
      <b-form-input v-model="filter" placeholder="Filter by Hero or Ability"  />
      <br />
      <div style="height: 300px; overflow-x: none; overflow-y: scroll;">
        <template v-for="skill in poolAbilities">
          <b-img :key="skill.id" :src="skill.img" :title="skill.name" @click="replaceAbility(skill)" class="ability-icon ability-icon-lg" />
        </template>
      </div>
    </b-modal>
  </section>
  <section v-else class="opaque-background text-center">
    <hgv-loader :color="'#ffc107'"></hgv-loader>
  </section>
</template>

<script>
import axios from 'axios'
import draggable from 'vuedraggable'

var unknowCounter = 1
function GetNextUnknown () {
  return { 'id': unknowCounter++ * -1, 'name': 'Unkown', 'img': 'https://hgv-hyperstone.azurewebsites.net/abilities/empty.png' }
}

export default {
  name: 'DraftLive', 
  components: { draggable },
  methods: {
    clear () {
      // Reset Data
      this.step = 1
      this.filter = null
      this.radiant = []
      this.dire = []
      this.selectionType = 0
      this.selectionHeroIndex = null
      this.selectionAbilityIndex = null
      this.abilities = []
      this.selectedHero = null
      // Reset query
      this.$router.replace({query: {}})
    },
    back () {
      if (this.step > 1) {
        this.step = this.step - 1
      }
    },
    heroesSelected () {
      let collection = []
      let teams = [this.radiant, this.dire]
      for (let i = 0; i < teams.length; i++) {
        const team = teams[i]
        for (let h = 0; h < team.length; h++) {
          const hero = team[h]
          let skills = hero.abilities.filter(a => a.enabled === true)
          collection = collection.concat(skills)
        }
      }

      this.abilities = collection

      let r = this.radiant.map(h => h.id).join(',')
      let d = this.dire.map(h => h.id).join(',')
      this.$router.replace({query: {'radiant': r, 'dire': d}})

      this.step = 2
    },
    abilitiesSelected () {
      let abilities = this.abilities.map(a => a.id).join(',')
      this.$router.replace({query: {'hero': this.selectedHero.id, 'pool': abilities}})
      this.step = 3
    },
    quickPick () {
      if (this.poolHeroes.length === 1) {
        let hero = this.poolHeroes[0]
        this.filter = null

        if (this.radiant.length < 6) {
          this.radiant.push(hero)
          return
        } 
        if (this.dire.length < 6) {
          this.dire.push(hero)
          return
        } 
      }
    },
    pick (hero) {
      if (this.radiant.length < 6) {
        this.radiant.push(hero)
        return
      }
      if (this.dire.length < 6) {
        this.dire.push(hero)
        return
      }
    },
    selectAbility (hero, ability) {
      this.selectionType = 1
      this.selectionHeroIndex = hero.id
      this.selectionAbilityIndex = ability.id
      this.$refs.RefSelectAbility.show()
    },
    selectUltimate (hero, ability) {
      this.selectionType = 2
      this.selectionHeroIndex = hero.id
      this.selectionAbilityIndex = ability.id
      this.$refs.RefSelectAbility.show()
    },
    replaceAbility (ability) {
      if (ability.id < 0) {
        return
      }

      let teams = [this.radiant, this.dire]

      for (let i = 0; i < teams.length; i++) {
        const team = teams[i]
        for (let h = 0; h < team.length; h++) {
          const hero = team[h]
          if (hero.id === this.selectionHeroIndex) {
            if (this.selectionType === 1) {
              for (let a = 0; a < hero.skills.length; a++) {
                let skill = hero.skills[a]
                if (skill.id === this.selectionAbilityIndex) {
                  hero.skills[a] = ability
                  this.abilities.push(ability)
                }
              }
            } else if (this.selectionType === 2) {
              hero.ultimate = ability
              this.abilities.push(ability)
            }
          }
        }
      }

      this.$refs.RefSelectAbility.hide()
    },
    selectHero (hero) {
      if (this.selectedHero == null || this.selectedHero.id !== hero.id) {
        this.selectedHero = hero
      } else {
        this.selectedHero = null
      }
    }
  },
  computed: {
    poolHeroes: {
      get: function () {
        let collection = this.heroes

        // Filter by Name or by acronym (if 2 letters)
        if (this.filter) {
          let lower = this.filter.toLowerCase()
          if (lower.length === 2) {
            collection = collection.filter((h) => { return h.acronym === lower })
          } else {
            collection = collection.filter((h) => { return h.name_lower.includes(lower) })
          }
        }

        // Filter by Selected
        collection = collection.filter(h => !this.radiant.includes(h)).filter(h => !this.dire.includes(h))

        // Sort by Name
        collection.sort((lhs, rhs) => { return lhs.name.localeCompare(rhs.name) })

        return collection
      },
      set: function () {}
    },
    poolAbilities () {
      let abilities = []
      let heroes = this.poolHeroes
      let filtered = false

      if (heroes.length === 0) {
        heroes = this.heroes
      } else {
        filtered = true
      }

      for (const hero of heroes) {
        let skills = hero.abilities.filter(a => a.enabled === true)

        if (this.selectionType === 1) {
          skills = skills.filter(a => a.ultimate === false)
        } else if (this.selectionType === 2) {
          skills = skills.filter(a => a.ultimate === true)
        }
        
        abilities = abilities.concat(skills)
      }

      if (filtered === false && this.filter !== null) {
        let f = this.filter.toLowerCase()
        abilities = abilities.filter(a => a.name.toLowerCase().includes(f))
      }
      return abilities
    },
    teamRadiant () {
      return this.radiant.slice(0, 5)
    },
    teamDire () {
      return this.dire.slice(0, 5)
    },
    step1Complete () {
      return this.radiant.length === 6 && this.dire.length === 6
    },
    step2Complete () {
      console.log('abilities', this.abilities)
      console.log('hero', this.selectedHero)
      return this.abilities.length === 48 && this.selectedHero != null
    }
  },
  data () {
    return {
      'ready': false,
      'step': 1,
      'heroes': [],
      'filter': null,
      'radiant': [],
      'dire': [],
      'selectionType': 0,
      'selectionHeroIndex': null,
      'selectionAbilityIndex': null,
      'abilities': [],
      'selectedHero': null
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

        // Abilties
        let skills = hero.abilities.filter(a => a.enabled === true)

        hero.skills = skills.filter(a => a.ultimate === false)
        while (hero.skills.length !== 3) {
          hero.skills.push(GetNextUnknown())
        }

        // Ultimate
        hero.ultimate = skills.filter(a => a.ultimate === true)[0]
        if (!hero.ultimate) {
          hero.ultimate = GetNextUnknown()
        }
      }

      vm.heroes = heroes

      if (vm.$route.query.radiant) {
        let query = vm.$route.query.radiant.split(',').map(lhs => parseInt(lhs))
        let collection = []
        for (let i = 0; i < query.length; i++) {
          const id = query[i]
          const data = heroes.filter(h => h.id === id)
          if (data.length === 1) {
            collection.push(data[0])
          }
        }
        vm.radiant = collection
      }

      if (vm.$route.query.dire) {
        let query = vm.$route.query.dire.split(',').map(lhs => parseInt(lhs))
        let collection = []
        for (let i = 0; i < query.length; i++) {
          const id = query[i]
          const data = heroes.filter(h => h.id === id)
          if (data.length === 1) {
            collection.push(data[0])
          }
        }
        vm.dire = collection
      }

      if (vm.radiant.length === 6 && vm.dire.length === 6) {
        let collection = []
        let teams = [vm.radiant, vm.dire]
        for (let i = 0; i < teams.length; i++) {
          const team = teams[i]
          for (let h = 0; h < team.length; h++) {
            const hero = team[h]
            let skills = hero.abilities.filter(a => a.enabled === true)
            collection = collection.concat(skills)
          }
        }

        vm.abilities = collection
        vm.step = 2
      }

      if (vm.$route.query.hero) {
        debugger
        let heroID = parseInt(vm.$route.query.hero)
        const data = heroes.filter(h => h.id === heroID)
        if (data.length === 1) {
          vm.selectedHero = data[0]
        }
      }

      if (vm.$route.query.pool) {
        let collection = []
        let ids = vm.$route.query.pool.split(',').map(lhs => parseInt(lhs))
        for (let h = 0; h < heroes.length; h++) {
          const hero = heroes[h]
          for (let a = 0; a < hero.abilities.length; a++) {
            const ability = hero.abilities[a]
            if (ids.includes(ability.id)) {
              collection.push(ability)
            }  
          }
        }
        vm.abilities = collection
      }

      if (vm.selectedHero != null && vm.abilities.length === 48) {
        vm.step = 3
      }

      // http://localhost:8080/#/draft?hero=4&pool=5585,5586,5587,5588,5365,5366,5368,5369,5345,5346,5347,5348,5003,5004,5005,5006,5007,5008,5009,5010,5012,5011,5014,5013,5320,5321,5322,5323,5015,5016,5017,5018,5285,5286,5287,5288,5400,5401,5402,5403,5548,5549,5550,5551,5279,5280,5281,5282

      vm.ready = true
    }).catch(function () {
      vm.$router.push('/error')
    })
  }
}
</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped>
.modal-content {
  background-color: #0B1425 !important;
}
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
.hero-selected {
  border-width: 4px !important;
}
</style>
