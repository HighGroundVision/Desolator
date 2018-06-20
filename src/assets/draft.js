export function getUnknownAbility () {
  return { 'id': 0, 'name': 'Unkown', 'img': 'https://hgv-hyperstone.azurewebsites.net/abilities/empty.png' }
}

export function getHeroes (q, heroes) {
  let query = q.split(',').map(lhs => parseInt(lhs))
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

export function getAbilities (q, abilities) {
  let query = q.split(',').map(lhs => parseInt(lhs))
  let collection = []
  for (let i = 0; i < query.length; i++) {
    const id = query[i]
    const data = abilities.filter(h => h.id === id)
    if (data.length === 1) {
      collection.push(data[0])
    }
  }
  return collection
}

export function filterPool (pool) {
  let heroes = pool.filter(h => h.enabled === true)
  for (let h = 0; h < heroes.length; h++) {
    let hero = heroes[h]
    // Hero
    hero.name_lower = hero.name.toLowerCase()
    hero.acronym = hero.acronym.toLowerCase()
    hero.icon = hero.img.replace('/banner/', '/icons/')

    // Abilities
    hero.abilities = hero.abilities.filter(a => a.enabled === true)

    for (let a = 0; a < hero.abilities.length; a++) {
      let ability = hero.abilities[a]
      ability.name_lower = ability.name.toLowerCase()
      ability.hero_lower = hero.name_lower
    }
  }

  return heroes
}

export function extractAbilties (teams) {
  let abilities = []
  
  for (let t = 0; t < teams.length; t++) {
    let team = teams[t]
    for (let h = 0; h < team.length; h++) {
      let hero = team[h]
      for (let a = 0; a < hero.abilities.length; a++) {
        let ability = hero.abilities[a]
        abilities.push(ability)
      }
    }
  }

  return abilities
}
