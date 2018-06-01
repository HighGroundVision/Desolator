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
            <b-button variant="secondary" @click="gotToOpenDota">View Full Details</b-button>
          </b-alert>
        </div>
      </b-col>
    </b-row>
    <b-row>
      <b-col>
        <h2>Overview</h2>
        <p>Lorem ipsum dolor sit amet, consectetur adipiscing elit. Maecenas eget porta velit. Etiam aliquam auctor nulla, vitae congue ligula rhoncus vel. Fusce porta imperdiet risus, ac maximus magna posuere in. Suspendisse quis sodales velit. Sed fringilla enim quis nibh congue efficitur. Proin ante lectus, rhoncus quis venenatis in, maximus malesuada ipsum. Cras hendrerit facilisis ante at molestie. Nullam ullamcorper diam vitae dolor placerat, nec euismod neque placerat.</p>
      </b-col>
    </b-row>
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
        <h2>Player Summarys</h2>
        <p>Lorem ipsum dolor sit amet, consectetur adipiscing elit. Maecenas eget porta velit. Etiam aliquam auctor nulla, vitae congue ligula rhoncus vel. Fusce porta imperdiet risus, ac maximus magna posuere in. Suspendisse quis sodales velit. Sed fringilla enim quis nibh congue efficitur. Proin ante lectus, rhoncus quis venenatis in, maximus malesuada ipsum. Cras hendrerit facilisis ante at molestie. Nullam ullamcorper diam vitae dolor placerat, nec euismod neque placerat.</p>
      </b-col>
    </b-row>
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
                        {{ability.winrate}}
                      </li>
                    </template>
                  </ul>
                </td>
                <td>
                  <ul class="ability-inline list-unstyled">
                    <template v-for="ability in player.abilities" >
                      <li v-bind:key="ability.id">
                        {{ability.casts}}
                      </li>
                    </template>
                  </ul>
                </td>
                <td>
                  <ul class="ability-inline list-unstyled">
                    <template v-for="ability in player.abilities" >
                      <li v-bind:key="ability.id">
                        {{ability.hit}}
                      </li>
                    </template>
                  </ul>
                </td>
                <td>
                  <ul class="ability-inline list-unstyled">
                    <template v-for="ability in player.abilities" >
                      <li v-bind:key="ability.id">
                        {{ability.damage}}
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
                      <li v-bind:key="combo.key">
                        {{combo.winrate}}
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
    <b-row>
      <b-col>
        <h2>Ability Combos</h2>
        <p>Lorem ipsum dolor sit amet, consectetur adipiscing elit. Maecenas eget porta velit. Etiam aliquam auctor nulla, vitae congue ligula rhoncus vel. Fusce porta imperdiet risus, ac maximus magna posuere in. Suspendisse quis sodales velit. Sed fringilla enim quis nibh congue efficitur. Proin ante lectus, rhoncus quis venenatis in, maximus malesuada ipsum. Cras hendrerit facilisis ante at molestie. Nullam ullamcorper diam vitae dolor placerat, nec euismod neque placerat.</p>
      </b-col>
    </b-row>
    <b-row>
      <template v-for="combo in combos">
        <b-col v-bind:key="combo.type">
          <h3>{{combo.type}}</h3>
          <ul class="ability-inline list-unstyled">
            <template v-for="item in combo.items" >
              <li v-bind:key="item.key">
                <b-link :to="'/stats/abilities/' + item.key">
                  <b-img :src="item.abilities[0].img" :title="item.abilities[0].name" class="ability-icon-sm" />
                  <b-img :src="item.abilities[1].img" :title="item.abilities[1].name" class="ability-icon-sm" />
                </b-link>
                {{item.winrate}}
              </li>
            </template>
          </ul>
        </b-col>
      </template>
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
      'data': {},
      'combos': []
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
    let p5 = axios.get('/static/data/items.json').then((reponse) => { return reponse.data })
    let p6 = axios.get('/static/data/stats-ability.json').then((reponse) => { return reponse.data })
    let p7 = axios.get('/static/data/stats-abilities.json').then((reponse) => { return reponse.data })

    Promise.all([p0, p1, p2, p3, p4, p5, p6, p7]).then((values) => {
      const match = values[0]
      const patchesDB = values[1]
      const regionsDB = values[2]
      const abilitiesDB = values[3]
      const heroesDB = values[4]
      const itemsDB = values[5]
      const abilityStatsDB = values[6]
      const comboStatsDB = values[7]

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
          let winRate = stats.length === 0 ? '' : Math.round(sum / stats.length) + ' %'

          let data = {
            'id': id,
            'name': ability.dname,
            'img': ability.img,
            'casts': usageTotal === 0 ? '--' : usageTotal,
            'hit': hitTotal === 0 ? '--' : hitTotal,
            'damage': damgeTotal === 0 ? '--' : damgeTotal > 1000 ? (damgeTotal / 1000).toFixed(1) + 'k' : damgeTotal, 
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
            'winrate': stats.length === 0 ? '--' : Math.round(sum / stats.length) + ' %'
          }
          combos.push(data)
        }

        combos.sort((lhs, rhs) => { return lhs.winrate === rhs.winrate ? 0 : lhs.winrate > rhs.winrate ? -1 : 1 })
        player.combos = combos

        // Items
        let items = []
        const itemsCollection = [player.item_0, player.item_1, player.item_2, player.item_3, player.item_4, player.item_5]
        for (let x = 0; x < itemsCollection.length; x++) {
          const id = itemsCollection[x]
          const item = itemsDB[id]
          if (item) {
            items.push(item)
          }
        }

        let backpack = []
        const backpackCollection = [player.backpack_0, player.backpack_1, player.backpack_2]
        for (let x = 0; x < backpackCollection.length; x++) {
          const id = backpackCollection[x]
          const item = itemsDB[id]
          if (item) {
            backpack.push(item)
          }
        }

        player.items = items
        player.backpack = backpack
      }

      // Ability Combos
      let combosMelee = []
      let combosRange = []
      let combosStr = []
      let combosAgi = []
      let combosInt = []

      const draft = pairwise(draftPool).map((x) => x.join('-'))
      for (let i = 0; i < draft.length; i++) {
        const key = draft[i]
        
        // TODO: Filter this list by the self.attack_type && self.primary_type
        let stats = comboStatsDB.filter(y => y.abilities === key)
        if (stats.length === 0) {
          continue
        }

        const parts = key.split('-')
        let abilities = parts.map(id => {
          let ability = abilitiesDB[id]
          let data = {
            'id': id,
            'name': ability.dname,
            'img': ability.img
          }
          return data
        })

        if (abilities.length !== 2) {
          continue
        }

        for (let z = 0; z < stats.length; z++) {
          const stat = stats[z]
          if (stat.type === 1) {
            combosMelee.push({'key': key, 'abilities': abilities, 'winrate': stat.win_rate + ' %'})
          } else if (stat.type === 2) {
            combosRange.push({'key': key, 'abilities': abilities, 'winrate': stat.win_rate + ' %'})
          } else if (stat.type === 3) {
            combosStr.push({'key': key, 'abilities': abilities, 'winrate': stat.win_rate + ' %'})
          } else if (stat.type === 4) {
            combosAgi.push({'key': key, 'abilities': abilities, 'winrate': stat.win_rate + ' %'})
          } else if (stat.type === 5) {
            combosInt.push({'key': key, 'abilities': abilities, 'winrate': stat.win_rate + ' %'})
          }
        }
      }

      combosMelee.sort((lhs, rhs) => { return lhs.winrate === rhs.winrate ? 0 : lhs.winrate > rhs.winrate ? -1 : 1 })
      combosRange.sort((lhs, rhs) => { return lhs.winrate === rhs.winrate ? 0 : lhs.winrate > rhs.winrate ? -1 : 1 })
      combosStr.sort((lhs, rhs) => { return lhs.winrate === rhs.winrate ? 0 : lhs.winrate > rhs.winrate ? -1 : 1 })
      combosAgi.sort((lhs, rhs) => { return lhs.winrate === rhs.winrate ? 0 : lhs.winrate > rhs.winrate ? -1 : 1 })
      combosInt.sort((lhs, rhs) => { return lhs.winrate === rhs.winrate ? 0 : lhs.winrate > rhs.winrate ? -1 : 1 })

      let combos = [
        {'type': 'Melee', 'items': combosMelee.slice(0, 25)},
        {'type': 'Range', 'items': combosRange.slice(0, 25)},
        {'type': 'Str', 'items': combosStr.slice(0, 25)},
        {'type': 'Agi', 'items': combosAgi.slice(0, 25)},
        {'type': 'Int', 'items': combosInt.slice(0, 25)}
      ]

      vm.data = match
      vm.combos = combos
      vm.ready = true
    }).catch(function (error) {
      console.log(error)
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
