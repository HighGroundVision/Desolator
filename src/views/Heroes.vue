<template>
  <b-row>
    <b-col cols="4" lg="3">
      <div class="text-center">
        <b-alert variant="info" show>   
          <p>
            Select a hero to unlock additional knowledge!
          </p>
        </b-alert>
        <img src="@/assets/imgs/cluckles-speach.png" class="cluckles-speach" />
      </div>
    </b-col>
    <b-col>
      <h4  class="text-center">Heroes</h4>
      <hr class="highlighted" />
      <p>Aliquam imperdiet ultricies ultricies. In vel lorem fermentum, commodo sem eu, accumsan dui. Donec hendrerit eleifend ligula eget ultrices. Praesent nec porta nulla. Donec sodales ex a maximus varius. Curabitur maximus dapibus feugiat. Nam suscipit augue vitae suscipit pulvinar.</p>
      <b-card bg-variant="dark">
        <b-card-body>
          <div class="customChart" ref="chartdiv1"></div>
          <hr />
          <div class="customChart" ref="chartdiv2"></div>
          <hr />
          <div class="customChart" ref="chartdiv3"></div>
        </b-card-body>
      </b-card>
    </b-col>
  </b-row>
</template>

<script>
import * as am4core from "@amcharts/amcharts4/core";
import * as am4charts from "@amcharts/amcharts4/charts";
import am4themes_animated from "@amcharts/amcharts4/themes/animated";

import numeral from 'numeral'
import heroes from '@/assets/data/hero-collection.json'
// $route.params.id

// am4core.useTheme(am4themes_animated);

export default {
  name: 'heroes',
  data () {
    return {};
  },
  mounted() {
    {
      // Create chart instance
      var chart = am4core.create(this.$refs.chartdiv1, am4charts.XYChart);

      var title = chart.titles.create();
      title.text = "Str Heroes";
      title.fontSize = 20;

      // Add data
      chart.data = heroes.filter(_ => _.attribute_primary == "DOTA_ATTRIBUTE_STRENGTH");

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
      valueAxis.min = -10;
      valueAxis.max = 10;
      valueAxis.strictMinMax = true;

      // Do not crop bullets
      chart.maskBullets = false;

      // Remove padding
      chart.paddingBottom = 0;

      // Create series
      var series = chart.series.push(new am4charts.ColumnSeries());
      series.dataFields.valueY = "win_rate_delta";
      series.dataFields.categoryX = "name";
      series.columns.template.propertyFields.fill = "color";
      series.columns.template.propertyFields.stroke = "color";
      //series.columns.template.column.cornerRadiusTopLeft = 15;
      //series.columns.template.column.cornerRadiusTopRight = 15;
      series.columns.template.tooltipText = "{categoryX}: [bold]{valueY}[/b]";

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
    }
    {
      // Create chart instance
      var chart = am4core.create(this.$refs.chartdiv2, am4charts.XYChart);

      var title = chart.titles.create();
      title.text = "Agi Heroes";
      title.fontSize = 20;

      // Add data
      chart.data = heroes.filter(_ => _.attribute_primary == "DOTA_ATTRIBUTE_AGILITY");

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
      valueAxis.min = -10;
      valueAxis.max = 10;
      valueAxis.strictMinMax = true;

      // Do not crop bullets
      chart.maskBullets = false;

      // Remove padding
      chart.paddingBottom = 0;

      // Create series
      var series = chart.series.push(new am4charts.ColumnSeries());
      series.dataFields.valueY = "win_rate_delta";
      series.dataFields.categoryX = "name";
      series.columns.template.propertyFields.fill = "color";
      series.columns.template.propertyFields.stroke = "color";
      //series.columns.template.column.cornerRadiusTopLeft = 15;
      //series.columns.template.column.cornerRadiusTopRight = 15;
      // series.columns.template.tooltipText = "{categoryX}: [bold]{valueY}[/b]";

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
    }
    {
      // Create chart instance
      var chart = am4core.create(this.$refs.chartdiv3, am4charts.XYChart);

      var title = chart.titles.create();
      title.text = "Int Heroes";
      title.fontSize = 20;

      // Add data
      chart.data = heroes.filter(_ => _.attribute_primary == "DOTA_ATTRIBUTE_INTELLECT");

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
      valueAxis.min = -10;
      valueAxis.max = 15;
      valueAxis.strictMinMax = true;

      // Do not crop bullets
      chart.maskBullets = false;

      // Remove padding
      chart.paddingBottom = 0;

      // Create series
      var series = chart.series.push(new am4charts.ColumnSeries());
      series.dataFields.valueY = "win_rate_delta";
      series.dataFields.categoryX = "name";
      series.columns.template.propertyFields.fill = "color";
      series.columns.template.propertyFields.stroke = "color";
      //series.columns.template.column.cornerRadiusTopLeft = 15;
      //series.columns.template.column.cornerRadiusTopRight = 15;
      // series.columns.template.tooltipText = "{categoryX}: [bold]{valueY}[/b]";

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
    }

  }
}
</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped>
.customChart {
  width: 100%;
  height: 200px;
}
</style>