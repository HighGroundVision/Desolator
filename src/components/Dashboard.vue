<template>
  <section class="opaque-background">
    <b-row>
      <b-col cols="4" lg="3">
        <div class="text-center">
            <b-img src="/static/images/cluckles.png" fluid  />
            <b-alert variant="info" show>   
                <strong>Hi, I am Cluckles!</strong>
                <br />
                <span>AD is hard and confusing, lucky I am here to help make it fun!</span>
            </b-alert>
        </div>
      </b-col>
      <b-col>
        <h1 class="text-warning">Ability Drafter</h1>
        <b-alert variant="warning" show>
          <b-row>
            <b-col>
              <strong>Under Construction</strong>
              <p>
                We are currently rebuilding the backend and front systems that run this web application. 
                Our continuous deployment process means that this site will be updated as we push out new features. 
                You are welcome to use the site and the new features. 
                Some features maybe unstable/incomplete and maybe removed in the future.
                This banner will be removed when the rework is finished.
              </p>
              <p>Please provide any feedback to <a href="https://twitter.com/DotaHGV" target="_blank">@DotaHGV</a> or open an issue on <a href="https://github.com/HighGroundVision/Desolator/issues" target="_blank">GitHub</a>.</p>
            </b-col>
          </b-row>
        </b-alert>
        <p>Lorem ipsum dolor sit amet, consectetur adipiscing elit. Nam suscipit elit sed magna rutrum, vel ornare est lacinia. Nam et mi fringilla, volutpat sem eget, eleifend enim. Vestibulum eget enim vehicula, placerat sem id, ullamcorper felis. Nullam tempor mauris ut sapien tincidunt, in vulputate odio iaculis. Vivamus vitae facilisis quam, eget egestas tellus. Suspendisse potenti. Mauris ac efficitur ante, suscipit molestie sapien. Vestibulum posuere mauris magna, nec varius orci sagittis at. Mauris pellentesque quam massa, non efficitur turpis auctor vel. Nam sed eros sit amet ligula dictum laoreet in at magna. Donec vitae dictum nunc, vel rhoncus arcu.</p>
        <p>Phasellus luctus ac diam nec maximus. Pellentesque habitant morbi tristique senectus et netus et malesuada fames ac turpis egestas. Quisque leo orci, vehicula at dapibus ut, sodales vitae justo. Sed sit amet ullamcorper arcu, et semper tortor. Curabitur enim augue, semper sed rhoncus tincidunt, suscipit eget tellus. Maecenas venenatis euismod quam in sodales. Ut lectus felis, porta et dapibus sed, semper nec purus. Etiam nec nisi ut odio feugiat pulvinar. Vestibulum ante ipsum primis in faucibus orci luctus et ultrices posuere cubilia Curae; Sed efficitur, turpis ut hendrerit rutrum, turpis augue eleifend ex, viverra tristique neque risus eu tellus. Integer ut faucibus justo. Integer sodales lorem tellus, ut aliquet ex volutpat sed. Nam sollicitudin eleifend molestie.</p>
        <p>Duis sit amet feugiat lacus, vel molestie lorem. Pellentesque habitant morbi tristique senectus et netus et malesuada fames ac turpis egestas. Maecenas eleifend vehicula dolor non egestas. Cras nec venenatis nibh, ac faucibus lacus. Cras convallis vulputate dapibus. Sed lectus turpis, sollicitudin non pulvinar et, fringilla sit amet augue. Phasellus massa lectus, congue et nibh eu, luctus iaculis purus. Integer nisl massa, pharetra sit amet erat at, pharetra pharetra magna. Fusce velit mi, egestas id magna posuere, malesuada semper mauris. Duis posuere est non enim venenatis dictum.</p>
      </b-col>
    </b-row>
  </section>
</template>

<script>
export default {
  name: 'Dashboard',
  data () {
    return {}
  }
}
</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped>
</style>
