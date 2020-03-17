import Vue from 'vue'
import App from './App.vue'
import router from './router'
import loader from './components/Loader.vue'

Vue.config.productionTip = false

Vue.component('loader', loader)

new Vue({
    router,
    render: h => h(App)
}).$mount('#app')