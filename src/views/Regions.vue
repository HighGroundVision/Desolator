<template>
  <div>
     <section class="wrapper header special">
      <div class="inner">
        <h1>Regions</h1>
      </div>
     </section>
     
     <section class="wrapper style2">
        <div class="inner">
          <h2>Regions</h2>
          <p>
            Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Purus viverra accumsan in nisl nisi scelerisque eu ultrices. Vitae ultricies leo integer malesuada nunc vel risus commodo viverra. Quis risus sed vulputate odio. Orci phasellus egestas tellus rutrum tellus pellentesque eu. Tellus in hac habitasse platea dictumst vestibulum rhoncus. Orci porta non pulvinar neque laoreet suspendisse interdum consectetur. Donec et odio pellentesque diam volutpat commodo. Diam maecenas sed enim ut sem viverra aliquet eget. Ultrices tincidunt arcu non sodales neque sodales ut etiam sit. In fermentum et sollicitudin ac. Purus gravida quis blandit turpis cursus in hac habitasse. Egestas egestas fringilla phasellus faucibus scelerisque. Mauris sit amet massa vitae. Ornare massa eget egestas purus.
          </p>
          <loader :loading="loadingRegions">
            <div id="chartRegions" class="chart" ref="chartRegions"></div>
          </loader>
          <h2>Timeline</h2>
          <p>
            Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Purus viverra accumsan in nisl nisi scelerisque eu ultrices. Vitae ultricies leo integer malesuada nunc vel risus commodo viverra. Quis risus sed vulputate odio. Orci phasellus egestas tellus rutrum tellus pellentesque eu. Tellus in hac habitasse platea dictumst vestibulum rhoncus. Orci porta non pulvinar neque laoreet suspendisse interdum consectetur. Donec et odio pellentesque diam volutpat commodo. Diam maecenas sed enim ut sem viverra aliquet eget. Ultrices tincidunt arcu non sodales neque sodales ut etiam sit. In fermentum et sollicitudin ac. Purus gravida quis blandit turpis cursus in hac habitasse. Egestas egestas fringilla phasellus faucibus scelerisque. Mauris sit amet massa vitae. Ornare massa eget egestas purus.
          </p>
          <div class="w3-content">
            <h4>Select Region</h4>
            <div class="select-wrapper">
              <select name="region" id="region" v-model="region" @change="changeRegion">
                <template v-for="(item) in regions">
                  <option v-bind:key="item.id" :value="item.id">{{item.name}} </option>
                </template>
              </select>
            </div>
          </div>
          <br />
          <loader :loading="loadingHistory">
            <div id="chartHistory" class="chart" ref="chartHistory"></div>
          </loader>
        </div>
      </section>
  </div>
</template>

<script>
import axios from "axios";
import { am4core, am4charts, am4maps, worldLow } from "@/plugins/amcharts-vue";

