
function DraftingViewModel() {
    var self = this;

    // Observables
    self.AvailableDraft = ko.observableArray();
    self.SelectedDraft = ko.observableArray();
    self.FilteredDraft = ko.observableArray();
    self.SearchDraft = ko.observable();

    self.SelectedHeroes = ko.observableArray();
    self.AvailableAbilities = ko.observableArray();

    self.Combos = ko.observableArray();

    self.SelectedHero = ko.observable(null);
    self.SelectedAbility = ko.observable(null);

    self.Searching = ko.observable(false);

    // Computed
    self.FilteredDraft = ko.computed(function () {
        var search = self.SearchDraft();
        if (search) {
            var txt = search.toLowerCase();
            var filtered = ko.utils.arrayFilter(self.AvailableDraft(), function (_) {
                var identity = _.identity.replace("_", " ");
                return identity.includes(txt);
            });

            if (filtered.length == 1) {
                self.SelectDraft(filtered[0]);
            } else {
                return filtered;
            }
        }

        return self.AvailableDraft();
    });

    self.FilteredAbilities = ko.computed(function () {
        return ko.utils.arrayFilter(self.AvailableAbilities(), function (_) {
            return _.ultimate === false;
        });
    });

    self.FilteredUltimateAbilities = ko.computed(function () {
        return ko.utils.arrayFilter(self.AvailableAbilities(), function (_) {
            return _.ultimate === true;
        });
    });

    // Style
    self.GetSelectedHeroStyling = function (item) {
        var entity = ko.utils.arrayFirst(self.SelectedDraft(), function (_) {
            return _.id == item.id;
        });

        if (entity) {
            return "selected-img";
        } else {
            return "";
        }
    }
    self.GetHeroRankStyling = function (item) {
        if (item.percentage < 0.20) {
            return "progress-bar progress-bar-danger";
        } else if (item.percentage > 0.80) {
            return "progress-bar progress-bar-success";
        } else {
            return "progress-bar progress-bar-primary";
        }
    }

    // Functions
    self.SelectDraft = function (item) {
        self.SearchDraft("");

        var entity = ko.utils.arrayFirst(self.SelectedDraft(), function (_) {
            return _.id == item.id;
        });

        if (!entity) {
            self.SelectedDraft.push(item);

            $.get("/Draft/GetHeroSummary/" + item.heroId).done(function (data) {
                self.SelectedHeroes.push(data);
            });

            $.get("/Draft/GetHeroesAbilities/" + item.heroId).done(function (data) {
                $.each(data, function (index, value) {
                    self.AvailableAbilities.push(value);

                    self.IfDraftIsFullThenGetCombos();
                });
            });
        } else {
            self.SelectedDraft.remove(item);

            var hero = ko.utils.arrayFirst(self.SelectedHeroes(), function (_) {
                return _.id == item.heroId;
            });
            self.SelectedHeroes.remove(hero);

            var abilities = ko.utils.arrayFilter(self.AvailableAbilities(), function (_) {
                return _.heroId === item.heroId;
            });

            self.AvailableAbilities.removeAll(abilities);
        }
    }

    self.IfDraftIsFullThenGetCombos = function () {
        if (self.SelectedDraft().length == 12 && self.AvailableAbilities().length == 48) {
            self.Searching(true);

            var ultimateData = $.map(self.SelectedDraft(), function (value, index) { return value.id; });

            new Clipboard('#share-draft', {
                text: function (trigger) {
                    return window.location.origin + window.location.pathname + "?key=" + ultimateData;
                }
            });

            var abilityData = $.map(self.AvailableAbilities(), function (value, index) { return value.id; });
            $.post("/Draft/GetCombosFromPool/", { abilities: abilityData }).done(function (data) {
                self.Searching(false);

                var collection = $.map(data, function (_) { return ConvertToRates(_) });
                self.Combos(collection);

                $('#tblCombos').DataTable({
                    "destroy": true,
                    "scrollX": true,
                    "order": [[4, "desc"]],
                    "pagingType": "simple",
                    "pageLength": 25,
                    "dom": "<'row'<'col-sm-6'i><'col-sm-6'f>><'row'<'col-sm-12'tr>><'row'<'col-sm-6'l><'col-sm-6'p>>",
                });
            });
        }
    }

    self.SelectHero = function (item) {
        if (self.SelectedDraft().length != 12) {
            return;
        }

        var collection = $.map(self.AvailableAbilities(), function (value, index) {
            return value.id;
        });

        $.post("/Draft/GetHeroDetails/" + item.id, { abilities: collection }).done(function (data) {

            data.item3 = ConvertToRates(data.item3);
            data.item4 = $.map(data.item4, function (_) { return ConvertToRates(_) });

            self.SelectedHero(data);

            $("#heroDetails").modal('show');

            setTimeout(function () {
                $('#tblHeroCombos').DataTable({
                    "scrollX": true,
                    "order": [[2, "desc"]],
                    "pagingType": "simple",
                    "dom": "<'row'<'col-sm-6'i><'col-sm-6'f>><'row'<'col-sm-12'tr>><'row'<'col-sm-5'l><'col-sm-7'p>>",
                });
            }, 200);
        });
    }

    self.SelectComboPart1 = function (item) {
        self.GetAbilityDetails(item.abilityId);
    }

    self.SelectComboPart2 = function (item) {
        self.GetAbilityDetails(item.comboId);
    }

    self.SelectAbility = function (item) {
        self.GetAbilityDetails(item.id);
    }

    self.GetAbilityDetails = function (abvilityId) {
        if (self.SelectedDraft().length != 12) {
            return;
        }

        var collection = $.map(self.AvailableAbilities(), function (value, index) {
            return value.id;
        });

        $.post("/Draft/GetAbilityDetails/" + abvilityId, { abilities: collection }).done(function (data) {

            data.item2 = ConvertToRates(data.item2);
            data.item3 = $.map(data.item3, function (_) { return ConvertToRates(_) });

            self.SelectedAbility(data);

            $("#abilityDetails").modal('show');

            setTimeout(function () {
                $('#tblAbilityCombos').DataTable({
                    "scrollX": true,
                    "order": [[2, "desc"]],
                    "pagingType": "simple",
                    "dom": "<'row'<'col-sm-6'i><'col-sm-6'f>><'row'<'col-sm-12'tr>><'row'<'col-sm-5'l><'col-sm-7'p>>",
                });
            }, 200);

        });
    }

    self.Reset = function () {
        window.location = window.location.href.split('?')[0];
    }

    self.AutoDraft = function (key) {
        var abilities = key.split(",");
        var index = 0;

        var interval = setInterval(function () {
            if (index >= abilities.length) {
                clearInterval(interval);
            } else {
                var value = abilities[index++];
                var abilityId = parseInt(value);
                var item = ko.utils.arrayFirst(self.AvailableDraft(), function (_) { return _.id === abilityId; });
                if (item) {
                    self.SelectDraft(item);
                }
            }
        }, 200);
    }

    self.Load = function () {
        $.get("/Draft/GetDraftPool", function (heroes) {
            self.AvailableDraft(heroes);

            var key = url('?key');
            if (key) {
                self.AutoDraft(key);
            }

        });
    }

    self.Load();
};

$(document).ready(function () {

    var vm = new DraftingViewModel();
    ko.applyBindings(vm);

});