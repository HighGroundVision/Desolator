import '@babel/polyfill'
import Vue from 'vue'
import './plugins/bootstrap-vue'
import App from './App.vue'
import router from './router'
import './registerServiceWorker'

// Components
import SocialShare from '@/components/SocialShare.vue'
import SocialFollow from '@/components/SocialFollow.vue'

Vue.component('hgv-social-share', SocialShare)
Vue.component('hgv-social-follow', SocialFollow)

Vue.config.productionTip = false

new Vue({
  router,
  render: h => h(App)
}).$mount('#app')
