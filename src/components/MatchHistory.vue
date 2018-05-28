<template>
  <section v-if="ready" class="opaque-background">
    <b-row>
      <b-col>
        <table class="table table-striped b-table">
          <tbody>
            <template v-for="match in items">
              <tr v-bind:item="match" v-bind:key="match.match_id">
                <td>
                  <b-link @click="explore(match)">{{match.match_id}}</b-link>
                </td>
                <template v-for="player in match.players">
                  <td v-bind:item="player" v-bind:key="player.player_slot">
                    <b-img :src="player.img" :title="player.hero_name" fluid class="hero-icon" />
                  </td>
                </template>
              </tr>
            </template>
          </tbody>
        </table>
      </b-col>
    </b-row>
  </section>
  <section v-else class="opaque-background text-center">
    <h1>Finding Lastest AD matches!</h1>
    <h2>sit tight this could take a minute...</h2>
    <hgv-loader :color="'#ffc107'"></hgv-loader>
  </section>
</template>

<script>
import axios from 'axios'
import { mapGetters } from 'vuex'

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

    if (vm.isAnonymous) {
      vm.$router.push('/login?r=/match/history')
      return
    }

    let p1 = axios.get(process.env.API_BASE_URL + 'MatchHistory?account=' + vm.dotaId).then((reponse) => { return reponse.data })
    let p2 = axios.get('/static/data/heroes.json').then((reponse) => { return reponse.data })

    Promise.all([p1, p2]).then((values) => {
      let items = values[0]
      let heroes = values[1]

      for (let i = 0; i < items.length; i++) {
        let match = items[i]
        for (let x = 0; x < match.players.length; x++) {
          let player = match.players[x]
          let hero = heroes[player.hero_id]
          player.hero_name = hero.name
          player.img = hero.img
        }
      }

      vm.items = items
      vm.ready = true
    }).catch(function (error) {
      console.log(error)
    })
  },
  methods: {
    explore (match) {
      window.open('https://www.opendota.com/matches/' + match.match_id)
    }
  },
  computed: mapGetters(['isAnonymous', 'dotaId'])
}
</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped>
</style>
