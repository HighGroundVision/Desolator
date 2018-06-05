<template>
  <section v-if="ready" class="opaque-background">
    <b-row>
      <b-col>
        <h1 class="text-warning">Match Details</h1>
        <p>Lorem ipsum dolor sit amet, consectetur adipiscing elit. Maecenas eget porta velit. Etiam aliquam auctor nulla, vitae congue ligula rhoncus vel. Fusce porta imperdiet risus, ac maximus magna posuere in.</p>
        <p>Suspendisse quis sodales velit. Sed fringilla enim quis nibh congue efficitur. Proin ante lectus, rhoncus quis venenatis in, maximus malesuada ipsum. Cras hendrerit facilisis ante at molestie. Nullam ullamcorper diam vitae dolor placerat, nec euismod neque placerat.</p>
        <p>Phasellus non dui id ligula rutrum porta id ac turpis. Nulla tristique nisi at nulla tempor, non gravida nibh malesuada. Vestibulum ac scelerisque velit. Curabitur bibendum eros sit amet odio rutrum, et semper nulla pretium. Nunc rhoncus leo a sapien molestie, non auctor eros iaculis. Nullam hendrerit nisi vitae arcu volutpat, et dictum arcu cursus.</p>
      </b-col>
      <b-col cols="6" md="4">
        <div class="text-center">
          <b-img src="/static/images/cluckles.png" fluid  />
          <b-alert variant="info" show>   
            <strong>Cluckles says!</strong>
            <br />
            <span>If you want to see the full details our friends over at OpenDota can help.</span>
            <b-button variant="secondary" @click="gotToOpenDota">View Full Details</b-button>
          </b-alert>
        </div>
      </b-col>
    </b-row>
    <hr class="highlighted" />
    <b-row>
      <b-col>
        <span>Victory</span> 
        <span v-if="data.radiant_win" class="text-success">RADIANT</span>
        <span v-else class="text-danger">DIRE</span>
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
        <span>PATCH</span>
        <br />
        <span>{{data.patch}}</span>
      </b-col>
      <b-col>
        <span>DATE</span>
        <br />
        <span>{{data.start_time}}</span>
      </b-col>
      <b-col>
        <span>DURATION</span>
        <br />
        <span>{{data.duration}}</span>
      </b-col>
      <b-col>
        <span>REGION</span>
        <br />
        <span>{{data.region}}</span>
      </b-col>
      <b-col>
        <span>MATCH ID</span>
        <br />
        <span>{{data.match_id}}</span>
      </b-col>
    </b-row>
    <hr class="highlighted" />
    <b-row>
      <b-col>
        <table class="table table-borderless table-sm b-table text-center">
          <thead>
            <tr>
              <th></th>
              <th>Player</th>
              <th>Hero</th>
              <th class="text-primary">Abilities</th>
              <th><span style="font-size: 0.8em;">Win Rate</span></th>
              <th><span style="font-size: 0.8em;">Cast</span></th>
              <th><span style="font-size: 0.8em;">Hits</span></th>
              <th><span style="font-size: 0.8em;">Dmg</span></th>
              <th class="text-primary">Combos</th>
              <th><span style="font-size: 0.8em;">Win Rate</span></th>
            </tr>
          </thead>
          <tbody>
            <template v-for="player in data.players">
              <tr v-bind:item="player" v-bind:key="player.player_slot"  v-bind:class="{ 'table-row-self': player.self }" >
                <td rowspan="5" v-if="player.player_slot === 0" class="bg-success align-middle">
                  <span>R</span><br />
                  <span>A</span><br />
                  <span>D</span><br />
                  <span>I</span><br />
                  <span>A</span><br />
                  <span>N</span><br />
                  <span>T</span><br />
                  <span v-if="data.radiant_win" title="Winner!"><i class="fas fa-trophy"></i></span>
                </td>
                <td rowspan="5" v-if="player.player_slot === 128"  class="bg-danger align-middle">
                  <span>D</span><br />
                  <span>I</span><br />
                  <span>R</span><br />
                  <span>E</span><br />
                  <span v-if="!data.radiant_win"  title="Winner!"><i class="fas fa-trophy"></i></span>
                </td>
                <td>
                  <span v-if="player.self" class="text-warning"><i class="fas fa-star"></i></span>
                  <span>{{player.personaname}}</span>
                </td>
                <td>
                  <b-img :src="player.hero_img" :title="player.hero_name" class="hero-icon-profile" />
                </td>
                <td class="text-left">
                  <ul class="list-unstyled">
                    <template v-for="ability in player.abilities" >
                      <li v-bind:key="ability.id">
                        <b-link :to="'/stats/ability/' + ability.id">
                          <b-img :src="ability.img" :title="ability.name" class="ability-icon-sm" />
                          {{ability.name}}
                        </b-link>
                      </li>
                    </template>
                  </ul>
                </td>
                <td>
                  <ul class="ability-inline list-unstyled">
                    <template v-for="ability in player.abilities" >
                      <li v-bind:key="ability.id">
                        <span v-if="ability.winrate" v-bind:class="{ 'text-success': ability.winrate > 50, 'text-danger': ability.winrate < 50, 'text-info': ability.winrate === 50 }">{{ability.winrate}} %</span>
                        <span v-else title="N/A">--</span>
                      </li>
                    </template>
                  </ul>
                </td>
                <td>
                  <ul class="ability-inline list-unstyled">
                    <template v-for="ability in player.abilities" >
                      <li v-bind:key="ability.id">
                        <span v-if="ability.casts">{{ability.casts}}</span>
                        <span v-else title="N/A">--</span>
                      </li>
                    </template>
                  </ul>
                </td>
                <td>
                  <ul class="ability-inline list-unstyled">
                    <template v-for="ability in player.abilities" >
                      <li v-bind:key="ability.id">
                        <span v-if="ability.hit">{{ability.hit}}</span>
                        <span v-else title="N/A">--</span>
                      </li>
                    </template>
                  </ul>
                </td>
                <td>
                  <ul class="ability-inline list-unstyled">
                    <template v-for="ability in player.abilities" >
                      <li v-bind:key="ability.id">
                        <span v-if="ability.damage">{{ability.damage}}</span>
                        <span v-else title="N/A">--</span>
                      </li>
                    </template>
                  </ul>
                </td>
                <td>
                  <ul class="ability-inline list-unstyled">
                    <template v-for="combo in player.combos" >
                      <li v-bind:key="combo.key">
                        <b-link :to="'/stats/abilities/' + combo.key">
                          <b-img :src="combo.ability1.img" :title="combo.ability1.name" class="ability-icon-sm" />
                          <b-img :src="combo.ability2.img" :title="combo.ability2.name" class="ability-icon-sm" />
                        </b-link>
                      </li>
                    </template>
                  </ul>
                </td>
                <td>
                  <ul class="ability-inline list-unstyled">
                    <template v-for="combo in player.combos" >
                      <li v-bind:key="combo.key" >
                        <span v-if="combo.winrate" v-bind:class="{ 'text-success': combo.winrate > 50, 'text-danger': combo.winrate < 50, 'text-info': combo.winrate === 50 }">{{combo.winrate}} %</span>
                        <span v-else title="N/A">--</span>
                      </li>
                    </template>
                  </ul>
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

