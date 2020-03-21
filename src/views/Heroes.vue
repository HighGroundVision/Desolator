<template>
  <div>
    <section class="wrapper header special">
      <div class="inner">
        <h1>Heroes</h1>
      </div>
    </section>

    <section class="wrapper style2">
      <div class="inner">
        <loader :loading="loading">
          <h2>Top Heroes</h2>
          <p>
            These are ability drafts top 9 heroes. 
            While others may rise and fall with the meta these heroes have stood the test of time.
            If you get one of these heroes your already well on your way to having a great match.
            Although each hero is unique, like a snowflake, so click on a hero to gain more insight into what makes each wonderful in their own way.
          </p>
          <div class="w3-row-padding">
            <template v-for="(item) in topHeroes">
              <div v-bind:key="item.id" class="w3-col s12 m6 l4 w3-padding-small">
                <div class="w3-card w3-row w3-padding">
                  <div class="w3-col" style="width:64px">
                    <img class="hero-icon-s" :src="item.imageProfile"  />
                  </div>
                  <div class="w3-rest w3-padding-small">
                    <b style="line-height: 40px; color:#25a2c3">
                      <router-link style="text-decoration: none" :to="'/hero/' + item.id">{{item.name}}</router-link>
                    </b>
                      <div class="w3-border w3-center w3-round">
                      <div :style="'background-color:#f6755e;height:32px;width:'+ Math.round(item.total.winRate * 100)+'%'">
                        <b>{{Math.round(item.total.winRate * 100)}}%</b>
                      </div>
                    </div>
                  </div>
                </div>
              </div>
            </template>
          </div>
          <br />
          <h2>Heroes</h2>
          <p>
            We have charted the heroes current week win rate and with their pervious week.
            So you can see how each hero compare to each other.
            Also how the meta for each hero is changing week to week or in response to a patch.
          </p>
          <p>
            The chart bellow list all the active heroes in order of highest win rate.
            It has dumbbells that connect the <b>Previous</b> week and the <b style="color: #f6755e">Current</b> week.
          </p>
          <div class="chart" ref="chart"></div>
        </loader>
      </div>
    </section>
  </div>
</template>

<script>
import axios from "axios"
import { am4core, am4charts } from "@/plugins/amcharts-vue"

export default {
  name: "heroes",
  data() {
    return {
      loading: true,
      heroes: [],
      topHeroes: []
    }
  },
  mounted() {
    this.loadData()
  },
  methods: {
    async loadData() {
      let self = this
      self.loading = true

      let response = await axios.get(
        "https://tarrasque.azurewebsites.net/api/heroes"
      )
      self.heroes = response.data

      self.heroes.sort((a, b) => b.total.winRate - a.total.winRate)
      self.topHeroes = self.heroes.slice(0, 9)

      self.heroes.sort((a, b) => b.current.winRate - a.current.winRate)

      self.loading = false

      self.$nextTick(() => {
			  self.makeChart()
		  })
    },
    makeChart() {
      let self = this

      let chart = am4core.create(this.$refs.chart, am4charts.XYChart)
      chart.numberFormatter.numberFormat = "#%"
      
      //chart.scrollbarY = new am4core.Scrollbar()

      let data = []
      for (const item of this.heroes) {
        data.push({
          id: item.id,
          category: item.name,
          open: item.current.winRate,
          close: item.previous.winRate
        })
      }

      chart.data = data

      var categoryAxis = chart.yAxes.push(new am4charts.CategoryAxis())
      //categoryAxis.start = 0
      //categoryAxis.end = 0.3
      categoryAxis.renderer.grid.template.location = 0
      categoryAxis.renderer.ticks.template.disabled = true
      categoryAxis.renderer.axisFills.template.disabled = true
      categoryAxis.dataFields.category = "category"
      categoryAxis.renderer.minGridDistance = 15
      categoryAxis.renderer.inversed = true
      categoryAxis.renderer.inside = false
      categoryAxis.renderer.minGridDistance = 16
      categoryAxis.renderer.grid.template.location = 0.5
      categoryAxis.renderer.grid.template.strokeDasharray = "1,3"
      categoryAxis.renderer.labels.template.events.on("hit", function(event) {
        self.$router.push({
          name: "hero",
          params: { id: event.target.dataItem.dataContext.id }
        })
      })

      var image = new am4core.Image()
      image.horizontalCenter = "middle"
      image.width = 20
      image.height = 20
      image.verticalCenter = "middle"
      image.adapter.add("href", (href, target) => {
        let category = target.dataItem.category
        if (category) {
          let cat = self.heroes.find(_ => _.name == category)
          return cat.imageIcon
        }
        return href
      })
      categoryAxis.dataItems.template.bullet = image

      var valueAxis = chart.xAxes.push(new am4charts.ValueAxis())
      valueAxis.tooltip.disabled = true
      valueAxis.renderer.ticks.template.disabled = true
      valueAxis.renderer.axisFills.template.disabled = true
      valueAxis.renderer.opposite = true
      valueAxis.title.text = "Win Rate"

      var series = chart.series.push(new am4charts.ColumnSeries())
      series.dataFields.categoryY = "category"
      series.dataFields.openValueX = "open"
      series.dataFields.valueX = "close"
      series.tooltipText = "low: {openValueX.value} high: {valueX.value}"
      series.sequencedInterpolation = true
      series.fillOpacity = 0
      series.strokeOpacity = 1
      series.stroke = "#000"
      series.columns.template.height = 0.01
      series.tooltip.pointerOrientation = "vertical"

      var preivousBullet = series.bullets.create(am4charts.CircleBullet)
      preivousBullet.fill = "#606060"
      preivousBullet.stroke = "#000"

      var currentBullet = series.bullets.create(am4charts.CircleBullet)
      currentBullet.locationX = 1
      currentBullet.fill = "#f6755e"
      currentBullet.stroke = "#000"

      chart.legend = new am4charts.Legend()
      chart.legend.position = "top"
      chart.legend.data = [
        {
          name: "Previous Winrate",
          fill: "#606060"
        },
        {
          name: "Current Winrate",
          fill: "#f6755e"
        }
      ]
    }
  }
}
</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped>
.chart {
  width: 100%;
  height: 3000px;
}
.hero-icon-xs
{
  width: 32px;
}
.hero-icon-s
{
  width: 64px;
}
.hero-icon
{
  width: 128px;
}
</style>

