<template>
  <div>
    <section class="wrapper header special">
      <div class="inner">
        <h1>Hero Details</h1>
      </div>
    </section>

    <section class="wrapper style2">
      <div class="inner">
        <loader :loading="loading">
          <div class="flex flex-3">
            <div class="flex-item left">
            </div>
            <div class="flex-item">
              <h2>{{hero.name}}</h2>
              <img class="w3-round-large" :src="hero.image" style="width:100%;" />
              <span>History (Win Rate)</span>
              <br />
              <div class="sparkline" ref="chart"></div>    
            </div>
            <div class="flex-item right">
            </div>
          </div>
          <br />
          <h3>Attributes</h3>
          <p>
            Knowing your hero is important in every mode in the Dota but in Ability Draft this can matter far more.
            Below we listed the primary attributes for the hero sorted by the rank of each attribute.
            Understanding your heroes strengths and weaknesses will help you understand which ability you can use to enhance your strengths / offset your weakness and this can also effect your item build.
          </p>
          <template v-for="(attribute) in hero.attributes">
            <div v-bind:key="attribute.name">
              <div class="w3-row-padding">
                <div class="w3-third w3-right-align">
                  <span>{{attribute.name}}</span>
                </div>
                <div class="w3-third">
                  <div class="w3-border">
                    <div :style="'background-color:#f6755e;height:24px;width:'+ Math.round(attribute.percentage * 100)+'%'"></div>
                  </div>
                </div>
                <div class="w3-third">
                  {{attribute.value}}
                </div>
              </div>
            </div>
          </template>
          <br />
          <h3>Talents</h3>
          <p>
            Some heroes are very ability dependant when its comes to talents.
            Examine each talents win rate to understand which abilities to prioritize.
            In Ability Draft extreme differences in win rate can highlight a heroes strengths and weaknesses.
          </p>
          <div class="w3-row-padding w3-center">
            <div class="w3-col s4">
               <b>Description</b>
            </div>
            <div class="w3-col s1">
               <b>Win Rate</b>
            </div>
            <div class="w3-col s2">
              <b>Level</b>
            </div>
            <div class="w3-col s1">
               <b>Win Rate</b>
            </div>
            <div class="w3-col s4">
              <b>Description</b>
            </div>
          </div>
          <template v-for="(talents, key) in hero.talents">
            <div class="w3-row-padding w3-center" v-bind:key="key">
              <div class="w3-col s4">
                {{talents[0].description}}
              </div>
              <div class="w3-col s1">
                {{Math.round(talents[0].winRate * 100)}}%
              </div>
              <div class="w3-col s2">
                <span class="w3-tag w3-round-large" style="background-color:#f6755e;">{{key}}</span>
              </div>
              <div class="w3-col s1">
                {{Math.round(talents[1].winRate * 100)}}%
              </div>
              <div class="w3-col s4">
                {{talents[1].description}}
              </div>
            </div>
          </template>
          <br />
          <h3>Abilities</h3>
          <p>
            The heroes default abilities removed form the combos because they could be required by talents so we wanted to make them easy to find to compare with the talent win rates.
            Keep in mind because the default abilities are always available with this hero the overall numbers are inflated when compared to other combos.
          </p>
          <div class="w3-row-padding">
            <template v-for="(item) in hero.abilities">
              <div v-bind:key="item.id" class="w3-col s6 m3 l3 w3-padding-small w3-center">
                <img class="w3-round-large" :src="item.image"  />
                <h4>
                  <router-link style="text-decoration: none;" :to="'/ability/' + item.id">{{item.name}}</router-link>
                </h4>
                <span class="w3-tag w3-xlarge w3-round-large" style="position: relative; top:-190px;left:50px;background-color:#f6755e;">{{Math.round(item.winRate * 100)}}%</span>
              </div>
            </template>
          </div>
          <br />
          <h3>Combos</h3>
          <p>
            A lot of people when talking about Ability Draft will focus on the ability combos as if they apply to all heroes equal.
            This is far from true and therefore we believe it is important to highlight abilities that players are picking with this hero but we have also included in the win rate so you can gage which abilities to prioritize over others.
            After you find the best abilities your your hero you can click them to see which abilities they combo with.
          </p>
          <div class="w3-center">
            <button @click="sortType = 1" class="button small" v-bind:class="{alt: sortType != 1}">Sort by Picks</button>
            &nbsp;
            <button @click="sortType = 2" class="button small" v-bind:class="{alt: sortType != 2}">Sort by Win Rate</button>
          </div>
          <br />
          <div class="w3-row-padding">
            <template v-for="(item) in combos">
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
      sortType: 1,
      hero: {}
    }
  },
  mounted() {
    this.loadData()
  },
  computed: {
    combos: function () {
      if(this.hero.combos) {
        var combos = this.hero.combos.slice();
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

      var response = await axios.get("https://tarrasque.azurewebsites.net/api/hero/" + this.$route.params.id)
      self.hero = response.data

      self.loading = false

      this.$nextTick(() => {
			  self.makeSparkline()
		  })
    },
    makeSparkline() {
      chart = am4core.create(this.$refs.chart, am4charts.XYChart)

      let data = []
      for (const item of this.hero.history) {
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
.truncate {
  width: 200px;
  white-space: nowrap;
  overflow: hidden;
  text-overflow: ellipsis;
  text-decoration: none;
}
</style>
