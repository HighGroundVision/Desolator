<template>
  <hgv-loading :urls="urls" v-on:loaded="loaded">
    <b-row>
      <b-col cols="12">
        <h4 class="text-center">Matchs / Region</h4>
      </b-col>
    </b-row>
    <hr class="highlighted" />
    <b-row>
      <b-col cols="4" offset="4">
        <b-dropdown id="regions" :text="(this.selection) ? this.selection.region_name : 'Select Region'" size="sm" block>
          <template v-for="(item) in summary">
            <b-dropdown-item v-bind:key="item.region_id" @click="changeRegion(item)">{{item.region_name}} <b-badge class="float-right">{{item.total}}</b-badge></b-dropdown-item>
          </template>
        </b-dropdown>
      </b-col>
    </b-row>
    <hr class="highlighted" />
    <b-row>
      <b-col cols="12">
        <div class="customChart" id="chartdiv"></div>
      </b-col>
    </b-row>
  </hgv-loading>
</template>

<script>
import axios from 'axios'
import moment from 'moment-timezone';

export default {
  name: 'timeline',
  data () {
    return {
      'urls': ['/static/regions-summary.json', '/static/region-chart.json', '/static/regions-chart.json'],
      'chart': null,
      'selection': null,
      'summary': [],
      'world': [],
      'regions': [],
    };
  },
  methods: {
    loaded(data) {
      this.summary = data[0];
      this.world = data[1];
      this.regions = data[2];

      // allow re-render of DOM so chart elements are ready
      var self = this;
      this.$nextTick(function () {
        self.buildCharts();
        self.loadData();
      });
    },
    buildCharts() {
      let am4charts = this.$am4charts;
      let am4core = this.$am4core;
      
      if(this.chart) {
        this.chart.dispose();
      }
      
      this.chart = am4core.create("chartdiv", am4charts.XYChart);
      this.chart.maskBullets = false;

      var xAxis = this.chart.xAxes.push(new am4charts.CategoryAxis());
      var yAxis = this.chart.yAxes.push(new am4charts.CategoryAxis());

      yAxis.dataFields.category = "day";
      xAxis.renderer.minGridDistance = 40;
      xAxis.dataFields.category = "hour";
      xAxis.renderer.grid.template.disabled = true;
      yAxis.renderer.grid.template.disabled = true;
      xAxis.renderer.axisFills.template.disabled = true;
      yAxis.renderer.axisFills.template.disabled = true;
      yAxis.renderer.ticks.template.disabled = true;
      xAxis.renderer.ticks.template.disabled = true;
      // xAxis.title.text = "Time (UTC)";
      yAxis.renderer.inversed = true;
      xAxis.renderer.labels.template.rotation = 45;

      var series = this.chart.series.push(new am4charts.ColumnSeries());
      series.dataFields.categoryY = "day";
      series.dataFields.categoryX = "hour";
      series.dataFields.value = "value";
      series.columns.template.disabled = true;
      series.sequencedInterpolation = true;

      var bullet = series.bullets.push(new am4core.Circle());
      bullet.tooltipText = "{day}, {hour}: {value.workingValue.formatNumber('#.')}";
      bullet.strokeWidth = 3;
      bullet.fill = am4core.color("#ffc107");
      bullet.stroke = am4core.color("#ffffff");
      bullet.strokeOpacity = 0;

      bullet.adapter.add("fill", (fill, target) => {
        let index = target.dataItem.index
        let lastValue = this.chart.data[index - 24];
        if(lastValue) {
          let currentValue = target.dataItem.values.value.value;
          if(currentValue > lastValue.value) {
            return am4core.color("#28a745");
          } else if(currentValue < lastValue.value) { 
            return am4core.color("#dc3545");
          } else {
            return fill;  
          }
        } else {
          return fill;
        }
      });

      bullet.adapter.add("tooltipY", function(tooltipY, target) {
        return -target.radius + 1;
      });

      series.heatRules.push({
        property: "radius",
        target: bullet,
        min: 0,
        max: 30
      });

      bullet.hiddenState.properties.scale = 0.01;
      bullet.hiddenState.properties.opacity = 1;

      var hoverState = bullet.states.create("hover");
      hoverState.properties.strokeOpacity = 1;

      this.chart.legend = new am4charts.Legend();
      this.chart.legend.data = [{
        "name": "No Change",
        "fill": "#ffc107"
      }, {
        "name": "More Matches",
        "fill": "#28a745"
      }, {
        "name": "Less Matches",
        "fill": "#dc3545"
      }];
    },
    setData(source) {
      let tz = moment.tz.guess();

      var data = source.map(_ => {
        let tz = moment.tz.guess();

        var date = moment.utc(_.date).tz(tz);
        // var date = moment.utc(_.date);
        return {
          date: date,
          hour: date.format("hA z"), // hA z
          day: date.format("MMM Do"),
          value: _.value
        };
      });

      let first = data[0];
      if(first) {
        let date = first.date.clone();
        for (let h = date.hour(); h > 0; h--) {
          date.subtract(1, 'hours');
          data.unshift({
            date: date.clone(),
            hour: date.format("hA z"), // hA z
            day: date.format("MMM Do"),
            value: 0
          });
        }
      }
        
      this.chart.data = data;
    },
    loadData() {
      this.setData(this.world);
    },
    changeRegion(item) {
      this.selection = item;

      if(item.region_id == -1) {
         this.setData(this.world);
      } else {
        let source = this.regions.filter(_ => _.region_id == item.region_id);
        this.setData(source);
      }
    },
  },
}
</script>

<style scoped>
.customChart {
  width: 100%;
  height: 725px;
}
</style>