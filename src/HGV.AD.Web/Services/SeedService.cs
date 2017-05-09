using HGV.AD.Web.Data;
using HGV.AD.Web.Models.Attributes;
using HGV.AD.Web.Models.DotaApi.Raindrop;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using CsvHelper;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace HGV.AD.Web.Services
{
    public class SeedService
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly ILogger _logger;

        public SeedService(
            ApplicationDbContext dbcontext,
            ILoggerFactory loggerFactory
        )
        {
            _dbContext = dbcontext;
            _dbContext.Database.SetCommandTimeout(3600); // 1 hr to complete seed methods

            _logger = loggerFactory.CreateLogger<SeedService>();
        }

        //~ time unknown
        public void MigrateDB()
        {
            _dbContext.Database.Migrate();
        }

        public void SeedHeroes()
        {
            var httpClient = new HttpClient();
            var jsonData = httpClient.GetStringAsync("http://hgv-raindrop.azurewebsites.net/api/heroes").Result;
            var heroes = JsonConvert.DeserializeObject<List<Hero>>(jsonData);

            foreach (var item in heroes)
            {
                // Hero
                var hero = _dbContext.Heroes.SingleOrDefault(_ => _.HeroId == item.hero_id);
                if(item.ability_draft_enabled == true)
                {
                    if (hero == null)
                    {
                        hero = new HeroAttributes() { HeroId = item.hero_id };
                        _dbContext.Heroes.Add(hero);
                    }
                }
                else
                {
                    if (hero != null)
                    {
                        _dbContext.Heroes.Remove(hero);
                    }

                    continue;
                }

                var primary = "";
                if(item.attribute_primary == 1)
                    primary = "STR";
                else if (item.attribute_primary == 2)
                    primary = "AGI";
                else if (item.attribute_primary == 3)
                    primary = "INT";

                hero.Name = item.name;
                hero.Patch = "7.05";
                hero.Primary = primary;
                hero.Movespeed = item.movement_speed;
                hero.MaxDmg = item.attack_damage_max;
                hero.MinDmg = item.attack_damage_min;
                hero.AvgDmg = (item.attack_damage_min + item.attack_damage_max) / 2;
                hero.HP = item.status_health;
                hero.Mana = item.status_mana;
                hero.HPRegen = item.status_health_regen;
                hero.ManaRegen = item.status_mana_regen;
                hero.Armor = item.armor;
                hero.Range = item.attack_range;
                hero.ProjectileSpeed = item.attack_projectile_speed;
                hero.BaseStr = item.attribute_base_strength;
                hero.BaseAgi = item.attribute_base_agility;
                hero.BaseInt = item.attribute_base_intelligence;
                hero.StrGain = item.attribute_strength_gain;
                hero.AgiGain = item.attribute_agility_gain;
                hero.IntGain = item.attribute_intelligence_gain;
                hero.DayVision = item.vision_daytime_range;
                hero.NightVision = item.vision_nighttime_range;
                hero.Turnrate = item.movement_turn_rate;
            }
            _dbContext.SaveChanges();

            // Attribute Ranks
            _dbContext.HeroAttributeRanks.RemoveRange(_dbContext.HeroAttributeRanks);
            _dbContext.SaveChanges();

            var attributes = new List<AttributeSumary>();
            attributes.Add(new AttributeSumary() { Name = "Base Str", Expression = _ => _.BaseStr, OrderBy = true });
            attributes.Add(new AttributeSumary() { Name = "Str Gain", Expression = _ => _.StrGain, OrderBy = true });
            attributes.Add(new AttributeSumary() { Name = "Base Agi", Expression = _ => _.BaseAgi, OrderBy = true });
            attributes.Add(new AttributeSumary() { Name = "Agi Gain", Expression = _ => _.AgiGain, OrderBy = true });
            attributes.Add(new AttributeSumary() { Name = "Base Int", Expression = _ => _.BaseInt, OrderBy = true });
            attributes.Add(new AttributeSumary() { Name = "Int Gain", Expression = _ => _.IntGain, OrderBy = true });
            attributes.Add(new AttributeSumary() { Name = "HP", Expression = _ => _.HP, OrderBy = true });
            attributes.Add(new AttributeSumary() { Name = "HP Regen", Expression = _ => _.HPRegen, OrderBy = true });
            attributes.Add(new AttributeSumary() { Name = "Mana", Expression = _ => _.Mana, OrderBy = true });
            attributes.Add(new AttributeSumary() { Name = "Armor", Expression = _ => _.Armor, OrderBy = true });
            attributes.Add(new AttributeSumary() { Name = "Max Dmg", Expression = _ => _.MaxDmg, OrderBy = true });
            attributes.Add(new AttributeSumary() { Name = "Min Dmg", Expression = _ => _.MinDmg, OrderBy = true });
            attributes.Add(new AttributeSumary() { Name = "Movespeed", Expression = _ => _.Movespeed, OrderBy = true });
            attributes.Add(new AttributeSumary() { Name = "Projectile", Expression = _ => _.ProjectileSpeed, OrderBy = true });
            attributes.Add(new AttributeSumary() { Name = "Range", Expression = _ => _.Range, OrderBy = true });
            attributes.Add(new AttributeSumary() { Name = "Turnrate", Expression = _ => _.Turnrate, OrderBy = false });

            var heroeCollection = _dbContext.Heroes.ToList();
            foreach (var hero in heroeCollection)
            {
                var index = 1;
                foreach (var item in attributes)
                {
                    var value = item.Expression.Compile().Invoke(hero);
                    var rank = item.OrderBy == true ?
                            _dbContext.Heroes.GroupBy(item.Expression).Select(_ => _.Key).OrderByDescending(_ => _).ToList().IndexOf(value) + 1 :
                            _dbContext.Heroes.GroupBy(item.Expression).Select(_ => _.Key).OrderBy(_ => _).ToList().IndexOf(value) + 1;

                    var entity = new HeroAttributeRank()
                    {
                        Index = index++,
                        HeroId = hero.HeroId,
                        Name = item.Name,
                        Value = value,
                        Rank = rank,
                        Min = _dbContext.Heroes.Min(item.Expression),
                        Max = _dbContext.Heroes.Max(item.Expression),
                        Avg = _dbContext.Heroes.Average(item.Expression),
                        Groups = _dbContext.Heroes.GroupBy(item.Expression).Count(),
                    };

                    entity.Percentage = (entity.Groups - entity.Rank + 1) / entity.Groups;

                    _dbContext.HeroAttributeRanks.Add(entity);
                }
            }

            _dbContext.SaveChanges();
        }

        public void SeedAbilities()
        {
            var abilities = new List<AbilityAttributes>()
            {
                new AbilityAttributes() { AbilityId = 5003, Identity = "antimage_mana_break", HeroId = 1, Ultimate = false, Notes = "" },
                new AbilityAttributes() { AbilityId = 5004, Identity = "antimage_blink", HeroId = 1, Ultimate = false, Notes = "" },
                new AbilityAttributes() { AbilityId = 5005, Identity = "antimage_spell_shield", HeroId = 1, Ultimate = false, Notes = "" },
                new AbilityAttributes() { AbilityId = 5006, Identity = "antimage_mana_void", HeroId = 1, Ultimate = true , Notes = "" },
                new AbilityAttributes() { AbilityId = 5007, Identity = "axe_berserkers_call", HeroId = 2, Ultimate = false, Notes = "" },
                new AbilityAttributes() { AbilityId = 5008, Identity = "axe_battle_hunger", HeroId = 2, Ultimate = false, Notes = "" },
                new AbilityAttributes() { AbilityId = 5009, Identity = "axe_counter_helix", HeroId = 2, Ultimate = false, Notes = "" },
                new AbilityAttributes() { AbilityId = 5010, Identity = "axe_culling_blade", HeroId = 2, Ultimate = true , Notes = "" },
                new AbilityAttributes() { AbilityId = 5011, Identity = "bane_brain_sap", HeroId = 3, Ultimate = false, Notes = "" },
                new AbilityAttributes() { AbilityId = 5012, Identity = "bane_enfeeble", HeroId = 3, Ultimate = false, Notes = "" },
                new AbilityAttributes() { AbilityId = 5013, Identity = "bane_fiends_grip", HeroId = 3, Ultimate = true , Notes = "" },
                new AbilityAttributes() { AbilityId = 5014, Identity = "bane_nightmare", HeroId = 3, Ultimate = false, Notes = "" },
                new AbilityAttributes() { AbilityId = 5015, Identity = "bloodseeker_bloodrage", HeroId = 4, Ultimate = false, Notes = "" },
                new AbilityAttributes() { AbilityId = 5016, Identity = "bloodseeker_blood_bath", HeroId = 4, Ultimate = false, Notes = "" },
                new AbilityAttributes() { AbilityId = 5017, Identity = "bloodseeker_thirst", HeroId = 4, Ultimate = false, Notes = "" },
                new AbilityAttributes() { AbilityId = 5018, Identity = "bloodseeker_rupture", HeroId = 4, Ultimate = true , Notes = "" },
                new AbilityAttributes() { AbilityId = 5019, Identity = "drow_ranger_frost_arrows",HeroId = 6, Ultimate = false, Notes = "" },
                new AbilityAttributes() { AbilityId = 5021, Identity = "drow_ranger_trueshot",HeroId = 6, Ultimate = false, Notes = "" },
                new AbilityAttributes() { AbilityId = 5022, Identity = "drow_ranger_marksmanship",HeroId = 6, Ultimate = true , Notes = "" },
                new AbilityAttributes() { AbilityId = 5023, Identity = "earthshaker_fissure",HeroId = 7, Ultimate = false, Notes = "" },
                new AbilityAttributes() { AbilityId = 5024, Identity = "earthshaker_enchant_totem",HeroId = 7, Ultimate = false, Notes = "" },
                new AbilityAttributes() { AbilityId = 5025, Identity = "earthshaker_aftershock",HeroId = 7, Ultimate = false, Notes = "" },
                new AbilityAttributes() { AbilityId = 5026, Identity = "earthshaker_echo_slam",HeroId = 7, Ultimate = true , Notes = "" },
                new AbilityAttributes() { AbilityId = 5027, Identity = "juggernaut_blade_dance",HeroId = 8, Ultimate = false, Notes = "" },
                new AbilityAttributes() { AbilityId = 5028, Identity = "juggernaut_blade_fury",HeroId = 8, Ultimate = false, Notes = "" },
                new AbilityAttributes() { AbilityId = 5029, Identity = "juggernaut_healing_ward",HeroId = 8, Ultimate = false, Notes = "" },
                new AbilityAttributes() { AbilityId = 5030, Identity = "juggernaut_omni_slash",HeroId = 8, Ultimate = true , Notes = "" },
                new AbilityAttributes() { AbilityId = 5031, Identity = "kunkka_torrent",HeroId = 23, Ultimate = false, Notes = "" },
                new AbilityAttributes() { AbilityId = 5032, Identity = "kunkka_tidebringer",HeroId = 23, Ultimate = false, Notes = "" },
                new AbilityAttributes() { AbilityId = 5033, Identity = "kunkka_x_marks_the_spot",HeroId = 23, Ultimate = false, Notes = "" },
                new AbilityAttributes() { AbilityId = 5035, Identity = "kunkka_ghostship",HeroId = 23, Ultimate = true , Notes = "" },
                new AbilityAttributes() { AbilityId = 5040, Identity = "lina_dragon_slave",HeroId = 25, Ultimate = false, Notes = "" },
                new AbilityAttributes() { AbilityId = 5041, Identity = "lina_light_strike_array",HeroId = 25, Ultimate = false, Notes = "" },
                new AbilityAttributes() { AbilityId = 5042, Identity = "lina_fiery_soul",HeroId = 25, Ultimate = false, Notes = "" },
                new AbilityAttributes() { AbilityId = 5043, Identity = "lina_laguna_blade",HeroId = 25, Ultimate = true , Notes = "" },
                new AbilityAttributes() { AbilityId = 5044, Identity = "lion_impale",HeroId = 26, Ultimate = false, Notes = "" },
                new AbilityAttributes() { AbilityId = 5045, Identity = "lion_voodoo",HeroId = 26, Ultimate = false, Notes = "" },
                new AbilityAttributes() { AbilityId = 5046, Identity = "lion_mana_drain",HeroId = 26, Ultimate = false, Notes = "" },
                new AbilityAttributes() { AbilityId = 5047, Identity = "lion_finger_of_death",HeroId = 26, Ultimate = true , Notes = "" },
                new AbilityAttributes() { AbilityId = 5048, Identity = "mirana_arrow",HeroId = 9, Ultimate = false, Notes = "" },
                new AbilityAttributes() { AbilityId = 5049, Identity = "mirana_invis",HeroId = 9, Ultimate = true, Keywords = "Invisibility " , Notes = "" },
                new AbilityAttributes() { AbilityId = 5050, Identity = "mirana_leap",HeroId = 9, Ultimate = false, Notes = "" },
                new AbilityAttributes() { AbilityId = 5051, Identity = "mirana_starfall",HeroId = 9, Ultimate = false, Notes = "" },
                new AbilityAttributes() { AbilityId = 5065, Identity = "phantom_lancer_spirit_lance", HeroId = 12, Ultimate = false, Notes = "" },
                new AbilityAttributes() { AbilityId = 5066, Identity = "phantom_lancer_doppelwalk", HeroId = 12, Ultimate = false, Notes = "" },
                new AbilityAttributes() { AbilityId = 5067, Identity = "phantom_lancer_juxtapose", HeroId = 12, Ultimate = true , Notes = "" },
                new AbilityAttributes() { AbilityId = 5068, Identity = "phantom_lancer_phantom_edge", HeroId = 12, Ultimate = false, Notes = "" },
                new AbilityAttributes() { AbilityId = 5074, Identity = "pudge_flesh_heap", HeroId = 14, Ultimate = false, Notes = "" },
                new AbilityAttributes() { AbilityId = 5075, Identity = "pudge_meat_hook", HeroId = 14, Ultimate = false, Notes = "" },
                new AbilityAttributes() { AbilityId = 5076, Identity = "pudge_rot", HeroId = 14, Ultimate = false, Notes = "" },
                new AbilityAttributes() { AbilityId = 5077, Identity = "pudge_dismember", HeroId = 14, Ultimate = true , Notes = "" },
                new AbilityAttributes() { AbilityId = 5078, Identity = "shadow_shaman_ether_shock",  HeroId = 27, Ultimate = false, Notes = "" },
                new AbilityAttributes() { AbilityId = 5079, Identity = "shadow_shaman_voodoo",  HeroId = 27, Ultimate = false, Notes = "" },
                new AbilityAttributes() { AbilityId = 5080, Identity = "shadow_shaman_shackles",  HeroId = 27, Ultimate = false, Notes = "" },
                new AbilityAttributes() { AbilityId = 5081, Identity = "shadow_shaman_mass_serpent_ward",  HeroId = 27, Ultimate = true , Notes = "" },
                new AbilityAttributes() { AbilityId = 5082, Identity = "razor_plasma_field",  HeroId = 15, Ultimate = false, Notes = "" },
                new AbilityAttributes() { AbilityId = 5083, Identity = "razor_static_link",  HeroId = 15, Ultimate = false, Notes = "" },
                new AbilityAttributes() { AbilityId = 5084, Identity = "razor_unstable_current",  HeroId = 15, Ultimate = false, Notes = "" },
                new AbilityAttributes() { AbilityId = 5085, Identity = "razor_eye_of_the_storm",  HeroId = 15, Ultimate = true , Notes = "" },
                new AbilityAttributes() { AbilityId = 5086, Identity = "skeleton_king_hellfire_blast",  HeroId = 42, Ultimate = false, Notes = "" },
                new AbilityAttributes() { AbilityId = 5087, Identity = "skeleton_king_vampiric_aura",  HeroId = 42, Ultimate = false, Keywords = "Lifesteal ", Notes = "" },
                new AbilityAttributes() { AbilityId = 5088, Identity = "skeleton_king_mortal_strike",  HeroId = 42, Ultimate = false, Notes = "" },
                new AbilityAttributes() { AbilityId = 5089, Identity = "skeleton_king_reincarnation",  HeroId = 42, Ultimate = true , Notes = "" },
                new AbilityAttributes() { AbilityId = 5090, Identity = "death_prophet_carrion_swarm",  HeroId = 43, Ultimate = false, Notes = "" },
                new AbilityAttributes() { AbilityId = 5091, Identity = "death_prophet_silence",  HeroId = 43, Ultimate = false, Notes = "" },
                new AbilityAttributes() { AbilityId = 5093, Identity = "death_prophet_exorcism",  HeroId = 43, Ultimate = true , Notes = "" },
                new AbilityAttributes() { AbilityId = 5094, Identity = "sven_storm_bolt",  HeroId = 18, Ultimate = false, Notes = "" },
                new AbilityAttributes() { AbilityId = 5095, Identity = "sven_great_cleave",  HeroId = 18, Ultimate = false, Notes = "" },
                new AbilityAttributes() { AbilityId = 5096, Identity = "sven_warcry",  HeroId = 18, Ultimate = false, Notes = "" },
                new AbilityAttributes() { AbilityId = 5097, Identity = "sven_gods_strength",  HeroId = 18, Ultimate = true , Notes = "" },
                new AbilityAttributes() { AbilityId = 5098, Identity = "storm_spirit_static_remnant",  HeroId = 17, Ultimate = false, Notes = "" },
                new AbilityAttributes() { AbilityId = 5099, Identity = "storm_spirit_electric_vortex",  HeroId = 17, Ultimate = false, Notes = "" },
                new AbilityAttributes() { AbilityId = 5100, Identity = "storm_spirit_overload",  HeroId = 17, Ultimate = false, Notes = "" },
                new AbilityAttributes() { AbilityId = 5101, Identity = "storm_spirit_ball_lightning",  HeroId = 17, Ultimate = true, Notes = "" },
                new AbilityAttributes() { AbilityId = 5102, Identity = "sandking_burrowstrike",  HeroId = 16, Ultimate = false, Notes = "" },
                new AbilityAttributes() { AbilityId = 5103, Identity = "sandking_sand_storm",  HeroId = 16, Ultimate = false, Notes = "" },
                new AbilityAttributes() { AbilityId = 5104, Identity = "sandking_caustic_finale",  HeroId = 16, Ultimate = false, Notes = "" },
                new AbilityAttributes() { AbilityId = 5105, Identity = "sandking_epicenter",  HeroId = 16, Ultimate = true , Notes = "" },
                new AbilityAttributes() { AbilityId = 5106, Identity = "tiny_avalanche",  HeroId = 19, Ultimate = false, Notes = "" },
                new AbilityAttributes() { AbilityId = 5107, Identity = "tiny_toss",  HeroId = 19, Ultimate = false, Notes = "" },
                new AbilityAttributes() { AbilityId = 5108, Identity = "tiny_craggy_exterior",  HeroId = 19, Ultimate = false, Notes = "" },
                new AbilityAttributes() { AbilityId = 5109, Identity = "tiny_grow",  HeroId = 19, Ultimate = true , Notes = "" },
                new AbilityAttributes() { AbilityId = 5110, Identity = "zuus_arc_lightning",  HeroId = 22, Ultimate = false, Notes = "" },
                new AbilityAttributes() { AbilityId = 5111, Identity = "zuus_lightning_bolt",  HeroId = 22, Ultimate = false, Notes = "" },
                new AbilityAttributes() { AbilityId = 5112, Identity = "zuus_static_field",  HeroId = 22, Ultimate = false, Notes = "" },
                new AbilityAttributes() { AbilityId = 5113, Identity = "zuus_thundergods_wrath",  HeroId = 22, Ultimate = true , Notes = "" },
                new AbilityAttributes() { AbilityId = 5114, Identity = "slardar_sprint",  HeroId = 28, Ultimate = false, Notes = "" },
                new AbilityAttributes() { AbilityId = 5115, Identity = "slardar_slithereen_crush",  HeroId = 28, Ultimate = false, Notes = "" },
                new AbilityAttributes() { AbilityId = 5116, Identity = "slardar_bash",  HeroId = 28, Ultimate = false, Notes = "" },
                new AbilityAttributes() { AbilityId = 5117, Identity = "slardar_amplify_damage",  HeroId = 28, Ultimate = true , Notes = "" },
                new AbilityAttributes() { AbilityId = 5118, Identity = "tidehunter_gush",  HeroId = 29, Ultimate = false, Notes = "" },
                new AbilityAttributes() { AbilityId = 5119, Identity = "tidehunter_kraken_shell",  HeroId = 29, Ultimate = false, Notes = "" },
                new AbilityAttributes() { AbilityId = 5120, Identity = "tidehunter_anchor_smash",  HeroId = 29, Ultimate = false, Notes = "" },
                new AbilityAttributes() { AbilityId = 5121, Identity = "tidehunter_ravage",  HeroId = 29, Ultimate = true , Notes = "" },
                new AbilityAttributes() { AbilityId = 5122, Identity = "vengefulspirit_magic_missile",  HeroId = 20, Ultimate = false, Notes = "" },
                new AbilityAttributes() { AbilityId = 5123, Identity = "vengefulspirit_command_aura",  HeroId = 20, Ultimate = false, Notes = "" },
                new AbilityAttributes() { AbilityId = 5124, Identity = "vengefulspirit_wave_of_terror",  HeroId = 20, Ultimate = false, Notes = "" },
                new AbilityAttributes() { AbilityId = 5125, Identity = "vengefulspirit_nether_swap",  HeroId = 20, Ultimate = true , Notes = "" },
                new AbilityAttributes() { AbilityId = 5126, Identity = "crystal_maiden_crystal_nova",  HeroId = 5, Ultimate = false, Notes = "" },
                new AbilityAttributes() { AbilityId = 5127, Identity = "crystal_maiden_frostbite",  HeroId = 5, Ultimate = false, Notes = "" },
                new AbilityAttributes() { AbilityId = 5128, Identity = "crystal_maiden_brilliance_aura",  HeroId = 5, Ultimate = false, Notes = "" },
                new AbilityAttributes() { AbilityId = 5129, Identity = "crystal_maiden_freezing_field",  HeroId = 5, Ultimate = true , Notes = "" },
                new AbilityAttributes() { AbilityId = 5130, Identity = "windrunner_shackleshot",  HeroId = 21, Ultimate = false, Notes = "" },
                new AbilityAttributes() { AbilityId = 5131, Identity = "windrunner_powershot",  HeroId = 21, Ultimate = false, Notes = "" },
                new AbilityAttributes() { AbilityId = 5132, Identity = "windrunner_windrun",  HeroId = 21, Ultimate = false, Notes = "" },
                new AbilityAttributes() { AbilityId = 5133, Identity = "windrunner_focusfire",  HeroId = 21, Ultimate = true , Notes = "" },
                new AbilityAttributes() { AbilityId = 5134, Identity = "lich_frost_nova",  HeroId = 31, Ultimate = false, Notes = "" },
                new AbilityAttributes() { AbilityId = 5135, Identity = "lich_frost_armor",  HeroId = 31, Ultimate = false, Notes = "" },
                new AbilityAttributes() { AbilityId = 5136, Identity = "lich_dark_ritual",  HeroId = 31, Ultimate = false, Notes = "" },
                new AbilityAttributes() { AbilityId = 5137, Identity = "lich_chain_frost",  HeroId = 31, Ultimate = true , Notes = "" },
                new AbilityAttributes() { AbilityId = 5138, Identity = "witch_doctor_paralyzing_cask",  HeroId = 30, Ultimate = false, Notes = "" },
                new AbilityAttributes() { AbilityId = 5139, Identity = "witch_doctor_voodoo_restoration",  HeroId = 30, Ultimate = false, Notes = "" },
                new AbilityAttributes() { AbilityId = 5140, Identity = "witch_doctor_maledict",  HeroId = 30, Ultimate = false, Notes = "" },
                new AbilityAttributes() { AbilityId = 5141, Identity = "witch_doctor_death_ward",  HeroId = 30, Ultimate = true , Notes = "" },
                new AbilityAttributes() { AbilityId = 5142, Identity = "riki_smoke_screen",  HeroId = 32, Ultimate = false, Keywords = "Silence " , Notes = "" },
                new AbilityAttributes() { AbilityId = 5143, Identity = "riki_blink_strike",  HeroId = 32, Ultimate = false, Notes = "" },
                new AbilityAttributes() { AbilityId = 5144, Identity = "riki_permanent_invisibility",  HeroId = 32, Ultimate = false, Notes = "" },
                new AbilityAttributes() { AbilityId = 5145, Identity = "riki_tricks_of_the_trade",  HeroId = 32, Ultimate = true , Notes = "" },
                new AbilityAttributes() { AbilityId = 5146, Identity = "enigma_malefice",  HeroId = 33, Ultimate = false, Notes = "" },
                new AbilityAttributes() { AbilityId = 5147, Identity = "enigma_demonic_conversion",  HeroId = 33, Ultimate = false, Notes = "" },
                new AbilityAttributes() { AbilityId = 5148, Identity = "enigma_midnight_pulse",  HeroId = 33, Ultimate = false, Notes = "" },
                new AbilityAttributes() { AbilityId = 5149, Identity = "enigma_black_hole",  HeroId = 33, Ultimate = true , Notes = "" },
                new AbilityAttributes() { AbilityId = 5150, Identity = "tinker_laser",  HeroId = 34, Ultimate = false, Notes = "" },
                new AbilityAttributes() { AbilityId = 5151, Identity = "tinker_heat_seeking_missile",  HeroId = 34, Ultimate = false, Notes = "" },
                new AbilityAttributes() { AbilityId = 5152, Identity = "tinker_march_of_the_machines",  HeroId = 34, Ultimate = false, Notes = "" },
                new AbilityAttributes() { AbilityId = 5153, Identity = "tinker_rearm",  HeroId = 34, Ultimate = true , Notes = "" },
                new AbilityAttributes() { AbilityId = 5154, Identity = "sniper_shrapnel",  HeroId = 35, Ultimate = false, Notes = "" },
                new AbilityAttributes() { AbilityId = 5155, Identity = "sniper_headshot",  HeroId = 35, Ultimate = false, Notes = "" },
                new AbilityAttributes() { AbilityId = 5156, Identity = "sniper_take_aim",  HeroId = 35, Ultimate = false, Notes = "" },
                new AbilityAttributes() { AbilityId = 5157, Identity = "sniper_assassinate",  HeroId = 35, Ultimate = true , Notes = "" },
                new AbilityAttributes() { AbilityId = 5158, Identity = "necrolyte_death_pulse",  HeroId = 36, Ultimate = false, Notes = "" },
                new AbilityAttributes() { AbilityId = 5159, Identity = "necrolyte_heartstopper_aura",  HeroId = 36, Ultimate = false, Notes = "" },
                new AbilityAttributes() { AbilityId = 5160, Identity = "necrolyte_sadist",  HeroId = 36, Ultimate = false, Notes = "" },
                new AbilityAttributes() { AbilityId = 5161, Identity = "necrolyte_reapers_scythe",  HeroId = 36, Ultimate = true , Notes = "" },
                new AbilityAttributes() { AbilityId = 5162, Identity = "warlock_fatal_bonds",  HeroId = 37, Ultimate = false, Notes = "" },
                new AbilityAttributes() { AbilityId = 5163, Identity = "warlock_shadow_word",  HeroId = 37, Ultimate = false, Notes = "" },
                new AbilityAttributes() { AbilityId = 5164, Identity = "warlock_upheaval",  HeroId = 37, Ultimate = false, Notes = "" },
                new AbilityAttributes() { AbilityId = 5165, Identity = "warlock_rain_of_chaos",  HeroId = 37, Ultimate = true , Notes = "" },
                new AbilityAttributes() { AbilityId = 5173, Identity = "queenofpain_shadow_strike",  HeroId = 39, Ultimate = false, Notes = "" },
                new AbilityAttributes() { AbilityId = 5174, Identity = "queenofpain_blink",  HeroId = 39, Ultimate = false, Notes = "" },
                new AbilityAttributes() { AbilityId = 5175, Identity = "queenofpain_scream_of_pain",  HeroId = 39, Ultimate = false, Notes = "" },
                new AbilityAttributes() { AbilityId = 5176, Identity = "queenofpain_sonic_wave",  HeroId = 39, Ultimate = true , Notes = "" },
                new AbilityAttributes() { AbilityId = 5178, Identity = "venomancer_venomous_gale",  HeroId = 40, Ultimate = false, Notes = "" },
                new AbilityAttributes() { AbilityId = 5179, Identity = "venomancer_poison_sting",  HeroId = 40, Ultimate = false, Notes = "" },
                new AbilityAttributes() { AbilityId = 5180, Identity = "venomancer_plague_ward",  HeroId = 40, Ultimate = false, Notes = "" },
                new AbilityAttributes() { AbilityId = 5181, Identity = "venomancer_poison_nova",  HeroId = 40, Ultimate = true , Notes = "" },
                new AbilityAttributes() { AbilityId = 5182, Identity = "faceless_void_time_walk",  HeroId = 41, Ultimate = false, Notes = "" },
                new AbilityAttributes() { AbilityId = 5184, Identity = "faceless_void_time_lock",  HeroId = 41, Ultimate = false, Notes = "" },
                new AbilityAttributes() { AbilityId = 5185, Identity = "faceless_void_chronosphere",  HeroId = 41, Ultimate = true , Notes = "" },
                new AbilityAttributes() { AbilityId = 5186, Identity = "pugna_nether_blast",  HeroId = 45, Ultimate = false, Notes = "" },
                new AbilityAttributes() { AbilityId = 5187, Identity = "pugna_decrepify",  HeroId = 45, Ultimate = false, Notes = "" },
                new AbilityAttributes() { AbilityId = 5188, Identity = "pugna_nether_ward",  HeroId = 45, Ultimate = false, Notes = "" },
                new AbilityAttributes() { AbilityId = 5189, Identity = "pugna_life_drain",  HeroId = 45, Ultimate = true , Notes = "" },
                new AbilityAttributes() { AbilityId = 5190, Identity = "phantom_assassin_stifling_dagger",  HeroId = 44, Ultimate = false, Notes = "" },
                new AbilityAttributes() { AbilityId = 5191, Identity = "phantom_assassin_phantom_strike",  HeroId = 44, Ultimate = false, Notes = "" },
                new AbilityAttributes() { AbilityId = 5192, Identity = "phantom_assassin_blur",  HeroId = 44, Ultimate = false, Notes = "" },
                new AbilityAttributes() { AbilityId = 5193, Identity = "phantom_assassin_coup_de_grace",  HeroId = 44, Ultimate = true , Notes = "" },
                new AbilityAttributes() { AbilityId = 5218, Identity = "viper_poison_attack",  HeroId = 47, Ultimate = false, Notes = "" },
                new AbilityAttributes() { AbilityId = 5219, Identity = "viper_nethertoxin",  HeroId = 47, Ultimate = false, Notes = "" },
                new AbilityAttributes() { AbilityId = 5220, Identity = "viper_corrosive_skin",  HeroId = 47, Ultimate = false, Notes = "" },
                new AbilityAttributes() { AbilityId = 5221, Identity = "viper_viper_strike",  HeroId = 47, Ultimate = true , Notes = "" },
                new AbilityAttributes() { AbilityId = 5222, Identity = "luna_lucent_beam",  HeroId = 48, Ultimate = false, Notes = "" },
                new AbilityAttributes() { AbilityId = 5223, Identity = "luna_moon_glaive",  HeroId = 48, Ultimate = false, Notes = "" },
                new AbilityAttributes() { AbilityId = 5224, Identity = "luna_lunar_blessing",  HeroId = 48, Ultimate = false, Notes = "" },
                new AbilityAttributes() { AbilityId = 5225, Identity = "luna_eclipse",  HeroId = 48, Ultimate = true , Notes = "" },
                new AbilityAttributes() { AbilityId = 5226, Identity = "dragon_knight_breathe_fire",  HeroId = 49, Ultimate = false, Notes = "" },
                new AbilityAttributes() { AbilityId = 5227, Identity = "dragon_knight_dragon_tail",  HeroId = 49, Ultimate = false, Notes = "" },
                new AbilityAttributes() { AbilityId = 5228, Identity = "dragon_knight_dragon_blood",  HeroId = 49, Ultimate = false, Notes = "" },
                new AbilityAttributes() { AbilityId = 5229, Identity = "dragon_knight_elder_dragon_form",  HeroId = 49, Ultimate = true , Notes = "" },
                new AbilityAttributes() { AbilityId = 5233, Identity = "dazzle_poison_touch",  HeroId = 50, Ultimate = false, Notes = "" },
                new AbilityAttributes() { AbilityId = 5234, Identity = "dazzle_shallow_grave",  HeroId = 50, Ultimate = false, Notes = "" },
                new AbilityAttributes() { AbilityId = 5235, Identity = "dazzle_shadow_wave",  HeroId = 50, Ultimate = false, Notes = "" },
                new AbilityAttributes() { AbilityId = 5236, Identity = "dazzle_weave",  HeroId = 50, Ultimate = true , Notes = "" },
                new AbilityAttributes() { AbilityId = 5237, Identity = "rattletrap_battery_assault",  HeroId = 51, Ultimate = false, Notes = "" },
                new AbilityAttributes() { AbilityId = 5238, Identity = "rattletrap_power_cogs",  HeroId = 51, Ultimate = false, Notes = "" },
                new AbilityAttributes() { AbilityId = 5239, Identity = "rattletrap_rocket_flare",  HeroId = 51, Ultimate = false, Notes = "" },
                new AbilityAttributes() { AbilityId = 5240, Identity = "rattletrap_hookshot",  HeroId = 51, Ultimate = true , Notes = "" },
                new AbilityAttributes() { AbilityId = 5241, Identity = "leshrac_split_earth",  HeroId = 52, Ultimate = false, Notes = "" },
                new AbilityAttributes() { AbilityId = 5242, Identity = "leshrac_diabolic_edict",  HeroId = 52, Ultimate = false, Notes = "" },
                new AbilityAttributes() { AbilityId = 5243, Identity = "leshrac_lightning_storm",  HeroId = 52, Ultimate = false, Notes = "" },
                new AbilityAttributes() { AbilityId = 5244, Identity = "leshrac_pulse_nova",  HeroId = 52, Ultimate = true , Notes = "" },
                new AbilityAttributes() { AbilityId = 5245, Identity = "furion_sprout",  HeroId = 53, Ultimate = false, Notes = "" },
                new AbilityAttributes() { AbilityId = 5246, Identity = "furion_teleportation",  HeroId = 53, Ultimate = false, Notes = "" },
                new AbilityAttributes() { AbilityId = 5247, Identity = "furion_force_of_nature",  HeroId = 53, Ultimate = false, Notes = "" },
                new AbilityAttributes() { AbilityId = 5248, Identity = "furion_wrath_of_nature",  HeroId = 53, Ultimate = true , Notes = "" },
                new AbilityAttributes() { AbilityId = 5249, Identity = "life_stealer_rage",  HeroId = 54, Ultimate = false, Notes = "" },
                new AbilityAttributes() { AbilityId = 5250, Identity = "life_stealer_feast",  HeroId = 54, Ultimate = false, Notes = "" },
                new AbilityAttributes() { AbilityId = 5251, Identity = "life_stealer_open_wounds",  HeroId = 54, Ultimate = false, Notes = "" },
                new AbilityAttributes() { AbilityId = 5252, Identity = "life_stealer_infest",  HeroId = 54, Ultimate = true , Notes = "" },
                new AbilityAttributes() { AbilityId = 5255, Identity = "dark_seer_vacuum",  HeroId = 55, Ultimate = false, Notes = "" },
                new AbilityAttributes() { AbilityId = 5256, Identity = "dark_seer_ion_shell",  HeroId = 55, Ultimate = false, Notes = "" },
                new AbilityAttributes() { AbilityId = 5257, Identity = "dark_seer_surge",  HeroId = 55, Ultimate = false, Notes = "" },
                new AbilityAttributes() { AbilityId = 5258, Identity = "dark_seer_wall_of_replica",  HeroId = 55, Ultimate = true , Notes = "" },
                new AbilityAttributes() { AbilityId = 5259, Identity = "clinkz_strafe",  HeroId = 56, Ultimate = false, Notes = "" },
                new AbilityAttributes() { AbilityId = 5260, Identity = "clinkz_searing_arrows",  HeroId = 56, Ultimate = false, Notes = "" },
                new AbilityAttributes() { AbilityId = 5261, Identity = "clinkz_wind_walk",  HeroId = 56, Ultimate = false, Keywords = "Invisibility " , Notes = "" },
                new AbilityAttributes() { AbilityId = 5262, Identity = "clinkz_death_pact",  HeroId = 56, Ultimate = true , Notes = "" },
                new AbilityAttributes() { AbilityId = 5263, Identity = "omniknight_purification",  HeroId = 57, Ultimate = false, Notes = "" },
                new AbilityAttributes() { AbilityId = 5264, Identity = "omniknight_repel",  HeroId = 57, Ultimate = false, Notes = "" },
                new AbilityAttributes() { AbilityId = 5265, Identity = "omniknight_degen_aura",  HeroId = 57, Ultimate = false, Notes = "" },
                new AbilityAttributes() { AbilityId = 5266, Identity = "omniknight_guardian_angel",  HeroId = 57, Ultimate = true , Notes = "" },
                new AbilityAttributes() { AbilityId = 5267, Identity = "enchantress_untouchable",  HeroId = 58, Ultimate = false, Notes = "" },
                new AbilityAttributes() { AbilityId = 5268, Identity = "enchantress_enchant",  HeroId = 58, Ultimate = false, Notes = "" },
                new AbilityAttributes() { AbilityId = 5269, Identity = "enchantress_natures_attendants",  HeroId = 58, Ultimate = false, Notes = "" },
                new AbilityAttributes() { AbilityId = 5270, Identity = "enchantress_impetus",  HeroId = 58, Ultimate = true , Notes = "" },
                new AbilityAttributes() { AbilityId = 5271, Identity = "huskar_inner_vitality",  HeroId = 59, Ultimate = false, Notes = "" },
                new AbilityAttributes() { AbilityId = 5272, Identity = "huskar_burning_spear",  HeroId = 59, Ultimate = false, Notes = "" },
                new AbilityAttributes() { AbilityId = 5273, Identity = "huskar_berserkers_blood",  HeroId = 59, Ultimate = false, Notes = "" },
                new AbilityAttributes() { AbilityId = 5274, Identity = "huskar_life_break",  HeroId = 59, Ultimate = true , Notes = "" },
                new AbilityAttributes() { AbilityId = 5275, Identity = "night_stalker_void",  HeroId = 60, Ultimate = false, Notes = "" },
                new AbilityAttributes() { AbilityId = 5276, Identity = "night_stalker_crippling_fear",  HeroId = 60, Ultimate = false, Notes = "" },
                new AbilityAttributes() { AbilityId = 5277, Identity = "night_stalker_hunter_in_the_night",  HeroId = 60, Ultimate = false, Notes = "" },
                new AbilityAttributes() { AbilityId = 5278, Identity = "night_stalker_darkness",  HeroId = 60, Ultimate = true , Notes = "" },
                new AbilityAttributes() { AbilityId = 5279, Identity = "broodmother_spawn_spiderlings",  HeroId = 61, Ultimate = false, Notes = "" },
                new AbilityAttributes() { AbilityId = 5280, Identity = "broodmother_spin_web",  HeroId = 61, Ultimate = false, Keywords = "Invisibility ", Notes = "" },
                new AbilityAttributes() { AbilityId = 5281, Identity = "broodmother_incapacitating_bite",  HeroId = 61, Ultimate = false, Notes = "" },
                new AbilityAttributes() { AbilityId = 5282, Identity = "broodmother_insatiable_hunger",  HeroId = 61, Ultimate = true , Notes = "" },
                new AbilityAttributes() { AbilityId = 5285, Identity = "bounty_hunter_shuriken_toss",  HeroId = 62, Ultimate = false, Notes = "" },
                new AbilityAttributes() { AbilityId = 5286, Identity = "bounty_hunter_jinada",  HeroId = 62, Ultimate = false, Notes = "" },
                new AbilityAttributes() { AbilityId = 5287, Identity = "bounty_hunter_wind_walk",  HeroId = 62, Ultimate = false, Notes = "" },
                new AbilityAttributes() { AbilityId = 5288, Identity = "bounty_hunter_track",  HeroId = 62, Ultimate = true , Notes = "" },
                new AbilityAttributes() { AbilityId = 5289, Identity = "weaver_the_swarm",  HeroId = 63, Ultimate = false, Notes = "" },
                new AbilityAttributes() { AbilityId = 5290, Identity = "weaver_shukuchi",  HeroId = 63, Ultimate = false, Notes = "" },
                new AbilityAttributes() { AbilityId = 5291, Identity = "weaver_geminate_attack",  HeroId = 63, Ultimate = false, Notes = "" },
                new AbilityAttributes() { AbilityId = 5292, Identity = "weaver_time_lapse",  HeroId = 63, Ultimate = true , Notes = "" },
                new AbilityAttributes() { AbilityId = 5297, Identity = "jakiro_dual_breath",  HeroId = 64, Ultimate = false, Notes = "" },
                new AbilityAttributes() { AbilityId = 5298, Identity = "jakiro_ice_path",  HeroId = 64, Ultimate = false, Notes = "" },
                new AbilityAttributes() { AbilityId = 5299, Identity = "jakiro_liquid_fire",  HeroId = 64, Ultimate = false, Notes = "" },
                new AbilityAttributes() { AbilityId = 5300, Identity = "jakiro_macropyre",  HeroId = 64, Ultimate = true , Notes = "" },
                new AbilityAttributes() { AbilityId = 5320, Identity = "batrider_sticky_napalm",  HeroId = 65, Ultimate = false, Notes = "" },
                new AbilityAttributes() { AbilityId = 5321, Identity = "batrider_flamebreak",  HeroId = 65, Ultimate = false, Notes = "" },
                new AbilityAttributes() { AbilityId = 5322, Identity = "batrider_firefly",  HeroId = 65, Ultimate = false, Notes = "" },
                new AbilityAttributes() { AbilityId = 5323, Identity = "batrider_flaming_lasso",  HeroId = 65, Ultimate = true , Notes = "" },
                new AbilityAttributes() { AbilityId = 5345, Identity = "ancient_apparition_cold_feet",  HeroId = 68, Ultimate = false, Notes = "" },
                new AbilityAttributes() { AbilityId = 5346, Identity = "ancient_apparition_ice_vortex",  HeroId = 68, Ultimate = false, Notes = "" },
                new AbilityAttributes() { AbilityId = 5347, Identity = "ancient_apparition_chilling_touch",  HeroId = 68, Ultimate = false, Notes = "" },
                new AbilityAttributes() { AbilityId = 5348, Identity = "ancient_apparition_ice_blast",  HeroId = 68, Ultimate = true , Notes = "" },
                new AbilityAttributes() { AbilityId = 5353, Identity = "spirit_breaker_charge_of_darkness",  HeroId = 71, Ultimate = false, Notes = "" },
                new AbilityAttributes() { AbilityId = 5354, Identity = "spirit_breaker_empowering_haste",  HeroId = 71, Ultimate = false, Notes = "" },
                new AbilityAttributes() { AbilityId = 5355, Identity = "spirit_breaker_greater_bash",  HeroId = 71, Ultimate = false, Notes = "" },
                new AbilityAttributes() { AbilityId = 5356, Identity = "spirit_breaker_nether_strike",  HeroId = 71, Ultimate = true , Notes = "" },
                new AbilityAttributes() { AbilityId = 5357, Identity = "ursa_earthshock",  HeroId = 70, Ultimate = false, Notes = "" },
                new AbilityAttributes() { AbilityId = 5358, Identity = "ursa_overpower",  HeroId = 70, Ultimate = false, Notes = "" },
                new AbilityAttributes() { AbilityId = 5359, Identity = "ursa_fury_swipes",  HeroId = 70, Ultimate = false, Notes = "" },
                new AbilityAttributes() { AbilityId = 5360, Identity = "ursa_enrage",  HeroId = 70, Ultimate = true , Notes = "" },
                new AbilityAttributes() { AbilityId = 5361, Identity = "gyrocopter_rocket_barrage",  HeroId = 72, Ultimate = false, Notes = "" },
                new AbilityAttributes() { AbilityId = 5362, Identity = "gyrocopter_homing_missile",  HeroId = 72, Ultimate = false, Notes = "" },
                new AbilityAttributes() { AbilityId = 5363, Identity = "gyrocopter_flak_cannon",  HeroId = 72, Ultimate = false, Notes = "" },
                new AbilityAttributes() { AbilityId = 5364, Identity = "gyrocopter_call_down",  HeroId = 72, Ultimate = true , Notes = "" },
                new AbilityAttributes() { AbilityId = 5365, Identity = "alchemist_acid_spray",  HeroId = 73, Ultimate = false, Notes = "" },
                new AbilityAttributes() { AbilityId = 5366, Identity = "alchemist_unstable_concoction",  HeroId = 73, Ultimate = false, Notes = "" },
                new AbilityAttributes() { AbilityId = 5368, Identity = "alchemist_goblins_greed",  HeroId = 73, Ultimate = false, Notes = "" },
                new AbilityAttributes() { AbilityId = 5369, Identity = "alchemist_chemical_rage",  HeroId = 73, Ultimate = true , Notes = "" },
                new AbilityAttributes() { AbilityId = 5377, Identity = "silencer_curse_of_the_silent",  HeroId = 75, Ultimate = false, Notes = "" },
                new AbilityAttributes() { AbilityId = 5378, Identity = "silencer_glaives_of_wisdom",  HeroId = 75, Ultimate = false, Notes = "" },
                new AbilityAttributes() { AbilityId = 5379, Identity = "silencer_last_word",  HeroId = 75, Ultimate = false, Notes = "" },
                new AbilityAttributes() { AbilityId = 5380, Identity = "silencer_global_silence",  HeroId = 75, Ultimate = true , Notes = "" },
                new AbilityAttributes() { AbilityId = 5391, Identity = "obsidian_destroyer_arcane_orb",  HeroId = 76, Ultimate = false, Notes = "" },
                new AbilityAttributes() { AbilityId = 5392, Identity = "obsidian_destroyer_astral_imprisonment",  HeroId = 76, Ultimate = false, Notes = "" },
                new AbilityAttributes() { AbilityId = 5393, Identity = "obsidian_destroyer_essence_aura",  HeroId = 76, Ultimate = false, Notes = "" },
                new AbilityAttributes() { AbilityId = 5394, Identity = "obsidian_destroyer_sanity_eclipse",  HeroId = 76, Ultimate = true , Notes = "" },
                new AbilityAttributes() { AbilityId = 5395, Identity = "lycan_summon_wolves",  HeroId = 77, Ultimate = false, Notes = "" },
                new AbilityAttributes() { AbilityId = 5396, Identity = "lycan_howl",  HeroId = 77, Ultimate = false, Notes = "" },
                new AbilityAttributes() { AbilityId = 5397, Identity = "lycan_feral_impulse",  HeroId = 77, Ultimate = false, Notes = "" },
                new AbilityAttributes() { AbilityId = 5398, Identity = "lycan_shapeshift",  HeroId = 77, Ultimate = true , Notes = "" },
                new AbilityAttributes() { AbilityId = 5400, Identity = "brewmaster_thunder_clap",  HeroId = 78, Ultimate = false, Notes = "" },
                new AbilityAttributes() { AbilityId = 5401, Identity = "brewmaster_drunken_haze",  HeroId = 78, Ultimate = false, Notes = "" },
                new AbilityAttributes() { AbilityId = 5402, Identity = "brewmaster_drunken_brawler",  HeroId = 78, Ultimate = false, Notes = "" },
                new AbilityAttributes() { AbilityId = 5403, Identity = "brewmaster_primal_split",  HeroId = 78, Ultimate = true , Notes = "" },
                new AbilityAttributes() { AbilityId = 5426, Identity = "chaos_knight_chaos_bolt",  HeroId = 81, Ultimate = false, Notes = "" },
                new AbilityAttributes() { AbilityId = 5427, Identity = "chaos_knight_reality_rift",  HeroId = 81, Ultimate = false, Notes = "" },
                new AbilityAttributes() { AbilityId = 5428, Identity = "chaos_knight_chaos_strike",  HeroId = 81, Ultimate = false, Notes = "" },
                new AbilityAttributes() { AbilityId = 5429, Identity = "chaos_knight_phantasm",  HeroId = 81, Ultimate = true , Notes = "" },
                new AbilityAttributes() { AbilityId = 5434, Identity = "treant_natures_guise",  HeroId = 83, Ultimate = false, Keywords = "Invisibility " , Notes = "" },
                new AbilityAttributes() { AbilityId = 5435, Identity = "treant_leech_seed",  HeroId = 83, Ultimate = false, Notes = "" },
                new AbilityAttributes() { AbilityId = 5436, Identity = "treant_living_armor",  HeroId = 83, Ultimate = false, Notes = "" },
                new AbilityAttributes() { AbilityId = 5437, Identity = "treant_overgrowth",  HeroId = 83, Ultimate = true , Notes = "" },
                new AbilityAttributes() { AbilityId = 5442, Identity = "undying_decay",  HeroId = 85, Ultimate = false, Notes = "" },
                new AbilityAttributes() { AbilityId = 5443, Identity = "undying_soul_rip",  HeroId = 85, Ultimate = false, Notes = "" },
                new AbilityAttributes() { AbilityId = 5444, Identity = "undying_tombstone",  HeroId = 85, Ultimate = false, Notes = "" },
                new AbilityAttributes() { AbilityId = 5447, Identity = "undying_flesh_golem",  HeroId = 85, Ultimate = true , Notes = "" },
                new AbilityAttributes() { AbilityId = 5458, Identity = "disruptor_thunder_strike",  HeroId = 87, Ultimate = false, Notes = "" },
                new AbilityAttributes() { AbilityId = 5459, Identity = "disruptor_glimpse",  HeroId = 87, Ultimate = false, Notes = "" },
                new AbilityAttributes() { AbilityId = 5460, Identity = "disruptor_kinetic_field",  HeroId = 87, Ultimate = false, Notes = "" },
                new AbilityAttributes() { AbilityId = 5461, Identity = "disruptor_static_storm",  HeroId = 87, Ultimate = true , Notes = "" },
                new AbilityAttributes() { AbilityId = 5462, Identity = "nyx_assassin_impale",  HeroId = 88, Ultimate = false, Notes = "" },
                new AbilityAttributes() { AbilityId = 5463, Identity = "nyx_assassin_mana_burn",  HeroId = 88, Ultimate = false, Notes = "" },
                new AbilityAttributes() { AbilityId = 5464, Identity = "nyx_assassin_spiked_carapace",  HeroId = 88, Ultimate = false, Notes = "" },
                new AbilityAttributes() { AbilityId = 5465, Identity = "nyx_assassin_vendetta",  HeroId = 88, Ultimate = true , Notes = "" },
                new AbilityAttributes() { AbilityId = 5467, Identity = "naga_siren_mirror_image",  HeroId = 89, Ultimate = false, Notes = "" },
                new AbilityAttributes() { AbilityId = 5468, Identity = "naga_siren_ensnare",  HeroId = 89, Ultimate = false, Notes = "" },
                new AbilityAttributes() { AbilityId = 5469, Identity = "naga_siren_rip_tide",  HeroId = 89, Ultimate = false, Notes = "" },
                new AbilityAttributes() { AbilityId = 5470, Identity = "naga_siren_song_of_the_siren",  HeroId = 89, Ultimate = true, Keywords = "Stun "  , Notes = "" },
                new AbilityAttributes() { AbilityId = 5480, Identity = "visage_grave_chill",  HeroId = 92, Ultimate = false, Notes = "" },
                new AbilityAttributes() { AbilityId = 5481, Identity = "visage_soul_assumption",  HeroId = 92, Ultimate = false, Notes = "" },
                new AbilityAttributes() { AbilityId = 5482, Identity = "visage_gravekeepers_cloak",  HeroId = 92, Ultimate = false, Notes = "" },
                new AbilityAttributes() { AbilityId = 5483, Identity = "visage_summon_familiars",  HeroId = 92, Ultimate = true , Notes = "" },
                new AbilityAttributes() { AbilityId = 5494, Identity = "slark_dark_pact",  HeroId = 93, Ultimate = false, Notes = "" },
                new AbilityAttributes() { AbilityId = 5495, Identity = "slark_pounce",  HeroId = 93, Ultimate = false, Notes = "" },
                new AbilityAttributes() { AbilityId = 5496, Identity = "slark_essence_shift",  HeroId = 93, Ultimate = false, Notes = "" },
                new AbilityAttributes() { AbilityId = 5497, Identity = "slark_shadow_dance",  HeroId = 93, Ultimate = true , Notes = "" },
                new AbilityAttributes() { AbilityId = 5504, Identity = "medusa_split_shot",  HeroId = 94, Ultimate = false, Notes = "" },
                new AbilityAttributes() { AbilityId = 5505, Identity = "medusa_mystic_snake",  HeroId = 94, Ultimate = false, Notes = "" },
                new AbilityAttributes() { AbilityId = 5506, Identity = "medusa_mana_shield",  HeroId = 94, Ultimate = false, Notes = "" },
                new AbilityAttributes() { AbilityId = 5507, Identity = "medusa_stone_gaze",  HeroId = 94, Ultimate = true , Notes = "" },
                new AbilityAttributes() { AbilityId = 5514, Identity = "centaur_hoof_stomp",  HeroId = 96, Ultimate = false, Notes = "" },
                new AbilityAttributes() { AbilityId = 5515, Identity = "centaur_double_edge",  HeroId = 96, Ultimate = false, Notes = "" },
                new AbilityAttributes() { AbilityId = 5516, Identity = "centaur_return",  HeroId = 96, Ultimate = false, Notes = "" },
                new AbilityAttributes() { AbilityId = 5517, Identity = "centaur_stampede",  HeroId = 96, Ultimate = true , Notes = "" },
                new AbilityAttributes() { AbilityId = 5518, Identity = "magnataur_shockwave",  HeroId = 97, Ultimate = false, Notes = "" },
                new AbilityAttributes() { AbilityId = 5519, Identity = "magnataur_empower",  HeroId = 97, Ultimate = false, Notes = "" },
                new AbilityAttributes() { AbilityId = 5520, Identity = "magnataur_skewer",  HeroId = 97, Ultimate = false, Notes = "" },
                new AbilityAttributes() { AbilityId = 5521, Identity = "magnataur_reverse_polarity",  HeroId = 97, Ultimate = true , Notes = "" },
                new AbilityAttributes() { AbilityId = 5548, Identity = "bristleback_viscous_nasal_goo",  HeroId = 99, Ultimate = false, Notes = "" },
                new AbilityAttributes() { AbilityId = 5549, Identity = "bristleback_quill_spray",  HeroId = 99, Ultimate = false, Notes = "" },
                new AbilityAttributes() { AbilityId = 5550, Identity = "bristleback_bristleback",  HeroId = 99, Ultimate = false, Notes = "" },
                new AbilityAttributes() { AbilityId = 5551, Identity = "bristleback_warpath",  HeroId = 99, Ultimate = true , Notes = "" },
                new AbilityAttributes() { AbilityId = 5581, Identity = "skywrath_mage_arcane_bolt",  HeroId = 101, Ultimate = false, Notes = "" },
                new AbilityAttributes() { AbilityId = 5582, Identity = "skywrath_mage_concussive_shot",  HeroId = 101, Ultimate = false, Notes = "" },
                new AbilityAttributes() { AbilityId = 5583, Identity = "skywrath_mage_ancient_seal",  HeroId = 101, Ultimate = false, Notes = "" },
                new AbilityAttributes() { AbilityId = 5584, Identity = "skywrath_mage_mystic_flare",  HeroId = 101, Ultimate = true , Notes = "" },
                new AbilityAttributes() { AbilityId = 5585, Identity = "abaddon_death_coil",  HeroId = 102, Ultimate = false, Notes = "" },
                new AbilityAttributes() { AbilityId = 5586, Identity = "abaddon_aphotic_shield",  HeroId = 102, Ultimate = false, Notes = "" },
                new AbilityAttributes() { AbilityId = 5587, Identity = "abaddon_frostmourne",  HeroId = 102, Ultimate = false, Notes = "" },
                new AbilityAttributes() { AbilityId = 5588, Identity = "abaddon_borrowed_time",  HeroId = 102, Ultimate = true , Notes = "" },
                new AbilityAttributes() { AbilityId = 5589, Identity = "elder_titan_echo_stomp",  HeroId = 103, Ultimate = false, Notes = "" },
                new AbilityAttributes() { AbilityId = 5591, Identity = "elder_titan_ancestral_spirit",  HeroId = 103, Ultimate = false, Notes = "" },
                new AbilityAttributes() { AbilityId = 5593, Identity = "elder_titan_natural_order",  HeroId = 103, Ultimate = false, Notes = "" },
                new AbilityAttributes() { AbilityId = 5594, Identity = "elder_titan_earth_splitter",  HeroId = 103, Ultimate = true , Notes = "" },
                new AbilityAttributes() { AbilityId = 5595, Identity = "legion_commander_overwhelming_odds",  HeroId = 104, Ultimate = false, Notes = "" },
                new AbilityAttributes() { AbilityId = 5596, Identity = "legion_commander_press_the_attack",  HeroId = 104, Ultimate = false, Notes = "" },
                new AbilityAttributes() { AbilityId = 5597, Identity = "legion_commander_moment_of_courage",  HeroId = 104, Ultimate = false, Notes = "" },
                new AbilityAttributes() { AbilityId = 5598, Identity = "legion_commander_duel",  HeroId = 104, Ultimate = true , Notes = "" },
                new AbilityAttributes() { AbilityId = 5619, Identity = "terrorblade_reflection",  HeroId = 109, Ultimate = false, Notes = "" },
                new AbilityAttributes() { AbilityId = 5620, Identity = "terrorblade_conjure_image",  HeroId = 109, Ultimate = false, Notes = "" },
                new AbilityAttributes() { AbilityId = 5621, Identity = "terrorblade_metamorphosis",  HeroId = 109, Ultimate = false, Notes = "" },
                new AbilityAttributes() { AbilityId = 5622, Identity = "terrorblade_sunder",  HeroId = 109, Ultimate = true , Notes = "" },
                new AbilityAttributes() { AbilityId = 5632, Identity = "drow_ranger_wave_of_silence",HeroId = 6, Ultimate = false, Notes = "" },
                new AbilityAttributes() { AbilityId = 5637, Identity = "oracle_fortunes_end",  HeroId = 111, Ultimate = false, Notes = "" },
                new AbilityAttributes() { AbilityId = 5638, Identity = "oracle_fates_edict",  HeroId = 111, Ultimate = false, Notes = "" },
                new AbilityAttributes() { AbilityId = 5639, Identity = "oracle_purifying_flames",  HeroId = 111, Ultimate = false, Notes = "" },
                new AbilityAttributes() { AbilityId = 5640, Identity = "oracle_false_promise",  HeroId = 111, Ultimate = true , Notes = "" },
                new AbilityAttributes() { AbilityId = 5651, Identity = "winter_wyvern_arctic_burn",  HeroId = 112, Ultimate = false, Notes = "" },
                new AbilityAttributes() { AbilityId = 5652, Identity = "winter_wyvern_splinter_blast",  HeroId = 112, Ultimate = false, Notes = "" },
                new AbilityAttributes() { AbilityId = 5653, Identity = "winter_wyvern_cold_embrace",  HeroId = 112, Ultimate = false, Notes = "" },
                new AbilityAttributes() { AbilityId = 5654, Identity = "winter_wyvern_winters_curse",  HeroId = 112, Ultimate = true , Notes = "" },
                new AbilityAttributes() { AbilityId = 5677, Identity = "arc_warden_flux",  HeroId = 113, Ultimate = false, Notes = "" },
                new AbilityAttributes() { AbilityId = 5678, Identity = "arc_warden_magnetic_field",  HeroId = 113, Ultimate = false, Notes = "" },
                new AbilityAttributes() { AbilityId = 5679, Identity = "arc_warden_spark_wraith",  HeroId = 113, Ultimate = false, Notes = "" },
                new AbilityAttributes() { AbilityId = 5683, Identity = "arc_warden_tempest_double",  HeroId = 113, Ultimate = true , Notes = "" },
                new AbilityAttributes() { AbilityId = 5685, Identity = "death_prophet_spirit_siphon",  HeroId = 43, Ultimate = false, Notes = "" },
                new AbilityAttributes() { AbilityId = 5691, Identity = "faceless_void_time_dilation",  HeroId = 41, Ultimate = false, Notes = "" },
                new AbilityAttributes() { AbilityId = 5613, Identity = "abyssal_underlord_firestorm",  HeroId = 108, Ultimate = false, Notes = "" },
                new AbilityAttributes() { AbilityId = 5614, Identity = "abyssal_underlord_pit_of_malice",  HeroId = 108, Ultimate = false, Notes = "" },
                new AbilityAttributes() { AbilityId = 5615, Identity = "abyssal_underlord_atrophy_aura",  HeroId = 108, Ultimate = false, Notes = "" },
                new AbilityAttributes() { AbilityId = 5616, Identity = "abyssal_underlord_dark_rift",  HeroId = 108, Ultimate = true, Notes = "" },
            };

            // Remove all others
            _dbContext.Abilities.RemoveRange(_dbContext.Abilities);
            _dbContext.SaveChanges();

            // Add new
            _dbContext.Abilities.AddRange(abilities);
            _dbContext.SaveChanges();

            var httpClient = new HttpClient();
            var jsonData = httpClient.GetStringAsync("http://www.dota2.com/jsfeed/heropediadata?feeds=abilitydata&l=en_us").Result;
            var root = JObject.Parse(jsonData);

            var regexAbility = new Regex(@"ABILITY: <span class=""attribVal"">(?<value>.*?)</span>", RegexOptions.None);
            var regexDamageType = new Regex(@"DAMAGE TYPE: <span class=""attribVal"">(?<value>.*?)</span>", RegexOptions.None);
            var regexAffects = new Regex(@"AFFECTS: <span class=""attribVal"">(?<value>.*?)</span>", RegexOptions.None);


            foreach (var data in root)
            {
                JObject collection = data.Value as JObject;

                foreach (var item in collection)
                {
                    var identity = item.Key;
                    var ability = _dbContext.Abilities.SingleOrDefault(_ => _.Identity == identity);
                    if (ability == null)
                    {
                        _logger.LogWarning(string.Format("No ability found for identity: {0}", identity));
                        continue;
                    }

                    var name = item.Value["dname"].Value<string>();
                    var affects = item.Value["affects"].Value<string>();
                    var description = item.Value["desc"].Value<string>();
                    var notes = item.Value["notes"].Value<string>();
                    var damage = item.Value["dmg"].Value<string>();
                    var attributes = item.Value["attrib"].Value<string>();
                    var commands = item.Value["cmb"].Value<string>();
                    var lore = item.Value["lore"].Value<string>();

                    ability.Name = name;
                    ability.Description = description;

                    // Target
                    var matchAbility = regexAbility.Match(affects);
                    if (matchAbility.Success)
                        ability.Target = matchAbility.Groups["value"].Value.Trim();

                    // Affects Whom
                    var matchAffects = regexAffects.Match(affects);
                    if (matchAffects.Success)
                        ability.AffectsWhom = matchAffects.Groups["value"].Value.Trim();

                    // DamageType
                    var matchDamageType = regexDamageType.Match(affects);
                    if (matchDamageType.Success)
                        ability.DamageType = matchDamageType.Groups["value"].Value.Trim()
                            .Replace("<font color=\"#d61107\">Pure</font>", "Pure")
                            .Replace("<font color=\"#71a9ea\">Physical</font>", "Physical");

                    // Formated Abiltiy Details Card
                    var template = "<div class=\"iconTooltip\"><div class=\"abilityTarget\"><div>{0}</div><hr /><div class=\"abilityDesc\">{1}</div><div class=\"abilityNotes\">{2}</div><div class=\"abilityDmg\">{3}</div><div>{4}</div><div class=\"abilityCMB\">{5}</div><div class=\"abilityLore\">{6}</div></div></div>";
                    ability.Html = string.Format(template, affects, description, notes, damage, attributes, commands, lore);

                    if (string.IsNullOrWhiteSpace(ability.DamageType) == false)
                        ability.Keywords += "Damage ";

                    var searchDescription = new Dictionary<string, string>()
                    {
                        { "Upgradable by Aghanim's Scepter", "Upgradable " },
                        { "Unique Attack Modifier", "UAM " },
                        { "lifesteal", "Lifesteal " },
                        { "silence", "Silence " },
                        { "silenced", "Silence " },
                        { "silencing", "Silence " },
                        { "stun", "Stun " },
                    };

                    foreach (var pair in searchDescription)
                    {
                        if (description.Contains(pair.Key))
                            ability.Keywords += pair.Value;
                    }

                    var searchAttributes = new Dictionary<string, string>()
                    {
                        { "SLOW", "Slow " },
                        { "SPEED", "Speed " },
                        { "HEAL", "Heal " },
                        { "INVISIBILITY ", "Invisibility " },
                        { "RESISTANCE", "Resistance " },
                        { "ARMOR", "Armor " }
                    };

                    foreach (var pair in searchAttributes)
                    {
                        if (attributes.Contains(pair.Key))
                            ability.Keywords += pair.Value;
                    }
                }
            }

            _dbContext.SaveChanges();
        }

        public void SeedTalenets()
        {
            var httpClient = new HttpClient();
            var jsonData = httpClient.GetStringAsync("http://hgv-raindrop.azurewebsites.net/api/heroes").Result;
            var heroes = JsonConvert.DeserializeObject<List<Hero>>(jsonData);

            foreach (var hero in heroes)
            {
                var existing = _dbContext.Talenets.Where(_ => _.HeroId == hero.hero_id);
                _dbContext.Talenets.RemoveRange(existing);
            }
            _dbContext.SaveChanges();

            foreach (var hero in heroes)
            {
                foreach (var item in hero.talenets)
                {
                    var ability = new TalenetAttributes();
                    ability.AbilityId = item.ability_id;
                    ability.HeroId = item.hero_id;
                    ability.Key = item.key;
                    ability.Level = item.level;
                    ability.Name = item.name;
                    _dbContext.Talenets.Add(ability);
                }
            }

            _dbContext.SaveChanges();
        }


        //~ time 5m
        public void SeedTrends()
        {
            // Checkpoint
            _dbContext.Checkpoints.RemoveRange(_dbContext.Checkpoints);
            _dbContext.SaveChanges();

            _dbContext.Checkpoints.Add(new Models.Statistics.Checkpoint() { MatchId = 0, MatchNumber = 0, MatchDate = DateTime.MinValue });
            _dbContext.SaveChanges();

            // Remove old trends
            _dbContext.Database.ExecuteSqlCommand("TRUNCATE TABLE NextAbilityTrends;TRUNCATE TABLE PerviousAbilityTrends;TRUNCATE TABLE CurrentAbilityTrends;");
            _dbContext.Database.ExecuteSqlCommand("TRUNCATE TABLE NextAbilityComboTrends;TRUNCATE TABLE PerviousAbilityComboTrends;TRUNCATE TABLE CurrentAbilityComboTrends;");
            _dbContext.Database.ExecuteSqlCommand("TRUNCATE TABLE NextHeroTrends;TRUNCATE TABLE PerviousHeroTrends;TRUNCATE TABLE CurrentHeroTrends;");
            _dbContext.Database.ExecuteSqlCommand("TRUNCATE TABLE NextHeroComboTrends;TRUNCATE TABLE PerviousHeroComboTrends;TRUNCATE TABLE CurrentHeroComboTrends;");

            // Add Trends
            var abilities = _dbContext.Abilities.ToList();
            var heroes = _dbContext.Heroes.ToList();

            // Add Abilities
            foreach (var item in abilities)
            {
                _dbContext.NextAbilityTrends.Add(new Models.Trends.NextAbilityStat()
                {
                    AbilityId = item.AbilityId,
                    Identity = item.Identity,
                    Name = item.Name,
                });
                _dbContext.PerviousAbilityTrends.Add(new Models.Trends.PerviousAbilityStat()
                {
                    AbilityId = item.AbilityId,
                    Identity = item.Identity,
                    Name = item.Name,
                });
                _dbContext.CurrentAbilityTrends.Add(new Models.Trends.CurrentAbilityStat()
                {
                    AbilityId = item.AbilityId,
                    Identity = item.Identity,
                    Name = item.Name,
                });
            }

            _dbContext.SaveChanges();

            // Add Ability Combos
            var abilityCombos = abilities
                .SelectMany(x => abilities, (x, y) => Tuple.Create(x, y))
                .Where(_ => _.Item1.AbilityId != _.Item2.AbilityId)
                .ToList();

            foreach (var combo in abilityCombos)
            {
                _dbContext.NextAbilityComboTrends.Add(new Models.Trends.NextAbilityComboStat()
                {
                    AbilityId = combo.Item1.AbilityId,
                    AbilityIdentity = combo.Item1.Identity,
                    AbilityName = combo.Item1.Name,
                    ComboId = combo.Item2.AbilityId,
                    ComboIdentity = combo.Item2.Identity,
                    ComboName = combo.Item2.Name,
                    SameSource = combo.Item1.HeroId == combo.Item2.HeroId,
                });
                _dbContext.CurrentAbilityComboTrends.Add(new Models.Trends.CurrentAbilityComboStat()
                {
                    AbilityId = combo.Item1.AbilityId,
                    AbilityIdentity = combo.Item1.Identity,
                    AbilityName = combo.Item1.Name,
                    ComboId = combo.Item2.AbilityId,
                    ComboIdentity = combo.Item2.Identity,
                    ComboName = combo.Item2.Name,
                    SameSource = combo.Item1.HeroId == combo.Item2.HeroId,
                });
                _dbContext.PerviousAbilityComboTrends.Add(new Models.Trends.PerviousAbilityComboStat()
                {
                    AbilityId = combo.Item1.AbilityId,
                    AbilityIdentity = combo.Item1.Identity,
                    AbilityName = combo.Item1.Name,
                    ComboId = combo.Item2.AbilityId,
                    ComboIdentity = combo.Item2.Identity,
                    ComboName = combo.Item2.Name,
                    SameSource = combo.Item1.HeroId == combo.Item2.HeroId,
                });
            }

            _dbContext.SaveChanges();

            // Add Heroes
            foreach (var item in heroes)
            {
                _dbContext.NextHeroTrends.Add(new Models.Trends.NextHeroStat()
                {
                    HeroId = item.HeroId,
                    Identity = item.Identity,
                    Name = item.Name,
                });
                _dbContext.CurrentHeroTrends.Add(new Models.Trends.CurrentHeroStat()
                {
                    HeroId = item.HeroId,
                    Identity = item.Identity,
                    Name = item.Name,
                });
                _dbContext.PerviousHeroTrends.Add(new Models.Trends.PerviousHeroStat()
                {
                    HeroId = item.HeroId,
                    Identity = item.Identity,
                    Name = item.Name,
                });
            }

            _dbContext.SaveChanges();

            // Add Heroes Combos
            foreach (var hero in heroes)
            {
                foreach (var ability in abilities)
                {
                    _dbContext.NextHeroComboTrends.Add(new Models.Trends.NextHeroComboStats()
                    {
                        HeroId = hero.HeroId,
                        HeroIdentity = hero.Identity,
                        HeroName = hero.Name,
                        AbilityId = ability.AbilityId,
                        AbilityIdentity = ability.Identity,
                        AbilityName = ability.Name,
                        SameSource = hero.HeroId  == ability.HeroId,
                    });
                    _dbContext.CurrentHeroComboTrends.Add(new Models.Trends.CurrentHeroComboStats()
                    {
                        HeroId = hero.HeroId,
                        HeroIdentity = hero.Identity,
                        HeroName = hero.Name,
                        AbilityId = ability.AbilityId,
                        AbilityIdentity = ability.Identity,
                        AbilityName = ability.Name,
                        SameSource = hero.HeroId == ability.HeroId,
                    });
                    _dbContext.PerviousHeroComboTrends.Add(new Models.Trends.PerviousHeroComboStats()
                    {
                        HeroId = hero.HeroId,
                        HeroIdentity = hero.Identity,
                        HeroName = hero.Name,
                        AbilityId = ability.AbilityId,
                        AbilityIdentity = ability.Identity,
                        AbilityName = ability.Name,
                        SameSource = hero.HeroId == ability.HeroId,
                    });
                }
            }

            _dbContext.SaveChanges();
        }
    }
    
}
