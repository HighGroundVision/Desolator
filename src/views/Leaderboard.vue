<template>
  <section>
    <b-row>
      <b-col>
        <h4  class="text-center">Leaderboard</h4>
        <hr class="highlighted" />
        <p>
          We track alot of ability draft players.
          If you have have a <b class="text-info">'Expose Public Match Data'</b> setting enabled in Dota2 and play more then the <b class="text-info">average of {{floorNumber(leaderboard.average_matches)}} matches</b> with our window then your included.
          If you meet the criteria above then you can search for you yourself or you friends to see where you/they rank.
          Our current collection window started on <b class="text-info">{{ formatDateTime(summary.range.start) }}</b> 
          and was last exported on <b class="text-info">{{ formatDateTime(summary.range.end) }}</b>.
        </p>
        <b-row>
          <b-col cols="4">
            <h5 class="text-center">Win Rate</h5>
            <template v-for="(value, i) in leaderboard.win_rate">
              <div :key="value.account_id">
                <div  class="card">
                  <b-badge class="card-rank" :variant="rankColor(i+1)">
                    <h5>{{rankNumber(i+1)}}</h5>
                  </b-badge>
                  <div class="card-body">
                    <div class="card-title">
                      <h5>{{formatPercentage(value.win_rate)}}</h5>
                    </div>
                    <p class="card-text" style="height: 60px;">
                      <a :href="value.profile_url">{{value.name}}</a>
                      <br />
                      Wins: <span>{{value.wins}}</span><br />
                      Matches: <span>{{value.matches}}</span><br />
                    </p>
                  </div>
                </div>
                <br />
              </div>
            </template>
          </b-col>
          <b-col cols="4">
            <h5 class="text-center">Wins</h5>
            <template v-for="(value, i) in leaderboard.wins">
              <div :key="value.account_id">
                <div  class="card">
                  <b-badge class="card-rank" :variant="rankColor(i+1)">
                    <h5>{{rankNumber(i+1)}}</h5>
                  </b-badge>
                  <div class="card-body">
                    <div class="card-title">
                      <h5>{{value.wins}}</h5>
                    </div>
                    <p class="card-text" style="height: 60px;">
                      <a :href="value.profile_url">{{value.name}}</a>
                      <br />
                      Matches: <span>{{value.matches}}</span><br />
                      Win Rate: <span>{{formatPercentage(value.win_rate)}}</span>
                    </p>
                  </div>
                </div>
                <br />
              </div>
            </template>
          </b-col>
          <b-col cols="4">
            <h5 class="text-center">Matches</h5>
            <template v-for="(value, i) in leaderboard.matches">
              <div :key="value.account_id">
                <div  class="card">
                  <b-badge class="card-rank" :variant="rankColor(i+1)">
                    <h5>{{rankNumber(i+1)}}</h5>
                  </b-badge>
                  <div class="card-body">
                    <div class="card-title">
                      <h5>{{value.matches}}</h5>
                    </div>
                    <p class="card-text" style="height: 60px;">
                      <a :href="value.profile_url">{{value.name}}</a>
                      <br />
                      Wins: <span>{{value.wins}}</span><br />
                      Win Rate: <span>{{formatPercentage(value.win_rate)}}</span>
                    </p>
                  </div>
                </div>
                <br />
              </div>
            </template>
          </b-col>
        </b-row>
        <hr class="highlighted" />
        <b-row>
          <b-col>
            <h5 class="text-center">Find Player</h5>
            <p>Enter your Gamer Tag, Dota Account ID, or Steam Profile ID</p>
            <b-form @submit.prevent="findPlayer">
              <b-input-group>
                <b-form-input type="text" v-model="search" />
                <b-input-group-addon>
                  <b-button variant="success" @click="findPlayer">Search</b-button>
                </b-input-group-addon>
              </b-input-group>
            </b-form>
            <hr style="height: 50px;" />
            <template v-for="(value) in player">
              <div :key="value.account_id">
                <div  class="card">
                  <b-badge class="card-rank" variant="info">
                    <h5>{{rankNumber(value.rank)}}</h5>
                  </b-badge>
                  <div class="card-body">
                    <div class="card-title">
                      <a :href="value.profile_url">{{value.name}}</a>
                    </div>
                    <p class="card-text" style="height: 60px;">
                      Wins: <span>{{value.wins}}</span><br />
                      Matches: <span>{{value.matches}}</span><br />
                      Win Rate: <span>{{formatPercentage(value.win_rate)}}</span>
                    </p>
                  </div>
                </div>
                <br />
              </div>
            </template>
          </b-col>
          <b-col>
            <div v-if="compared">
              <b-button class="centered" variant="primary" @click="compared = false">Compare</b-button>
            </div>
            <div v-else>
              <h5 class="text-center">Compare</h5>
              <p>Enter a friends Gamer Tag, Dota Account ID, or Steam Profile ID</p>
              <b-form @submit.prevent="comparePlayer">
                <b-input-group>
                  <b-form-input type="text" v-model="compare" />
                  <b-input-group-addon>
                    <b-button variant="success" @click="comparePlayer">Search</b-button>
                  </b-input-group-addon>
                </b-input-group>
              </b-form>
              <hr style="height: 50px;" />
              <template v-for="(value) in comparables">
                <div :key="value.account_id">
                  <div  class="card">
                    <b-badge class="card-rank" variant="info">
                      <h5>{{rankNumber(value.rank)}}</h5>
                    </b-badge>
                    <div class="card-body">
                      <div class="card-title">
                        <a :href="value.profile_url">{{value.name}}</a>
                      </div>
                      <p class="card-text" style="height: 60px;">
                        Wins: <span>{{value.wins}}</span><br />
                        Matches: <span>{{value.matches}}</span><br />
                        Win Rate: <span>{{formatPercentage(value.win_rate)}}</span>
                      </p>
                    </div>
                  </div>
                  <br />
                </div>
              </template>
            </div>
          </b-col>
        </b-row>
      </b-col>
      <b-col cols="4" lg="3">
        <div class="text-center">
          <b-alert variant="info" show>   
            <span>
              Shout-out to this sites creator
            </span>
            <br />
            <template v-for="(creator) in leaderboard.creators">
              <div :key="creator.profile_id">
                <span><a :href="creator.profile_url" target="_blank">{{creator.name}}</a></span><br />
                <span>Rank {{ rankNumber(creator.rank) }}</span><br />
                <span>Win Rate {{formatPercentage(creator.win_rate)}}</span>
              </div>
            </template>
            <span>Go play more Dota...</span>
          </b-alert>
          <img src="@/assets/imgs/cluckles-speach.png" class="cluckles-speach" />
        </div>
      </b-col>
    </b-row> 
  </section>
