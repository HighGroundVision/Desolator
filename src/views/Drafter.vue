<template>
  <div>
     <section class="wrapper header special">
      <div class="inner">
        <h1>Drafter</h1>
      </div>
     </section>

     <!-- 
      Options:
      1. Search by Text
      2. Search by Voice
      3. Search by Hero Icons
      4. Search by Ability Icons
      5. Search by Screen Share
      6. Search by Camera (Phone)
      -->
     
     <section class="wrapper style2">
        <div class="inner">
          <div class="w3-center">
            <p>
              For those that need a little assistance with drafting don't worry Cluckles will guide you.
              You can filter the combos specifically for your hero and limit the abilities to only those in the draft.
              Select the heroes in the draft including the <span title="Phantom Heroes are ones that are not part of either team but contribute their abilities to the draft.">phantom heroes</span>.
            </p>
          </div>
          <loader :loading="loading">
            <div class="w3-center">
              <ul>
                <li style="display:inline;">
                  <span class="w3-tag w3-round w3-xlarge" style="cursor: pointer; background-color: #25a2c3;" @click="filter.selection = ''">~</span>
                </li>
                <template v-for="(item) in filter.options">
                  <li v-bind:key="item" style="display:inline;">
                    <span class="w3-tag w3-round w3-xlarge" style="cursor: pointer; background-color: #25a2c3;" @click="filter.selection = item">{{item}}</span>
                  </li>
                </template>
              </ul>
            </div>
            <div class="w3-center">
              <template v-for="(hero) in heroes">
                <img v-bind:key="hero.id" v-if="hero.enabled" :src="hero.imageIcon" @click="selectHero(hero)" v-bind:class="{ 'hero-disabled': limited && hero.primary == false }" class="hero-icon" />
              </template>
            </div>
            <div v-if="roster.length > 0">
              <hr />
              <h2 class="w3-center">Ability Pool</h2>
            </div>
            <div class="w3-center" style="width: 100%;">
              <div class="w3-row" style="width:50%; display: inline-block;">
                <template v-for="(ability) in ultimates">
                  <div v-bind:key="ability.id" class="w3-col s2">
                    <img v-bind:key="ability.id" :src="ability.image" class="ability-icon w3-round-large" style="margin: 5px;" />
                  </div>
                </template>
              </div>
            </div>
            <div class="w3-center" style="width: 100%;">
              <div class="w3-row" style="width:50%; display: inline-block;">
                <br />
                <template v-for="(ability) in skills">
                  <div v-bind:key="ability.id" class="w3-col s2">
                    <img :src="ability.image" class="ability-icon w3-round-large" style="margin: 5px;" />
                  </div>
                </template>
              </div>
            </div>
            <div v-if="roster.length > 0" class="w3-center">
              <hr />
              <h2>Hero Roster</h2>
              <p>
                To continue select your hero from the roster.
              </p>
            </div>
            <div class="w3-center">
              <template v-for="(hero, index) in roster">
                <img v-bind:key="hero.id" :src="hero.imageProfile" @click="pickHero(hero.id)" v-bind:class="{ 'roster-disabled': index >= 10 }" class="hero-profile w3-round-large" style="margin: 5px;" />
              </template>
            </div>
          </loader>
        </div>
      </section>
  </div>
</template>

<script>
import axios from 'axios'

export default {
  name: 'guide',
  data () {  
    return {
      loading: false,
      filter: {
        options: ['A','B','C','D','E','F','G','H','I','J','K','L','M','N','O','P','Q','R','S','T','U','V','W','X','Y','Z'],
        selection: '',
      },
      pool: [],
      roster: [],
    }
  },
  created() {
    this.loadData()
  },
  computed: {
    heroes: function () {
      return this.pool.filter(_ => _.enabled == true).filter(_ => (this.filter.selection) ? _.name.startsWith(this.filter.selection) : true)
    },
    skills: function() {
      return this.roster.map(_ => _.abilities).flat().filter(_ => _.enabled == true).filter(_ => _.isGranted == false).filter(_ => _.isUltimate == false).slice(0, 36)
    },
    ultimates: function() {
      return this.roster.map(_ => _.abilities).flat().filter(_ => _.enabled == true).filter(_ => _.isGranted == false).filter(_ => _.isUltimate == true).slice(0, 12)
    },
    limited: function() {
      return this.roster.filter(_ => _.primary == false).length > 0
    },
    full: function() {
      return this.skills.length == 36 && this.ultimates.length == 12
    }
  },
  methods: {
    async  loadData() {
      this.loading = true
      let response = await axios.get(process.env.VUE_APP_BASE_API + "api/draft/pool")
      this.pool = response.data
      this.loading = false
    },
    selectHero(hero) {
      if(this.roster.includes(hero))
        this.removeHero(hero)
      else
        this.addHero(hero)
    },
    addHero(hero) {
      if(this.full) {
        return
      }

      if(this.limited && hero.primary == false) {
        return
      }

      this.roster.push(hero)
    },
    removeHero(hero) {
      this.roster = this.roster.filter(_ => _ != hero)
    },
    pickHero(id) {
      if(!this.full) {
        return
      }

      var params = { 
        hero: id,
        roster: this.roster.slice(0, 10).map(_ => _.id),
        ultimates: this.ultimates.map(_ => _.id),
        skills: this.skills.map(_ => _.id),
      }
      let json = JSON.stringify(params)
      let buff = Buffer.from(json)
      let key = buff.toString("base64")
      this.$router.push({ path: 'draft', query: { key: key } })
    }
  }
}
</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped>
hr  {
  margin: 10px !important;
  color: orange;
  border: 1px  solid orange;
}
.hero-disabled {
  opacity: 0.2;
  -webkit-filter: grayscale(100%);
       -moz-filter: grayscale(100%);
         -o-filter: grayscale(100%);
        -ms-filter: grayscale(100%);
            filter: grayscale(100%); 
}
.roster-disabled {
  -webkit-filter: grayscale(100%);
       -moz-filter: grayscale(100%);
         -o-filter: grayscale(100%);
        -ms-filter: grayscale(100%);
            filter: grayscale(100%); 
}
.hero-icon
{
  width: 32px;
  height: 32px;
}
.hero-profile
{
  width: 64px;
  height: 90px;
}
.ability-icon
{
  width: 64px;
  height: 64px;
}
</style>

