<template>
  <b-row>
    <b-col cols="4" lg="3">
      <div class="text-center">
        <b-alert variant="info" show>   
          <p>
            Select a ability to unlock additional knowledge!
          </p>
        </b-alert>
        <img src="@/assets/imgs/cluckles-speach.png" class="cluckles-speach" />
      </div>
    </b-col>
    <b-col>
      <h4  class="text-center">Abilities</h4>
      <hr class="highlighted" />
      <p>...</p>
      <b-row>
        <b-col>
          <h5 class="text-center">Skills</h5>
          <b-row>
            <template v-for="(value) in abilities">
              <b-col cols="6" :key="value.key">
                <router-link :to="`/hero/${value.id}`">
                  <div class="card m-1">
                    <div class="card-body">
                      <span class="card-title">{{value.name}}</span>
                      <div>
                        <b-img :src="value.image" class="p-1" fluid-grow  />
                      </div>
                      <div>
                        <b-progress :max="1">
                          <b-progress-bar :value="value.win_rate" variant="info" :striped="true" >
                            <strong>{{ formatPercentage(value.win_rate) }}</strong>
                          </b-progress-bar>
                          <b-progress-bar :value="1-value.win_rate" variant="secondary" />
                        </b-progress>
                      </div>
                      <div>
                        <b-progress :max="1">
                          <b-progress-bar :value="value.picks_percentage" variant="warning" :striped="true" >
                            <strong>{{ formatPercentage(value.picks_percentage) }}</strong>
                          </b-progress-bar>
                          <b-progress-bar :value="1-value.picks_percentage" variant="secondary" />
                        </b-progress>
                      </div>
                      <div>
                        <b-progress :max="1">
                          <b-progress-bar :value="value.wins_percentage" variant="success" :striped="true" >
                            <strong>{{ formatPercentage(value.wins_percentage) }}</strong>
                          </b-progress-bar>
                          <b-progress-bar :value="1-value.wins_percentage" variant="secondary" />
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
                <router-link :to="`/hero/${value.id}`">
                  <div class="card m-1">
                    <div class="card-body">
                      <span class="card-title">{{value.name}}</span>
                      <div>
                        <b-img :src="value.image" class="p-1" fluid-grow  />
                      </div>
                      <div>
                        <b-progress :max="1">
                          <b-progress-bar :value="value.win_rate" variant="info" :striped="true" >
                            <strong>{{ formatPercentage(value.win_rate) }}</strong>
                          </b-progress-bar>
                          <b-progress-bar :value="1-value.win_rate" variant="secondary" />
                        </b-progress>
                      </div>
                      <div>
                        <b-progress :max="1">
                          <b-progress-bar :value="value.picks_percentage" variant="warning" :striped="true" >
                            <strong>{{ formatPercentage(value.picks_percentage) }}</strong>
                          </b-progress-bar>
                          <b-progress-bar :value="1-value.picks_percentage" variant="secondary" />
                        </b-progress>
                      </div>
                      <div>
                        <b-progress :max="1">
                          <b-progress-bar :value="value.wins_percentage" variant="success" :striped="true" >
                            <strong>{{ formatPercentage(value.wins_percentage) }}</strong>
                          </b-progress-bar>
                          <b-progress-bar :value="1-value.wins_percentage" variant="secondary" />
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
    abilities.sort((lhs, rhs) => rhs.win_rate - lhs.win_rate);
    ultimates.sort((lhs, rhs) => rhs.win_rate - lhs.win_rate);
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