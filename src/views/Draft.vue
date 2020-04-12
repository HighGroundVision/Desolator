<template>
  <div>
     <section class="wrapper header special">
      <div class="inner">
        <h1>Draft</h1>
      </div>
     </section>
     
     <section class="wrapper style2">
        <div class="inner">
          <loader :loading="loading">
            <div class="flex flex-3">
              <div class="flex-item left">

              </div>
              <div class="flex-item">
                <h2>{{hero.name}}</h2>
                <img class="w3-round-large" :src="hero.image" style="width:100%;" />
              </div>
              <div class="flex-item right">
                
              </div>
            </div>
            <br />
            <h3>Attributes</h3>
            <p>
              Knowing your hero is important in every mode in the Dota but in Ability Draft this can matter far more.
              Below we listed the primary attributes for the hero sorted by the rank of each attribute.
              Understanding your heroes strengths and weaknesses will help you understand which ability you can use to enhance your strengths / offset your weakness and this can also effect your item build.
            </p>
            <template v-for="(attribute) in hero.attributes">
              <div v-bind:key="attribute.name">
                <div class="w3-row-padding">
                  <div class="w3-third w3-right-align">
                    <span>{{attribute.name}}</span>
                  </div>
                  <div class="w3-third">
                    <div class="w3-border">
                      <div :style="'background-color:#f6755e;height:24px;width:'+ Math.round(attribute.percentage * 100)+'%'"></div>
                    </div>
                  </div>
                  <div class="w3-third">
                    {{attribute.value}}
                  </div>
                </div>
              </div>
            </template>
            <br />
            <h3>Talents</h3>
            <p>
              Some heroes are very ability dependant when its comes to talents.
              Examine each talents win rate to understand which abilities to prioritize.
              <b>Highlighted</b> talents are dependent on the hero's default abilities.
              Talents only work when you draft the associated skill, it will not affect a skill on another player.
            </p>
            <div class="w3-row-padding w3-center">
              <div class="w3-col s4">
                <h5>Description</h5>
              </div>
              <div class="w3-col s1">
                <h5>Win Rate</h5>
              </div>
              <div class="w3-col s2">
                <h5>Level</h5>
              </div>
              <div class="w3-col s1">
                <h5>Win Rate</h5>
              </div>
              <div class="w3-col s4">
                <h5>Description</h5>
              </div>
            </div>
            <template v-for="(talents, key) in hero.talents">
              <div class="w3-row-padding w3-center" v-bind:key="key">
                <div class="w3-col s4">
                  <b v-if="talents[0].key.includes('unique')">{{talents[0].description}}</b>
                  <span v-else>{{talents[0].description}}</span>
                </div>
                <div class="w3-col s1">
                  {{Math.round(talents[0].winRate * 100)}}%
                </div>
                <div class="w3-col s2">
                  <span class="w3-tag w3-round-large" style="background-color:#f6755e;">{{key}}</span>
                </div>
                <div class="w3-col s1">
                  {{Math.round(talents[1].winRate * 100)}}%
                </div>
                <div class="w3-col s4">
                  <b v-if="talents[1].key.includes('unique')">{{talents[1].description}}</b>
                  <span v-else>{{talents[1].description}}</span>
                </div>
              </div>
            </template>
            <br />
            <h3>Abilities</h3>
            <p>
              We have limited the results return to only include those abilities in the draft. 
              A lot of people when talking about Ability Draft will focus on the ability combos as if they apply to all heroes equal but this is not true.
              The abilities can be sorted by priority (how much other players want this ability), picks (compared to this hero), or win rate (compared to this hero).
              We have also included a icon if the ability has Aghanim's scepter upgrade.
              You can select an ability to see it's combos and we will automatically adjust the pool based on your choices.
            </p>
            <div class="w3-center">
              <b>Sort By</b>
              <div>
                <button @click="sortType = 3" class="button small alt w3-margin" v-bind:class="{special: sortType == 3}">Win Rate</button>
                <button @click="sortType = 2" class="button small alt w3-margin" v-bind:class="{special: sortType == 2}">Picks</button>
                <button @click="sortType = 1" class="button small alt w3-margin" v-bind:class="{special: sortType == 1}">Priority</button>
              </div>
            </div>
            <br />
            <div class="w3-row-padding">
              <template v-for="(item) in sortedCombos">
                <div v-bind:key="item.id" class="w3-col s1" style="position: relative;">
                  <img @click="selectAbility(item)" :src="item.image" class="w3-round-large ability-icon-s" />
                  <span class="w3-tag w3-round w3-small" style="position: absolute; top:-5px;left:0px;background-color:#f6755e;">{{Math.round(item.winRate * 100)}}%</span>
                  <span v-if="item.hasUpgrade" class="w3-tag w3-round w3-small" style="position: absolute; top:-5px;right:5px;background-color:#25a2c3;">
                    <img src="https://hgv-hyperstone.azurewebsites.net/mics/aghanims_scepter_upgrade.png" class="aghanims_scepter" />
                  </span>
                </div>
              </template>
            </div>
            <br />
            <h3>Combos</h3>
            <p>
              A great ability combo on the right hero can dominate a game but no combo is unbeatable even if some look like they are. 
              Just remember to balance against your play style, strategy, and/or item builds.
            </p>
            <div class="w3-row">
              <template v-for="(item) in sortedDraft">
                <div v-bind:key="item.ability.id" class="w3-row-padding">
                  <div class="w3-col s1" style="position: relative;">
                    <img @click="removeAbility(item.ability.id)" :src="item.ability.image" class="w3-round-large ability-icon-s" />
                    <span class="w3-tag w3-round w3-small" style="position: absolute; top:-5px;left:0px;background-color:#f6755e;">{{Math.round(item.ability.winRate * 100)}}%</span>
                    <span v-if="item.ability.hasUpgrade" class="w3-tag w3-round w3-small" style="position: absolute; top:-5px;right:5px;background-color:#25a2c3;">
                      <img src="https://hgv-hyperstone.azurewebsites.net/mics/aghanims_scepter_upgrade.png"  class="aghanims_scepter" />
                    </span>
                  </div>
                  <div class="w3-col s11">
                    <loader :small="true" :loading="item.loading">
                      <template v-for="(combo) in item.sortedCombos">
                        <div v-bind:key="combo.id" class="w3-col s1" style="position: relative;">
                          <img @click="selectAbility(combo)" :src="combo.image" class="w3-round-large ability-icon-s" />
                          <span class="w3-tag w3-round w3-small" style="position: absolute; top:-5px;left:-5px;background-color:#f6755e;">{{Math.round(combo.winRate * 100)}}%</span>
                          <span v-if="combo.hasUpgrade" class="w3-tag w3-round w3-small" style="position: absolute; top:-5px;right:5px;background-color:#25a2c3;">
                            <img src="https://hgv-hyperstone.azurewebsites.net/mics/aghanims_scepter_upgrade.png"  class="aghanims_scepter" />
                          </span>
                        </div>
                      </template>
                    </loader>
                  </div>
                </div>
              </template>
            </div>
          </loader>
        </div>
      </section>
  </div>
