var RemoveServiceWorkerPlugin = require('webpack-remove-serviceworker-plugin')

module.exports = {
  configureWebpack: {
    plugins: [
      new RemoveServiceWorkerPlugin({ filename: 'service-worker.js' })
    ]
  }
}