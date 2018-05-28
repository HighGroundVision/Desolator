<template>
  <div v-if="enabled">
    <b-navbar-nav v-if="isAnonymous" class="mr-auto">
      <b-nav-item to="/login">Login</b-nav-item>
    </b-navbar-nav>
    <b-navbar-nav v-if="isAuthenticated" class="mr-auto">
      <span class="d-none d-md-block navbar-text"><i class="far fa-user-circle text-warning"></i> &nbsp;</span>
      <span v-if="isAuthenticated" class="d-none d-lg-block navbar-text text-warning">{{userName}}</span>
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
