export function getCollectionFromQueryString (q, heroes) {
  let query = q.radiant.split(',').map(lhs => parseInt(lhs))
  let collection = []
  for (let i = 0; i < query.length; i++) {
    const id = query[i]
    const data = heroes.filter(h => h.id === id)
    if (data.length === 1) {
      collection.push(data[0])
    }
  }
  return collection
}

export function GetUnknownAbility () {
  return { 'id': 0, 'name': 'Unkown', 'img': 'https://hgv-hyperstone.azurewebsites.net/abilities/empty.png' }
}
