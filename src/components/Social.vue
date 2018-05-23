<template>
  <div class="text-center">
    <b-img src="/static/images/cluckles.png" fluid  />
    <b-alert variant="info" show>   
        <strong>Cluckles says sharing is caring!</strong>
        <br />
        <b-button variant="secondary"  v-if="enabled" @click="reddit">
          <i class="fab fa-reddit-square"></i> Reddit
        </b-button>
         <b-button variant="secondary" v-if="enabled" @click="twitter">
          <i class="fab fa-twitter-square"></i> Twitter
        </b-button>
    </b-alert>
  </div>
</template>

<script>
const defultMessage = 'Cluckles says sharing is caring!'
const defaultWindowSettings = 'status=no,height=600,width=900,resizable=yes,left=0,top=0,screenX=0,screenY=0,toolbar=no,menubar=no,scrollbars=no,location=no,directories=no'

export default {
  name: 'Soical',
  props: {
    msg: {
      'type': String,
      'default': defultMessage,
      'required': true
    },
    link: {
      'type': String,
      'default': window.location.href
    }
  },
  data () {
    return {
      'enabled': process.env.FLAG_SOICAL,
      'url': this.link,
      'message': this.msg,
      'popup': undefined
    }
  },
  methods: {
    reddit: function () {
      let url = 'https://www.reddit.com/r/abilitydraft/submit?url=' + encodeURIComponent(this.url) + '&title=' + encodeURIComponent(this.msg)
      this.popup = window.open(url, defultMessage, defaultWindowSettings)
      this.popup.focus()
    },
    twitter: function () {
      let url = 'https://twitter.com/intent/tweet?text=' + encodeURIComponent(this.msg) + '&url=' + encodeURIComponent(this.url) + '&hashtags=dota2,abilitydraft'
      this.popup = window.open(url, defultMessage, defaultWindowSettings)
      this.popup.focus()
    }
  }
}
</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped>
</style>
