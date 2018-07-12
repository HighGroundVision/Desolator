<template>
  <section v-if="ready" class="opaque-background">
    <b-row>
      <b-col>
        <h1 class="text-warning">Combo Stats</h1>
      </b-col>
    </b-row>
    <b-row>
      <b-col>
        <p>
          We have compiled win rates since 7.07 patch using random daily sampling. 
          Combos are created by combining all active skills with each other (creating Set A) then all skills are combind with each active ultimate (creating Set B). 
          Set A is then merge with Set B creating all posible ability combos this is then paired with each type (Str, Agi, Int, Melee, Range) to create ~1M triplets
          This also means the chance that one of these triplets show up in a match is very low. 
          To counter act this offset we have filter out of results triplets that do not meet the minimum number of picks. 
          Which is implemented an average of whole set. 
          After this filter we are left with ~120,000 triplets.
        </p>
      </b-col>
    </b-row>
    <b-row>
      <b-col>
        <div class="text-center">
          <b-btn variant="primary" v-b-modal.modalSearch >Search For Combo</b-btn>
        </div>
      </b-col>
    </b-row>
    <hr class="highlighted" />
    <b-row>
      <b-col>
        <div class="text-center">
          <p>Below we highlight the top 10 combos under each category.</p>
        </div>
      </b-col>
    </b-row>
    <b-row>
      <b-col class="text-center">
        <b-img src="https://hgv-hyperstone.azurewebsites.net/mics/type_melee.png" title="Melee" class="ability-icon-sm" /> 
        <h3>Melee</h3>
      </b-col>
    </b-row>
    <br />
    <b-row>
      <b-col>
        <b-row v-for="stat in topMelee" :key="topMelee.indexOf(stat)">
          <b-col>
            <b-img :src="stat.pair[0].img" class="ability-icon-sm" />
            <b-img :src="stat.pair[1].img" class="ability-icon-sm" />
          </b-col>
          <b-col cols="4">
            <b-link :to="'/stats/abilities/' + stat.abilities">{{stat.pair[0].dname}} | {{stat.pair[1].dname}}</b-link>
          </b-col>
          <b-col cols="4">
            <b-progress height="1.5rem" :value="stat.win_rate" :min="0" :max="100" :striped="true" show-progress></b-progress>
          </b-col>
          <b-col>
            {{stat.wins}} / {{stat.picks}}
          </b-col>
          <b-col>
            <span v-if="stat.ranking">{{stat.ranking.total}}</span>
            <span v-else title="N/A">--</span>
          </b-col>
        </b-row>
      </b-col>
    </b-row>
    <hr />
    <b-row>
      <b-col class="text-center">
        <b-img src="https://hgv-hyperstone.azurewebsites.net/mics/type_range.png" title="Range" class="ability-icon-sm" /> 
        <h3>Range</h3>
      </b-col>
    </b-row>
    <br />
    <b-row>
      <b-col>
        <b-row v-for="stat in topRange" :key="topRange.indexOf(stat)">
          <b-col>
            <b-img :src="stat.pair[0].img" class="ability-icon-sm" />
            <b-img :src="stat.pair[1].img" class="ability-icon-sm" />
          </b-col>
          <b-col cols="4">
            <b-link :to="'/stats/abilities/' + stat.abilities">{{stat.pair[0].dname}} | {{stat.pair[1].dname}}</b-link>
          </b-col>
          <b-col cols="4">
            <b-progress height="1.5rem" :value="stat.win_rate" :min="0" :max="100" :striped="true" show-progress></b-progress>
          </b-col>
          <b-col>
            {{stat.wins}} / {{stat.picks}}
          </b-col>
          <b-col>
            <span v-if="stat.ranking">{{stat.ranking.total}}</span>
            <span v-else title="N/A">--</span>
          </b-col>
        </b-row>
      </b-col>
    </b-row>
    <hr />
    <b-row>
      <b-col class="text-center">
        <b-img src="https://hgv-hyperstone.azurewebsites.net/mics/primary_str.png" title="Str" class="ability-icon-sm" /> 
        <h3>Str</h3>
      </b-col>
    </b-row>
    <br />
    <b-row>
      <b-col>
        <b-row v-for="stat in topStr" :key="topStr.indexOf(stat)">
          <b-col>
            <b-img :src="stat.pair[0].img" class="ability-icon-sm" />
            <b-img :src="stat.pair[1].img" class="ability-icon-sm" />
          </b-col>
          <b-col cols="4">
            <b-link :to="'/stats/abilities/' + stat.abilities">{{stat.pair[0].dname}} | {{stat.pair[1].dname}}</b-link>
          </b-col>
          <b-col cols="4">
            <b-progress height="1.5rem" :value="stat.win_rate" :min="0" :max="100" :striped="true" show-progress></b-progress>
          </b-col>
          <b-col>
            {{stat.wins}} / {{stat.picks}}
          </b-col>
          <b-col>
            <span v-if="stat.ranking">{{stat.ranking.total}}</span>
            <span v-else title="N/A">--</span>
          </b-col>
        </b-row>
      </b-col>
    </b-row>
    <hr />
    <b-row>
      <b-col class="text-center">
        <b-img src="https://hgv-hyperstone.azurewebsites.net/mics/primary_agi.png" title="Agi" class="ability-icon-sm" /> 
        <h3>Agi</h3>
      </b-col>
    </b-row>
    <br />
    <b-row>
      <b-col>
        <b-row v-for="stat in topAgi" :key="topAgi.indexOf(stat)">
          <b-col>
            <b-img :src="stat.pair[0].img" class="ability-icon-sm" />
            <b-img :src="stat.pair[1].img" class="ability-icon-sm" />
          </b-col>
          <b-col cols="4">
            <b-link :to="'/stats/abilities/' + stat.abilities">{{stat.pair[0].dname}} | {{stat.pair[1].dname}}</b-link>
          </b-col>
          <b-col cols="4">
            <b-progress height="1.5rem" :value="stat.win_rate" :min="0" :max="100" :striped="true" show-progress></b-progress>
          </b-col>
          <b-col>
            {{stat.wins}} / {{stat.picks}}
          </b-col>
          <b-col>
            <span v-if="stat.ranking">{{stat.ranking.total}}</span>
            <span v-else title="N/A">--</span>
          </b-col>
        </b-row>
      </b-col>
    </b-row>
    <hr />
    <b-row>
      <b-col class="text-center">
        <b-img src="https://hgv-hyperstone.azurewebsites.net/mics/primary_int.png" title="Int" class="ability-icon-sm" /> 
        <h3>Int</h3>
      </b-col>
    </b-row>
    <br />
    <b-row>
      <b-col>
        <b-row v-for="stat in topInt" :key="topInt.indexOf(stat)">
          <b-col>
            <b-img :src="stat.pair[0].img" class="ability-icon-sm" />
            <b-img :src="stat.pair[1].img" class="ability-icon-sm" />
          </b-col>
          <b-col cols="4">
            <b-link :to="'/stats/abilities/' + stat.abilities">{{stat.pair[0].dname}} | {{stat.pair[1].dname}}</b-link>
          </b-col>
          <b-col cols="4">
            <b-progress height="1.5rem" :value="stat.win_rate" :min="0" :max="100" :striped="true" show-progress></b-progress>
          </b-col>
          <b-col>
            {{stat.wins}} / {{stat.picks}}
          </b-col>
          <b-col>
            <span v-if="stat.ranking">{{stat.ranking.total}}</span>
            <span v-else title="N/A">--</span>
          </b-col>
        </b-row>
      </b-col>
    </b-row>
    <!--Dialog - Help -->
    <b-modal id="modalSearch" title="Search" size="lg" :hide-footer="true" header-bg-variant="dark" header-text-variant="light" body-bg-variant="dark" footer-bg-variant="dark">
      <b-form>
        <b-form-group>
          <b-form-input v-model="searchAbility1" placeholder="Ability 1"  />
        </b-form-group>
        <b-form-group>
          <b-form-input v-model="searchAbility2" placeholder="Ability 2"  />
        </b-form-group>
        <b-form-group>
          <b-btn variant="primary" @click="search">Find</b-btn>
        </b-form-group>
      </b-form>
      <hr class="highlighted" />
       <b-container class="serachResults">
        <template v-for="item in searchList">
          <b-row :key="item.key" >
            <template v-for="ability in item.abilities">
              <b-col :key="ability.id">
                <b-img :src="ability.img" class="ability-icon-sm" ></b-img>
                <span>{{ability.name}}</span>
              </b-col>
            </template>
            <b-col>
              <b-link :to="'/stats/abilities/' + item.key">Details</b-link>
            </b-col>
          </b-row>
        </template>
      </b-container>
    </b-modal>
  </section>
  <section v-else class="opaque-background text-center">
    <hgv-loader :color="'#ffc107'"></hgv-loader>
  </section>
