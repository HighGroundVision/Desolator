<template>
  <section class="opaque-background">
      <div class="row">
        <div class="col-xl-8 col-lg-10">
          <h2 class="text-warning">Draft Pool</h2>
          <p>Find out exactly which heroes and which abilities are active. Below are listed the heroes and abilities that make up the draft pool. The active heroes and abilities are in colour while the disabled ones are greyed out.</p>
        </div>
        <div class="col-xl-4 col-lg-2">
          <div class="text-center">
            <b-img src="/static/images/cluckles.png" fluid />
            <b-alert variant="info" show>
              <strong>Drafting Changes!</strong><br />
              <p>With all the new additions it gives hope that one day all heroes will be included.</p>
            </b-alert>
          </div>
        </div>
      </div>
      <div class="row">
          <div class="col-lg-12">
              <table id="roaster" class="table table-striped b-table">
                <tbody>
                  <template v-for="hero in items">
                    <tr v-bind:item="hero" v-bind:key="hero.id">
                      <td>
                        <b-img :src="hero.img" :title="hero.name" v-bind:class="{ disabled: !hero.enabled }" fluid class="hero-icon" />
                      </td>
                      <template v-for="ability in hero.abilities">
                        <td v-bind:key="ability.id">
                          <b-link :to="'/ability/single/' + ability.id" v-if="ability.enabled">
                            <b-img :src="ability.img" :title="ability.name" v-bind:class="{ disabled: !ability.enabled }" fluid class="ability-icon" />
                          </b-link>
                          <b-img v-else :src="ability.img" :title="ability.name" v-bind:class="{ disabled: !ability.enabled }" fluid class="ability-icon" />
                        </td>
                      </template>
                    </tr>
                  </template>
                </tbody>
              </table>
          </div>
      </div>
  </section>
</template>

<script>
import pool from '@/data/draftpool.json'

export default {
  name: 'DraftPool',
  created: function () {
    pool.sort((lhs, rhs) => {
      if (lhs.name < rhs.name) return -1
      if (lhs.name > rhs.name) return 1
      return 0
    })
  },
  data () {
    return {
      items: pool
    }
  }
}
</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped>
.ability-icon {
    height: 90px;
}
.hero-icon {
    height: 90px;
}
img.disabled {
    -webkit-filter: grayscale(100%);
    -moz-filter: grayscale(100%);
    -o-filter: grayscale(100%);
    -ms-filter: grayscale(100%);
    filter: grayscale(100%);
}
</style>
