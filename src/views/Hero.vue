<template>
  <b-row>
    <b-col>
      <h4 class="text-center">{{hero.summary.name}}</h4>
      <b-row>
        <b-col>
          <img :src="hero.summary.image">    
        </b-col>
        <b-col>
          <span>Win Rate</span>
          <b-progress :max="1">
            <b-progress-bar :value="hero.summary.win_rate" variant="primary" :striped="true">
              <strong>{{ formatPercentage(hero.summary.win_rate) }}</strong>
            </b-progress-bar>
            <b-progress-bar :value="1-hero.summary.win_rate" variant="secondary"/>
          </b-progress>
          <span>
            Wins
            <small>as a ratio to the max wins</small>
          </span>
          <b-progress :max="1">
            <b-progress-bar :value="hero.summary.wins_ratio" variant="warning" :striped="true">
              <strong>{{ formatPercentage(hero.summary.wins_ratio) }}</strong>
            </b-progress-bar>
            <b-progress-bar :value="1-hero.summary.wins_ratio" variant="secondary"/>
          </b-progress>
        </b-col>
      </b-row>
      <br />
      
      <h4 class="text-center">Attributes</h4>
      <p>Knowing what your hero is made of will help you pick the ability to best complement the heroes' strengths and help make up for their weaknesses.</p>
      <p>
        To that end we have created a table of the hero's most impactful attributes. 
        We also expressed each attribute has a ratio compared to the other heroes.
        So you can see how a hero stacks up to the others.
      </p>
      <ul class="list-group">
        <li class="list-group-item">
          <b-row>
            <b-col>
              <h5>Primary</h5>
            </b-col>
            <b-col cols="2">
              <b-img
                v-if="hero.summary.attribute_primary === 'DOTA_ATTRIBUTE_STRENGTH'"
                src="https://hgv-hyperstone.azurewebsites.net/mics/primary_str.png"
                rounded
                class="ability-icon-sm"
              />
              <b-img
                v-if="hero.summary.attribute_primary === 'DOTA_ATTRIBUTE_AGILITY'"
                src="https://hgv-hyperstone.azurewebsites.net/mics/primary_agi.png"
                rounded
                class="ability-icon-sm"
              />
              <b-img
                v-if="hero.summary.attribute_primary === 'DOTA_ATTRIBUTE_INTELLECT'"
                src="https://hgv-hyperstone.azurewebsites.net/mics/primary_int.png"
                rounded
                class="ability-icon-sm"
              />
            </b-col>
          </b-row>
        </li>
        <li class="list-group-item">
          <b-row>
            <b-col cols="2">
              <b-img
                src="https://hgv-hyperstone.azurewebsites.net/mics/primary_str.png"
                title="Strength"
                rounded
                class="ability-icon-sm"
              />
            </b-col>
            <b-col>
              <b-row>
                <b-col>
                  <span>Base</span>
                </b-col>
                <b-col>
                  <span>{{hero.attributes.attribute_base_strength}}</span>
                </b-col>
                <b-col>
                  <b-progress
                    variant="dark"
                    :value="hero.attributes.ranking_base_strength"
                    :min="0"
                    :max="1"
                    :striped="true"
                  ></b-progress>
                </b-col>
              </b-row>
              <b-row>
                <b-col>
                  <span>Gain</span>
                </b-col>
                <b-col>
                  <span>{{hero.attributes.attribute_strength_gain}}</span>
                </b-col>
                <b-col>
                  <b-progress
                    variant="dark"
                    :value="hero.attributes.ranking_strength_gain"
                    :min="0"
                    :max="1"
                    :striped="true"
                  ></b-progress>
                </b-col>
              </b-row>
            </b-col>
          </b-row>
        </li>
        <li class="list-group-item">
          <b-row>
            <b-col cols="2">
              <b-img
                src="https://hgv-hyperstone.azurewebsites.net/mics/primary_agi.png"
                title="Agility"
                rounded
                class="ability-icon-sm"
              />
            </b-col>
            <b-col>
              <b-row>
                <b-col>
                  <span>Base</span>
                </b-col>
                <b-col>
                  <span>{{hero.attributes.attribute_base_agility}}</span>
                </b-col>
                <b-col>
                  <b-progress
                    variant="dark"
                    :value="hero.attributes.ranking_base_agility"
                    :min="0"
                    :max="1"
                    :striped="true"
                  ></b-progress>
                </b-col>
              </b-row>
              <b-row>
                <b-col>
                  <span>Gain</span>
                </b-col>
                <b-col>
                  <span>{{hero.attributes.attribute_agility_gain}}</span>
                </b-col>
                <b-col>
                  <b-progress
                    variant="dark"
                    :value="hero.attributes.ranking_agility_gain"
                    :min="0"
                    :max="1"
                    :striped="true"
                  ></b-progress>
                </b-col>
              </b-row>
            </b-col>
          </b-row>
        </li>
        <li class="list-group-item">
          <b-row>
            <b-col cols="2">
              <b-img
                src="https://hgv-hyperstone.azurewebsites.net/mics/primary_int.png"
                title="Intelligence"
                rounded
                class="ability-icon-sm"
              />
            </b-col>
            <b-col>
              <b-row>
                <b-col>
                  <span>Base</span>
                </b-col>
                <b-col>
                  <span>{{hero.attributes.attribute_base_intelligence}}</span>
                </b-col>
                <b-col>
                  <b-progress
                    variant="dark"
                    :value="hero.attributes.ranking_base_intelligence"
                    :min="0"
                    :max="1"
                    :striped="true"
                  ></b-progress>
                </b-col>
              </b-row>
              <b-row>
                <b-col>
                  <span>Gain</span>
                </b-col>
                <b-col>
                  <span>{{hero.attributes.attribute_intelligence_gain}}</span>
                </b-col>
                <b-col>
                  <b-progress
                    variant="dark"
                    :value="hero.attributes.ranking_intelligence_gain"
                    :min="0"
                    :max="1"
                    :striped="true"
                  ></b-progress>
                </b-col>
              </b-row>
            </b-col>
          </b-row>
        </li>
        <li class="list-group-item">
          <b-row>
            <b-col cols="2">
              <b-img
                src="https://hgv-hyperstone.azurewebsites.net/mics/health.png"
                title="Health"
                rounded
                class="ability-icon-sm"
              />
            </b-col>
            <b-col>
              <b-row>
                <b-col>
                  <span>Base</span>
                </b-col>
                <b-col>
                  <span>{{hero.attributes.status_health}}</span>
                </b-col>
                <b-col>
                  <b-progress
                    variant="dark"
                    :value="hero.attributes.ranking_health"
                    :min="0"
                    :max="1"
                    :striped="true"
                  ></b-progress>
                </b-col>
              </b-row>
              <b-row>
                <b-col>
                  <span>Regen</span>
                </b-col>
                <b-col>
                  <span>{{hero.attributes.status_health_regen}}</span>
                </b-col>
                <b-col>
                  <b-progress
                    variant="dark"
                    :value="hero.attributes.ranking_health_regen"
                    :min="0"
                    :max="1"
                    :striped="true"
                  ></b-progress>
                </b-col>
              </b-row>
            </b-col>
          </b-row>
        </li>
        <li class="list-group-item">
          <b-row>
            <b-col cols="2">
              <b-img
                src="https://hgv-hyperstone.azurewebsites.net/mics/mana.png"
                title="Mana"
                rounded
                class="ability-icon-sm"
              />
            </b-col>
            <b-col>
              <b-row>
                <b-col>
                  <span>Base</span>
                </b-col>
                <b-col>
                  <span>{{hero.attributes.status_mana}}</span>
                </b-col>
                <b-col>
                  <b-progress
                    variant="dark"
                    :value="hero.attributes.ranking_mana"
                    :min="0"
                    :max="1"
                    :striped="true"
                  ></b-progress>
                </b-col>
              </b-row>
              <b-row>
                <b-col>
                  <span>Regen</span>
                </b-col>
                <b-col>
                  <span>{{hero.attributes.status_mana_regen}}</span>
                </b-col>
                <b-col>
                  <b-progress
                    variant="dark"
                    :value="hero.attributes.ranking_mana_regen"
                    :min="0"
                    :max="1"
                    :striped="true"
                  ></b-progress>
                </b-col>
              </b-row>
            </b-col>
          </b-row>
        </li>
        <li class="list-group-item">
          <b-row>
            <b-col cols="2">
              <b-img
                src="https://hgv-hyperstone.azurewebsites.net/mics/damage.png"
                title="Attack"
                rounded
                class="ability-icon-sm"
              />
            </b-col>
            <b-col>
              <b-row>
                <b-col>
                  <span>Type</span>
                </b-col>
                <b-col>
                  <span
                    class="invert"
                    v-if="hero.attack_capabilities === 'DOTA_UNIT_CAP_MELEE_ATTACK'"
                    title="Attack Type"
                  >Melee</span>
                  <span
                    class="invert"
                    v-if="hero.attack_capabilities === 'DOTA_UNIT_CAP_RANGED_ATTACK'"
                    title="Attack Type"
                  >Ranged</span>
                </b-col>
                <b-col></b-col>
              </b-row>
              <b-row>
                <b-col>
                  <span>Distance</span>
                </b-col>
                <b-col>
                  <span>{{hero.attributes.attack_range}}</span>
                </b-col>
                <b-col>
                  <b-progress
                    variant="dark"
                    :value="hero.attributes.ranking_attack_range"
                    :min="0"
                    :max="1"
                    :striped="true"
                  ></b-progress>
                </b-col>
              </b-row>
              <b-row>
                <b-col>
                  <span>Damage</span>
                </b-col>
                <b-col>
                  <span>{{hero.attributes.attack_damage_min}} - {{hero.attributes.attack_damage_max}}</span>
                </b-col>
                <b-col>
                  <b-progress
                    variant="dark"
                    :value="hero.attributes.ranking_attack_damage"
                    :min="0"
                    :max="1"
                    :striped="true"
                  ></b-progress>
                </b-col>
              </b-row>
            </b-col>
          </b-row>
        </li>
        <li class="list-group-item">
          <b-row>
            <b-col cols="2">
              <b-img
                src="https://hgv-hyperstone.azurewebsites.net/mics/movment.png"
                title="Movement"
                rounded
                class="ability-icon-sm"
              />
            </b-col>
            <b-col>
              <b-row>
                <b-col>
                  <span>Speed</span>
                </b-col>
                <b-col>
                  <span>{{hero.attributes.movement_speed}}</span>
                </b-col>
                <b-col>
                  <b-progress
                    variant="dark"
                    :value="hero.attributes.ranking_movement_speed"
                    :min="0"
                    :max="1"
                    :striped="true"
                  ></b-progress>
                </b-col>
              </b-row>
              <b-row>
                <b-col>
                  <span>Turn</span>
                </b-col>
                <b-col>
                  <span>{{hero.attributes.movement_turn_rate}}</span>
                </b-col>
                <b-col>
                  <b-progress
                    variant="dark"
                    :value="hero.attributes.ranking_movement_turn_rate"
                    :min="0"
                    :max="1"
                    :striped="true"
                  ></b-progress>
                </b-col>
              </b-row>
            </b-col>
          </b-row>
        </li>
        <li class="list-group-item">
          <b-row>
            <b-col cols="2">
              <b-img
                src="https://hgv-hyperstone.azurewebsites.net/mics/vision.png"
                title="Vision"
                rounded
                class="ability-icon-sm"
              />
            </b-col>
            <b-col>
              <b-row>
                <b-col>
                  <span>Day</span>
                </b-col>
                <b-col>
                  <span>{{hero.attributes.vision_daytime_range}}</span>
                </b-col>
                <b-col>
                  <b-progress
                    variant="dark"
                    :value="hero.attributes.ranking_vision_daytime_range"
                    :min="0"
                    :max="1"
                    :striped="true"
                  ></b-progress>
                </b-col>
              </b-row>
              <b-row>
                <b-col>
                  <span>Night</span>
                </b-col>
                <b-col>
                  <span>{{hero.attributes.vision_nighttime_range}}</span>
                </b-col>
                <b-col>
                  <b-progress
                    variant="dark"
                    :value="hero.attributes.ranking_vision_nighttime_range"
                    :min="0"
                    :max="1"
                    :striped="true"
                  ></b-progress>
                </b-col>
              </b-row>
            </b-col>
          </b-row>
        </li>
      </ul>
      <br />
      <h4 class="text-center">Abilities</h4>
      <p>
        We include the default abilities for this your so you can see how good there native build is compared to some of the best combos.
        Some times the best choice is the heroes own ability, be it for a talent or just cause it best ability left in the pool.
      </p>
      <ul class="list-group">
        <li class="list-group-item text-center">
          <b-row>
            <b-col cols="1"></b-col>
            <b-col></b-col>
            <b-col>Picks</b-col>
            <b-col cols="1"></b-col>
            <b-col>Wins</b-col>
            <b-col cols="1"></b-col>
            <b-col>Win Rate</b-col>
          </b-row>
        </li>
        <template v-for="ability in hero.abilities">
          <li :key="ability.id" class="list-group-item">
            <b-row>
              <b-col cols="1">
                <b-img :src="ability.image" rounded class="ability-icon-sm"/>
              </b-col>
              <b-col>
                <b-link :to="'/ability/' + ability.id" target="_blank">
                  <span class="invert">{{ability.name}}</span>
                </b-link>
              </b-col>
              <b-col>
                <b-progress
                  variant="info"
                  height="1.5rem"
                  :value="ability.picks_ratio"
                  :min="0"
                  :max="1"
                  :striped="true"
                ></b-progress>
              </b-col>
              <b-col cols="1">{{formatNumber(ability.picks)}}</b-col>
              <b-col>
                <b-progress
                  variant="warning"
                  height="1.5rem"
                  :value="ability.wins_ratio"
                  :min="0"
                  :max="1"
                  :striped="true"
                ></b-progress>
              </b-col>
              <b-col cols="1">{{formatNumber(ability.wins)}}</b-col>
              <b-col>
                <b-progress
                  height="1.5rem"
                  :value="ability.win_rate"
                  :min="0"
                  :max="1"
                  :striped="true"
                  show-progress
                ></b-progress>
              </b-col>
            </b-row>
          </li>
        </template>

        <template v-for="ability in hero.ultimates">
          <li :key="ability.id" class="list-group-item">
            <b-row>
              <b-col cols="1">
                <b-img :src="ability.image" rounded class="ability-icon-sm"/>
              </b-col>
              <b-col>
                <b-link :to="'/ability/' + ability.id" target="_blank">
                  <span class="invert">{{ability.name}}</span>
                </b-link>
              </b-col>
              <b-col>
                <b-progress
                  variant="info"
                  height="1.5rem"
                  :value="ability.picks_ratio"
                  :min="0"
                  :max="1"
                  :striped="true"
                ></b-progress>
              </b-col>
              <b-col cols="1">{{formatNumber(ability.picks)}}</b-col>
              <b-col>
                <b-progress
                  variant="warning"
                  height="1.5rem"
                  :value="ability.wins_ratio"
                  :min="0"
                  :max="1"
                  :striped="true"
                ></b-progress>
              </b-col>
              <b-col cols="1">{{formatNumber(ability.wins)}}</b-col>
              <b-col>
                <b-progress
                  height="1.5rem"
                  :value="ability.win_rate"
                  :min="0"
                  :max="1"
                  :striped="true"
                  show-progress
                ></b-progress>
              </b-col>
            </b-row>
          </li>
        </template>
      </ul>
      <br />

      <h4 class="text-center">Talents</h4>
      <p>
        We have include the talents win rate so you know which talents win more then others.
        You should always take into account your draft and how the match progressing to prioritize different talents based on your needs.
        You should know about choices that require one of the heroes abilities, which we have <b  class="text-info">highlighted</b>.
        Also, watch out for forks that both require a heroes ability.
      </p>
      <b-row>
         <template v-for="talent in hero.talents">
          <b-col :key="talent.key" cols="6">
            <div class="p-1">
              <b v-bind:class="{ 'text-info': talent.key.includes('unique') }" :title="talent.description">{{talent.name}}</b>
              <br />
              <b-progress :value="talent.win_rate" :min="0" :max="1" show-progress></b-progress>
            </div>
          </b-col>
        </template>
      </b-row>
      <br />

      <h4 class="text-center">Combos</h4>
      <p>
        We have included the top Ability and Ultimate Combos for this hero.
        Only abilities that where above the avg pick rate where included.
        We also only show the top 10 for brevity.
      </p>
      <p>
        It is more important to gain a sense of which types of abilities from <b class="text-info">Speed</b>, <b class="text-info">Damage</b>, <b class="text-info">Tank</b>, <b class="text-info">Stuns</b>, <b class="text-info">Escape</b> etc. give your hero the best chance of success.
        As the draft often moves quickly and changes with each pick, locking your sights in on one ability or combo early on is one the sins of amateur Drafter.
        Knowing your archetypes will help you adapt quickly in a draft.
      </p>
      <h5 class="text-center">Abilities</h5>
      <b-table 
          :fields="['image', 'name', 'has_upgrade', 'picks', 'picks_ratio', 'wins', 'wins_ratio', 'win_rate']"
          :items="hero.combos.abilities" 
          >
          <template slot="image" slot-scope="row">
            <b-img :src="row.item.image" class="ability-icon-sm" />
          </template>
          <template slot="name" slot-scope="row">
            <b-link :to="'/ability/' + row.item.ability_id" target="_blank">{{row.item.name}}</b-link>
          </template>
          <template slot="win_rate" slot-scope="row">
            <b-progress height="1.5rem" :value="row.item.win_rate" :min="0" :max="1" :striped="true" show-progress></b-progress>
          </template>
          <template slot="wins" slot-scope="row">
            <span>{{row.item.wins}}</span>
          </template>
          <template slot="picks" slot-scope="row">
            <span>{{row.item.picks}}</span>
          </template>
          <template slot="wins_ratio" slot-scope="row">
            <b-progress variant="warning" height="1.5rem" :value="row.item.wins_ratio" :min="0" :max="1" :striped="true" show-progress></b-progress>
          </template>
          <template slot="picks_ratio" slot-scope="row">
            <b-progress variant="info" height="1.5rem" :value="row.item.picks_ratio" :min="0" :max="1" :striped="true" show-progress></b-progress>
          </template>
          <template slot="has_upgrade" slot-scope="row">
            <span v-if="row.item.has_upgrade" class="badge badge-success">Yes</span>
            <span v-else class="badge badge-secondary">No</span>
          </template>
        </b-table>

      <h5 class="text-center">Ultimates</h5>
      <b-table 
          :fields="['image', 'name', 'has_upgrade', 'picks', 'picks_ratio', 'wins', 'wins_ratio', 'win_rate']"
          :items="hero.combos.ultimates" 
          >
          <template slot="image" slot-scope="row">
            <b-img :src="row.item.image" class="ability-icon-sm" />
          </template>
          <template slot="name" slot-scope="row">
            <b-link :to="'/ability/' + row.item.ability_id" target="_blank">{{row.item.name}}</b-link>
          </template>
          <template slot="win_rate" slot-scope="row">
            <b-progress height="1.5rem" :value="row.item.win_rate" :min="0" :max="1" :striped="true" show-progress></b-progress>
          </template>
          <template slot="wins" slot-scope="row">
            <span>{{row.item.wins}}</span>
          </template>
          <template slot="picks" slot-scope="row">
            <span>{{row.item.picks}}</span>
          </template>
          <template slot="wins_ratio" slot-scope="row">
            <b-progress variant="warning" height="1.5rem" :value="row.item.wins_ratio" :min="0" :max="1" :striped="true" show-progress></b-progress>
          </template>
          <template slot="picks_ratio" slot-scope="row">
            <b-progress variant="info" height="1.5rem" :value="row.item.picks_ratio" :min="0" :max="1" :striped="true" show-progress></b-progress>
          </template>
          <template slot="has_upgrade" slot-scope="row">
            <span v-if="row.item.has_upgrade" class="badge badge-success">Yes</span>
            <span v-else class="badge badge-secondary">No</span>
          </template>
        </b-table>

    </b-col>
    <b-col cols="4" lg="3">
      <div class="text-center">
        <b-alert variant="info" show>
          <p>Every hero is unique, like a snowflake.</p>
        </b-alert>
        <img src="@/assets/imgs/cluckles-speach.png" class="cluckles-speach">
      </div>
    </b-col>
  </b-row>
</template>

<script>
import numeral from "numeral";
import heroes from "@/assets/data/hero-details.json";

export default {
  name: "hero",
  data() {
    return {
      hero: heroes[this.$route.params.id]
    };
  },
  methods: {
    formatPercentage(value) {
      return numeral(value).format("0%");
    },
    formatNumber(value) {
      return numeral(value).format('0.0a');
    },
  },
  watch: {
    $route(to, from) {
      this.hero = heroes[to.params.id];
    }
  }
};
</script>