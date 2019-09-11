import moment from 'moment'
import numeral from 'numeral'

export default {
  methods: {
    formatDateTime(value) {
      return moment(value).format("MMMM Do");
    },
    formatDuration(start, end) {
      var x = moment(start);
      var y = moment(end);
      var duration = moment.duration(x.diff(y))
      return duration.humanize();
    },
    formatNumber(value) {
      return numeral(value).format('0.00 a').toUpperCase();
    },
    formatDecimal(value) {
      return numeral(value).format('0.0');
    },
    formatPercentage(value) {
      return numeral(value).format('0%');
    },
    formatRank(value) {
      return numeral(value).format('0,0');
    },
  }
};