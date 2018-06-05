<template>
  <section>
    <div style="float: right;">
      <i class="far fa-question-circle" :title="help"></i> 
    </div>
    <div class="text-center">
      <b-img src="/static/images/cluckles.png" />
      <b-alert variant="info" show>
          <strong>Cluckles Says</strong>
          <br />
          <span>Sharing is caring!</span>
          <br />
          <b-button variant="secondary"  v-if="enabled" @click="reddit">
            <i class="fab fa-reddit-square"></i> Reddit
          </b-button>
          <b-button variant="secondary" v-if="enabled" @click="twitter">
            <i class="fab fa-twitter-square"></i> Twitter
          </b-button>
      </b-alert>
    </div>
  </section>
</template>

<script>
const title = 'Cluckles says sharing is caring!'

function getSettings () {
  const w = 900
  const h = 600
  const left = (screen.width / 2) - (w / 2)
  const top = (screen.height / 2) - (h / 2)
  let settings = 'status=no,height=' + h + ',width=' + w + ',resizable=yes,left=' + left + ',top=' + top + ',screenX=' + left + ',screenY=' + top + ',toolbar=no,menubar=no,scrollbars=no,location=no,directories=no'
  return settings
}

export default {
  name: 'SocialShare',
  props: {
    msg: {
      'type': String,
      'required': true
    }
  },
  data () {
    return {
      'enabled': process.env.FLAG_SOCIAL,
      'help': 'Share your favourite abilities and combos with Reddit & Twitter',
      'message': this.msg,
      'popup': undefined
    }
  },
  methods: {
    reddit () {
      const settings = getSettings()
      let url = 'https://www.reddit.com/r/abilitydraft/submit?url=' + encodeURIComponent(window.location.href) + '&title=' + encodeURIComponent(this.msg)
      this.popup = window.open(url, title, settings)
      this.popup.focus()
    },
    twitter () {
      const settings = getSettings()
      let url = 'https://twitter.com/intent/tweet?text=' + encodeURIComponent(this.msg) + '&url=' + encodeURIComponent(window.location.href) + '&hashtags=dota2,abilitydraft'
      this.popup = window.open(url, title, settings)
      this.popup.focus()
    }
  }
}
</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped>
</style>
