<template>
  <section class="opaque-background">
    <div class="row">
        <div class="col-xl-3 col-lg-3 col-md-4 col-sm-4 col-12">
            <div class="text-center">
                <b-img src="/static/images/cluckles.png" fluid  />
                <b-alert variant="info" show>   
                    <strong>Hi, I am Cluckles!</strong><br />
                    AD is hard and confusing, lucky I am here to help make it fun!
                </b-alert>
            </div>
        </div>
        <div class="col-xl-9 col-lg-9 col-md-8 col-sm-8 col-12">
            <h1 class="text-warning">Ability Drafter</h1>
            <p>Dota has changed once again with the 7.07 patch.
            We are going to have to change with it.
            We have currently retired the existing branch and are working on vNext.
            It is nice to see Ability Draft (AD) getting some attention.
            This change will fix a number of existing game breaking bugs.
            As well as introduce some new heroes and some old favourites.
            Although some these heroes will have limited abilities in the pool.
            Check the <b-button size="" variant="link" to="/draft/pool">Pool</b-button> for info on which heroes &amp; abilities are active in AD.
            Read on for more details about what has changed and what is coming next!
            </p>
        </div>
    </div>
    <div class="row">
        <div class="col-12">
            <h2>Patch 7.07</h2>
            <ul>
                <li>New Drafting UI</li>
                <li>Added support for chat during draft.</li>
                <li>Added support for swapping ability slots.</li>
                <li>Added tooltips for hero talents and base stats as well as indicators for hero primary attributes.</li>
                <li>Scoreboard will now show drafted abilities at end of game and when viewing replays.</li>
                <li><b class="text-success">17 New</b> heroes. Including Dark Willow and Pangolier.</li>
                <li><b class="text-warning">6 Existing</b> heroes have been "fixed".</li>
            </ul>
            <h2>vNext</h2>
            A number of these changes have made the existing parts of this website obsolete.
            <ul>
                <li>Tooltips for base stats</li>
                <li>Tooltips for primary attribute</li>
                <li>Tooltips for talents</li>
            </ul>
            Since the primary focus of the website was to provide these details to drafters and that will no longer be needed after the update we are focusing the website towards drafting.
            We are currently rewriting the API to provide a better drafting experience. We want to include the stats for Ability &amp; Combos. As well as filters for Stuns, Disables, Passives, etc.
            We are going to investigate the changes to the scoreboard as it may contain insights into the drafting order.
            You can get more update details on our twitter <a href="https://twitter.com/DotaHGV">@DotaHGV</a>
        </div>
    </div>
  </section>
</template>

<script>
export default {
  name: 'Dashboard',
  data () {
    return {
      msg: '...'
    }
  }
}
</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped>
</style>
