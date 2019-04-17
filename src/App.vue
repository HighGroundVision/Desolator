<template>
  <div id="app">
    <b-container>
      <header class="top-spacing">
        <b-navbar toggleable="md" type="dark" variant="secondary" class="rounded">
          <b-navbar-toggle target="nav_collapse"></b-navbar-toggle>
          <b-navbar-brand to="/">[HGV] Ability Drafter</b-navbar-brand>
          <b-collapse is-nav id="nav_collapse">
            <b-navbar-nav class="mr-auto">
              <b-nav-item to="/about">About</b-nav-item>
              <b-nav-item to="/pool">Pool</b-nav-item>
              <b-nav-item to="/calendar">Calendar</b-nav-item>
              <b-nav-item to="/heroes">Heroes</b-nav-item>
              <b-nav-item to="/abilities">Abilities</b-nav-item>
              <b-nav-item to="/leaderboard">Leaderboard</b-nav-item>
              <!--<b-nav-item to="/draft">Draft</b-nav-item>-->
            </b-navbar-nav>
          </b-collapse>
        </b-navbar>
      </header>
    </b-container>
    <b-container class="top-spacing">
      <router-view></router-view>
      <hgv-social-follow />
      <hr class="highlighted" />
      <footer>
        <div class="text-center">
          <p>&copy; 2019 - <a href="https://hgv-tango.azurewebsites.net/" target="_blank">High Ground Vision</a></p>
        </div>
      </footer>
    </b-container>
  </div>
</template>

<style>
.top-spacing {
  padding-top: 10px;
}
</style>
