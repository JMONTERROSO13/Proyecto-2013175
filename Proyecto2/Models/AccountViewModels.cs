using Microsoft.AspNet.Identity.EntityFramework;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Proyecto2.Models
{
    public class ManageUserViewModel
    {
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Contraseña Actual")]
        public string OldPassword { get; set; }

        [Required]
        [StringLength(100, ErrorMessage =
            "La {0} debe tener al menos {2} carácteres.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Nueva Contraseña")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirmar Nueva Contraseña")]
        [Compare("NewPassword", ErrorMessage =
            "La nueva contraseña y la confirmación de contraseña no coinciden")]
        public string ConfirmPassword { get; set; }
    }


    public class LoginViewModel
    {
        [Required]
        [Display(Name = "Nombre De Usuario")]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Contraseña")]
        public string Password { get; set; }

        [Display(Name = "Recuerdame?")]
        public bool RememberMe { get; set; }
    }


    public class RegisterViewModel
    {
        [Required]
        [Display(Name = "Nombre De Usuario")]
        public string UserName { get; set; }

        [Required]
        [StringLength(100, ErrorMessage =
            "La {0} debe tener al menos {2} carácteres.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Contraseña")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirmar  Contraseña")]
        [Compare("Password", ErrorMessage =
            "La nueva contraseña y la confirmación de contraseña no coinciden")]
        public string ConfirmPassword { get; set; }

        // New Fields added to extend Application User class:
        [StringLength(13)]
        [Display(Name = "Identificación Personal(DPI)")]
        [RegularExpression(@"^[0-9]+$", ErrorMessage = "Ingrese un DPI correcto")]
        public string PersonaDpi { get; set; }

        public Persona Persona { get; set; }

        // Return a pre-poulated instance of AppliationUser:
        public Users GetUser()
        {
            var user = new Users()
            {
                UserName = this.UserName,
                PersonaDpi = this.PersonaDpi,
                Persona = this.Persona
            };
            return user;
        }
    }


    public class EditUserViewModel
    {
        public EditUserViewModel() { }

        // Allow Initialization with an instance of Users:
        public EditUserViewModel(Users user)
        {
                this.UserName = user.UserName;
        }
        
        [Required]
        [Display(Name = "Nombre De Usuario")]
        public string UserName { get; set; }

    }
    public class IndexView
    {
        public IndexView() { }

        // Allow Initialization with an instance of Users:
        public IndexView(Users user)
        {
            this.UserName = user.UserName;
            this.PersonaDpi  = user.PersonaDpi;
            
        }

        [Display(Name = "Nombre De Usuario")]
        public string UserName { get; set; }

        public string PersonaDpi { get; set; }

    }

    public class SelectUserRolesViewModel
    {
        public SelectUserRolesViewModel()
        {
            this.Roles = new List<SelectRoleEditorViewModel>();
        }


        // Enable initialization with an instance of Users:
        public SelectUserRolesViewModel(Users user)
            : this()
        {
            this.UserName = user.UserName;
        

            var Db = new ApplicationDbContext();

            // Add all available roles to the list of EditorViewModels:
            var allRoles = Db.Roles;
            foreach (var role in allRoles)
            {
                // An EditorViewModel will be used by Editor Template:
                var rvm = new SelectRoleEditorViewModel(role);
                this.Roles.Add(rvm);
            }

            // Set the Selected property to true for those roles for 
            // which the current user is a member:
            foreach (var userRole in user.Roles)
            {
                var checkUserRole =
                    this.Roles.Find(r => r.RoleName == userRole.Role.Name);
                checkUserRole.Selected = true;
            }
        }
        public string UserName { get; set; }
        public List<SelectRoleEditorViewModel> Roles { get; set; }
    }

    // Used to display a single role with a checkbox, within a list structure:
    public class SelectRoleEditorViewModel
    {
        public SelectRoleEditorViewModel() { }
        public SelectRoleEditorViewModel(IdentityRole role)
        {
            this.RoleName = role.Name;
        }

        public bool Selected { get; set; }

        [Required]
        public string RoleName { get; set; }
    }
}
