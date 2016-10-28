function ConvertToRates(data) {
    var self = this;

    var total = data.wins + data.loses;
    data.wins = Math.round((data.wins / total) * 100);
    data.loses = Math.round((data.loses / total) * 100);
    data.total = total;

    return data;
};