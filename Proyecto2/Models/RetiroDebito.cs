using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Proyecto2.Models
{
    public class RetiroDebito
    {
        public int Id { get; set; }

        public int TarjetaDebitoId { get; set; }
        public int TarjetaDebitoCredito { get; set; }
        public virtual TarjetaDebito TarjetaDebito { get; set; }

        [StringLength(10, MinimumLength = 4)]
        public string Descripcion { get; set; }
         
        public int Monto { get; set; }
    }
}