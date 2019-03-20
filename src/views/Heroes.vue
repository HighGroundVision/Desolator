<template>
  <section>
    <div>
      <h4  class="text-center">Heroes</h4>
      <hr class="highlighted" />
    </div>
    <b-row>
      <b-col cols="4" lg="3">
        <div class="text-center">
          <b-alert variant="info" show>   
            <p>
              Search for a hero to unlock additional knowledge!
            </p>
          </b-alert>
          <img src="@/assets/imgs/cluckles-speach.png" class="cluckles-speach" />
        </div>
      </b-col>
      <b-col>
        <b-form @submit.prevent="findHero">
          <b-input-group>
            <b-form-input type="text" v-model="search" />
            <b-input-group-addon>
              <b-button variant="success" @click="findHero">Find</b-button>
            </b-input-group-addon>
          </b-input-group>
        </b-form>
        <br />
        <ul class="list-unstyled">
          <template v-for="(value) in heroes">
            <li :key="value.id">
              <img :src="value.image" class="m-1"/>
              <b-link :to="'/hero/' + value.id">{{value.name}}</b-link>
            </li>
          </template>
        </ul>
      </b-col>
    </b-row>
    <div>
      <hr class="highlighted" />
      <p>
        We have charted the heroes <b class="text-info">Win Rate</b> as delta from <b class="text-info">50%</b>.
        This should provide a clear picture of which heroes are doing better then others. 
        We have grouped the heroes together by type to make it simpler to compare.
        We have also <b class="text-warning">highlighted</b> the heroes that are below the average number of picks.
        These are heroes that do not show up as often in a the draft pool.
      </p>
      <b-card bg-variant="dark">
        <div class="customChart" ref="chartdiv1"></div>
        <hr />
        <div class="customChart" ref="chartdiv2"></div>
        <hr />
        <div class="customChart" ref="chartdiv3"></div>
      </b-card>
    </div>
  </section>
</template>

<script>
import * as am4core from "@amcharts/amcharts4/core";
import * as am4charts from "@amcharts/amcharts4/charts";
import am4themes_animated from "@amcharts/amcharts4/themes/animated";

import numeral from 'numeral'
import heroes from '@/assets/data/hero-collection.json'
// $route.params.id

am4core.useTheme(am4themes_animated);

export default {
  name: 'heroes',
  data () {
    return {
      search: null,
      heroes: [],
    };
  }, 
  mounted() {
    var collection = [
      {
        element: this.$refs.chartdiv1,
        data: heroes.filter(_ => _.attribute_primary == "DOTA_ATTRIBUTE_STRENGTH"),
        title: "Str Heroes",
      },
      {
        element: this.$refs.chartdiv2,
        data: heroes.filter(_ => _.attribute_primary == "DOTA_ATTRIBUTE_AGILITY"),
        title: "Agi Heroes",
      },
      {
        element: this.$refs.chartdiv3,
        data: heroes.filter(_ => _.attribute_primary == "DOTA_ATTRIBUTE_INTELLECT"),
        title: "Int Heroes",
      }
    ];
    
    for (const item of collection) {
      // Create chart instance
      var chart = am4core.create(item.element, am4charts.XYChart);

      var title = chart.titles.create();
      title.text = item.title;
      title.fontSize = 20;

      // Add data
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
  },
  methods: {
    findHero() {
      if(this.search) {
        let s = this.search.toLowerCase();
        this.heroes = heroes.filter(_ => _.name.toLowerCase().includes(s)).slice(0, 5);
      } else {
        this.heroes = [];
      }
    },
  }
}
</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped>
.customChart {
  width: 100%;
  height: 200px;
}
.card-body {
  padding: 0px !important;
}
</style>