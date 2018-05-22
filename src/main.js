// The Vue build version to load with the `import` command
// (runtime-only or standalone) has been set in webpack.base.conf with an alias.

// JS
import Vue from 'vue'
import VueResource from 'vue-resource'
import BootstrapVue from 'bootstrap-vue'
import SocialSharing from 'vue-social-sharing'
import App from './App'
import router from './router'
import store from './store'

// Components
import Authentication from '@/components/Authentication.vue'
import MissingData from '@/components/MissingData.vue'
import MissingCombos from '@/components/MissingCombos.vue'
import Social from '@/components/Social.vue'

// CSS
import 'bootstrap/dist/css/bootstrap.min.css'
import 'bootstrap-vue/dist/bootstrap-vue.css'
import '@/assets/soical-follow.css'
import '@/assets/main.css'

Vue.use(VueResource)
Vue.use(BootstrapVue)
Vue.use(SocialSharing)

Vue.component('hgv-authentication', Authentication)
Vue.component('hgv-missing-data', MissingData)
Vue.component('hgv-missing-combos', MissingCombos)
Vue.component('hgv-social', Social)

Vue.config.productionTip = false

/* eslint-disable no-new */
new Vue({
  el: '#app',
  router,
  store,
  template: '<App/>',
  components: { App }
})
