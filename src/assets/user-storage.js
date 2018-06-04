import axios from 'axios'
import * as moment from 'moment'

function hasLocationStorage () {
  if (typeof (Storage) !== 'undefined') {
    return true
  } else {
    return false
  }
}

function requestRefresh (user) {
  axios.get(process.env.API_BASE_URL + 'RefreshAccount?mode=18&account=' + user.dotaId)
}

function refreshLease (user) {
  let now = moment.utc()
  let offset = moment.utc(user.lease)
  if (offset < now) {
    requestRefresh(user)
    user.lease = moment().utc().add(1, 'h')
    localStorage.setItem('hgv-ad-user', JSON.stringify(user))
  }
}

export function setUser (store, data) {
  if (!hasLocationStorage()) {
    return
  }

  let offset = moment.utc().add(1, 'h')

  const user = {
    steamId: data.steam_id, 
    dotaId: data.dota_id,
    username: data.persona,
    lease: offset
  }

  localStorage.setItem('hgv-ad-user', JSON.stringify(user))

  requestRefresh(user)

  store.commit('login', user)
}

export function hydrateUser (store) {
  if (!hasLocationStorage()) {
    return false
  }

  let user

  const json = localStorage.getItem('hgv-ad-user')
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

  localStorage.setItem('hgv-ad-user', '')
}
