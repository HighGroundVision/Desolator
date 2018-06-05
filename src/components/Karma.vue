<template>
  <section v-if="ready">
    <div style="float: right;">
      <i class="far fa-question-circle" :title="help"></i> 
    </div>
    <div class="text-center">
      <b-img src="/static/images/cluckles.png" />
      <b-alert variant="info" show>
        <b-row>
          <b-col>
            <strong>Cluckles Says</strong>
            <br />
            <span>Get involved and vote!</span>
          </b-col>
        </b-row>
        <hr />
        <b-row>
          <b-col>
            <b-row>
              <b-col style="font-size: 1.3em;">
                <span>Karma</span>
                <br />
                <span>{{ranking.total}}</span>
              </b-col>
            </b-row>
          </b-col>
          <b-col>
            <b-row>
              <b-col>
                <b-button variant="success" @click="upVote">
                  <i class="fas fa-arrow-up"></i> {{ranking.up}}
                </b-button>
              </b-col>
            </b-row>
            <b-row>
              <b-col>
                <b-button variant="danger" @click="downVote">
                  <i class="fas fa-arrow-down"></i> {{ranking.down}}
                </b-button>
              </b-col>
            </b-row>
          </b-col>
        </b-row>
      </b-alert>
    </div>
  </section>
  <section v-else>
    <hgv-loader :color="'#ffc107'"></hgv-loader>
  </section>
</template>

<script>
import axios from 'axios'
import { voteAbility } from '@/assets/karma-storage'
import { mapGetters } from 'vuex'

export default {
  name: 'Karma',
  props: {
    type: {
      'type': Number,
      'required': true
    }
  },
  data () {
    return {
      'ready': false,
      'help': '' + 
        'Karma is a calculated value from the number of upvotes and downvotes.' + '\u000d' +
        'You can only Up / Down vote an ability or combo once.' + '\u000d' +
        'This make take some time to propagate through the whole system.',
      'ranking': { 'up': 0, 'down': 0, 'total': 0 }
    }
  },
  created: function () {
    const vm = this

    let web = []

    if (this.type === 1) {
      web.push(axios.get(process.env.API_BASE_URL + 'AbilityRanking').then((reponse) => { return reponse.data }))
    } else if (this.type === 2) {
      web.push(axios.get(process.env.API_BASE_URL + 'ComboRanking').then((reponse) => { return reponse.data }))
    }
    
    Promise.all(web).then((values) => {
      const rankingAbilityDB = values[0]
      
      let abilityKey = vm.$route.params.key
      let key = parseInt(abilityKey)
      let ranking = rankingAbilityDB[key]
      if (ranking) {
        vm.ranking = ranking
      }
      
      vm.ready = true
    }).catch(function (error) {
      console.log(error)
    })
  },
  computed: mapGetters(['dotaId']),
  methods: {
    upVote () {
      const result = voteAbility(this.dotaId, this.type, this.$route.params.key, true)
      // Give instant feedback to the user
      if (result) {
        this.ranking.up++
        this.ranking.total++
      }
    },
    downVote () {
      const result = voteAbility(this.dotaId, this.type, this.$route.params.key, false)
      // Give instant feedback to the user
      if (result) {
        this.ranking.down++
        this.ranking.total = this.ranking.total === 0 ? 0 : this.ranking.total - 1
      }
    }
  }
}
</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped>
</style>
