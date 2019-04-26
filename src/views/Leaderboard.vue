<template>
  <hgv-loading :urls="urls" v-on:loaded="loaded">
    <b-row>
      <b-col>
        <h4  class="text-center">Leaderboard</h4>
        <hr class="highlighted" />
        <p v-if="range">
          We reset our baseline when a major patch is released that changes the balance of abilities.
          We normally export our master database roughly once a week.
          Our current stats collection was started on <b class="text-info">{{ formatDateTime(range.start) }}</b> and was last exported on <b class="text-info">{{ formatDateTime(range.end) }}</b>, that is approximately <b class="text-info">{{ formatDuration(range.start, range.end) }}</b>, with a total of <b class="text-info">{{ formatNumber(range.matches) }}</b> AD matches processed. 
          But we did notice that <b class="text-info">{{ formatPercentage(range.abandoned_ratio) }}</b> of matches where abandoned, you can do better people!
        </p>
        <div>
          <hgv-regions @change="onRegionChanged" />
        </div>
        <table class="table table-sm" style="color: white;">
          <tr>
            <td>Rank</td>
            <td></td>
            <td>Persona</td>
            <td class="text-center">Win Rate</td>
            <td class="text-center"># Matches</td>
          </tr>
          <template v-for="(value) in leaderboard">
            <template v-for="(item, index) in value.players">
              <tr :key="item.account_id">
                <td>
                  <i v-if="index == 0" class="fas fa-2x fa-award" style="color: #FFD700;" title="1st"></i>
                  <i v-if="index == 1" class="fas fa-2x fa-award" style="color: #C0C0C0;" title="2nd"></i>
                  <i v-if="index == 2" class="fas fa-2x fa-award" style="color: #CD7F32;" title="3rd"></i>
                </td>
                <td>
                  <div>
                    <img :src="item.avatar" class="ability-icon-sm" />
                  </div>
                </td>
                <td>
                  <div>
                    <span>{{item.name}}</span>
                  </div>
                </td>
                <td>
                  <div>
                    <b-progress variant="info" :value="item.win_rate" :min="0" :max="1" :striped="true" show-progress></b-progress>
                  </div>
                </td>
                <td>
                  <div class="text-center">
                    {{item.matches}}
                  </div>
                </td>
              </tr>
            </template>
          </template>
        </table>
        <hr class="highlighted" />
        <b-form @submit.prevent="onSearchChanged">
          <b-form-group label="Search for Player">
            <b-input-group>
              <b-form-input v-model="search" />
               <b-input-group-append>
                 <b-button variant="success" @click="onSearchChanged">Search</b-button>
               </b-input-group-append>
            </b-input-group>
          </b-form-group>
        </b-form>
        <div>
          <table class="table table-sm" style="color: white;">
            <tr v-if="results.length > 0">
              <td>Rank</td>
              <td></td>
              <td>Persona</td>
              <td class="text-center">Win Rate</td>
              <td class="text-center"># Matches</td>
            </tr>
            <template v-for="(item) in results">
              <tr :key="item.account_id">
                <td>
                  {{formatRank(item.rank)}}
                </td>
                <td>
                  <div>
                    <img :src="item.avatar" class="ability-icon-sm" />
                  </div>
                </td>
                <td>
                  <div>
                    <span>{{item.name}}</span>
                  </div>
                </td>
                <td>
                  <div>
                    <b-progress variant="info" :value="item.win_rate" :min="0" :max="1" :striped="true" show-progress></b-progress>
                  </div>
                </td>
                <td>
                  <div class="text-center">
                    {{item.matches}}
                  </div>
                </td>
              </tr>
            </template>
          </table>
        </div>
        <p>
          We track a lot of ability draft players.
          If you have have a <b class="text-info">'Expose Public Match Data'</b> setting enabled in Dota2 and play more then the <b class="text-info">average of {{average_matches}} matches</b> with our window then your included.
          If you meet the criteria above then you can search for you yourself or you friends to see where you/they rank. Make sure you are searching in the correct region.
        </p>
      </b-col>
      <b-col cols="4" lg="3">
        <div class="text-center">
          <b-alert variant="info" show>   
            <span v-if="creators.length > 0">
              Shout-out to this sites creators
            </span>
            <template v-for="(creator) in creators">
              <div :key="creator.profile_id">
                <span><a :href="creator.profile_url" target="_blank">{{creator.name}}</a></span><br />
                <span>with a rank of {{formatRank(creator.rank)}} </span><br />
                <span>and win rate of {{formatPercentage(creator.win_rate)}}</span><br />
                <span>over {{formatNumber(creator.matches)}} matches</span><br />
                <span>in US EAST!</span>
              </div>
            </template>
            <br />
            <span>Go play more Dota...</span>
          </b-alert>
          <img src="@/assets/imgs/cluckles-speach.png" class="cluckles-speach" />
        </div>
      </b-col>
    </b-row> 
  </hgv-loading>
</template>

<script>
export default {
  name: 'leaderboard',
  data () {
    return {
      'urls': ['/static/leaderboard-collection.json', '/static/leaderboard-regions.json', '/static/schedule.json'],
      // Region Selection
      'selection': '',
      // Search for Player
      'players': [],
      'search': null,
      'results': [],
      // Region Summaries
      'regions': [],
      'leaderboard': [],
      // Cluckles
      'creators': [],
      // Matches
      'average_matches': 0,
      'range':  null,
    }
  },
  watch: {
    selection() {
      this.leaderboard = this.regions.filter(_ => _.key == this.selection);
    }
  },
  methods: {
    loaded(data) {
      this.players = data[0];
      this.average_matches = data[1].average_matches;
      this.regions = data[1].regions;
      this.creators = data[1].creators;
      this.range = data[2].range;
      this.selection = '2';
    },
    onRegionChanged(index) {
      this.selection = index;
    },
    onSearchChanged() {
      let search = this.search.toLowerCase();
      if(search) {
        this.results = this.players.filter(_ => _.region == this.selection && _.name.toLowerCase().includes(search));
        this.results.sort((lhs, rhs) => lhs.rank - rhs.rank);
      } else {
        this.results = [];
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
</style>
