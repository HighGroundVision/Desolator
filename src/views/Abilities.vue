<template>
  <div>
     <section class="wrapper header special">
      <div class="inner">
        <h1>Abilities</h1>
      </div>
     </section>
     
     <section class="wrapper style2">
        <div class="inner">
          <loader :loading="loading">
            <h2>Top Abilities</h2>
            <p>
              Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Purus viverra accumsan in nisl nisi scelerisque eu ultrices. Vitae ultricies leo integer malesuada nunc vel risus commodo viverra. Quis risus sed vulputate odio. Orci phasellus egestas tellus rutrum tellus pellentesque eu. Tellus in hac habitasse platea dictumst vestibulum rhoncus. Orci porta non pulvinar neque laoreet suspendisse interdum consectetur. Donec et odio pellentesque diam volutpat commodo. Diam maecenas sed enim ut sem viverra aliquet eget. Ultrices tincidunt arcu non sodales neque sodales ut etiam sit. In fermentum et sollicitudin ac. Purus gravida quis blandit turpis cursus in hac habitasse. Egestas egestas fringilla phasellus faucibus scelerisque. Mauris sit amet massa vitae. Ornare massa eget egestas purus.
            </p>
            <div class="w3-row-padding">
              <template v-for="(item) in topAbilities">
                <div v-bind:key="item.id" class="w3-col s12 m6 l4 w3-padding-small">
                  <div class="w3-card w3-row w3-padding">
                    <div class="w3-col" style="width:64px;">
                      <img class="ability-icon-s" :src="item.image"  />
                    </div>
                    <div class="w3-rest w3-padding-small">
                      <b style="line-height: 28px; color:#25a2c3;">
                        <router-link style="text-decoration: none;" :to="'/ability/' + item.id">{{item.name}}</router-link>
                      </b>
                      <div class="w3-border w3-center w3-round">
                        <div :style="'background-color:#f6755e;height:28px;width:'+ Math.round(item.total.winRate * 100)+'%'">
                          <b>{{Math.round(item.total.winRate * 100)}}%</b>
                        </div>
                      </div>
                    </div>
                  </div>
                </div>
              </template>
            </div>
            <br />
            <h2>Priority Abilities</h2>
            <p>
              Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Purus viverra accumsan in nisl nisi scelerisque eu ultrices. Vitae ultricies leo integer malesuada nunc vel risus commodo viverra. Quis risus sed vulputate odio. Orci phasellus egestas tellus rutrum tellus pellentesque eu. Tellus in hac habitasse platea dictumst vestibulum rhoncus. Orci porta non pulvinar neque laoreet suspendisse interdum consectetur. Donec et odio pellentesque diam volutpat commodo. Diam maecenas sed enim ut sem viverra aliquet eget. Ultrices tincidunt arcu non sodales neque sodales ut etiam sit. In fermentum et sollicitudin ac. Purus gravida quis blandit turpis cursus in hac habitasse. Egestas egestas fringilla phasellus faucibus scelerisque. Mauris sit amet massa vitae. Ornare massa eget egestas purus.
            </p>
            <div class="w3-row-padding">
              <template v-for="(item) in priorityAbilities">
                <div v-bind:key="item.id" class="w3-col s12 m6 l4 w3-padding-small">
                  <div class="w3-card w3-row w3-padding">
                    <div class="w3-col" style="width:64px;">
                      <img class="ability-icon-s" :src="item.image"  />
                    </div>
                    <div class="w3-rest w3-padding-small">
                      <b style="line-height: 28px; color:#25a2c3;">
                        <router-link style="text-decoration: none;" :to="'/ability/' + item.id">{{item.name}}</router-link>
                      </b>
                      <div class="w3-border w3-center w3-round">
                        <div :style="'background-color:#f6755e;height:28px;width:'+ Math.round(item.total.winRate * 100)+'%'">
                          <b>{{Math.round(item.total.winRate * 100)}}%</b>
                        </div>
                      </div>
                    </div>
                  </div>
                </div>
              </template>
            </div>
            <br />
            <h2>Ancestry Abilities</h2>
            <p>
              Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Purus viverra accumsan in nisl nisi scelerisque eu ultrices. Vitae ultricies leo integer malesuada nunc vel risus commodo viverra. Quis risus sed vulputate odio. Orci phasellus egestas tellus rutrum tellus pellentesque eu. Tellus in hac habitasse platea dictumst vestibulum rhoncus. Orci porta non pulvinar neque laoreet suspendisse interdum consectetur. Donec et odio pellentesque diam volutpat commodo. Diam maecenas sed enim ut sem viverra aliquet eget. Ultrices tincidunt arcu non sodales neque sodales ut etiam sit. In fermentum et sollicitudin ac. Purus gravida quis blandit turpis cursus in hac habitasse. Egestas egestas fringilla phasellus faucibus scelerisque. Mauris sit amet massa vitae. Ornare massa eget egestas purus.
            </p>
            <div class="w3-row-padding">
              <template v-for="(item) in ancestryAbilities">
                <div v-bind:key="item.id" class="w3-col s12 m6 l4 w3-padding-small">
                  <div class="w3-card w3-row w3-padding">
                    <div class="w3-col" style="width:64px;">
                      <img class="ability-icon-s" :src="item.image"  />
                    </div>
                    <div class="w3-rest w3-padding-small">
                      <b style="line-height: 28px; color:#25a2c3;">
                        <router-link style="text-decoration: none;" :to="'/ability/' + item.id">{{item.name}}</router-link>
                      </b>
                        <div class="w3-border w3-center w3-round">
                        <div :style="'background-color:#f6755e;height:28px;width:'+ Math.round(item.total.winRate * 100)+'%'">
                          <b>{{Math.round(item.total.winRate * 100)}}%</b>
                        </div>
                      </div>
                    </div>
                  </div>
                </div>
              </template>
            </div>
            <br />
            <h2>Abilities</h2>
            <p>
              Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Purus viverra accumsan in nisl nisi scelerisque eu ultrices. Vitae ultricies leo integer malesuada nunc vel risus commodo viverra. Quis risus sed vulputate odio. Orci phasellus egestas tellus rutrum tellus pellentesque eu. Tellus in hac habitasse platea dictumst vestibulum rhoncus. Orci porta non pulvinar neque laoreet suspendisse interdum consectetur. Donec et odio pellentesque diam volutpat commodo. Diam maecenas sed enim ut sem viverra aliquet eget. Ultrices tincidunt arcu non sodales neque sodales ut etiam sit. In fermentum et sollicitudin ac. Purus gravida quis blandit turpis cursus in hac habitasse. Egestas egestas fringilla phasellus faucibus scelerisque. Mauris sit amet massa vitae. Ornare massa eget egestas purus.
            </p>
            <div class="chart" ref="chart"></div>
          </loader>
        </div>
      </section>
  </div>
