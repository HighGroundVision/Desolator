<template>
  <div>
     <section class="wrapper header special">
      <div class="inner">
        <h1>Latter</h1>
      </div>
     </section>
     
     <section class="wrapper style2">
        <div class="inner">
            <p>
              Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Purus viverra accumsan in nisl nisi scelerisque eu ultrices. Vitae ultricies leo integer malesuada nunc vel risus commodo viverra. Quis risus sed vulputate odio. Orci phasellus egestas tellus rutrum tellus pellentesque eu. Tellus in hac habitasse platea dictumst vestibulum rhoncus. Orci porta non pulvinar neque laoreet suspendisse interdum consectetur. Donec et odio pellentesque diam volutpat commodo. Diam maecenas sed enim ut sem viverra aliquet eget. Ultrices tincidunt arcu non sodales neque sodales ut etiam sit. In fermentum et sollicitudin ac. Purus gravida quis blandit turpis cursus in hac habitasse. Egestas egestas fringilla phasellus faucibus scelerisque. Mauris sit amet massa vitae. Ornare massa eget egestas purus.
            </p>
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
            <div class="w3-row-padding">
              <div v-if="latter.length == 0" class="w3-center">
                <p>No players are calibrated in this region</p>
              </div>
              <template v-for="(item) in latter">
                <div v-bind:key="item.accountId" class="w3-col s12 m12 l12 w3-padding-small">
                  <div class="w3-card w3-row w3-padding">
                    <div class="w3-col" style="width:64px;">
                      <img class="profile-icon" :src="item.avatar"  />
                    </div>
                    <div class="w3-rest w3-padding-small">
                      <div class="w3-row">
                        <div class="w3-col s12">
                          <b style="color:#25a2c3;">
                            <router-link style="text-decoration: none;" :to="'/player/' + item.accountId">{{item.persona}}</router-link>
                          </b>
                        </div>
                      </div>
                      <div class="w3-row">
                        <div class="w3-col s4">
                          <b>Ranking: </b><span class="w3-tag w3-round-large w3-center" style="background-color: #f6755e;">{{Math.round(item.ranking)}}</span>
                        </div>
                        <div class="w3-col s4">
                          <b>Matches: </b><span class="w3-tag w3-round-large w3-center"  style="background-color: #f6755e;">{{item.total}}</span>
                        </div>
                        <div class="w3-col s4">
                           <b>Win Rate: </b><span  class="w3-tag w3-round-large w3-center" style="background-color: #f6755e;">{{Math.round(item.winRate * 100)}}%</span>
                        </div>
                      </div>
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
    return {
      loading: true,
      regions: [],
      region: 0,
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
      var response = await axios.get("https://tarrasque.azurewebsites.net/api/regions")
      this.regions = response.data
      this.loading = false
    },
    async loadLatterData() {
      this.loading = true

      var url = "https://tarrasque.azurewebsites.net/api/leaderboard/global";
      if(self.region != 0)
        url = "https://tarrasque.azurewebsites.net/api/leaderboard/region/" + this.region

      var response = await axios.get(url)
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
  height: 64px;
}
</style>

