using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace RhAspMvc.Models
{
    public class Medecin
    {
        public int Id { get; set; }
        [Required]
        public string Matricule { get; set; }
        [Required]
        public string Prenom { get; set; }
        [Required]
        public string Nom { get; set; }
        [Required]
        public DateTime DateNaiss { get; set; }
        [Required]
        public int Salaire { get; set; }
        public ICollection<Specialite> Specialites { get; set; }
        public int ServiceId { get; set; }
        [ForeignKey("ServiceId")]
        public virtual Service Service { get; set; }

    }
}