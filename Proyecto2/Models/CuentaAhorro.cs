using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Proyecto2.Models
{
    
    public class CuentaAhorro
    {
        public int Id { get; set; }

        [StringLength(7)]
        [RegularExpression(@"^[0-9]+$")]
        public string nombreCuenta { get; set; }
        
        public decimal saldo { get; set; }
         
            public string PersonaDpi { get; set; }
            public Persona Persona { get; set; }

        public virtual ICollection<AbonoAhorro> Abonos { get; set; }
        public virtual ICollection<RetiroAhorro> Retiros { get; set; }
    }
}