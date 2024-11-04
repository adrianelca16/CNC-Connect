﻿
using System.ComponentModel.DataAnnotations;

namespace CNC_shared.DTOs
{
    public class LoginDTO
    {
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public string Cedula { get; set; } = null!;

        [Display(Name = "Contraseña")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        [MinLength(6, ErrorMessage = "El campo {0} debe tener al menos {1} carácteres.")]
        public string Password { get; set; } = null!;
    }
}
