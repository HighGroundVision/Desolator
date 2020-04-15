<template>
  <div>
     <section class="wrapper header special">
      <div class="inner">
        <h1>Ladder</h1>
      </div>
     </section>
     
     <section class="wrapper style2">
        <div class="inner">
          <p>
            We have created a Elo based rating system that is updated for each <span title="Those that have 'Expose Public Match Data' settings checked">public player</span>.
            We have also combined each region into a global ladder.
            See how you rank against others in your region by changing the regional section.
          </p>
          <p>
            Want to see  what the best AD players in the world are drafting then click on a player and see their recent history of matches.
          </p>
          <div class="w3-card w3-padding w3-round-large" style="background-color: #f6755e;">
            <p style="margin: 0px;">
              <b>
                <i class="fas fa-bullhorn"></i> 
                This is a new feature as of Patch 7.25 and it will take some time before the rankings are correctly disturbed as there was no way to seed the players. 
                So all players start at the default calibration.
                We will remove this alert when the standard deviation looks correct. 
              </b>
            </p>
          </div>
          <br />
          <div class="w3-content">
            <h4>Regions</h4>
            <div class="select-wrapper">
              <select name="region" id="region" v-model="region" @change="changeRegion">
                <option value="0">Global</option>
                <template v-for="(item) in regions">
                  <option v-bind:key="item.id" :value="item.id">{{item.name}} </option>
                </template>
              </select>
            </div>
          </div>
          <hr />
          <loader :loading="loading">
          <div class="w3-row">
            <div v-if="latter.length == 0" class="w3-center">
              <p>No players are calibrated in this region</p>
            </div>
            <template v-for="(item, index) in latter">
              <div v-bind:key="item.accountId" class="w3-col s12 m12 l12 w3-padding-small">
                <div class="w3-card s12 w3-row w3-round w3-padding">
                  <div class="w3-col s1">
                    <i v-if="index == 0" class="fas fa-medal icon-metals gold"></i>
                    <i v-else-if="index == 1" class="fas fa-medal icon-metals silver"></i>
                    <i v-else-if="index == 2" class="fas fa-medal icon-metals bronze"></i>
                    <i v-else class="fas fa-medal icon-metals"></i>
                  </div>
                  <div class="w3-col s8">
                    <img v-if="item.avatar == null" class="profile-icon" src="http://cdn.akamai.steamstatic.com/steamcommunity/public/images/avatars/fe/fef49e7fa7e1997310d705b2a6158ff8dc1cdfeb_full.jpg"  />
                    <img v-else class="profile-icon" :src="item.avatar"  />
                    <h2 style="display: inline; vertical-align: 50%;">
                      <router-link style="text-decoration: none;" :to="'/player/' + item.accountId">
                        <span v-if="item.persona == null">{{item.accountId}}</span>
                        <span v-else>{{item.persona}}</span>
                      </router-link>
                    </h2>
                  </div>
                  <div class="w3-col s1">
                    <i class="fas icon-location" v-bind:class="'fa-'+item.regionGroup" :title="item.regionName" ></i>
                  </div>
                  <div class="w3-col s2">
                    <b>Rating: </b><span>{{Math.round(item.ranking)}}</span>
                    <br />
                    <b>Matches: </b><span>{{item.total}}</span>
                    <br />
                    <b>Win Rate: </b><span>{{Math.round(item.winRate * 100)}}%</span>
                  </div>
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
import axios from "axios"

export default {
  name: 'latter',
  data() {
    let regionId = localStorage.getItem("regionId")
    regionId = regionId == undefined ? 0 : regionId

    return {
      loading: true,
      regions: [],
      region: regionId,
      latter: {}
    }
  },
  mounted() {
    this.loadRegionData()
    this.loadLatterData()
  },
  methods: {
    async loadRegionData() {
      this.loading = true
      var response = await axios.get(process.env.VUE_APP_BASE_API + "api/regions")
      this.regions = response.data
      
      this.loading = false
    },
    async loadLatterData() {
      this.loading = true
      var response = await axios.get(process.env.VUE_APP_BASE_API + "api/leaderboard/region/" + this.region)
      this.latter = response.data

      this.loading = false
    },
    changeRegion() {
      this.loadLatterData()
    }
  }
}
</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped>
hr  {
  color: orange;
  border: 1px  solid orange;
}
.profile-icon
{
  margin: 5px;
  height: 64px;
}
.icon-metals {
  font-size:4em; 
  padding: 10px;
  opacity: 0.1;
}
.icon-metals.gold {
  color: gold;
  opacity: 1;
}
.icon-metals.silver {
  color: silver;
  opacity: 1;
}
.icon-metals.bronze  {
  color: #cd7f32;
  opacity: 1;
}
.icon-location {
  font-size:3em;
  padding: 10px;
  color: #f6755e;
}
</style>


