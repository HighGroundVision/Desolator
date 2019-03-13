<template>
  <section>
    <b-row>
      <b-col>
        <h4  class="text-center">Leaderboard</h4>
        <hr class="highlighted" />
        <p>
          We track the leaders in ability draft. 
          To be include on the leaderboard you need to have more matches in the collection window then the average player.
          We then sort the list of players by win rate and select the top 100.
          Our current collection window started on <b class="text-info">{{ formatDateTime(summary.range.start) }}</b> 
          and was last exported on <b class="text-info">{{ formatDateTime(summary.range.end) }}</b>,
          that is a total of <b class="text-info">{{ formatDuration(summary.range.start, summary.range.end) }}</b>.
        </p>
        <div>
          <b-table 
            :fields="['image', 'personaname', 'wins', 'matches', 'win_rate']" 
            :items="players" 
            >
            <template slot="image" slot-scope="row">
              <img :src="row.item.profile.avatar">
            </template>
            <template slot="personaname" slot-scope="row">
              <a :href="row.item.profile.profileurl" target="_blank">{{ row.item.profile.personaname }}</a>
            </template>
            <template slot="win_rate" slot-scope="row">
              <b-progress height="1.5rem" :value="row.item.win_rate" :min="0" :max="1" :striped="true" show-progress></b-progress>
            </template>
            <template slot="wins" slot-scope="row">
              <span>{{row.item.wins}}</span>
            </template>
            <template slot="matches" slot-scope="row">
              <span>{{row.item.matches}}</span>
            </template>
          </b-table>
        </div>
      </b-col>
      <b-col cols="4" lg="3">
        <div class="text-center">
          <b-alert variant="info" show>   
            <span>
              Shout-out to HGV leader and this sites creator
            </span>
            <br />
            <span><a :href="summary.creator.profile.profileurl" target="_blank">{{summary.creator.profile.personaname}}</a></span>
            <br />
            <span>With a {{formatPercentage(summary.creator.stats.win_rate)}} win rate</span>
            <br />
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
import players from '@/assets/data/players-collection.json'

export default {
  name: 'leaderboard',
  data () {
    players.sort((lhs, rhs) => rhs.win_rate - lhs.win_rate);
    return {
      'summary': summary,
      'players': players
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
    }
  },
  computed: {
    topPlayers: function () {
      return this.players.filter(_ => _.win_rate == 1).map(_ => _.profile.personaname);
    }
  }
}
</script>
