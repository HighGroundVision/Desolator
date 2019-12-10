<template>
  <hgv-loading :urls="urls" v-on:loaded="loaded">
    <div class="customChart" id="chartdiv"></div>
  </hgv-loading>
</template>

<script>
import axios from 'axios'

export default {
  name: 'pool',
  data () {
    return {
      'urls': ['/static/chart-heroes-categories.json', '/static/chart-heroes-data.json'],
      'categories': [],
      'heroes': [],
    };
  },
  methods: {
    loaded(data) {
      this.categories = data[0];

      this.heroes = data[1];
      this.heroes.sort((lhs, rhs) => lhs.name.localeCompare(rhs.name));

      // allow re-render of DOM so chart elements are ready
      var self = this;
      this.$nextTick(function () {
        self.buildCharts();
      });
    },
    buildCharts() {
      let am4charts = this.$am4charts;
      let am4core = this.$am4core;

      var chart = am4core.create("chartdiv", am4charts.XYChart);
      chart.numberFormatter.numberFormat = "#%";

      var data = [];
      for (const hero of this.heroes) {
        data.push({ category: hero.name, open: hero.current, close: hero.preivous });
      }

      chart.data = data;

      var categoryAxis = chart.yAxes.push(new am4charts.CategoryAxis());
      categoryAxis.renderer.grid.template.location = 0;
      categoryAxis.renderer.ticks.template.disabled = true;
      categoryAxis.renderer.axisFills.template.disabled = true;
      categoryAxis.dataFields.category = "category";
      categoryAxis.renderer.minGridDistance = 15;
      categoryAxis.renderer.inversed = true;
      categoryAxis.renderer.inside = true;
      categoryAxis.renderer.grid.template.location = 0.5;
      categoryAxis.renderer.grid.template.strokeDasharray = "1,3";

      var image = new am4core.Image();
      image.horizontalCenter = "middle";
      image.width = 20;
      image.height = 20;
      image.verticalCenter = "middle";
      image.adapter.add("href", (href, target)=>{
        let category = target.dataItem.category;
        if(category){
          let cat = this.categories.find(_ => _.name == category)
          return cat.image;
        }
        return href;
      })
      categoryAxis.dataItems.template.bullet = image;

      var valueAxis = chart.xAxes.push(new am4charts.ValueAxis());
      valueAxis.tooltip.disabled = true;
      valueAxis.renderer.ticks.template.disabled = true;
      valueAxis.renderer.axisFills.template.disabled = true;
      valueAxis.renderer.opposite = true;
      valueAxis.title.text = "Win Rate";

      var series = chart.series.push(new am4charts.ColumnSeries());
      series.dataFields.categoryY = "category";
      series.dataFields.openValueX = "open";
      series.dataFields.valueX = "close";
      series.tooltipText = "low: {openValueX.value} high: {valueX.value}";
      series.sequencedInterpolation = true;
      series.fillOpacity = 0;
      series.strokeOpacity = 1;
      series.stroke = "#000";
      series.columns.template.height = 0.01;
      series.tooltip.pointerOrientation = "vertical";

      var preivousBullet = series.bullets.create(am4charts.CircleBullet);
      preivousBullet.fill = "#606060";
      preivousBullet.stroke = "#000";

      var currentBullet = series.bullets.create(am4charts.CircleBullet);
      currentBullet.locationX = 1;
      currentBullet.fill = "#ffc107";
      currentBullet.stroke = "#000";
      currentBullet.adapter.add("fill", (fill, target, key) => {
        let delta = target.dataItem.values.valueX.value - target.dataItem.values.openValueX.value;
        if(delta > 0) {
          return "#C63016";
        } else if(delta < 0) {
          return "#13B63D";
        } else {
          return fill;
        }
      });
      
      chart.legend = new am4charts.Legend();
      chart.legend.data = [{
        "name": "Previous Winrate",
        "fill": "#606060"
      },{
        "name": "Current Winrate (+)",
        "fill": "#28a745"
      }, {
        "name": "Current Winrate (-)",
        "fill": "#dc3545"
      }];
    },
  }
}
</script>

<style scoped>
.customChart {
  width: 100%;
  height: 3000px;
}
</style>