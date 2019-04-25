<template>
  <hgv-loading :urls="urls" v-on:loaded="loaded">
    <b-row v-if="hero">
      <b-col>
        <h4 class="text-center">{{hero.details.name}}</h4>
        <b-card bg-variant="light">
        <b-row>
          <b-col cols="4">
            <b-img :src="hero.details.image" fluid-grow />
          </b-col>
          <b-col cols="8">
              <b-row>
                <b-col>
                  <span>Kda</span>
                </b-col>
                <b-col>
                  {{formatDecimal(hero.summary.kda)}}
                </b-col>
              </b-row>
              <b-row>
                <b-col>
                  <span>Kills</span>
                </b-col>
                <b-col>
                  {{formatNumber(hero.summary.kills)}}
                </b-col>
              </b-row>
              <b-row>
                <b-col>
                  <span>Picks</span>
                </b-col>
                <b-col>
                  {{formatNumber(hero.summary.picks)}}
                </b-col>
              </b-row>
              <b-row>
                <b-col>
                  <span>Wins</span>
                </b-col>
                <b-col>
                  {{formatNumber(hero.summary.wins)}}
                </b-col>
              </b-row>
              <b-row>
                <b-col>
                  <span>Win Rate</span>
                </b-col>
                <b-col>
                  <b-progress :max="1" :value="hero.summary.win_rate" variant="primary" :striped="true" show-progress></b-progress>
                </b-col>
              </b-row>
          </b-col>
        </b-row>
        </b-card>
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
                    <span>{{hero.attributes.attribute_base_strength.value}}</span>
                  </b-col>
                  <b-col>
                    <b-progress
                      variant="dark"
                      :value="hero.attributes.attribute_base_strength.rank"
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
                    <span>{{hero.attributes.attribute_strength_gain.value}}</span>
                  </b-col>
                  <b-col>
                    <b-progress
                      variant="dark"
                      :value="hero.attributes.attribute_strength_gain.rank"
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
                    <span>{{hero.attributes.attribute_base_agility.value}}</span>
                  </b-col>
                  <b-col>
                    <b-progress
                      variant="dark"
                      :value="hero.attributes.attribute_base_agility.rank"
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
                    <span>{{hero.attributes.attribute_agility_gain.value}}</span>
                  </b-col>
                  <b-col>
                    <b-progress
                      variant="dark"
                      :value="hero.attributes.attribute_agility_gain.rank"
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
                    <span>{{hero.attributes.attribute_base_intelligence.value}}</span>
                  </b-col>
                  <b-col>
                    <b-progress
                      variant="dark"
                      :value="hero.attributes.attribute_base_intelligence.rank"
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
                    <span>{{hero.attributes.attribute_intelligence_gain.value}}</span>
                  </b-col>
                  <b-col>
                    <b-progress
                      variant="dark"
                      :value="hero.attributes.attribute_intelligence_gain.rank"
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
                    <span>{{hero.attributes.status_health.value}}</span>
                  </b-col>
                  <b-col>
                    <b-progress
                      variant="dark"
                      :value="hero.attributes.status_health.rank"
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
                    <span>{{hero.attributes.status_health_regen.value}}</span>
                  </b-col>
                  <b-col>
                    <b-progress
                      variant="dark"
                      :value="hero.attributes.status_health_regen.rank"
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
                    <span>{{hero.attributes.status_mana.value}}</span>
                  </b-col>
                  <b-col>
                    <b-progress
                      variant="dark"
                      :value="hero.attributes.status_mana.rank"
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
                    <span>{{hero.attributes.status_mana_regen.value}}</span>
                  </b-col>
                  <b-col>
                    <b-progress
                      variant="dark"
                      :value="hero.attributes.status_mana_regen.rank"
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
                      v-if="hero.details.attack_capabilities === 'DOTA_UNIT_CAP_MELEE_ATTACK'"
                      title="Attack Type"
                    >Melee</span>
                    <span
                      class="invert"
                      v-if="hero.details.attack_capabilities === 'DOTA_UNIT_CAP_RANGED_ATTACK'"
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
                    <span>{{hero.attributes.attack_range.value}}</span>
                  </b-col>
                  <b-col>
                    <b-progress
                      variant="dark"
                      :value="hero.attributes.attack_range.rank"
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
                    <span>{{hero.attributes.attack_damage_min.value}} - {{hero.attributes.attack_damage_max.value}}</span>
                  </b-col>
                  <b-col>
                    <b-progress
                      variant="dark"
                      :value="hero.attributes.attack_damage_max.rank"
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
                    <span>{{hero.attributes.movement_speed.value}}</span>
                  </b-col>
                  <b-col>
                    <b-progress
                      variant="dark"
                      :value="hero.attributes.movement_speed.rank"
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
                    <span>{{hero.attributes.movement_turn_rate.value}}</span>
                  </b-col>
                  <b-col>
                    <b-progress
                      variant="dark"
                      :value="hero.attributes.movement_turn_rate.rank"
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
                    <span>{{hero.attributes.vision_daytime_range.value}}</span>
                  </b-col>
                  <b-col>
                    <b-progress
                      variant="dark"
                      :value="hero.attributes.vision_daytime_range.rank"
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
                    <span>{{hero.attributes.vision_nighttime_range.value}}</span>
                  </b-col>
                  <b-col>
                    <b-progress
                      variant="dark"
                      :value="hero.attributes.vision_nighttime_range.rank"
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
              <b-col cols="1">Picks</b-col>
              <b-col cols="1">Wins</b-col>
              <b-col>Win Rate</b-col>
              <b-col cols="1">Kills</b-col>
              <b-col cols="1">KDA</b-col>
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
                <b-col cols="1">{{formatNumber(ability.picks)}}</b-col>
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
                <b-col cols="1">{{formatNumber(ability.kills)}}</b-col>
                <b-col cols="1">{{formatDecimal(ability.kda)}}</b-col>
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
                <b-col cols="1">{{formatNumber(ability.picks)}}</b-col>
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
                 <b-col cols="1">{{formatNumber(ability.kills)}}</b-col>
                <b-col cols="1">{{formatDecimal(ability.kda)}}</b-col>
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
        <b-card bg-variant="light">
          <template v-for="talent in hero.talents">
            <b-row :key="talent.level">
              <b-col>
                <b v-bind:class="{ 'text-info': talent.lhs.key.includes('unique') }" :title="talent.lhs.description">{{talent.lhs.name}}</b>
                <br />
                <b-progress :value="talent.lhs.win_rate" :min="0" :max="1" show-progress></b-progress>
              </b-col>
              <b-col cols="2">
                <h5 class="text-center"><b-badge>{{talent.level}}</b-badge></h5>
              </b-col>
              <b-col>
                <b v-bind:class="{ 'text-info': talent.rhs.key.includes('unique') }" :title="talent.rhs.description">{{talent.rhs.name}}</b>
                <br />
                <b-progress :value="talent.rhs.win_rate" :min="0" :max="1" show-progress></b-progress>
              </b-col>
            </b-row>
          </template>
        </b-card>
        
        <br />

        <h4 class="text-center">Combos</h4>
        <p>
          We have included the top Ability and Ultimate Combos for this hero.
          These skills are ordered by KDA.
          We only show 10 at a time but you can page through the table to find the ability you are interested in.
        </p>
        <h5 class="text-center">Abilities</h5>
        <b-card bg-variant="light">
        <b-table 
            :fields="['image', 'name', 'kda', 'kills', 'picks', 'wins', 'win_rate']"
            :items="hero.combo_abilities"
            :per-page="page.size"
            :current-page="page.abilities"
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
              <span>{{formatNumber(row.item.wins)}}</span>
            </template>
            <template slot="kda" slot-scope="row">
              <span>{{formatDecimal(row.item.kda)}}</span>
            </template>
            <template slot="kills" slot-scope="row">
              <span>{{formatNumber(row.item.kills)}}</span>
            </template>
            <template slot="picks" slot-scope="row">
              <span>{{formatNumber(row.item.picks)}}</span>
            </template>
          </b-table>
        </b-card>
        <br />
        <b-pagination v-model="page.abilities" :total-rows="hero.combo_abilities.length" :per-page="page.size" align="center"></b-pagination>

        <h5 class="text-center">Ultimates</h5>
        <b-card bg-variant="light">
        <b-table 
            :fields="['image', 'name', 'kda',  'kills', 'picks', 'wins', 'win_rate']"
            :items="hero.combo_ultimates"
            :per-page="page.size"
            :current-page="page.ultimates"
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
              <span>{{formatNumber(row.item.wins)}}</span>
            </template>
            <template slot="kda" slot-scope="row">
              <span>{{formatDecimal(row.item.kda)}}</span>
            </template>
            <template slot="kills" slot-scope="row">
              <span>{{formatNumber(row.item.kills)}}</span>
            </template>
            <template slot="picks" slot-scope="row">
              <span>{{formatNumber(row.item.picks)}}</span>
            </template>
          </b-table>
        </b-card>
        <br />
        <b-pagination v-model="page.ultimates" :total-rows="hero.combo_ultimates.length" :per-page="page.size" align="center"></b-pagination>

        <h5 class="text-center">Ability Groups</h5>
        <p>
          It is more important to gain a sense of which types of abilities from <b class="text-info">Speed</b>, <b class="text-info">Damage</b>, <b class="text-info">Tank</b>, <b class="text-info">Stuns</b>, <b class="text-info">Escape</b> etc. give your hero the best chance of success.
          As the draft often moves quickly and changes with each pick, locking your sights in on one ability or combo early on is one the sins of amateur Drafter.
          Knowing your archetypes will help you adapt quickly in a draft.
        </p>
        <b-card bg-variant="light">
          <b-table small
            :fields="['keyword', 'win_rate']"
            :items="hero.ability_groups" 
            >
            <template slot="keyword" slot-scope="row">
              <b v-if="row.item.win_rate > 0.505" class="text-success">{{row.item.keyword}}</b>
              <b v-else-if="row.item.win_rate < 0.495"  class="text-warning">{{row.item.keyword}}</b>
              <b v-else class="text-info" >{{row.item.keyword}}</b>
            </template>
            <template slot="win_rate" slot-scope="row">
              <b-progress :value="row.item.win_rate" :min="0" :max="1" :striped="true" show-progress></b-progress>
            </template>
          </b-table>
        </b-card>

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
  </hgv-loading>
</template>

<script>
export default {
  name: "hero",
  data() {
    let id = this.$route.params.id;
    return {
      'urls': [`/static/hero.${id}.json`],
      'hero': null,
      'page': {
        'size': 10,
        'abilities': 1,
        'ultimates': 1,
      }
    };
  },
  methods: {
    loaded(data) {
      var self = this;
      this.hero = data[0];
    }
  },
  /*
  watch: {
    $route(to, from) {
      let id = to.params.id;
      this.urls = ['/static/hero.' + id + '.json'];
    }
  }
  */
};
</script>
