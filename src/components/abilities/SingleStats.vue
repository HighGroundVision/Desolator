<template>
  <section>
    <div v-if="loading" class="opaque-background">
      <div class="col-2 offset-5 ">
        <pacman-loader :loading="loading" :color="color" ></pacman-loader>
      </div>
    </div>
    <div v-else class="opaque-background">
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
        <div class="row">
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
            <div class="col-12">
                <h2>Range Heroes</h2>
                <div class="row">
                    <div class="col-4">
                        Pick Rate
                    </div>
                    <div class="col-2">
                        {{round(range.pickRate)}} %
                    </div>
                    <div class="col-2">
                        {{range.picks}} / {{range.total}}
                    </div>
                    <div class="col-4">
                        <b-progress :value="round(range.pickRate)" :min="0" :max="100"></b-progress>
                    </div>
                </div>
                <div class="row">
                    <div class="col-4">
                        Win Rate
                    </div>
                    <div class="col-2">
                        {{round(range.winRate)}} %
                    </div>
                    <div class="col-2">
                        {{range.wins}} / {{range.picks}}
                    </div>
                    <div class="col-4">
                        <b-progress :value="round(range.winRate)" :min="0" :max="100"></b-progress>
                    </div>
                </div>
            </div>
        </div>
    </div>
  </section>
</template>

<script>
import abilities from '@/data/abilities.json'

export default {
  name: 'SingleStats',
  created () {
    var url = process.env.API_BASE_URL + '/RequestStats?key=' + this.$route.params.key
    this.$http.get(url).then((response) => {
      if (!response.ok) {
        return
      }

      var data = response.body
      this.ability = abilities[data.abilities[0]]
      this.melee = data.melee
      this.range = data.range
      this.loading = false
    })
  },
  data () {
    return {
      'color': '#007bff',
      'loading': true,
      'ability': {},
      'melee': {},
      'range': {}
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
