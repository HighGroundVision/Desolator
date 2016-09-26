using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace HGV.AD.Web.Models.Attributes
{
	public class AbilityAttributes
	{
		public int AbilityId { get; set; }
		public string Identity { get; set; }
		public int HeroId { get; set; }

		public string Name { get; set; }
		public string Description { get; set; }

		public string Html { get; set; }

		public bool Ultimate { get; set; }

		public string Target { get; set; }
		public string DamageType { get; set; }
		public string AffectsWhom { get; set; }

		public string Keywords { get; set; }
	}
}
