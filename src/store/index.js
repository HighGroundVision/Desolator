import Vue from 'vue'
import Vuex from 'vuex'

Vue.use(Vuex)

export default new Vuex.Store({
  state: {
    authenticated: false,
    steamId: '',
    dotaId: 0,
    username: ''
  },
  getters: {
    isAuthenticated: state => {
      return state.authenticated === true
    },
    isAnonymous: state => {
      return state.authenticated === false
    },
    steamId: state => {
      return state.steamId
    },
    dotaId: state => {
      return state.dotaId
    },
    userName: state => {
      return state.username
    }
  },
  mutations: {
    login (state, user) {
      state.authenticated = true
      state.steamId = user.steamId
      state.dotaId = user.dotaId
      state.username = user.username
    },
    logout (state) {
      state.authenticated = false
      state.steamId = ''
      state.dotaId = ''
      state.username = ''
    }
  }
})
