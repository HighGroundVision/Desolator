<template>
  <b-row>
    <b-col cols="4" lg="3">
      <div class="text-center">
        <b-alert variant="info" show>   
          <p>
            Select a ability to learn its secrets and gain insight!
          </p>
        </b-alert>
        <img src="@/assets/imgs/cluckles-speach.png" class="cluckles-speach" />
      </div>
    </b-col>
    <b-col>
      <h4  class="text-center">Abilities</h4>
      <hr class="highlighted" />
      <p>
        While It made called Ability Draft, abilities are not the be all and end all of the mode.
        Make sure to check the heroes pages as good a ability in general maybe a bad choice for your hero.
        We have ordered the lists below by the abilities / ultimates <b class="text-primary">win rate</b>.
      </p>
      <p>
        We have also include the <b class="text-info">pick rate</b> as ratio to other abilities. 
        As knowing the pick rate you can predict which abilities will be snap up first. 
        If you are looking at few abilities that have similar win rates the pick rate can help you understand if you can wait till next round or if you should pickup that ability now.
      </p>
      <p>
        Also abilities that have high win rates and low pick rates are outliers your opponents will never see coming!
      </p>
      <b-row>
        <b-col>
          <h5 class="text-center">Skills</h5>
          <b-row>
            <template v-for="(value) in abilities">
              <b-col cols="6" :key="value.key">
                <router-link :to="`/ability/${value.id}`">
                  <div class="card m-1">
                    <div class="card-body">
                      <span class="card-title">{{value.name}}</span>
                      <div class="p-1">
                        <b-img :src="value.image" fluid-grow  />
                      </div>
                      <div class="p-1">
                        <b-progress :max="1">
                          <b-progress-bar :value="value.win_rate" variant="primary" :striped="true" >
                            <strong>{{ formatPercentage(value.win_rate) }}</strong>
                          </b-progress-bar>
                          <b-progress-bar :value="1-value.win_rate" variant="secondary" />
                        </b-progress>
                      </div>
                      <div class="p-1">
                        <b-progress :max="1">
                          <b-progress-bar :value="value.picks_percentage" variant="info" :striped="true" />
                          <b-progress-bar :value="1-value.picks_percentage" variant="secondary" />
                        </b-progress>
                      </div>
                    </div>
                  </div>
                </router-link>
              </b-col>
            </template>
          </b-row>
        </b-col>
        <b-col>
          <h5 class="text-center">Ultimates</h5>
          <b-row>
            <template v-for="(value) in ultimates">
              <b-col cols="6" :key="value.key">
                <router-link :to="`/ability/${value.id}`">
                  <div class="card m-1">
                    <div class="card-body">
                      <span class="card-title">{{value.name}}</span>
                      <div class="p-1">
                        <b-img :src="value.image" fluid-grow  />
                      </div>
                      <div class="p-1">
                        <b-progress :max="1">
                          <b-progress-bar :value="value.win_rate" variant="primary" :striped="true" >
                            <strong>{{ formatPercentage(value.win_rate) }}</strong>
                          </b-progress-bar>
                          <b-progress-bar :value="1-value.win_rate" variant="secondary" />
                        </b-progress>
                      </div>
                      <div class="p-1">
                        <b-progress :max="1">
                          <b-progress-bar :value="value.picks_percentage" variant="info" :striped="true" />
                          <b-progress-bar :value="1-value.picks_percentage" variant="secondary" />
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
      
    </b-col>
  </b-row>
</template>

<script>
import numeral from 'numeral'
import abilities from '@/assets/data/abilities-collection.json'
import ultimates from '@/assets/data/ultimates-collection.json'
// $route.params.id

export default {
  name: 'abilities',
  data () {
    // abilities.sort((lhs, rhs) => rhs.win_rate - lhs.win_rate);
    // ultimates.sort((lhs, rhs) => rhs.win_rate - lhs.win_rate);
    return {
      'abilities': abilities,
      'ultimates': ultimates
    }
  },
  methods: {
    formatPercentage(value) {
      return numeral(value).format('0%');
    }
  }
}
</script>