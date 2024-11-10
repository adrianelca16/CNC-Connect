using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CNC_shared.Entities
{
    public class Clientes
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required(ErrorMessage = "Este campo es obligatorio.")]
        [MaxLength(100, ErrorMessage = "El campo {0} mas de {1} caracteres")]
        public string Nombres { get; set; } = null!;

        [Required(ErrorMessage = "Este campo es obligatorio.")]
        [MaxLength(100, ErrorMessage = "El campo {0} mas de {1} caracteres")]
        public string Apellidos { get; set; } = null!;

        [Required(ErrorMessage = "Este campo es obligatorio.")]
        [MaxLength(20, ErrorMessage = "El campo {0} mas de {1} caracteres")]
        public string TipoDocumento { get; set; } = null!;

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Required(ErrorMessage = "Este campo es obligatorio.")]
        [MaxLength(50, ErrorMessage = "El campo {0} mas de {1} caracteres")]
        public string Documento { get; set; } = null!;

        [Required(ErrorMessage = "Este campo es obligatorio.")]
        [MaxLength(255, ErrorMessage = "El campo {0} mas de {1} caracteres")]
        public string Direccion { get; set; } = null!;

        [Required(ErrorMessage = "Este campo es obligatorio.")]
        [MaxLength(50, ErrorMessage = "El campo {0} mas de {1} caracteres")]
        public string Telefono { get; set; } = null!;

        [Required(ErrorMessage = "Este campo es obligatorio campo")]
        [MaxLength(50, ErrorMessage = "El campo {0} mas de {1} caracteres")]
        [EmailAddress]
        public string Email { get; set; } = null!;

        public string? Usuario_Creacion { get; set; } = null!;

        [MaxLength(100)]
        public string? Usuario_Modificacion { get; set; }

        [MaxLength(100)]
        public string? Usuario_Anulacion { get; set; }

        public DateTime Fecha_Creacion { get; set; } = DateTime.Now;

        public DateTime? Fecha_Modificacion { get; set; }

        public DateTime? Fecha_Anulacion { get; set; }

        public bool? Anulado { get; set; } = false;

    }
}