</template>

<script>
import axios from "axios"
import { am4core, am4charts } from "@/plugins/amcharts-vue"

export default {
  name: 'abilities',
  data() {
    return {
      loading: true,
      abilities: [],
      topAbilities: [],
      priorityAbilities: [],
      ancestryAbilities: [],
    }
  },
  mounted() {
    this.loadData()
  },
  methods: {
    async  loadData() {
      let self = this
      self.loading = true

      var response = await axios.get("https://tarrasque.azurewebsites.net/api/abilities/")
      self.abilities = response.data

      self.abilities.sort((a,b) => b.total.winRate - a.total.winRate)
      self.topAbilities = self.abilities.slice(0,9);

      self.abilities.sort((a,b) => b.total.priority - a.total.priority)
      self.priorityAbilities = self.abilities.slice(0,9);

      self.abilities.sort((a,b) => b.total.ancestry - a.total.ancestry)
      self.ancestryAbilities = self.abilities.slice(0,9);

      self.abilities.sort((a,b) => b.current.winRate - a.current.winRate)

      self.loading = false

      self.$nextTick(() => {
			  self.makeChart()
		  })
    },
    makeChart() {
      let self = this

      let chart = am4core.create(this.$refs.chart, am4charts.XYChart);
      chart.numberFormatter.numberFormat = "#%";

      let data = [];
      for (const item of this.abilities) {
        data.push({
          id: item.id,
          category: item.name,
          open: item.current.winRate,
          close: item.current.winRate - 0.01
        });
      }

      chart.data = data;

      var categoryAxis = chart.yAxes.push(new am4charts.CategoryAxis());
      categoryAxis.renderer.grid.template.location = 0;
      categoryAxis.renderer.ticks.template.disabled = true;
      categoryAxis.renderer.axisFills.template.disabled = true;
      categoryAxis.dataFields.category = "category";
      categoryAxis.renderer.minGridDistance = 15;
      categoryAxis.renderer.inversed = true;
      categoryAxis.renderer.inside = false;
      categoryAxis.renderer.grid.template.location = 0.5;
      categoryAxis.renderer.grid.template.strokeDasharray = "1,3";
      categoryAxis.renderer.labels.template.events.on("hit", function(event) {
        self.$router.push({
          name: "ability",
          params: { id: event.target.dataItem.dataContext.id }
        });
      });

      var image = new am4core.Image();
      image.horizontalCenter = "middle";
      image.width = 20;
      image.height = 20;
      image.verticalCenter = "middle";
      image.adapter.add("href", (href, target) => {
        let category = target.dataItem.category;
        if (category) {
          let cat = self.abilities.find(_ => _.name == category);
          return cat.image;
        }
        return href;
      });
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
      currentBullet.fill = "#f6755e";
      currentBullet.stroke = "#000";

      chart.legend = new am4charts.Legend();
      chart.legend.position = "top";
      chart.legend.data = [
        {
          name: "Previous Winrate",
          fill: "#606060"
        },
        {
          name: "Current Winrate",
          fill: "#f6755e"
        }
      ];

      chart.scrollbarY = new am4core.Scrollbar();
      categoryAxis.start = 0;
      categoryAxis.end = 0.07;
    }
  }
}
</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped>
.chart {
  width: 100%;
  height: 900px;
}
.ability-icon-s
{
  width: 64px;
  height: 64px;
}
.ability-icon-xs
{
  width: 30px;
  height: 30px;
}
</style>

