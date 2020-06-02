<template>
  <div>
     <section class="wrapper header special">
      <div class="inner">
        <h1>Profile</h1>
      </div>
     </section>
     
     <section class="wrapper style2">
        <div class="inner">
          <loader :loading="loading">
            <section v-if="missing">
              <p>
                We can't find your profile. There are number of reasons why this could be.
              </p>
              <ol>
                <li>
                  <span>The account number provided dose not exist. Double check you have the correct value.</span>
                </li>
                <li>
                  <span>Have you played any Ability Draft matches since patch 7.25 when we started exporting profiles. Currently we only export matches going forward (from that time) so your previous matches are not include. This is to make sure our history aligns with when we started tracking the ladder.</span>
                </li>
                <li>
                  <span>You don't have the "Expose Public Match Data" option checked in Dota 2 settings. Unfortunately because you where anonymous we will not be able to retrieve your history. Only future matches will be tracked.</span>
                </li>
                <li>
                  <span>If also else fails we may have as bug. Get in touch with us to report it and we will look into it.</span>
                </li>
              </ol>
            </section>
            <section v-else>
              
              <div style="line-height: 64px;">
                  <img :src="details.avatar" class="avatar" style="float:right;" />
                  <h2>{{details.persona}}</h2>
              </div>
              <hr />
              <h3>Regions</h3>
              
              <p>
                We summarize the matches in each region played in. 
                This means the player will have a rank in each region and we include the delta from the maximum rank in this region.
                We also added the total matches played in each region and its delta from the maximum.
                As well computed the win rate for each region.
              </p>
              <p>
                If the player has a green <i class="fas fa-check-circle w3-text-green" title="Calibrated"></i> then they are calibrated in that region.
                When the player has more then minimum limit of matches in a region they are calibrated and they will be eligible for the leaderboard in that region.
              </p>
              <div class="w3-row-padding">
                <template v-for="(item) in details.summaries">
                  <div v-bind:key="item.regionName" class="w3-col s12 m6 l4 w3-padding-small">
                    <div class="w3-card w3-row w3-padding">
                      <div class="w3-col" style="width:64px;">
                        <i class="fas icon-location" v-bind:class="'fa-'+item.regionGroup" ></i>
                      </div>
                      <div class="w3-rest w3-padding-small">
                        <div style="float:right;">
                          <i v-if="item.calibrated" class="fas fa-check-circle w3-text-green fa-2x" title="Calibrated"></i>
                          <i v-else class="fas fa-times-circle w3-text-red fa-2x" title="Not Calibrated"></i>
                        </div>
                        <h4 style="margin: 0;">
                          {{item.regionName}}
                        </h4>
                        <b>Rating: </b><span>{{Math.round(item.ranking)}}</span> | 
                        <b v-if="item.deltaRaking > 0" title="Delta from max ranking">-{{Math.round(item.deltaRaking)}}</b>
                        <i v-else class="fas fa-crown" style="color: gold;" title="Top of the Ladder"></i>
                        <br />
                        <b>Matches: </b><span>{{item.total}}</span> | <b title="Delta from max matches">-{{item.deltaTotal}}</b>
                        <br />
                        <b>Win Rate: </b><span>{{Math.round(item.winRate* 100)}}%</span>
                      </div>
                    </div>
                  </div>
                </template>
              </div>
              <br />
              <div class="w3-card w3-padding w3-round-large" style="background-color: #f6755e;">
                <p style="margin: 0px;">
                  <b>
                    <i class="fas fa-bullhorn"></i> 
                    This is a new feature as of Patch 7.25. 
                    Currently we only export matches going forward (with time) so your previous matches are not include. 
                    This is to make sure our history aligns with when we started tracking the ladder.
                  </b>
                </p>
              </div>
              
              <hr />
              <h3>History</h3>
              <p>
                The history of the players most recent matches.
                We include basic match details and the players draft with a link to details match statistics provided by <a href="https://www.opendota.com/">OpenDota</a>.
                We have also included the public combatants for each match and their draft.
              </p>
              <div>
                <table class="w3-table w3-bordered">
                  <template v-for="(item) in history">
                    <tr v-bind:key="item.matchId" class="w3-pale-blue">
                      <td>
                        <b :title="formateDate(item.date)">{{humizeDate(item.date)}}</b>
                      </td>
                      <td>
                        <span v-if="item.victory" class="w3-tag w3-padding-small w3-round-large w3-center w3-pale-green" >Victory</span>
                        <span v-else class="w3-tag w3-padding-small w3-round-large w3-center w3-pale-red" >Defeat </span>
                      </td>
                      <td>
                        <b><a :href="'https://www.opendota.com/matches/' +  item.matchId" target="_blank" style="text-decoration: none;">{{item.matchId}}</a></b>
                      </td>
                      <td>
                      </td>
                      <td>
                      </td>
                    </tr>
                    <tr v-bind:key="item.matchId + ':' + details.accountId">
                      <td>
                      </td>
                      <td>
                      </td>
                      <td>
                        <b>{{details.persona}}</b>
                      </td>
                      <td>
                        <router-link :to="'/hero/' + item.hero.id">
                          <img :src="item.hero.image" :title="item.hero.name" />
                        </router-link>
                      </td>
                      <td>
                        <template v-for="(ability) in item.abilities">
                          <div v-bind:key="ability.id" style="display: inline-block; margin: 2px;">
                          <router-link :to="'/ability/' + ability.id" >
                            <img class="ability-icon-xs w3-round" :src="ability.image" :title="ability.name" />
                          </router-link>
                          </div>
                        </template>
                      </td>
                    </tr>
                    <template v-for="(combatant) in item.with">
                      <tr v-bind:key="item.matchId + ':' + combatant.accountId">
                        <td>
                        </td>
                        <td>
                          <span>With</span>
                        </td>
                        <td>
                          <b>{{combatant.persona}}</b>
                        </td>
                        <td>
                          <router-link :to="'/hero/' + combatant.hero.id">
                            <img :src="combatant.hero.image" :title="combatant.hero.name" />
                          </router-link>
                        </td>
                        <td>
                          <template v-for="(ability) in combatant.abilities">
                            <div v-bind:key="ability.id" style="display: inline-block; margin: 2px;">
                            <router-link :to="'/ability/' + ability.id" >
                              <img class="ability-icon-xs w3-round" :src="ability.image" :title="ability.name" />
                            </router-link>
                            </div>
                          </template>
                        </td>
                      </tr>
                    </template>
                    <template v-for="(combatant) in item.against">
                      <tr v-bind:key="item.matchId + ':' + combatant.accountId">
                        <td>
                        </td>
                        <td>
                          <span>Against</span>
                        </td>
                        <td>
                          <b>{{combatant.persona}}</b>
                        </td>
                        <td>
                          <router-link :to="'/hero/' + combatant.hero.id">
                            <img :src="combatant.hero.image" :title="combatant.hero.name" />
                          </router-link>
                        </td>
                        <td>
                          <template v-for="(ability) in combatant.abilities">
                            <div v-bind:key="ability.id" style="display: inline-block; margin: 2px;">
                            <router-link :to="'/ability/' + ability.id" >
                              <img class="ability-icon-xs w3-round" :src="ability.image" :title="ability.name" />
                            </router-link>
                            </div>
                          </template>
                        </td>
                      </tr>
                    </template>
                  </template>
                </table>
                <br />
                <div class="w3-center">
                  <button v-if="summarizeHistory" @click="summarizeHistory = false" class="button alt small">More...</button>
                </div>
              </div>
              <hr />
              <h3>Combatants</h3>
              <p>
                We have summarized each public combatant from the players matches.
                The combatant's name will appear in <b class="w3-text-green">green</b> if they are friends with the player.
                We have included the total number of matches player together and break down of matches player with and against.
              </p>
              <div>
                <h4>As Allies</h4>
                <div class="w3-row-padding">
                  <template v-for="(item) in details.combatants.filter(_  => _.with > 1).sort((lhs, rhs) => rhs.with  - lhs.with)">
                    <div v-bind:key="item.accountId" class="w3-col s6 w3-padding">
                      <div class="w3-card w3-padding">
                        <div class="w3-row">
                          <div class="w3-col" style="width:70px;">
                            <img :src="item.avatar" class="avatar" />
                          </div>
                          <div class="w3-rest">
                            <div style="float:right;">
                              <div title="Victories With">
                                <span  class="w3-tag w3-padding w3-round w3-pale-blue w3-center">{{item.victoriesWith}} / {{item.with}}</span>
                              </div>
                            </div>
                            <router-link :to="'/player/' + item.accountId" style="text-decoration: none;">
                              <h3 class="truncate" v-bind:class="{'w3-text-green': item.friend}">{{item.persona}}</h3> 
                            </router-link>                     
                          </div>
                        </div>
                      </div>
                    </div>
                  </template>
                </div>
                <h4>As Enemies</h4>
                <div class="w3-row-padding">
                  <template v-for="(item) in details.combatants.filter(_  => _.against > 1).sort((lhs, rhs) => rhs.against - lhs.against)">
                    <div v-bind:key="item.accountId" class="w3-col s6 w3-padding">
                      <div class="w3-card w3-padding">
                        <div class="w3-row">
                          <div class="w3-col" style="width:70px;">
                            <img :src="item.avatar" class="avatar" />
                          </div>
                          <div class="w3-rest">
                            <div style="float:right;">
                              <div title="Victories Against">
                                <span class="w3-tag w3-padding w3-round w3-pale-blue w3-center">{{item.victoriesAgainst}} / {{item.against}}</span>
                              </div>
                            </div>
                            <router-link :to="'/player/' + item.accountId" style="text-decoration: none;">
                              <h3 class="truncate" v-bind:class="{'w3-text-green': item.friend}">{{item.persona}}</h3> 
                            </router-link>                     
                          </div>
                        </div>
                      </div>
                    </div>
                  </template>
                </div>
              </div>
            </section>
            
          </loader>
        </div>
      </section>
  </div>
