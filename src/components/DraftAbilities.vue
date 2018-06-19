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
import {GetUnknownAbility} from '@/assets/draft'

export default {
  name: 'DraftAbilities', 
  methods: {
    clear () {
      this.$router.push({name: 'Draft'})
    },
    back () {
      this.$router.push({name: 'DraftHeroes', query: this.query})
    }
  },
  computed: {
    isStepComplete () {
      return this.abilities.length === 48 && this.selectedHero != null
    }
  },
  data () {
    return {
      'ready': false,
      'query': null,
      'abilities': [],
      'selectedHero': null
    }
  },
  created () {
    const vm = this

    if (vm.$route.query) {
      this.query = vm.$route.query
    } else {
      this.$router.push({name: 'Draft'})
    }

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
          hero.skills.push(GetUnknownAbility())
        }

        // Ultimate
        hero.ultimate = skills.filter(a => a.ultimate === true)[0]
        if (!hero.ultimate) {
          hero.ultimate = GetUnknownAbility()
        }
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
