var CopyWebpackPlugin = require('copy-webpack-plugin')
var path = require('path')

module.exports = {
    configureWebpack: {
        plugins: [
            new CopyWebpackPlugin([{
                from: path.resolve(__dirname, './static'),
                to: 'static',
                ignore: ['.*']
            }])
        ]
    }
}