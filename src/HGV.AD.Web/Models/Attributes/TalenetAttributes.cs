using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HGV.AD.Web.Models.Attributes
{
    public class TalenetAttributes
    {
        public int AbilityId { get; set; }
        public int HeroId { get; set; }

        public string Key { get; set; }
        public string Name { get; set; }
        public int Level { get; set; }
    }
}
