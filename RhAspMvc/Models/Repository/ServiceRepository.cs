using RhAspMvc.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RhAspMvc.Models.Repository
{
    public class ServiceRepository
    {
        private RhContext db = new RhContext();

        public Service FindServiceByMedecinId(int? id)
        {
            return (from m in db.Medecins where m.Id == id select m.Service).FirstOrDefault();
        }
    }
}