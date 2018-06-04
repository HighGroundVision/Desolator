<template>
  <section class="opaque-background">
    <b-row>
      <b-col cols="4" lg="3">
        <div class="text-center">
            <b-img src="/static/images/cluckles.png" fluid  />
            <b-alert variant="info" show>   
                <strong>Hi, I am Cluckles!</strong>
                <br />
                <span>AD is hard and confusing, lucky I am here to help make it fun!</span>
            </b-alert>
        </div>
      </b-col>
      <b-col>
        <h1 class="text-warning">Ability Drafter</h1>
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
        <p>So what is Ability Draft [AD]? Players are given a random hero from Ability-Draft enabled heroes. Each players' abilities are put into a pool (with a few exceptions, see Pool). Players take turns selecting abilities from the pool, given five seconds for each choice. Each player selects three normal abilities and one ultimate. AD is niche game mode that accounts for about 1% of total matches played daily. AD requires a large knowledge base of default hero attributes, ability attributes, ability combinations, and exceptions.</p>
        <p>We here at High Ground Vision [HGV] have been playing AD matches almost exclusively for years now, with ~5,000 AD games between the team. HGV has been collecting stats on AD games since the major changes in 7.07 on Nov 2017. We have been focusing on Ability and Combo win rates and community engagement.</p>
        <p>The first step of AD is knowing the <b-link :to="'/pool'">Draft Pool</b-link>. Which heroes are active in the pool, which heroes' abilities are included in the pool and which heroes' aghanim's scepter upgrade works? The pool of available heroes and abilities has never been greater than with the 7.07 patch. With only 11 heroes left out of the draft, there are a lot of options to work with.</p>
        <p>The next step is getting to know the <b-link :to="'/abilities'">Abilities</b-link> and <b-link :to="'/combos'">Combos</b-link>. We have compiled win rates since 7.07 patch using random sampling every day. We have also added a breakdown of that win rate by primary type (Str, Agi, Int) and attack type (Melee, Range). We also let users rank and share Abilities and Combos with the community.</p>
        <p>The next step is tracking your matches. After login, you can view your match history to see a list filtered for your AD matches. Because of a number of limitations with the STEAM API, this may take some time for your matches to populate. After which you will be able to view match details. This will process the details into a format a Drafter will find interesting and gain some insights from. This includes ability casts, hits, damage, and win rates.</p>
        <p>HGV started as a side project of a couple of Dota2 fans and it still operates that way. We have a number of other <a href="http://highgroundvision.com">niche tools</a> for Dota2. We don’t intend to make any money from it, although we try to at least break even on the server bills. We’re also happy to share our knowledge with the community <a href="https://github.com/HighGroundVision/" target="_blank">open sourcing</a> our stack.  Development is done by volunteers contributing in their free time, so we don’t have employees.</p>
        <p>Provide any feedback about this project to <a href="https://twitter.com/DotaHGV" target="_blank">@DotaHGV</a> or open an issue on <a href="https://github.com/HighGroundVision/Desolator/issues" target="_blank">GitHub</a></p>
      </b-col>
    </b-row>
  </section>
</template>

<script>
export default {
  name: 'Dashboard',
  data () {
    return {}
  }
}
</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped>
</style>
