using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Proyecto2.Models
{

    public class CuentaMonetaria
    {
        public int Id { get; set; } 

        [StringLength(10)]
        [RegularExpression(@"^[0-9]+$")]
        public string NombreCuenta { get; set; }
         
        public int Saldo { get; set; }

        public string PersonaDpi { get; set; }
        public Persona Persona { get; set; }

        public virtual ICollection<TarjetaCredito> TarjetasCreditos { get; set; }
        public virtual ICollection<TarjetaDebito> TarjetaDebito { get; set; }
        public virtual ICollection<Prestamo> Prestamos{ get; set; }

    }
}