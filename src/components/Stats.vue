<template>
  <section class="opaque-background">
    <div v-if="isSingle">
      <ability-stats :abilityKey="keys[0]"></ability-stats>
    </div>
    <div v-if="isArray">
     <combo-stats :abilityKeys="keys"></combo-stats>
    </div>
  </section>
</template>

<script>

export default {
  name: 'Stats',
  data () {
    const keys = this.$route.params.key.split('-')

    return {
      'isSingle': keys.length === 1, 
      'isArray': keys.length > 1,
      'keys': keys
    }
  }
}
</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped>
</style>
