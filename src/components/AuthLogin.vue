<template>
  <section class="opaque-background">
    <b-row>
      <b-col>
        <div class="text-center">
          <b-alert variant="warning" show>   
            <strong>Redirecting...</strong>
            <br />
            <span>Sit tight we are redirecting you to the Steam Login</span>
          </b-alert>
        </div>
      </b-col>
    </b-row>
  </section>
</template>

<script>
import {getUserIfExists} from '@/assets/user-storage'

export default {
  name: 'AuthLogin',
  data () {
    return {}
  },
  created: function () {
    const user = getUserIfExists()
    if (user) {
      this.$store.commit('login', user)
      this.$router.push('/')
    }

    const returnUrl = encodeURIComponent(window.location.origin + '/#/authenticated')
    const realmUrl = encodeURIComponent(window.location.origin)
    let base = 'https://steamcommunity.com/openid/login?openid.mode=checkid_setup&openid.ns=http%3A%2F%2Fspecs.openid.net%2Fauth%2F2.0&openid.ns.sreg=http%3A%2F%2Fopenid.net%2Fextensions%2Fsreg%2F1.1&openid.sreg.optional=nickname%2Cemail%2Cfullname%2Cdob%2Cgender%2Cpostcode%2Ccountry%2Clanguage%2Ctimezone&openid.ns.ax=http%3A%2F%2Fopenid.net%2Fsrv%2Fax%2F1.0&openid.ax.mode=fetch_request&openid.ax.type.fullname=http%3A%2F%2Faxschema.org%2FnamePerson&openid.ax.type.firstname=http%3A%2F%2Faxschema.org%2FnamePerson%2Ffirst&openid.ax.type.lastname=http%3A%2F%2Faxschema.org%2FnamePerson%2Flast&openid.ax.type.email=http%3A%2F%2Faxschema.org%2Fcontact%2Femail&openid.ax.required=fullname%2Cfirstname%2Clastname%2Cemail&openid.identity=http%3A%2F%2Fspecs.openid.net%2Fauth%2F2.0%2Fidentifier_select&openid.claimed_id=http%3A%2F%2Fspecs.openid.net%2Fauth%2F2.0%2Fidentifier_select&openid.return_to={RETURN}&openid.realm={REALM}'
    const url = base.replace('{RETURN}', returnUrl).replace('{REALM}', realmUrl)
    window.location = url
  }
}
</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped>
</style>
