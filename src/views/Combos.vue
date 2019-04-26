<template>
  <hgv-loading :urls="urls" v-on:loaded="loaded">
    <div>
      <h4 class="text-center">Combos</h4>
      <p>
        While knowing your heroes and abilities is key to starting a great build, combos are the reason we play AD.
        We have complied a list of ability-ability pairs and ultimate-ability pairs for each metric.
      </p>
      <hr class="highlighted" />
    </div>
    <div>
      <h5 class="text-center">Abilities</h5>
    </div>
    <b-card bg-variant="secondary" style="color: white;">
      <b-row v-if="abilities">
        <b-col>
          <h5 class="text-center">KDA</h5>
          <table class="table table-sm" style="color: white;">
            <template v-for="(value, index) in abilities['kda']">
              <tr :key="index">
                <td style="width: 35px">
                  <i v-if="index == 0" class="fas fa-2x fa-award" style="color: #FFD700;" title="1st"></i>
                  <i v-if="index == 1" class="fas fa-2x fa-award" style="color: #C0C0C0;" title="2nd"></i>
                  <i v-if="index == 2" class="fas fa-2x fa-award" style="color: #CD7F32;" title="3rd"></i>
                  <span v-if="index > 2">{{(index+1)}}th</span>
                </td>
                <td style="width: 35px">
                  <img :src="value.ability1.image" class="ability-icon-sm"/>
                </td>
                <td style="width: 35px">
                  <img :src="value.ability2.image" class="ability-icon-sm"/>
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
        <b-col>
          <h5 class="text-center">Win Rate</h5>
          <table class="table table-sm" style="color: white;">
            <template v-for="(value, index) in abilities['winrate']">
              <tr :key="index">
                <td style="width: 35px">
                  <i v-if="index == 0" class="fas fa-2x fa-award" style="color: #FFD700;" title="1st"></i>
                  <i v-if="index == 1" class="fas fa-2x fa-award" style="color: #C0C0C0;" title="2nd"></i>
                  <i v-if="index == 2" class="fas fa-2x fa-award" style="color: #CD7F32;" title="3rd"></i>
                  <span v-if="index > 2">{{(index+1)}}th</span>
                </td>
                <td style="width: 35px">
                  <img :src="value.ability1.image" class="ability-icon-sm"/>
                </td>
                <td style="width: 35px">
                  <img :src="value.ability2.image" class="ability-icon-sm"/>
                </td>
                <td>
                  <b-link :to="'/hero/' + value.id">{{value.name}}</b-link>
                </td>
                <td>
                  <span>{{formatPercentage(value.winrate)}}</span>
                </td>
              </tr>
            </template>
          </table>
        </b-col>
        <b-col>
          <h5 class="text-center">Wins</h5>
          <table class="table table-sm" style="color: white;">
            <template v-for="(value, index) in abilities['wins']">
              <tr :key="index">
                <td style="width: 35px">
                  <i v-if="index == 0" class="fas fa-2x fa-award" style="color: #FFD700;" title="1st"></i>
                  <i v-if="index == 1" class="fas fa-2x fa-award" style="color: #C0C0C0;" title="2nd"></i>
                  <i v-if="index == 2" class="fas fa-2x fa-award" style="color: #CD7F32;" title="3rd"></i>
                  <span v-if="index > 2">{{(index+1)}}th</span>
                </td>
                <td style="width: 35px">
                  <img :src="value.ability1.image" class="ability-icon-sm"/>
                </td>
                <td style="width: 35px">
                  <img :src="value.ability2.image" class="ability-icon-sm"/>
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
          <table class="table table-sm" style="color: white;">
            <template v-for="(value, index) in abilities['picks']">
              <tr :key="index">
                <td style="width: 35px">
                  <i v-if="index == 0" class="fas fa-2x fa-award" style="color: #FFD700;" title="1st"></i>
                  <i v-if="index == 1" class="fas fa-2x fa-award" style="color: #C0C0C0;" title="2nd"></i>
                  <i v-if="index == 2" class="fas fa-2x fa-award" style="color: #CD7F32;" title="3rd"></i>
                  <span v-if="index > 2">{{(index+1)}}th</span>
                </td>
                <td style="width: 35px">
                  <img :src="value.ability1.image" class="ability-icon-sm"/>
                </td>
                <td style="width: 35px">
                  <img :src="value.ability2.image" class="ability-icon-sm"/>
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
          <table class="table table-sm" style="color: white;">
            <template v-for="(value, index) in abilities['kills']">
              <tr :key="index">
                <td style="width: 35px">
                  <i v-if="index == 0" class="fas fa-2x fa-award" style="color: #FFD700;" title="1st"></i>
                  <i v-if="index == 1" class="fas fa-2x fa-award" style="color: #C0C0C0;" title="2nd"></i>
                  <i v-if="index == 2" class="fas fa-2x fa-award" style="color: #CD7F32;" title="3rd"></i>
                  <span v-if="index > 2">{{(index+1)}}th</span>
                </td>
                <td style="width: 35px">
                  <img :src="value.ability1.image" class="ability-icon-sm"/>
                </td>
                <td style="width: 35px">
                  <img :src="value.ability2.image" class="ability-icon-sm"/>
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
      </b-row>
    </b-card>
    <br />
    <div>
      <h5 class="text-center">Ultimates</h5>       
    </div>
    <b-card bg-variant="secondary">
      <b-row v-if="ultimates" style="color: white;">
        <b-col>
          <h5 class="text-center">KDA</h5>
          <table class="table table-sm" style="color: white;">
            <template v-for="(value, index) in ultimates['kda']">
              <tr :key="index">
                <td style="width: 35px">
                  <i v-if="index == 0" class="fas fa-2x fa-award" style="color: #FFD700;" title="1st"></i>
                  <i v-if="index == 1" class="fas fa-2x fa-award" style="color: #C0C0C0;" title="2nd"></i>
                  <i v-if="index == 2" class="fas fa-2x fa-award" style="color: #CD7F32;" title="3rd"></i>
                  <span v-if="index > 2">{{(index+1)}}th</span>
                </td>
                <td style="width: 35px">
                  <img :src="value.ability.image" class="ability-icon-sm"/>
                </td>
                <td style="width: 35px">
                  <img :src="value.ultimate.image" class="ability-icon-sm"/>
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
        <b-col>
          <h5 class="text-center">Win Rate</h5>
          <table class="table table-sm" style="color: white;">
            <template v-for="(value, index) in ultimates['winrate']">
              <tr :key="index">
                <td style="width: 35px">
                  <i v-if="index == 0" class="fas fa-2x fa-award" style="color: #FFD700;" title="1st"></i>
                  <i v-if="index == 1" class="fas fa-2x fa-award" style="color: #C0C0C0;" title="2nd"></i>
                  <i v-if="index == 2" class="fas fa-2x fa-award" style="color: #CD7F32;" title="3rd"></i>
                  <span v-if="index > 2">{{(index+1)}}th</span>
                </td>
                <td style="width: 35px">
                  <img :src="value.ability.image" class="ability-icon-sm"/>
                </td>
                <td style="width: 35px">
                  <img :src="value.ultimate.image" class="ability-icon-sm"/>
                </td>
                <td>
                  <b-link :to="'/hero/' + value.id">{{value.name}}</b-link>
                </td>
                <td>
                  <span>{{formatPercentage(value.winrate)}}</span>
                </td>
              </tr>
            </template>
          </table>
        </b-col>
        <b-col>
          <h5 class="text-center">Wins</h5>
          <table class="table table-sm" style="color: white;">
            <template v-for="(value, index) in ultimates['wins']">
              <tr :key="index">
                <td style="width: 35px">
                  <i v-if="index == 0" class="fas fa-2x fa-award" style="color: #FFD700;" title="1st"></i>
                  <i v-if="index == 1" class="fas fa-2x fa-award" style="color: #C0C0C0;" title="2nd"></i>
                  <i v-if="index == 2" class="fas fa-2x fa-award" style="color: #CD7F32;" title="3rd"></i>
                  <span v-if="index > 2">{{(index+1)}}th</span>
                </td>
                <td style="width: 35px">
                  <img :src="value.ability.image" class="ability-icon-sm"/>
                </td>
                <td style="width: 35px">
                  <img :src="value.ultimate.image" class="ability-icon-sm"/>
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
          <table class="table table-sm" style="color: white;">
            <template v-for="(value, index) in ultimates['picks']">
              <tr :key="index">
                <td style="width: 35px">
                  <i v-if="index == 0" class="fas fa-2x fa-award" style="color: #FFD700;" title="1st"></i>
                  <i v-if="index == 1" class="fas fa-2x fa-award" style="color: #C0C0C0;" title="2nd"></i>
                  <i v-if="index == 2" class="fas fa-2x fa-award" style="color: #CD7F32;" title="3rd"></i>
                  <span v-if="index > 2">{{(index+1)}}th</span>
                </td>
                <td style="width: 35px">
                  <img :src="value.ability.image" class="ability-icon-sm"/>
                </td>
                <td style="width: 35px">
                  <img :src="value.ultimate.image" class="ability-icon-sm"/>
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
          <table class="table table-sm" style="color: white;">
            <template v-for="(value, index) in ultimates['kills']">
              <tr :key="index">
                <td style="width: 35px">
                  <i v-if="index == 0" class="fas fa-2x fa-award" style="color: #FFD700;" title="1st"></i>
                  <i v-if="index == 1" class="fas fa-2x fa-award" style="color: #C0C0C0;" title="2nd"></i>
                  <i v-if="index == 2" class="fas fa-2x fa-award" style="color: #CD7F32;" title="3rd"></i>
                  <span v-if="index > 2">{{(index+1)}}th</span>
                </td>
                <td style="width: 35px">
                  <img :src="value.ability.image" class="ability-icon-sm"/>
                </td>
                <td style="width: 35px">
                  <img :src="value.ultimate.image" class="ability-icon-sm"/>
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
      </b-row>
    </b-card>
  </hgv-loading>
</template>

<script>
export default {
  name: 'combos',
  data () {
    return {
      'urls': ['/static/summary-combos.json'],
      'abilities': null,
      'ultimates': null,
    }
  },
  methods: {
    loaded(data) {
      this.abilities = data[0].abilities;
      this.ultimates = data[0].ultimates;
    }
  }
}
</script>