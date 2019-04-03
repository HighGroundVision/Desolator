var RemoveServiceWorkerPlugin = require('webpack-remove-serviceworker-plugin')

module.exports = {
  assetsSubDirectory: 'static',
  configureWebpack: {
    plugins: [
      new RemoveServiceWorkerPlugin({ filename: 'service-worker.js' })
    ]
  }
}