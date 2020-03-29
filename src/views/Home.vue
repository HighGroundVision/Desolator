<template>
  <div>
    <!-- Banner -->
    <section id="banner">
      <div class="content">
        <div class="flex flex-3">
          <div class="flex-item left">
          </div>
          <div class="flex-item">
            <div style="text-align:center">
              <img src="/static/images/cluckles.png" />
              <br />
              <b>Hi, I am Cluckles.</b>
              <br />
              <b>
                Ability Draft is hard game mode to understand. 
                Its a lot like a normal match of Dota but things are… rarely as they seem.
                In this mode you draft your Abilities not your Hero.
                It is challenging to become a drafting master but luckily I am here to help!
              </b>
            </div>
            <br />
            <ul class="actions">
              <li><router-link  class="button scrolly" to="/guide">Get Started Guide</router-link></li>
            </ul>
          </div>
          <div class="flex-item right">
          </div>
        </div>
      </div>
    </section>

    <section class="wrapper style1 ">
      <div class="inner">
        <h2>News & Updates</h2>
        <ul style="list-style:none">
          <li><span class="w3-tag w3-round w3-light-blue" style="width:80px;">7.25</span> 17 Mar 2020: Unlocked many new abilities granted by Aghanim's Scepter</li>
          <li><span class="w3-tag w3-round w3-light-blue" style="width:80px;">7.24c</span> 12 Mar 2020: Added Lone Druid, as well as Spirit Bear, Spirit Link, Savage Roar and True Form</li>
          <li><span class="w3-tag w3-round w3-light-blue" style="width:80px;">7.24b</span> 09 Mar 2020: Enabled Morphling, but without attribute shift or morph</li>
          <li><span class="w3-tag w3-round w3-light-blue" style="width:80px;"></span> 03 Feb 2020: Sign up for Ability Draft Challenge League</li>
          <li><span class="w3-tag w3-round w3-light-blue" style="width:80px;">7.24a</span> 26 Jan 2020: Void Spirit has now been enabled</li>
          <li><span class="w3-tag w3-round w3-light-blue" style="width:80px;">7.23</span> 26 Nov 2019: Outlanders are here but only Snapfire is in the pool</li>
          <li><span class="w3-tag w3-round w3-light-blue" style="width:80px;"></span> 24 Sep 2019: Look for HGV @ Midas Mode 2.0</li>
        </ul>
        <p v-if="construction">
          <b><i class="fas fa-bullhorn"></i> We are currently rebuilding the systems that run this web application.</b>
          <br />
          Our continuous deployment process means that this site will be updated as we push out new features. 
          You are welcome to use the site and the new features. 
          Some features maybe unstable/incomplete and maybe removed in the future.
          We will remove this alert when the work is complete.
        </p>
      </div>
    </section>

    <section class="wrapper">
      <div class="inner flex flex-3">
        <div class="flex-item left">
          <div>
            <h3><router-link to="/pool">Pool</router-link></h3>
            <p>A list has been complied of all the heroes and abilities that are enabled in AD. As well which Aghanim's Scepter upgrades are enabled.</p>
          </div>
          <div>
            <h3><router-link to="/heroes">Heroes</router-link></h3>
            <p>See how the base models stack up against each other with details on each hero including ability combos.</p>
          </div>
        </div>
        <div class="flex-item">
          <div class="center" style="width:100%;">
            <h2 style="color: #f6755e;">Statistics!</h2>
            <i class="fas fa-chart-line icon-header" style="font-size:15em;"></i>
          </div>
        </div>
        <div class="flex-item right">
          <div>
            <h3><router-link to="/abilities">Abilities</router-link></h3>
            <p>Study up on which abilities are strongest, highest priority and make powerful combos.</p>
          </div>
          <div>
            <h3><router-link to="/profile">Profile</router-link></h3>
            <p>Get a bunch of information personalized for you including history, combatants, and ranking.</p>
          </div>
        </div>
      </div>
    </section>

    <section class="wrapper">
      <div class="inner flex flex-3">
        <div class="flex-item box w3-card">
          <div class="center">
            <i class="fas fa-list-ol icon-header"></i>
          </div>
          <div class="content">
            <h3 class="center"><router-link to="/ladder">Ladder</router-link></h3>
            <p>
              See how you rank against others in your region. 
              We have created a Elo based rating system that is updated for each <span title="Those that have 'Expose Public Match Data' settings checked">public player</span>.
            </p>
            <p>
              As this feature is new it will take some time before the rankings are correctly disturbed as there was no way to seed the players. 
              So all players start at the default calibration.
            </p>
          </div>
        </div>
        <div class="flex-item box w3-card">
          <div style="text-align:center;">
            <i class="fas fa-globe-americas icon-header"></i>
            <i class="fas fa-globe-europe icon-header"></i>
            <i class="fas fa-globe-asia icon-header"></i>
          </div>
          <div class="content">
            <h3 class="center"><router-link to="/regions">Regions</router-link></h3>
            <p>Answers to questions you never knew you wanted.</p>
            <p>
              Which region is most popular? 
              When are people playing in your region? 
              How many matches are played in each region? 
              Which is best day of week and time of day to minimize my queue time?
            </p>
          </div>
        </div>
        <div class="flex-item box w3-card">
          <div class="center">
            <i class="fas fa-drafting-compass icon-header"></i>
          </div>
          <div class="content">
            <h3 class="center"><router-link to="/drafter">Drafter</router-link></h3>
            <p>
              For those that need a little assistance with drafting don't worry Cluckles will guide you.
            </p>
            <p>
              You can filter the combos specifically for your hero and limit the abilities to only those in the draft.
            </p>
          </div>
        </div>
      </div>
    </section>
  </div>
</template>

<script>
export default {
  name: 'home',
  data () {
    return { construction: true }
  }
}
</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped>
li:not(:last-child) {
    margin-bottom: 5px;
}
.center {
  text-align:center;
}
.icon-header {
  font-size:5em; 
  padding: 10px;
  color: #f6755e;
}
</style>

