using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace KrisnaldoApp.Models
{
    public class Speler
    {
        public Speler ()
        {
            this.SpelerMatchen = new List<SpelerMatch>();
            this.Goals = new List<Goal>();
            this.Assists = new List<Goal>();
        }
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Display(Name="ID")]
        public int SpelerID { get; set; }
        [Display(Name="Voornaam")]
        public string VoorNaam { get; set; }
        [Display(Name = "Familienaam")]
        public string FamilieNaam { get; set; }
        public string Positie { get; set; }
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime GeboorteDatum { get; set; }
        public string Woonplaats { get; set; }
        public virtual List<SpelerMatch> SpelerMatchen { get; set; }
        public virtual List<Goal> Goals { get; set; }
        public virtual List<Goal> Assists { get; set; }
    }
}
