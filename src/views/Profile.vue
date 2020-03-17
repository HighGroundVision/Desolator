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
            <div style="line-height: 64px;">
                <img :src="details.avatar" style="float:right; height: 64px;" />
                <h2>{{details.persona}}</h2>
            </div>
            <h3>Regions</h3>
            <p>
              Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Purus viverra accumsan in nisl nisi scelerisque eu ultrices. Vitae ultricies leo integer malesuada nunc vel risus commodo viverra. Quis risus sed vulputate odio. Orci phasellus egestas tellus rutrum tellus pellentesque eu. 
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
                      <b>Ranking: </b><span>{{Math.round(item.ranking)}}</span>
                      <br />
                      <b>Matches: </b><span>{{item.total}}</span>
                      <br />
                      <b>Win Rate: </b><span>{{Math.round(item.winRate* 100)}}%</span>
                    </div>
                  </div>
                </div>
              </template>
            </div>
            <br />
            <h3>History</h3>
            <p>
              Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Purus viverra accumsan in nisl nisi scelerisque eu ultrices. Vitae ultricies leo integer malesuada nunc vel risus commodo viverra. Quis risus sed vulputate odio. Orci phasellus egestas tellus rutrum tellus pellentesque eu. 
            </p>
            <div class="table-wrapper">
              <table>
                <thead>
                  <tr>
                    <th>Match</th>
                    <th>Date</th>
                    <th>Hero</th>
                    <th>Abilites</th>
                  </tr>
                </thead>
                <template v-for="(item) in details.history">
                  <tbody v-bind:key="item.matchId">
                    <tr v-bind:class="{ 'w3-pale-green': item.victory, 'w3-pale-red': !item.victory }"> 
                      <td>
                        <a :href="'https://www.opendota.com/matches/' +  item.matchId" target="_blank">{{item.matchId}}</a>
                      </td>
                      <td>
                        <span :title="formateDate(item.date)">{{humizeDate(item.date)}}</span>
                      </td>
                      <td>
                        <router-link :to="'/hero/' + item.hero.id">
                          <img :src="item.hero.image" :title="item.hero.name" />
                        </router-link>
                      </td>
                      <td>
                         <template v-for="(ability) in item.abilities">
                           <div v-bind:key="ability.id" style="display: inline-block; margin: 2px;">
                            <router-link :to="'/ability/' + ability.id">
                              <img class="ability-icon-xs w3-round" :src="ability.image" :title="ability.name" />
                            </router-link>
                           </div>
                         </template>
                      </td>
                     
                    </tr>
                  </tbody>
                </template>
              </table>
            </div>
            <h3>Combatants</h3>
            <p>
              Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Purus viverra accumsan in nisl nisi scelerisque eu ultrices. Vitae ultricies leo integer malesuada nunc vel risus commodo viverra. Quis risus sed vulputate odio. Orci phasellus egestas tellus rutrum tellus pellentesque eu. Tellus in hac habitasse platea dictumst vestibulum rhoncus. Orci porta non pulvinar neque laoreet suspendisse interdum consectetur. Donec et odio pellentesque diam volutpat commodo. 
            </p>
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
      loading: false,
      details: {},
    }
  },
  created() {
    var id = this.fetchAccount()
    this.loadData(id);
  },
  methods: {
    fetchAccount() {
      var id =  this.$route.params.id
      if(id != undefined)
        return id

      var accountId = localStorage.getItem("accountId");
      if(accountId != undefined)
        return accountId

      this.$router.push({ name: 'login' });
    },
    async loadData(id) {
      this.loading = true
      var response = await axios.get("https://tarrasque.azurewebsites.net/api/player/details/" + id)
      this.details = response.data
      this.loading = false
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
</style>

