<template>
  <div class="sticky-container">
    <ul class="sticky">
      <li>
        <i class="fab fa-2x fa-twitter-square text-warning"></i>
        <p><a href="https://twitter.com/DotaHGV/" target="_blank">Follow Us on<br>Twitter</a></p>
      </li>
      <li>
        <i class="fab fa-2x fa-reddit-square text-warning"></i>
        <p><a href="https://www.reddit.com/r/abilitydraft/" target="_blank">Find Us on<br>Reddit</a></p>
      </li>
    </ul>
  </div>
</template>

<script>
export default {
  name: 'SoicalFollow',
  data () {
    return {}
  }
}
</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped>
.sticky-container{
  padding:0px;
  margin:0px;
  position:fixed;
  right:-135px;
  top:230px;
  width:210px;
  z-index: 1100;
}
.sticky li{
  border-radius: 5px 5px 5px 5px; 
  list-style-type:none;
  background-color: #0B1425;
  color:#efefef;
  height:43px;
  padding:0px;
  margin:0px 0px 1px 0px;
  -webkit-transition:all 0.25s ease-in-out;
  -moz-transition:all 0.25s ease-in-out;
  -o-transition:all 0.25s ease-in-out;
  transition:all 0.25s ease-in-out;
  cursor:pointer;
}
.sticky li:hover{
  margin-left:-115px;
}
.sticky li i{
  float:left;
  margin:5px 4px;
  margin-right:5px;
}
.sticky li p{
  padding-top:5px;
  margin:0px;
  line-height:16px;
  font-size:11px;
}
.sticky li p a{
  text-decoration:none;
  color:#ffff;
}
.sticky li p a:hover{
  text-decoration:underline;
}
</style>
