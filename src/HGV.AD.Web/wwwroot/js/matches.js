
function PlayerEnityModel(data, heroes, skills, items) {
    var self = this;

    self.personaname = ko.observable(data.personaname ? data.personaname : "Anonymous");
    self.hero_img = ko.observable();

    self.win = ko.observable(data.win);
    self.lose = ko.observable(data.lose);

    self.items = ko.observableArray([]);
    self.abilities = ko.observableArray([]);

    self.kills = ko.observable(data.kills);
    self.deaths = ko.observable(data.deaths);
    self.assists = ko.observable(data.assists);

    self.last_hits = ko.observable(data.last_hits);
    self.denies = ko.observable(data.denies);

    self.gold = ko.observable(data.gold);
    self.gold_per_min = ko.observable(data.gold_per_min);
    self.gold_spent = ko.observable(data.gold_spent);

    self.xp = ko.observable(data.xp);
    self.xp_per_min = ko.observable(data.xp_per_min);

    self.hero_damage = ko.observable(data.hero_damage);
    self.hero_healing = ko.observable(data.hero_healing);

    self.obs_placed = ko.observable(data.obs_placed);
    self.sen_placed = ko.observable(data.sen_placed);
    self.rune_pickups = ko.observable(data.rune_pickups);

    self.neutral_kills = ko.observable(data.neutral_kills);
    self.tower_kills = ko.observable(data.tower_kills);
    self.courier_kills = ko.observable(data.courier_kills);
    self.lane_kills = ko.observable(data.lane_kills);
    self.hero_kills = ko.observable(data.hero_kills);
    self.observer_kills = ko.observable(data.observer_kills);
    self.sentry_kills = ko.observable(data.sentry_kills);
    self.roshan_kills = ko.observable(data.roshan_kills);
    self.necronomicon_kills = ko.observable(data.necronomicon_kills);
    self.ancient_kills = ko.observable(data.ancient_kills);

    self.lane_efficiency = ko.observable(data.lane_efficiency_pct);
    self.lane_role = ko.observable();

    var hero = ko.utils.arrayFirst(heroes, function (h) {
        return data.hero_id == h.heroId;
    });
    self.hero_img("/images/heroes/" + hero.identity + ".png");

    // Abilities
    var ability_upgrades_arr = _.uniq(data.ability_upgrades_arr);
    var upgrades = $.map(ability_upgrades_arr, function (abilityId) {
        var ability = ko.utils.arrayFirst(skills, function (ability) {
            return abilityId == ability.abilityId;
        });
        return {
            name: ability.name,
            ability_img: (ability.identity) ? "/images/spellicons/" + ability.identity + ".png" : "/images/spellicons/talent_tree.png"
        };
    });
    self.abilities(upgrades);

    // Items
    if (data.item_0 === 0) {
        self.items.push({ item_img: "/images/items/emptyitembg.png", name: "None" });
    } else {
        var idenity = items[data.item_0];
        self.items.push({ item_img: "/images/items/" + idenity + ".png", name: "Item" });
    }
    if (data.item_1 === 0) {
        self.items.push({ item_img: "/images/items/emptyitembg.png", name: "None" });
    } else {
        var idenity = items[data.item_1];
        self.items.push({ item_img: "/images/items/" + idenity + ".png", name: "Item" });
    }
    if (data.item_2 === 0) {
        self.items.push({ item_img: "/images/items/emptyitembg.png", name: "None" });
    } else {
        var idenity = items[data.item_2];
        self.items.push({ item_img: "/images/items/" + idenity + ".png", name: "Item" });
    }
    if (data.item_3 === 0) {
        self.items.push({ item_img: "/images/items/emptyitembg.png", name: "None" });
    } else {
        var idenity = items[data.item_3];
        self.items.push({ item_img: "/images/items/" + idenity + ".png", name: "Item" });
    }
    if (data.item_4 === 0) {
        self.items.push({ item_img: "/images/items/emptyitembg.png", name: "None" });
    } else {
        var idenity = items[data.item_4];
        self.items.push({ item_img: "/images/items/" + idenity + ".png", name: "Item" });
    }
    if (data.item_5 === 0) {
        self.items.push({ item_img: "/images/items/emptyitembg.png", name: "None" });
    } else {
        var idenity = items[data.item_5];
        self.items.push({ item_img: "/images/items/" + idenity + ".png", name: "Item" });
    }

    switch (data.lane_role) {

        case 1:
            self.lane_role("Safe");
            break;
        case 2:
            self.lane_role("Mid");
            break;
        case 3:
            self.lane_role("Off");
            break;
        case 4:
            self.lane_role("Jungle");
            break;
        default:
            break;
    }
}

