import '@babel/polyfill'
import Vue from 'vue'
import './plugins/bootstrap-vue'
import App from './App.vue'
import MixIn from './mixins/helper'
import router from './router'


// Components
import Loading from "@/components/Loading";
import SocialShare from '@/components/SocialShare.vue'
import SocialFollow from '@/components/SocialFollow.vue'
import Regions from '@/components/Regions.vue'

Vue.component('hgv-loading', Loading)
Vue.component('hgv-social-share', SocialShare)
Vue.component('hgv-social-follow', SocialFollow)
Vue.component('hgv-regions', Regions)

Vue.mixin(MixIn);

Vue.config.productionTip = false

new Vue({
  router,
  render: h => h(App)
}).$mount('#app')

