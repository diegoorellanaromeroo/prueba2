using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Elmonte.Models
{
    [Table("Jueces")]
    public class Juez
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Rut { get; set; }
        public Boolean Sexo { get; set; }
        public string Domicilio { get; set; }
        public List<Condena> Condena { get; set; }
    }
}