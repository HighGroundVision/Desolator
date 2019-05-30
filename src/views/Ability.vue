<template>
  <hgv-loading :urls="urls" v-on:loaded="loaded">
    <b-row v-if="ability">
      <b-col>
        <h4 class="text-center">{{ability.details.name}}</h4>

        <AbilityCard :ability="ability.details" :showTitle="false" />
        <br />
        
        <b-card bg-variant="light">
          <b-row>
            <b-col>
              <b>Kda</b><br />
              <span>{{formatDecimal(ability.summary.kda)}}</span>
            </b-col>
            <b-col>
              <b>Kills</b><br />
            <span>{{formatNumber(ability.summary.kills)}}</span>
            </b-col>
            <b-col>
              <b>Picks</b><br />
            <span>{{formatNumber(ability.summary.picks)}}</span>
            </b-col>
            <b-col>
              <b>Wins</b><br />
            <span>{{formatNumber(ability.summary.wins)}}</span>
            </b-col>
            <b-col>
              <b>Win Rate</b><br />
              <b-progress :max="1" :value="ability.summary.winrate" variant="primary" :striped="true" show-progress></b-progress>
            </b-col>
          </b-row>
        </b-card>
        <br />

        <h4 class="text-center">Heroes</h4>
        <p>
          We have included the top Heroes for this ability.
          These skills are ordered by KDA.
          We only show 10 at a time but you can page through the table to find the hero you are interested in.
        </p>
        <b-card bg-variant="light">
          <b-table 
              :fields="['image', 'name', 'kda', 'kills', 'picks', 'wins', 'win_rate']"
              :items="ability.heroes"
              :per-page="page.size"
              :current-page="page.heroes"
              :sort-by="'kda'"
              :sort-desc="true"
              >
              <template slot="image" slot-scope="row">
                <b-img :src="row.item.image" class="hero-icon-banner-sm" />
              </template>
              <template slot="name" slot-scope="row">
                <b-link :to="'/hero/' + row.item.id" target="_blank">{{row.item.name}}</b-link>
              </template>
              <template slot="win_rate" slot-scope="row">
                <b-progress variant="primary" height="1.5rem" :value="row.item.win_rate" :min="0" :max="1" :striped="true" show-progress></b-progress>
              </template>
              <template slot="kda" slot-scope="row">
                <span>{{formatDecimal(row.item.kda)}}</span>
              </template>
              <template slot="kills" slot-scope="row">
                <span>{{formatNumber(row.item.kills)}}</span>
              </template>
              <template slot="wins" slot-scope="row">
                <span>{{formatNumber(row.item.wins)}}</span>
              </template>
              <template slot="picks" slot-scope="row">
                <span>{{formatNumber(row.item.picks)}}</span>
              </template>
          </b-table>
        </b-card>
        <br />
        <b-pagination v-model="page.heroes" :total-rows="ability.heroes.length" :per-page="page.size" align="center"></b-pagination>

        <p>
          It is more important to gain a sense of which types of heroes are a good fit for this ability.
          To that end we have include the hero graph filter to this ability and hero pairs.
        </p>
        <b-card bg-variant="dark">
          <div class="customChart" id="chartdiv1"></div>
          <hr />
          <div class="customChart" id="chartdiv2"></div>
          <hr />
          <div class="customChart" id="chartdiv3"></div>
        </b-card>
        <br />

        <h4 class="text-center">Combos</h4>   
        <p>
          After selecting the best ability for your hero or the strongest ability in the pool, what is next? 
          This is where you can find the best pairing for that ability.
        </p>
         <p>
          We have included the top Ability and Ultimate Combos for this ability.
          If this this skill is an Ultimate then no other Ultimates will appear in the combos list as you can't have more then one.
        </p>
        <div>
          <h5 class="text-center">Abilities</h5>
          <b-card bg-variant="light">
            <b-table 
                :fields="['image', 'name', 'kda', 'kills', 'picks', 'wins', 'win_rate']"
                :items="ability.combo_abilities" 
                :per-page="page.size"
                :current-page="page.abilities"
                :sort-by="'kda'"
                :sort-desc="true"
                >
                <template slot="image" slot-scope="row">
                  <b-img :src="row.item.image" class="hero-icon-banner-sm" />
                </template>
                <template slot="name" slot-scope="row">
                  <b-link :to="'/ability/' + row.item.id" target="_blank">{{row.item.name}}</b-link>
                </template>
                <template slot="win_rate" slot-scope="row">
                  <b-progress height="1.5rem" :value="row.item.win_rate" :min="0" :max="1" :striped="true" show-progress></b-progress>
                </template>
                <template slot="kda" slot-scope="row">
                  <span>{{formatDecimal(row.item.kda)}}</span>
                </template>
                <template slot="kills" slot-scope="row">
                  <span>{{formatNumber(row.item.kills)}}</span>
                </template>
                <template slot="wins" slot-scope="row">
                  <span>{{formatNumber(row.item.wins)}}</span>
                </template>
                <template slot="picks" slot-scope="row">
                  <span>{{formatNumber(row.item.matches)}}</span>
                </template>
              </b-table>
            </b-card>
            <br />
            <b-pagination v-model="page.abilities" :total-rows="ability.combo_abilities.length" :per-page="page.size" align="center"></b-pagination>
        </div>
        <div v-if="ability.combo_ultimates.length > 0">
          <h5 class="text-center">Ultimates</h5>
          <b-card bg-variant="light">
            <b-table 
                :fields="['image', 'name', 'kda', 'kills', 'picks', 'wins', 'win_rate']"
                :items="ability.combo_ultimates" 
                :per-page="page.size"
                :current-page="page.ultimates"
                :sort-by="'kda'"
                :sort-desc="true"
                >
                <template slot="image" slot-scope="row">
                  <b-img :src="row.item.image" class="hero-icon-banner-sm" />
                </template>
                <template slot="name" slot-scope="row">
                  <b-link :to="'/ability/' + row.item.id" target="_blank">{{row.item.name}}</b-link>
                </template>
                <template slot="win_rate" slot-scope="row">
                  <b-progress height="1.5rem" :value="row.item.win_rate" :min="0" :max="1" :striped="true" show-progress></b-progress>
                </template>
                  <template slot="kda" slot-scope="row">
                  <span>{{formatDecimal(row.item.kda)}}</span>
                </template>
                <template slot="kills" slot-scope="row">
                  <span>{{formatNumber(row.item.kills)}}</span>
                </template>
                <template slot="wins" slot-scope="row">
                  <span>{{formatNumber(row.item.wins)}}</span>
                </template>
                <template slot="picks" slot-scope="row">
                  <span>{{formatNumber(row.item.matches)}}</span>
                </template>
              </b-table>
            </b-card>
            <br />
            <b-pagination v-model="page.ultimates" :total-rows="ability.combo_abilities.length" :per-page="page.size" align="center"></b-pagination>
        </div>
      </b-col>
      <b-col cols="4" lg="3">
        <div class="text-center">
          <b-alert variant="info" show>   
            <p>
              Get to know your abilities. We have included the basic details that everyone should know.
            </p>
          </b-alert>
          <img src="@/assets/imgs/cluckles-speach.png" class="cluckles-speach" />
        </div>
      </b-col>
    </b-row>
  </hgv-loading>
