import Vue from 'vue'
import Router from 'vue-router'
import Dashboard from '@/components/Dashboard'
import DraftPool from '@/components/DraftPool'
import Stats from '@/components/Stats'

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
      path: '/stats/:key',
      name: 'Stats',
      component: Stats
    }
  ]
})
