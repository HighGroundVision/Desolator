using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HGV.AD.Web.Models.Attributes
{
    public class HeroAttributeRank
    {
        public int Id { get; set; }

        public int Index { get; set; }
        public int HeroId { get; set; }
        public string Name { get; set; }
        public double Value { get; set; }
        public int Rank { get; set; }
        public double Min { get; set; }
        public double Max { get; set; }
        public double Avg { get; set; }
        public double Groups { get; set; }
        public double Percentage { get; set; }
    }
}