</template>

<script>
import moment from 'moment'
import numeral from 'numeral'
import summary from '@/assets/data/summary.json'
import players from '@/assets/data/leaderboard-collection.json'
import leaderboard from '@/assets/data/leaderboard-summary.json'

export default {
  name: 'leaderboard',
  data () {
    return {
      'leaderboard': leaderboard,
      'summary': summary,
      'search': null,
      'player': [],
      'compared': true,
      'compare': null,
      'comparables': [],
    }
  },
  methods: {
    formatDateTime(value) {
      return moment(value).format("MMMM Do");
    },
    formatDuration(start, end) {
      var x = moment(start);
      var y = moment(end);
      var duration = moment.duration(x.diff(y))
      return duration.humanize();
    },
    formatPercentage(value) {
      return numeral(value).format('0%');
    },
    floorNumber(value) {
      return numeral(value).format('0');
    },
    rankNumber(value) {
      return numeral(value).format('0o');
    },
    rankColor(value) {
      return value == 1 ? 'success' : value == 2 ? 'warning' : 'info';
    },
    findPlayer() {
      if(this.search) {
        let s = this.search.toLowerCase();
        this.player = players.filter(_ => _.name.toLowerCase().includes(s) || _.account_id == s || _.profile_id == s);
      } else {
        this.player = [];
      }
    },
    comparePlayer() {
      if(this.compare) {
        let s = this.compare.toLowerCase();
        this.comparables = players.filter(_ => _.name.toLowerCase().includes(s) || _.account_id == s || _.profile_id == s);
      } else {
        this.comparables = [];
      }
    }
  }
}
</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped>
.card-rank {
  position: absolute; 
  right: -10px; 
  top:-10px;
}
.centered {
  position: absolute; 
  left: 45%;
  top: 45%;
}
</style>
