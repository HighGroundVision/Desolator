import Vue from 'vue'
import Router from 'vue-router'
import Dashboard from '@/components/Dashboard'
import DraftPool from '@/components/DraftPool'
import AbilityStats from '@/components/AbilityStats'
import ComboStats from '@/components/ComboStats'
import Abilities from '@/components/Abilities'

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
      path: '/stats/ability/:key',
      name: 'StatsAbility',
      component: AbilityStats
    },
    {
      path: '/stats/abilities/:key',
      name: 'ComboStats',
      component: ComboStats
    },
    {
      path: '/abilities',
      name: 'AbilitiesList',
      component: Abilities
    }
  ]
})
