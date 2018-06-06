var merge = require('webpack-merge')
var prodEnv = require('./prod.env')

module.exports = merge(prodEnv, {
  NODE_ENV: '"development"',
  API_BASE_URL: '"https://hgv-tarrasque.azurewebsites.net/api/"', //http://localhost:7071/api/
  FLAG_CONSTRUCTION: true,
  FLAG_AUTHENTICATION: true,
  FLAG_SOCIAL: true,
  FLAG_KARMA: true
})
