using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HGV.AD.Web.Models.DotaApi.GetMatchHistory
{
	public class Root
	{
		public Result result { get; set; }
	}

	public class Result
	{
		public List<Match> matches { get; set; }

		public int status { get; set; }
		public int num_results { get; set; }
		public int total_results { get; set; }
		public int results_remaining { get; set; }
	}

	public class Match
	{
		public long match_id { get; set; }
		public long match_seq_num { get; set; }
		public long start_time { get; set; }

	}
}