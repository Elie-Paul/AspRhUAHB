using RhAspMvc.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RhAspMvc.Models.Repository
{
    public class SpecialiteRepository
    {
        private RhContext db = new RhContext();

        public List<Specialite> FindSpecialiteByServiceId(int id)
        {
            return (from sp in db.Specialites where sp.Service.Id == id select sp).ToList();
        }
    }
}