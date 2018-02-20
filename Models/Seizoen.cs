using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace KrisnaldoApp.Models
{
    public class Seizoen
    {

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int SeizoenID { get; set; }        
        public string SeizoenNaam { get; set; }
        public DateTime StartDatum { get; set; }
        public DateTime EindDatum { get; set; }
        public virtual List<Match> Matchen { get; set; }
        
    }
}
