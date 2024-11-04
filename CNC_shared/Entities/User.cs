using CNC_shared.Enums;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CNC_shared.Entities
{
    public class User : IdentityUser
    {
        [Display(Name = "Nombres")]
        [MaxLength(50, ErrorMessage = "El campo {0} debe tener máximo {1} caractéres.")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public string FirstName { get; set; } = null!;

        [Display(Name = "Apellidos")]
        [MaxLength(50, ErrorMessage = "El campo {0} debe tener máximo {1} caractéres.")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public string LastName { get; set; } = null!;


        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [Display(Name = "Fecha_nacimiento")]
        public DateTime Birth_Date { get; set; }

        [MaxLength(20, ErrorMessage = "El campo {0} debe tener máximo {1} caractéres.")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public string Cedula { get; set; } = null!;

        [Display(Name = "Rol")]
        public UserType UserType { get; set; }

        [Display(Name = "Usuario_Creacion")]
        public string User_Creation { get; set; } = string.Empty;

        [Display(Name = "Usuario_Modificacion")]
        public string User_Modification { get; set; } = string.Empty;

        [Display(Name = "Usuario_Anulacion")]
        public string User_Voided { get; set; } = string.Empty;

        [Display(Name = "Fecha_Creacion")]
        public DateTime Date_Creation { get; set; }

        [Display(Name = "Fecha_Modificacion")]
        public DateTime Date_Modification { get; set; }

        [Display(Name = "Fecha_Anulacion")]
        public DateTime Date_Voided { get; set; }

        [Display(Name = "Estatus")]
        public bool Status { get; set; } = false;
    }
}
