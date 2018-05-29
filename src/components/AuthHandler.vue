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
    const vm = this

    const redirect = vm.$route.query.r === undefined ? '/' : vm.$route.query.r

    let openidIdentity = this.$route.query['openid.identity']
    const identity = openidIdentity.replace('https://steamcommunity.com/openid/id/', '')

    const url = process.env.API_BASE_URL + 'PlayerSummary?identity=' + identity
    axios.get(url).then((response) => {
      const user = {
        steamId: response.data.steam_id, 
        dotaId: response.data.dota_id,
        username: response.data.persona
      }

      axios.get(process.env.API_BASE_URL + 'AcquireLease?mode=18&account=' + user.dotaId)

      setUser(user)

      vm.$store.commit('login', user)
      vm.$router.push(redirect)
    }).catch(function () {
      clearUser()
      
      vm.$store.commit('logout')
      vm.$router.push('/')
    })
  }
}
</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped>
</style>
