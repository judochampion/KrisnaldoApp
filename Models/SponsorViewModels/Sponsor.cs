using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace KrisnaldoApp.SponsorViewModels
{
    public class Sponsor
    {
        public string DisplayNaam { get; set;}
        public string RuwePictureNaam { get; set; }
        public string Link { get; set; }

    }
}