function pairwise (list) {
  var pairs = []
  list.slice(0, list.length - 1).forEach(function (first, n) {
    var tail = list.slice(n + 1, list.length)
    tail.forEach(function (item) {
      pairs.push([first, item])
    })
  })
  return pairs
}

export default {
  name: 'MatchHistory',
  data () {
    return {
      'ready': false,
      'data': {}
    }
  },
  created () {
    const vm = this

    const matchId = vm.$route.params.key
    let p0 = axios.get(process.env.API_BASE_URL + 'MatchDetails?match=' + matchId).then((reponse) => { return reponse.data })
    let p1 = axios.get('/static/data/patches.json').then((reponse) => { return reponse.data })
    let p2 = axios.get('/static/data/regions.json').then((reponse) => { return reponse.data })
    let p3 = axios.get('/static/data/abilities.json').then((reponse) => { return reponse.data })
    let p4 = axios.get('/static/data/heroes.json').then((reponse) => { return reponse.data })
    let p5 = axios.get('/static/data/stats-abilities.json').then((reponse) => { return reponse.data })
    let p6 = axios.get('/static/data/stats-combos.json').then((reponse) => { return reponse.data })

    Promise.all([p0, p1, p2, p3, p4, p5, p6]).then((values) => {
      const match = values[0]
      const patchesDB = values[1]
      const regionsDB = values[2]
      const abilitiesDB = values[3]
      const heroesDB = values[4]
      const abilityStatsDB = values[5]
      const comboStatsDB = values[6]

      // Overview
      match.start_time = moment.unix(match.start_time).fromNow()
      match.duration = moment.duration(match.duration, 'seconds').humanize()

      let region = regionsDB[match.region]
      match.region = region === undefined ? 'Unknown (' + match.region + ')' : region

      let patch = patchesDB[match.patch]
      match.patch = patch === undefined ? 'Unknown (' + match.patch + ')' : patch.name

      const parsed = match.version !== null

      let myPool = []
      let draftPool = []

      // Player Details
      for (let i = 0; i < match.players.length; i++) {
        let player = match.players[i]

        // Account is Self?
        if (player.account_id === vm.dotaId) {
          player.self = true
        }

        // Player Name or 'Anonymous'
        if (player.personaname === undefined) {
          player.personaname = 'Anonymous'
        }

        // Hero
        const hero = heroesDB[player.hero_id]
        if (hero) {
          player.hero_img = hero.img.replace('/banner/', '/profile/npc_dota_hero_')
          player.hero_name = hero.name
        }
      
        // Abilties & Stats
        let abilities = []

        let skills = Array.from(new Set(player.ability_upgrades_arr))
        for (let x = 0; x < skills.length; x++) {
          const id = skills[x]
          const ability = abilitiesDB[id]
          if (ability === undefined) {
            continue
          }

          draftPool.push(id)

          if (player.self === true) {
            myPool.push(id)
          }

          let usageTotal = 0
          let hitTotal = 0
          let damgeTotal = 0

          if (parsed) {
            let usageKey = Object.keys(player.ability_uses).filter((lhs) => lhs === ability.key)
            if (usageKey) {
              if (player.ability_uses[usageKey]) {
                usageTotal = player.ability_uses[usageKey]
              }
            }

            let hitKey = Object.keys(player.ability_targets).filter((lhs) => lhs === ability.key)
            if (hitKey) {
              let hitTargets = player.ability_targets[hitKey]
              for (const key in hitTargets) {
                if (hitTargets.hasOwnProperty(key)) {
                  const value = hitTargets[key]
                  hitTotal += value
                }
              }
            }
    
            let damageKey = Object.keys(player.damage_targets).filter((lhs) => lhs === ability.key)
            if (damageKey) {
              let damageTargets = player.damage_targets[damageKey]
              for (const key in damageTargets) {
                if (damageTargets.hasOwnProperty(key)) {
                  const value = damageTargets[key]
                  damgeTotal += value
                }
              }
            }
          }

          let stats = abilityStatsDB.filter((lhs) => { return lhs.abilities === String(id) && (lhs.type === hero.primary || lhs.type === hero.attack) })
          let sum = stats.reduce(function (a, b) { return a.win_rate + b.win_rate })
          let winRate = stats.length === 0 ? '' : Math.round(sum / stats.length)

          let data = {
            'id': id,
            'name': ability.dname,
            'img': ability.img,
            'casts': usageTotal === 0 ? '' : usageTotal,
            'hit': hitTotal === 0 ? '' : hitTotal,
            'damage': damgeTotal === 0 ? '' : damgeTotal > 1000 ? (damgeTotal / 1000).toFixed(1) + 'k' : damgeTotal, 
            'winrate': winRate
          }
          abilities.push(data)
        }

        abilities.sort((lhs, rhs) => { return lhs.winrate === rhs.winrate ? 0 : lhs.winrate > rhs.winrate ? -1 : 1 })
        player.abilities = abilities

        // Combos
        const pairs = pairwise(abilities)
        let combos = []
        for (let z = 0; z < pairs.length; z++) {
          const pair = pairs[z]
          pair.sort((lhs, rhs) => { return lhs.id === rhs.id ? 0 : lhs.id > rhs.id ? 1 : -1 })

          const key = pair.map((lhs) => lhs.id).join('-')
          let stats = comboStatsDB.filter((lhs) => { return lhs.abilities === key && (lhs.type === hero.primary || lhs.type === hero.attack) })

          let sum = stats.map((lhs) => lhs.win_rate).reduce(function (a, b) { return a + b }, 0)
          let data = {
            'key': key,
            'ability1': pair[0],
            'ability2': pair[1],
            'winrate': stats.length === 0 ? '' : Math.round(sum / stats.length)
          }
          combos.push(data)
        }

        combos.sort((lhs, rhs) => { return lhs.winrate === rhs.winrate ? 0 : lhs.winrate > rhs.winrate ? -1 : 1 })
        player.combos = combos
      }
      vm.data = match

      vm.ready = true
    }).catch(function () {
      vm.$router.push('/error')
    })
  },
  methods: {
    gotToOpenDota () {
      window.open('https://www.opendota.com/matches/' + this.data.match_id)
    }
  },
  computed: mapGetters(['isAnonymous', 'dotaId'])
}
</script>

<!-- Add 'scoped' attribute to limit CSS to this component only -->
<style scoped>
.ability-inline {
  line-height: 32px;
}
a {
    color: white;
    text-decoration: none;
}
</style>
