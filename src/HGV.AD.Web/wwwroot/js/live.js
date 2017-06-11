
function DraftingViewModel() {
    var self = this;

    self.Loading = ko.observable(true);

    // Hero
    self.Hero = ko.observable();

    // Talenets
    self.Talenets = ko.observableArray();
    self.TalenetGrouped = ko.observable();
    self.getStyleForTalenets = function (key) {
        return key.includes("unique") ? "info" : "";
    }

    // Attributes
    self.AttributesGood = ko.observableArray([]);
    self.AttributesBad = ko.observableArray([]);
    self.getStyleForHeroStatRanks = function (item) {
        var value = item.percentage;
        if (value >= 0.60) {
            return "progress-bar-success";
        } else if (value >= 0.40) {
            return "progress-bar-warning";
        } else {
            return "progress-bar-danger";
        }
    }

    // Ability Pool
    self.Pool = ko.observableArray([]);
    self.Abilities = ko.computed(function () {
        return ko.utils.arrayFilter(self.Pool(), function (i) { return i.ultimate == false; });
    });

    self.Ultimates = ko.computed(function () {
        return ko.utils.arrayFilter(self.Pool(), function (i) { return i.ultimate == true; });
    });

    // Trends
    self.ComboTrends = ko.observableArray([]);

    self.AbilityTrends = ko.observableArray([]);
    self.SelectedTrend = ko.observable(0);
    self.SelectTrend = function (index) {
        var current = self.SelectedTrend();
        if (current == index) {
            self.SelectedTrend(0);
        } else {
            self.SelectedTrend(index);
        }
    }
    self.getStyleForSelectedTrend = function (index) {
        var current = self.SelectedTrend();
        if (current == index) {
            return "picked-trend";
        } else {
            return "";
        }
    }
    self.getFilterForSelectedTrend = function (item) {

        var takenCollection = self.TakenAbilities();
        var taken = ko.utils.arrayFirst(takenCollection, function (i) { return i.abilityId == item.abilityId; });
        if (taken)
            return 100;

        if (item.ultimate == true) {
            var pickedCollection = self.PickedAbilities();
            var picked = ko.utils.arrayFirst(pickedCollection, function (i) { return i.ultimate == true && i.abilityId != item.abilityId; });
            if (picked)
                return 100;
        }

        var current = self.SelectedTrend();
        if (current == 0) {
            return 0;
        } else if (current == 1) {
            return _.some(self.Talenets(), function (i) { return i.name.toLowerCase().includes(item.name.toLowerCase()); }) == true ? 0 : 100;
        } else if (current > 1) {
            if (item.keywords == null) {
                return 100;
            }

            switch (current) {
                case 2:
                    return item.keywords.includes("Upgradable") == true ? 0 : 100;
                case 3:
                    return item.keywords.includes("Stun") == true ? 0 : 100;
                case 4:
                    return item.keywords.includes("Slow") == true ? 0 : 100;
                case 5:
                    return item.keywords.includes("Speed") == true ? 0 : 100;
                case 6:
                    return item.keywords.includes("Disable") == true ? 0 : 100;
                case 7:
                    return item.keywords.includes("Heal") == true ? 0 : 100;
                case 8:
                    return item.keywords.includes("Tank") == true ? 0 : 100;
                case 9:
                    return item.keywords.includes("Nuke") == true ? 0 : 100;
                case 10: 
                    return item.keywords.includes("Passive") == true ? 0 : 100;
                case 11:
                    return item.keywords.includes("Attack") == true ? 0 : 100;
                default:
                    return 0;
            }
        }
    }

    self.SelectedAbility = ko.observable(null);
    self.SelectAbility = function (item) {
        var current = self.SelectedAbility();
        if (current) {
            if (current.abilityId == item.abilityId) {
                self.SelectedAbility(null);
            } else {
                self.SelectedAbility(item);
            }
        } else {
            self.SelectedAbility(item);
        }
    }
    self.getStyleForSelectedAbility = function (item) {       
        var picks = self.PickedAbilities();
        if (picks.length > 0) {
            if (picks[0])
                if (picks[0].abilityId == item.abilityId)
                    return "picked-self-one-img";

            if (picks[1])
                if (picks[1].abilityId == item.abilityId)
                    return "picked-self-two-img";

            if (picks[2])
                if (picks[2].abilityId == item.abilityId)
                    return "picked-self-three-img";
        }

        var current = self.SelectedAbility();
        if (current)
            if (current.abilityId == item.abilityId)
                return "selected-img";

    }
    self.getWinRateOfSelectForProgress = function (item) {

        var selected = self.SelectedAbility();
        if (selected) {
            
            var combos = self.ComboTrends();
            var filtered = ko.utils.arrayFilter(combos, function (i) {
                return i.abilityId == selected.abilityId || i.comboId == selected.abilityId;
            });

            var max = _.max(filtered, function (i) { return i.wins; });
            var entity = ko.utils.arrayFirst(filtered, function (i) { return i.abilityId == selected.abilityId && i.comboId == item.abilityId; });
            if (!entity)
                return 100;

            var value = (entity.wins / max.wins) * 100;
            return value;
        } else {
            var combos = self.AbilityTrends();
            var max = _.max(combos, function (i) { return i.wins; });
            var entity = ko.utils.arrayFirst(combos, function (i) { return i.abilityId == item.abilityId; });
            if (!entity)
                return 100;

            var value = (entity.wins / max.wins) * 100;
            return value;
        }
    }

    self.getWinRateOfPick1ForProgress = function (item) {
        var picks = self.PickedAbilities();
        if (picks.length > 0) {
            var combos = self.ComboTrends();
            var filtered = ko.utils.arrayFilter(combos, function (i) {
                return i.abilityId == picks[0].abilityId || i.comboId == item.abilityId;
            });

            var max = _.max(filtered, function (i) { return i.wins; });
            var entity = ko.utils.arrayFirst(filtered, function (i) { return i.abilityId == picks[0].abilityId && i.comboId == item.abilityId; });
            if (!entity)
                return 0;

            var value = (entity.wins / max.wins) * 100;
            return value;

        } else {
            return 0;
        }
    }

    self.getWinRateOfPick2ForProgress = function (item) {
        var picks = self.PickedAbilities();
        if (picks.length > 1) {

            var combos = self.ComboTrends();
            var filtered = ko.utils.arrayFilter(combos, function (i) {
                return i.abilityId == picks[1].abilityId || i.comboId == item.abilityId;
            });

            var max = _.max(filtered, function (i) { return i.wins; });
            var entity = ko.utils.arrayFirst(filtered, function (i) { return i.abilityId == picks[1].abilityId && i.comboId == item.abilityId; });
            if (!entity)
                return 0;

            var value = (entity.wins / max.wins) * 100;
            return value;

        } else {
            return 0;
        }
    }

    self.getWinRateOfPick3ForProgress = function (item) {
        var picks = self.PickedAbilities();
        if (picks.length > 2) {

            var combos = self.ComboTrends();
            var filtered = ko.utils.arrayFilter(combos, function (i) {
                return i.abilityId == picks[2].abilityId || i.comboId == item.abilityId;
            });

            var max = _.max(filtered, function (i) { return i.wins; });
            var entity = ko.utils.arrayFirst(filtered, function (i) { return i.abilityId == picks[2].abilityId && i.comboId == item.abilityId; });
            if (!entity)
                return 0;

            var value = (entity.wins / max.wins) * 100;
            return value;

        } else {
            return 0;
        }
    }


    // Pick Ability
    self.PickedAbilities = ko.observableArray([]);
    self.PickAbility = function (item) {
        self.SelectedTrend(0);

        var picked = self.PickedAbilities();
        var entity = ko.utils.arrayFirst(picked, function (i) { return i.abilityId == item.abilityId; });
        if (entity) {
            self.PickedAbilities.remove(item);
        } else {
            if (picked.length < 3) {
                self.PickedAbilities.push(item);
            }
        }
    }

    self.hasFirstPick = ko.computed(function () {
        var picks = self.PickedAbilities();
        if (picks.length > 0) {
            return true;
        } else {
            return false;
        }
    });
    self.hasSecondPick = ko.computed(function () {
        var picks = self.PickedAbilities();
        if (picks.length > 1) {
            return true;
        } else {
            return false;
        }
    });
    self.hasThridPick = ko.computed(function () {
        var picks = self.PickedAbilities();
        if (picks.length > 2) {
            return true;
        } else {
            return false;
        }
    });

    // Taken Ability
    self.TakenAbilities = ko.observableArray([]);
    self.TakenAbility = function (item) {
        var picked = self.PickedAbilities();
        var entityPicked = ko.utils.arrayFirst(picked, function (i) { return i.abilityId == item.abilityId; });
        if (entityPicked) {
            return;
        }

        var taken = self.TakenAbilities();
        var entityTaken = ko.utils.arrayFirst(taken, function (i) { return i.abilityId == item.abilityId; });
        if (entityTaken) {
            self.TakenAbilities.remove(item);
        } else {
            self.TakenAbilities.push(item);
        }
    }

    self.openAbilityDetails = function (item) {
        window.open("/Abilities/Details/" + item.abilityId);
    }
    

    self.Load = function () {
        var heroId = url('?heroId');
        var draft = url('?draft');

        var d1 = $.get("/Draft/GetHeroDetails/?heroId=" + heroId);
        var d2 = $.get("/Draft/GetAbilityPoolFromDraft/?draft=" + draft);
        var d3 = $.get("/Draft/GetAbilityTrendsFromDraft/?heroId=" + heroId + "&draft=" + draft);
        var d4 = $.get("/Draft/GetComboTrendsFromDraft/?draft=" + draft);

        $.when(d1, d2, d3, d4).done(function (v1, v2, v3, v4) {
            var data = v1[0];
            var abilities = v2[0];
            var ability_trends = v3[0];
            var combo_trends = v4[0];

            self.Hero(data.item1);

            self.Talenets(data.item2);
            self.TalenetGrouped(_.groupBy(data.item2, 'level'));

            self.AttributesGood(data.item3);
            self.AttributesBad(data.item4);

            self.Pool(abilities);

            self.AbilityTrends(ability_trends);
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