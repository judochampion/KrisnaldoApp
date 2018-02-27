using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace KrisnaldoApp.Models
{
    public class Match
    {

        public Match()
        {
            this.SpelerMatchen = new List<SpelerMatch>();
            this.Goals = new List<Goal>();
            this.Paragrafen = new List<Paragraaf>();
        }
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int MatchID { get; set; }
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime Datum { get; set; }
        public Rol Rol { get; set; }
        public Type Type { get; set; }
        public int OnzeScore { get; set; }
        public int HunScore { get; set; }
        public string Tegenstander { get; set; }
        public string LinkNaam { get; set; }
        public int SeizoenID { get; set; }
        public Seizoen Seizoen { get; set; }
        public virtual List<SpelerMatch> SpelerMatchen { get; set; }
        public virtual List<Goal> Goals { get; set; }
        public virtual List<Paragraaf> Paragrafen { get; set; }


}
    public enum Rol
    {

        [Display(Name = "Thuismatch")]
        THUIS,
        [Display(Name = "Uitmatch")]
        UIT
    }

    public enum Type
    {
        [Display(Name = "Competitie")]
        COMPETITIE,
        [Display(Name = "Beker")]
        BEKER,
        [Display(Name = "Vriendschappelijk")]
        VRIENDSCHAPPELIJK,
        [Display(Name = "Tornooi")]
        TORNOOI
    }


}
