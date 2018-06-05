import axios from 'axios'

function hasLocationStorage () {
  if (typeof (Storage) !== 'undefined') {
    return true
  } else {
    return false
  }
}

function voteByType (type, data) {
  if (!hasLocationStorage()) {
    return false
  }

  if (data.account_id === 0) {
    return false
  }

  let json = localStorage.getItem(type)
  let karma = JSON.parse(json)

  if (!karma) {
    karma = []
  }

  let result = false

  if (karma.includes(data.key) === false) {
    karma.push(data.key)
    axios.post(process.env.API_BASE_URL + 'Vote', data)
    result = true
  } else {
    result = false
    console.log('Karma', 'Already voted for ' + data.key)
  }

  json = JSON.stringify(karma)
  localStorage.setItem(type, json)

  return result
}

export function voteAbility (dotaId, type, key, vote) {
  let data = {
    'account_id': dotaId, 
    'type': type, 
    'key': key, 
    'vote': vote
  }
  return voteByType('hgv-ad-karma-abilities', data)
}

export function voteCombo (dotaId, type, key, vote) {
  let data = {
    'account_id': dotaId, 
    'type': type, 
    'key': key, 
    'vote': vote
  }
  return voteByType('hgv-ad-karma-combos', data)
}
