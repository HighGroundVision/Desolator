<template>
  <div id="app">
    <header>
      <nav class="navbar navbar-inverse navbar-fixed-top">
        <div class="container">
            <div class="navbar-header">
                <button type="button" class="navbar-toggle" data-toggle="collapse" data-target=".navbar-collapse">
                    <span class="sr-only">Toggle navigation</span>
                    <span class="icon-bar"></span>
                    <span class="icon-bar"></span>
                    <span class="icon-bar"></span>
                </button>
                <router-link to="/" class="navbar-brand">[HGV] Ability Drafter</router-link>
            </div>
            <div class="navbar-collapse collapse">
                <ul class="nav navbar-nav">
                    <li>
                      <router-link to="/foo">Go to Foo</router-link>
                    </li>
                    <li>
                      <router-link to="/bar">Go to Bar</router-link>
                    </li>
                </ul>
            </div>
        </div>
      </nav>
    </header>
    <hr />
    <router-view/>
    <hr />
    <footer>
        <p>&copy; 2017 - High Ground Vision</p>
    </footer>
  </div>
</template>

<script>
export default {
  name: 'App'
}
</script>

<style>
body {
  background-color: #0B1425;
  background: #0B1425 url("/images/backgrounds/background.jpg") no-repeat;
}
#app {
    /*padding-top: 50px;*/
    /*padding-bottom: 20px;*/
    font-family: 'Lato', serif;
    color: white;
}
</style>
