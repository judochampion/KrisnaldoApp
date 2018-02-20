using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace KrisnaldoApp.Models
{
    public class Paragraaf
    {

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ParagraafID { get; set; }
        public int MatchID { get; set; }
        public Match Match { get; set; }
        public string Tekst { get; set; }
    }
}