export default {
  name: 'regions',
  data() {
    return {
      loadingRegions: true,
      loadingHistory: true,
      regions: [],
      region: 2,
      history: {},
    }
  },
  mounted() {
    this.loadDataRegions()
    this.loadDataHistory()
  },
   methods: {
    async loadDataRegions() {
      this.loadingRegions = true
      let response = await axios.get("https://tarrasque.azurewebsites.net/api/regions")
      this.regions = response.data
      this.loadingRegions = false

      let self = this
      this.$nextTick(() => {
			  self.makeChartRegions()
		  })
    },
    async loadDataHistory() {
      this.loadingHistory = true
      let response = await axios.get("https://tarrasque.azurewebsites.net/api/region/" + this.region + "/history")
      this.history = response.data
      this.loadingHistory = false

      let self = this
      this.$nextTick(() => {
			  self.makeChartTimelane()
		  })
    },
    makeChartTimelane() {
      let self = this

      let chart = am4core.create(this.$refs.chartHistory, am4charts.XYChart)
      chart.maskBullets = false

      var xAxis = chart.xAxes.push(new am4charts.CategoryAxis())
      var yAxis = chart.yAxes.push(new am4charts.CategoryAxis())

      yAxis.dataFields.category = "weekday"
      xAxis.renderer.minGridDistance = 40
      xAxis.dataFields.category = "hour"

      xAxis.renderer.grid.template.disabled = true
      yAxis.renderer.grid.template.disabled = true
      xAxis.renderer.axisFills.template.disabled = true
      yAxis.renderer.axisFills.template.disabled = true
      yAxis.renderer.ticks.template.disabled = true
      xAxis.renderer.ticks.template.disabled = true

      yAxis.renderer.inversed = true

      var series = chart.series.push(new am4charts.ColumnSeries())
      series.dataFields.categoryY = "weekday"
      series.dataFields.categoryX = "hour"
      series.dataFields.value = "value"
      series.columns.template.disabled = true
      series.sequencedInterpolation = true
      //series.defaultState.transitionDuration = 3000

      var bullet = series.bullets.push(new am4core.Circle())
      bullet.tooltipText = "{value.workingValue.formatNumber('#.')}"
      bullet.strokeWidth = 3
      bullet.stroke = am4core.color("#ffffff")
      bullet.strokeOpacity = 0

      bullet.adapter.add("tooltipY", function(tooltipY, target) {
        return -target.radius + 1
      })

      series.heatRules.push({
        property: "radius",
        target: bullet,
        min: 1,
        max: 30
      })

      bullet.hiddenState.properties.scale = 0.01
      bullet.hiddenState.properties.opacity = 1

      var hoverState = bullet.states.create("hover")
      hoverState.properties.strokeOpacity = 1

      let data = []
      for (const key in this.history) {
        if (this.history.hasOwnProperty(key)) {
          const value = this.history[key]
          var parts = key.split(":")
          data.push({
            hour: parts[1],
            weekday: parts[0],
            value: value,
          });
        }
      }

      chart.data = data
    },
    makeChartRegions() {
      let self = this
      
      // Create map instance
      var chart = am4core.create(this.$refs.chartRegions, am4maps.MapChart);
      chart.chartContainer.wheelable = false;
      
      var colorSet = new am4core.ColorSet();
      var mapData = []
      for (const item of self.regions) {
        mapData.push({
          name: item.name,
          latitude: item.latitude,
          longitude: item.longitude,
          value: item.total,
          color: am4core.color("#f6755e")
        });
      }

      // Set map definition
      chart.geodata = worldLow;

      // Set projection
      chart.projection = new am4maps.projections.Miller();

      // Create map polygon series
      var polygonSeries = chart.series.push(new am4maps.MapPolygonSeries());
      polygonSeries.exclude = ["AQ"];
      polygonSeries.useGeodata = true;
      polygonSeries.nonScalingStroke = true;
      polygonSeries.strokeWidth = 0.5;
      polygonSeries.calculateVisualCenter = true;

      var imageSeries = chart.series.push(new am4maps.MapImageSeries());
      imageSeries.data = mapData;
      imageSeries.dataFields.value = "value";

      var imageTemplate = imageSeries.mapImages.template;
      imageTemplate.nonScaling = true
      //imageTemplate.propertyFields.longitude = "longitude";
      //imageTemplate.propertyFields.latitude = "latitude";

      var circle = imageTemplate.createChild(am4core.Circle);
      circle.fillOpacity = 0.7;
      circle.propertyFields.fill = "color";
      circle.tooltipText = "{name}: [bold]{value}[/]";

      imageSeries.heatRules.push({
        "target": circle,
        "property": "radius",
        "min": 5,
        "max": 20,
        "dataField": "value"
      })

      imageTemplate.adapter.add("latitude", function(latitude, target) {
        return target.dataItem.dataContext.latitude;
      })

      imageTemplate.adapter.add("longitude", function(longitude, target) {
        debugger;
        return target.dataItem.dataContext.longitude;
      })

    },
    changeRegion() {
      this.loadDataHistory()
    }
  }
}
</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped>
.chart {
  width: 100%;
  height: 600px;
}
</style>

