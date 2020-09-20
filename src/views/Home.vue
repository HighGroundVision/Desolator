<template>
  <div>
    <!-- Banner -->
    <section id="banner">
      <div class="content">
        <div class="flex flex-3">
          <div class="flex-item left">
          </div>
          <div class="flex-item">
            <div style="text-align:center">
              <img src="/static/images/cluckles-surrender.png" />
              <br />
              <b>We Surrender!</b>
              <br />
              <b>
                For years we have been build AD tools then Datdota comes along a releases what we have been trying to build. 
                They cover almost all the aspects we do but a better job of presenting the information.
                So rather then have two systems that do basically the same thing and end up confusing users with the differences we have retired this website and its supporting systems.
                As always everything we do at HGV is open source so you can always check out the source over at <a href="https://github.com/orgs/HighGroundVision/dashboard">Github</a>.
                We maybe back in the future with new tools and encourage you to check <a href="https://hgv-tango.azurewebsites.net/">High Ground Vision</a> for updates on this and our other projects.
                We suggest you to visit Ability Draft Statistics (Created by Noxville) for Hero statics, Ability combos, Leaderboards and more.
              </b>
            </div>
            <br />
            <ul class="actions">
              <li><a  class="button scrolly" href="https://abilitydraft.datdota.com/" target="_blank">Ability Draft Statistics</a></li>
            </ul>
          </div>
          <div class="flex-item right">
          </div>
        </div>
      </div>
    </section>
  </div>
</template>

<script>

export default {
  name: 'home',
  data () {
    return { construction: true }
  }
}
</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped>
li:not(:last-child) {
    margin-bottom: 5px;
}
.center {
  text-align:center;
}
.icon-header {
  font-size:5em; 
  padding: 10px;
  color: #f6755e;
}
</style>

