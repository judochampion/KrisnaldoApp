using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KrisnaldoApp.Models
{
    public class SpelerMatch
    {

        public int SpelerID { get; set; }
        public Speler Speler { get; set; }
        public int MatchID { get; set; }
        public Match Match { get; set; }
    }
}
