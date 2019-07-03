<template>
  <hgv-loading :urls="urls" v-on:loaded="loaded">
    <b-row>
      <b-col>
        <h4  class="text-center">Draft Pool</h4>
        <hr class="highlighted" />
        <div>
          <h5>Innate Skills</h5>
          <ul class="list-unstyled">
            <li>
              <img src="https://hgv-hyperstone.azurewebsites.net/heroes/banner/npc_dota_hero_silencer.png" class="hero-icon-banner-sm" /> <b class="text-info"> Silencer's</b> innate <b class="text-info">Intelligence Steal</b>  is not disabled and works for Silencer. It is not bound to <b class="text-info">Glaives of Wisdom</b>.
            </li>
            <li>
              <img src="https://hgv-hyperstone.azurewebsites.net/heroes/banner/npc_dota_hero_faceless_void.png" class="hero-icon-banner-sm" /> <b class="text-info"> Faceless Void's</b> innate immunity to <b class="text-info">Chronosphere</b> 
            </li>
            <li>
              <img src="https://hgv-hyperstone.azurewebsites.net/heroes/banner/npc_dota_hero_bane.png" class="hero-icon-banner-sm" /> <b class="text-info"> Bane's</b> innate ability to attack <b class="text-info">Nightmared</b> units are not disabled.
            </li>
            <li>
              <img src="https://hgv-hyperstone.azurewebsites.net/heroes/banner/npc_dota_hero_alchemist.png" class="hero-icon-banner-sm" /> <b class="text-info"> Alchemist</b> can still infuse allies with  <b class="text-info">Aghanim's Scepter</b>.
            </li>
          </ul>
        </div>
        <hr class="highlighted" />
        <div>
          <h5>Disabled Heroes</h5>
          <ul class="list-inline">
            <template v-for="(hero) in pool">
              <li :key="hero.id" v-if="!hero.enabled" class="list-inline-item">
                <span>{{hero.name}}</span>
                <br />
                <b-img :src="hero.image" class="hero-icon-profile-md" />
              </li>    
            </template>
          </ul>
        </div>
        
        <hr class="highlighted" />
        <div>
          <h5>Active Heroes, Abilities, and Upgrades</h5>
          <table class="table">
            <thead>
              <tr>
                <th>Hero</th>
                <th></th>
                <th>Ability</th>
                <th></th>
                <th>Upgrade</th>
                <th>Notes</th>
              </tr>
            </thead>
            <tbody>
              <template v-for="(hero) in pool">
                <template v-for="(ability, index) in hero.abilities">
                  <tr :key="ability.id" v-if="hero.enabled">
                    <td>
                      <span v-if="index == 0">{{hero.name}}</span>
                    </td>
                    <td><b-img v-if="index == 0" :src="hero.image" class="hero-icon-profile-md" v-bind:class="{'disabled': !hero.enabled }" /></td>
                    <td>
                      <span v-if="ability.enabled">{{ability.name}}</span>
                    </td>
                    <td><b-img :src="ability.image" class="ability-icon-md" v-bind:class="{'disabled': !ability.enabled }" /></td>
                    <td>
                      <img v-if="ability.has_upgrade" src="https://hgv-hyperstone.azurewebsites.net/items/ultimate_scepter.png" class="ability-icon-md" v-bind:class="{'disabled': !ability.enabled }" />
                    </td>
                    <td>
                      <span v-if="ability.enabled == false && ability.has_data == false">Disabled</span>
                      <span v-if="ability.enabled == false && ability.has_data == true">Game data says that this ability is disabled but we have collected data. It could be enabled or is not reported correctly in the API</span>
                      <span v-if="ability.enabled == true && ability.has_data == false">Game data says that this ability is enabled but we have not collected any data. It could be disbaled in other ways or not reported in the API.</span>
                      <span v-if="hasReason(ability.id)">{{getReason(ability.id)}}</span>
                    </td>
                  </tr>
                </template>              
              </template>
            </tbody>
          </table>
        </div>
        
      </b-col>
      <b-col cols="4" lg="3">
        <div class="text-center">
          <b-alert variant="info" show>
            <span>As of the 7.21b patch there so few heroes left to include it is simpler to talk about what is not enabled and why!</span>
          </b-alert>
          <img src="@/assets/imgs/cluckles-speach.png" class="cluckles-speach" />
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
      'urls': ['/static/draft-pool.json', '/static/draft-pool-reasons.json'],
      'pool': [],
      'reasons': []
    }
  },
  methods: {
    loaded(data) {
      this.pool = data[0];
      this.reasons = data[1];
    },
    hasReason(id) {
      if(this.reasons[id.toString()]) {
        return true;
      } else {
        return false;
      }
    },
    getReason(id) {
      if(this.reasons[id.toString()]) {
        return this.reasons[id.toString()];
      } else {
        return "";
      }
    }
  }
}
</script>
