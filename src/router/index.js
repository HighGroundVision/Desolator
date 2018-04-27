import Vue from 'vue'
import Router from 'vue-router'
import Dashboard from '@/components/Dashboard'
import DraftPool from '@/components/DraftPool'

Vue.use(Router)

export default new Router({
  routes: [
    {
      path: '/',
      name: 'Dashboard',
      component: Dashboard
    },
    {
      path: '/draft/pool',
      name: 'DraftPool',
      component: DraftPool
    },
    {
      path: '/abilities',
      name: 'abilities',
      component: DraftPool
    }
  ]
})