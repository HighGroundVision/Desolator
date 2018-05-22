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
// import axios from 'axios'

export default {
  name: 'AuthHandler',
  data () {
    return {}
  },
  created: function () {
    let openidIdentity = this.$route.query['openid.identity']
    const identity = openidIdentity.replace('https://steamcommunity.com/openid/id/', '')
    
    const user = { userid: identity, username: 'User: ' + identity }
    this.$store.commit('login', user)

    this.$router.push('/')

    /*
    const url = 'https://steamcommunity.com/id/{ID}/?xml=1'.replace('{ID}', identity)
    axios.get(url).then((response) => {
      console.log(response.data)
    })
    */
  }
}
</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped>
</style>
