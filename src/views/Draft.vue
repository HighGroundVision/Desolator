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
              In Ability Draft extreme differences in win rate can highlight a heroes strengths and weaknesses.
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
              The heroes default abilities removed form the combos because they could be required by talents so we wanted to make them easy to find to compare with the talent win rates.
              Keep in mind because the default abilities are always available with this hero the overall numbers are inflated when compared to other combos.
            </p>
            <div class="w3-row">
              <template v-for="(item) in hero.abilities">
                <div v-bind:key="item.id" class="w3-col s6 m3 l3 w3-padding-small w3-center" style="position: relative;">
                  <img @click="selectAbility(item)" class="w3-round-large ability-icon" :src="item.image"  />
                  <h5>{{item.name}}</h5>
                  <span class="w3-tag w3-xlarge w3-round-large" style="position: absolute; top:0px;left:40px;background-color:#f6755e;">{{Math.round(item.winRate * 100)}}%</span>
                  <span class="w3-tag w3-xlarge w3-round-large" style="position: absolute; top:0px;right:40px;background-color:#25a2c3;">{{numeralFormat(item.picks)}}</span>
                </div>
              </template>
            </div>
            <br />
            <h3>Abilities</h3>
            <p>
              A lot of people when talking about Ability Draft will focus on the ability combos as if they apply to all heroes equal.
              This is far from true and therefore we believe it is important to highlight abilities that players are picking with this hero but we have also included in the win rate so you can gage which abilities to prioritize over others.
              We have limited the results return to only include those abilities in the draft. You can use the supplied options to pick or remove abilities from the pool to farther improve the results.
            </p>
            <div class="w3-center">
              <button @click="sortType = 1" class="button small alt" v-bind:class="{special: sortType == 1}">Sort by Picks</button>
              &nbsp;
              <button @click="sortType = 2" class="button small alt" v-bind:class="{special: sortType == 2}">Sort by Win Rate</button>
            </div>
            <br />
            <div class="w3-row-padding">
              <template v-for="(item) in sortedCombos">
                <div v-bind:key="item.id" class="w3-col s1" style="position: relative;">
                  <img @click="selectAbility(item)" :src="item.image" class="w3-round-large ability-icon-s" />
                  <span v-if="sortType == 2" class="w3-tag w3-round w3-small" style="position: absolute; top:-5px;left:0px;background-color:#f6755e;">{{Math.round(item.winRate * 100)}}%</span>
                  <span v-else class="w3-tag w3-round w3-small" style="position: absolute; top:-5px;left:0px;background-color:#25a2c3;">{{numeralFormat(item.picks)}}</span>
                </div>
              </template>
            </div>
            <br />
            <h3>Combos</h3>
            <p>
              A great ability combo on the right hero can dominate a game but no combo is unbeatable even if some look like they are. Just remember to balance against your play style, strategy, and/or item builds.
            </p>
            <div class="w3-row">
              <template v-for="(item) in sortedDraft">
                <div v-bind:key="item.ability.id" class="w3-row-padding">
                  <div class="w3-col s1" style="position: relative;">
                    <img @click="removeAbility(item.ability.id)" :src="item.ability.image" class="w3-round-large ability-icon-s w3-border w3-leftbar w3-rightbar  w3-topbar w3-bottombar w3-border-green" />
                    <span v-if="sortType == 2" class="w3-tag w3-round w3-small" style="position: absolute; top:-5px;left:0px;background-color:#f6755e;">{{Math.round(item.ability.winRate * 100)}}%</span>
                    <span v-else class="w3-tag w3-round w3-small" style="position: absolute; top:-5px;left:0px;background-color:#25a2c3;">{{numeralFormat(item.ability.picks)}}</span>
                  </div>
                  <div class="w3-col s11">
                    <loader :small="true" :loading="item.loading">
                      <template v-for="(combo) in item.sortedCombos">
                        <div v-bind:key="combo.id" class="w3-col s1" style="position: relative;">
                          <img  @click="selectAbility(combo)" v-bind:class="{'w3-border w3-leftbar w3-rightbar  w3-topbar w3-bottombar w3-border-green': draftedAbilities.includes(combo.id)}" :src="combo.image" class="w3-round-large ability-icon-s" />
                          <span v-if="sortType == 2"  class="w3-tag w3-round w3-small" style="position: absolute; top:-5px;left:-5px;background-color:#f6755e;">{{Math.round(combo.winRate * 100)}}%</span>
                          <span v-else class="w3-tag w3-round w3-small" style="position: absolute; top:-5px;left:-5px;background-color:#25a2c3;">{{numeralFormat(combo.picks)}}</span>
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
      sortType: 2,
      combos: [],
      hero: {},
      draft: [],
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
      return this.sortCombosPool(combos, 36)
    },
    sortedDraft: function () {
      let draft = this.draft.slice()
      for (const item of draft) {
         var combos = item.combos.slice()
         item.sortedCombos = this.sortCombosDraft(combos, 11)
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
    sortCombosPool(combos, slice) {
      let self = this
      if(self.hasUltimate && self.hasAbilities) {
        combos = []
      } else if(self.hasUltimate) {
        combos = combos.filter(_ => _.isUltimate == false)
      } else  if(self.hasAbilities) {
        combos = combos.filter(_ => _.isUltimate == true)
      }
      combos.sort((a,b) => (self.sortType == 1) ? b.picks - a.picks : b.winRate - a.winRate)
      return combos.slice(0, slice)
    },
    sortCombosDraft(combos, slice) {
      let self = this
      if(self.hasUltimate) {
        combos = combos.filter(_ => _.isUltimate == false || self.draftedAbilities.includes(_.id))
      }
      if(self.hasAbilities) {
        combos = combos.filter(_ => _.isUltimate == true || self.draftedAbilities.includes(_.id))
      }
      combos.sort((a,b) => (self.sortType == 1) ? b.picks - a.picks : b.winRate - a.winRate)
      return combos.slice(0, slice)
    },
    async loadData() {
      this.loading = true

      let key = this.$route.query.key
      let buff = new Buffer(key, 'base64')
      let json = buff.toString('ascii')
      this.params = JSON.parse(json)

      var response = await axios.get(process.env.VUE_APP_BASE_API + "api/hero/" + this.params.hero)
      this.hero = response.data

      this.combos = this.hero.combos.filter(_ => this.params.skills.includes(_.id) || this.params.ultimates.includes(_.id))

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
      
      axios.get(process.env.VUE_APP_BASE_API + "api/ability/" + ability.id).then((res) => {
        item.combos = res.data.abilities.filter(_ => self.params.skills.includes(_.id) || self.params.ultimates.includes(_.id))
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
</style>

