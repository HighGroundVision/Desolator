<template>
</template>

<script>
import convertor from 'steam-id-convertor'

export default {
  name: 'authenticated',
  created() {
    var steamId = this.$route.query['openid.identity'].replace('https://steamcommunity.com/openid/id/','')
    var accountId = convertor.to32(steamId)
    localStorage.setItem("accountId", accountId);
    this.$router.push({ name: 'profile' });
  }
}
</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped>
</style>

