<template>
  <section class="opaque-background">
      <b-row>
        <b-col cols="8">
          <h1 class="text-warning">Draft Pool</h1>
          <p>Find out exactly which heroes and which abilities are active. Below are listed the heroes and abilities that make up the draft pool. The active heroes and abilities are in colour while the disabled ones are greyed out.</p>
        </b-col>
        <b-col>
          <div class="text-center">
            <b-img src="/static/images/cluckles.png" fluid />
            <b-alert variant="info" show>
              <strong>Drafting Changes!</strong><br />
              <p>With all the new additions it gives hope that one day all heroes will be included.</p>
            </b-alert>
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
          <table id="roaster" class="table table-striped b-table">
            <tbody>
              <template v-for="hero in computedItems">
                <tr v-bind:item="hero" v-bind:key="hero.id">
                  <td>
                    <b-img :src="hero.img" :title="hero.name" v-bind:class="{ disabled: !hero.enabled }" fluid class="hero-icon" />
                  </td>
                  <template v-for="ability in hero.abilities">
                    <td v-bind:key="ability.id">
                      <b-link :to="'/stats/ability/' + ability.id" v-if="ability.enabled">
                        <b-img :src="ability.img" :title="ability.name" v-bind:class="{ disabled: !ability.enabled }" fluid class="ability-icon-lg" />
                      </b-link>
                      <b-img v-else :src="ability.img" :title="ability.name" v-bind:class="{ disabled: !ability.enabled }" fluid class="ability-icon-lg" />
                    </td>
                  </template>
                </tr>
              </template>
            </tbody>
          </table>
        </b-col>
      </b-row>
  </section>
</template>

<script>
import axios from 'axios'

export default {
  name: 'DraftPool',
  data () {
    return {
      'filter': null,
      'items': []
    }
  },
  created: function () {
    const self = this

    let p1 = axios.get('/static/data/draftpool.json').then((reponse) => { return reponse.data })
    Promise.all([p1]).then((values) => {
      const pool = values[0]

      pool.sort((lhs, rhs) => {
        return lhs.name.localeCompare(rhs.name)
      })

      self.items = pool
    }).catch(function (error) {
      console.log(error)
    })
  },
  computed: {
    computedItems: function () {
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
.hero-icon {
    height: 64px;
}
img.disabled {
    -webkit-filter: grayscale(100%);
    -moz-filter: grayscale(100%);
    -o-filter: grayscale(100%);
    -ms-filter: grayscale(100%);
    filter: grayscale(100%);
}
</style>
