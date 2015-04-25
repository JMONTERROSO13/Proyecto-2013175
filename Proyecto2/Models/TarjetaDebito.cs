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

        public string NumeroTarjeta { get; set; }
         
        [DisplayFormat(NullDisplayText = "Activa")]
        public Estado? Estado { get; set; }

        [Display(Name = "Credito")]
        public int Credito { get; set; }

        [Display(Name = "Credito Restante")]
        public int CuentaMonetariaSaldo { get; set; } 
        public int CuentaMonetariaId { get; set; }
        public virtual CuentaMonetaria CuentaMonetaria { get; set; }
        
        [StringLength(10, MinimumLength = 4)]
        [RegularExpression(@"^[0-9]+$", ErrorMessage = "Ingrese un PIN correcto")]
        public string Pin { get; set; } 

        public virtual ICollection<AbonoDebito> AbonoDebito { get; set; }
        public virtual ICollection<RetiroDebito> RetiroDebito { get; set; } 
    }
}