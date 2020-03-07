import Vue from 'vue'
import Router from 'vue-router'
import Home from './views/Home.vue'

Vue.use(Router)

// route level code-splitting
// this generates a separate chunk (about.[hash].js) for this route
// which is lazy-loaded when the route is visited.
/* webpackChunkName: "about" */

export default new Router({
    routes: [{
            path: '/',
            name: 'home',
            component: Home
        },
        {
            path: '/guide',
            name: 'guide',
            component: () =>
                import ('./views/Guide.vue')
        },
        {
            path: '/pool',
            name: 'pool',
            component: () =>
                import ('./views/Pool.vue')
        },
        {
            path: '/heroes',
            name: 'heroes',
            component: () =>
                import ('./views/Heroes.vue')
        },
        {
            path: '/abilities',
            name: 'abilities',
            component: () =>
                import ('./views/Abilities.vue')
        },
        {
            path: '/latter',
            name: 'latter',
            component: () =>
                import ('./views/Latter.vue')
        },
        {
            path: '/regions',
            name: 'regions',
            component: () =>
                import ('./views/Regions.vue')
        },
        {
            path: '/drafter',
            name: 'drafter',
            component: () =>
                import ('./views/Drafter.vue')
        },
        {
            path: '/profile',
            name: 'profile',
            component: () =>
                import ('./views/Profile.vue')
        },
    ]
})