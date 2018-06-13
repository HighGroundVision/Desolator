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
    <div v-if="phase === 1">
      <b-row>
        <b-col>
          <p>Et excepteur nulla ea occaecat pariatur sunt nulla sunt. Consequat esse ipsum quis exercitation cillum voluptate non sint eu eiusmod officia mollit deserunt aliqua. Sint ipsum minim aliqua consectetur eiusmod cillum reprehenderit commodo non. Sint cillum dolor fugiat ut nostrud. Aliquip nisi irure consectetur consectetur eiusmod eu ullamco aute laboris eiusmod nostrud. Nulla ipsum ad sunt id officia minim sunt.</p>
        </b-col>
      </b-row>
      <b-row>
        <b-col>
          <b-form-input v-model="filter" placeholder="Filter by Hero"  />
        </b-col>
      </b-row>
      <br />
      <b-row>
        <b-col class="text-center">
          <draggable v-model="poolHeroes" :options="{group:'heroes',sort:false}">
            <b-img v-for="item in poolHeroes" :key="item.id" :src="item.icon" :title="item.name" class="moveable" />
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
      <br />
      <b-row>
        <b-col>
          <b-btn v-if="phase1Complete" variant="primary" @click="heroesSelected">Next</b-btn>
          <b-btn v-else variant="secondary">Next</b-btn>
        </b-col>
      </b-row>
    </div>
    <!-- STEP # 2 -->
    <div v-if="phase === 2">
      <b-row>
        <b-col>
          <p>Lorem ipsum dolor sit amet, consectetur adipiscing elit. Aenean porttitor rhoncus vehicula. Vivamus sodales risus orci, nec cursus ex pulvinar et. Praesent nunc libero, rutrum et quam et, condimentum scelerisque tellus. In et odio nulla. Morbi lacus nisl, maximus vitae nulla ac, venenatis sagittis nisi. </p>
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
      <br />
      <b-row>
        <b-col>
          <b-btn v-if="phase2Complete" variant="primary" @click="abilitiesSelected">Next</b-btn>
          <b-btn v-else variant="secondary">Next</b-btn>
        </b-col>
      </b-row>
    </div>
    <!-- STEP # 3 -->
    <div v-if="phase === 3">
      <p>#WINNING!</p>
      <!--
      <b-row>
        <b-col>
          <p>Vivamus sodales risus orci, nec cursus ex pulvinar et. Praesent nunc libero, rutrum et quam et, condimentum scelerisque tellus. In et odio nulla. Morbi lacus nisl, maximus vitae nulla ac, venenatis sagittis nisi. </p>
        </b-col>
      </b-row>
      <b-row>
        <b-col class="text-right">
          <b-row>
            <b-col>
              <b-img :src="radiant[0].ultimate.img" class="ability-icon ability-icon-lg" @click="selectUltimate(radiant[0], radiant[0].ultimate)" />
              <b-img :src="radiant[1].ultimate.img" class="ability-icon ability-icon-lg" @click="selectUltimate(radiant[1], radiant[1].ultimate)" />
              <b-img :src="radiant[2].ultimate.img" class="ability-icon ability-icon-lg" @click="selectUltimate(radiant[2], radiant[2].ultimate)" />
            </b-col>
          </b-row>
          <b-row>
            <b-col>
              <b-img :src="radiant[3].ultimate.img" class="ability-icon ability-icon-lg" @click="selectUltimate(radiant[3], radiant[3].ultimate)" />
              <b-img :src="radiant[4].ultimate.img" class="ability-icon ability-icon-lg" @click="selectUltimate(radiant[4], radiant[4].ultimate)" />
              <b-img :src="radiant[5].ultimate.img" class="ability-icon ability-icon-lg" @click="selectUltimate(radiant[5], radiant[5].ultimate)" />
            </b-col>
          </b-row>
        </b-col>
        <b-col class="text-left">
          <b-row>
            <b-col>
              <b-img :src="dire[2].ultimate.img" class="ability-icon ability-icon-lg" @click="selectUltimate(dire[2], dire[2].ultimate)" />
              <b-img :src="dire[1].ultimate.img" class="ability-icon ability-icon-lg" @click="selectUltimate(dire[1], dire[1].ultimate)" />
              <b-img :src="dire[0].ultimate.img" class="ability-icon ability-icon-lg" @click="selectUltimate(dire[0], dire[0].ultimate)" />
            </b-col>
          </b-row>
          <b-row>
            <b-col>
              <b-img :src="dire[5].ultimate.img" class="ability-icon ability-icon-lg" @click="selectUltimate(dire[5], dire[5].ultimate)" />
              <b-img :src="dire[4].ultimate.img" class="ability-icon ability-icon-lg" @click="selectUltimate(dire[4], dire[4].ultimate)" />
              <b-img :src="dire[3].ultimate.img" class="ability-icon ability-icon-lg" @click="selectUltimate(dire[3], dire[3].ultimate)" />
            </b-col>
          </b-row>
        </b-col>
      </b-row>
      <br />
      <b-row>
        <b-col class="text-right">
          <template v-for="item in radiant">
            <b-row :key="item.id">
              <b-col>
                <template v-for="skill in item.skills">
                  <b-img :key="skill.id" :src="skill.img" class="ability-icon ability-icon-lg" @click="selectAbility(item, skill)" />
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
                  <b-img :key="skill.id" :src="skill.img" class="ability-icon ability-icon-lg"  @click="selectAbility(item, skill)" />
                </template>
              </b-col>
            </b-row>
          </template>
        </b-col>
      </b-row>
      -->
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
    heroesSelected () {
      this.phase = 2
    },
    abilitiesSelected () {
      this.phase = 3
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
    poolHeroes () {
      let collection = this.heroes

      // Filter by Name
      if (this.filter) {
        let f = this.filter.toLowerCase()
        collection = collection.filter((h) => { return h.name_lower.includes(f) })
      }

      // Filter by Selected
      collection = collection.filter(h => !this.radiant.includes(h)).filter(h => !this.dire.includes(h))

      // Sort by Name
      collection.sort((lhs, rhs) => { return lhs.name.localeCompare(rhs.name) })

      return collection
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
    phase1Complete () {
      return this.radiant.length === 6 && this.dire.length === 6
    },
    phase2Complete () {
      return this.abilities.length === 48 && this.selectedHero != null
    }
  },
  data () {
    return {
      'ready': false,
      'phase': 1,
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
        let abilities = []
        let teams = [vm.radiant, vm.dire]
        for (let i = 0; i < teams.length; i++) {
          const team = teams[i]
          for (let h = 0; h < team.length; h++) {
            const hero = team[h]
            let skills = hero.abilities.filter(a => a.enabled === true)
            abilities = abilities.concat(skills)
          }
        }

        vm.abilities = abilities
        vm.phase = 2
      }

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
