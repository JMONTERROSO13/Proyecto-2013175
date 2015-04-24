using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Proyecto2.Models
{
    public class TarjetaDebito
    {
        public int Id { get; set; }

        public string numeroTarjeta { get; set; }

        //       public Estado Estado { get; set; }

        [Display(Name = "Credito")]
        public decimal credito { get; set; }

        [Display(Name = "Credito Restante")]
        public decimal usoCredito { get; set; }

        // public int CuentaId { get; set; }
        // public virtual Cuenta Cuenta { get; set; }

        public int CuentaMonetariaId { get; set; }
        public virtual CuentaMonetaria CuentaMonetaria { get; set; }


        [StringLength(10, MinimumLength = 4)]
        [RegularExpression(@"^[0-9]+$", ErrorMessage = "Ingrese un PIN correcto")]
        public string Pin { get; set; } 

        public virtual ICollection<AbonoDebito> Abono { get; set; }
        public virtual ICollection<RetiroDebito> Retiro { get; set; } 
    }
}