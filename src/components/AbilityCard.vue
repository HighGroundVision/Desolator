<template>
  <section>
    <b-card>
      <b-row>
        <b-col cols="2">
          <img :src="ability.image" class="ability-icon-md" />
        </b-col>
        <b-col>
          <h5 v-if="showTitle">{{ability.name}}</h5>
          <span>{{ability.description}}</span><br />
          <br />
          <div v-if="ability.upgrade_description">
            <b-badge class="text-dark">Upgrade</b-badge>
            <span> {{ability.upgrade_description}}</span>
            <br />
          </div>
          <div>
            <b-badge class="text-dark">Keywords</b-badge>
            <span> {{formatKeywords(ability.keywords)}}</span>
          </div>
        </b-col>
      </b-row>
      <hr class="highlighted" />
      <b-row>
        <b-col cols="6">
          <b-row>
            <b-col cols="6"><b-badge class="text-dark">Type</b-badge></b-col>
            <b-col>{{formatAbilityType(ability.ability_type)}}</b-col>
          </b-row>
          <b-row>
            <b-col cols="6"><b-badge class="text-dark">Behaviors</b-badge></b-col>
            <b-col>{{formatAbilityBehaviors(ability.ability_behaviors)}}</b-col>
          </b-row>
          <b-row v-if="formatAbilityUnitTargetTeam(ability.ability_unit_target_team)">
            <b-col cols="6"><b-badge class="text-dark">Unit Target Team</b-badge></b-col>
            <b-col>{{formatAbilityUnitTargetTeam(ability.ability_unit_target_team)}}</b-col>
          </b-row>
          <b-row v-if="formatAbilityUnitTargetType(ability.ability_unit_target_type)">
            <b-col cols="6"><b-badge class="text-dark">Unit Target</b-badge></b-col>
            <b-col>{{formatAbilityUnitTargetType(ability.ability_unit_target_type)}}</b-col>
          </b-row>
          <b-row>
            <b-col cols="6"><b-badge class="text-success">Aghanims Upgrade</b-badge></b-col>
            <b-col>{{ability.has_scepter_upgrade ? "Yes" : "No"}}</b-col>
          </b-row>
          <b-row v-if="formatAbilityUnitDamageType(ability.ability_unit_damage_type)">
            <b-col cols="6"><b-badge class="text-dark">Damage Type</b-badge></b-col>
            <b-col>{{formatAbilityUnitDamageType(ability.ability_unit_damage_type)}}</b-col>
          </b-row>
          <b-row v-if="formatSpellImmunityType(ability.spell_immunity_type)">
            <b-col cols="6"><b-badge class="text-dark">Spell Immunity</b-badge></b-col>
            <b-col>{{formatSpellImmunityType(ability.spell_immunity_type)}}</b-col>
          </b-row>
          <b-row v-if="formatSpellDispellableType(ability.spell_dispellable_type)">
            <b-col cols="6"><b-badge class="text-dark">Dispellable</b-badge></b-col>
            <b-col>{{formatSpellDispellableType(ability.spell_dispellable_type)}}</b-col>
          </b-row>
        </b-col>
        <b-col cols="6">
          <b-row v-if="formatList(ability.ability_mana_cost)">
            <b-col cols="6"><b-badge class="text-primary">Manacost</b-badge></b-col>
            <b-col>{{formatList(ability.ability_mana_cost)}}</b-col>
          </b-row>
          <b-row v-if="formatList(ability.ability_cooldown)">
            <b-col cols="6"><b-badge class="text-warning">Cooldown</b-badge></b-col>
            <b-col>{{formatList(ability.ability_cooldown)}}</b-col>
          </b-row>
          <b-row v-if="formatList(ability.ability_cast_range)">
            <b-col cols="6"><b-badge class="text-dark">Cast Range</b-badge></b-col>
            <b-col>{{formatList(ability.ability_cast_range)}}</b-col>
          </b-row>
          <b-row v-if="formatList(ability.ability_channel_time)">
            <b-col cols="6"><b-badge class="text-dark">Channel Time</b-badge></b-col>
            <b-col>{{formatList(ability.ability_channel_time)}}</b-col>
          </b-row>
          <b-row v-if="formatList(ability.ability_duration)">
            <b-col cols="6"><b-badge class="text-dark">Duration</b-badge></b-col>
            <b-col>{{formatList(ability.ability_duration)}}</b-col>
          </b-row>
          <b-row v-if="formatList(ability.ability_damage)">
            <b-col cols="6"><b-badge class="text-dark">Damage</b-badge></b-col>
            <b-col>{{formatList(ability.ability_damage)}}</b-col>
          </b-row>

          <template v-for="(value, key) in ability.ability_special">
            <b-row :key="key">
              <b-col cols="6"><b-badge class="text-dark">{{formatKey(key)}}</b-badge></b-col>
              <b-col>{{formatList(value)}}</b-col>
            </b-row>
          </template>
        </b-col>
      </b-row>
      
      
    </b-card>
  </section>
</template>

<script>
const humanizeString = require('humanize-string');

