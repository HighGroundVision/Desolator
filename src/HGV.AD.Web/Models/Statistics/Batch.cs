using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HGV.AD.Web.Models.Statistics
{
    public class Batch
    {
        public int Id { get; set; }

        public DateTime DateProcessed { get; set; }

        public long SquenceStart { get; set; }

        public long SquenceEnd { get; set; }

        public int NumberOfMatches { get; set; }
        
        public int NumberOfAD { get; set; }
    }
}
