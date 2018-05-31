import Vue from 'vue'
import Router from 'vue-router'

import AuthLogin from '@/components/AuthLogin'
import AuthLogout from '@/components/AuthLogout'
import AuthHandler from '@/components/AuthHandler'
import Dashboard from '@/components/Dashboard'
import DraftPool from '@/components/DraftPool'
import AbilityStats from '@/components/AbilityStats'
import ComboStats from '@/components/ComboStats'
import Abilities from '@/components/Abilities'
import Combos from '@/components/Combos'
import MatchHistory from '@/components/MatchHistory'
import MatchDetails from '@/components/MatchDetails'

import store from '../store'

Vue.use(Router)

let router = new Router({
  routes: [
    {
      path: '/',
      name: 'Home',
      component: Dashboard
    },
    {
      path: '/login',
      name: 'Login',
      component: AuthLogin
    },
    {
      path: '/logout',
      name: 'Logout',
      component: AuthLogout
    },
    {
      path: '/authenticated',
      name: 'Authenticated',
      component: AuthHandler
    },
    {
      path: '/pool',
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
    },
    {
      path: '/combos',
      name: 'CombosList',
      component: Combos
    },
    {
      path: '/match/history',
      name: 'MatchHistory',
      component: MatchHistory,
      meta: { authorization: true }
    },
    {
      path: '/match/details/:key',
      name: 'MatchDetails',
      component: MatchDetails
    }
  ]
})

router.beforeEach((to, from, next) => {
  if (to.meta.authorization && store.getters.isAnonymous) {
    next('/login?r=' + to.fullPath)
  } else {
    next()
  }
})

export default router
