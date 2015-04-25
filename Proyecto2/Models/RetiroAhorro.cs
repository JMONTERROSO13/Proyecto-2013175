using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Proyecto2.Models
{
    public class RetiroAhorro
    {
        public int Id { get; set; }
        
        public int CuentaAhorroId { get; set; }
        public int CuentaAhorroSaldo { get; set; }
        public virtual CuentaAhorro CuentaAhorro { get; set; }

        [StringLength(10, MinimumLength = 4)] 
        public string Descripcion { get; set; }

        public int Monto { get; set; }
    }
}