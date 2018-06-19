<template>
  <section class="opaque-background">
    <b-row>
      <b-col>
        <h1 class="text-warning">Draft</h1>
      </b-col>
    </b-row>
    <b-row>
      <b-col>
        <p>Lorem ipsum dolor sit amet, consectetur adipiscing elit. Maecenas metus eros, vulputate eu neque non, fringilla pretium massa. Curabitur sed auctor metus. Mauris nunc felis, porttitor ut varius ac, finibus vel nibh. In semper accumsan ullamcorper. Donec sollicitudin, quam sed finibus molestie, purus tortor venenatis massa, vel vehicula mi orci non purus. Aenean fringilla, enim mollis sollicitudin tristique, lorem enim consequat risus, eget auctor arcu turpis ac augue. Sed odio mi, consequat sit amet dui vel, consectetur laoreet diam. Donec pharetra dapibus dictum. Vivamus et dapibus lectus, nec porta leo. Nulla rutrum lorem non lorem gravida pulvinar. Sed lectus nibh, fermentum venenatis vestibulum sed, ultrices auctor dolor. Sed condimentum lacinia mi, et blandit dolor convallis ac. Nulla commodo nibh purus, at sodales dolor ornare vel.</p>
        <p class="text-center">
          <b-link to="/draft/heroes">
            <b-button variant="success">Start Draft</b-button>
          </b-link>
        </p>
      </b-col>
    </b-row>
  </section>
</template>

<script>
export default {
  name: 'Draft',
  data () {
    return {}
  },
  created () {
    // vm.$route.query.radiant
    // vm.$route.query.dire
    // vm.$route.query.hero
    // vm.$route.query.pool
  }
}
</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped>
</style>
