<template>
  <section v-if="ready" class="opaque-background">
    <b-row>
      <b-col>
        <h1 class="text-warning">Recent Matches</h1>
      </b-col>
    </b-row>
    <b-row>
      <b-col>
        <p>
          Because of a number of limitations with the STEAM API, It may take some time for your matches to populate. 
          After a filtered list of just your AD matches will be available.
          A request to update this list is made every time you login and is cached for 1 hour. 
          It while take approximately 5-10 minutues to poplulate cache with your lastest matches.
        </p>
        <p>
          Also you need to select the option in Dota "Expose Public Match Data" which exposes match data from your public games to the STEAM API.
        </p>
      </b-col>
    </b-row>
    <hr class="highlighted" />
    <b-row v-if="items.length > 0">
      <b-col>
        <table class="table table-borderless table-sm b-table">
          <thead>
            <tr>
              <th>Status</th>
              <th>Hero</th>
              <th class="text-center">Abilities</th>
              <th>Date / Duration</th>
              <th>Level</th>
              <th>K / D / A</th>
              <th></th>
            </tr>
          </thead>
          <tbody>
            <template v-for="item in items">
              <tr v-bind:item="item" v-bind:key="item.match_id">
                <td class="align-middle">
                  <span v-if="item.won" class="text-success">Victory</span>
                  <span v-else class="text-danger">Defeat</span>
                </td>
                <td class="align-middle">
                  <b-img :src="item.img" :title="item.name" class="hero-icon-lg" />
                </td>
                <td class="align-middle" width="320px">
                  <ul class="list-inline">
                    <template v-for="ability in item.abilities" >
                      <li class="list-inline-item" v-bind:key="ability.id">
                        <b-link @click="gotoAbility(ability)">
                          <b-img :src="ability.img" :title="ability.name" class="ability-icon-lg" />
                        </b-link>
                      </li>
                    </template>
                  </ul>
                </td>
                <td class="align-middle">
                  {{convertTimestamp(item.start_time)}}
                  <br />
                  {{convertDuration(item.duration)}}
                </td>
                <td class="align-middle">
                  <span class="text-warning">{{item.level}}</span>
                </td>
                <td class="align-middle" width="120px">
                  <span class="text-success">{{item.kills}}</span> / <span class="text-danger">{{item.deaths}}</span> / <span class="text-info">{{item.assists}}</span>
                </td>
                <td class="align-middle">
                  <b-button :variant="'primary'" @click="gotoDetails(item)">Details</b-button>
                </td>
              </tr>
            </template>
          </tbody>
        </table>
      </b-col>
    </b-row>
    <b-row v-else>
      <b-col>
        <p>
          You currently do not have AD matches listed in your history. 
          Either we are still processing your history and they will be available shortly or you haven't played any AD games in the last 100 matches. 
          In which case go play some more AD games and check back with us later...
        </p>
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
import { mapGetters } from 'vuex'
import * as moment from 'moment'

export default {
  name: 'MatchHistory',
  data () {
    return {
      'ready': false,
      'items': []
    }
  },
  created () {
    const vm = this

    let p1 = axios.get(process.env.API_BASE_URL + 'MatchHistory?mode=18&account=' + vm.dotaId).then((reponse) => { return reponse.data })
    let p2 = axios.get('/static/data/heroes.json').then((reponse) => { return reponse.data })
    let p3 = axios.get('/static/data/abilities.json').then((reponse) => { return reponse.data })

    Promise.all([p1, p2, p3]).then((values) => {
      let items = values[0]
      let heroes = values[1]
      let abilities = values[2]

      for (let i = 0; i < items.length; i++) {
        let item = items[i]
        let hero = heroes[item.hero_id]
        item.img = hero.img
        item.name = hero.name

        for (let x = 0; x < item.abilities.length; x++) {
          const id = item.abilities[x]
          let ability = abilities[id]
          let data = {
            'id': id,
            'img': ability.img,
            'name': ability.dname
          }
          item.abilities[x] = data
        }
      }

      vm.items = items
      vm.ready = true
    }).catch(function () {
      vm.$router.push('/error')
    })
  },
  methods: {
    gotoDetails (item) {
      this.$router.push('/match/details/' + item.match_id)
    },
    gotoAbility (item) {
      this.$router.push('/stats/ability/' + item.id)
    },
    convertTimestamp (timestamp) {
      var ts = moment.unix(timestamp)
      return ts.fromNow()
    },
    convertDuration (timestamp) {
      var ts = moment.duration(timestamp, 'seconds')
      return ts.humanize()
    }
  },
  computed: mapGetters(['isAuthenticated', 'isAnonymous', 'dotaId'])
}
</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped>
</style>
