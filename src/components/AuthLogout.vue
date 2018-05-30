<template>
  <section class="opaque-background">
    <b-row>
      <b-col>
        <div class="text-center">
          <b-alert variant="warning" show>   
            <strong>Redirecting...</strong>
            <br />
            <span>Sit tight we are logging you out.</span>
          </b-alert>
        </div>
      </b-col>
    </b-row>
  </section>
</template>

<script>
import {clearUser} from '@/assets/user-storage'

export default {
  name: 'AuthLogout',
  data () {
    return {}
  },
  created: function () {
    clearUser(this.$store)
    
    this.$router.push('/')
  }
}
</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped>
</style>
