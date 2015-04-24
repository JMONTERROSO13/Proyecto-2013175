using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Proyecto2.Models
{
    public class AbonoDebito
    {
        public int Id { get; set; }

        public int TarjetaDebitoId { get; set; }
        public virtual TarjetaDebito TarjetaDebito { get; set; }

        [StringLength(10, MinimumLength = 4)]
        public string Descripcion { get; set; } 

        public decimal Monto { get; set; }

    }
}