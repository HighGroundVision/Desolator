<template>
  <div id="app">
    <header>
      <b-navbar toggleable="md" type="dark" class="b-navbar">
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
hr {
  background-color: orange;
}
.opaque-background {
    border-radius: 10px;
    padding-left: 10px;
    background-color: #0B1425;
    margin: 10px;
    padding: 10px;
}
.b-navbar {
   background-color: #0B1425;
}
.body-background {
  background-color: black;
}
#app {
  font-family: 'Gugi', Arial, sans-serif;
  -webkit-font-smoothing: antialiased;
  -moz-osx-font-smoothing: grayscale;
  color: white;
  background: url("~/static/images/bg-left.png") left top no-repeat, url("~/static/images/bg-right.png") right top no-repeat;
}
</style>
