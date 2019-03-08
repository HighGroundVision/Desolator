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
        <b-alert variant="warning" show>
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
        <p>
          So what is <span title="AD">Ability Draft</span>? Players are given a random hero from Ability-Draft enabled heroes. 
          Each players' abilities are put into a pool. Players take turns selecting abilities from the pool, given five seconds for each choice.
          Each player selects three normal abilities and one ultimate.
        </p>
        <p>
          We are often asked when do people play AD? To that end we have included the daily counts as a percentage of the total matches.
        </p>
        <div class="card">
          <div class="card-body">
            <h5 class="card-title">Play Time Break Down</h5>
            <b-row>
              <b-col></b-col>
              <b-col></b-col>
              <b-col>
                <b-row>
                  <b-col cols="3">Morning</b-col>
                  <b-col cols="3">Afternoon</b-col>
                  <b-col cols="3">Evening</b-col>
                  <b-col cols="3">Night</b-col>
                </b-row>
              </b-col>
            </b-row>
            <b-row>
              <b-col>Sunday</b-col>
              <b-col>
                 <b-progress :max="1">
                  <b-progress-bar :value="summary.daily.sunday">
                    <strong>{{ Math.round(summary.daily.sunday * 100) }} %</strong>
                  </b-progress-bar>
                </b-progress>
              </b-col>
              <b-col>
                <b-progress :max="1">
                  <b-progress-bar :value="0.25" variant="warning" />
                  <b-progress-bar :value="0.25" variant="primary" />
                  <b-progress-bar :value="0.25" variant="info" />
                  <b-progress-bar :value="0.25" variant="secondary" />
                </b-progress>
              </b-col>
            </b-row>
            <b-row>
              <b-col >Monday</b-col>
              <b-col >
                 <b-progress :max="1">
                  <b-progress-bar :value="summary.daily.monday">
                    <strong>{{ Math.round(summary.daily.monday * 100) }} %</strong>
                  </b-progress-bar>
                </b-progress>
              </b-col>
              <b-col>
                <b-progress :max="1">
                  <b-progress-bar :value="0.25" variant="warning" />
                  <b-progress-bar :value="0.25" variant="primary" />
                  <b-progress-bar :value="0.25" variant="info" />
                  <b-progress-bar :value="0.25" variant="secondary" />
                </b-progress>
              </b-col>
            </b-row>
            <b-row>
              <b-col >Tuesday</b-col>
              <b-col >
                 <b-progress :max="1">
                  <b-progress-bar :value="summary.daily.tuesday">
                    <strong>{{ Math.round(summary.daily.tuesday * 100) }} %</strong>
                  </b-progress-bar>
                </b-progress>
              </b-col>
              <b-col>
                <b-progress :max="1">
                  <b-progress-bar :value="0.25" variant="warning" />
                  <b-progress-bar :value="0.25" variant="primary" />
                  <b-progress-bar :value="0.25" variant="info" />
                  <b-progress-bar :value="0.25" variant="secondary" />
                </b-progress>
              </b-col>
            </b-row>
            <b-row>
              <b-col >Wednesday</b-col>
              <b-col >
                <b-progress :max="1">
                  <b-progress-bar :value="summary.daily.wednesday">
                    <strong>{{ Math.round(summary.daily.wednesday * 100) }} %</strong>
                  </b-progress-bar>
                </b-progress>
              </b-col>
              <b-col>
                <b-progress :max="1">
                  <b-progress-bar :value="0.25" variant="warning" />
                  <b-progress-bar :value="0.25" variant="primary" />
                  <b-progress-bar :value="0.25" variant="info" />
                  <b-progress-bar :value="0.25" variant="secondary" />
                </b-progress>
              </b-col>
            </b-row>
            <b-row>
              <b-col >Thursday</b-col>
              <b-col >
                 <b-progress :max="1">
                  <b-progress-bar :value="summary.daily.thursday">
                    <strong>{{ Math.round(summary.daily.thursday * 100) }} %</strong>
                  </b-progress-bar>
                </b-progress>
              </b-col>
              <b-col>
                <b-progress :max="1">
                  <b-progress-bar :value="0.25" variant="warning" />
                  <b-progress-bar :value="0.25" variant="primary" />
                  <b-progress-bar :value="0.25" variant="info" />
                  <b-progress-bar :value="0.25" variant="secondary" />
                </b-progress>
              </b-col>
            </b-row>
            <b-row>
              <b-col >Friday</b-col>
              <b-col >
                <b-progress :max="1">
                  <b-progress-bar :value="summary.daily.friday">
                    <strong>{{ Math.round(summary.daily.friday * 100) }} %</strong>
                  </b-progress-bar>
                </b-progress>
              </b-col>
              <b-col>
                <b-progress :max="1">
                  <b-progress-bar :value="0.25" variant="warning" />
                  <b-progress-bar :value="0.25" variant="primary" />
                  <b-progress-bar :value="0.25" variant="info" />
                  <b-progress-bar :value="0.25" variant="secondary" />
                </b-progress>
              </b-col>
            </b-row>
            <b-row>
              <b-col >Saturday</b-col>
              <b-col >
                <b-progress :max="1">
                  <b-progress-bar :value="summary.daily.saturday">
                    <strong>{{ Math.round(summary.daily.saturday * 100) }} %</strong>
                  </b-progress-bar>
                </b-progress>
              </b-col>
              <b-col>
                <b-progress :max="1">
                  <b-progress-bar :value="0.25" variant="warning" />
                  <b-progress-bar :value="0.25" variant="primary" />
                  <b-progress-bar :value="0.25" variant="info" />
                  <b-progress-bar :value="0.25" variant="secondary" />
                </b-progress>
              </b-col>
            </b-row>
          </div>
        </div>
        <br />
        <p>
          In the war that is Dota two teams continue to an set of endless battles.
          But who is winning... Maybe the team that always has first pick?
        </p>
        <div class="card">
          <div class="card-body">
            <h5 class="card-title">Radiant vs Dire</h5>
            <b-row>
              <b-col>
                <b-progress :max="1">
                  <b-progress-bar :value="summary.team.radiant" variant="success">
                    <strong>Radiant ({{ Math.round(summary.team.radiant * 100) }} %)</strong>
                  </b-progress-bar>
                  <b-progress-bar :value="summary.team.dire" variant="danger">
                    <strong>Dire ({{ Math.round(summary.team.dire * 100) }} %)</strong>
                  </b-progress-bar>
                </b-progress>
              </b-col>
            </b-row>
          </div>
        </div>
        <br />
        <p>
          Our current stats collection is based started on <b class="text-info">{{ formatDateTime(summary.range.start) }}</b> and was last exported on <b class="text-info">{{ formatDateTime(summary.range.end) }}</b> with a total of <b class="text-info">{{ formatNumber(summary.range.matches) }}</b> AD matches processed. 
          We reset our baseline when a major patch is released that changes the balance of abilities.
          We normally export our master database approximately once a week.
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

export default {
  name: 'home',
  data () {
    return {
      'summary': summary
    }
  },
  methods: {
    formatDateTime(value) {
      return moment(value).format("MMMM Do");
    },
    formatNumber(value) {
      return numeral(value).format('0,0');
    }
  }
}
</script>
