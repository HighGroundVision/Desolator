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
        <b-alert v-if="construction" variant="warning" show>
          <b-row>
            <b-col>
              <strong><i class="fas fa-exclamation-triangle"></i> Under Construction</strong>
              <p>
                We are currently rebuilding the systems that run this web application. 
                Our continuous deployment process means that this site will be updated as we push out new features. 
                You are welcome to use the site and the new features. 
                Some features maybe unstable/incomplete and maybe removed in the future.
                This banner will be removed when the rework is finished.
              </p>
            </b-col>
          </b-row>
        </b-alert>
        <h4 class="text-center">Welcome!</h4>
        <!--<hr class="highlighted" />-->
        <p>
          We at HGV are dedicated to the Ability Draft community.
          Providing the tools to understand AD better, and hopefully have some fun along the way. 
          Remember everyone, that while Carrys may kill heroes but Supports win games!
        </p>
        <hr class="highlighted" />
        
      </b-col>
    </b-row>
  </section>
</template>

<script>
import moment from 'moment'
import numeral from 'numeral'
// import summary from '@/assets/data/summary.json'
// import heroes from '@/assets/data/hero-summary.json'
// import leaderboard from '@/assets/data/leaderboard-summary.json'

export default {
  name: 'home',
  data () {
    return {
      "construction": false,
      'summary': summary,
      'heroes': heroes,
      'leaderboard': leaderboard,
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
    formatNumber(value) {
      return numeral(value).format('0,0');
    },
    formatPercentage(value) {
      return numeral(value).format('0%');
    }
  }
}
</script>
