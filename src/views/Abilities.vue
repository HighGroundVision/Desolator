<template>
  <hgv-loading :urls="urls" v-on:loaded="loaded">
    <b-row>
      <b-col cols="4" offset="4">
        <b-dropdown id="axis" text="Select Axis to Sort" size="sm" block>
          <b-dropdown-item @click="changeSource(1)">Win Rate</b-dropdown-item>
          <b-dropdown-item @click="changeSource(2)">Pick Priority</b-dropdown-item>
          <b-dropdown-item @click="changeSource(3)">Strength</b-dropdown-item>
        </b-dropdown>
      </b-col>
    </b-row>
    <hr class="highlighted" />
    <b-row>
      <b-col>
        <div class="customChart" id="chartdiv"></div>
      </b-col>
    </b-row>
  </hgv-loading>
</template>

<script>
export default {
  name: 'abilities',
  data () {
    return {
      'urls': ['/static/chart-abilities-categories.json', '/static/chart-abilities-data.json'],
      'chart': null,
      'categories': [],
      'abilities': [],
    };
  },
  methods: {
    loaded(data) {
      this.categories = data[0];
      this.abilities = data[1];

      // allow re-render of DOM so chart elements are ready
      var self = this;
      this.$nextTick(function () {
        self.buildCharts();
        this.changeSource(1);
      });
    },
    buildCharts() {
      let am4charts = this.$am4charts;
      let am4core = this.$am4core;

      var interfaceColors = new am4core.InterfaceColorSet();
      this.chart = am4core.create("chartdiv", am4charts.XYChart);

      // the following line makes value axes to be arranged vertically.
      this.chart.bottomAxesContainer.layout = "horizontal";
      this.chart.bottomAxesContainer.reverseOrder = true;

      var categoryAxis = this.chart.yAxes.push(new am4charts.CategoryAxis());
      categoryAxis.dataFields.category = "category";
      categoryAxis.renderer.grid.template.stroke = interfaceColors.getFor("background");
      categoryAxis.renderer.grid.template.strokeOpacity = 1;
      categoryAxis.renderer.grid.template.location = 1;
      categoryAxis.renderer.minGridDistance = 20;

       var image = new am4core.Image();
      image.horizontalCenter = "middle";
      image.width = 20;
      image.height = 20;
      image.verticalCenter = "middle";
      image.adapter.add("href", (href, target)=>{
        let category = target.dataItem.category;
        if(category){
          let cat = this.categories.find(_ => _.name + " (" + _.id + ")" == category)
          if(cat) {
            return cat.image;
          } else {
            return href;
          }
        }
        return href;
      })
      categoryAxis.dataItems.template.bullet = image;
      categoryAxis.renderer.inversed = true;

      var valueAxis1 = this.chart.xAxes.push(new am4charts.ValueAxis());
      valueAxis1.tooltip.disabled = true;
      valueAxis1.renderer.baseGrid.disabled = true;
      valueAxis1.marginLeft = 30;
      valueAxis1.marginRight = 30;
      valueAxis1.title.text = "Win Rate";
      // valueAxis1.renderer.labels.template.disabled = true;
      valueAxis1.numberFormatter.numberFormat = "#%";

      var series1 = this.chart.series.push(new am4charts.LineSeries());
      series1.stroke = am4core.color("#9575CD");
      series1.strokeWidth = 3; 
      series1.dataFields.categoryY = "category";
      series1.dataFields.valueX = "value1";
      series1.xAxis = valueAxis1;
      series1.name = "Win Rate";
      var bullet1 = series1.bullets.push(new am4charts.CircleBullet());
      bullet1.fill = am4core.color("#9575CD");
      bullet1.tooltipText = "{valueX.value}";

      var valueAxis2 = this.chart.xAxes.push(new am4charts.ValueAxis());
      valueAxis2.renderer.baseGrid.disabled = true;
      valueAxis2.marginLeft = 30;
      valueAxis2.marginRight = 30;
      valueAxis2.title.text = "Pick Priority";
      valueAxis2.renderer.labels.template.disabled = true;
      valueAxis2.cursorTooltipEnabled = false;

      var series2 = this.chart.series.push(new am4charts.ColumnSeries());
      series2.columns.template.stroke = am4core.color("#673AB7");
      series2.columns.template.fill = am4core.color("#673AB7"); 
      series2.dataFields.categoryY = "category";
      series2.dataFields.valueX = "value2";
      series2.xAxis = valueAxis2;
      series2.name = "Pick Priority";

      var valueAxis3 = this.chart.xAxes.push(new am4charts.ValueAxis());
      valueAxis3.renderer.baseGrid.disabled = true;
      valueAxis3.marginLeft = 30;
      valueAxis3.title.text = "Strength";
      valueAxis3.renderer.labels.template.disabled = true;
      valueAxis3.cursorTooltipEnabled = false;

      var series3 = this.chart.series.push(new am4charts.ColumnSeries());
      series3.columns.template.stroke = am4core.color("#420F8D"); 
      series3.columns.template.fill = am4core.color("#420F8D"); 
      series3.dataFields.categoryY = "category";
      series3.dataFields.valueX = "value3";
      series3.xAxis = valueAxis3;
      series3.name = "Strength";
 
    },
    setData(source) {
      var data = [];
      for (const ability of source) {
        data.push({ category: ability.name + " (" + ability.id + ")", value1: ability.win_rate, value2:ability.draft_order, value3:ability.most_kills });
      }
      this.chart.data = data;
    },
    changeSource(type) {
      let data = this.abilities;
      if(type == 1) {
        data.sort((lhs, rhs) => rhs.win_rate - lhs.win_rate);
      } else if(type == 2) {
        data.sort((lhs, rhs) => rhs.draft_order - lhs.draft_order);
      } else if(type == 3) {
        data.sort((lhs, rhs) => rhs.most_kills - lhs.most_kills);
      }
      this.setData(data.slice(0, 25));
    }
  }
}
</script>

<style scoped>
.customChart {
  width: 100%;
  height: 750px;
}
</style>