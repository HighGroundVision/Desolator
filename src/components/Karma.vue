<template>
  <section v-if="ready">
    <div class="text-center">
      <b-alert variant="info" show>
        <b-row>
          <b-col>
            <strong>Cluckles Says</strong>
            <br />
            <span>Get involved and vote!</span>
          </b-col>
        </b-row>
        <hr />
        <b-row v-if="enabled">
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
      <div style="float: right; position: relative; top:-15px;">
        <i class="far fa-question-circle" :title="help"></i> 
      </div>
      <b-img src="/static/images/cluckles-speach.png" class="cluckles-speach" />
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
import swal from 'sweetalert'

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
      'enabled': process.env.FLAG_KARMA === true,
      'ready': false,
      'help': '' + 
        'Karma is calculated from up-votes and down-votes.' + '\u000d' +
        'You can only cast your vote once.' + '\u000d' +
        'Changes may take some time to propagate throughout the system.',
      'ranking': { 'up': 0, 'down': 0, 'total': 0 }
    }
  },
  created: function () {
    const vm = this

    let requests = []

    if (this.type === 1) {
      requests.push(axios.get(process.env.API_BASE_URL + 'AbilityRanking').then((reponse) => { return reponse.data }))
    } else if (this.type === 2) {
      requests.push(axios.get(process.env.API_BASE_URL + 'ComboRanking').then((reponse) => { return reponse.data }))
    }
    
    Promise.all(requests).then((values) => {
      const rankingAbilityDB = values[0]

      let abilityKey = vm.$route.params.key
      let ranking = rankingAbilityDB[abilityKey]
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
      } else {
        swal('You already voted', 'You can only cast your vote once')
      }
    },
    downVote () {
      const result = voteAbility(this.dotaId, this.type, this.$route.params.key, false)

      // Give instant feedback to the user
      if (result) {
        this.ranking.down++
        this.ranking.total = this.ranking.total === 0 ? 0 : this.ranking.total - 1
      } else {
        swal('You already voted', 'You can only cast your vote once')
      }
    }
  }
}
</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped>
</style>
