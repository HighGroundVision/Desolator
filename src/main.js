import '@babel/polyfill'
import Vue from 'vue'
import './plugins/bootstrap-vue'
import App from './App.vue'
import router from './router'

// Components
import SocialShare from '@/components/SocialShare.vue'
import SocialFollow from '@/components/SocialFollow.vue'
import Loading from "@/components/Loading";

Vue.component('hgv-social-share', SocialShare)
Vue.component('hgv-social-follow', SocialFollow)
Vue.component('hgv-loading', Loading)

Vue.config.productionTip = false

new Vue({
  router,
  render: h => h(App)
}).$mount('#app')
