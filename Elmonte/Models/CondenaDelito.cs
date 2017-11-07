using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Elmonte.Models
{
    [Table("CondenaDelitos")]
    public class CondenaDelito
    {
        public int Id { get; set; }
        public int CondenaId { get; set; }
        public int DelitoId { get; set; }
        public int Condena { get; set; }

        public Condena Condenas { get; set; }
        public Delito Delito { get; set; }
    }
}