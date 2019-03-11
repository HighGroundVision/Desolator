<template>
  <section>
    <b-row>
      <b-col cols="4" lg="3">
        <div class="text-center">
          <b-alert variant="info" show>   
            <strong>Hi, I am Cluckles!</strong>
            <br />
            <span>AD is hard and confusing, luckily I am here to help make it fun!</span>
          </b-alert>
          <div style="float: right; position: relative; top:-15px;">
            <a href="https://steamcommunity.com/sharedfiles/filedetails/?id=111384500" target="_blank" class="text-white">
              <i class="far fa-question-circle" title="I also come in this really cool Courier form too!"></i>
            </a>
          </div>
          <img src="@/assets/imgs/cluckles-speach.png" class="cluckles-speach" />
        </div>
      </b-col>
      <b-col>
        <b-alert v-if="false" variant="warning" show>
          <b-row>
            <b-col>
              <strong>Under Construction</strong>
              <p>
                We are currently rebuilding the backend and front systems that run this web application. 
                Our continuous deployment process means that this site will be updated as we push out new features. 
                You are welcome to use the site and the new features. 
                Some features maybe unstable/incomplete and maybe removed in the future.
                This banner will be removed when the rework is finished.
              </p>
            </b-col>
          </b-row>
        </b-alert>
        <h4 class="text-center">Welcome!</h4>
        <hr class="highlighted" />
        <p>
          We at HGV are deadacted to the Ability Draft communtiy. 
          Providing the tools to understand AD better, and hopfuly have some fun along the way. 
          Remember everyone that while Carry may kill heroes but Supports win games!
        </p>
        <p>
          The strongest Hero is <b class="text-info">{{ summary.leaders.hero.best_win_rate.name }}</b>. 
          Check out the Heroes list and details pages for more details on each hero.
        </p>
        <div class="card">
          <div class="card-body">
            <h5 class="card-title">Hero Types</h5>
            <b-row>
              <template v-for="(value) in heroes">
                <b-col :key="value.key">
                  <img :src="value.image" class="float-left p-1" />
                  <b>{{value.name}}</b>
                  <b-progress :max="1">
                    <b-progress-bar :value="value.win_rate" variant="info" :striped="true" > 
                      <strong>{{ formatPercentage(value.win_rate) }}</strong>
                    </b-progress-bar>
                    <b-progress-bar :value="1-value.win_rate" variant="secondary"  />
                  </b-progress>
                </b-col>
              </template>
            </b-row>
          </div>
        </div> 
        <br />
        <p>
          The Ability with the most wins is <b class="text-info">{{ summary.leaders.ability.most_wins.name }}</b> while <b class="text-info">{{ summary.leaders.ability.best_win_rate.name }}</b> <span v-if="summary.leaders.ability.best_win_rate.id == summary.leaders.ability.most_wins.id"> also</span> has the highest win rate.
          Check out the Abilities list and details pages for more details on each ability.
        </p>
        <p>
          Shout-out to <b class="text-info">{{ summary.leaders.players.best_win_rate.profile.personaname }}</b> with a win rate of <b class="text-info">{{ formatPercentage(summary.leaders.players.best_win_rate.stats.win_rate) }}</b> over <b class="text-info">{{ summary.leaders.players.best_win_rate.stats.wins }}</b> matches. 
          Props to <b class="text-info">{{ summary.leaders.players.most_wins.profile.personaname }}</b> with <b class="text-info">{{ summary.leaders.players.most_wins.stats.wins }}</b> wins (the most in this export range) with a win rate of <b class="text-info">{{ formatPercentage(summary.leaders.players.most_wins.stats.win_rate) }}</b>.
          Wow! Slow down <b class="text-info">{{ summary.leaders.players.most_matches.profile.personaname }}</b> with over <b class="text-info">{{ summary.leaders.players.most_matches.stats.matches }}</b> matches (the most in this export range) with a win rate of <b class="text-info">{{ formatPercentage(summary.leaders.players.most_matches.stats.win_rate) }}</b>.
          Check out our Leaderboard for more details.
        </p>
        <p>
          We are often asked when do people play AD?
          To that end we have included the daily counts as a percentage of the total matches.
          We also break down the time of day matches are being played, based on the UTC time.
          There is always going to be a little bais based on day and time we run the export.
        </p>
        <div class="card">
          <div class="card-body">
            <div class="float-right">
              <ul class="list-inline">
                <li class="list-inline-item text-primary">Night</li>
                <li class="list-inline-item text-warning">Morning</li>
                <li class="list-inline-item text-success">Afternoon</li>
                <li class="list-inline-item text-danger">Evening</li>
              </ul>
            </div>
            <h5 class="card-title">Time Breakdown</h5>
            <b-row>
              <b-col>Sunday</b-col>
              <b-col>
                 <b-progress :max="100">
                  <b-progress-bar :value="summary.daily.sunday.total" variant="info" :striped="true" > 
                    <strong>{{ summary.daily.sunday.total  }}%</strong>
                  </b-progress-bar>
                  <b-progress-bar :value="100-summary.daily.sunday.total" variant="secondary"  />
                </b-progress>
              </b-col>
              <b-col>
                <b-progress :max="100">
                  <b-progress-bar :value="summary.daily.sunday.hours['1']" variant="primary" :striped="true" >
                    <strong>{{ summary.daily.sunday.hours['1'] }}%</strong>
                  </b-progress-bar>
                  <b-progress-bar :value="summary.daily.sunday.hours['2']" variant="warning" :striped="true" >
                    <strong>{{ summary.daily.sunday.hours['2'] }}%</strong>
                  </b-progress-bar>
                  <b-progress-bar :value="summary.daily.sunday.hours['3']" variant="success" :striped="true" >
                    <strong>{{ summary.daily.sunday.hours['3'] }}%</strong>
                  </b-progress-bar>
                  <b-progress-bar :value="summary.daily.sunday.hours['4']" variant="danger" :striped="true" >
                    <strong>{{ summary.daily.sunday.hours['4'] }}%</strong>
                  </b-progress-bar>
                </b-progress>
              </b-col>
            </b-row>
            <b-row>
              <b-col >Monday</b-col>
              <b-col >
                 <b-progress :max="100">
                  <b-progress-bar :value="summary.daily.monday.total" variant="info" :striped="true" >
                    <strong>{{ summary.daily.monday.total }}%</strong>
                  </b-progress-bar>
                  <b-progress-bar :value="100-summary.daily.monday.total" variant="secondary" />
                </b-progress>
              </b-col>
              <b-col>
                <b-progress :max="100">
                  <b-progress-bar :value="summary.daily.monday.hours['1']" variant="primary" :striped="true" >
                    <strong>{{ summary.daily.monday.hours['1'] }}%</strong>
                  </b-progress-bar>
                  <b-progress-bar :value="summary.daily.monday.hours['2']" variant="warning" :striped="true" >
                    <strong>{{ summary.daily.monday.hours['2'] }}%</strong>
                  </b-progress-bar>
                  <b-progress-bar :value="summary.daily.monday.hours['3']" variant="success" :striped="true" >
                    <strong>{{ summary.daily.monday.hours['3'] }}%</strong>
                  </b-progress-bar>
                  <b-progress-bar :value="summary.daily.monday.hours['4']" variant="danger" :striped="true" >
                    <strong>{{ summary.daily.monday.hours['4'] }}%</strong>
                  </b-progress-bar>
                </b-progress>
              </b-col>
            </b-row>
            <b-row>
              <b-col >Tuesday</b-col>
              <b-col >
                 <b-progress :max="100">
                  <b-progress-bar :value="summary.daily.tuesday.total" variant="info" :striped="true" >
                    <strong>{{ summary.daily.tuesday.total }}%</strong>
                  </b-progress-bar>
                  <b-progress-bar :value="100-summary.daily.tuesday.total" variant="secondary" />
                </b-progress>
              </b-col>
              <b-col>
                <b-progress :max="100">
                  <b-progress-bar :value="summary.daily.tuesday.hours['1']" variant="primary" :striped="true" >
                    <strong>{{ summary.daily.tuesday.hours['1'] }}%</strong>
                  </b-progress-bar>
                  <b-progress-bar :value="summary.daily.tuesday.hours['2']" variant="warning" :striped="true" >
                    <strong>{{ summary.daily.tuesday.hours['2'] }}%</strong>
                  </b-progress-bar>
                  <b-progress-bar :value="summary.daily.tuesday.hours['3']" variant="success" :striped="true" >
                    <strong>{{ summary.daily.tuesday.hours['3'] }}%</strong>
                  </b-progress-bar>
                  <b-progress-bar :value="summary.daily.tuesday.hours['4']" variant="danger" :striped="true" >
                    <strong>{{ summary.daily.tuesday.hours['4'] }}%</strong>
                  </b-progress-bar>
                </b-progress>
              </b-col>
            </b-row>
            <b-row>
              <b-col >Wednesday</b-col>
              <b-col >
                <b-progress :max="100">
                  <b-progress-bar :value="summary.daily.wednesday.total" variant="info" :striped="true" >
                    <strong>{{ summary.daily.wednesday.total }}%</strong>
                  </b-progress-bar>
                  <b-progress-bar :value="100-summary.daily.wednesday.total" variant="secondary" />
                </b-progress>
              </b-col>
              <b-col>
                <b-progress :max="100">
                  <b-progress-bar :value="summary.daily.wednesday.hours['1']" variant="primary" :striped="true" >
                    <strong>{{ summary.daily.wednesday.hours['1'] }}%</strong>
                  </b-progress-bar>
                  <b-progress-bar :value="summary.daily.wednesday.hours['2']" variant="warning" :striped="true" >
                    <strong>{{ summary.daily.wednesday.hours['2'] }}%</strong>
                  </b-progress-bar>
                  <b-progress-bar :value="summary.daily.wednesday.hours['3']" variant="success" :striped="true" >
                    <strong>{{ summary.daily.wednesday.hours['3'] }}%</strong>
                  </b-progress-bar>
                  <b-progress-bar :value="summary.daily.wednesday.hours['4']" variant="danger" :striped="true"  >
                    <strong>{{ summary.daily.wednesday.hours['4'] }}%</strong>
                  </b-progress-bar>
                </b-progress>
              </b-col>
            </b-row>
            <b-row>
              <b-col >Thursday</b-col>
              <b-col >
                 <b-progress :max="100">
                  <b-progress-bar :value="summary.daily.thursday.total" variant="info" :striped="true" >
                    <strong>{{ summary.daily.thursday.total }}%</strong>
                  </b-progress-bar>
                  <b-progress-bar :value="100-summary.daily.thursday.total" variant="secondary" />
                </b-progress>
              </b-col>
              <b-col>
                <b-progress :max="100">
                  <b-progress-bar :value="summary.daily.thursday.hours['1']" variant="primary" :striped="true">
                    <strong>{{ summary.daily.thursday.hours['1'] }}%</strong>
                  </b-progress-bar>
                  <b-progress-bar :value="summary.daily.thursday.hours['2']" variant="warning" :striped="true">
                    <strong>{{ summary.daily.thursday.hours['2'] }}%</strong>
                  </b-progress-bar>
                  <b-progress-bar :value="summary.daily.thursday.hours['3']" variant="success" :striped="true">
                    <strong>{{ summary.daily.thursday.hours['3'] }}%</strong>
                  </b-progress-bar>
                  <b-progress-bar :value="summary.daily.thursday.hours['4']" variant="danger" :striped="true">
                    <strong>{{ summary.daily.thursday.hours['4'] }}%</strong>
                  </b-progress-bar>
                </b-progress>
              </b-col>
            </b-row>
            <b-row>
              <b-col >Friday</b-col>
              <b-col >
                <b-progress :max="100">
                  <b-progress-bar :value="summary.daily.friday.total" variant="info" :striped="true" >
                    <strong>{{ summary.daily.friday.total }}%</strong>
                  </b-progress-bar>
                  <b-progress-bar :value="100-summary.daily.friday.total" variant="secondary" />
                </b-progress>
              </b-col>
              <b-col>
                <b-progress :max="100">
                  <b-progress-bar :value="summary.daily.friday.hours['1']" variant="primary" :striped="true" >
                    <strong>{{ summary.daily.friday.hours['1'] }}%</strong>
                  </b-progress-bar>
                  <b-progress-bar :value="summary.daily.friday.hours['2']" variant="warning" :striped="true" >
                    <strong>{{ summary.daily.friday.hours['2'] }}%</strong>
                  </b-progress-bar>
                  <b-progress-bar :value="summary.daily.friday.hours['3']" variant="success" :striped="true" >
                    <strong>{{ summary.daily.friday.hours['3'] }}%</strong>
                  </b-progress-bar>
                  <b-progress-bar :value="summary.daily.friday.hours['4']" variant="danger" :striped="true" >
                    <strong>{{ summary.daily.friday.hours['4'] }}%</strong>
                  </b-progress-bar>
                </b-progress>
              </b-col>
            </b-row>
            <b-row>
              <b-col >Saturday</b-col>
              <b-col >
                <b-progress :max="100">
                  <b-progress-bar :value="summary.daily.saturday.total" variant="info" :striped="true" >
                    <strong>{{ summary.daily.saturday.total }}%</strong>
                  </b-progress-bar>
                  <b-progress-bar :value="100-summary.daily.saturday.total" variant="secondary" />
                </b-progress>
              </b-col>
              <b-col>
                <b-progress :max="100">
                  <b-progress-bar :value="summary.daily.saturday.hours['1']" variant="primary" :striped="true" >
                    <strong>{{ summary.daily.saturday.hours['1'] }}%</strong>
                  </b-progress-bar>
                  <b-progress-bar :value="summary.daily.saturday.hours['2']" variant="warning" :striped="true" >
                    <strong>{{ summary.daily.saturday.hours['2'] }}%</strong>
                  </b-progress-bar>
                  <b-progress-bar :value="summary.daily.saturday.hours['3']" variant="success" :striped="true" >
                    <strong>{{ summary.daily.saturday.hours['3'] }}%</strong>
                  </b-progress-bar>
                  <b-progress-bar :value="summary.daily.saturday.hours['4']" variant="danger" :striped="true" >
                    <strong>{{ summary.daily.saturday.hours['4'] }}%</strong>
                  </b-progress-bar>
                </b-progress>
              </b-col>
            </b-row>
          </div>
        </div>
        <br />
        <p>
          In the war that is Dota two teams continue a set of endless battles.
          But who is winning... Maybe the team that always has first pick?
        </p>
        <div class="card">
          <div class="card-body">
            <h5 class="card-title">Radiant vs Dire</h5>
            <b-row>
              <b-col>
                <b-progress :max="1">
                  <b-progress-bar :value="summary.team.radiant" variant="success">
                    <strong>Radiant {{ Math.round(summary.team.radiant * 100) }}%</strong>
                  </b-progress-bar>
                  <b-progress-bar :value="summary.team.dire" variant="danger">
                    <strong>Dire {{ Math.round(summary.team.dire * 100) }}%</strong>
                  </b-progress-bar>
                </b-progress>
              </b-col>
            </b-row>
          </div>
        </div>
        <p>
          Nope, as the <b class="text-info">Radiant</b> looks to have a <a href="https://www.dotabuff.com/heroes/meta?view=played&metric=faction">~5% Side Advantage</a> since the last set of map changes. 
          This means that first pick is not as important as people think...
        </p>
        <p>
          We reset our baseline when a major patch is released that changes the balance of abilities.
          We normally export our master database approximately once a week.
          Our current stats collection is based started on <b class="text-info">{{ formatDateTime(summary.range.start) }}</b> and was last exported on <b class="text-info">{{ formatDateTime(summary.range.end) }}</b> with a total of <b class="text-info">{{ formatNumber(summary.range.matches) }}</b> AD matches processed. 
          But we did notice that <b class="text-info">{{ summary.range.abandoned }}%</b> of matches where abandoned, you can do better people!
        </p>
      </b-col>
    </b-row>
  </section>
</template>

<script>
// import HelloWorld from '@/components/HelloWorld.vue'
// components: { HelloWorld }

import moment from 'moment'
import numeral from 'numeral'
import summary from '@/assets/data/summary.json'
import heroes from '@/assets/data/hero-summary.json'

export default {
  name: 'home',
  data () {
    return {
      'summary': summary,
      'heroes': heroes,
    }
  },
  methods: {
    formatDateTime(value) {
      return moment(value).format("MMMM Do");
    },
    formatNumber(value) {
      return numeral(value).format('0,0');
    },
    formatPercentage(value) {
      return numeral(value).format('0%');
    }
  }
}
</script>
