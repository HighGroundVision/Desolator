<template>
  <section class="opaque-background">
    <b-row>
      <b-col>
        <h1 class="text-warning">Draft</h1>
      </b-col>
    </b-row>
    <b-row>
      <b-col>
        <p>Drafting in AD is by far the hardest part of the game mode. People often have little idea what to pick and will often make bad picks for there hero based on fact they have no idea what is going on, or worst are drafting based on past AD trends.</p>
        <p>We hope to help stop that! We track each ability pair with the attack type and primary type so the stats of each ability is more revent to your hero's makup. We also do the same with combos.</p>
        <p>We have created this drafting wizard to guide you through the process entering the correct infomation to avire at the Live Draft. We will ask you select the heroes involed. Then fill in any missing abilities and select your hero.</p>
        <p class="text-center">
          <b-link to="/draft/heroes">
            <b-button variant="success">Start Draft</b-button>
          </b-link>
        </p>
      </b-col>
    </b-row>
  </section>
</template>

<script>
export default {
  name: 'Draft',
  data () {
    return {}
  }
}
</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped>
</style>
