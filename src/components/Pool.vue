<template>
  <section v-if="ready">
    <b-row>
      <b-col>
        <h2 class="text-center text-warning">Draft Pool</h2>
      </b-col>
    </b-row>
    <hr class="highlighted" />
    <b-row>
      <b-col>
        
        <h5>Is A Hero or Ability in AD?</h5>
        <b-form-input v-model="filter" placeholder="" />
        <br />
        <p>
          Find out exactly which heroes and which abilities are active. 
          Below is a list of heroes and abilities that make up the draft pool the active ones are in colour while the disabled ones are greyed out.
        </p>
      </b-col>
      <b-col cols="4">
        <div class="text-center">
          <b-alert variant="info" show>
            <strong>Cluckles Says</strong><br />
            <p>Only 3 Heroes left out! We feel bad for you Morphling, Invoker and Earth Spirit...</p>
          </b-alert>
          <b-img src="/static/images/cluckles-speach.png" class="cluckles-speach" />
        </div>
      </b-col>
    </b-row>
    <hr class="highlighted" />
    <b-row>
      <b-col>
        <table class="table  b-table">
          <tbody>
            <template v-for="hero in computedItems">
              <tr v-bind:item="hero" v-bind:key="hero.id">
                <td>
                  <!--
                  <b-link :to="'/hero/' + hero.id">
                    <b-img :src="ability.img" :title="ability.name" v-bind:class="{ disabled: !ability.enabled }" class="ability-icon-md" />
                  </b-link>
                  -->
                  <b-img :src="ability.img" :title="ability.name" v-bind:class="{ disabled: !ability.enabled }" class="ability-icon-md" />
                </td>
                <template v-for="ability in hero.abilities">
                  <td v-bind:key="ability.id">
                    <b-img :src="ability.img" :title="ability.name" v-bind:class="{ disabled: !ability.enabled }" class="ability-icon-md" />
                    <!--
                    <b-link :to="'/ability/' + ability.id" v-if="ability.enabled">
                      <b-img :src="ability.img" :title="ability.name" v-bind:class="{ disabled: !ability.enabled }" class="ability-icon-md" />
                    </b-link>
                    <b-img v-else :src="ability.img" :title="ability.name" v-bind:class="{ disabled: !ability.enabled }" class="ability-icon-md" />
                    -->
                  </td>
                </template>
              </tr>
            </template>
          </tbody>
        </table>
      </b-col>
    </b-row>
  </section>
  <section v-else class="text-center">
    <hgv-loader :color="'#ffc107'"></hgv-loader>
  </section>
</template>

<script>
import axios from 'axios'

export default {
  name: 'DraftPool',
  data () {
    return {
      'ready': false,
      'filter': null,
      'items': []
    }
  },
  created () {
    const vm = this

    let p1 = axios.get('/static/data/pool.json').then((reponse) => { return reponse.data })
    Promise.all([p1]).then((values) => {
      vm.items = values[0]
      vm.ready = true
    }).catch(function () {
      vm.$router.push('/error')
    })
  },
  computed: {
    computedItems () {
      let items = this.items

      if (this.filter) {
        items = items.filter((hero) => {
          const abilties = hero.abilities.filter((ability) => { return ability.name.toLowerCase().includes(this.filter.toLowerCase()) })
          return abilties.length > 0 || hero.name.toLowerCase().includes(this.filter.toLowerCase())
        })
      }

      return items
    }
  }
}
</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped>

</style>
