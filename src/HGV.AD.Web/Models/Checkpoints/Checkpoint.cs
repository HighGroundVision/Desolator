using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace HGV.AD.Web.Models.Checkpoints
{
    public class Checkpoint
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Key]
        public long MatchId { get; set; }

        public long MatchNumber { get; set; }

        public DateTime MatchDate { get; set; }
    }
}