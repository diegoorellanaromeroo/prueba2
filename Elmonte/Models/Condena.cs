using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Elmonte.Models
{
    public class Condena
    {
        public int Id { get; set; }
        public DateTime FechaInicioCondena { get; set; }
        public DateTime FechaCondena { get; set; }
        public int PresoId { get; set; }
        public Preso Preso { get; set; }
        public int JuezId { get; set; }
        public Juez Juez { get; set; }

        public List<CondenaDelito> CondenaDelito { get; set; }
    }
}