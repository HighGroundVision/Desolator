<template>
  <div id="app">
    <!-- Header -->
    <header id="header">
      <nav class="left">
        <a href="#menu"></a>
      </nav>
      <img class="logo-icon" src="/favicon.ico" style=""/>
      <a href="/" class="logo">High Ground Vision</a>
      <!--
      <nav class="right">
        <a href="#" class="button fit">Login</a>
      </nav>
      -->
    </header>

		<!-- Menu -->
    <nav id="menu">      
      <ul class="links">
        <li><a href="https://www.reddit.com/r/Abilitydraft/comments/gcy778/how_to_tryhard_at_ability_draft/" target="_blank">Guide</a></li>
        <li><span>(What Works)</span> <router-link to="/pool">Pool</router-link> </li>
        <li><span>(Numbers)</span> <router-link to="/heroes">Heroes</router-link> </li>
        <li><span>(More Numbers)</span> <router-link to="/abilities">Abilities</router-link> </li>
        <li><span>(Regional Leaderboards)</span> <router-link to="/ladder">Ladder</router-link> </li>
        <li><span>(regional Peak times)</span> <router-link to="/regions">Regions</router-link> </li>
        <li><span>(Drafting Assistant)</span> <router-link to="/drafter">Drafter</router-link> </li>
        <li><span>(Personalized Numbers)</span> <router-link to="/profile">Profile</router-link> </li>
      </ul>
    </nav>

		<router-view :key="$route.fullPath"></router-view>

		<!-- Footer -->
    <footer id="footer">
      <div class="inner">
        <h2>Get In Touch</h2>
        <ul class="actions">
          <li><i class="fab fa-twitter"></i> <a href="https://twitter.com/dotahgv" target="_blank"> @DotaHGV</a></li>
          <li><i class="fab fa-reddit-alien"></i> <a href="https://www.reddit.com/r/Abilitydraft/" target="_blank"> r/AbilityDraft</a></li>
          <li><i class="fab fa-discord"></i> <a href="https://discord.gg/sSFjabV" target="_blank"> Chat on Discord</a></li>
          <li><i class="fab fa-github"></i> <a href="https://github.com/HighGroundVision" target="_blank"> We love Open Source</a></li>
        </ul>
      </div>
      <div class="copyright">
        <div class="image round">
          <a href="https://hgv-tango.azurewebsites.net/" target="_blank">
            <img src="/static/images/wards.jpg" />
          </a>
        </div>
        <br />
        &copy; 2020 High Ground Vision
        <br />
        Template by <a href="https://templated.co" target="_blank">TEMPLATED</a>
      </div>
    </footer>
    
  </div>
</template>

<style>
</style>
