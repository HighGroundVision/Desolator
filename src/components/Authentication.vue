<template>
  <div v-if="enabled">
    <b-navbar-nav v-if="isAnonymous" class="mr-auto">
      <b-nav-item to="/login">Login</b-nav-item>
    </b-navbar-nav>
    <b-navbar-nav v-if="isAuthenticated" class="mr-auto">
      <span v-if="isAuthenticated" class="navbar-text text-info">{{userName}}</span>
      <b-nav-item to="/logout">Logout</b-nav-item>
    </b-navbar-nav>
  </div>
</template>

<script>
import { mapGetters } from 'vuex'

export default {
  name: 'AuthReponse',
  data () {
    return {
      'enabled': process.env.FLAG_AUTHENTICATION
    }
  },
  computed: mapGetters(['isAuthenticated', 'isAnonymous', 'userName'])
}
</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped>
</style>
