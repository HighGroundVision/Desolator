<template>
  <hgv-loading :urls="urls" v-on:loaded="loaded">
   <b-row>
    <b-col cols="4" lg="3">
      <div class="text-center">
        <b-alert variant="info" show>
          <span>The AD community is small. Understand who is playing where and when so you do not get left out.</span>
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
        <h4  class="text-center">Calendar / Matches</h4>
        <hr class="highlighted" />
      </div>
      <p>
        We are often asked when do people play AD?
        To that end we have included the daily counts broken down by the time of day matches are being played with the times, offset to match your browser's timezone.
        We also include the total # matches per region.
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
        <div class="customChart" id="chart"></div>
      </b-card>
      <br />
      <p>
        We reset our baseline when a major patch is released that changes the balance of abilities.
        We normally export our master database approximately once a week.
        Our current stats collection was started on <b class="text-info">{{ formatDateTime(range.start) }}</b> and was last exported on <b class="text-info">{{ formatDateTime(range.end) }}</b>, that is ~ <b class="text-info">{{ formatDuration(range.start, range.end) }}</b>, with a total of <b class="text-info">{{ formatNumber(range.matches) }}</b> AD matches processed. 
        But we did notice that <b class="text-info">{{ formatPercentage(range.abandoned_ratio) }}</b> of matches where abandoned, you can do better people!
      </p>
    </b-col>
  </b-row>
  </hgv-loading>
</template>

<script>
import axios from 'axios'
import * as am4core from "@amcharts/amcharts4/core";
import * as am4charts from "@amcharts/amcharts4/charts";
import am4themes_animated from "@amcharts/amcharts4/themes/animated";
import { timeout } from 'q';
import { setTimeout } from 'timers';

am4core.useTheme(am4themes_animated);

export default {
  name: 'calendar',
  data () {
    return {
      'urls': ['/static/schedule.json', '/static/regions.json'],
      'regions': null,
      'schedule': [],
      'range':  {
        "start": "",
        "end": "",
        "matches": 0,
        "abandoned_ratio": 0
      },
      'regionSelected': 2,
      'regionOptions': []
    }
  },
  methods: {
    loaded(data) {
      var self = this;
      self.regions = data[0].regions;
      self.schedule = data[0].schedule;
      self.range = data[0].range;

      var _regions = data[1];
      for (const key in _regions) {
        if (_regions.hasOwnProperty(key)) {
          const element = _regions[key];
          self.regionOptions.push({value: key, text: element })
        }
      }

      // correct hour form UTC to local time based on browser's timezone.
      var x = new Date();
      var currentTimeZoneOffsetInHours = (x.getTimezoneOffset() / 60) * -1;
      for (let i = 0; i < self.schedule.length; i++) {
        let offset = self.schedule[i].hour + currentTimeZoneOffsetInHours;
        if(offset < 0) offset = 24 + offset;
        self.schedule[i].hour = offset;
      }
      self.schedule.sort((lhs, rhs) => lhs.hour - rhs.hour);
      
      // allow re-render of DOM so chart elements are ready
      this.$nextTick(function () {
        self.buildCharts();
      });
    },
    buildCharts() {
      // Create chart instance
      var chart = am4core.create('chart', am4charts.XYChart);

      let data = this.schedule.filter(_ => _.region == this.regionSelected);
      chart.data = data;

      let chartElements = [
        { day: 1, title: 'Monday', color: '#FF3333', },
        { day: 2, title: 'Tuesday', color: '#FFC433',  },
        { day: 3, title: 'Wednesday',  color: '#D8DA43', },
        { day: 4, title: 'Thursday', color: '#33FF61',  },
        { day: 5, title: 'Friday', color: '#33C7FF',  },
        { day: 6, title: 'Saturday',  color: '#4233FF',  },
        { day: 0, title: 'Sunday', color: '#D133FF',  },
      ];

      // Create axes
      var categoryAxis = chart.xAxes.push(new am4charts.CategoryAxis());
      categoryAxis.dataFields.category = "hour";
      categoryAxis.numberFormatter.numberFormat = "#";
      categoryAxis.renderer.grid.template.location = 0;
      categoryAxis.renderer.cellStartLocation = 0.1;
      categoryAxis.renderer.cellEndLocation = 0.9;
      categoryAxis.renderer.minGridDistance = 10;

      var valueAxis = chart.yAxes.push(new am4charts.ValueAxis());
     
      chart.legend = new am4charts.Legend();

      for (let i = 0; i < chartElements.length; i++) {
        const item = chartElements[i];

        // Create series
        var series = chart.series.push(new am4charts.ColumnSeries());
        // series.hidden = true;
        series.data = data.filter(_ => _.day == item.day);
        series.dataFields.categoryX = "hour";
        series.dataFields.valueY = "matches";
        series.name = item.title;
        series.columns.template.tooltipText = "{name} {categoryX}: [bold]{valueY}[/]";
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
  height: 450px;
}
</style>