</template>

<script>
import axios from 'axios'

export default {
  name: 'CombosList',
  methods: {
    format: function (stat) {
      if (Array.isArray(stat)) {
        return stat.join(' / ')
      } else {
        return stat
      }
    },
    search () {
      let filter1 = (this.searchAbility1) ? this.searchAbility1.toLowerCase() : ''
      let filter2 = (this.searchAbility2) ? this.searchAbility2.toLowerCase() : ''
      if (filter1 === '' && filter2 === '') {
        return
      }

      let abilities1 = this.abilities.filter(_ => _.name.toLowerCase().includes(filter1))
      let abilities2 = this.abilities.filter(_ => _.name.toLowerCase().includes(filter2))

      let collection = []
      for (const item1 of abilities1) {
        for (const item2 of abilities2) {
          if (item1.id === item2.id) {
            continue
          }
          if (item1.ultimate === true && item2.ultimate === true) {
            continue
          }

          let data = {
            'key': (item1.id < item2.id) ? item1.id + '-' + item2.id : item2.id + '-' + item1.id,
            'abilities': [item1, item2]
          }
          collection.push(data)
        }
      }

      this.searchList = collection
    }
  },
  data () {
    return {
      'ready': false,
      'abilities': [],
      'topMelee': [],
      'topRange': [],
      'topStr': [],
      'topAgi': [],
      'topInt': [],
      'searchAbility1': null,
      'searchAbility2': null,
      'searchList': []
    }
  },
  created: function () {
    const vm = this

    let web = [
      axios.get('/static/data/draftpool.json').then((reponse) => { return reponse.data }),
      axios.get('/static/data/abilities.json').then((reponse) => { return reponse.data }),
      axios.get('/static/data/stats-combos.json').then((reponse) => { return reponse.data }),
      axios.get(process.env.API_BASE_URL + 'ComboRanking').then((reponse) => { return reponse.data })
    ]

    Promise.all(web).then((values) => {
      const poolDB = values[0]
      const abilitiesDB = values[1]
      const statsDB = values[2]
      const rankingsDB = values[3]

      let melee = []
      let range = []
      let str = []
      let agi = []
      let int = []

      for (let i = 0; i < statsDB.length; i++) {
        let stat = statsDB[i]

        stat.pair = []

        stat.ranking = rankingsDB[stat.abilities]

        const keys = stat.abilities.split('-')
        for (let x = 0; x < keys.length; x++) {
          const id = keys[x]
          const ability = abilitiesDB[id]
          stat.pair.push(ability)
        }

        if (stat.type === 1 && melee.length < 10) {
          melee.push(stat)
        } else if (stat.type === 2 && range.length < 10) {
          range.push(stat)
        } else if (stat.type === 3 && str.length < 10) {
          str.push(stat)
        } else if (stat.type === 4 && agi.length < 10) {
          agi.push(stat)
        } else if (stat.type === 5 && int.length < 10) {
          int.push(stat)
        }

        if (melee.length === 10 && range.length === 10 && str.length === 10 && agi.length === 10 && int.length === 10) {
          break
        }
      }

      for (var h in poolDB) {
        let hero = poolDB[h]
        if (hero.enabled === false) {
          continue
        }

        for (var a in hero.abilities) {
          let ability = hero.abilities[a]
          if (ability.enabled === false) {
            continue
          }

          vm.abilities.push(ability)
        }
      }

      vm.stats = 
      vm.topMelee = melee
      vm.topRange = range
      vm.topStr = str
      vm.topAgi = agi
      vm.topInt = int
      vm.ready = true
    }).catch(function () {
      vm.$router.push('/error')
    })
  }
}
</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped>
.serachResults {
  height: 300px;
  overflow-x: none;
  overflow-y: auto;
}
</style>
