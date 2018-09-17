import Vue from 'vue'
import Router from 'vue-router'

import ErrorHandler from '@/components/Error'
import Dashboard from '@/components/Dashboard'

Vue.use(Router)

let router = new Router({
  routes: [
    {
      path: '/',
      name: 'Home',
      component: Dashboard
    },
    {
      path: '/error',
      name: 'Error',
      component: ErrorHandler
    }
  ]
})

export default router
