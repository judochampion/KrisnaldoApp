using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KrisnaldoApp.Models.StatistiekViewModels
{
    public class CompetitieStatistiek
    {

        public Seizoen Seizoen { get; set; }
        public int AantalGespeeldeMatchen { get; set; }
        public int NogTeSpelenMatchen { get; set; }
        public int GespeeldPercentage { get; set; }
        public string AantalMatchenStyleWidthAttribute { get; set; }
        public int AantalMatchenGewonnen { get; set; }
        public string AantalMatchenGewonnenStyleWidthAttribute { get; set; }
        public int AantalMatchenGelijk { get; set; }

        public int AantalGoalsGescoord { get; set; }
        public int AantalTegenGoalsGeIncasseerd { get; set; }
        public string AantalMatchenGelijkStyleWidthAttribute { get; set; }
        public int AantalMatchenVerloren { get; set; }
        public string AantalMatchenVerlorenStyleWidthAttribute { get; set; }
        public static string StyleWidthMaker(int prcntage)
        {
            return "width:" + prcntage + "%";
        }

        public int AantalCleanSheets { get; set; }
        public Dictionary<string,int> dScoorDic { get; set; }
        public Dictionary<string, int> dGespeeldDic { get; set; }
        public Dictionary<string, int> dAssistDic { get; set; }

    }    

    public class CompetitieSeizoenStatistiekSpeler
    {
        public Speler speler { get; set; }
        public int AantalGespeeld { get; set; }

        public string SpelerNaam { get; set; }
        public int AantalGescoord { get; set; }
        public int AantalGeassisteerd { get; set; }
    }
}