export default {
  name: 'AbilityCard',
  props: {
    ability: Object,
    showTitle: Boolean,
  },
  methods: {
    formatList(list) {
      if(list)
        return list.join('/');
      else
        return "";
    },
    formatAbilityType(value) {
      if(value == "DOTA_ABILITY_TYPE_BASIC") 
        return "Skill";
      else if(value == "DOTA_ABILITY_TYPE_ULTIMATE") 
        return "Ultimate";
      else 
        return "";
    },
    formatAbilityBehaviors(list) {
      var value = [];
      if(list.includes("DOTA_ABILITY_BEHAVIOR_UNIT_TARGET")) {
        value.push("Unit Target");
      }
      if(list.includes("DOTA_ABILITY_BEHAVIOR_POINT_TARGET")) {
        value.push("Point Target");
      }
      if(list.includes("DOTA_ABILITY_BEHAVIOR_NO_TARGET")) {
        value.push("No Target");
      }
      if(list.includes("DOTA_ABILITY_BEHAVIOR_AOE")) {
        value.push("AoE");
      }
      if(list.includes("DOTA_ABILITY_BEHAVIOR_POINT")) {
        value.push("Point");
      }
      if(list.includes("DOTA_ABILITY_BEHAVIOR_AUTOCAST")) {
        value.push("Autocast");
      }
      if(list.includes("DOTA_ABILITY_BEHAVIOR_ATTACK")) {
        value.push("Attack");
      }
      if(list.includes("DOTA_ABILITY_BEHAVIOR_PASSIVE")) {
        value.push("Passive");
      }
      if(list.includes("DOTA_ABILITY_BEHAVIOR_IMMEDIATE")) {
        value.push("Immediate");
      }
      if(list.includes("DOTA_ABILITY_BEHAVIOR_CHANNELLED")) {
        value.push("Channelled");
      }
      if(list.includes("DOTA_ABILITY_BEHAVIOR_TOGGLE")) {
        value.push("Toggle");
      }
      if(list.includes("DOTA_ABILITY_BEHAVIOR_DIRECTIONAL")) {
        value.push("Directional");
      }
      if(list.includes("DOTA_ABILITY_BEHAVIOR_AURA")) {
        value.push("Aura");
      }
     
      return value.join(", ");
    },
    formatAbilityUnitTargetTeam(value) {
      if(value == "DOTA_UNIT_TARGET_TEAM_FRIENDLY") {
        return "Friendly";
      } else if(value == "DOTA_UNIT_TARGET_TEAM_ENEMY") {
        return "Enemy";
      } else if(value == "DOTA_UNIT_TARGET_TEAM_BOTH") {
        return "Both";
      } else if(value == "DOTA_UNIT_TARGET_TEAM_ENEMY | DOTA_UNIT_TARGET_TEAM_FRIENDLY") {
        return "Either";
      } else if(value == "DOTA_UNIT_TARGET_TEAM_FRIENDLY | DOTA_UNIT_TARGET_TEAM_ENEMY") {
        return "Either";
      } else {
        return "";
      }
    },
    formatAbilityUnitTargetType(list) {
      var value = [];
      if(list.includes("DOTA_UNIT_TARGET_HERO")) {
        value.push("Hero");
      }
      if(list.includes("DOTA_UNIT_TARGET_BASIC")) {
        value.push("Basic");
      }
      if(list.includes("DOTA_UNIT_TARGET_CREEP")) {
        value.push("Creep");
      }
      if(list.includes("DOTA_UNIT_TARGET_BUILDING")) {
        value.push("Building");
      }
      if(list.includes("DOTA_UNIT_TARGET_TREE")) {
        value.push("Tree");
      }

      if(value.length > 0)
        return value.join(", ");
      else 
        return "";
    },
    formatSpellImmunityType(value) {
      if(value == "SPELL_IMMUNITY_ALLIES_YES")
        return "Doesn't Effect Allies";
      else if(value == "SPELL_IMMUNITY_ALLIES_NO")
        return "Effect Allies";
      else if(value == "SPELL_IMMUNITY_ENEMIES_NO")
        return "Doesn't Effect Enemies";
      else if(value == "SPELL_IMMUNITY_ENEMIES_YES")
        return "Effect Enemies";
      else 
        return "";
    },
    formatSpellDispellableType(value) {
      if(value == "SPELL_DISPELLABLE_YES")
        return "Yes";
      else if(value == "SPELL_DISPELLABLE_NO")
        return "No";
      else
        return "";
    },
    formatAbilityUnitDamageType(value) {
      if(value == "DAMAGE_TYPE_PHYSICAL")
        return "Physical";
      else if(value == "DAMAGE_TYPE_MAGICAL")
        return "Magical";
      else if(value == "DAMAGE_TYPE_PURE")
        return "Pure";
      else 
        return "";
    },
    formatKeywords(value) {
      if(value)
        return value.join(', ');
      else
        return "";
    },
    formatKey(name) {
      return humanizeString(name);
    }
  }
}
</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped>
</style>
