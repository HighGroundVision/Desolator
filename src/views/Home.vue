<template>
  <b-row>
    <b-col>
      <b-row>
        <b-col>
          <h4 class="text-center">Welcome!</h4>
        </b-col>
      </b-row>
      <hr class="highlighted" />
      <b-row>
        <b-col cols="3">
          <div class="text-center">
            <b-alert variant="info bordered-alert" show>
              <strong>Hi, I am Cluckles!</strong>
              <br />
              <span>AD is hard and confusing, luckily I am here to help make it fun!</span>
            </b-alert>
            <div style="float: right; position: relative; top:-15px;">
              <a href="https://steamcommunity.com/sharedfiles/filedetails/?id=111384500" target="_blank" class="text-white">
                <i class="far fa-question-circle" title="I also come in this really cool Courier form too!"></i>
              </a>
            </div>
            <img src="@/assets/imgs/cluckles.png" class="cluckles-speach" />
          </div>
        </b-col>
        <b-col>
          <p>
            We at High Ground Vision are dedicated to the Ability Draft community.
          </p>
          <p>
            Ability Draft is not really understood by the majority of the Dota community who think it is confusing and a joke.
            But that is not the case, there is addational level of depth required to draft a hero a the begining of every match.
            We aim to help players understand the things that people miss understand about ability draft.
            This includes the Draft Pool, Hero Strength, Ability Pick Priority, and all persented in an interesting and informative way.
          </p>
        </b-col>
      </b-row>
      <hr class="highlighted" />
      <b-row>
        <b-col>
          <div class="text-center">
            <img src="@/assets/imgs/outlanders.png" style="width: 30%; height: 30%;" class="float-right rounded" />
          </div>
          <p>
            The Outlanders update has added a new Hero! 
          </p>
          <p>
            Welcome Snapfire and her abilties Scatterblast, Firesnap Cookie, Lil' Shredder and  Mortimer Kisses.
          </p>
          <p>
            Void Sprit is still in the void for AD players and is not in the draft pool. He joins Earth Spirit, Invoker, Lone Druid, and Morphling as the 5 disabled heroes.
          </p>
          <p>
            The 7.23 patch added a bunch of new scepter grant abilties these are <span class="text-uppercase"> not </span> enabled don't expect them to work. Check out the <b-link to="/pool">Pool</b-link> page for more details.
          </p>
          <p>
            With this new patch we have created a data model that can be capture the connections and history of heroes and abilties. 
            Look for our fancy new charts that explore these connections and provide a view of this history.
          </p>
        </b-col>
      </b-row>
      <hr v-if="construction" class="highlighted" />
      <b-row v-if="construction" >
        <b-col cols="3">
          <div class="text-center">
            <b-alert variant="warning bordered-alert" show>
              <strong>Under Construction</strong>
              <br />
              <span>We are currently rebuilding the systems that run this web application. </span>
            </b-alert>
            <img src="@/assets/imgs/cluckles.png" class="cluckles-speach" />
          </div>
        </b-col>
        <b-col>
          <p>
            We are currently rebuilding the systems that run this web application. 
            Our continuous deployment process means that this site will be updated as we push out new features. 
            You are welcome to use the site and the new features. 
            Some features maybe unstable/incomplete and maybe removed in the future.
          </p>
          <p>This banner will be removed when the rework is finished.</p>
        </b-col>
      </b-row>
    </b-col>
  </b-row>
</template>

<script>
export default {
  name: 'home',
  data () {
    return {
      construction: true,
    }
  }
}
</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped>
  
</style>