</template>

<script>
import axios from 'axios'
import numeral from 'numeral';

export default {
  name: 'guide',
  data () {  
    return {
      loading: false,
      params: {},
      sortType: 3,
      combos: [],
      hero: {},
      draft: [],
      abilities: [],
    }
  },
  created() {
    this.loadData()
  },
  computed: {
    hasUltimate: function() {
      return this.draft.filter(_ => _.ability.isUltimate == true).length == 1
    },
    hasAbilities: function() {
      return this.draft.filter(_ => _.ability.isUltimate == false).length == 3
    },
    sortedCombos: function () {
      var combos = this.combos.slice()
      return this.sortCombos(combos, 48)
    },
    sortedDraft: function () {
      let draft = this.draft.slice()
      for (const item of draft) {
         var combos = item.combos.slice()
         item.sortedCombos = this.sortCombos(combos, 11)
      }
      return draft;
    },
    draftedAbilities: function () {
      let draft = this.draft.slice()
      return draft.map(_ => _.ability.id)
    }
  },
  methods: {
    numeralFormat(value) {
      return numeral(value).format('0a')
    },
    sort(sortType, combos) {
      combos.sort((a,b) => {
        if(sortType == 1) {
          return b.priority - a.priority
        } else if (sortType == 2) {
          return b.picks - a.picks
        } else if (sortType == 3) {
          return b.winRate - a.winRate
        }
      })
    },
    sortCombos(combos, slice) {
      combos = combos.filter(_ => this.draftedAbilities.includes(_.id) == false)
      if(this.hasUltimate) {
        combos = combos.filter(_ => _.isUltimate == false)
      }
      if(this.hasAbilities) {
        combos = combos.filter(_ => _.isUltimate == true )
      }
      this.sort(this.sortType, combos)
      return combos.slice(0, slice)
    },
    mergeAbilityData(collection) {
      for (const item of collection) {
        let data = this.abilities.find(_ => _.id == item.id)
        if (data) {
          item.priority = data.priority
          item.hasUpgrade = data.hasUpgrade
          item.isGranted = data.isGranted
        }
      }
    },
    async loadData() {
      this.loading = true

      let key = this.$route.query.key
      let buff = new Buffer(key, 'base64')
      let json = buff.toString('ascii')
      this.params = JSON.parse(json)
      let collection = this.params.skills.concat(this.params.ultimates)

      let responsePool = await axios.get(process.env.VUE_APP_BASE_API + "/api/draft/pool")
      let pool = responsePool.data
        .map(_ => _.abilities)
        .flat()
        .filter(_ => collection.includes(_.id))
        .map(_ => { return { id: _.id, hasUpgrade: _.hasUpgrade, isGranted: _.isGranted } })

      let responseAbilities = await axios.get(process.env.VUE_APP_BASE_API + "/api/abilities")
      let abilities = responseAbilities.data
        .filter(_ => collection.includes(_.id))
        .map(_ => { return { id: _.id, priority: _.total.priority } })

      this.abilities = abilities.map(x => Object.assign(x, pool.find(y => y.id == x.id)))

      let responseHero = await axios.get(process.env.VUE_APP_BASE_API + "api/hero/" + this.params.hero)
      this.hero = responseHero.data

      this.combos = this.hero.combos.filter(_ => collection.includes(_.id))
      this.combos = this.combos.concat(this.hero.abilities)
      this.mergeAbilityData(this.combos)

      this.loading = false
    },
    
    selectAbility(ability) {
      let self = this;

      if(self.draft.filter(_ => _.ability.id == ability.id).length > 0) {
        return
      }

      if(this.hasUltimate && ability.isUltimate == true) {
        return
      }

      if(this.hasAbilities && ability.isUltimate == false) {
        return
      }
      
      let item = {
        ability: ability,
        combos: [],
        sortedCombos: [],
        loading: true,
      }
      self.draft.push(item)

      let collection = this.params.skills.concat(this.params.ultimates)
      
      axios.get(process.env.VUE_APP_BASE_API + "api/ability/" + ability.id).then((res) => {
        item.combos = res.data.abilities.filter(_ => collection.includes(_.id))
        self.mergeAbilityData(item.combos)
        item.loading = false
      })
    },
    removeAbility(id) {
      this.draft = this.draft.filter(_ => _.ability.id != id)
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
.ability-icon
{
  width: 128px;
  height: 128px;
}
.ability-icon-s
{
  width: 64px;
  height: 64px;
}
.ability-icon-xs
{
  width: 30px;
  height: 30px;
}
.truncate {
  width: 200px;
  white-space: nowrap;
  overflow: hidden;
  text-overflow: ellipsis;
  text-decoration: none;
}
.aghanims_scepter {
  width: 15px; 
  height: 15px; 
  vertical-align: text-middle; 
  filter: invert(100%) sepia(0%) saturate(7490%) hue-rotate(331deg) brightness(103%) contrast(99%);
}
</style>

