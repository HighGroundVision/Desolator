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
              <b-nav-item to="/match/history">Matches</b-nav-item>
              <b-nav-item to="/draft">Draft</b-nav-item>
            </b-navbar-nav>
            <hgv-authentication></hgv-authentication>
          </b-collapse>
        </b-navbar>
      </header>
      <router-view></router-view>
      <hgv-social-follow></hgv-social-follow>
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
