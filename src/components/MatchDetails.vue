<template>
  <section v-if="ready" class="opaque-background">
    <b-row>
      <b-col>

      </b-col>
    </b-row>
  </section>
  <section v-else class="opaque-background text-center">
    <h1>Loading...</h1>
    <hgv-loader :color="'#ffc107'"></hgv-loader>
  </section>
</template>

<script>
import axios from 'axios'
// import * as moment from 'moment'
import { mapGetters } from 'vuex'

export default {
  name: 'MatchHistory',
  data () {
    return {
      'ready': false,
      'items': []
    }
  },
  created () {
    const vm = this

    const matchId = vm.$route.params.key
    let p1 = axios.get(process.env.API_BASE_URL + 'MatchDetails?match=' + matchId).then((reponse) => { return reponse.data })

    Promise.all([p1]).then((values) => {
      const match = values[0]
      console.log(match)
      
      vm.ready = true
    }).catch(function (error) {
      console.log(error)
    })
  },
  methods: {
  },
  computed: mapGetters(['isAnonymous', 'dotaId'])
}
</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped>
</style>
