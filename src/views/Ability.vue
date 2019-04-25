<template>
  <hgv-loading :urls="urls" v-on:loaded="loaded">
    <b-row v-if="ability">
      <b-col>
        <h4 class="text-center">{{ability.details.name}}</h4>

        <AbilityCard :ability="ability.details" :showTitle="false" />
        <br />
        
        <b-card bg-variant="light">
          <b-row>
            <b-col>
              <b>Kda</b><br />
              <span>{{formatDecimal(ability.summary.kda)}}</span>
            </b-col>
            <b-col>
              <b>Kills</b><br />
            <span>{{formatNumber(ability.summary.kills)}}</span>
            </b-col>
            <b-col>
              <b>Picks</b><br />
            <span>{{formatNumber(ability.summary.picks)}}</span>
            </b-col>
            <b-col>
              <b>Wins</b><br />
            <span>{{formatNumber(ability.summary.wins)}}</span>
            </b-col>
            <b-col>
              <b>Win Rate</b><br />
              <b-progress :max="1" :value="ability.summary.winrate" variant="primary" :striped="true" show-progress></b-progress>
            </b-col>
          </b-row>
        </b-card>
        <br />

        <h4 class="text-center">Heroes</h4>
        <p>
          We have include the top heroes paired with this ability.
          Only heroes that where above avg pick rate where included.
          We also only show the top 10 for brevity.
        </p>
        <b-card bg-variant="light">
          <b-table 
              :fields="['image', 'name', 'kda', 'kills', 'picks', 'wins', 'win_rate']"
              :items="ability.heroes"
              :per-page="page.size"
              :current-page="page.heroes"
              :sort-by="'kda'"
              :sort-desc="'Descending'"
              >
              <template slot="image" slot-scope="row">
                <b-img :src="row.item.image" class="hero-icon-banner-sm" />
              </template>
              <template slot="name" slot-scope="row">
                <b-link :to="'/ability/' + row.item.ability_id" target="_blank">{{row.item.name}}</b-link>
              </template>
              <template slot="win_rate" slot-scope="row">
                <b-progress variant="primary" height="1.5rem" :value="row.item.win_rate" :min="0" :max="1" :striped="true" show-progress></b-progress>
              </template>
              <template slot="kda" slot-scope="row">
                <span>{{formatDecimal(row.item.kda)}}</span>
              </template>
              <template slot="kills" slot-scope="row">
                <span>{{formatNumber(row.item.kills)}}</span>
              </template>
              <template slot="wins" slot-scope="row">
                <span>{{formatNumber(row.item.wins)}}</span>
              </template>
              <template slot="picks" slot-scope="row">
                <span>{{formatNumber(row.item.picks)}}</span>
              </template>
          </b-table>
        </b-card>
        <br />
        <b-pagination v-model="page.heroes" :total-rows="ability.heroes.length" :per-page="page.size" align="center"></b-pagination>

        <p>
          Much like it is more important to gain a sense of which types ability work for heroes, you can use this to gain a sense best type of heroes that fit this ability.
          Knowing your archetypes will help you make sure you can getting the most out of any ability.
        </p>
        <b-card bg-variant="light">
          <b-row>  
              <template v-for="(value) in ability.hero_roles">
                <b-col :key="value.key">
                  <b>{{value.role}}</b>
                  <b-progress :max="1" :value="value.win_rate" :striped="true" show-progress></b-progress>
                </b-col>
              </template>
          </b-row>
        </b-card>
        <br />
        <h4 class="text-center">Combos</h4>
        <p>
          We have included the top Ability and Ultimate Combos for this ability.
          Only abilities that where above the avg pick rate where included.
          We also only show the top 10 for brevity.
        </p>
        <p>
          After selecting the best ability for your hero or the strongest ability in the pool, what is next? 
          This is where you can find the best pairing for that ability.
        </p>
        <p>
          If this this skill is an Ultimate then no other Ultimates will appear in the combos list as you can't have more then one.
        </p>
        <div>
          <h5 class="text-center">Abilities</h5>
          <b-card bg-variant="light">
            <b-table 
                :fields="['image', 'name', 'kda', 'kills', 'picks', 'wins', 'win_rate']"
                :items="ability.combo_abilities" 
                :per-page="page.size"
                :current-page="page.abilities"
                :sort-by="'kda'"
                :sort-desc="'Descending'"
                >
                <template slot="image" slot-scope="row">
                  <b-img :src="row.item.image" class="hero-icon-banner-sm" />
                </template>
                <template slot="name" slot-scope="row">
                  <b-link :to="'/ability/' + row.item.ability_id" target="_blank">{{row.item.name}}</b-link>
                </template>
                <template slot="win_rate" slot-scope="row">
                  <b-progress height="1.5rem" :value="row.item.win_rate" :min="0" :max="1" :striped="true" show-progress></b-progress>
                </template>
                <template slot="kda" slot-scope="row">
                  <span>{{formatDecimal(row.item.kda)}}</span>
                </template>
                <template slot="kills" slot-scope="row">
                  <span>{{formatNumber(row.item.kills)}}</span>
                </template>
                <template slot="wins" slot-scope="row">
                  <span>{{formatNumber(row.item.wins)}}</span>
                </template>
                <template slot="picks" slot-scope="row">
                  <span>{{formatNumber(row.item.matches)}}</span>
                </template>
              </b-table>
            </b-card>
            <br />
            <b-pagination v-model="page.abilities" :total-rows="ability.combo_abilities.length" :per-page="page.size" align="center"></b-pagination>
        </div>
        <div v-if="ability.combo_ultimates.length > 0">
          <h5 class="text-center">Ultimates</h5>
          <b-card bg-variant="light">
            <b-table 
                :fields="['image', 'name', 'kda', 'kills', 'picks', 'wins', 'win_rate']"
                :items="ability.combo_ultimates" 
                :per-page="page.size"
                :current-page="page.ultimates"
                :sort-by="'kda'"
                :sort-desc="'Descending'"
                >
                <template slot="image" slot-scope="row">
                  <b-img :src="row.item.image" class="hero-icon-banner-sm" />
                </template>
                <template slot="name" slot-scope="row">
                  <b-link :to="'/ability/' + row.item.ability_id" target="_blank">{{row.item.name}}</b-link>
                </template>
                <template slot="win_rate" slot-scope="row">
                  <b-progress height="1.5rem" :value="row.item.win_rate" :min="0" :max="1" :striped="true" show-progress></b-progress>
                </template>
                  <template slot="kda" slot-scope="row">
                  <span>{{formatDecimal(row.item.kda)}}</span>
                </template>
                <template slot="kills" slot-scope="row">
                  <span>{{formatNumber(row.item.kills)}}</span>
                </template>
                <template slot="wins" slot-scope="row">
                  <span>{{formatNumber(row.item.wins)}}</span>
                </template>
                <template slot="picks" slot-scope="row">
                  <span>{{formatNumber(row.item.matches)}}</span>
                </template>
              </b-table>
            </b-card>
            <br />
            <b-pagination v-model="page.abilities" :total-rows="ability.combo_abilities.length" :per-page="page.size" align="center"></b-pagination>
        </div>
      </b-col>
      <b-col cols="4" lg="3">
        <div class="text-center">
          <b-alert variant="info" show>   
            <p>
              Get to know your abilities. We have included the basic details that everyone should know.
            </p>
          </b-alert>
          <img src="@/assets/imgs/cluckles-speach.png" class="cluckles-speach" />
        </div>
      </b-col>
    </b-row>
  </hgv-loading>
</template>

<script>
import AbilityCard from "@/components/AbilityCard";

export default {
  name: 'ability',
  data () {
    let id = this.$route.params.id;
    return {
      'urls': [`/static/ability.${id}.json`],
      'ability': null,
      'page': {
        'size': 10,
        'heroes': 1,
        'abilities': 1,
        'ultimates': 1,
      }
    }
  },
  components: { AbilityCard },
  methods: {
    loaded(data) {
      var self = this;
      self.ability = data[0];
    },
  },
  /*
  watch: {
    $route(to, from) {
      // this.details = abilities[to.params.id];
    }
  }
  */
}
</script>