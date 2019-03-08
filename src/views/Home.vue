<template>
  <section>
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
        <b-alert variant="warning" show>
          <b-row>
            <b-col>
              <strong>Under Construction</strong>
              <p>
                We are currently rebuilding the backend and front systems that run this web application. 
                Our continuous deployment process means that this site will be updated as we push out new features. 
                You are welcome to use the site and the new features. 
                Some features maybe unstable/incomplete and maybe removed in the future.
                This banner will be removed when the rework is finished.
              </p>
            </b-col>
          </b-row>
        </b-alert>
        <p>So what is <span title="AD">Ability Draft</span>? Players are given a random hero from AD enabled heroes. Each hero's enabled abilities are put into a pool (with a few exceptions). Players take turns selecting abilities from the pool, given five seconds for each choice. Each player selects three normal abilities and one ultimate. AD is niche game mode that accounts for about 1% of total matches played daily. AD requires a large knowledge base of default hero attributes, ability attributes, ability combinations, and exceptions.</p>
        <p>We here at High Ground Vision [HGV] have been playing AD matches almost exclusively for years now. HGV has been collecting stats on AD games since the major changes in 7.07 on Nov 2017. We have been focusing on Ability and Combo win rates and community engagement.</p>
        <p>The first step of AD is knowing the <b-link :to="'/pool'">Draft Pool</b-link>. Not all heroes are in the pool, and some abilities of some heroes are not available as well.  Also, in multiple cases Aghanim’s scepter does not work, or requires a specific hero or combo to function. The pool of available heroes and abilities has never been greater than with the 7.07 patch. With only a handful heroes left out of the draft, there are a lot of options to work with.</p>
        <p>The next step is getting to know the <b-link :to="'/heroes'">Heroes</b-link> and <b-link :to="'/abilities'">Abilities</b-link>. We have compiled win rates using random daily sampling. For each hero + abilty, plus the hero + abiltiy pair and ability + abiltiy pairs.</p>
        <p>HGV started as a side project of a group of Dota2 fans and it still operates that way. We have a number of other <a href="http://highgroundvision.com">niche tools</a> for Dota2. We don’t intend to make any money from it, although we try to at least break even on the server bills. We’re also happy to share our knowledge with the community by <a href="https://github.com/HighGroundVision/" target="_blank">open sourcing</a> our stack.  Development is done by volunteers contributing in their free time.</p>
        <p>Provide any feedback about this project to <a href="https://twitter.com/DotaHGV" target="_blank">@DotaHGV</a> or open an issue on <a href="https://github.com/HighGroundVision/Desolator/issues" target="_blank">GitHub</a></p>
      </b-col>
    </b-row>
  </section>
</template>

<script>
// @ is an alias to /src
// import HelloWorld from '@/components/HelloWorld.vue'

export default {
  name: 'home',
  // components: { HelloWorld }
}
</script>
