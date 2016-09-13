using HGV.AD.Web.Data;
using HGV.AD.Web.Models.Attributes;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

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
            _logger = loggerFactory.CreateLogger<SeedService>();
        }

        public void SeedHeroes()
        {
            var heroes = new List<HeroAttributes>()
            {
                new HeroAttributes() { Id = 1, LookupId = 30, Identity = "antimage" },
                new HeroAttributes() { Id = 2, LookupId = 16, Identity = "axe" },
                new HeroAttributes() { Id = 3, LookupId = 78, Identity = "bane" },
                new HeroAttributes() { Id = 4, LookupId = 46, Identity = "bloodseeker" },
                new HeroAttributes() { Id = 5, LookupId = 61, Identity = "crystal_maiden" },
                new HeroAttributes() { Id = 6, LookupId = 31, Identity = "drow_ranger" },
                new HeroAttributes() { Id = 7, LookupId = 1, Identity = "earthshaker" },
                new HeroAttributes() { Id = 8, LookupId = 32, Identity = "juggernaut" },
                new HeroAttributes() { Id = 9, LookupId = 33, Identity = "mirana" },
                new HeroAttributes() { Id = 12, LookupId = 35, Identity = "phantom_lancer" },
                new HeroAttributes() { Id = 14, LookupId = 17, Identity = "pudge" },
                new HeroAttributes() { Id = 15, LookupId = 48, Identity = "razor" },
                new HeroAttributes() { Id = 16, LookupId = 18, Identity = "sand_king" },
                new HeroAttributes() { Id = 17, LookupId = 63, Identity = "storm_spirit" },
                new HeroAttributes() { Id = 18, LookupId = 2, Identity = "sven" },
                new HeroAttributes() { Id = 19, LookupId = 3, Identity = "tiny" },
                new HeroAttributes() { Id = 20, LookupId = 36, Identity = "vengefulspirit" },
                new HeroAttributes() { Id = 21, LookupId = 64, Identity = "windrunner" },
                new HeroAttributes() { Id = 22, LookupId = 65, Identity = "zuus" },
                new HeroAttributes() { Id = 23, LookupId = 4, Identity = "kunkka" },
                new HeroAttributes() { Id = 25, LookupId = 66, Identity = "lina" },
                new HeroAttributes() { Id = 26, LookupId = 80, Identity = "lion" },
                new HeroAttributes() { Id = 27, LookupId = 67, Identity = "shadow_shaman" },
                new HeroAttributes() { Id = 28, LookupId = 19, Identity = "slardar" },
                new HeroAttributes() { Id = 29, LookupId = 20, Identity = "tidehunter" },
                new HeroAttributes() { Id = 30, LookupId = 81, Identity = "witch_doctor" },
                new HeroAttributes() { Id = 31, LookupId = 79, Identity = "lich" },
                new HeroAttributes() { Id = 32, LookupId = 37, Identity = "riki" },
                new HeroAttributes() { Id = 33, LookupId = 82, Identity = "enigma" },
                new HeroAttributes() { Id = 34, LookupId = 68, Identity = "tinker" },
                new HeroAttributes() { Id = 35, LookupId = 38, Identity = "sniper" },
                new HeroAttributes() { Id = 36, LookupId = 83, Identity = "necrolyte" },
                new HeroAttributes() { Id = 37, LookupId = 84, Identity = "warlock" },
                new HeroAttributes() { Id = 39, LookupId = 85, Identity = "queenofpain" },
                new HeroAttributes() { Id = 40, LookupId = 49, Identity = "venomancer" },
                new HeroAttributes() { Id = 41, LookupId = 50, Identity = "faceless_void" },
                new HeroAttributes() { Id = 42, LookupId = 21, Identity = "skeleton_king" },
                new HeroAttributes() { Id = 43, LookupId = 86, Identity = "death_prophet" },
                new HeroAttributes() { Id = 44, LookupId = 51, Identity = "phantom_assassin" },
                new HeroAttributes() { Id = 45, LookupId = 87, Identity = "pugna" },
                new HeroAttributes() { Id = 47, LookupId = 52, Identity = "viper" },
                new HeroAttributes() { Id = 48, LookupId = 40, Identity = "luna" },
                new HeroAttributes() { Id = 49, LookupId = 6, Identity = "dragon_knight" },
                new HeroAttributes() { Id = 50, LookupId = 88, Identity = "dazzle" },
                new HeroAttributes() { Id = 51, LookupId = 7, Identity = "rattletrap" },
                new HeroAttributes() { Id = 52, LookupId = 89, Identity = "leshrac" },
                new HeroAttributes() { Id = 53, LookupId = 69, Identity = "furion" },
                new HeroAttributes() { Id = 54, LookupId = 22, Identity = "life_stealer" },
                new HeroAttributes() { Id = 55, LookupId = 90, Identity = "dark_seer" },
                new HeroAttributes() { Id = 56, LookupId = 53, Identity = "clinkz" },
                new HeroAttributes() { Id = 57, LookupId = 8, Identity = "omniknight" },
                new HeroAttributes() { Id = 58, LookupId = 70, Identity = "enchantress" },
                new HeroAttributes() { Id = 59, LookupId = 9, Identity = "huskar" },
                new HeroAttributes() { Id = 60, LookupId = 23, Identity = "night_stalker" },
                new HeroAttributes() { Id = 61, LookupId = 54, Identity = "broodmother" },
                new HeroAttributes() { Id = 62, LookupId = 41, Identity = "bounty_hunter" },
                new HeroAttributes() { Id = 63, LookupId = 55, Identity = "weaver" },
                new HeroAttributes() { Id = 64, LookupId = 71, Identity = "jakiro" },
                new HeroAttributes() { Id = 65, LookupId = 91, Identity = "batrider" },
                new HeroAttributes() { Id = 68, LookupId = 92, Identity = "ancient_apparition" },
                new HeroAttributes() { Id = 70, LookupId = 42, Identity = "ursa" },
                new HeroAttributes() { Id = 71, LookupId = 25, Identity = "spirit_breaker" },
                new HeroAttributes() { Id = 72, LookupId = 43, Identity = "gyrocopter" },
                new HeroAttributes() { Id = 73, LookupId = 10, Identity = "alchemist" },
                new HeroAttributes() { Id = 75, LookupId = 73, Identity = "silencer" },
                new HeroAttributes() { Id = 76, LookupId = 94, Identity = "obsidian_destroyer" },
                new HeroAttributes() { Id = 77, LookupId = 26, Identity = "lycan" },
                new HeroAttributes() { Id = 78, LookupId = 11, Identity = "brewmaster" },
                new HeroAttributes() { Id = 81, LookupId = 27, Identity = "chaos_knight" },
                new HeroAttributes() { Id = 83, LookupId = 12, Identity = "treant" },
                new HeroAttributes() { Id = 85, LookupId = 28, Identity = "undying" },
                new HeroAttributes() { Id = 87, LookupId = 76, Identity = "disruptor" },
                new HeroAttributes() { Id = 88, LookupId = 58, Identity = "nyx_assassin" },
                new HeroAttributes() { Id = 89, LookupId = 45, Identity = "naga_siren" },
                new HeroAttributes() { Id = 92, LookupId = 96, Identity = "visage" },
                new HeroAttributes() { Id = 93, LookupId = 59, Identity = "slark" },
                new HeroAttributes() { Id = 94, LookupId = 60, Identity = "medusa" },
                new HeroAttributes() { Id = 96, LookupId = 14, Identity = "centaur" },
                new HeroAttributes() { Id = 97, LookupId = 29, Identity = "magnataur" },
                new HeroAttributes() { Id = 99, LookupId = 99, Identity = "bristleback" },
                new HeroAttributes() { Id = 101, LookupId = 100, Identity = "skywrath_mage" },
                new HeroAttributes() { Id = 102, LookupId = 102, Identity = "abaddon" },
                new HeroAttributes() { Id = 103, LookupId = 101, Identity = "elder_titan" },
                new HeroAttributes() { Id = 104, LookupId = 105, Identity = "legion_commander" },
                new HeroAttributes() { Id = 109, LookupId = 106, Identity = "terrorblade" },
                new HeroAttributes() { Id = 111, LookupId = 109, Identity = "oracle" },
                new HeroAttributes() { Id = 112, LookupId = 110, Identity = "winter_wyvern" },
                new HeroAttributes() { Id = 113, LookupId = 111, Identity = "arc_warden" }
            };

            // Remove all others
            _dbContext.Heroes.RemoveRange(_dbContext.Heroes);
            _dbContext.SaveChanges();

            // Add new
            _dbContext.Heroes.AddRange(heroes);
            _dbContext.SaveChanges();

            // Get other hero attributes
            var httpClient = new HttpClient();
            var jsonData = httpClient.GetStringAsync("http://herostats.io:322/heroes/all").Result;

            var collection = JObject.Parse(jsonData);

            foreach (var item in collection)
            {
                JObject obj = item.Value as JObject;

                var lookupID = obj["ID"].Value<int>();

                var hero = _dbContext.Heroes.SingleOrDefault(_ => _.LookupId == lookupID);
                if (hero == null)
                {
                    _logger.LogWarning(string.Format("No hero found for id: {0}", lookupID));
                    continue;
                }

                hero.Name = obj["Name"].Value<string>();
                hero.Patch = obj["Patch"].Value<string>();
                hero.Primary = obj["PrimaryStat"].Value<int>();
                hero.Alignment = obj["Alignment"].Value<int>();

                hero.Movespeed = obj["Movespeed"].Value<double>();
                hero.MaxDmg = obj["MaxDmg"].Value<double>();
                hero.MinDmg = obj["MinDmg"].Value<double>();
                hero.HP = obj["HP"].Value<double>();
                hero.Mana = obj["Mana"].Value<double>();
                hero.HPRegen = obj["HPRegen"].Value<double>();
                hero.ManaRegen = obj["ManaRegen"].Value<double>();
                hero.Armor = obj["Armor"].Value<double>();
                hero.Range = obj["Range"].Value<double>();
                hero.ProjectileSpeed = obj["ProjectileSpeed"].Value<double>();
                hero.BaseStr = obj["BaseStr"].Value<double>();
                hero.BaseAgi = obj["BaseAgi"].Value<double>();
                hero.BaseInt = obj["BaseInt"].Value<double>();
                hero.StrGain = obj["StrGain"].Value<double>();
                hero.AgiGain = obj["AgiGain"].Value<double>();
                hero.IntGain = obj["IntGain"].Value<double>();
                hero.DayVision = obj["DayVision"].Value<double>();
                hero.NightVision = obj["NightVision"].Value<double>();
                hero.BaseAttackTime = obj["BaseAttackTime"].Value<double>();
                hero.AttackPoint = obj["AttackPoint"].Value<double>();
                hero.AttackSwing = obj["AttackSwing"].Value<double>();
                hero.CastPoint = obj["CastPoint"].Value<double>();
                hero.CastSwing = obj["CastSwing"].Value<double>();
                hero.Turnrate = obj["Turnrate"].Value<double>();
            }

            _dbContext.SaveChanges();

            // Remove all others
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
                        HeroId = hero.Id,
                        Name = item.Name,
                        Value = value,
                        Rank = rank,
                        Min = _dbContext.Heroes.Min(item.Expression),
                        Max = _dbContext.Heroes.Max(item.Expression),
                        Avg = _dbContext.Heroes.Average(item.Expression),
                        Groups = _dbContext.Heroes.GroupBy(item.Expression).Count(),
                    };

                    _dbContext.HeroAttributeRanks.Add(entity);
                }
            }

            _dbContext.SaveChanges();

        }

        public void SeedAbilities()
        {
            var abilities = new List<AbilityAttributes>()
            {
                new AbilityAttributes() { Id = 5003, Identity = "antimage_mana_break", HeroId = 1, Ultimate = false},
                new AbilityAttributes() { Id = 5004, Identity = "antimage_blink", HeroId = 1, Ultimate = false},
                new AbilityAttributes() { Id = 5005, Identity = "antimage_spell_shield", HeroId = 1, Ultimate = false},
                new AbilityAttributes() { Id = 5006, Identity = "antimage_mana_void", HeroId = 1, Ultimate = true },
                new AbilityAttributes() { Id = 5007, Identity = "axe_berserkers_call", HeroId = 2, Ultimate = false},
                new AbilityAttributes() { Id = 5008, Identity = "axe_battle_hunger", HeroId = 2, Ultimate = false},
                new AbilityAttributes() { Id = 5009, Identity = "axe_counter_helix", HeroId = 2, Ultimate = false},
                new AbilityAttributes() { Id = 5010, Identity = "axe_culling_blade", HeroId = 2, Ultimate = true },
                new AbilityAttributes() { Id = 5011, Identity = "bane_brain_sap", HeroId = 3, Ultimate = false},
                new AbilityAttributes() { Id = 5012, Identity = "bane_enfeeble", HeroId = 3, Ultimate = false},
                new AbilityAttributes() { Id = 5013, Identity = "bane_fiends_grip", HeroId = 3, Ultimate = true },
                new AbilityAttributes() { Id = 5014, Identity = "bane_nightmare", HeroId = 3, Ultimate = false},
                new AbilityAttributes() { Id = 5015, Identity = "bloodseeker_bloodrage", HeroId = 4, Ultimate = false},
                new AbilityAttributes() { Id = 5016, Identity = "bloodseeker_blood_bath", HeroId = 4, Ultimate = false},
                new AbilityAttributes() { Id = 5017, Identity = "bloodseeker_thirst", HeroId = 4, Ultimate = false},
                new AbilityAttributes() { Id = 5018, Identity = "bloodseeker_rupture", HeroId = 4, Ultimate = true },
                new AbilityAttributes() { Id = 5019, Identity = "drow_ranger_frost_arrows",HeroId = 6, Ultimate = false},
                new AbilityAttributes() { Id = 5021, Identity = "drow_ranger_trueshot",HeroId = 6, Ultimate = false},
                new AbilityAttributes() { Id = 5022, Identity = "drow_ranger_marksmanship",HeroId = 6, Ultimate = true },
                new AbilityAttributes() { Id = 5023, Identity = "earthshaker_fissure",HeroId = 7, Ultimate = false},
                new AbilityAttributes() { Id = 5024, Identity = "earthshaker_enchant_totem",HeroId = 7, Ultimate = false},
                new AbilityAttributes() { Id = 5025, Identity = "earthshaker_aftershock",HeroId = 7, Ultimate = false},
                new AbilityAttributes() { Id = 5026, Identity = "earthshaker_echo_slam",HeroId = 7, Ultimate = true },
                new AbilityAttributes() { Id = 5027, Identity = "juggernaut_blade_dance",HeroId = 8, Ultimate = false},
                new AbilityAttributes() { Id = 5028, Identity = "juggernaut_blade_fury",HeroId = 8, Ultimate = false},
                new AbilityAttributes() { Id = 5029, Identity = "juggernaut_healing_ward",HeroId = 8, Ultimate = false},
                new AbilityAttributes() { Id = 5030, Identity = "juggernaut_omni_slash",HeroId = 8, Ultimate = true },
                new AbilityAttributes() { Id = 5031, Identity = "kunkka_torrent",HeroId = 23, Ultimate = false},
                new AbilityAttributes() { Id = 5032, Identity = "kunkka_tidebringer",HeroId = 23, Ultimate = false},
                new AbilityAttributes() { Id = 5033, Identity = "kunkka_x_marks_the_spot",HeroId = 23, Ultimate = false},
                new AbilityAttributes() { Id = 5035, Identity = "kunkka_ghostship",HeroId = 23, Ultimate = true },
                new AbilityAttributes() { Id = 5040, Identity = "lina_dragon_slave",HeroId = 25, Ultimate = false},
                new AbilityAttributes() { Id = 5041, Identity = "lina_light_strike_array",HeroId = 25, Ultimate = false},
                new AbilityAttributes() { Id = 5042, Identity = "lina_fiery_soul",HeroId = 25, Ultimate = false},
                new AbilityAttributes() { Id = 5043, Identity = "lina_laguna_blade",HeroId = 25, Ultimate = true },
                new AbilityAttributes() { Id = 5044, Identity = "lion_impale",HeroId = 26, Ultimate = false},
                new AbilityAttributes() { Id = 5045, Identity = "lion_voodoo",HeroId = 26, Ultimate = false},
                new AbilityAttributes() { Id = 5046, Identity = "lion_mana_drain",HeroId = 26, Ultimate = false},
                new AbilityAttributes() { Id = 5047, Identity = "lion_finger_of_death",HeroId = 26, Ultimate = true },
                new AbilityAttributes() { Id = 5048, Identity = "mirana_arrow",HeroId = 9, Ultimate = false},
                new AbilityAttributes() { Id = 5049, Identity = "mirana_invis",HeroId = 9, Ultimate = true, Keywords = "Invisibility " },
                new AbilityAttributes() { Id = 5050, Identity = "mirana_leap",HeroId = 9, Ultimate = false},
                new AbilityAttributes() { Id = 5051, Identity = "mirana_starfall",HeroId = 9, Ultimate = false},
                new AbilityAttributes() { Id = 5065, Identity = "phantom_lancer_spirit_lance", HeroId = 12, Ultimate = false},
                new AbilityAttributes() { Id = 5066, Identity = "phantom_lancer_doppelwalk", HeroId = 12, Ultimate = false},
                new AbilityAttributes() { Id = 5067, Identity = "phantom_lancer_juxtapose", HeroId = 12, Ultimate = true },
                new AbilityAttributes() { Id = 5068, Identity = "phantom_lancer_phantom_edge", HeroId = 12, Ultimate = false},
                new AbilityAttributes() { Id = 5074, Identity = "pudge_flesh_heap", HeroId = 14, Ultimate = false},
                new AbilityAttributes() { Id = 5075, Identity = "pudge_meat_hook", HeroId = 14, Ultimate = false},
                new AbilityAttributes() { Id = 5076, Identity = "pudge_rot", HeroId = 14, Ultimate = false},
                new AbilityAttributes() { Id = 5077, Identity = "pudge_dismember", HeroId = 14, Ultimate = true },
                new AbilityAttributes() { Id = 5078, Identity = "shadow_shaman_ether_shock",  HeroId = 27, Ultimate = false},
                new AbilityAttributes() { Id = 5079, Identity = "shadow_shaman_voodoo",  HeroId = 27, Ultimate = false},
                new AbilityAttributes() { Id = 5080, Identity = "shadow_shaman_shackles",  HeroId = 27, Ultimate = false},
                new AbilityAttributes() { Id = 5081, Identity = "shadow_shaman_mass_serpent_ward",  HeroId = 27, Ultimate = true },
                new AbilityAttributes() { Id = 5082, Identity = "razor_plasma_field",  HeroId = 15, Ultimate = false},
                new AbilityAttributes() { Id = 5083, Identity = "razor_static_link",  HeroId = 15, Ultimate = false},
                new AbilityAttributes() { Id = 5084, Identity = "razor_unstable_current",  HeroId = 15, Ultimate = false},
                new AbilityAttributes() { Id = 5085, Identity = "razor_eye_of_the_storm",  HeroId = 15, Ultimate = true },
                new AbilityAttributes() { Id = 5086, Identity = "skeleton_king_hellfire_blast",  HeroId = 42, Ultimate = false},
                new AbilityAttributes() { Id = 5087, Identity = "skeleton_king_vampiric_aura",  HeroId = 42, Ultimate = false, Keywords = "Lifesteal "},
                new AbilityAttributes() { Id = 5088, Identity = "skeleton_king_mortal_strike",  HeroId = 42, Ultimate = false},
                new AbilityAttributes() { Id = 5089, Identity = "skeleton_king_reincarnation",  HeroId = 42, Ultimate = true },
                new AbilityAttributes() { Id = 5090, Identity = "death_prophet_carrion_swarm",  HeroId = 43, Ultimate = false},
                new AbilityAttributes() { Id = 5091, Identity = "death_prophet_silence",  HeroId = 43, Ultimate = false},
                new AbilityAttributes() { Id = 5093, Identity = "death_prophet_exorcism",  HeroId = 43, Ultimate = true },
                new AbilityAttributes() { Id = 5094, Identity = "sven_storm_bolt",  HeroId = 18, Ultimate = false},
                new AbilityAttributes() { Id = 5095, Identity = "sven_great_cleave",  HeroId = 18, Ultimate = false},
                new AbilityAttributes() { Id = 5096, Identity = "sven_warcry",  HeroId = 18, Ultimate = false},
                new AbilityAttributes() { Id = 5097, Identity = "sven_gods_strength",  HeroId = 18, Ultimate = true },
                new AbilityAttributes() { Id = 5098, Identity = "storm_spirit_static_remnant",  HeroId = 17, Ultimate = false},
                new AbilityAttributes() { Id = 5099, Identity = "storm_spirit_electric_vortex",  HeroId = 17, Ultimate = false},
                new AbilityAttributes() { Id = 5100, Identity = "storm_spirit_overload",  HeroId = 17, Ultimate = false},
                new AbilityAttributes() { Id = 5101, Identity = "storm_spirit_ball_lightning",  HeroId = 17, Ultimate = true},
                new AbilityAttributes() { Id = 5102, Identity = "sandking_burrowstrike",  HeroId = 16, Ultimate = false},
                new AbilityAttributes() { Id = 5103, Identity = "sandking_sand_storm",  HeroId = 16, Ultimate = false},
                new AbilityAttributes() { Id = 5104, Identity = "sandking_caustic_finale",  HeroId = 16, Ultimate = false},
                new AbilityAttributes() { Id = 5105, Identity = "sandking_epicenter",  HeroId = 16, Ultimate = true },
                new AbilityAttributes() { Id = 5106, Identity = "tiny_avalanche",  HeroId = 19, Ultimate = false},
                new AbilityAttributes() { Id = 5107, Identity = "tiny_toss",  HeroId = 19, Ultimate = false},
                new AbilityAttributes() { Id = 5108, Identity = "tiny_craggy_exterior",  HeroId = 19, Ultimate = false},
                new AbilityAttributes() { Id = 5109, Identity = "tiny_grow",  HeroId = 19, Ultimate = true },
                new AbilityAttributes() { Id = 5110, Identity = "zuus_arc_lightning",  HeroId = 22, Ultimate = false},
                new AbilityAttributes() { Id = 5111, Identity = "zuus_lightning_bolt",  HeroId = 22, Ultimate = false},
                new AbilityAttributes() { Id = 5112, Identity = "zuus_static_field",  HeroId = 22, Ultimate = false},
                new AbilityAttributes() { Id = 5113, Identity = "zuus_thundergods_wrath",  HeroId = 22, Ultimate = true },
                new AbilityAttributes() { Id = 5114, Identity = "slardar_sprint",  HeroId = 28, Ultimate = false},
                new AbilityAttributes() { Id = 5115, Identity = "slardar_slithereen_crush",  HeroId = 28, Ultimate = false},
                new AbilityAttributes() { Id = 5116, Identity = "slardar_bash",  HeroId = 28, Ultimate = false},
                new AbilityAttributes() { Id = 5117, Identity = "slardar_amplify_damage",  HeroId = 28, Ultimate = true },
                new AbilityAttributes() { Id = 5118, Identity = "tidehunter_gush",  HeroId = 29, Ultimate = false},
                new AbilityAttributes() { Id = 5119, Identity = "tidehunter_kraken_shell",  HeroId = 29, Ultimate = false},
                new AbilityAttributes() { Id = 5120, Identity = "tidehunter_anchor_smash",  HeroId = 29, Ultimate = false},
                new AbilityAttributes() { Id = 5121, Identity = "tidehunter_ravage",  HeroId = 29, Ultimate = true },
                new AbilityAttributes() { Id = 5122, Identity = "vengefulspirit_magic_missile",  HeroId = 20, Ultimate = false},
                new AbilityAttributes() { Id = 5123, Identity = "vengefulspirit_command_aura",  HeroId = 20, Ultimate = false},
                new AbilityAttributes() { Id = 5124, Identity = "vengefulspirit_wave_of_terror",  HeroId = 20, Ultimate = false},
                new AbilityAttributes() { Id = 5125, Identity = "vengefulspirit_nether_swap",  HeroId = 20, Ultimate = true },
                new AbilityAttributes() { Id = 5126, Identity = "crystal_maiden_crystal_nova",  HeroId = 5, Ultimate = false},
                new AbilityAttributes() { Id = 5127, Identity = "crystal_maiden_frostbite",  HeroId = 5, Ultimate = false},
                new AbilityAttributes() { Id = 5128, Identity = "crystal_maiden_brilliance_aura",  HeroId = 5, Ultimate = false},
                new AbilityAttributes() { Id = 5129, Identity = "crystal_maiden_freezing_field",  HeroId = 5, Ultimate = true },
                new AbilityAttributes() { Id = 5130, Identity = "windrunner_shackleshot",  HeroId = 21, Ultimate = false},
                new AbilityAttributes() { Id = 5131, Identity = "windrunner_powershot",  HeroId = 21, Ultimate = false},
                new AbilityAttributes() { Id = 5132, Identity = "windrunner_windrun",  HeroId = 21, Ultimate = false},
                new AbilityAttributes() { Id = 5133, Identity = "windrunner_focusfire",  HeroId = 21, Ultimate = true },
                new AbilityAttributes() { Id = 5134, Identity = "lich_frost_nova",  HeroId = 31, Ultimate = false},
                new AbilityAttributes() { Id = 5135, Identity = "lich_frost_armor",  HeroId = 31, Ultimate = false},
                new AbilityAttributes() { Id = 5136, Identity = "lich_dark_ritual",  HeroId = 31, Ultimate = false},
                new AbilityAttributes() { Id = 5137, Identity = "lich_chain_frost",  HeroId = 31, Ultimate = true },
                new AbilityAttributes() { Id = 5138, Identity = "witch_doctor_paralyzing_cask",  HeroId = 30, Ultimate = false},
                new AbilityAttributes() { Id = 5139, Identity = "witch_doctor_voodoo_restoration",  HeroId = 30, Ultimate = false},
                new AbilityAttributes() { Id = 5140, Identity = "witch_doctor_maledict",  HeroId = 30, Ultimate = false},
                new AbilityAttributes() { Id = 5141, Identity = "witch_doctor_death_ward",  HeroId = 30, Ultimate = true },
                new AbilityAttributes() { Id = 5142, Identity = "riki_smoke_screen",  HeroId = 32, Ultimate = false, Keywords = "Silence " },
                new AbilityAttributes() { Id = 5143, Identity = "riki_blink_strike",  HeroId = 32, Ultimate = false},
                new AbilityAttributes() { Id = 5144, Identity = "riki_permanent_invisibility",  HeroId = 32, Ultimate = false},
                new AbilityAttributes() { Id = 5145, Identity = "riki_tricks_of_the_trade",  HeroId = 32, Ultimate = true },
                new AbilityAttributes() { Id = 5146, Identity = "enigma_malefice",  HeroId = 33, Ultimate = false},
                new AbilityAttributes() { Id = 5147, Identity = "enigma_demonic_conversion",  HeroId = 33, Ultimate = false},
                new AbilityAttributes() { Id = 5148, Identity = "enigma_midnight_pulse",  HeroId = 33, Ultimate = false},
                new AbilityAttributes() { Id = 5149, Identity = "enigma_black_hole",  HeroId = 33, Ultimate = true },
                new AbilityAttributes() { Id = 5150, Identity = "tinker_laser",  HeroId = 34, Ultimate = false},
                new AbilityAttributes() { Id = 5151, Identity = "tinker_heat_seeking_missile",  HeroId = 34, Ultimate = false},
                new AbilityAttributes() { Id = 5152, Identity = "tinker_march_of_the_machines",  HeroId = 34, Ultimate = false},
                new AbilityAttributes() { Id = 5153, Identity = "tinker_rearm",  HeroId = 34, Ultimate = true },
                new AbilityAttributes() { Id = 5154, Identity = "sniper_shrapnel",  HeroId = 35, Ultimate = false},
                new AbilityAttributes() { Id = 5155, Identity = "sniper_headshot",  HeroId = 35, Ultimate = false},
                new AbilityAttributes() { Id = 5156, Identity = "sniper_take_aim",  HeroId = 35, Ultimate = false},
                new AbilityAttributes() { Id = 5157, Identity = "sniper_assassinate",  HeroId = 35, Ultimate = true },
                new AbilityAttributes() { Id = 5158, Identity = "necrolyte_death_pulse",  HeroId = 36, Ultimate = false},
                new AbilityAttributes() { Id = 5159, Identity = "necrolyte_heartstopper_aura",  HeroId = 36, Ultimate = false},
                new AbilityAttributes() { Id = 5160, Identity = "necrolyte_sadist",  HeroId = 36, Ultimate = false},
                new AbilityAttributes() { Id = 5161, Identity = "necrolyte_reapers_scythe",  HeroId = 36, Ultimate = true },
                new AbilityAttributes() { Id = 5162, Identity = "warlock_fatal_bonds",  HeroId = 37, Ultimate = false},
                new AbilityAttributes() { Id = 5163, Identity = "warlock_shadow_word",  HeroId = 37, Ultimate = false},
                new AbilityAttributes() { Id = 5164, Identity = "warlock_upheaval",  HeroId = 37, Ultimate = false},
                new AbilityAttributes() { Id = 5165, Identity = "warlock_rain_of_chaos",  HeroId = 37, Ultimate = true },
                new AbilityAttributes() { Id = 5173, Identity = "queenofpain_shadow_strike",  HeroId = 39, Ultimate = false},
                new AbilityAttributes() { Id = 5174, Identity = "queenofpain_blink",  HeroId = 39, Ultimate = false},
                new AbilityAttributes() { Id = 5175, Identity = "queenofpain_scream_of_pain",  HeroId = 39, Ultimate = false},
                new AbilityAttributes() { Id = 5176, Identity = "queenofpain_sonic_wave",  HeroId = 39, Ultimate = true },
                new AbilityAttributes() { Id = 5178, Identity = "venomancer_venomous_gale",  HeroId = 40, Ultimate = false},
                new AbilityAttributes() { Id = 5179, Identity = "venomancer_poison_sting",  HeroId = 40, Ultimate = false},
                new AbilityAttributes() { Id = 5180, Identity = "venomancer_plague_ward",  HeroId = 40, Ultimate = false},
                new AbilityAttributes() { Id = 5181, Identity = "venomancer_poison_nova",  HeroId = 40, Ultimate = true },
                new AbilityAttributes() { Id = 5182, Identity = "faceless_void_time_walk",  HeroId = 41, Ultimate = false},
                new AbilityAttributes() { Id = 5184, Identity = "faceless_void_time_lock",  HeroId = 41, Ultimate = false},
                new AbilityAttributes() { Id = 5185, Identity = "faceless_void_chronosphere",  HeroId = 41, Ultimate = true },
                new AbilityAttributes() { Id = 5186, Identity = "pugna_nether_blast",  HeroId = 45, Ultimate = false},
                new AbilityAttributes() { Id = 5187, Identity = "pugna_decrepify",  HeroId = 45, Ultimate = false},
                new AbilityAttributes() { Id = 5188, Identity = "pugna_nether_ward",  HeroId = 45, Ultimate = false},
                new AbilityAttributes() { Id = 5189, Identity = "pugna_life_drain",  HeroId = 45, Ultimate = true },
                new AbilityAttributes() { Id = 5190, Identity = "phantom_assassin_stifling_dagger",  HeroId = 44, Ultimate = false},
                new AbilityAttributes() { Id = 5191, Identity = "phantom_assassin_phantom_strike",  HeroId = 44, Ultimate = false},
                new AbilityAttributes() { Id = 5192, Identity = "phantom_assassin_blur",  HeroId = 44, Ultimate = false},
                new AbilityAttributes() { Id = 5193, Identity = "phantom_assassin_coup_de_grace",  HeroId = 44, Ultimate = true },
                new AbilityAttributes() { Id = 5218, Identity = "viper_poison_attack",  HeroId = 47, Ultimate = false},
                new AbilityAttributes() { Id = 5219, Identity = "viper_nethertoxin",  HeroId = 47, Ultimate = false},
                new AbilityAttributes() { Id = 5220, Identity = "viper_corrosive_skin",  HeroId = 47, Ultimate = false},
                new AbilityAttributes() { Id = 5221, Identity = "viper_viper_strike",  HeroId = 47, Ultimate = true },
                new AbilityAttributes() { Id = 5222, Identity = "luna_lucent_beam",  HeroId = 48, Ultimate = false},
                new AbilityAttributes() { Id = 5223, Identity = "luna_moon_glaive",  HeroId = 48, Ultimate = false},
                new AbilityAttributes() { Id = 5224, Identity = "luna_lunar_blessing",  HeroId = 48, Ultimate = false},
                new AbilityAttributes() { Id = 5225, Identity = "luna_eclipse",  HeroId = 48, Ultimate = true },
                new AbilityAttributes() { Id = 5226, Identity = "dragon_knight_breathe_fire",  HeroId = 49, Ultimate = false},
                new AbilityAttributes() { Id = 5227, Identity = "dragon_knight_dragon_tail",  HeroId = 49, Ultimate = false},
                new AbilityAttributes() { Id = 5228, Identity = "dragon_knight_dragon_blood",  HeroId = 49, Ultimate = false},
                new AbilityAttributes() { Id = 5229, Identity = "dragon_knight_elder_dragon_form",  HeroId = 49, Ultimate = true },
                new AbilityAttributes() { Id = 5233, Identity = "dazzle_poison_touch",  HeroId = 50, Ultimate = false},
                new AbilityAttributes() { Id = 5234, Identity = "dazzle_shallow_grave",  HeroId = 50, Ultimate = false},
                new AbilityAttributes() { Id = 5235, Identity = "dazzle_shadow_wave",  HeroId = 50, Ultimate = false},
                new AbilityAttributes() { Id = 5236, Identity = "dazzle_weave",  HeroId = 50, Ultimate = true },
                new AbilityAttributes() { Id = 5237, Identity = "rattletrap_battery_assault",  HeroId = 51, Ultimate = false},
                new AbilityAttributes() { Id = 5238, Identity = "rattletrap_power_cogs",  HeroId = 51, Ultimate = false},
                new AbilityAttributes() { Id = 5239, Identity = "rattletrap_rocket_flare",  HeroId = 51, Ultimate = false},
                new AbilityAttributes() { Id = 5240, Identity = "rattletrap_hookshot",  HeroId = 51, Ultimate = true },
                new AbilityAttributes() { Id = 5241, Identity = "leshrac_split_earth",  HeroId = 52, Ultimate = false},
                new AbilityAttributes() { Id = 5242, Identity = "leshrac_diabolic_edict",  HeroId = 52, Ultimate = false},
                new AbilityAttributes() { Id = 5243, Identity = "leshrac_lightning_storm",  HeroId = 52, Ultimate = false},
                new AbilityAttributes() { Id = 5244, Identity = "leshrac_pulse_nova",  HeroId = 52, Ultimate = true },
                new AbilityAttributes() { Id = 5245, Identity = "furion_sprout",  HeroId = 53, Ultimate = false},
                new AbilityAttributes() { Id = 5246, Identity = "furion_teleportation",  HeroId = 53, Ultimate = false},
                new AbilityAttributes() { Id = 5247, Identity = "furion_force_of_nature",  HeroId = 53, Ultimate = false},
                new AbilityAttributes() { Id = 5248, Identity = "furion_wrath_of_nature",  HeroId = 53, Ultimate = true },
                new AbilityAttributes() { Id = 5249, Identity = "life_stealer_rage",  HeroId = 54, Ultimate = false},
                new AbilityAttributes() { Id = 5250, Identity = "life_stealer_feast",  HeroId = 54, Ultimate = false},
                new AbilityAttributes() { Id = 5251, Identity = "life_stealer_open_wounds",  HeroId = 54, Ultimate = false},
                new AbilityAttributes() { Id = 5252, Identity = "life_stealer_infest",  HeroId = 54, Ultimate = true },
                new AbilityAttributes() { Id = 5255, Identity = "dark_seer_vacuum",  HeroId = 55, Ultimate = false},
                new AbilityAttributes() { Id = 5256, Identity = "dark_seer_ion_shell",  HeroId = 55, Ultimate = false},
                new AbilityAttributes() { Id = 5257, Identity = "dark_seer_surge",  HeroId = 55, Ultimate = false},
                new AbilityAttributes() { Id = 5258, Identity = "dark_seer_wall_of_replica",  HeroId = 55, Ultimate = true },
                new AbilityAttributes() { Id = 5259, Identity = "clinkz_strafe",  HeroId = 56, Ultimate = false},
                new AbilityAttributes() { Id = 5260, Identity = "clinkz_searing_arrows",  HeroId = 56, Ultimate = false},
                new AbilityAttributes() { Id = 5261, Identity = "clinkz_wind_walk",  HeroId = 56, Ultimate = false, Keywords = "Invisibility " },
                new AbilityAttributes() { Id = 5262, Identity = "clinkz_death_pact",  HeroId = 56, Ultimate = true },
                new AbilityAttributes() { Id = 5263, Identity = "omniknight_purification",  HeroId = 57, Ultimate = false},
                new AbilityAttributes() { Id = 5264, Identity = "omniknight_repel",  HeroId = 57, Ultimate = false},
                new AbilityAttributes() { Id = 5265, Identity = "omniknight_degen_aura",  HeroId = 57, Ultimate = false},
                new AbilityAttributes() { Id = 5266, Identity = "omniknight_guardian_angel",  HeroId = 57, Ultimate = true },
                new AbilityAttributes() { Id = 5267, Identity = "enchantress_untouchable",  HeroId = 58, Ultimate = false},
                new AbilityAttributes() { Id = 5268, Identity = "enchantress_enchant",  HeroId = 58, Ultimate = false},
                new AbilityAttributes() { Id = 5269, Identity = "enchantress_natures_attendants",  HeroId = 58, Ultimate = false},
                new AbilityAttributes() { Id = 5270, Identity = "enchantress_impetus",  HeroId = 58, Ultimate = true },
                new AbilityAttributes() { Id = 5271, Identity = "huskar_inner_vitality",  HeroId = 59, Ultimate = false},
                new AbilityAttributes() { Id = 5272, Identity = "huskar_burning_spear",  HeroId = 59, Ultimate = false},
                new AbilityAttributes() { Id = 5273, Identity = "huskar_berserkers_blood",  HeroId = 59, Ultimate = false},
                new AbilityAttributes() { Id = 5274, Identity = "huskar_life_break",  HeroId = 59, Ultimate = true },
                new AbilityAttributes() { Id = 5275, Identity = "night_stalker_void",  HeroId = 60, Ultimate = false},
                new AbilityAttributes() { Id = 5276, Identity = "night_stalker_crippling_fear",  HeroId = 60, Ultimate = false},
                new AbilityAttributes() { Id = 5277, Identity = "night_stalker_hunter_in_the_night",  HeroId = 60, Ultimate = false},
                new AbilityAttributes() { Id = 5278, Identity = "night_stalker_darkness",  HeroId = 60, Ultimate = true },
                new AbilityAttributes() { Id = 5279, Identity = "broodmother_spawn_spiderlings",  HeroId = 61, Ultimate = false},
                new AbilityAttributes() { Id = 5280, Identity = "broodmother_spin_web",  HeroId = 61, Ultimate = false, Keywords = "Invisibility "},
                new AbilityAttributes() { Id = 5281, Identity = "broodmother_incapacitating_bite",  HeroId = 61, Ultimate = false},
                new AbilityAttributes() { Id = 5282, Identity = "broodmother_insatiable_hunger",  HeroId = 61, Ultimate = true },
                new AbilityAttributes() { Id = 5285, Identity = "bounty_hunter_shuriken_toss",  HeroId = 62, Ultimate = false},
                new AbilityAttributes() { Id = 5286, Identity = "bounty_hunter_jinada",  HeroId = 62, Ultimate = false},
                new AbilityAttributes() { Id = 5287, Identity = "bounty_hunter_wind_walk",  HeroId = 62, Ultimate = false},
                new AbilityAttributes() { Id = 5288, Identity = "bounty_hunter_track",  HeroId = 62, Ultimate = true },
                new AbilityAttributes() { Id = 5289, Identity = "weaver_the_swarm",  HeroId = 63, Ultimate = false},
                new AbilityAttributes() { Id = 5290, Identity = "weaver_shukuchi",  HeroId = 63, Ultimate = false},
                new AbilityAttributes() { Id = 5291, Identity = "weaver_geminate_attack",  HeroId = 63, Ultimate = false},
                new AbilityAttributes() { Id = 5292, Identity = "weaver_time_lapse",  HeroId = 63, Ultimate = true },
                new AbilityAttributes() { Id = 5297, Identity = "jakiro_dual_breath",  HeroId = 64, Ultimate = false},
                new AbilityAttributes() { Id = 5298, Identity = "jakiro_ice_path",  HeroId = 64, Ultimate = false},
                new AbilityAttributes() { Id = 5299, Identity = "jakiro_liquid_fire",  HeroId = 64, Ultimate = false},
                new AbilityAttributes() { Id = 5300, Identity = "jakiro_macropyre",  HeroId = 64, Ultimate = true },
                new AbilityAttributes() { Id = 5320, Identity = "batrider_sticky_napalm",  HeroId = 65, Ultimate = false},
                new AbilityAttributes() { Id = 5321, Identity = "batrider_flamebreak",  HeroId = 65, Ultimate = false},
                new AbilityAttributes() { Id = 5322, Identity = "batrider_firefly",  HeroId = 65, Ultimate = false},
                new AbilityAttributes() { Id = 5323, Identity = "batrider_flaming_lasso",  HeroId = 65, Ultimate = true },
                new AbilityAttributes() { Id = 5345, Identity = "ancient_apparition_cold_feet",  HeroId = 68, Ultimate = false},
                new AbilityAttributes() { Id = 5346, Identity = "ancient_apparition_ice_vortex",  HeroId = 68, Ultimate = false},
                new AbilityAttributes() { Id = 5347, Identity = "ancient_apparition_chilling_touch",  HeroId = 68, Ultimate = false},
                new AbilityAttributes() { Id = 5348, Identity = "ancient_apparition_ice_blast",  HeroId = 68, Ultimate = true },
                new AbilityAttributes() { Id = 5353, Identity = "spirit_breaker_charge_of_darkness",  HeroId = 71, Ultimate = false},
                new AbilityAttributes() { Id = 5354, Identity = "spirit_breaker_empowering_haste",  HeroId = 71, Ultimate = false},
                new AbilityAttributes() { Id = 5355, Identity = "spirit_breaker_greater_bash",  HeroId = 71, Ultimate = false},
                new AbilityAttributes() { Id = 5356, Identity = "spirit_breaker_nether_strike",  HeroId = 71, Ultimate = true },
                new AbilityAttributes() { Id = 5357, Identity = "ursa_earthshock",  HeroId = 70, Ultimate = false},
                new AbilityAttributes() { Id = 5358, Identity = "ursa_overpower",  HeroId = 70, Ultimate = false},
                new AbilityAttributes() { Id = 5359, Identity = "ursa_fury_swipes",  HeroId = 70, Ultimate = false},
                new AbilityAttributes() { Id = 5360, Identity = "ursa_enrage",  HeroId = 70, Ultimate = true },
                new AbilityAttributes() { Id = 5361, Identity = "gyrocopter_rocket_barrage",  HeroId = 72, Ultimate = false},
                new AbilityAttributes() { Id = 5362, Identity = "gyrocopter_homing_missile",  HeroId = 72, Ultimate = false},
                new AbilityAttributes() { Id = 5363, Identity = "gyrocopter_flak_cannon",  HeroId = 72, Ultimate = false},
                new AbilityAttributes() { Id = 5364, Identity = "gyrocopter_call_down",  HeroId = 72, Ultimate = true },
                new AbilityAttributes() { Id = 5365, Identity = "alchemist_acid_spray",  HeroId = 73, Ultimate = false},
                new AbilityAttributes() { Id = 5366, Identity = "alchemist_unstable_concoction",  HeroId = 73, Ultimate = false},
                new AbilityAttributes() { Id = 5368, Identity = "alchemist_goblins_greed",  HeroId = 73, Ultimate = false},
                new AbilityAttributes() { Id = 5369, Identity = "alchemist_chemical_rage",  HeroId = 73, Ultimate = true },
                new AbilityAttributes() { Id = 5377, Identity = "silencer_curse_of_the_silent",  HeroId = 75, Ultimate = false},
                new AbilityAttributes() { Id = 5378, Identity = "silencer_glaives_of_wisdom",  HeroId = 75, Ultimate = false},
                new AbilityAttributes() { Id = 5379, Identity = "silencer_last_word",  HeroId = 75, Ultimate = false},
                new AbilityAttributes() { Id = 5380, Identity = "silencer_global_silence",  HeroId = 75, Ultimate = true },
                new AbilityAttributes() { Id = 5391, Identity = "obsidian_destroyer_arcane_orb",  HeroId = 76, Ultimate = false},
                new AbilityAttributes() { Id = 5392, Identity = "obsidian_destroyer_astral_imprisonment",  HeroId = 76, Ultimate = false},
                new AbilityAttributes() { Id = 5393, Identity = "obsidian_destroyer_essence_aura",  HeroId = 76, Ultimate = false},
                new AbilityAttributes() { Id = 5394, Identity = "obsidian_destroyer_sanity_eclipse",  HeroId = 76, Ultimate = true },
                new AbilityAttributes() { Id = 5395, Identity = "lycan_summon_wolves",  HeroId = 77, Ultimate = false},
                new AbilityAttributes() { Id = 5396, Identity = "lycan_howl",  HeroId = 77, Ultimate = false},
                new AbilityAttributes() { Id = 5397, Identity = "lycan_feral_impulse",  HeroId = 77, Ultimate = false},
                new AbilityAttributes() { Id = 5398, Identity = "lycan_shapeshift",  HeroId = 77, Ultimate = true },
                new AbilityAttributes() { Id = 5400, Identity = "brewmaster_thunder_clap",  HeroId = 78, Ultimate = false},
                new AbilityAttributes() { Id = 5401, Identity = "brewmaster_drunken_haze",  HeroId = 78, Ultimate = false},
                new AbilityAttributes() { Id = 5402, Identity = "brewmaster_drunken_brawler",  HeroId = 78, Ultimate = false},
                new AbilityAttributes() { Id = 5403, Identity = "brewmaster_primal_split",  HeroId = 78, Ultimate = true },
                new AbilityAttributes() { Id = 5426, Identity = "chaos_knight_chaos_bolt",  HeroId = 81, Ultimate = false},
                new AbilityAttributes() { Id = 5427, Identity = "chaos_knight_reality_rift",  HeroId = 81, Ultimate = false},
                new AbilityAttributes() { Id = 5428, Identity = "chaos_knight_chaos_strike",  HeroId = 81, Ultimate = false},
                new AbilityAttributes() { Id = 5429, Identity = "chaos_knight_phantasm",  HeroId = 81, Ultimate = true },
                new AbilityAttributes() { Id = 5434, Identity = "treant_natures_guise",  HeroId = 83, Ultimate = false, Keywords = "Invisibility " },
                new AbilityAttributes() { Id = 5435, Identity = "treant_leech_seed",  HeroId = 83, Ultimate = false},
                new AbilityAttributes() { Id = 5436, Identity = "treant_living_armor",  HeroId = 83, Ultimate = false},
                new AbilityAttributes() { Id = 5437, Identity = "treant_overgrowth",  HeroId = 83, Ultimate = true },
                new AbilityAttributes() { Id = 5442, Identity = "undying_decay",  HeroId = 85, Ultimate = false},
                new AbilityAttributes() { Id = 5443, Identity = "undying_soul_rip",  HeroId = 85, Ultimate = false},
                new AbilityAttributes() { Id = 5444, Identity = "undying_tombstone",  HeroId = 85, Ultimate = false},
                new AbilityAttributes() { Id = 5447, Identity = "undying_flesh_golem",  HeroId = 85, Ultimate = true },
                new AbilityAttributes() { Id = 5458, Identity = "disruptor_thunder_strike",  HeroId = 87, Ultimate = false},
                new AbilityAttributes() { Id = 5459, Identity = "disruptor_glimpse",  HeroId = 87, Ultimate = false},
                new AbilityAttributes() { Id = 5460, Identity = "disruptor_kinetic_field",  HeroId = 87, Ultimate = false},
                new AbilityAttributes() { Id = 5461, Identity = "disruptor_static_storm",  HeroId = 87, Ultimate = true },
                new AbilityAttributes() { Id = 5462, Identity = "nyx_assassin_impale",  HeroId = 88, Ultimate = false},
                new AbilityAttributes() { Id = 5463, Identity = "nyx_assassin_mana_burn",  HeroId = 88, Ultimate = false},
                new AbilityAttributes() { Id = 5464, Identity = "nyx_assassin_spiked_carapace",  HeroId = 88, Ultimate = false},
                new AbilityAttributes() { Id = 5465, Identity = "nyx_assassin_vendetta",  HeroId = 88, Ultimate = true },
                new AbilityAttributes() { Id = 5467, Identity = "naga_siren_mirror_image",  HeroId = 89, Ultimate = false},
                new AbilityAttributes() { Id = 5468, Identity = "naga_siren_ensnare",  HeroId = 89, Ultimate = false},
                new AbilityAttributes() { Id = 5469, Identity = "naga_siren_rip_tide",  HeroId = 89, Ultimate = false},
                new AbilityAttributes() { Id = 5470, Identity = "naga_siren_song_of_the_siren",  HeroId = 89, Ultimate = true, Keywords = "Stun "  },
                new AbilityAttributes() { Id = 5480, Identity = "visage_grave_chill",  HeroId = 92, Ultimate = false},
                new AbilityAttributes() { Id = 5481, Identity = "visage_soul_assumption",  HeroId = 92, Ultimate = false},
                new AbilityAttributes() { Id = 5482, Identity = "visage_gravekeepers_cloak",  HeroId = 92, Ultimate = false},
                new AbilityAttributes() { Id = 5483, Identity = "visage_summon_familiars",  HeroId = 92, Ultimate = true },
                new AbilityAttributes() { Id = 5494, Identity = "slark_dark_pact",  HeroId = 93, Ultimate = false},
                new AbilityAttributes() { Id = 5495, Identity = "slark_pounce",  HeroId = 93, Ultimate = false},
                new AbilityAttributes() { Id = 5496, Identity = "slark_essence_shift",  HeroId = 93, Ultimate = false},
                new AbilityAttributes() { Id = 5497, Identity = "slark_shadow_dance",  HeroId = 93, Ultimate = true },
                new AbilityAttributes() { Id = 5504, Identity = "medusa_split_shot",  HeroId = 94, Ultimate = false},
                new AbilityAttributes() { Id = 5505, Identity = "medusa_mystic_snake",  HeroId = 94, Ultimate = false},
                new AbilityAttributes() { Id = 5506, Identity = "medusa_mana_shield",  HeroId = 94, Ultimate = false},
                new AbilityAttributes() { Id = 5507, Identity = "medusa_stone_gaze",  HeroId = 94, Ultimate = true },
                new AbilityAttributes() { Id = 5514, Identity = "centaur_hoof_stomp",  HeroId = 96, Ultimate = false},
                new AbilityAttributes() { Id = 5515, Identity = "centaur_double_edge",  HeroId = 96, Ultimate = false},
                new AbilityAttributes() { Id = 5516, Identity = "centaur_return",  HeroId = 96, Ultimate = false},
                new AbilityAttributes() { Id = 5517, Identity = "centaur_stampede",  HeroId = 96, Ultimate = true },
                new AbilityAttributes() { Id = 5518, Identity = "magnataur_shockwave",  HeroId = 97, Ultimate = false},
                new AbilityAttributes() { Id = 5519, Identity = "magnataur_empower",  HeroId = 97, Ultimate = false},
                new AbilityAttributes() { Id = 5520, Identity = "magnataur_skewer",  HeroId = 97, Ultimate = false},
                new AbilityAttributes() { Id = 5521, Identity = "magnataur_reverse_polarity",  HeroId = 97, Ultimate = true },
                new AbilityAttributes() { Id = 5548, Identity = "bristleback_viscous_nasal_goo",  HeroId = 99, Ultimate = false},
                new AbilityAttributes() { Id = 5549, Identity = "bristleback_quill_spray",  HeroId = 99, Ultimate = false},
                new AbilityAttributes() { Id = 5550, Identity = "bristleback_bristleback",  HeroId = 99, Ultimate = false},
                new AbilityAttributes() { Id = 5551, Identity = "bristleback_warpath",  HeroId = 99, Ultimate = true },
                new AbilityAttributes() { Id = 5581, Identity = "skywrath_mage_arcane_bolt",  HeroId = 101, Ultimate = false},
                new AbilityAttributes() { Id = 5582, Identity = "skywrath_mage_concussive_shot",  HeroId = 101, Ultimate = false},
                new AbilityAttributes() { Id = 5583, Identity = "skywrath_mage_ancient_seal",  HeroId = 101, Ultimate = false},
                new AbilityAttributes() { Id = 5584, Identity = "skywrath_mage_mystic_flare",  HeroId = 101, Ultimate = true },
                new AbilityAttributes() { Id = 5585, Identity = "abaddon_death_coil",  HeroId = 102, Ultimate = false},
                new AbilityAttributes() { Id = 5586, Identity = "abaddon_aphotic_shield",  HeroId = 102, Ultimate = false},
                new AbilityAttributes() { Id = 5587, Identity = "abaddon_frostmourne",  HeroId = 102, Ultimate = false},
                new AbilityAttributes() { Id = 5588, Identity = "abaddon_borrowed_time",  HeroId = 102, Ultimate = true },
                new AbilityAttributes() { Id = 5589, Identity = "elder_titan_echo_stomp",  HeroId = 103, Ultimate = false},
                new AbilityAttributes() { Id = 5591, Identity = "elder_titan_ancestral_spirit",  HeroId = 103, Ultimate = false},
                new AbilityAttributes() { Id = 5593, Identity = "elder_titan_natural_order",  HeroId = 103, Ultimate = false},
                new AbilityAttributes() { Id = 5594, Identity = "elder_titan_earth_splitter",  HeroId = 103, Ultimate = true },
                new AbilityAttributes() { Id = 5595, Identity = "legion_commander_overwhelming_odds",  HeroId = 104, Ultimate = false},
                new AbilityAttributes() { Id = 5596, Identity = "legion_commander_press_the_attack",  HeroId = 104, Ultimate = false},
                new AbilityAttributes() { Id = 5597, Identity = "legion_commander_moment_of_courage",  HeroId = 104, Ultimate = false},
                new AbilityAttributes() { Id = 5598, Identity = "legion_commander_duel",  HeroId = 104, Ultimate = true },
                new AbilityAttributes() { Id = 5619, Identity = "terrorblade_reflection",  HeroId = 109, Ultimate = false},
                new AbilityAttributes() { Id = 5620, Identity = "terrorblade_conjure_image",  HeroId = 109, Ultimate = false},
                new AbilityAttributes() { Id = 5621, Identity = "terrorblade_metamorphosis",  HeroId = 109, Ultimate = false},
                new AbilityAttributes() { Id = 5622, Identity = "terrorblade_sunder",  HeroId = 109, Ultimate = true },
                new AbilityAttributes() { Id = 5632, Identity = "drow_ranger_wave_of_silence",HeroId = 6, Ultimate = false},
                new AbilityAttributes() { Id = 5637, Identity = "oracle_fortunes_end",  HeroId = 111, Ultimate = false},
                new AbilityAttributes() { Id = 5638, Identity = "oracle_fates_edict",  HeroId = 111, Ultimate = false},
                new AbilityAttributes() { Id = 5639, Identity = "oracle_purifying_flames",  HeroId = 111, Ultimate = false},
                new AbilityAttributes() { Id = 5640, Identity = "oracle_false_promise",  HeroId = 111, Ultimate = true },
                new AbilityAttributes() { Id = 5651, Identity = "winter_wyvern_arctic_burn",  HeroId = 112, Ultimate = false},
                new AbilityAttributes() { Id = 5652, Identity = "winter_wyvern_splinter_blast",  HeroId = 112, Ultimate = false},
                new AbilityAttributes() { Id = 5653, Identity = "winter_wyvern_cold_embrace",  HeroId = 112, Ultimate = false},
                new AbilityAttributes() { Id = 5654, Identity = "winter_wyvern_winters_curse",  HeroId = 112, Ultimate = true },
                new AbilityAttributes() { Id = 5677, Identity = "arc_warden_flux",  HeroId = 113, Ultimate = false},
                new AbilityAttributes() { Id = 5678, Identity = "arc_warden_magnetic_field",  HeroId = 113, Ultimate = false},
                new AbilityAttributes() { Id = 5679, Identity = "arc_warden_spark_wraith",  HeroId = 113, Ultimate = false},
                new AbilityAttributes() { Id = 5683, Identity = "arc_warden_tempest_double",  HeroId = 113, Ultimate = true },
                new AbilityAttributes() { Id = 5685, Identity = "death_prophet_spirit_siphon",  HeroId = 43, Ultimate = false},
                new AbilityAttributes() { Id = 5691, Identity = "faceless_void_time_dilation",  HeroId = 41, Ultimate = false}
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

        public void SeedTrends()
        {
            var abilities = _dbContext.Abilities.ToList();
            var heroes = _dbContext.Heroes.ToList();
            
            _dbContext.Database.ExecuteSqlCommand("TRUNCATE TABLE NextAbilityTrends;TRUNCATE TABLE PerviousAbilityTrends;TRUNCATE TABLE CurrentAbilityTrends;");
            _dbContext.Database.ExecuteSqlCommand("TRUNCATE TABLE NextAbilityComboTrends;TRUNCATE TABLE PerviousAbilityComboTrends;TRUNCATE TABLE CurrentAbilityComboTrends;");
            _dbContext.Database.ExecuteSqlCommand("TRUNCATE TABLE NextHeroTrends;TRUNCATE TABLE PerviousHeroTrends;TRUNCATE TABLE CurrentHeroTrends;");
            _dbContext.Database.ExecuteSqlCommand("TRUNCATE TABLE NextHeroComboTrends;TRUNCATE TABLE PerviousHeroComboTrends;TRUNCATE TABLE CurrentHeroComboTrends;");

            // Add Abilities
            foreach (var item in abilities)
            {
                _dbContext.NextAbilityTrends.Add(new Models.Statistics.NextAbilityStat()
                {
                    AbilityId = item.Id,
                    Identity = item.Identity,
                    Name = item.Name,
                });
                _dbContext.PerviousAbilityTrends.Add(new Models.Statistics.PerviousAbilityStat()
                {
                    AbilityId = item.Id,
                    Identity = item.Identity,
                    Name = item.Name,
                });
                _dbContext.CurrentAbilityTrends.Add(new Models.Statistics.CurrentAbilityStat()
                {
                    AbilityId = item.Id,
                    Identity = item.Identity,
                    Name = item.Name,
                });
            }

            _dbContext.SaveChanges();

            // Add Ability Combos
            var abilityCombos = abilities
                .SelectMany(x => abilities, (x, y) => Tuple.Create(x, y))
                .Where(_ => _.Item1.Id != _.Item2.Id)
                .ToList();

            foreach (var combo in abilityCombos)
            {
                _dbContext.NextAbilityComboTrends.Add(new Models.Statistics.NextAbilityComboStat()
                {
                    AbilityId = combo.Item1.Id,
                    AbilityIdentity = combo.Item1.Identity,
                    AbilityName = combo.Item1.Name,
                    ComboId = combo.Item2.Id,
                    ComboIdentity = combo.Item2.Identity,
                    ComboName = combo.Item2.Name,
                });
                _dbContext.CurrentAbilityComboTrends.Add(new Models.Statistics.CurrentAbilityComboStat()
                {
                    AbilityId = combo.Item1.Id,
                    AbilityIdentity = combo.Item1.Identity,
                    AbilityName = combo.Item1.Name,
                    ComboId = combo.Item2.Id,
                    ComboIdentity = combo.Item2.Identity,
                    ComboName = combo.Item2.Name,
                });
                _dbContext.PerviousAbilityComboTrends.Add(new Models.Statistics.PerviousAbilityComboStat()
                {
                    AbilityId = combo.Item1.Id,
                    AbilityIdentity = combo.Item1.Identity,
                    AbilityName = combo.Item1.Name,
                    ComboId = combo.Item2.Id,
                    ComboIdentity = combo.Item2.Identity,
                    ComboName = combo.Item2.Name,
                });
            }

            _dbContext.SaveChanges();

            // Add Heroes
            foreach (var item in heroes)
            {
                _dbContext.NextHeroTrends.Add(new Models.Statistics.NextHeroStat()
                {
                    HeroId = item.Id,
                    Identity = item.Identity,
                    Name = item.Name,
                });
                _dbContext.CurrentHeroTrends.Add(new Models.Statistics.CurrentHeroStat()
                {
                    HeroId = item.Id,
                    Identity = item.Identity,
                    Name = item.Name,
                });
                _dbContext.PerviousHeroTrends.Add(new Models.Statistics.PerviousHeroStat()
                {
                    HeroId = item.Id,
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
                    _dbContext.NextHeroComboTrends.Add(new Models.Statistics.NextHeroComboStats()
                    {
                        HeroId = hero.Id,
                        HeroIdentity = hero.Identity,
                        HeroName = hero.Name,
                        AbilityId = ability.Id,
                        AbilityIdentity = ability.Identity,
                        AbilityName = ability.Name
                    });
                    _dbContext.CurrentHeroComboTrends.Add(new Models.Statistics.CurrentHeroComboStats()
                    {
                        HeroId = hero.Id,
                        HeroIdentity = hero.Identity,
                        HeroName = hero.Name,
                        AbilityId = ability.Id,
                        AbilityIdentity = ability.Identity,
                        AbilityName = ability.Name
                    });
                    _dbContext.PerviousHeroComboTrends.Add(new Models.Statistics.PerviousHeroComboStats()
                    {
                        HeroId = hero.Id,
                        HeroIdentity = hero.Identity,
                        HeroName = hero.Name,
                        AbilityId = ability.Id,
                        AbilityIdentity = ability.Identity,
                        AbilityName = ability.Name
                    });
                }
            }

            _dbContext.SaveChanges();
        }
    }
    
}
