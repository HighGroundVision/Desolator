<template>
  <section class="opaque-background">
    <div v-if="loading">
      <b-row>
        <b-col class="col-2 offset-5">
          
        </b-col>
      </b-row>
      <div class="col-2 offset-5 ">
        <pacman-loader :loading="loading" :color="color" ></pacman-loader>
      </div>
    </div>
    <div v-else>
        <div class="row">
            <div class="col-12">
                <h1>{{ability.dname}}</h1>
            </div>
        </div>
        <hr />
        <div class="row">
          <div class="col-2">
              <b-img :src="ability.img" :title="ability.dname" fluid class="ability-icon" />
          </div>
          <div class="col-10">
              {{ability.desc}}
          </div>
        </div>
        <hr />
        <div class="row" >
            <div class="col-12">
                <h2>Melee Heroes</h2>
                <div class="row">
                    <div class="col-4">
                        Pick Rate
                    </div>
                    <div class="col-2">
                        {{round(melee.pickRate)}} %
                    </div>
                    <div class="col-2">
                        {{melee.picks}} / {{range.total}}
                    </div>
                    <div class="col-4">
                        <b-progress :value="round(melee.pickRate)" :min="0" :max="100"></b-progress>
                    </div>
                </div>
                <div class="row">
                    <div class="col-4">
                        Win Rate
                    </div>
                    <div class="col-2">
                        {{round(melee.winRate)}} %
                    </div>
                    <div class="col-2">
                        {{melee.wins}} / {{range.picks}}
                    </div>
                    <div class="col-4">
                        <b-progress :value="round(melee.winRate)" :min="0" :max="100"></b-progress>
                    </div>
                </div>
            </div>
        </div>
    </div>
  </section>
</template>

<script>
import abilities from '@/data/abilities.json'
import db from '@/data/db-extract-ability.json'

export default {
  name: 'SingleStats',
  created () {
    // var url = process.env.API_BASE_URL + '/RequestStats?key=' + this.$route.params.key

    const id = parseInt(this.$route.params.key)
    this.ability = abilities[id]
    this.stats = db.filter(stat => stat.abilities === id)

    this.loading = false
  },
  data () {
    return {
      'color': '#007bff',
      'loading': true,
      'ability': {},
      'stats': []
    }
  },
  methods: {
    round: function (stat) {
      return Math.round(stat * 100)
    }
  }
}
</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped>
.ability-icon {
    height: 120px;
}
</style>
