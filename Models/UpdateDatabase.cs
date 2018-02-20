using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace KrisnaldoApp.Models
{
    public class UpdateDatabase

    {
        [Required]
        public string Password { get; set; }
    }
}