function MatchDetailEnityModel(summary, heroes, skills) {
    var self = this;

    self.match_id = ko.observable();
    self.match_url = ko.observable();

    self.start_time = ko.observable();
    self.duration = ko.observable();

    self.patch = ko.observable();
    self.region = ko.observable();

    self.win_radiant = ko.observable();
    self.win_dire = ko.observable();
    self.score_dire = ko.observable();
    self.score_radiant = ko.observable();

    self.players_radiant = ko.observableArray([]);
    self.players_dire = ko.observableArray([]);

    $.get("https://api.opendota.com/api/matches/" + summary.match_id).done(function (data) {

        var start_date = moment.unix(data.start_time);
        var end_date = moment.unix(data.start_time).add(data.duration, 'seconds');
        var dif = end_date.diff(start_date);
        var dur = moment.duration(dif);

        var pathes = GetPatches();
        var regions = GetRegions();
        var items = GetItems();

        self.match_id(summary.match_id);
        self.match_url("https://www.opendota.com/matches/" + summary.match_id);

        self.start_time(start_date.fromNow());
        self.duration(Math.round(dur.asMinutes()));
        
        self.patch(pathes[data.patch].name);
        self.region(regions[data.region]);

        self.win_radiant(data.radiant_win);
        self.win_dire(!data.radiant_win);
        self.score_dire(data.dire_score);
        self.score_radiant(data.radiant_score);
        
        var players = $.map(data.players, function (i) {
            return new PlayerEnityModel(i, heroes, skills, items);
        });

        self.players_radiant(players.slice(0, 5));
        self.players_dire(players.slice(5));
        
    });
}


function MatchSummaryEntity(data, heroes, skills) {
    var self = this;

    var score = data.radiant_win == true ? (data.player_slot < 5) : (data.player_slot > 5);
    var start_date = moment.unix(data.start_time);
    var end_date = moment.unix(data.start_time).add(data.duration, 'seconds');
    var dif = end_date.diff(start_date);
    var dur = moment.duration(dif);
    var format = Math.round(dur.asMinutes());

    var hero = ko.utils.arrayFirst(heroes, function (hero) {
        return data.hero_id == hero.heroId;
    });

    self.match_id = ko.observable(data.match_id);
    self.match_url = ko.observable("https://www.opendota.com/matches/" + data.match_id);
    self.start_time = ko.observable(start_date.fromNow());
    self.duration = ko.observable(format);
    self.win = ko.observable(score);
    self.lost = ko.observable(!score);
    self.kills = ko.observable(data.kills);
    self.deaths = ko.observable(data.deaths);
    self.assists = ko.observable(data.assists);
    self.hero_img = ko.observable("/images/heroes/" + hero.identity + ".png");

    self.abilities = ko.observableArray([]);

    $.get("https://api.opendota.com/api/matches/" + data.match_id).done(function (match) {
        var player = ko.utils.arrayFirst(match.players, function (player) {
            return AccountId == player.account_id;
        });

        var upgrades = _.uniq(player.ability_upgrades_arr);
        var collection = $.map(upgrades, function (abilityId) {
            var ability = ko.utils.arrayFirst(skills, function (ability) {
                return abilityId == ability.abilityId;
            });
            return {
                name: ability.name,
                ability_img: (ability.identity) ? "/images/spellicons/" + ability.identity + ".png" : "/images/spellicons/talent_tree.png"
            };
        });
        self.abilities(collection);
    });
}

function MatchesViewModel() {
    var self = this;

    self.Loading = ko.observable(true);

    self.Lastest = ko.observable();
    self.Matches = ko.observableArray([]);

    self.Load = function () {
        self.Matches([]);

        var d1 = $.get("https://api.opendota.com/api/players/" + AccountId + "/recentMatches");
        var d2 = $.get("/Matches/GetHeroes");
        var d3 = $.get("/Matches/GetAbilities");
        var d4 = $.get("/Matches/GetTalenets");

        $.when(d1, d2, d3, d4).done(function (v1, v2, v3, v4) {
            var recentMatches = v1[0];
            var heroes = v2[0];
            var abilities = v3[0];
            var talents = v4[0];
            
           
            var skills = abilities.concat(talents);

            var filteredMatches = ko.utils.arrayFilter(recentMatches, function (i) {
                return i.game_mode == 18;
            });

            var lastestMatch = filteredMatches.shift();
            var lastestMatch = filteredMatches.shift();
            var lastest = new MatchDetailEnityModel(lastestMatch, heroes, skills);
            self.Lastest(lastest);

            filteredMatches.unshift(lastestMatch);

            var matches = $.map(filteredMatches, function (i) {
                return new MatchSummaryEntity(i, heroes, skills);
            });

            self.Matches(matches);
            self.Loading(false);
        });

    }

    self.Load();
};

$(document).ready(function () {

    var vm = new MatchesViewModel();
    ko.applyBindings(vm);

});