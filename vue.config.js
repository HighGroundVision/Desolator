var RemoveServiceWorkerPlugin = require('webpack-remove-serviceworker-plugin')
var CopyWebpackPlugin = require('copy-webpack-plugin')
var path = require('path')

module.exports = {
  configureWebpack: {
    plugins: [
      new RemoveServiceWorkerPlugin({ filename: 'service-worker.js' }),
      new CopyWebpackPlugin([
        {
          from: path.resolve(__dirname, './static'),
          to: 'static',
          ignore: ['.*']
        }
      ])
    ]
  }
}