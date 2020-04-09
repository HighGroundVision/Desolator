<template>
  <div>
      <section class="wrapper header special">
      <div class="inner">
        <h1>Pool</h1>
      </div>
     </section>
     
     <section class="wrapper style2">
        <div class="inner">
          <div>
            <p>
              We have complied a list of all the heroes and abilities that are enabled in AD.
              Some abilities of heroes are <b>Disabled</b> and highlighted as such, this includes scepter upgrades.
              Abilities marked by <img class="ability-icon-xs" src="https://hgv-hyperstone.azurewebsites.net/mics/aghanims_scepter_upgrade.png" /> are upgraded or granted by Aghanim's Scepter.
              Only abilities granted by an ultimate are currently enabled.
              <b style="color: #f6755e;">Class A</b> heroes have four enabled abilities while <b style="color: #25a2c3;">Class B</b> heroes will have less then four. 
              No more then one Class B hero will appear in the draft pool at once limiting the number of potential combinations.
            </p>
          </div>
          <hr />
          <loader :loading="loading">
            <div>
              <h2>Enabled Heroes</h2>
              <div class="row">
                <template v-for="(hero) in pool">
                  <div v-bind:key="hero.id" class="3u 12u$(medium)">
                    <img :src="hero.imageBanner" v-bind:class="{ disabled: !hero.enabled }" style="width:100%; border-radius: 15px 15px 0px 0px;" />
                    <h3 v-if="hero.enabled && hero.primary" class="hero-name" style="background-color: #f6755e; color: white;"><router-link :to="'/hero/'+hero.id">{{hero.name}}</router-link></h3>
                    <h3 v-else-if="hero.enabled" class="hero-name"  style="background-color: #25a2c3; color: white;">{{hero.name}}</h3>
                    <h3 v-else class="hero-name" style="color: #727a82;">{{hero.name}}</h3>
                    <div v-if="hero.enabled" style="height: 250px;">
                      <template v-for="(ability) in hero.abilities">
                        <div v-bind:key="ability.id" class="truncate" style="line-height: 25px; padding: 5px;">
                          <img v-bind:class="{ disabled: !ability.enabled }" class="ability-icon-xs" :src="ability.image" style="border-radius: 5px; vertical-align: middle;"  />
                          <img v-if="ability.hasUpgrade" class="ability-icon-xs" src="https://hgv-hyperstone.azurewebsites.net/mics/aghanims_scepter_upgrade.png" style="vertical-align: middle;" />
                          <img v-if="ability.isGranted" class="ability-icon-xs" src="https://hgv-hyperstone.azurewebsites.net/mics/aghanims_scepter_granted.png" style="vertical-align: middle;" />
                          <span v-bind:class="{ disabled: !ability.enabled, 'ability-ultimate': ability.isUltimate }"  class="ability-name">{{ability.name}}</span>
                        </div>
                      </template>
                    </div>
                  </div>
                </template>
              </div>
            </div>
          </loader>
          
        </div>
      </section>
  </div>
</template>

<script>
import axios from 'axios'

export default {
  name: 'pool',
  data () {  
    return {
      loading: false,
      pool: []
    }
  },
  created() {
    this.loadData()
  },
  methods: {
    async  loadData() {
      this.loading = true
      var response = await axios.get(process.env.VUE_APP_BASE_API + "api/draft/pool")
      this.pool = response.data
      this.loading = false
    }
  }
}
</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped>
hr  {
  color: orange;
  border: 1px  solid orange;
}
img.disabled {
  -webkit-filter: grayscale(100%);
       -moz-filter: grayscale(100%);
         -o-filter: grayscale(100%);
        -ms-filter: grayscale(100%);
            filter: grayscale(100%); 
}
.hero-name {
  position: relative; 
  top: -7px; 
  text-align: center;
}
.ability-name {
  color: #25a2c3; 
  font-weight: bold; 
  padding-left:5px;
  
}
.ability-ultimate {
  color: orange; 
}
span.disabled {
  color: #767d85;
}
.truncate {
  width: 250px;
  white-space: nowrap;
  overflow: hidden;
  text-overflow: ellipsis;
}
.ability-icon-xs
{
  width: 30px;
  height: 30px;
}
</style>

