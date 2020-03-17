<template>
  <div>
    <section class="wrapper header special">
      <div class="inner">
        <h1>Hero Details</h1>
      </div>
    </section>

    <section class="wrapper style2">
      <div class="inner">
        <loader :loading="loading">
          <div class="w3-row-padding w3-center">
            <div class="w3-third">
              <span>~</span>
            </div>
            <div class="w3-third">
              <h2>{{ability.name}}</h2>
              <img class="w3-round-large ability-icon" :src="ability.image"/>
              <br />
              <span>History (Win Rate)</span>
              <br />
              <div class="sparkline" ref="chart"></div>    
            </div>
            <div class="w3-third">
              <span>~</span>
            </div>
          </div>
          <br />
          <h3>Hero Combos</h3>
          <p>Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Purus viverra accumsan in nisl nisi scelerisque eu ultrices. Vitae ultricies leo integer malesuada nunc vel risus commodo viverra. Quis risus sed vulputate odio. Orci phasellus egestas tellus rutrum tellus pellentesque eu. Tellus in hac habitasse platea dictumst vestibulum rhoncus. Orci porta non pulvinar neque laoreet suspendisse interdum consectetur. Donec et odio pellentesque diam volutpat commodo. Diam maecenas sed enim ut sem viverra aliquet eget. Ultrices tincidunt arcu non sodales neque sodales ut etiam sit. In fermentum et sollicitudin ac. Purus gravida quis blandit turpis cursus in hac habitasse. Egestas egestas fringilla phasellus faucibus scelerisque. Mauris sit amet massa vitae. Ornare massa eget egestas purus.</p>
          <div class="w3-row-padding">
            <template v-for="(item) in ability.heroes">
              <div v-bind:key="item.id" class="w3-col s12 m6 l4 w3-padding-small">
                <div class="w3-card w3-row w3-padding">
                  <div class="w3-col" style="width:64px;">
                    <img class="hero-icon-s" :src="item.image"  />
                  </div>
                  <div class="w3-rest w3-padding-small">
                    <b style="line-height: 28px; color:#25a2c3;">
                      <router-link style="text-decoration: none;" :to="'/hero/' + item.id">{{item.name}}</router-link>
                    </b>
                    <div class="w3-border w3-center  w3-round">
                      <div :style="'background-color:#f6755e;height:28px;width:'+ Math.round(item.winRate * 100)+'%'">
                        <b>{{Math.round(item.winRate * 100)}}%</b>
                      </div>
                    </div>
                  </div>
                </div>
              </div>
            </template>
          </div>
           <br />
          <h3>Ability Combos</h3>
          <p>Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Purus viverra accumsan in nisl nisi scelerisque eu ultrices. Vitae ultricies leo integer malesuada nunc vel risus commodo viverra. Quis risus sed vulputate odio. Orci phasellus egestas tellus rutrum tellus pellentesque eu. Tellus in hac habitasse platea dictumst vestibulum rhoncus. Orci porta non pulvinar neque laoreet suspendisse interdum consectetur. Donec et odio pellentesque diam volutpat commodo. Diam maecenas sed enim ut sem viverra aliquet eget. Ultrices tincidunt arcu non sodales neque sodales ut etiam sit. In fermentum et sollicitudin ac. Purus gravida quis blandit turpis cursus in hac habitasse. Egestas egestas fringilla phasellus faucibus scelerisque. Mauris sit amet massa vitae. Ornare massa eget egestas purus.</p>
          <div class="w3-row-padding">
            <template v-for="(item) in ability.abilities">
              <div v-bind:key="item.id" class="w3-col s12 m6 l4 w3-padding-small">
                <div class="w3-card w3-row w3-padding">
                  <div class="w3-col" style="width:64px;">
                    <img class="ability-icon-s" :src="item.image"  />
                  </div>
                  <div class="w3-rest w3-padding-small">
                    <b style="line-height: 28px; color:#25a2c3;">
                      <router-link style="text-decoration: none;" :to="'/ability/' + item.id">{{item.name}}</router-link>
                    </b>
                    <div class="w3-border w3-center  w3-round">
                      <div :style="'background-color:#f6755e;height:28px;width:'+ Math.round(item.winRate * 100)+'%'">
                        <b>{{Math.round(item.winRate * 100)}}%</b>
                      </div>
                    </div>
                  </div>
                </div>
              </div>
            </template>
          </div>
        </loader>
      </div>
    </section>
  </div>
</template>

<script>
import axios from "axios"
import { am4core, am4charts } from "@/plugins/amcharts-vue"

export default {
  name: "hero",
  data() {
    return {
      loading: true,
      ability: {}
    }
  },
  mounted() {
    this.loadData()
  },
  methods: {
    async  loadData() {
      let self = this
      self.loading = true

      var response = await axios.get("https://tarrasque.azurewebsites.net/api/ability/" + this.$route.params.id)
      self.ability = response.data

      self.loading = false

      this.$nextTick(() => {
			  self.makeSparkline()
		  })
    },
    makeSparkline() {
      var chart = am4core.create(this.$refs.chart, am4charts.XYChart)

      let data = []
      for (const item of this.ability.history) {
        data.push({
          "date": new Date(item.day),
          "value": item.winRate * 100
        })
      }

      chart.data = data 

      chart.padding(0, 0, 0, 0)

      var dateAxis = chart.xAxes.push(new am4charts.DateAxis())
      dateAxis.renderer.grid.template.disabled = true
      dateAxis.renderer.labels.template.disabled = true
      dateAxis.startLocation = 0.5
      dateAxis.endLocation = 0.7
      dateAxis.cursorTooltipEnabled = false

      var valueAxis = chart.yAxes.push(new am4charts.ValueAxis())
      valueAxis.min = 0
      valueAxis.renderer.grid.template.disabled = true
      valueAxis.renderer.baseGrid.disabled = true
      valueAxis.renderer.labels.template.disabled = true
      valueAxis.cursorTooltipEnabled = false

      chart.cursor = new am4charts.XYCursor()
      chart.cursor.lineY.disabled = true

      var series = chart.series.push(new am4charts.LineSeries())
      series.tooltipText = "{date}: [bold]{value}"
      series.dataFields.dateX = "date"
      series.dataFields.valueY = "value"
      series.tensionX = 0.8
      series.strokeWidth = 2
      series.stroke = am4core.color("#ff0000");

      // render data points as bullets
      var bullet = series.bullets.push(new am4charts.CircleBullet())
      bullet.stroke = am4core.color("#ff0000");
      bullet.fill = am4core.color("#ff0000");
      bullet.circle.radius = 3
    }
  }
}
</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped>
.sparkline {
  /*width: 200px*/
  width: 100%;
  height: 30px;
  border: 1px dotted #eee;
}
.hero-icon-s
{
  height: 64px;
}
.ability-icon-s
{
  width: 64px;
  height: 64px;
}
.ability-icon-xs
{
  width: 30px;
  height: 30px;
}
.ability-icon
{
  width: 128px;
  height: 128px;
}
</style>
