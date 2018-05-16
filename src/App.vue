<template>
  <div id="app">
    <b-container>
      <header>
        <b-navbar toggleable="md" type="dark" class="opaque-background">
          <b-navbar-toggle target="nav_collapse"></b-navbar-toggle>
          <b-navbar-brand to="/">[HGV] Ability Drafter</b-navbar-brand>
          <b-collapse is-nav id="nav_collapse">
            <b-navbar-nav>
              <b-nav-item to="/pool">Pool</b-nav-item>
              <b-nav-item to="/abilities">Abilities</b-nav-item>
              <b-nav-item to="/combos">Combos</b-nav-item>
            </b-navbar-nav>
          </b-collapse>
        </b-navbar>
      </header>
      <router-view></router-view>
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
.body-app {
  font-family: /*'Gugi',*/ Arial, sans-serif;
  -webkit-font-smoothing: antialiased;
  -moz-osx-font-smoothing: grayscale;
  color: white;
  background: url("~/static/images/bg-left.png") left top / 50% no-repeat, url("~/static/images/bg-right.png") right top / 50% no-repeat, black;
}
.opaque-background {
    border-radius: 10px;
    padding-left: 10px;
    background-color: #0B1425;
    margin: 10px;
    padding: 10px;
}
.ability-icon {
    box-shadow: 5px 4px #000000;
}
.ability-icon-lg {
  height: 64px;
  width: 64px;
}
.ability-icon-sm {
  height: 33px;
  width: 33px;
}
</style>
