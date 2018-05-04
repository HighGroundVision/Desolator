import Vue from 'vue'
import Router from 'vue-router'
import Dashboard from '@/components/Dashboard'
import DraftPool from '@/components/DraftPool'
import SingleStats from '@/components/abilities/SingleStats'
import PairStats from '@/components/abilities/PairStats'

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
      path: '/ability/single/:key',
      name: 'SingleStats',
      component: SingleStats
    },
    {
      path: '/ability/pairs/:key',
      name: 'PairStats',
      component: PairStats
    }
  ]
})
