import Vue from 'vue'
import Vuex from 'vuex'

Vue.use(Vuex)

export default new Vuex.Store({
  state: {
    authenticated: false,
    username: '',
    userid: 0
  },
  getters: {
    isAuthenticated: state => {
      return state.authenticated === true
    },
    isAnonymous: state => {
      return state.authenticated === false
    },
    userId: state => {
      return state.userid
    },
    userName: state => {
      return state.username
    }
  },
  mutations: {
    login (state, user) {
      state.authenticated = true
      state.username = user.username
      state.userid = user.userid
    },
    logout (state) {
      state.authenticated = false
      state.username = ''
      state.userid = 0
    }
  }
})
