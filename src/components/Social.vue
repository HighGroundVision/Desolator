<template>
  <div class="text-center">
    <b-img src="/static/images/cluckles.png" fluid  />
    <b-alert variant="info" show>   
        <strong>Cluckles says sharing is caring!</strong>
        <br />
        <!--
        <social-sharing :url="url" :title="message" hashtags="dota2,abilitydraft" twitter-user="DotaHGV" inline-template>
          <div>
            <network network="reddit">
              <b-button variant="secondary">
                <i class="fab fa-reddit-square"></i> Reddit
              </b-button>
            </network>
            <network network="twitter">
              <b-button variant="secondary">
                <i class="fab fa-twitter-square"></i> Twitter
              </b-button>
            </network>
          </div>
        </social-sharing>
        -->
    </b-alert>
  </div>
</template>

<script>
export default {
  name: 'Soical',
  props: {
    msg: {
      'type': String,
      'default': 'Cluckles says sharing is caring!',
      'required': true
    },
    link: {
      'type': String,
      'default': window.location.href
    }
  },
  data () {
    return {
      'url': this.link,
      'message': this.msg
    }
  }
}
</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped>
</style>
