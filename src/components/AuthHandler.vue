<template>
  <section class="opaque-background">
    <b-row>
      <b-col>
        <div class="text-center">
          <b-alert variant="warning" show>   
            <strong>Finalizing...</strong>
            <br />
            <span>Sit tight we are finalizing the login process</span>
          </b-alert>
        </div>
      </b-col>
    </b-row>
  </section>
</template>

<script>
import axios from 'axios'

export default {
  name: 'AuthHandler',
  data () {
    return {}
  },
  created: function () {
    let openidIdentity = this.$route.query['openid.identity']
    const identity = openidIdentity.replace('https://steamcommunity.com/openid/id/', '')

    const url = process.env.API_BASE_URL + 'PlayerSummary?identity=' + identity
    axios.get(url).then((response) => {
      const user = { 
        steamid: response.data.SteamId, 
        dotaId: response.data.DotaId,
        username: response.data.Persona
      }

      this.$store.commit('login', user)
      this.$router.push('/')
    }).catch(function () {
      this.$store.commit('logout')
      this.$router.push('/')
    })
  }
}
</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped>
</style>
