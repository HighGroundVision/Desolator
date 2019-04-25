<template>
  <hgv-loading :urls="urls" v-on:loaded="loaded">
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
          <b-input-group prepend="Search by Hero">
            <b-form-input type="text" v-model="search"/>
            <b-input-group-append>
              <b-button variant="success" @click="findHero">Find</b-button>
            </b-input-group-append>
          </b-input-group>
        </b-form>
        <br />
        <ul class="list-unstyled">
          <template v-for="(value) in results">
            <li :key="value.id">
              <img :src="value.image" class="m-1"/>
              <b-link :to="'/hero/' + value.id">{{value.name}}</b-link>
            </li>
          </template>
        </ul>
      </b-col>
    </b-row>
    <hr class="highlighted" />
    <div v-if="summary">
      <p>
        Lorem ipsum dolor sit amet, consectetur adipiscing elit. Donec et luctus purus. Phasellus hendrerit leo in eros luctus mollis. Praesent consectetur metus mauris, id maximus felis sodales vel. Praesent vehicula sagittis urna, at ultrices quam aliquet quis. Donec vitae turpis non urna ultrices accumsan. In hac habitasse platea dictumst. Phasellus ac venenatis libero. Duis hendrerit id erat eleifend tempor. Sed vehicula massa ac justo dictum, at elementum ligula facilisis. Suspendisse potenti. Nunc euismod rhoncus arcu, et luctus arcu cursus nec.
      </p>
      <b-row>
        <b-col>
          <h5 class="text-center">Wins</h5>
          <table class="table table-sm">
            <template v-for="(value, index) in summary['wins']">
              <tr :key="index">
                <td style="width: 35px">
                  <i v-if="index == 0" class="fas fa-2x fa-award" style="color: #FFD700;" title="1st"></i>
                  <i v-if="index == 1" class="fas fa-2x fa-award" style="color: #C0C0C0;" title="2nd"></i>
                  <i v-if="index == 2" class="fas fa-2x fa-award" style="color: #CD7F32;" title="3rd"></i>
                </td>
                <td style="width: 35px">
                  <img :src="value.icon" class="ability-icon-sm"/>
                </td>
                <td>
                  <b-link :to="'/hero/' + value.id">{{value.name}}</b-link>
                </td>
                <td>
                  <span>{{formatNumber(value.wins)}}</span>
                </td>
              </tr>
            </template>
          </table>
        </b-col>
        <b-col>
          <h5 class="text-center">Picks</h5>
          <table class="table table-sm">
            <template v-for="(value, index) in summary['picks']">
              <tr :key="index">
                <td style="width: 35px">
                  <i v-if="index == 0" class="fas fa-2x fa-award" style="color: #FFD700;" title="1st"></i>
                  <i v-if="index == 1" class="fas fa-2x fa-award" style="color: #C0C0C0;" title="2nd"></i>
                  <i v-if="index == 2" class="fas fa-2x fa-award" style="color: #CD7F32;" title="3rd"></i>
                </td>
                <td style="width: 35px">
                  <img :src="value.icon" class="ability-icon-sm"/>
                </td>
                <td>
                  <b-link :to="'/hero/' + value.id">{{value.name}}</b-link>
                </td>
                <td>
                  <span>{{formatNumber(value.picks)}}</span>
                </td>
              </tr>
            </template>
          </table>
        </b-col>
        <b-col>
          <h5 class="text-center">Kills</h5>
          <table class="table table-sm">
            <template v-for="(value, index) in summary['kills']">
              <tr :key="index">
                <td style="width: 35px">
                  <i v-if="index == 0" class="fas fa-2x fa-award" style="color: #FFD700;" title="1st"></i>
                  <i v-if="index == 1" class="fas fa-2x fa-award" style="color: #C0C0C0;" title="2nd"></i>
                  <i v-if="index == 2" class="fas fa-2x fa-award" style="color: #CD7F32;" title="3rd"></i>
                </td>
                <td style="width: 35px">
                  <img :src="value.icon" class="ability-icon-sm"/>
                </td>
                <td>
                  <b-link :to="'/hero/' + value.id">{{value.name}}</b-link>
                </td>
                <td>
                  <span>{{formatNumber(value.kills)}}</span>
                </td>
              </tr>
            </template>
          </table>
        </b-col>
        <b-col>
          <h5 class="text-center">KDA</h5>
          <table class="table table-sm">
            <template v-for="(value, index) in summary['kda']">
              <tr :key="index">
                <td style="width: 35px">
                  <i v-if="index == 0" class="fas fa-2x fa-award" style="color: #FFD700;" title="1st"></i>
                  <i v-if="index == 1" class="fas fa-2x fa-award" style="color: #C0C0C0;" title="2nd"></i>
                  <i v-if="index == 2" class="fas fa-2x fa-award" style="color: #CD7F32;" title="3rd"></i>
                </td>
                <td style="width: 35px">
                  <img :src="value.icon" class="ability-icon-sm"/>
                </td>
                <td>
                  <b-link :to="'/hero/' + value.id">{{value.name}}</b-link>
                </td>
                <td>
                  <span>{{formatDecimal(value.kda)}}</span>
                </td>
              </tr>
            </template>
          </table>
        </b-col>
      </b-row>
    </div>
    <hr class="highlighted" />
    <div>
      <p>
        We have charted the heroes <b class="text-info">Win Rate</b> as delta from <b class="text-info">50%</b>.
        This should provide a clear picture of which heroes are doing better then others. 
        We have grouped the heroes together by type to make it simpler to compare.
      </p>
      <b-card bg-variant="dark">
        <div class="customChart" id="chartdiv1"></div>
        <hr />
        <div class="customChart" id="chartdiv2"></div>
        <hr />
        <div class="customChart" id="chartdiv3"></div>
      </b-card>
    </div>
    
    
  </hgv-loading>
</template>

<script>
import * as am4core from "@amcharts/amcharts4/core";
import * as am4charts from "@amcharts/amcharts4/charts";
import am4themes_animated from "@amcharts/amcharts4/themes/animated";

am4core.useTheme(am4themes_animated);

export default {
  name: 'heroes',
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

      var collection = [
        {
          element: 'chartdiv1',
          data: this.heroes.filter(_ => _.attribute == 1 && _.region == 2),
          title: "Str Win Rate",
        },
        {
          element: 'chartdiv2',
          data: this.heroes.filter(_ => _.attribute == 2 && _.region == 2),
          title: "Agi Win Rate",
        },
        {
          element: 'chartdiv3',
          data: this.heroes.filter(_ => _.attribute == 3 && _.region == 2),
          title: "Int Win Rate",
        }
      ];
      
      for (const item of collection) {
        // Create chart instance
        var chart = am4core.create(item.element, am4charts.XYChart);

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