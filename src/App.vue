<template>
  <div id="app">
    <!-- Header -->
    <header id="header">
      <nav class="left">
        <a href="#menu"></a>
      </nav>
      <img src="/favicon.ico" style="height: 35px; padding-right: 5px;"/>
      <a href="/" class="logo">High Ground Vision</a>
      <nav class="right">
        <a href="#" class="button fit">Login</a>
      </nav>
    </header>

		<!-- Menu -->
    <nav id="menu">
      <ul class="links">
        <li><a href="/">Home</a></li>
        <li><span>(What Works)</span> <router-link to="/pool">Pool</router-link> </li>
        <li><span>(Numbers)</span> <router-link to="/heroes">Heroes</router-link> </li>
        <li><span>(More Numbers)</span> <router-link to="/abilities">Abilities</router-link> </li>
        <li><span>(Regional Leaderboards)</span> <router-link to="/latter">Latter</router-link> </li>
        <li><span>(Regional Peek Times)</span> <router-link to="/regions">Regions</router-link> </li>
        <li><span>(Drafting Assistant)</span> <router-link to="/drafter">Drafter</router-link> </li>
        <li><span>(Personalized Numbers)</span> <router-link to="/profile">Profile</router-link> </li>
      </ul>
      <div style="text-align:center">
        <img src="/static/images/cluckles.png" />
        <br />
        <span>Hi, I am Cluckles. Ability Draft is hard game mode to understand and it is challenging to become a drafting master but luckly I am here to help!</span>
      </div>
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
        &copy; 2020 <a href="https://hgv-tango.azurewebsites.net/" target="_blank">High Ground Vision</a>
        <br />
        Template by <a href="https://templated.co" target="_blank">TEMPLATED</a>
      </div>
    </footer>
    
  </div>
</template>

<style>
</style>
