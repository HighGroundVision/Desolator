import Vue from 'vue'
import './plugins/fragment-vue'
//import './plugins/bootstrap-vue'
//import './plugins/amcharts-vue'
import App from './App.vue'
import MixIn from './mixins/helper'
import router from './router'

Vue.mixin(MixIn);

Vue.config.productionTip = false

new Vue({
  router,
  render: h => h(App)
}).$mount('#app')

