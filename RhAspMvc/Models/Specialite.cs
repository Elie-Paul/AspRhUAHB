using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace RhAspMvc.Models
{
    public class Specialite
    {
        public int Id { get; set; }
        [Required]
        public string Libelle { get; set; }
        public int ServiceId { get; set; }
        [ForeignKey("ServiceId")]
        public virtual Service Service { get; set; }
        public ICollection<Medecin> Medecins { get; set; }
    }
}