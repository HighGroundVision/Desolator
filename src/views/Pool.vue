<template>
  <hgv-loading :urls="urls" v-on:loaded="loaded">
    <b-row>
      <b-col>
        <h4  class="text-center">Draft Pool</h4>
        <p>
            We have complied a list of all the heroes and abilities that are enabled in AD.
            Some abilities of heroes are <b-badge class="bg-secondary text-white">Disabled</b-badge> and highlighted as such, this includes scepter upgrades.
            Abilities marked by <img class="ability-icon-xs" src="@/assets/imgs/aghanims_scepter.svg" /> are upgraded or granted by Aghanim's Scepter.
        </p>
        <hr class="highlighted" />
        <div>
          <h5>Enabled Heroes</h5>
          <b-row>
            <template v-for="(hero) in pool" >
              <b-col cols="12" sm="6" md="6" lg="4" xl="3" class="p-1 m-0" :key="hero.id" v-if="hero.enabled">
                <div class="card m-1">
                  <img class="card-img-top" :src="hero.image" :alt="hero.name">
                  <div class="card-body">
                    <h5 class="card-title">{{hero.name}}</h5>
                    <p class="card-text">{{getHeroText(hero.id)}}</p>
                  </div>
                  <ul class="list-group list-group-flush">
                    <template v-for="(ability) in hero.abilities">
                      <li :key="ability.id" class="list-group-item" v-bind:class="getAbilityHighlight(ability)">
                        <img v-if="ability.has_upgrade" class="ability-icon-sm float-right" src="@/assets/imgs/aghanims_scepter.svg" />
                        <img class="ability-icon-sm" :src="ability.image" />
                        <span> {{ability.name}}</span>
                      </li>
                    </template>
                  </ul>
                  <div class="card-body text-center">
                    <a href="#" class="card-link">Details</a>
                  </div>
                </div>
              </b-col>
            </template>
          </b-row>
        </div>
        <hr class="highlighted" />
        <div>
          <h5>Disabled Heroes</h5>
          <ul class="list-inline">
            <b-row>
              <template v-for="(hero) in pool" >
                <b-col cols="12" sm="6" md="6" lg="4" xl="3" class="p-1 m-0"  :key="hero.id" v-if="!hero.enabled">
                  <div class="card">
                    <img class="card-img-top" :src="hero.image" :alt="hero.name">
                    <div class="card-body">
                      <h5 class="card-title">{{hero.name}}</h5>
                    </div>
                    <ul class="list-group list-group-flush">
                      <template v-for="(ability) in hero.abilities">
                        <li :key="ability.id" class="list-group-item bg-secondary">
                          <img v-if="ability.has_upgrade" class="ability-icon-sm float-right" src="@/assets/imgs/aghanims_scepter.svg" />
                          <img class="ability-icon-sm" :src="ability.image" />
                          <span> {{ability.name}}</span>
                        </li>
                      </template>
                    </ul>
                  </div>
                </b-col>
              </template>
            </b-row>
          </ul>
        </div>
      </b-col>
    </b-row>
  </hgv-loading>
</template>

<script>
import axios from 'axios'

export default {
  name: 'pool',
  data () {
    return {
      'urls': ['/static/draft-pool.json'],
      'pool': [],
    }
  },
  methods: {
    loaded(data) {
      this.pool = data[0];
    },
    getAbilityHighlight(ability) {
      if(ability.enabled || ability.has_data == true) {
        return "bg-light";
      } else {
        return "bg-secondary";
      }
    },
    getHeroText(id) {
      switch (id) {
        case 3: 
          return "Bane can attack Nightmared units";
        case 73:
          return "Alchemist can infuse allies with Aghanim's Scepter";
        case 75:
          return "Silencer's innate Intelligence Steal is enabled";
        case 41:
          return "Faceless Void's is immunity to Chronosphere.";
        case 119:
          return "Dark Willow's Bedlam & Terrorize are paired."
        default:
          return "";
      }
    }
  }
}
</script>

