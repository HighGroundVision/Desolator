<template>
  <section v-if="ready">
      <b-row>
        <b-col cols="8">
          <h2 class="text-warning">Draft Pool</h2>
          <p>
            Find out exactly which heroes and which abilities are active. 
            Below is a list of heroes and abilities that make up the draft pool the active ones are in colour while the disabled ones are greyed out.
          </p>
          <p>
            The pool of available heroes and abilities has never been greater than with the 7.07 patch. 
            With only 10 heroes left out of the draft, there are a lot of options to work with.
          </p>
        </b-col>
        <b-col>
          <div class="text-center">
            <b-alert variant="info" show>
              <strong>Cluckles Says</strong><br />
              <p>With all the new additions it gives hope that one day all heroes will be included!</p>
            </b-alert>
            <b-img src="/static/images/cluckles-speach.png" class="cluckles-speach" />
          </div>
        </b-col>
      </b-row>
      <b-row>
        <b-col cols="6">
          <b-form-input v-model="filter" placeholder="Filter by Hero or Ability"  />
        </b-col>
      </b-row>
      <br />
      <b-row>
        <b-col>
          <table class="table table-striped b-table">
            <tbody>
              <template v-for="hero in computedItems">
                <tr v-bind:item="hero" v-bind:key="hero.id">
                  <td>
                    <b-link :to="'/hero/' + hero.id">
                      <b-img :src="hero.img" :title="hero.name" v-bind:class="{ disabled: !hero.enabled }" class="hero-icon-banner" />
                    </b-link>
                  </td>
                  <template v-for="ability in hero.abilities">
                    <td v-bind:key="ability.id">
                      <b-link :to="'/ability/' + ability.id" v-if="ability.enabled">
                        <b-img :src="ability.img" :title="ability.name" v-bind:class="{ disabled: !ability.enabled }" class="ability-icon-md" />
                      </b-link>
                      <b-img v-else :src="ability.img" :title="ability.name" v-bind:class="{ disabled: !ability.enabled }" class="ability-icon-md" />
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
