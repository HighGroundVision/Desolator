<template>
  <b-row>
    <b-col>
      <h4>{{hero.summary.name}}</h4>
      <h5>Summary</h5>
      <img :src="hero.summary.image">
      <br>
      <span>Win Rate</span>
      <b-progress :max="1">
        <b-progress-bar :value="hero.summary.win_rate" variant="info" :striped="true">
          <strong>{{ formatPercentage(hero.summary.win_rate) }}</strong>
        </b-progress-bar>
        <b-progress-bar :value="1-hero.summary.win_rate" variant="secondary"/>
      </b-progress>
      <span>
        Wins
        <small>as a ratio to the other heroes</small>
      </span>
      <b-progress :max="1">
        <b-progress-bar :value="hero.summary.wins" variant="info" :striped="true">
          <strong>{{ formatPercentage(hero.summary.wins) }}</strong>
        </b-progress-bar>
        <b-progress-bar :value="1-hero.summary.wins" variant="secondary"/>
      </b-progress>

      <h5>Attributes</h5>
      <p>As a ratio compared to the other heroes</p>
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
      <h5>Abilities</h5>
      <p>...</p>
      <ul class="list-group">
        <li class="list-group-item text-center">
          <b-row>
            <b-col cols="2"></b-col>
            <b-col></b-col>
            <b-col>Most Picks</b-col>
            <b-col>Most Wins</b-col>
            <b-col>Win Rate</b-col>
          </b-row>
        </li>
        <template v-for="ability in hero.abilities">
          <li :key="ability.id" class="list-group-item">
            <b-row>
              <b-col cols="2">
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
                  :value="ability.picks_percentage"
                  :min="0"
                  :max="1"
                  :striped="true"
                ></b-progress>
              </b-col>
              <b-col>
                <b-progress
                  variant="warning"
                  height="1.5rem"
                  :value="ability.wins_percentage"
                  :min="0"
                  :max="1"
                  :striped="true"
                ></b-progress>
              </b-col>
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
              <b-col cols="2">
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
                  :value="ability.picks_percentage"
                  :min="0"
                  :max="1"
                  :striped="true"
                ></b-progress>
              </b-col>
              <b-col>
                <b-progress
                  variant="warning"
                  height="1.5rem"
                  :value="ability.wins_percentage"
                  :min="0"
                  :max="1"
                  :striped="true"
                ></b-progress>
              </b-col>
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

      <h5>Talents</h5>
      <p>...</p>
      <b-row>
         <template v-for="talent in hero.talents">
          <b-col :key="talent.key" cols="6">
            <span :title="talent.description">{{talent.name}}</span>
            <br />
            <b-progress :value="talent.win_rate" :min="0" :max="1" show-progress></b-progress>
          </b-col>
        </template>
      </b-row>

      <h5>Combos - Abilities</h5>
      <p>...</p>
      <b-table 
          :fields="['image', 'name', 'has_upgrade', 'picks', 'wins', 'win_rate']"
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
            <b-progress variant="warning" height="1.5rem" :value="row.item.wins" :min="0" :max="100" :striped="true"></b-progress>
          </template>
          <template slot="picks" slot-scope="row">
            <b-progress variant="info" height="1.5rem" :value="row.item.picks" :min="0" :max="100" :striped="true"></b-progress>
          </template>
          <template slot="has_upgrade" slot-scope="row">
            <span v-if="row.item.has_upgrade" class="badge badge-success">Yes</span>
            <span v-else class="badge badge-secondary">No</span>
          </template>
        </b-table>

      <h5>Combos - Ultimates</h5>
      <p>...</p>
      <b-table 
          :fields="['image', 'name', 'has_upgrade', 'picks', 'wins', 'win_rate']"
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
            <b-progress variant="warning" height="1.5rem" :value="row.item.wins" :min="0" :max="100" :striped="true"></b-progress>
          </template>
          <template slot="picks" slot-scope="row">
            <b-progress variant="info" height="1.5rem" :value="row.item.picks" :min="0" :max="100" :striped="true"></b-progress>
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
    }
  },
  watch: {
    $route(to, from) {
      this.hero = heroes[to.params.id];
    }
  }
};
</script>