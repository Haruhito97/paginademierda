//------------------------------------------------------------------------------
// <auto-generated>
//    Este código se generó a partir de una plantilla.
//
//    Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//    Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace arara.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    
    public partial class gerente
    {
        public gerente()
        {
            this.locales = new HashSet<locales>();
        }

        [RegularExpression(@"^[a-zA-Z0-9_ ]{3,10}?$")]
        [Required]
        [StringLength(10, ErrorMessage = "El ID debe tener entre 3 a 10 caracteres", MinimumLength = 3)]
        [Display(Name = "ID Gerente")]
        public string id_gerente { get; set; }

        [RegularExpression(@"^[a-zA-Z0-9_ ]{3,20}?$")]
        [Required]
        [StringLength(20, ErrorMessage = "El ID debe tener entre 3 a 20 caracteres", MinimumLength = 3)]
        [Display(Name = "Nombre")]
        public string nombre { get; set; }

        [RegularExpression(@"^[a-zA-Z0-9_ ]{3,20}?$")]
        [Required]
        [StringLength(20, ErrorMessage = "El ID debe tener entre 3 a 20 caracteres", MinimumLength = 3)]
        [Display(Name = "Apellido Paterno")]
        public string apellido_p { get; set; }

        [RegularExpression(@"^[a-zA-Z0-9_ ]{3,20}?$")]
        [Required]
        [StringLength(20, ErrorMessage = "El ID debe tener entre 3 a 20 caracteres", MinimumLength = 3)]
        [Display(Name = "Apellido Materno")]
        public string apellido_m { get; set; }
    
        public virtual ICollection<locales> locales { get; set; }
    }
}