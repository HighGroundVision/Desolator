<template>
  <div id="app">
    <!-- Header -->
    <header id="header">
      <img class="logo-icon" src="/favicon.ico" style=""/>
      <a href="/" class="logo">Ability Drafter</a>
    </header>

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
      </div>
    </footer>
    
  </div>
</template>

<style>
</style>
