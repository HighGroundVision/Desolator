<template>
  <div>
    <section class="wrapper header special">
      <div class="inner">
        <h1>Ability Details</h1>
      </div>
    </section>

    <section class="wrapper style2">
      <div class="inner">
        <loader :loading="loading">
          <div class="w3-row-padding w3-center">
            <div class="w3-third">
              <span>~</span>
            </div>
            <div class="w3-third">
              <h2>{{ability.name}}</h2>
              <img class="w3-round-large ability-icon" :src="ability.image"/>
              <br />
              <span>History (Win Rate)</span>
              <br />
              <div class="sparkline" ref="chart"></div>    
            </div>
            <div class="w3-third">
              <span>~</span>
            </div>
          </div>
          <br />
          <h3>Hero Combos</h3>
          <p>
            A lot of people when talking about Ability Draft will focus on the ability combos as if they apply to all heroes equal.
            This is far from true and therefore we believe it is important to highlight heroes pair well with this ability. 
          </p>
          <div class="w3-center">
            <button @click="sortType = 1" class="button small" v-bind:class="{alt: sortType != 1}">Sort by Picks</button>
            &nbsp;
            <button @click="sortType = 2" class="button small" v-bind:class="{alt: sortType != 2}">Sort by Win Rate</button>
          </div>
          <br />
          <div class="w3-row-padding">
            <template v-for="(item) in heroCombos">
              <div v-bind:key="item.id" class="w3-col s12 m6 l4 w3-padding-small">
                <div class="w3-card w3-row w3-padding">
                  <div style="float: right;">
                    <span class="w3-badge" style="background-color: #25a2c3">{{item.picks}}</span>
                  </div>
                  <div class="w3-col" style="width:64px;">
                    <img class="hero-icon-s" :src="item.image"  />
                  </div>
                  <div class="w3-rest w3-padding-small">
                    <b style="line-height: 28px; color:#25a2c3;">
                      <router-link class="truncate" :to="'/hero/' + item.id">{{item.name}}</router-link>
                    </b>
                    <div class="w3-border w3-center  w3-round">
                      <div :style="'background-color:#f6755e;height:28px;width:'+ Math.round(item.winRate * 100)+'%'">
                        <b>{{Math.round(item.winRate * 100)}}%</b>
                      </div>
                    </div>
                  </div>
                </div>
              </div>
            </template>
          </div>
           <br />
          <h3>Ability Combos</h3>
          <p>
            A great ability combo on the right hero can dominate a game but no combo is unbeatable even if some look like they are.
            Just remember to balance against your play style, strategy, and/or item builds.
          </p>
          <div class="w3-center">
            <button @click="sortType = 1" class="button small" v-bind:class="{alt: sortType != 1}">Sort by Picks</button>
            &nbsp;
            <button @click="sortType = 2" class="button small" v-bind:class="{alt: sortType != 2}">Sort by Win Rate</button>
          </div>
          <br />
          <div class="w3-row-padding">
            <template v-for="(item) in abilityCombos">
              <div v-bind:key="item.id" class="w3-col s12 m6 l4 w3-padding-small">
                <div class="w3-card w3-row w3-padding">
                  <div style="float: right;">
                    <span class="w3-badge" style="background-color: #25a2c3">{{item.picks}}</span>
                  </div>
                  <div class="w3-col" style="width:64px;">
                    <img class="ability-icon-s" :src="item.image"  />
                  </div>
                  <div class="w3-rest w3-padding-small">
                    <b style="line-height: 28px; color:#25a2c3;">
                      <router-link class="truncate" :to="'/ability/' + item.id">{{item.name}}</router-link>
                    </b>
                    <div class="w3-border w3-center  w3-round">
                      <div :style="'background-color:#f6755e;height:28px;width:'+ Math.round(item.winRate * 100)+'%'">
                        <b>{{Math.round(item.winRate * 100)}}%</b>
                      </div>
                    </div>
                  </div>
                </div>
              </div>
            </template>
          </div>
        </loader>
      </div>
    </section>
  </div>
</template>

<script>
import axios from "axios"
import { am4core, am4charts } from "@/plugins/amcharts-vue"

let chart = undefined;

export default {
  name: "hero",
  data() {
    return {
      loading: true,
      sortType: 2,
      ability: {}
    }
  },
  mounted() {
    this.loadData()
  },
  computed: {
    heroCombos: function () {
      if(this.ability.heroes) {
        var combos = this.ability.heroes.slice();
        combos.sort((a,b) => {
          if(this.sortType == 1)
            return b.picks - a.picks;
          else if(this.sortType == 2)
            return b.winRate - a.winRate;
        });
        return combos
      } 
      else
      {
        return []
      }
    },
    abilityCombos: function () {
      if(this.ability.abilities) {
        var combos = this.ability.abilities.slice();
        combos.sort((a,b) => {
          if(this.sortType == 1)
            return b.picks - a.picks;
          else if(this.sortType == 2)
            return b.winRate - a.winRate;
        });
        return combos
      }
      else 
      {
        return []
      }
      
    }
  },
  methods: {
    async  loadData() {
      let self = this
      self.loading = true

      var response = await axios.get(process.env.VUE_APP_BASE_API + "api/ability/" + this.$route.params.id)
      self.ability = response.data

      self.loading = false

      this.$nextTick(() => {
			  self.makeSparkline()
		  })
    },
    makeSparkline() {
      chart = am4core.create(this.$refs.chart, am4charts.XYChart)

      let data = []
      for (const item of this.ability.history) {
        data.push({
          "date": new Date(item.day),
          "value": item.winRate * 100
        })
      }

      chart.data = data 

      chart.padding(0, 0, 0, 0)

      var dateAxis = chart.xAxes.push(new am4charts.DateAxis())
      dateAxis.renderer.grid.template.disabled = true
      dateAxis.renderer.labels.template.disabled = true
      dateAxis.startLocation = 0.5
      dateAxis.endLocation = 0.7
      dateAxis.cursorTooltipEnabled = false

      var valueAxis = chart.yAxes.push(new am4charts.ValueAxis())
      valueAxis.min = 0
      valueAxis.renderer.grid.template.disabled = true
      valueAxis.renderer.baseGrid.disabled = true
      valueAxis.renderer.labels.template.disabled = true
      valueAxis.cursorTooltipEnabled = false

      chart.cursor = new am4charts.XYCursor()
      chart.cursor.lineY.disabled = true

      var series = chart.series.push(new am4charts.LineSeries())
      series.tooltipText = "{date}: [bold]{value}"
      series.dataFields.dateX = "date"
      series.dataFields.valueY = "value"
      series.tensionX = 0.8
      series.strokeWidth = 2
      series.stroke = am4core.color("#ff0000");

      // render data points as bullets
      var bullet = series.bullets.push(new am4charts.CircleBullet())
      bullet.stroke = am4core.color("#ff0000");
      bullet.fill = am4core.color("#ff0000");
      bullet.circle.radius = 3
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
.sparkline {
  /*width: 200px*/
  width: 100%;
  height: 30px;
  border: 1px dotted #eee;
}
.hero-icon-s
{
  height: 64px;
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
.ability-icon
{
  width: 128px;
  height: 128px;
}
.truncate {
  width: 200px;
  white-space: nowrap;
  overflow: hidden;
  text-overflow: ellipsis;
  text-decoration: none;
}
</style>
