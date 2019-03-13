<template>
  <b-row>
    <b-col>
      <h4 class="text-center">{{details.summary.name}}</h4>
      <b-row>
        <b-col>
          <img :src="details.summary.image" class="ability-icon-lg">
        </b-col>
        <b-col>
          <table class="table table-sm ">
            <tr>
              <td>Type</td>
              <td>
                <span v-if="details.ability.is_ultimate == true">Ultimate</span>
                <span v-else>Skill</span>
              </td>
            </tr>
            <tr>
              <td>Effect</td>
              <td>
                <span>{{ formatBehaviorsList(details.ability.ability_behaviors) }}</span>
              </td>
            </tr>
            <tr v-if="details.ability.ability_unit_damage_type != null">
              <td>Damage</td>
              <td>
                <span v-if="details.ability.ability_unit_damage_type == 'DAMAGE_TYPE_MAGICAL'" class="text-success">Magical</span>
                <span v-if="details.ability.ability_unit_damage_type == 'DAMAGE_TYPE_PHYSICAL'" class="text-primary">Physical</span>
                <span v-if="details.ability.ability_unit_damage_type == 'DAMAGE_TYPE_PURE'" class="text-danger">Pure</span>
              </td>
            </tr>
            <tr v-if="details.ability.has_scepter_upgrade == true">
              <td>Aghanim's <img src="https://hgv-hyperstone.azurewebsites.net/items/ultimate_scepter.png" class="ability-icon-xs" /></td>
              <td>
                <span class="text-info">Yes</span>
              </td>
            </tr>
            <tr>
              <td>Cooldown</td>
              <td>
                {{ details.ability.ability_cooldown.join(" / ") }}
              </td>
            </tr>
            <tr>
              <td>Manacost</td>
              <td>
                {{ details.ability.ability_mana_cost.join(" / ") }}
              </td>
            </tr>
          </table>
        </b-col>
        <b-col>
          <div class="p-1">
            <b>Win Rate</b>
            <b-progress :max="1">
              <b-progress-bar :value="details.summary.win_rate" variant="primary" :striped="true">
                <strong>{{ formatPercentage(details.summary.win_rate) }}</strong>
              </b-progress-bar>
              <b-progress-bar :value="1-details.summary.win_rate" variant="secondary"/>
            </b-progress>
          </div>
          <div class="p-1">
            <b>
              Picks
              <small>as a ratio to the other abilities</small>
            </b>
            <b-progress :max="1">
              <b-progress-bar :value="details.summary.picks_percentage" variant="info" :striped="true" />
              <b-progress-bar :value="1-details.summary.picks_percentage" variant="secondary"/>
            </b-progress>
          </div>
          <div class="p-1">
            <b>
              Wins
              <small>as a ratio to the other abilities</small>
            </b>
            <b-progress :max="1">
              <b-progress-bar :value="details.summary.wins_percentage" variant="warning" :striped="true" />
              <b-progress-bar :value="1-details.summary.wins_percentage" variant="secondary"/>
            </b-progress>
          </div>
        </b-col>
      </b-row>
      <br />
      <div>
        <p>{{details.ability.description}}</p>
        <p v-if="details.ability.upgrade_description">{{details.ability.upgrade_description}}</p>
      </div>
      <h4 class="text-center">Heroes</h4>
      <p>
        We have include the top heroes paired with this ability.
        Only heroes that where above avg pick rate where included.
        We also only show the top 10 for brevity.
      </p>
      <b-table 
          :fields="['image', 'name', 'picks', 'wins', 'win_rate']"
          :items="details.heroes" 
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
          <template slot="wins" slot-scope="row">
            <b-progress variant="warning" height="1.5rem" :value="row.item.wins" :min="0" :max="100" :striped="true"></b-progress>
          </template>
          <template slot="picks" slot-scope="row">
            <b-progress variant="info" height="1.5rem" :value="row.item.picks" :min="0" :max="100" :striped="true"></b-progress>
          </template>
      </b-table>
      <br />
      <p>
        Much like it is more important to gain a sense of which types ability work for heroes, you can use this to gain a sense best type of heroes that fit this ability.
        Knowing your archetypes will help you make sure you can getting the most out of any ability.
      </p>
      <b-row>
        <template v-for="(value) in details.types">
          <b-col :key="value.key">
            <img :src="value.image" class="float-left p-1" />
            <b>{{value.name}}</b>
            <b-progress :max="1">
              <b-progress-bar :value="value.win_rate" variant="info" :striped="true" > 
                <strong>{{ formatPercentage(value.win_rate) }}</strong>
              </b-progress-bar>
              <b-progress-bar :value="1-value.win_rate" variant="secondary"  />
            </b-progress>
          </b-col>
        </template>
      </b-row>
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
        <b-table 
            :fields="['image', 'name', 'picks', 'wins', 'win_rate']"
            :items="details.combos.abilities" 
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
            <template slot="wins" slot-scope="row">
              <b-progress variant="warning" height="1.5rem" :value="row.item.wins" :min="0" :max="100" :striped="true"></b-progress>
            </template>
            <template slot="picks" slot-scope="row">
              <b-progress variant="info" height="1.5rem" :value="row.item.picks" :min="0" :max="100" :striped="true"></b-progress>
            </template>
          </b-table>
      </div>
      <div v-if="details.combos.ultimates.length > 0">
        <h5 class="text-center">Ultimates</h5>
        <b-table 
            :fields="['image', 'name', 'picks', 'wins', 'win_rate']"
            :items="details.combos.ultimates" 
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
            <template slot="wins" slot-scope="row">
              <b-progress variant="warning" height="1.5rem" :value="row.item.wins" :min="0" :max="100" :striped="true"></b-progress>
            </template>
            <template slot="picks" slot-scope="row">
              <b-progress variant="info" height="1.5rem" :value="row.item.picks" :min="0" :max="100" :striped="true"></b-progress>
            </template>
          </b-table>
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
</template>

<script>
import numeral from 'numeral'
import abilities from "@/assets/data/ability-details.json";

export default {
  name: 'ability',
  data () {
    return {
      'details': abilities[this.$route.params.id]
    }
  },
  methods: {
    formatPercentage(value) {
      return numeral(value).format('0%');
    },
    formatBehaviorsList(list) {
      var elements = [];
      if(list.includes('DOTA_ABILITY_BEHAVIOR_PASSIVE')) {
        elements.push('Passive');
      }
      if(list.includes('DOTA_ABILITY_BEHAVIOR_AOE')) {
        elements.push('AOE');
      }
      if(list.includes('DOTA_ABILITY_BEHAVIOR_CHANNELLED')) {
        elements.push('Channelled');
      }
      if(list.includes('DOTA_ABILITY_BEHAVIOR_AUTOCAST')) {
        elements.push('AutoCast');
      }
      if(list.includes('DOTA_ABILITY_BEHAVIOR_TOGGLE')) {
        elements.push('Toggle');
      }
      if(list.includes('DOTA_ABILITY_BEHAVIOR_AURA')) {
        elements.push('Aura');
      }

      return elements.join(', ');
    }
  },
  watch: {
    $route(to, from) {
      this.details = abilities[to.params.id];
    }
  }
}
</script>