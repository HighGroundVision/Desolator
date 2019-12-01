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
      'urls': ['/static/heroes-chart.json', '/static/summary-heroes.json'],
      'heroes': [],
      'summary': null,
      'search': null,
      'results': [],
    };
  },
  methods: {
    loaded(data) {
      this.heroes = data[0];
      this.summary = data[1];

      // allow re-render of DOM so chart elements are ready
      var self = this;
      this.$nextTick(function () {
        self.buildCharts();
      });
    },
    buildCharts() {
      let am4charts = this.$am4charts;
      let am4core = this.$am4core;

      var collection = [
        {
          data: this.heroes.filter(_ => _.attribute == 1 && _.region == 2),
          title: "Str Heroes",
          color: "#C63016",
        },
        {
          data: this.heroes.filter(_ => _.attribute == 2 && _.region == 2),
          title: "Agi Heroes",
          color: "#13B63D",
        },
        {
          data: this.heroes.filter(_ => _.attribute == 3 && _.region == 2),
          title: "Int Heroes",
          color: "#1671C6",
        }
      ];

      // Create chart instance
      var container = am4core.create("chartdiv", am4core.Container);
      container.layout = "vertical";
      container.fixedWidthGrid = false;
      container.width = am4core.percent(100);
      container.height = am4core.percent(100);

      for (const item of collection) {
        // Create chart instance
        var chart = container.createChild(am4charts.XYChart);
        chart.width = am4core.percent(100);
        chart.height = 200;

        chart.colors.list = [am4core.color(item.color)];

        // Add data
        item.data.sort((lhs, rhs) => lhs.name.localeCompare(rhs.name));
        chart.data = item.data;

        // Create axes
        var categoryAxis = chart.xAxes.push(new am4charts.CategoryAxis());
        categoryAxis.dataFields.category = "name";
        categoryAxis.renderer.grid.template.disabled = false;
        categoryAxis.renderer.minGridDistance = 1;
        categoryAxis.renderer.inside = false;
        categoryAxis.renderer.labels.template.fill = am4core.color("#fff");
        categoryAxis.renderer.labels.template.fontSize = 20;
        categoryAxis.hidden = true;

        var valueAxis = chart.yAxes.push(new am4charts.ValueAxis());
        valueAxis.renderer.grid.template.strokeDasharray = "4,4";
        valueAxis.renderer.labels.template.disabled = false;
        valueAxis.baseValue = 0.5;
        valueAxis.min = 0.4;
        valueAxis.max = 0.6;
        valueAxis.strictMinMax = true;
        valueAxis.title.text = item.title;
        valueAxis.title.rotation = 0;

        // formating 
        chart.numberFormatter.numberFormat = "#.0%";

        // Do not crop bullets
        chart.maskBullets = false;

        // Remove padding
        chart.paddingBottom = 0;

        // Create series
        var series = chart.series.push(new am4charts.ColumnSeries());
        series.dataFields.valueY = "win_rate";
        series.dataFields.categoryX = "name";
        series.columns.template.propertyFields.fill = "color";
        series.columns.template.propertyFields.stroke = "color";
        series.columns.template.tooltipText = "{categoryX}: [bold]{valueY}[/b]";
        series.columns.template.events.on("hit", function(ev) {
          var id = ev.target.dataItem.dataContext.id;
          this.$router.push('/hero/' + id);
        }, this);

        // Add bullets
        var bullet = series.bullets.push(new am4charts.Bullet());
        var image = bullet.createChild(am4core.Image);
        image.width = 20;
        image.height = 20;
        image.horizontalCenter = "middle";
        image.verticalCenter = "bottom";
        image.dy = 10;
        image.y = am4core.percent(100);
        image.propertyFields.href = "image";
        image.tooltipText = series.columns.template.tooltipText;
        image.propertyFields.fill = "color";
        image.filters.push(new am4core.DropShadowFilter());
        image.events.on("hit", function(ev) {
          var id = ev.target.dataItem.dataContext.id;
          this.$router.push('/hero/' + id);
        }, this);
      }
    },
    findHero() {
      if(this.search) {
        let s = this.search.toLowerCase();
        this.results = this.heroes.filter(_ => _.region == 0 &&_.name.toLowerCase().includes(s)).slice(0, 5);
      } else {
        this.results = [];
      }
    },
  }
}
</script>

<style scoped>
.customChart {
  width: 100%;
  height: 600px;
}
</style>