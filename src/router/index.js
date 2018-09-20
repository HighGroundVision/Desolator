import Vue from 'vue'
import Router from 'vue-router'

import PageError from '@/components/Error'
import PageDashboard from '@/components/Dashboard'
import PagePool from '@/components/Pool'
import PageHeroes from '@/components/Heroes'
import PageHero from '@/components/Hero'
import PageAbilities from '@/components/Abilities'
import PageAbility from '@/components/Ability'
import PageDraft from '@/components/Draft'

Vue.use(Router)

let router = new Router({
  routes: [
    {
      path: '/',
      name: 'Home',
      component: PageDashboard
    },
    {
      path: '/pool',
      name: 'Pool',
      component: PagePool
    },
    {
      path: '/hero/:id',
      name: 'Hero',
      component: PageHero
    },
    {
      path: '/heroes',
      name: 'Heroes',
      component: PageHeroes
    },
    {
      path: '/abilities',
      name: 'Abilities',
      component: PageAbilities
    },
    {
      path: '/ability/:id',
      name: 'Ability',
      component: PageAbility
    },
    {
      path: '/draft',
      name: 'Draft',
      component: PageDraft
    },
    {
      path: '/error',
      name: 'Error',
      component: PageError
    }
  ]
})

export default router
