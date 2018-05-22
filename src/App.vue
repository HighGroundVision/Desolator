<template>
  <div id="app">
    <b-container>
      <header>
        <b-navbar toggleable="md" type="dark" class="opaque-background">
          <b-navbar-toggle target="nav_collapse"></b-navbar-toggle>
          <b-navbar-brand to="/">[HGV] Ability Drafter</b-navbar-brand>
          <b-collapse is-nav id="nav_collapse">
            <b-navbar-nav class="mr-auto">
              <b-nav-item to="/pool">Pool</b-nav-item>
              <b-nav-item to="/abilities">Abilities</b-nav-item>
              <b-nav-item to="/combos">Combos</b-nav-item>
            </b-navbar-nav>
            <hgv-authentication></hgv-authentication>
          </b-collapse>
        </b-navbar>
      </header>
      <router-view></router-view>
      <div class="sticky-container">
        <ul class="sticky">
            <li>
                <i class="fab fa-2x fa-twitter-square text-warning"></i>
                <p><a href="https://twitter.com/DotaHGV/" target="_blank">Follow Us on<br>Twitter</a></p>
            </li>
            <li>
                <i class="fab fa-2x fa-reddit-square text-warning"></i>
                <p><a href="https://www.reddit.com/r/abilitydraft/" target="_blank">Find Us on<br>Reddit</a></p>
            </li>
        </ul>
      </div>
      <footer>
        <div class="text-center">
          <p>&copy; 2018 - High Ground Vision</p>
        </div>
      </footer>
    </b-container>
  </div>
</template>

<script>
export default {
  name: 'app'
}
</script>

<style>
</style>
