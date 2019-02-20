using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CRUD_Alumnos.Models
{
    public class AlumnoCE
    {
        public int Id { get; set; }

        [Required]
        [Display(Name ="Ingrese Nombres")]
        public string Nombres { get; set; }

        [Required]
        [Display(Name = "Ingrese Apellidos")]
        public string Apellidos { get; set; }

        [Required]
        [Display(Name = "Edad del Alumno")]
        public int Edad { get; set; }
        
        [Required]
        [Display(Name = "Sexo del Alumno")]
        public string Sexo { get; set; }

        [Required]
        [Display(Name = "Ciudad")]
        public int CodCiudad { get; set; }

        public System.DateTime FechaRegisro { get; set; }
        public string NombreCiudad { get; set; }
        public string NombreCompleto { get { return Nombres + " " + Apellidos; } }
    }

    [MetadataType(typeof(AlumnoCE))]

    public partial class Alumno
    {
        public int Estado { get; set; }
        public string NombreCiudad { get; set; }
        public string NombreCompleto { get { return Nombres + " " + Apellidos; } }
    }
}