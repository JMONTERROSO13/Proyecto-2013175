using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Proyecto2.Models
{
    public class PagoCredito
    {
        public int Id { get; set; }

        public int TarjetaCreditoCredito { get; set; }
        public int TarjetaCreditoId { get; set; }
        public virtual TarjetaCredito TarjetaCredito { get; set; }

        [StringLength(10, MinimumLength = 4)]
        public string Descripcion { get; set; }
         
        public int Monto { get; set; }
    }
}