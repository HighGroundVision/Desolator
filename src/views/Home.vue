<template>
  <hgv-loading :urls="urls" v-on:loaded="loaded">
    <b-row>
      <b-col cols="4" lg="3">
        <div class="text-center">
          <b-alert variant="info" show>   
            <strong>Hi, I am Cluckles!</strong>
            <br />
            <span>AD is hard and confusing, luckily I am here to help make it fun!</span>
          </b-alert>
          <div style="float: right; position: relative; top:-15px;">
            <a href="https://steamcommunity.com/sharedfiles/filedetails/?id=111384500" target="_blank" class="text-white">
              <i class="far fa-question-circle" title="I also come in this really cool Courier form too!"></i>
            </a>
          </div>
          <img src="@/assets/imgs/cluckles-speach.png" class="cluckles-speach" />
        </div>
      </b-col>
      <b-col>
        <b-alert v-if="construction" variant="warning" show>
          <b-row>
            <b-col>
              <strong><i class="fas fa-exclamation-triangle"></i> Under Construction</strong>
              <p>
                We are currently rebuilding the systems that run this web application. 
                Our continuous deployment process means that this site will be updated as we push out new features. 
                You are welcome to use the site and the new features. 
                Some features maybe unstable/incomplete and maybe removed in the future.
                This banner will be removed when the rework is finished.
              </p>
            </b-col>
          </b-row>
        </b-alert>
        <h4 class="text-center">Welcome!</h4>
        <!--<hr class="highlighted" />-->
        <p>
          We at HGV are dedicated to the Ability Draft community.
          Providing the tools to understand AD better, and hopefully have some fun along the way. 
          On our quest to identify the best Heroes and Abilities for AD we found 5 key metrics which to measure by. 
          These are Picks, Wins, Win Rate (Wins / Picks), Kills, and KDA ([Kills + [Assists/3] - Deaths] / Picks).
          You will see these metrics on most sections. 
          These are meant to give you as complete as picture as possible.
          Just because something wins a lot or kills a lot dose not make it a choice or the right choice. 
          We have found while metrics like Kills and Wins tends to speak to a entity's strength compare to others.
          Win Rates will have bias because of the delta in picks between entities (even heroes, but this gets worst for abilities)
          To that end we found the KDA ratio works best to compare heroes and abilities.
          This is because in reviewing the data, winning the match was not enough to say that the hero, ability, or combos was good.
          While KDA provided a better view its not the complete picture hence the need for these other metrics.
        </p>
        <hr class="highlighted" />
        <h4 class="text-center">Heroes</h4>
        <p>
          We have collected metrics on each hero then we grouped those by type and calculated the results. 
        </p>
        <b-card bg-variant="secondary">
          <div>
            <b>Hero Types</b>
          </div>
          <table class="table table-sm" style="color: white;">
            <tr>
              <th></th>
              <template v-for="(value) in heroes.types">
                <th :key="value.key">
                  <div v-if="value.attribute == 1">
                    <b>Strength</b>
                  </div>
                  <div v-if="value.attribute == 2">
                    <b>Agility</b>
                  </div>
                  <div v-if="value.attribute == 3">
                    <b>Intelligence</b>
                  </div>
                </th>
              </template>
            </tr>
            <tr>
              <td>Wins</td>
              <template v-for="(value) in heroes.types">
                <td :key="value.key">{{formatNumber(value.wins) }}</td>
              </template>
            </tr>
            <tr>
              <td>Win Rate</td>
              <template v-for="(value) in heroes.types">
                <td :key="value.key">{{formatPercentage(value.win_rate) }}</td>
              </template>
            </tr>
            <tr>
              <td>Kills</td>
              <template v-for="(value) in heroes.types">
                <td :key="value.key">{{formatNumber(value.kills) }}</td>
              </template>
            </tr>
            <tr>
              <td>KDA</td>
              <template v-for="(value) in heroes.types">
                <td :key="value.key">{{formatDecimal(value.kda) }}</td>
              </template>
            </tr>
          </table>
        </b-card>
        <br />
        <p>
          We have collected metrics on each hero then we grouped those by roles and calculated the results.
          We based this on what Dota states each hero should be, not what we calculated they should be.
        </p>
        <b-card bg-variant="secondary">
          <div>
            <b>Hero Roles</b>
          </div>
          <table class="table table-sm" style="color: white;">
            <tr>
              <th></th>
              <template v-for="(value) in heroes.roles">
                <th :key="value.key">{{value.role}}</th>
              </template>
            </tr>
            <tr>
              <td>Wins</td>
              <template v-for="(value) in heroes.roles">
                <td :key="value.key">{{formatNumber(value.wins) }}</td>
              </template>
            </tr>
            <tr>
              <td>Win Rate</td>
              <template v-for="(value) in heroes.roles">
                <td :key="value.key">{{formatPercentage(value.win_rate) }}</td>
              </template>
            </tr>
            <tr>
              <td>Kills</td>
              <template v-for="(value) in heroes.roles">
                <td :key="value.key">{{formatNumber(value.kills) }}</td>
              </template>
            </tr>
            <tr>
              <td>KDA</td>
              <template v-for="(value) in heroes.roles">
                <td :key="value.key">{{formatDecimal(value.kda) }}</td>
              </template>
            </tr>
          </table>
          <i>NOTE: No one should ever think Jungling instead of lanning is a good idea! I don't care what your build is...</i>
        </b-card>
        <br />
        <p>
          We have collected the top 3 heroes by kda. See above for why we use KDA instead of Win Rate.
        </p>
        <b-card bg-variant="secondary">
          <div>
            <b>Top Heroes</b>
          </div>
          <!-- heroes.best -->
          <table class="table table-sm" style="color: white;">
            <template v-for="(value, index) in heroes.best['kda']">
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
                  <span>{{value.name}}</span>
                </td>
              </tr>
            </template>
          </table>
        </b-card>
        <p>
          We have provided a small summary of the heroes in AD. 
          Check out the <b-link to="/heroes">Heroes Page</b-link> for more information and dive into each hero's details.
        </p>
        <h4 class="text-center">Abilities</h4>
        <p>
          The best abilities is collected a little differently then others and looking at the metrics listed above dose not provide a clear victory for the title of best ability.
          But if we look at the which ability (by KDA) is the top ability for hero then we count each time that ability was the top for all heroes.
          This to us means that these are the abilities that are best across all heroes and therefore are the best abilities.
        </p>
        <b-card v-if="abilities" bg-variant="secondary">
          <div>
            <b>Best Abilities</b>
          </div>
          <table class="table table-sm" style="color: white;">
            <template v-for="(value, index) in abilities['rank']">
              <tr :key="index">
                <td style="width: 35px">
                  <i v-if="index == 0" class="fas fa-2x fa-award" style="color: #FFD700;" title="1st"></i>
                  <i v-if="index == 1" class="fas fa-2x fa-award" style="color: #C0C0C0;" title="2nd"></i>
                  <i v-if="index == 2" class="fas fa-2x fa-award" style="color: #CD7F32;" title="3rd"></i>
                </td>
                <td style="width: 35px">
                  <img :src="value.image" class="ability-icon-sm"/>
                </td>
                <td>
                  <span>{{value.name}}</span>
                </td>
              </tr>
            </template>
          </table>
          <i>If you are not 1st picking one these abilities you better know what your doing!</i>
        </b-card>
        <br />
        <h4 class="text-center">Combos</h4>
        <p>
          AD is build on ability combos. While a single ability is good a great combo can destroy your opponents.
          We have included the top combos for Ability pairs and Ultimate pairs.
        </p>
        <b-card v-if="combos" bg-variant="secondary">
          <div>
            <b>Top Ability Pairs</b>
          </div>
          <table class="table table-sm" style="color: white;">
            <template v-for="(value, index) in combos.abilities['kda']">
              <tr :key="index">
                <td style="width: 35px">
                  <i v-if="index == 0" class="fas fa-2x fa-award" style="color: #FFD700;" title="1st"></i>
                  <i v-if="index == 1" class="fas fa-2x fa-award" style="color: #C0C0C0;" title="2nd"></i>
                  <i v-if="index == 2" class="fas fa-2x fa-award" style="color: #CD7F32;" title="3rd"></i>
                </td>
                <td style="width: 35px">
                  <img :src="value.ability1.image" class="ability-icon-sm"/>
                </td>
                <td style="width: 300px">
                  <span>{{value.ability1.name}}</span>
                </td>
                <td style="width: 35px">
                  <img :src="value.ability2.image" class="ability-icon-sm"/>
                </td>
                <td style="width: 300px">
                  <span>{{value.ability2.name}}</span>
                </td>
              </tr>
            </template>
          </table>
        </b-card>
        <br />
        <b-card v-if="combos" bg-variant="secondary">
          <div>
            <b>Top Ultimate Pairs</b>
          </div>
          <table class="table table-sm" style="color: white;">
            <template v-for="(value, index) in combos.ulimates['kda']">
              <tr :key="index">
                <td style="width: 35px">
                  <i v-if="index == 0" class="fas fa-2x fa-award" style="color: #FFD700;" title="1st"></i>
                  <i v-if="index == 1" class="fas fa-2x fa-award" style="color: #C0C0C0;" title="2nd"></i>
                  <i v-if="index == 2" class="fas fa-2x fa-award" style="color: #CD7F32;" title="3rd"></i>
                </td>
                <td style="width: 35px">
                  <img :src="value.ability1.image" class="ability-icon-sm"/>
                </td>
                <td style="width: 300px">
                  <span>{{value.ability1.name}}</span>
                </td>
                <td style="width: 35px">
                  <img :src="value.ability2.image" class="ability-icon-sm"/>
                </td>
                <td style="width: 300px">
                  <span>{{value.ability2.name}}</span>
                </td>
              </tr>
            </template>
          </table>
        </b-card>
        <p>
          We have provided a small summary of the Abilities in AD. 
          Check out the <b-link to="/abilities">Abilities Page</b-link> for more information and dive into each ability's details.
        </p>
        <br />
        <h4 class="text-center">Leaderboard</h4>
        <p>
          We have collected the top players in each region, ordered by win rate then by # of matches. 
          We only include matches with our export range.
          You need to have the <b class="text-info">'Expose Public Match Data'</b> setting enabled in Dota2 to be included.
          If a region is missing then it did not have a enough AD matches to be include in the export.
        </p>
        <b-card v-if="leaderboard" bg-variant="secondary">
          <div>
            <b>Leaderboard</b>
          </div>
          <table class="table table-sm" style="color: white;">
            <tr>
              <td>Region</td>
              <td></td>
              <td>Persona</td>
              <td class="text-center">Win Rate</td>
              <td class="text-center">Matches</td>
            </tr>
            <template v-for="(value, key, index) in leaderboard.regions">
              <tr :key="index" v-if="value.length > 0">
                <td>{{key}}</td>
                <td>
                  <div>
                    <img :src="value[0].avatar" class="ability-icon-sm" />
                  </div>
                </td>
                <td>
                  <div>
                    <span>{{value[0].name}}</span>
                  </div>
                </td>
                <td>
                  <div>
                    <b-progress variant="info" :value="value[0].win_rate" :min="0" :max="1" :striped="true" show-progress></b-progress>
                  </div>
                </td>
                <td>
                  <div class="text-center">
                    {{value[0].matches}}
                  </div>
                </td>
              </tr>
            </template>
          </table>
          <i>Shout-out those of you that have a 100% win rate.</i>
        </b-card>
        <p>
          Check out the <b-link to="/leaderboard">Leaderboard Page</b-link> for more details and find where you or your friends rank in your region.
        </p>
        <br />
        <h4 class="text-center">Matches</h4>
        <p>
          We are often asked when do people play AD? To that end we have included the daily counts broken down by the time of day matches are being played with the times, offset to match your browser's timezone. We also include the total # matches per region. There is always going to be a little bias based on day and time we run the export.
        </p>
        <b-card bg-variant="secondary">
          <div>
            <b>Matches / Region</b>
          </div>
          <table class="table table-sm" style="color: white;">
            <template v-for="(value, key, index) in matches.regions">
              <tr :key="index">
                <td>{{key}}</td>
                <td>{{formatNumber(value)}}</td>
              </tr>
            </template>
          </table>
        </b-card>
        <p>
          Check out the <b-link to="/calendar">Calendar Page</b-link> for more details about how many players are in each region and when they are playing.
        </p>
        <br />
        <p v-if="matches.range">
          We reset our baseline when a major patch is released that changes the balance of abilities.
          We normally export our master database roughly once a week.
          Our current stats collection was started on <b class="text-info">{{ formatDateTime(matches.range.start) }}</b> and was last exported on <b class="text-info">{{ formatDateTime(matches.range.end) }}</b>, that is approximately <b class="text-info">{{ formatDuration(matches.range.start, matches.range.end) }}</b>, with a total of <b class="text-info">{{ formatNumber(matches.range.matches) }}</b> AD matches processed. 
          But we did notice that <b class="text-info">{{ formatPercentage(matches.range.abandoned_ratio) }}</b> of matches where abandoned, you can do better people!
        </p>
      </b-col>
    </b-row>
  </hgv-loading>
</template>

<script>

export default {
  name: 'home',
  data () {
    return {
      construction: true,
      urls: [
        '/static/schedule.json', 
        '/static/heroes-types.json', 
        '/static/heroes-roles.json', 
        '/static/summary-heroes.json',
        '/static/leaderboard-regions.json',
        '/static/summary-abilities.json',
        '/static/summary-combos.json'        
      ],
      matches: {
        range: null,
        regions: [],
      },
      heroes: {
        types: [],
        roles: [],
        best: [],
      },
      leaderboard: null,
      abilities: null,
      combos: null,
    }
  },
  methods: {
    loaded(data) {
      var self = this;
      self.matches.range = data[0].range;
      self.matches.regions = data[0].regions;

      self.heroes.types = data[1].filter(_ => _.region == 2); 
      self.heroes.roles = data[2].filter(_ => _.region == 2);
      self.heroes.best = data[3];

      self.leaderboard = data[4];

      self.abilities = data[5];
      self.combos = data[6];
    }
  }
}
</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped>
.card.bg-secondary {
  color: white;
}
</style>
