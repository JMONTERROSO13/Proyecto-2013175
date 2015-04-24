using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Proyecto2.Models
{
    public enum Tipo
    {
        Ahorro, Monetaria
    }

    public class Cuenta
    {
        public int Id { get; set; }

        [StringLength(10, MinimumLength = 7)]
        [RegularExpression(@"^[0-9]+$")]
        public string nombreCuenta { get; set; }
        
        public Tipo? Tipo { get; set; }

        public decimal saldo { get; set; }
        
        public string PersonaDpi { get; set; }
        public Persona Persona { get; set; } 
        
        public virtual ICollection<TarjetaCredito> TarjetasCredito { get; set; }
        public virtual ICollection<TarjetaDebito> TarjetaDebito{ get; set; }
        public virtual ICollection<Prestamo> Prestamos{ get; set; }

    }
}