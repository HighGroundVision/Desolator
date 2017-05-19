
function DraftingViewModel() {
    var self = this;

    self.Loading = ko.observable(true);
    self.Hero = ko.observable();
    self.Talenets = ko.observableArray();
    self.TalenetGrouped = ko.observable();
    self.AttributesGood = ko.observableArray([]);
    self.AttributesBad = ko.observableArray([]);

    self.Pool = ko.observableArray([]);
    self.ComboTrends = ko.observableArray([]);

    self.MostPicked = ko.observableArray([]);
    self.LeastPicked = ko.observableArray([]);
    self.MostWins = ko.observableArray([]);
    self.MostKills = ko.observableArray([]);
    self.MostLinked = ko.observableArray([]);

    self.SelectedAbility = ko.observable(null);
    self.SelectedCombos = ko.observableArray([]);


    self.Abilities = ko.computed(function () {
        return ko.utils.arrayFilter(self.Pool(), function (i) { return i.ultimate == false; });
    });

    self.Ultimates = ko.computed(function () {
        return ko.utils.arrayFilter(self.Pool(), function (i) { return i.ultimate == true; });
    });

    self.isHero = function (item) {
        return item.heroId == self.Hero().heroId;
    }

    self.hasTalents = function (item) {
        return _.some(self.Talenets(), function (i) { return i.name.includes(item.name); });
    }

    self.isMostPicked = function (item) {
        return _.some(self.MostPicked(), function (i) { return i.abilityId == item.abilityId; });
    }

    self.isLeastPicked = function (item) {
        return _.some(self.LeastPicked(), function (i) { return i.abilityId == item.abilityId; });
    }

    self.isMostWins = function (item) {
        return _.some(self.MostWins(), function (i) { return i.abilityId == item.abilityId; });
    }

    self.isMostKills = function (item) {
        return _.some(self.MostKills(), function (i) { return i.abilityId == item.abilityId; });
    }

    self.isLinked = function (item) {
        return _.some(self.MostLinked(), function (i) { return i == item.abilityId; });
    }

    self.isUpgradable = function (item) {
        return item.keywords ? item.keywords.includes("Upgradable") : false;
    }

    self.getStyleForHeroStatRanks = function (item) {
        var value = item.percentage;
        if (value >= 0.80) {
            return "progress-bar-success";
        }
        if (value >= 0.50) {
            return "progress-bar-info";
        }
        if (value >= 0.20) {
            return "progress-bar-warning";
        }

        return "progress-bar-danger";
    }

    self.getStyleForSelectedAbility = function (item) {
        var selected = self.SelectedAbility();
        if (!selected) {
            return "";
        }

        if (selected.abilityId != item.abilityId) {
            return "";
        }

        return "selected-img";
    }


    self.getSelectedProgress = function (item) {
        var selected = self.SelectedAbility();
        if (!selected) {
            return -1;
        }
        if (selected.abilityId == item.abilityId) {
            return -1;
        }

        var combos = self.SelectedCombos();
        var total = _.max(combos, function (i) { return i.wins; });
        var entity = ko.utils.arrayFirst(combos, function (i) { return i.abilityId == selected.abilityId && i.comboId == item.abilityId; });
        if (!entity)
            return -1;

        var value = (entity.wins / total.wins) * 100;
        if (value < 10)
            return 0;

        return value;
    }

    self.SelectAbility = function (item) {
        var selected = self.SelectedAbility();
        if (selected) {
            if (selected.abilityId == item.abilityId) {
                self.SelectedAbility(null);
                self.SelectedCombos([]);
                return;
            }
        }

        var trends = self.ComboTrends();
        var combos = ko.utils.arrayFilter(trends, function (i) { return i.abilityId == item.abilityId || i.comboId == item.abilityId ; });
        self.SelectedCombos(combos);
        self.SelectedAbility(item);

    }

    self.Reset = function () {
        window.location = "/Draft";
    }

    self.Load = function () {
        var heroId = url('?heroId');
        var draft = url('?draft');

        var d1 = $.get("/Draft/GetHeroDetails/?heroId=" + heroId);
        var d2 = $.get("/Draft/GetAbilityPoolFromDraft/?draft=" + draft);
        var d3 = $.get("/Draft/GetAbilityTrendsFromDraft/?draft=" + draft);
        var d4 = $.get("/Draft/GetComboTrendsFromDraft/?draft=" + draft);

        $.when(d1, d2, d3, d4).done(function (v1, v2, v3, v4) {
            var data = v1[0];
            var abilities = v2[0];
            var ability_trends = v3[0];
            var combo_trends = v4[0];

            var most_picks = _.sortBy(ability_trends, function (i) { return i.total; }).reverse().slice(0, 10);
            var least_picks = _.sortBy(ability_trends, function (i) { return i.total; }).slice(0, 5);
            var most_wins = _.sortBy(ability_trends, function (i) { return i.wins; }).reverse().slice(0, 10);
            var most_kills = _.sortBy(ability_trends, function (i) { return i.kills; }).reverse().slice(0, 10);

            self.MostPicked(most_picks);
            self.LeastPicked(least_picks);
            self.MostWins(most_wins);
            self.MostKills(most_kills);
    
            var collection = _.chain(combo_trends)
                .sortBy('wins')
                .reverse()
                .slice(0, 100)
                .map(function (i) { return [i.abilityId, i.comboId]; })
                .flatten()
                .countBy(function (i) { return i; })
                .map(function (i, key) { return { abilityId: key, count: i }; })
                .sortBy('count')
                .map(function (i) { return i.abilityId })
                .reverse()
                .slice(0, 10)
                .value();

            self.MostLinked(collection);

            self.Hero(data.item1);

            self.Talenets(data.item2);
            self.TalenetGrouped(_.groupBy(data.item2, 'level'));

            self.AttributesGood(data.item3);
            self.AttributesBad(data.item4);

            self.Pool(abilities);

            self.ComboTrends(combo_trends);

            self.Loading(false);
        });
    }

    self.Load();
};

$(document).ready(function () {

    var vm = new DraftingViewModel();
    ko.applyBindings(vm);

});