</template>

<script>
import axios from 'axios'
import * as moment from 'moment';

export default {
  name: 'profile',
  data () {
    return {
      profile: false,
      loading: false,
      missing: false,
      details: {},
      summarizeHistory: true,
    }
  },
  created() {
    var id = this.fetchAccount()
    this.loadData(id);
  },
  computed: {
    history: function () {
      if(this.details.history)
        return this.summarizeHistory ? this.details.history.slice(0,5) : this.details.history
      else 
        return []
    }
  },
  methods: {
    fetchAccount() {
      let id =  this.$route.params.id
      if(id != undefined)
      {
        this.profile = false
        return id
      }

      let accountId = localStorage.getItem("accountId");
      if(accountId != undefined)
      {
        this.profile = true
        return accountId
      }
      
      this.$router.push({ name: 'login' });
    },
    async loadData(id) {
      this.loading = true
      try {
        let response = await axios.get(process.env.VUE_APP_BASE_API + "api/player/details/" + id)
        this.details = response.data
        this.summarizeHistory = this.details.history.length > 10
        this.setRegion()
      } catch (error) {
        this.missing = true
      }
      this.loading = false
    },
    setRegion() {
      if(!this.profile)
        return

      if(this.details.summaries.length == 0 )
        return

      let regionId = this.details.summaries[0].regionId;
      localStorage.setItem("regionId", regionId);
    },
    formateDate(date) {
      return moment(date).format("dddd, MMMM Do YYYY, h:mm A")
    },
    humizeDate(date) {
      return moment(date).fromNow()
    }
  }
}
</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped>
ul {
  list-style: none;
}
hr  {
  color: orange;
  border: 1px  solid orange;
}
.icon-location {
  font-size:3em;
  padding: 10px;
  color: #f6755e;
}
.ability-icon-xs
{
  width: 32px;
  height: 32px;
}
.avatar {
  height: 64px;
}
.truncate {
  width: 250px;
  white-space: nowrap;
  overflow: hidden;
  text-overflow: ellipsis;
}
</style>