</template>

<script>
import AbilityCard from "@/components/AbilityCard";
import * as am4core from "@amcharts/amcharts4/core";
import * as am4charts from "@amcharts/amcharts4/charts";
import am4themes_animated from "@amcharts/amcharts4/themes/animated";

am4core.useTheme(am4themes_animated);

export default {
  name: 'ability',
  data () {
    let id = this.$route.params.id;
    return {
      'urls': [`/static/ability.${id}.json`],
      'ability': null,
      'page': {
        'size': 10,
        'heroes': 1,
        'abilities': 1,
        'ultimates': 1,
      }
    }
  },
  components: { AbilityCard },
  methods: {
    loaded(data) {
      this.ability = data[0];

      var self = this;
      this.$nextTick(function () {
        self.buildCharts();
      });
    },
    buildCharts() {

      var collection = [
        {
          element: 'chartdiv1',
          data: this.ability.heroes.filter(_ => _.attribute == 1),
          title: "Str Win Rate",
        },
        {
          element: 'chartdiv2',
          data: this.ability.heroes.filter(_ => _.attribute == 2),
          title: "Agi Win Rate",
        },
        {
          element: 'chartdiv3',
          data: this.ability.heroes.filter(_ => _.attribute == 3),
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
        valueAxis.min = 0.0;
        valueAxis.max = 1.0;
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
        image.propertyFields.href = "icon";
        image.tooltipText = series.columns.template.tooltipText;
        image.propertyFields.fill = "color";
        image.filters.push(new am4core.DropShadowFilter());
        image.events.on("hit", function(ev) {
          var id = ev.target.dataItem.dataContext.id;
          this.$router.push('/hero/' + id);
        }, this);
      }
    },
  },
  /*
  watch: {
    $route(to, from) {
      // this.details = abilities[to.params.id];
    }
  }
  */
}
</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped>
.customChart {
  width: 100%;
  height: 200px;
}
</style>