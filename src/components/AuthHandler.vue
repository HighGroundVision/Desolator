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
import {setUser, clearUser} from '@/assets/user-storage'

export default {
  name: 'AuthHandler',
  data () {
    return {}
  },
  created: function () {
    const self = this

    let openidIdentity = this.$route.query['openid.identity']
    const identity = openidIdentity.replace('https://steamcommunity.com/openid/id/', '')

    const url = process.env.API_BASE_URL + 'PlayerSummary?identity=' + identity
    axios.get(url).then((response) => {
      const user = { 
        steamid: response.data.SteamId, 
        dotaId: response.data.DotaId,
        username: response.data.Persona
      }

      setUser(user)

      self.$store.commit('login', user)
      self.$router.push('/')
    }).catch(function () {
      clearUser()
      
      self.$store.commit('logout')
      self.$router.push('/')
    })
  }
}
</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped>
</style>
