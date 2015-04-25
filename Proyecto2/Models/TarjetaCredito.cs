using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Proyecto2.Models
{
    public enum Estado
    {
        bloqueada, activa
    }
    public class TarjetaCredito
    {

        public int Id { get; set; }

        public string NumeroTarjeta { get; set; } 
    
        [DisplayFormat(NullDisplayText = "Activa")]
        public Estado? Estado { get; set; }

        [Display(Name = "Credito")]
        public int Credito { get; set; }

        [Display(Name = "Credito Restante")]
        public int CuentaAhorroSaldo { get; set; }

        public int CuentaMonetariaSaldo { get; set; }
        public int CuentaMonetariaId { get; set; }
        public virtual CuentaMonetaria CuentaMonetaria { get; set; } 


        [StringLength(10, MinimumLength = 4)]
        [RegularExpression(@"^[0-9]+$", ErrorMessage = "Ingrese un PIN correcto")]
        public string Pin { get; set; }

        public virtual ICollection<CargoCredito> Cargos { get; set; }
        public virtual ICollection<PagoCredito> Pagos { get; set; } 

    }
}