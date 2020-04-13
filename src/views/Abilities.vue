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
               These are ability drafts top abilities.
               While others may rise and fall with the meta these abilities have stood the test of time.
            </p>
            <div class="w3-row-padding">
              <template v-for="(item) in topAbilities">
                <div v-bind:key="item.id" class="w3-col s12 m6 l3 w3-padding-small">
                  <div class="w3-card w3-row w3-padding">
                    <div class="w3-col" style="width:64px;">
                      <img class="ability-icon-s" :src="item.image"  />
                    </div>
                    <div class="w3-rest w3-padding-small">
                      <b style="color:#25a2c3;">
                        <router-link class="title-short" :to="'/ability/' + item.id">{{item.name}}</router-link>
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
              Using some cleaver math we are able to deuced the drafting order for abilities. 
              Since this is based on fuzzy math we are calling this Pick Priority which is a indication of which abilities players are picking first.
              As you can see just cause people think an ability is 1st pick  worthy dose not mean it has a high win rate.
            </p>
            <div class="w3-row-padding">
              <template v-for="(item) in priorityAbilities">
                <div v-bind:key="item.id" class="w3-col s12 m6 l3 w3-padding-small">
                  <div class="w3-card w3-row w3-padding">
                    <div class="w3-col" style="width:64px;">
                      <img class="ability-icon-s" :src="item.image"  />
                    </div>
                    <div class="w3-rest w3-padding-small">
                      <b style="color:#25a2c3;">
                        <router-link class="title-short" :to="'/ability/' + item.id">{{item.name}}</router-link>
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
              Some things are just better together like SUNSfan and syndereN or peanut butter and jam but 'beware things are… rarely as they seem'.
              Below are abilities that players felt paired well with the source hero. 
              This could be because of a talent, limited imagination or a panic pick (don't judge we have all been there).
              However as you can see some of these abilities do not have a very high win rate.
            </p>
            <div class="w3-row-padding">
              <template v-for="(item) in ancestryAbilities">
                <div v-bind:key="item.id" class="w3-col s12 m6 l3 w3-padding-small">
                  <div class="w3-card w3-row w3-padding">
                    <div class="w3-col" style="width:64px;">
                      <img class="ability-icon-s" :src="item.image"  />
                    </div>
                    <div class="w3-rest w3-padding-small">
                      <b style="color:#25a2c3;">
                        <router-link class="title-short" :to="'/ability/' + item.id">{{item.name}}</router-link>
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
              We have charted the abilities current week win rate and with their pervious week.
              So you can see how each ability compare to each other.
              Also how the meta for each ability is changing week to week or in response to a patch.
            </p>
            <p>
              The chart bellow list all the active abilities in order of highest win rate.
              It has dumbbells that connect the <b>Previous</b> week and the <b style="color: #f6755e">Current</b> week.
            </p>
          </loader>
          <div class="chart" ref="chart"></div>
        </div>
      </section>
  </div>
</template>

<script>
import axios from "axios"
import { am4core, am4charts } from "@/plugins/amcharts-vue"

let chart = undefined

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

      var response = await axios.get(process.env.VUE_APP_BASE_API + "api/abilities/")
      self.abilities = response.data

      self.abilities.sort((a,b) => b.total.winRate - a.total.winRate)
      self.topAbilities = self.abilities.slice(0,24);

      self.abilities.sort((a,b) => b.current.priority - a.current.priority)
      self.priorityAbilities = self.abilities.slice(0,24);
      self.priorityAbilities.sort((a,b) => b.total.winRate - a.total.winRate)

      self.abilities.sort((a,b) => b.current.ancestry - a.current.ancestry)
      self.ancestryAbilities = self.abilities.slice(0,24);
      self.ancestryAbilities.sort((a,b) => b.total.winRate - a.total.winRate)

      self.abilities.sort((a,b) => b.current.winRate - a.current.winRate)

      self.loading = false

      self.$nextTick(() => {
			  self.makeChart()
		  })
    },
    makeChart() {
      let self = this

      chart = am4core.create(this.$refs.chart, am4charts.XYChart);
      chart.numberFormatter.numberFormat = "#%";

      let data = [];
      for (const item of this.abilities) {
        data.push({
          id: item.id,
          category: item.name,
          open: item.current.winRate,
          close: isNaN(item.previous.winRate) ? item.current.winRate : item.previous.winRate
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
    }
  },
  beforeRouteLeave (to, from , next) {
    chart.dispose()
    next()
  }
}
</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped>
.chart {
  width: 100%;
  height: 9000px;
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
.title-short {
  white-space: nowrap; 
  width: 100px; 
  overflow: hidden;
  text-overflow: ellipsis;
  text-decoration: none;
}
.title-short:hover {
  overflow: visible;
}
</style>

