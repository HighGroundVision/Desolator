<template>
  <section>
    <div>
      <h4  class="text-center">Abilities</h4>
      <hr class="highlighted" />
    </div>
    <b-row>
      <b-col cols="4" lg="3">
        <div class="text-center">
          <b-alert variant="info" show>   
            <p>
              Search for an ability by name or by the hero's they belong to.
              Click the ability to learn its secrets!
            </p>
          </b-alert>
          <img src="@/assets/imgs/cluckles-speach.png" class="cluckles-speach" />
        </div>
      </b-col>
      <b-col>
        <b-form @submit.prevent="find">
          <b-input-group>
            <b-input-group-addon>
              <b-dropdown :text="searchLabel" variant="light">
                <b-dropdown-item @click="search_type = 1">By Ability</b-dropdown-item>
                <b-dropdown-item @click="search_type = 2">By Hero</b-dropdown-item>
                <b-dropdown-item @click="search_type = 3">By Keyword</b-dropdown-item>
              </b-dropdown>
            </b-input-group-addon>
            <b-form-input type="text" v-model="search_item" />
            <b-input-group-addon>
              <b-button variant="success" @click="find">Find</b-button>
            </b-input-group-addon>
          </b-input-group>
        </b-form>
        <br />
        <b-row>
          <template v-for="(value) in abilities">
            <b-col :key="value.id" cols="3">
              <img :src="value.image" class="m-1 ability-icon-sm "/>
              <b-link :to="'/ability/' + value.id">{{value.name}}</b-link>
            </b-col>
          </template>
          <b-btn v-if="search_more" @click="more" variant="link">More</b-btn>
        </b-row>
        <br />
        <p>
          When Searching we only display the top 24 results by win rate. 
          If you the ability is not there then it is more then likely not included in the Ability Draft. 
          Check out the draft pool for more details.
        </p>
        <hr class="highlighted" />
        <p>
          We have grouped the abilities by keyword.
          These keywords represent the primary mechanics.
          The count is approximately the number of abilities at that fall under this group (there is always some error in the labeled data).
          While we have also included the win rate for each group.
        </p>
        <b-table small
          :fields="['keyword', 'count', 'win_rate']"
          :items="groups" 
          >
          <template slot="keyword" slot-scope="row">
            <a v-if="row.item.win_rate > 0.505" href="javascript:void(0);"><b class="text-success" @click="findKeyword(row.item.keyword)">{{row.item.keyword}}</b></a>
            <a v-else-if="row.item.win_rate < 0.495" href="javascript:void(0);"><b class="text-warning" @click="findKeyword(row.item.keyword)">{{row.item.keyword}}</b></a>
            <a v-else href="javascript:void(0);"><b class="text-info" @click="findKeyword(row.item.keyword)">{{row.item.keyword}}</b></a>
          </template>
          <template slot="count" slot-scope="row">
            {{row.item.count}}
          </template>
          <template slot="win_rate" slot-scope="row">
            <b-progress :value="row.item.win_rate" :min="0" :max="1" :striped="true" show-progress></b-progress>
          </template>
          
        </b-table>
      </b-col>
    </b-row>
    
  </section>
</template>

<script>
import numeral from 'numeral'
import groups from '@/assets/data/ability-groups.json'
import abilities from '@/assets/data/ability-collection.json'

export default {
  name: 'abilities',
  data () {
    return {
      'groups': groups,
      'search_item': null,
      'search_type': 1,
      'search_limit': 12,
      'search_more': false,
      'abilities': [],
    }
  },
  computed: {
    searchLabel: function () {
      return this.search_type == 1 ? 'Search By Ability' : this.search_type == 2 ? 'Search By Hero' : this.search_type == 3 ? 'Search By Keyword' : '...';
    }
  },
  methods: {
    formatPercentage(value) {
      return numeral(value).format('0%');
    },
    findKeyword(item) {
      this.search_item = item;
      this.search_type = 3;
      this.search_more = false;
      this.search_limit = 12;
      this.find();
    },
    find() {
      if(this.search_item) {
        let s = this.search_item.toLowerCase();
        let t = this.search_type;
        let l = this.search_limit;
        var collection = [];
        
        if(t == 1) {
          collection = abilities.filter(_ => _.name.toLowerCase().includes(s));
        } else if(t == 2) {
          collection = abilities.filter(_ => _.hero_name.toLowerCase().includes(s));
        } else if(t == 3) {
          collection = abilities.filter(_ => _.keywords.map(_ => _.toLowerCase()).includes(s));
        }
        
        collection.sort((lhs, rhs) => rhs.win_rate - lhs.win_rate);
        this.abilities = collection.slice(0, l);
        this.search_more = collection.length !=  this.abilities.length && collection.length > 0;
      } else {
        this.abilities = [];
      }
    },
    more() {
      this.search_limit = this.search_limit + 12;
      this.find();
    }
  }
}
</script>