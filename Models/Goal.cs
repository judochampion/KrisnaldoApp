using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace KrisnaldoApp.Models
{
    public class Goal
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int GoalID { get; set; }
        public int ScorerSpelerID { get; set; }
        public Speler ScorerSpeler { get; set; }
        public bool HasAssist { get; set; }
        public int? AssistSpelerID { get; set; }
        public Speler AssistSpeler { get; set; }
        public int MatchID { get; set; } 
        public Match Match { get; set; }
    }
}
