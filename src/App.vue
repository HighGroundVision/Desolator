<template>
  <div id="app">
    <header>
      <b-navbar toggleable="md" type="dark" class="bd-navbar">
        <b-container>
          <b-navbar-toggle target="nav_collapse"></b-navbar-toggle>
          <b-navbar-brand to="/">[HGV] Ability Drafter</b-navbar-brand>
          <b-collapse is-nav id="nav_collapse">
            <b-navbar-nav>
              <b-nav-item to="/draft/pool">Draft Pool</b-nav-item>
            </b-navbar-nav>
          </b-collapse>
        </b-container>
      </b-navbar>
    </header>
    <b-container>
      <router-view></router-view>
      <hr />
      <footer>
        <p>&copy; 2018 - High Ground Vision</p>
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
body {
    color: white;
    background-color: #0B1425;
    background: #0B1425 url("~/static/images/background.jpg") no-repeat;
}
.bd-navbar {
  background-color:#573dcc;
}
#app {
  font-family: 'Avenir', Helvetica, Arial, sans-serif;
  -webkit-font-smoothing: antialiased;
  -moz-osx-font-smoothing: grayscale;
}
</style>
