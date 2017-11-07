$(document).ready(function () {
    $.get("/data/draftpool.json", function (pool) {

        for (var h = 0; h < pool.length ; h++) {
            var hero = pool[h];

            var row = $('<tr>');

            var heroImg = $('<img>').attr('src', hero.img).attr('title', hero.name).css("height", "90");
            if (!hero.enabled) {
                heroImg.addClass("disabled");
            }
            row.append($('<td>').append(heroImg));

            for (var a = 0; a < hero.abilities.length; a++) {
                var ability = hero.abilities[a];

                var abilityImg = $('<img>').attr('src', ability.img).attr('title', ability.name);
                if (!ability.enabled) {
                    abilityImg.addClass("disabled");
                }
                row.append($('<td>').append(abilityImg));
            }

            $('#roaster').find('tbody').append(row);
        }
        

    });   
});