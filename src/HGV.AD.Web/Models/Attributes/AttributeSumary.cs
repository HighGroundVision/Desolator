using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace HGV.AD.Web.Models.Attributes
{
    public class AttributeSumary
    {
        public string Name { get; set; }

        public Expression<Func<HeroAttributes, double>> Expression {get; set;}

        public Expression<Func<HeroAttributes, bool>> Include { get; set; }
    }
}
