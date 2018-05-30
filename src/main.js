// The Vue build version to load with the `import` command
// (runtime-only or standalone) has been set in webpack.base.conf with an alias.

// JS
import Vue from 'vue'
import VueResource from 'vue-resource'
import BootstrapVue from 'bootstrap-vue'
import App from './App'
import router from './router'
import store from './store'
import {hydrateUser} from '@/assets/user-storage'

// Components
import Loader from 'vue-spinner/src/PulseLoader.vue'
import Authentication from '@/components/Authentication.vue'
import MissingData from '@/components/MissingData.vue'
import MissingCombos from '@/components/MissingCombos.vue'
import SocialShare from '@/components/SocialShare.vue'
import SocialFollow from '@/components/SocialFollow.vue'

// CSS
import 'bootstrap/dist/css/bootstrap.min.css'
import 'bootstrap-vue/dist/bootstrap-vue.css'
import '@/assets/main.css'

Vue.use(VueResource)
Vue.use(BootstrapVue)

Vue.component('hgv-authentication', Authentication)
Vue.component('hgv-missing-data', MissingData)
Vue.component('hgv-missing-combos', MissingCombos)
Vue.component('hgv-social-share', SocialShare)
Vue.component('hgv-social-follow', SocialFollow)
Vue.component('hgv-loader', Loader)

Vue.config.productionTip = false

hydrateUser(store)

/* eslint-disable no-new */
new Vue({
  el: '#app',
  router,
  store,
  template: '<App/>',
  components: { App }
})
