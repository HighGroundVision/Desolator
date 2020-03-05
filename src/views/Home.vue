<template>
  <div>
    <!-- Banner -->
    <section id="banner">
      <div class="content">
        <h1>Ability Draft</h1>
        <p>
          Alot like a normal match of Dota<br />
          But draft your <b>Abilities</b> not your Hero!<br />
          And you thought remebering 100+ heroes was hard try 400+ abilities...
        </p>
        <ul class="actions">
          <li><a href="#one" class="button scrolly">Get Started</a></li>
        </ul>
      </div>
    </section>

    <section class="wrapper style1 special">
      <div class="inner">
        <h2>News & Updates</h2>
        <ul style="list-style:none">
          <li>3 Feb 2020: Sign up for Ability Draft Challenge League</li>
          <li>26 Jan 2020 [7.24]: Void Spirit has now been enabled</li>
          <li>26 Nov 2019 [7.23]: Outlanders are here but only Snapfire is in the pool</li>
          <li>24 Sep 2019: Look for HGV @ Midas Mode 2.0</li>
        </ul>
        <p v-if="construction">
          <b><span class="icon fa-bullhorn"></span> We are currently rebuilding the systems that run this web application.</b>
          <br />
          Our continuous deployment process means that this site will be updated as we push out new features. 
          You are welcome to use the site and the new features. 
          Some features maybe unstable/incomplete and maybe removed in the future.
        </p>
      </div>
    </section>

    <section class="wrapper">
      <div class="inner flex flex-3">
        <div class="flex-item left">
          <div>
            <h3>Pool</h3>
            <p>A list has been complied of all the heroes and abilities that are enabled in AD. As well which Aghanim's Scepter upgrades are enabled.</p>
          </div>
          <div>
            <h3>Heros</h3>
            <p>See how the base models stack up against each other with details on each hero including ability combos.</p>
          </div>
        </div>
        <div class="flex-item">
          <div class="center" style="width:100%;">
            <h2>Statistics!</h2>
            <i class="fas fa-chart-line icon-header" style="font-size:15em;"></i>
          </div>
        </div>
        <div class="flex-item right">
          <div>
            <h3>Abilities</h3>
            <p>Study up on which abilities are strongest and highest priority and which abilities make powerful combos.</p>
          </div>
          <div>
            <h3>Profile</h3>
            <p>Get a bunch of infomation personalized for you including history, combatants, and ranking.</p>
          </div>
        </div>
      </div>
    </section>

    <section class="wrapper">
      <div class="inner flex flex-3">
        <div class="flex-item box">
          <div class="center">
            <i class="fas fa-list-ol icon-header"></i>
          </div>
          <div class="content">
            <h3 style="text-align: center;">Latter</h3>
            <p>
              See how you rank against others in your region. 
              We have created a Elo based rating system that is updated for each <span title="Those that have 'Expose Public Match Data' settings checked">public player</span>.
            </p>
            <p>
              As this feature is new it will take some time before the rankings are correctly disturbed as there was no way to seed the players. So all players start at the default calibration.
            </p>
          </div>
        </div>
        <div class="flex-item box">
          <div style="text-align:center;">
            <i class="fas fa-globe-americas icon-header"></i>
            <i class="fas fa-globe-europe icon-header"></i>
            <i class="fas fa-globe-asia icon-header"></i>
          </div>
          <div class="content">
            <h3 class="center">Regions</h3>
            <p>Answers to questions you never knew you wanted.</p>
            <p>
              Which region is most popular? 
              When are people playing in your region? 
              How many matches are played in each region? 
              Which is best day of week and time of day to minimize my queue time?
            </p>
          </div>
        </div>
        <div class="flex-item box">
          <div class="center">
            <i class="fas fa-drafting-compass icon-header"></i>
          </div>
          <div class="content">
            <h3 class="center">Drafter</h3>
            <p>
              For those that need alittle assitant with drafting don't worry Cluckles will guide you.
            </p>
            <p>
              You can now filter the combos specifically for your hero and limit the abilities to only those in the draft.
            </p>
            <p class="center">
              <b>Coming Soon!</b>
              <br />
              <span style="font-size: 0.4em;"> (Valve Time™)</span>
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
    return { construction: false }
  }
}
</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped>
.center {
  text-align:center;
}
.icon-header {
  font-size:5em; 
  padding: 10px;
  color: #f6755e;
}
</style>

