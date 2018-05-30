import axios from 'axios'
import * as moment from 'moment'

function hasLocationStorage () {
  if (typeof (Storage) !== 'undefined') {
    return true
  } else {
    return false
  }
}

function refreshLease (user) {
  let now = moment()
  let offset = moment(user.lease)
  if (offset < now) {
    axios.get(process.env.API_BASE_URL + 'RefreshAccount?mode=18&account=' + user.dotaId)
    user.lease = moment().add(1, 'h')
    localStorage.setItem('user', JSON.stringify(user))
  }
}

export function setUser (store, data) {
  if (!hasLocationStorage()) {
    return
  }

  let offset = moment().add(1, 'h')

  const user = {
    steamId: data.steam_id, 
    dotaId: data.dota_id,
    username: data.persona,
    lease: offset
  }

  localStorage.setItem('user', JSON.stringify(user))

  axios.get(process.env.API_BASE_URL + 'RefreshAccount?mode=18&account=' + user.dotaId)

  store.commit('login', user)
}

export function hydrateUser (store) {
  if (!hasLocationStorage()) {
    return false
  }

  let user

  const json = localStorage.getItem('user')
  if (json) {
    user = JSON.parse(json)
  } else {
    return false
  }

  if (user) {
    refreshLease(user)

    store.commit('login', user)
    return true
  } else {
    return false
  }
}

export function clearUser (store) {
  if (!hasLocationStorage()) {
    return
  }

  store.commit('logout')

  localStorage.setItem('user', '')
}
