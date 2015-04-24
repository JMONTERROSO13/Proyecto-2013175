using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Proyecto2.Models
{
    public class Persona
    {
        [Required]
        [Key]
        [StringLength(13)]
        [RegularExpression(@"^[0-9]+$", ErrorMessage = "Un DPI Correcto")]
        public string Dpi { get; set; }

        [StringLength(60, MinimumLength = 3)]
        public string Nombre { get; set; }

        public string UserName{ get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        [EmailAddress]
        public string Email { get; set; }

        [StringLength(60, MinimumLength = 3)]
        public string Apellido { get; set; }

        [StringLength(200, MinimumLength = 20)]
        public string Direccion { get; set; }

        [StringLength(10, MinimumLength = 8)]
        [RegularExpression(@"^[0-9]+$", ErrorMessage = "Ingrese un numero telefonico correcto")]
        public string Telefono { get; set; } 

        [StringLength(100, MinimumLength = 10)]
        public string Referencia1 { get; set; }

        [StringLength(10, MinimumLength = 8)]
        [RegularExpression(@"^[0-9]+$", ErrorMessage = "Ingrese un numero telefonico correcto")]
        public string TelefonoReferencia1 { get; set; }

        [StringLength(100, MinimumLength = 10)]
        public string Referencia2 { get; set; }

        [StringLength(12, MinimumLength = 8)]
        [RegularExpression(@"^[0-9]+$", ErrorMessage = "Ingrese un numero telefonico correcto")]
        public string TelefonoReferencia2 { get; set; }

        public virtual ICollection<Cuenta> Cuentas { get; set; }
        public virtual ICollection<CuentaMonetaria> CuentasMonetarias { get; set; }
        public virtual ICollection<CuentaAhorro> CuentasAhorro { get; set; }

         
    }
}