import Vue from 'vue'
import Router from 'vue-router'
import Dashboard from '@/components/Dashboard'
import DraftPool from '@/components/DraftPool'
import AbilityStats from '@/components/AbilityStats'

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
      path: '/ability/stats/:key',
      name: 'AbilityStats',
      component: AbilityStats
    }
  ]
})
