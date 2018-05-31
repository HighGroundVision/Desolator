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
              <th>Player</th>
              <th>Heroes</th>
              <th class="text-primary">Abilities</th>
              <th class="text-primary"><span style="font-size: 0.8em;">Cast</span></th>
              <th class="text-primary"><span style="font-size: 0.8em;">Hits</span></th>
              <th class="text-primary"><span style="font-size: 0.8em;">Dmg</span></th>
              <th>Level</th>
              <th class="text-success">K</th>
              <th class="text-danger">D</th>
              <th class="text-info">A</th>
              <th class="text-warning">G</th>
              <th class="text-warning">GPM</th>
              <th>XPM</th>
              <th width="300px">Items</th>
            </tr>
          </thead>
          <tbody>
            <template v-for="player in data.players">
              <tr v-bind:item="player" v-bind:key="player.player_slot" v-bind:class="{ 'bg-info': player.self }" >
                <td>
                  {{player.personaname}}
                </td>
                <td>
                  <b-img :src="player.hero_img" :title="player.hero_name" class="hero-icon-lg" />
                </td>
                <td>
                  <ul class="list-unstyled">
                    <template v-for="ability in player.abilities" >
                      <li v-bind:key="ability.id">
                        <b-link @click="gotoAbility(ability)">
                          <b-img :src="ability.img" :title="ability.name" class="ability-icon-sm" />
                        </b-link>
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
                  {{player.level}}
                </td>
                <td>
                  {{player.kills}}
                </td>
                <td>
                  {{player.assists}}
                </td>
                <td>
                  {{player.assists}}
                </td>
                <td>
                  {{player.gold}}
                </td>
                <td>
                  {{player.gold_per_min}}
                </td>
                <td>
                  {{player.xp_per_min}}
                </td>
                <td class="text-left">
                  <template v-for="item in player.items" >
                    <b-img v-bind:key="item.id" :src="item.img" :title="item.name" class="item-icon-sm" />
                  </template>
                  <template v-for="item in player.backpack" >
                    <b-img v-bind:key="item.id" :src="item.img" :title="item.name" class="item-icon-sm" />
                  </template>
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
    let p5 = axios.get('/static/data/items.json').then((reponse) => { return reponse.data })

    Promise.all([p0, p1, p2, p3, p4, p5]).then((values) => {
      const match = values[0]
      const patchesDB = values[1]
      const regionsDB = values[2]
      const abilitiesDB = values[3]
      const heroesDB = values[4]
      const itemsDB = values[5]

      match.start_time = moment.unix(match.start_time).fromNow()
      match.duration = moment.duration(match.duration, 'seconds').humanize()

      let region = regionsDB[match.region]
      match.region = region === undefined ? 'Unknown (' + match.region + ')' : region

      let patch = patchesDB[match.patch]
      match.patch = patch === undefined ? 'Unknown (' + match.patch + ')' : patch.name

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
        player.hero_img = hero.img
        player.hero_name = hero.name

        // Abilties & Stats
        let abilities = []
        let skills = Array.from(new Set(player.ability_upgrades_arr))
        for (let x = 0; x < skills.length; x++) {
          const id = skills[x]
          const ability = abilitiesDB[id]
          if (ability === undefined) {
            continue
          }

          let usageTotal = 0
          let usageKey = Object.keys(player.ability_uses).filter((lhs) => lhs === ability.key)
          if (usageKey) {
            if (player.ability_uses[usageKey]) {
              usageTotal = player.ability_uses[usageKey]
            }
          }

          let hitTotal = 0
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
  
          let damgeTotal = 0
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

          let data = {
            'id': id,
            'name': ability.dname,
            'img': ability.img,
            'casts': usageTotal === 0 ? '--' : usageTotal,
            'hit': hitTotal === 0 ? '--' : hitTotal,
            'damage': damgeTotal === 0 ? '--' : damgeTotal
          }
          abilities.push(data)
        }

        player.abilities = abilities

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

      vm.data = match
      vm.ready = true
    }).catch(function (error) {
      console.log(error)
    })
  },
  methods: {
    gotToOpenDota () {
      window.open('https://www.opendota.com/matches/' + this.data.match_id)
    },
    gotoAbility (ability) {
      console.log(ability)
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
</style>
