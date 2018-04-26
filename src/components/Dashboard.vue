<template>
  <section>
    <br />
    <div class="row">
        <div class="col-lg-2">
            <b-img src="/static/images/cluckles.png" fluid  />
            <b-alert variant="info" show>
                <div class="text-center">
                    <strong>Hi, I am Cluckles!</strong><br />
                    <p>AD is hard and confusing, lucky I am here to help make it fun and insightful</p>
                </div>
            </b-alert>
        </div>
        <div class="col-lg-6 offset-lg-4">
            <h2 class="text-warning">High Ground Vision</h2>
            <h3>Ability Drafter</h3>
            <p>Dota has changed once again with the 7.07 patch.</p>
            <p>We are going to have to change with it.</p>
            <p>We have currently retired the existing branch and are working on vNext.</p>
            <p>It is nice to see Ability Draft (AD) getting some attention.</p>
            <p>This change will fix a number of existing game breaking bugs.</p>
            <p>As well as introduce some new heroes and some old favourites.</p>
            <p>Although some these heroes will have limited abilities in the pool.</p>
            <p>Check the <b-button size="" variant="link" to="/draft/pool">Pool</b-button> for info on which heroes &amp; abilities are active in AD.</p>
            <p>Read on for more details about what has changed and what is coming next!</p>
            <br />
            <br />
        </div>
    </div>
    <hr />
    <div class="row">
        <div class="col-md-12">

            <h2>Patch 7.07 &amp; vNext</h2>
            <ul>
                <li>New Drafting UI</li>
                <li>Added support for chat during draft.</li>
                <li>Added support for swapping ability slots.</li>
                <li>Added tooltips for hero talents and base stats as well as indicators for hero primary attributes.</li>
                <li>Scoreboard will now show drafted abilities at end of game and when viewing replays.</li>
                <li><b class="text-success">17 New</b> heroes. Including Dark Willow and Pangolier.</li>
                <li><b class="text-warning">6 Existing</b> heroes have been "fixed".</li>
            </ul>

            <p>A number of these changes have made the existing parts of this website obsolete.</p>
            <ul>
                <li>Tooltips for base stats</li>
                <li>Tooltips for primary attribute</li>
                <li>Tooltips for talents</li>
            </ul>
            <p>Since the primary focus of the website was to provide these details to drafters and that will no longer be needed after the update we are focusing the website towards drafting.</p>
            <p>We are currently rewriting the API to provide a better drafting experience. We want to include the stats for Ability &amp; Combos. As well as filters for Stuns, Disables, Passives, etc.</p>
            <p>We are going to investigate the changes to the scoreboard as it may contain insights into the drafting order.</p>
            <p>You can get more update details on our twitter <a href="https://twitter.com/DotaHGV">@DotaHGV</a></p>
        </div>
    </div>
  </section>
</template>

<script>
export default {
  name: 'dashboard',
  data () {
    return {
      msg: 'Welcome to the Dashboard!'
    }
  }
}
</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped>
</style>
