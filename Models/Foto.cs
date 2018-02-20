using System.ComponentModel.DataAnnotations.Schema;

namespace KrisnaldoApp.Models
{
    public class Foto
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int FotoID { get; set; }
        public int AlbumID { get; set; }
        public Album Album { get; set; }
        public string RuweBestandsNaam { get; set; }
    }
}