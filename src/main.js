// The Vue build version to load with the `import` command
// (runtime-only or standalone) has been set in webpack.base.conf with an alias.
import Vue from 'vue'
import VueResource from 'vue-resource'
import BootstrapVue from 'bootstrap-vue'
import App from './App'
import router from './router'
import MissingData from '@/components/MissingData.vue'
import AbilityStats from '@/components/AbilityStats.vue'
import ComboStats from '@/components/ComboStats.vue'
import 'bootstrap/dist/css/bootstrap.min.css'
import 'bootstrap-vue/dist/bootstrap-vue.css'

Vue.use(VueResource)
Vue.use(BootstrapVue)
Vue.config.productionTip = false

Vue.component('missing-data', MissingData)
Vue.component('ability-stats', AbilityStats)
Vue.component('combo-stats', ComboStats)

/* eslint-disable no-new */
new Vue({
  el: '#app',
  router,
  template: '<App/>',
  components: { App }
})
