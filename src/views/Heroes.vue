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
        "name": "Last Week Winrate",
        "fill": "#606060"
      },{
        "name": "Current Week Winrate Higher",
        "fill": "#28a745"
      }, {
        "name": "Current Week Winrate Lower",
        "fill": "#dc3545"
      }];
    },
    /*
    buildCharts() {
      let am4charts = this.$am4charts;
      let am4core = this.$am4core;

      var collection = [
        {
          data: this.heroes.filter(_ => _.attribute == 1),
          title: "Str Heroes",
          color: "#C63016",
        },
        {
          data: this.heroes.filter(_ => _.attribute == 2),
          title: "Agi Heroes",
          color: "#13B63D",
        },
        {
          data: this.heroes.filter(_ => _.attribute == 3),
          title: "Int Heroes",
          color: "#1671C6",
        }
      ];

      // Create chart instance
      var container = am4core.create("chartdiv", am4core.Container);
      container.layout =  "vertical" 
      container.fixedWidthGrid = false;
      container.width = am4core.percent(100);
      container.height = am4core.percent(100);

      for (const item of collection) {
        // Create chart instance
        var chart = container.createChild(am4charts.XYChart);
        chart.width = am4core.percent(100);
        chart.height = 250;

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
        valueAxis.min = 0.3;
        valueAxis.max = 0.7;
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
          // var id = ev.target.dataItem.dataContext.id;
          // this.$router.push('/hero/' + id);
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
          // var id = ev.target.dataItem.dataContext.id;
          // this.$router.push('/hero/' + id);
        }, this);
      }


    }
    */
  }
}
</script>

<style scoped>
.customChart {
  width: 100%;
  height: 3000px;
}
</style>