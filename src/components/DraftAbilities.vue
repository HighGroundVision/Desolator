<template>
  <section v-if="ready" class="opaque-background">
    <b-row>
      <b-col>
        <h1 class="text-warning">Draft</h1>
      </b-col>
    </b-row>
    <b-row>
      <b-col>
        <p>Please fill in any missing skills or ultimate abilities. To help in this process we show the just the skills or ultimates in a dialog with a filter for name.</p>
        <p>Also, you will need to select your hero by clicking on the image.</p>
      </b-col>
    </b-row>
    <b-row>
      <b-col>
        <div class="text-center">
          <h5>Heroes</h5>
        </div>
      </b-col>
    </b-row>
    <b-row>
      <template v-for="item in radiant" >
        <b-col :key="item.id" cols="2">
          <b-img :src="item.img" :title="item.name" @click="selectHero(item)" v-bind:class="isHeroSelected(item)" />
        </b-col>
      </template>
    </b-row>
    <br />
    <b-row>
      <template v-for="item in dire" >
        <b-col :key="item.id" cols="2">
          <b-img :src="item.img" :title="item.name" @click="selectHero(item)" v-bind:class="isHeroSelected(item)" />
        </b-col>
      </template>
    </b-row>
    <hr class="highlighted" />
    <b-row>
      <b-col>
        <div class="text-center">
          <h5 v-b-toggle.collapseUltimates class="grabable" title="Click to toggle">Ultimates</h5>
        </div>
      </b-col>
    </b-row>
    <b-row v-if="isUltimatesFull">
      <b-col>
        <b-collapse id="collapseUltimates" class="mt-2">
          <b-row>
            <template v-for="item in selectedUltimates" >
              <b-col :key="item.id" cols="1">
                <b-img :src="item.img" :title="item.name" class="ability-icon-lg" />
              </b-col>
            </template>
          </b-row>
        </b-collapse>   
      </b-col>
    </b-row>
    <b-row v-else>
      <b-col>
        <div class="text-center">
          <b-btn variant="info" @click="findUltimate">Select Missing Ultimate</b-btn>
        </div>
      </b-col>
    </b-row>
    <hr class="highlighted" />
    <b-row>
      <b-col>
        <div class="text-center">
          <h5 v-b-toggle.collapseSkills class="grabable" title="Click to toggle">Skills</h5>
        </div>
      </b-col>
    </b-row>
    <b-row v-if="isSkillsFull">
      <b-col>
        <b-collapse id="collapseSkills" class="mt-2">
          <b-row>
            <template v-for="item in selectedSkills" >
              <b-col :key="item.id" cols="1">
                <b-img :src="item.img" :title="item.name" class="ability-icon-lg" />
              </b-col>
            </template>
          </b-row>
        </b-collapse>   
      </b-col>
    </b-row>
    <b-row v-else>
      <b-col>
        <div class="text-center">
          <b-btn variant="info" @click="findSkill">Select Missing Skill</b-btn>
        </div>
      </b-col>
    </b-row>
    <hr class="highlighted" />
    <b-row>
      <b-col>
        <b-btn variant="primary" @click="back" class="btn-block">Back</b-btn>
      </b-col>
      <b-col>
        <b-btn v-if="isStepComplete" variant="primary" @click="next" class="btn-block">Next</b-btn>
        <b-btn v-else variant="secondary" class="btn-block">Next</b-btn>
      </b-col>
      <b-col>
        <b-btn variant="danger" @click="clear" class="btn-block">Clear</b-btn>
      </b-col>
    </b-row>
    <!--Dialog - Skills -->
    <b-modal ref="RefSelectAbility" title="Select Missing Ability" size="lg" :hide-footer="true" header-bg-variant="dark" header-text-variant="light" body-bg-variant="dark" footer-bg-variant="dark">
      <b-form-input v-model="filterSkills" placeholder="Filter by Ability"  />
      <br />
      <div style="height: 300px; overflow-x: none; overflow-y: scroll;">
        <template v-for="item in availableSkills">
          <b-img :key="item.id" :src="item.img" :title="item.name" @click="selectSkill(item)" class="ability-icon-lg" />
        </template>
      </div>
    </b-modal>
    <!--Dialog - Ultimates -->
    <b-modal ref="RefSelectUltimate" title="Select Missing Ability" size="lg" :hide-footer="true" header-bg-variant="dark" header-text-variant="light" body-bg-variant="dark" footer-bg-variant="dark">
      <b-form-input v-model="filterUltimate" placeholder="Filter by Ultimate"  />
      <br />
      <div style="height: 300px; overflow-x: none; overflow-y: scroll;">
        <template v-for="item in availableUltimates">
          <b-img :key="item.id" :src="item.img" :title="item.name" @click="selectUltimate(item)" class="ability-icon-lg" />
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
import {filterPool, getHeroes, getAbilities, extractAbilties} from '@/assets/draft'

