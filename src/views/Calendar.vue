<template>
  <b-row>
    <b-col cols="4" lg="3">
      <div class="text-center">
        <b-alert variant="info" show>
          <span>The AD community is small. Understand who is playing where and whens so you do not get left out.</span>
        </b-alert>
        <img src="@/assets/imgs/cluckles-speach.png" class="cluckles-speach" />
      </div>
      <br />
      <h5>Matches / Regions</h5>
      <table>
        <template v-for="(value, key, index) in regions">
          <tr :key="index">
            <td>{{key}}</td>
            <td><b-badge>{{value}}</b-badge></td>
          </tr>
        </template>
      </table>
    </b-col>
    <b-col>
      <div>
        <h4  class="text-center">Matches</h4>
        <hr class="highlighted" />
      </div>
      <p>
        We are often asked when do people play AD?
        To that end we have included the daily counts as a percentage of the total matches.
        We also break down the time of day matches are being played with the times offset by the the browsers timezone.
        There is always going to be a little bias based on day and time we run the export.
      </p>
      <div>
        <b-form-group label="Selected Region">
          <b-form-select v-model="regionSelected" :options="regionOptions" @change="buildCharts">
          </b-form-select>
        </b-form-group>
      </div>
      <br />
      <b-card bg-variant="dark">
        <div class="customChart" ref="chartMon"></div>
        <div class="customChart" ref="chartTus"></div>
        <div class="customChart" ref="chartWed"></div>
        <div class="customChart" ref="chartThr"></div>
        <div class="customChart" ref="chartFri"></div>
        <div class="customChart" ref="chartSat"></div>
        <div class="customChart" ref="chartSun"></div>
      </b-card>
    </b-col>
  </b-row>
</template>

<script>
import axios from 'axios'
import * as am4core from "@amcharts/amcharts4/core";
import * as am4charts from "@amcharts/amcharts4/charts";
import am4themes_animated from "@amcharts/amcharts4/themes/animated";

am4core.useTheme(am4themes_animated);

export default {
  name: 'calendar',
  data () {
    return {
      'regions': null,
      'schedule': [],
      'regionSelected': 2,
      'regionOptions': [],
    }
  },
  async mounted() {
    var response = await axios.get('/static/schedule.json');
    this.regions = response.data.regions;
    this.schedule =  response.data.schedule;

    var response = await axios.get('/static/regions.json');
    for (const key in response.data) {
      if (response.data.hasOwnProperty(key)) {
        const element = response.data[key];
        this.regionOptions.push({value: key, text: element })
      }
    }

    this.buildCharts();
  },
  methods: {
    buildCharts() {
      var data = this.schedule.filter(_ => _.region == this.regionSelected);

      let chartElements = [
        { day: 1, title: 'Monday', element: this.$refs.chartMon, color: '#E6E6FA', },
        { day: 2, title: 'Tuesday', element: this.$refs.chartTus, color: '#EE82EE',  },
        { day: 3, title: 'Wednesday', element: this.$refs.chartWed, color: '#FF00FF',  },
        { day: 4, title: 'Thursday', element: this.$refs.chartThr, color: '#BA55D3',  },
        { day: 5, title: 'Friday', element: this.$refs.chartFri, color: '#9370DB',  },
        { day: 6, title: 'Saturday', element: this.$refs.chartSat, color: '#800080',  },
        { day: 0, title: 'Sunday', element: this.$refs.chartSun, color: '#4B0082',  },
      ];

      var x = new Date();
      var currentTimeZoneOffsetInHours = (x.getTimezoneOffset() / 60) * -1;

      for (let i = 0; i < chartElements.length; i++) {
        const item = chartElements[i];
        
        // Create chart instance
        var chart = am4core.create(item.element, am4charts.XYChart);

        var title = chart.titles.create();
        title.text = item.title;

        // Add data
        let local = data.filter(_ => _.day == item.day);
        for (const item of local) {
          let offset = item.hour + currentTimeZoneOffsetInHours;
          if(offset < 0)
            offset = 24 + offset;
          item.hour = offset;
        }
        local.sort((lhs, rhs) => lhs.hour - rhs.hour);

        chart.data = local;

        // Create axes
        var categoryAxis = chart.xAxes.push(new am4charts.CategoryAxis());
        categoryAxis.dataFields.category = "hour";
        categoryAxis.numberFormatter.numberFormat = "#";
        categoryAxis.renderer.grid.template.location = 0;
        categoryAxis.renderer.cellStartLocation = 0.1;
        categoryAxis.renderer.cellEndLocation = 0.9;
        categoryAxis.renderer.minGridDistance = 10;

        var valueAxis = chart.yAxes.push(new am4charts.ValueAxis()); 

        // Create series
        var series = chart.series.push(new am4charts.ColumnSeries());
        series.dataFields.categoryX = "hour";
        series.dataFields.valueY = "matches";
        series.name = "Hours";
        series.clustered = true;
        series.columns.template.tooltipText = "Hour {categoryX}: [bold]{valueY}[/]";
        series.fill = am4core.color(item.color);
        series.stroke = am4core.color(item.color);
      }
    }
  }
}
</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped>
.customChart {
  width: 100%;
  height: 150px;
}
</style>
