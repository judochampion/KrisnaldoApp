using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KrisnaldoApp.Models.SpelerViewModels
{
    public class SpelerIndividueelStatistiek
    {

        public Speler Speler { get; set; }
        public int AantalGespeeldeMatchen { get; set; }
        public int GespeeldPercentage { get; set; }
        public List<Match> MatchenWaarHijHeeftAanMeegedaan { get; set; }

        public List<string> SeizoenenWaarinHijVoorkomt { get; set; }

        public int AantalGescoordeGoals { get; set; }

        public Dictionary<string, object[] > QuotesUitVerslagen { get; set; }

        
    }    
    
}