export default {
  name: 'DraftAbilities', 
  methods: {
    packageData () {
      return {
        'radiant': this.radiant.map(h => h.id).join(','),
        'dire': this.dire.map(h => h.id).join(','),
        'ultimates': this.selectedUltimates.map(h => h.id).join(','),
        'skills': this.selectedSkills.map(h => h.id).join(',')
      }
    },
    updateQueryString () {
      let data = this.packageData()
      this.$router.replace({query: data})
    },
    clear () {
      this.$router.push({name: 'Draft'})
    },
    back () {
      let data = this.packageData()
      this.$router.push({name: 'DraftHeroes', query: data})
    },
    next () {
      let data = this.packageData()
      data.hero = this.selectedHero
      this.$router.push({name: 'DraftLive', query: data})
    },
    findSkill () {
      this.$refs.RefSelectAbility.show()
    },
    findUltimate () {
      this.$refs.RefSelectUltimate.show()
    },
    selectSkill (ability) {
      this.selectedSkills.push(ability)
      this.$refs.RefSelectAbility.hide()
      this.filterSkills = null

      this.updateQueryString()
    },
    selectUltimate (ultimate) {
      this.selectedUltimates.push(ultimate)
      this.$refs.RefSelectUltimate.hide()
      this.filterUltimate = null

      this.updateQueryString()
    },
    selectHero (hero) {
      this.selectedHero = hero.id

      this.updateQueryString()
    },
    isHeroSelected (hero) {
      return {'border border-warning hero-selected': hero.id === this.selectedHero}
    }
  },
  computed: {
    abilities () {
      return [].concat(this.selectedUltimates).concat(this.selectedSkills)
    },
    isUltimatesFull () {
      return this.selectedUltimates.length === 12
    },
    isSkillsFull () {
      return this.selectedSkills.length === 36
    },
    availableSkills () {
      let skills = this.skills

      skills = skills.filter(h => !this.selectedSkills.includes(h))

      if (this.filterSkills) {
        let f = this.filterSkills.toLowerCase()
        skills = skills.filter(a => a.name_lower.includes(f))
      }
      return skills
    },
    availableUltimates () {
      let ultimates = this.ultimates

      ultimates = ultimates.filter(h => !this.selectedUltimates.includes(h))

      if (this.filterUltimate) {
        let f = this.filterUltimate.toLowerCase()
        ultimates = ultimates.filter(a => a.name_lower.includes(f))
      }
      return ultimates
    },
    isStepComplete () {
      return this.isUltimatesFull && this.isSkillsFull && this.selectedHero !== 0
    }
  },
  data () {
    return {
      'ready': false,
      // Available Skills, Ultimates & Filters
      'skills': [],
      'ultimates': [],
      'filterSkills': null,
      'filterUltimate': null,
      // Teams with heroes
      'radiant': [],
      'dire': [],
      // details for next step
      'selectedSkills': [],
      'selectedUltimates': [],
      'selectedHero': 0
    }
  },
  created () {
    const vm = this

    let p1 = axios.get('/static/data/draftpool.json').then((reponse) => { return reponse.data })
    Promise.all([p1]).then((values) => {
      const pool = values[0]
      
      // Get heroes from Pool
      let heroes = filterPool(pool)

      // Get Available Skills and Ultimates
      let availableAbilities = extractAbilties([heroes])
      vm.skills = availableAbilities.filter(a => a.ultimate === false)
      vm.ultimates = availableAbilities.filter(a => a.ultimate === true)

      // Extract Values from Query String
      if (vm.$route.query.radiant && vm.$route.query.dire) {
        vm.radiant = getHeroes(vm.$route.query.radiant, heroes)
        vm.dire = getHeroes(vm.$route.query.dire, heroes)
      }

      // Gruad Teams Are Populated
      if (vm.radiant.length !== 6 && vm.radiant.length !== 6) {
        this.$router.push('/draft')
      }

      if (vm.$route.query.ultimates && vm.$route.query.skills) { 
        // Get Abilities from Query String
        vm.selectedSkills = getAbilities(vm.$route.query.skills, vm.skills)
        vm.selectedUltimates = getAbilities(vm.$route.query.ultimates, vm.ultimates)
      } else {
        // Extract Abilities from Teams
        let teams = [vm.radiant, vm.dire]
        let abilities = extractAbilties(teams)
        vm.selectedSkills = abilities.filter(a => a.ultimate === false)
        vm.selectedUltimates = abilities.filter(a => a.ultimate === true)

        // Update the Query String
        this.updateQueryString()
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
.grabable {
  cursor: -webkit-grab; cursor: grab;
}
.hero-selected {
  border-width: 4px !important;
}
</style>
