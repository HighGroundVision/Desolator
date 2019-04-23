<template>
  <hgv-loading :urls="urls" v-on:loaded="loaded">
    <b-row>
      <b-col>
        <h4  class="text-center">Leaderboard</h4>
        <hr class="highlighted" />
        <p>
          We track a lot of ability draft players.
          If you have have a <b class="text-info">'Expose Public Match Data'</b> setting enabled in Dota2 and play more then the <b class="text-info">average of {{leaderboard.average_matches}} matches</b> with our window then your included.
          If you meet the criteria above then you can search for you yourself or you friends to see where you/they rank.
        </p>
        <p v-if="range">
          We reset our baseline when a major patch is released that changes the balance of abilities.
          We normally export our master database roughly once a week.
          Our current stats collection was started on <b class="text-info">{{ formatDateTime(range.start) }}</b> and was last exported on <b class="text-info">{{ formatDateTime(range.end) }}</b>, that is approximately <b class="text-info">{{ formatDuration(range.start, range.end) }}</b>, with a total of <b class="text-info">{{ formatNumber(range.matches) }}</b> AD matches processed. 
          But we did notice that <b class="text-info">{{ formatPercentage(range.abandoned_ratio) }}</b> of matches where abandoned, you can do better people!
        </p>
        <table class="table table-sm" style="color: white;">
          <template v-for="(value, key) in leaderboard.regions" >
            <template v-for="(item, index) in value" >
              <tr :key="item.account_id">
                <td>
                  <span v-if="index === 0">{{key}}</span>
                </td>
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
                <span>with a {{formatPercentage(creator.win_rate)}} Win Rate.</span>
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
import moment from 'moment'
import numeral from 'numeral'

export default {
  name: 'leaderboard',
  data () {
    return {
      'urls': ['/static/leaderboard-collection.json', '/static/leaderboard-regions.json', '/static/schedule.json', '/static/regions.json'],
      'players': [],
      'leaderboard': [],
      'regions': [],
      'creators': [],
      'range':  null,
    }
  },
  methods: {
    loaded(data) {
      var self = this;

      self.players = data[0];
      self.leaderboard = data[1];
      self.range = data[2].range;
      
      self.regions = [];
      var _regions = data[3];
      for (const key in _regions) {
        if (_regions.hasOwnProperty(key)) {
          const element = _regions[key];
          self.regions.push({value: key, text: element })
        }
      }

      self.creators = self.players.players.filter(_ => _.account_id === 13029812);
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
