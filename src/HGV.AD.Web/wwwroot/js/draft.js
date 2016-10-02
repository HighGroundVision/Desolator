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

    self.IfDraftIsFullThenGetCombos = function() {
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

                self.Combos(data);

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

    self.SelectHero = function (item, element) {
        if (self.SelectedDraft().length != 12) {
            return;
        }

        var collection = $.map(self.AvailableAbilities(), function (value, index) {
            return value.id;
        });

        $.post("/Draft/GetHeroDetails/" + item.id, { abilities: collection }).done(function (data) {
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

    self.SelectAbility = function (item, element) {
        if (self.SelectedDraft().length != 12) {
            return;
        }

        var collection = $.map(self.AvailableAbilities(), function (value, index) {
            return value.id;
        });

        $.post("/Draft/GetAbilityDetails/" + item.id, { abilities: collection }).done(function (data) {
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

        self.SearchDraft("");
        self.SelectedDraft([]);
        self.SelectedHeroes([]);
        self.AvailableAbilities([]);
        self.SelectedAbility(null);
        self.SelectedAbility(null);
        self.Combos([]);
    }

    self.Load = function () {
        $.get("/Draft/GetDraftPool", function (heroes) {
            self.AvailableDraft(heroes);
            
            var key = url('?key');
            if (key) {
                var abilities = url('?key').split(",");
                $.each(abilities, function (index, value) {
                    var abilityId = parseInt(value);
                    var item = ko.utils.arrayFirst(self.AvailableDraft(), function (_) { return _.id === abilityId; });
                    if (item) {
                        self.SelectDraft(item);
                    }
                });
            }
            
        });
    }

    self.Load();
}

$(document).ready(function () {

    var vm = new DraftingViewModel();
    ko.applyBindings(vm);

});