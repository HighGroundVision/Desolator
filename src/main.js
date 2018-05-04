// The Vue build version to load with the `import` command
// (runtime-only or standalone) has been set in webpack.base.conf with an alias.
import Vue from 'vue'
import VueResource from 'vue-resource'
import BootstrapVue from 'bootstrap-vue'
import App from './App'
import router from './router'
import PacmanLoader from 'vue-spinner/src/PacmanLoader.vue'
import 'bootstrap/dist/css/bootstrap.min.css'
import 'bootstrap-vue/dist/bootstrap-vue.css'

Vue.use(VueResource)
Vue.use(BootstrapVue)
Vue.config.productionTip = false

Vue.component('pacman-loader', PacmanLoader)

/* eslint-disable no-new */
new Vue({
  el: '#app',
  router,
  template: '<App/>',
  components: { App }
})
