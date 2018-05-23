export function getUserIfExists () {
  if (typeof (Storage) !== 'undefined') {
    const json = localStorage.getItem('user')
    if (json) {
      return JSON.parse(json)
    } else {
      return undefined
    }
  }
}

export function setUser (user) {
  if (typeof (Storage) !== 'undefined') {
    localStorage.setItem('user', JSON.stringify(user))
  }
}

export function clearUser () {
  if (typeof (Storage) !== 'undefined') {
    localStorage.setItem('user', '')
  }
}
