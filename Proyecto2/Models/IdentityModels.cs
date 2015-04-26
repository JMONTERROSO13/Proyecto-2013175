using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Proyecto2.Models
{
    public class Users : IdentityUser
    {

        [DisplayFormat(NullDisplayText = "Admin")]
        public string PersonaDpi { get; set; }
        public Persona Persona { get; set; } 
    
        public virtual ICollection<Cuenta> Cuentas { get; set; }
        public virtual ICollection<CuentaAhorro> CuentasAhorro { get; set; }
        public virtual ICollection<CuentaMonetaria> CuentasMonetarias { get; set; }

    }


    public class ApplicationDbContext : IdentityDbContext<Users>
    {
        public ApplicationDbContext()
            : base("DefaultConnection")
        {

        }

        public System.Data.Entity.DbSet<Proyecto2.Models.Persona> Personas { get; set; }

        public System.Data.Entity.DbSet<Proyecto2.Models.Cuenta> Cuentas { get; set; }

        public System.Data.Entity.DbSet<Proyecto2.Models.CuentaAhorro> CuentaAhorroes { get; set; }

        public System.Data.Entity.DbSet<Proyecto2.Models.CuentaMonetaria> CuentaMonetarias { get; set; }

        public System.Data.Entity.DbSet<Proyecto2.Models.Prestamo> Prestamoes { get; set; }

        public System.Data.Entity.DbSet<Proyecto2.Models.TarjetaCredito> TarjetaCreditoes { get; set; }

        public System.Data.Entity.DbSet<Proyecto2.Models.TarjetaDebito> TarjetaDebitoes { get; set; }
    }


    public class IdentityManager
    {
        public bool RoleExists(string name)
        {
            var rm = new RoleManager<IdentityRole>(
                new RoleStore<IdentityRole>(new ApplicationDbContext()));
            return rm.RoleExists(name);
        }


        public bool CreateRole(string name)
        {
            var rm = new RoleManager<IdentityRole>(
                new RoleStore<IdentityRole>(new ApplicationDbContext()));
            var idResult = rm.Create(new IdentityRole(name));
            return idResult.Succeeded;
        }


        public bool CreateUser(Users user, string password)
        {
            var um = new UserManager<Users>(
                new UserStore<Users>(new ApplicationDbContext()));
            var idResult = um.Create(user, password);
            return idResult.Succeeded;
        }


        public bool AddUserToRole(string userId, string roleName)
        {
            var um = new UserManager<Users>(
                new UserStore<Users>(new ApplicationDbContext()));
            var idResult = um.AddToRole(userId, roleName);
            return idResult.Succeeded;
        }


        public void ClearUserRoles(string userId)
        {
            var um = new UserManager<Users>(
                new UserStore<Users>(new ApplicationDbContext()));
            var user = um.FindById(userId);
            var currentRoles = new List<IdentityUserRole>();
            currentRoles.AddRange(user.Roles);
            foreach (var role in currentRoles)
            {
                um.RemoveFromRole(userId, role.Role.Name);
            }
        }
    }
}