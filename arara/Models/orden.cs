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
    
    public partial class orden
    {
        public string id_orden { get; set; }
        public string detalles { get; set; }
        public int id_repartidor { get; set; }
        public string direccion_cliente { get; set; }
        public string id_local { get; set; }
    
        public virtual locales locales { get; set; }
    }
}