using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SpecialOlympics.Models
{
    [Table("Voluntarios")]
    public class Voluntario
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdVoluntario { get; set; }

        [Required(ErrorMessage = "StringRequired")]
        [StringLength(20, ErrorMessage = "Length20")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "StringRequired")]
        [StringLength(30, ErrorMessage = "Length30")]
        [Display(Name = "Primer apellido")]
        public string Apellido1 { get; set; }

        [StringLength(30, ErrorMessage = "Length30")]
        [Display(Name = "Segundo apellido")]
        public string Apellido2 { get; set; }

        [NotMapped]
        public string Apellidos
        {
            get { return String.Format("{0} {1}", Apellido1, Apellido2); }
            set { Apellidos = value; }
        }

        [Required(ErrorMessage = "DNIRequired")]
        [StringLength(9, ErrorMessage = "Length12")]
        public string DNI { get; set; }

        [Required(ErrorMessage = "PhoneRequired")]
        [StringLength(15, ErrorMessage = "Length15")]
        [Phone(ErrorMessage = "PhoneInvalid")]
        [Display(Name = "Teléfono")]
        public string Telefono1 { get; set; }

        [Required(ErrorMessage = "EmailRequired")]
        [EmailAddress(ErrorMessage = "EmailInvalid")]
        [StringLength(80, ErrorMessage = "Length80")]
        public string Email { get; set; }

        [Required(ErrorMessage = "AddressRequired")]
        [StringLength(200, ErrorMessage = "Length200")]
        [Display(Name = "Dirección")]
        public string DireccionCompleta { get; set; }

        [StringLength(50, ErrorMessage = "Length50")]
        [Display(Name = "Población")]
        public string Poblacion { get; set; }

        [Required(ErrorMessage = "StringRequired")]
        [StringLength(50, ErrorMessage = "Length50")]
        public string Provincia { get; set; }

        [Required(ErrorMessage = "PostalCodeRequired")]
        [StringLength(10, ErrorMessage = "Length10")]
        [Display(Name = "Código Postal")]
        public string CodigoPostal { get; set; }

        [Required(ErrorMessage = "DateRequired")]
        [DataType(DataType.Date)]
        [Display(Name = "Fecha Nacimiento")]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = false)]
        public DateTime? FechaNacimiento { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Fecha Alta")]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = false)]
        public DateTime? FechaAlta { get; set; }

        [StringLength(15, ErrorMessage = "Length15")]
        [Display(Name = "Segundo Teléfono")]
        public string Telefono2 { get; set; }

        [StringLength(15, ErrorMessage = "Length15")]
        [Display(Name = "Teléfono Emergencia")]        
        public string TelefonoEmergencia { get; set; }

        [Display(Name = "Alérgico")]
        public bool IsAlergico { get; set; }

        [StringLength(200, ErrorMessage = "Length200")]
        [Display(Name = "Alergias")]
        public string DescripcionAlergias { get; set; }

        [Display(Name = "En activo")]
        public bool IsActivo { get; set; }

        [StringLength(400, ErrorMessage = "Length400")]
        public string Observaciones { get; set; }

        //public byte[] Foto { get; set; } 

        [Display(Name = "Imagen")]
        public string ProfileImagePath { get; set; } // Creo que lo mejor sera añadir la imagen como string ImagePath y guardarlas en wwwroot\Images o algo así. En teoría se guardará en el VirtualPrivateServer en los archivos publicados

        //[ForeignKey("Documentos")]
        //public int? IdDocumento1 { get; set; }

        public string RutaDocumento1 { get; set; }
        //public IFormFile Foto { get; set; }

        //[Display(Name = "Release Date"), DataType(DataType.Date)]
        //[DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = false)]
        //public Image { get; set; }

        //public PDF Autorizaciones { get; set; }

    }
}
