using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RhAspMvc.Models
{
    public class Service
    {
        public int Id { get; set; }
        [Required]
        public string Libelle { get; set; }
        public ICollection<Specialite> Specialites { get; set; }
        public ICollection<Medecin> Medecins { get; set; }
    }
}