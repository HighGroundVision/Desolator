export function getUserIfExists () {
  if (typeof (Storage) !== 'undefined') {
    const json = localStorage.getItem('user')
    const user = JSON.parse(json)
    return user
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
