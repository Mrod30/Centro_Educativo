//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Centro_Educativo.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class EstudiantesMaterias
    {
        public int EstudianteMateriaID { get; set; }
        public int EstudianteID { get; set; }
        public int MateriaID { get; set; }
    
        public virtual Estudiantes Estudiantes { get; set; }
        public virtual Materias Materias { get; set; }
    }
}
