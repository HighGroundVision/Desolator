import Vue from 'vue'
import Router from 'vue-router'
import Home from './views/Home.vue'

Vue.use(Router)

// route level code-splitting
// this generates a separate chunk (about.[hash].js) for this route
// which is lazy-loaded when the route is visited.
/* webpackChunkName: "about" */

export default new Router({
  routes: [
    {
      path: '/',
      name: 'home',
      component: Home
    },
    {
      path: '/about',
      name: 'about',
      component: () => import('./views/About.vue') 
    },
    {
      path: '/pool',
      name: 'pool',
      component: () => import('./views/Pool.vue') 
    },
    {
      path: '/calendar',
      name: 'calendar',
      component: () => import('./views/Calendar.vue')
    },
    {
      path: '/heroes',
      name: 'heroes',
      component: () => import('./views/Heroes.vue')
    },
    {
      path: '/hero/:id',
      name: 'hero',
      component: () => import('./views/Hero.vue')
    },
    {
      path: '/abilities',
      name: 'abilities',
      component: () => import('./views/Abilities.vue')
    },
    {
      path: '/ability/:id',
      name: 'ability',
      component: () => import('./views/Ability.vue')
    },
    {
      path: '/leaderboard',
      name: 'leaderboard',
      component: () => import('./views/Leaderboard.vue')
    },
    {
      path: '/draft',
      name: 'draft',
      component: () => import('./views/Draft.vue')
    }
  ]
})
