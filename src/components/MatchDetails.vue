<template>
  <section v-if="ready" class="opaque-background">
    <b-row>
      <b-col>
        <h1 class="text-warning">Match Details</h1>
        <p>Lorem ipsum dolor sit amet, consectetur adipiscing elit. Maecenas eget porta velit. Etiam aliquam auctor nulla, vitae congue ligula rhoncus vel. Fusce porta imperdiet risus, ac maximus magna posuere in. Suspendisse quis sodales velit. Sed fringilla enim quis nibh congue efficitur. Proin ante lectus, rhoncus quis venenatis in, maximus malesuada ipsum. Cras hendrerit facilisis ante at molestie. Nullam ullamcorper diam vitae dolor placerat, nec euismod neque placerat.</p>
      </b-col>
      <b-col cols="6" md="4">
        <div class="text-center">
          <b-img src="/static/images/cluckles.png" fluid  />
          <b-alert variant="info" show>   
            <strong>Cluckles says!</strong>
            <br />
            <span>If you want to see the full details our friends over at OpenDota can help.</span>
            <b-button variant="secondary" @click="opendota">View Full Details</b-button>
          </b-alert>
        </div>
      </b-col>
    </b-row>
    <hr class="highlighted" />
    <b-row>
      <b-col>
        <span>MATCH ID</span>
        <br />
        <span>{{data.match_id}}</span>
      </b-col>
      <b-col>
        <span>PATCH</span>
        <br />
        <span>{{convertPatch(data.patch)}}</span>
      </b-col>
      <b-col>
        <span>DATE</span>
        <br />
        <span>{{convertTimestamp(data.start_time)}}</span>
      </b-col>
      <b-col>
        <span>DURATION</span>
        <br />
        <span>{{convertDuration(data.duration)}}</span>
      </b-col>
      <b-col>
        <span>REGION</span>
        <br />
        <span>{{convertRegion(data.region)}}</span>
      </b-col>
      <b-col>
        <span>RADIANT</span> 
        <br />
        <span>{{data.radiant_score}}</span>
      </b-col>
      <b-col>
        <span>DIRE</span> 
        <br />
        <span>{{data.dire_score}}</span>
      </b-col>
      <b-col>
        <span>Victory</span> 
        <span v-if="data.radiant_win" class="text-success">RADIANT</span>
        <span v-else class="text-danger">DIRE</span>
      </b-col>
    </b-row>
    <hr />
    <b-row>
      <b-col>
        <table class="table table-striped b-table">
          <thead>
            <tr>
              <th>Hero</th>
            </tr>
          </thead>
          <tbody>
            <template v-for="player in data.players">
              <tr v-bind:item="player" v-bind:key="player.player_slot">
                <td>
                  <b-img :src="getHeroImg(player.hero_id)" class="hero-icon-sm" />
                </td>
              </tr>
            </template>
          </tbody>
        </table>
      </b-col>
    </b-row>
  </section>
  <section v-else class="opaque-background text-center">
    <h1>Loading...</h1>
    <hgv-loader :color="'#ffc107'"></hgv-loader>
  </section>
</template>

<script>
import axios from 'axios'
import * as moment from 'moment'
import { mapGetters } from 'vuex'

export default {
  name: 'MatchHistory',
  data () {
    return {
      'ready': false,
      'data': {},
      'heroes': [],
      'abilities': [],
      'patches': [],
      'regions': {}
    }
  },
  created () {
    const vm = this

    const matchId = vm.$route.params.key
    let p1 = axios.get(process.env.API_BASE_URL + 'MatchDetails?match=' + matchId).then((reponse) => { return reponse.data })
    let p2 = axios.get('/static/data/patches.json').then((reponse) => { return reponse.data })
    let p3 = axios.get('/static/data/regions.json').then((reponse) => { return reponse.data })
    let p4 = axios.get('/static/data/abilities.json').then((reponse) => { return reponse.data })
    let p5 = axios.get('/static/data/heroes.json').then((reponse) => { return reponse.data })

    Promise.all([p1, p2, p3, p4, p5]).then((values) => {
      const match = values[0]
      const patches = values[1]
      const regions = values[2]
      const abilities = values[3]
      const heroes = values[4]

      // TODO: process JSON added Hero & Ability details

      vm.heroes = heroes
      vm.abilities = abilities
      vm.patches = patches
      vm.regions = regions
      vm.data = match
      vm.ready = true
    }).catch(function (error) {
      console.log(error)
    })
  },
  methods: {
    opendota () {
      window.open('https://www.opendota.com/matches/' + this.data.match_id)
    },
    convertTimestamp (timestamp) {
      var ts = moment.unix(timestamp)
      return ts.fromNow()
    },
    convertDuration (timestamp) {
      var ts = moment.duration(timestamp, 'seconds')
      return ts.humanize()
    },
    convertRegion (r) {
      let region = this.regions[r]
      if (region) {
        return region
      } else {
        return 'Unknown (' + r + ')'
      }
    },
    convertPatch (p) {
      let patch = this.patches[p]
      if (patch) {
        return patch.name
      } else {
        return 'Unknown (' + p + ')'
      }
    },
    getHeroImg (id) {
      let hero = this.heroes[id]
      if (hero) {
        return hero.img
      } else {
        return ''
      }
    }
  },
  computed: mapGetters(['isAnonymous', 'dotaId'])
}
</script>

<!-- Add 'scoped' attribute to limit CSS to this component only -->
<style scoped>
</style>
