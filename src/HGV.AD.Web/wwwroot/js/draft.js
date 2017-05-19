
function DraftingViewModel() {
    var self = this;

    // Observables
    self.AvailableDraft = ko.observableArray();
    self.SelectedDraft = ko.observableArray();
    self.SearchDraft = ko.observable();

    self.IsDrafting = ko.computed(function () {
        return self.SelectedDraft().length != 12;
    });

    self.IsDraftFull = ko.computed(function () {
        return self.SelectedDraft().length == 12;
    });

    self.TeamRadiant = ko.computed(function () {
        return self.SelectedDraft().slice(0, 5);
    });

    self.TeamDire = ko.computed(function () {
        return self.SelectedDraft().slice(5, 10);
    });

    self.GetSelectedStyling = function (item) {
        var search = self.SearchDraft();
        var selected = self.SelectedDraft();

        var entity = ko.utils.arrayFirst(selected, function (_) {
            return _.abilityId == item.abilityId;
        });

        if (entity) {
            return "selected-img";
        } else {
            if (!search || !item.identity.includes(search)) {
                return "";
            }

            return "searched-img";
        }
    }

    self.SelectAbility = function (item) {
        self.SearchDraft("");

        var entity = ko.utils.arrayFirst(self.SelectedDraft(), function (_) {
            return _.abilityId == item.abilityId;
        });

        if (!entity) {
            self.SelectedDraft.push(item);
        } else {
            self.SelectedDraft.remove(item);
        }
    }

    self.AutoDraft = function (key) {
        var abilities = key.split(",");
        $.each(abilities, function (index, value) {
            var entity = ko.utils.arrayFirst(self.AvailableDraft(), function (_) {
                return _.abilityId == value;
            });
            self.SelectedDraft.push(entity);
        });
    }

    self.SelectHero = function (item) {
        var draft = $.map(self.SelectedDraft(), function (_) { return _.abilityId; });
        window.location = "/Draft/Recommendations/?heroId=" + item.heroId + "&draft=" + draft.join();
    }

    self.Reset = function () {
        window.location = "/Draft";
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