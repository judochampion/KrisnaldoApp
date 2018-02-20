using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace KrisnaldoApp.Models
{
    public class Album
    {

        public Album()
        {
            this.Fotos = new List<Foto>();
        }
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int AlbumID { get; set; }

        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime Datum { get; set; }
        public string DisplayNaam { get; set;}
        public string RuweNaam { get; set; }
        public string Intro { get; set; }
        public virtual List <Foto> Fotos { get; set; }

}

}
