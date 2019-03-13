<template>
  <b-row>
    <b-col cols="4" lg="3">
      <div class="text-center">
        <b-alert variant="info" show>   
          <p>
            Select a hero to unlock additional knowledge!
          </p>
        </b-alert>
        <img src="@/assets/imgs/cluckles-speach.png" class="cluckles-speach" />
      </div>
    </b-col>
    <b-col>
      <h4  class="text-center">Heroes</h4>
      <hr class="highlighted" />
      <p>
        We track all the enabled heroes. 
        Not all heroes are created equal!
        We have ordered the list below by the heroes win rate.
        The <b class="text-success">strongest</b> heroes will have wins rates around <b class="text-info">60%</b>. These heroes just rock have fun!
        While <b class="text-danger">weakest</b> ones will have win rates around <b class="text-info">40%</b>. These heroes need a lot of help, build you draft carefully.
      </p>
      <p>
        We have detected two classifications of <b class="text-info">picks deviations</b> which we have label <b class="text-success">A</b> and <b class="text-warning">B</b>.
        This confirms that there are two groups of heroes that show up in the pool and this is why we see some heroes more than others, a lot more...
        We have labeled the heroes with the deviation class you see can see which group the hero belongs to.
      </p>
      <b-row>
        <template v-for="(value) in heroes">
          <b-col cols="4" :key="value.key">
            <router-link :to="`/hero/${value.id}`">
              <div class="card m-1">
                <div class="card-body">
                  <b-badge class="float-right">
                    <b v-if="value.picks_deviation == 0" class="text-success h6">A</b>
                    <b v-if="value.picks_deviation < 0" class="text-warning h6">B</b>
                  </b-badge>
                  <span class="card-title">{{value.name}}</span>
                  <div>
                    <b-img :src="value.image" class="p-1" fluid-grow  />
                    <b-progress :max="1">
                      <b-progress-bar :value="value.win_rate" variant="primary" :striped="true" >
                        <strong>{{ formatPercentage(value.win_rate) }}</strong>
                      </b-progress-bar>
                      <b-progress-bar :value="1-value.win_rate" variant="secondary" />
                    </b-progress>
                  </div>
                </div>
              </div>
            </router-link>
          </b-col>
        </template>
      </b-row>
    </b-col>
  </b-row>
</template>

<script>
import numeral from 'numeral'
import heroes from '@/assets/data/hero-collection.json'
// $route.params.id

export default {
  name: 'heroes',
  data () {
    heroes.sort((lhs, rhs) => rhs.win_rate - lhs.win_rate);
    return {
      'heroes': heroes
    }
  },
  methods: {
    formatPercentage(value) {
      return numeral(value).format('0%');
    }
  }
}
</script>