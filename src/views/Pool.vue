<template>
  <hgv-loading :urls="urls" v-on:loaded="loaded">
    <b-row>
      <b-col>
        <h4  class="text-center">Draft Pool</h4>
        <hr class="highlighted" />
        <div>
          <h5>Unavailable Heroes</h5>
          <ul class="list-unstyled">
            <template v-for="(value) in heroes">
              <li :key="value.id">
                <b-img :src="value.image"  class="hero-icon-banner-sm" />
                <b class="text-info"> {{ value.name }}</b>
              </li>
            </template>
          </ul>
          <p>
            Each of these heroes is unlikely to make it into AD in the future as they bring very little to pool 
            or they where in the pool and have been removed because there abilities interactions are to unstable.
          </p>
        </div>
        <b-row>
          <b-col>
            <h5>Unavailable Abilities</h5>
            <ul class="list-unstyled">
              <template v-for="(value) in abilities">
                <li :key="value.ability.id">
                  <b-img :src="value.ability.image" class="ability-icon-sm" />
                  <span> {{ value.ability.name }}</span>
                  <sup> {{ value.reason }}</sup>
                </li>
              </template>
            </ul>
          </b-col>
          <b-col>
            <h5>Unavailable Ultimates</h5>
            <ul class="list-unstyled">
              <template v-for="(value) in ultimates">
                <li :key="value.ability.id">
                  <b-img :src="value.ability.image" class="ability-icon-sm" />
                  <span> {{ value.ability.name }}</span>
                  <sup> {{ value.reason }}</sup>
                  <sup v-if="value.ability.has_data" >1</sup>
                </li>
              </template>
            </ul>
          </b-col>
          <b-col>
            <h5>Unavailable Upgrades <img src="https://hgv-hyperstone.azurewebsites.net/items/ultimate_scepter.png" class="ability-icon-xs" /></h5>
            <ul class="list-unstyled">
              <template v-for="(value) in upgrades">
                <li :key="value.ability.id">
                  <b-img :src="value.ability.image" class="ability-icon-sm" />
                  <span> {{ value.ability.name }}</span>
                  <sup> {{ value.reason }}</sup>
                </li>
              </template>
            </ul>
          </b-col>
        </b-row>
        <div style="font-size: 0.8em;">
          <ul class="list-unstyled">
            <li><small>1</small> Spells are disabled in the game data but we have collected data for it, this means Valve fucked up and ability is more and likly is (or was) active.</li>
            <li><small>2</small> Disabled for balance reasons.</li>
            <li><small>3</small> Spells which require more than one ability slot are disabled because of hotkey issues.</li>
            <li><small>4</small> Spells which switch between melee and ranged for are all disabled for technical reasons.</li>
            <li><small>5</small> Spells which have direct synergy/interaction with other spells are disabled, as they are deemed too limiting.</li>
            <li><small>6</small> Some spells granted by <b class="text-info">Aghanim's Scepter</b> are disabled because they add spells that use additional slots. REF #3</li>
            <li><small>7</small> According to game files infest does not grant control, this causes infest to be sort of useless</li>
            <li><small>8</small> Your guess is as good as mine...</li>
          </ul>
        </div>
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
        <div>
          <h5>Upgrades <img src="https://hgv-hyperstone.azurewebsites.net/items/ultimate_scepter.png" class="ability-icon-xs" /></h5>
          <ul class="list-unstyled">
            <li>
              <img src="https://hgv-hyperstone.azurewebsites.net/heroes/banner/npc_dota_hero_nyx_assassin.png" class="hero-icon-banner-sm" /><b class="text-info"> Nyx Assassin</b> when acquiring Aghanim's Scepter will still be grant  <b class="text-info">Burrow</b>, regardless of what spells he has.
            </li>
            <li>
              <img src="https://hgv-hyperstone.azurewebsites.net/heroes/banner/npc_dota_hero_treant.png" class="hero-icon-banner-sm" /> <b class="text-info"> Treant Protector</b> when acquiring Aghanim's Scepter will still be grant  <b class="text-info">Eyes In The Forest</b>, regardless of what spells he has.
            </li>
            <li>
              <img src="https://hgv-hyperstone.azurewebsites.net/heroes/banner/npc_dota_hero_gyrocopter.png" class="hero-icon-banner-sm" /> <b class="text-info">Gyrocopter</b> when acquiring Aghanim's Scepter will still be grant  <b class="text-info">Side Gunner</b> only if Gryo also has  <b class="text-info">Flak Cannon</b>.
            </li>
          </ul>
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
      'heroes': [],
      'abilities': [],
      'ultimates': [],
      'upgrades': [],
    }
  },
  methods: {
    loaded(data) {
      let pool = data[0];
      let reasons = data[1];
      let skills = pool.filter(_ => _.enabled == true).map(_ => _.abilities).flat().filter(_ => _.enabled == false);

      this.heroes = pool.filter(_ => _.enabled == false);;
      this.abilities = skills.filter(_ => _.is_ultimate == false && _.has_upgrade == false).map(_ => { return {ability: _, reason: reasons[_.id]}; });
      this.ultimates = skills.filter(_ => _.is_ultimate == true).map(_ => { return {ability: _, reason: reasons[_.id]}; });
      this.upgrades = skills.filter(_ => _.has_upgrade == true &&  _.is_ultimate == false).map(_ => { return {ability: _, reason: reasons[_.id]}; });
    }
  }
}
</script>