using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Elmonte.Models
{
    [Table("Delitos")]
    public class Delito
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public int CondenaMinima { get; set; }
        public int CondenaMaxima { get; set; }
        public List<CondenaDelito> CondenaDelitoList { get; set; }
    }
}