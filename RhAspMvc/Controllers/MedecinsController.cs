using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using RhAspMvc.DAL;
using RhAspMvc.Models;
using RhAspMvc.Models.Repository;
using RhAspMvc.Services;

namespace RhAspMvc.Controllers
{
    public class MedecinsController : Controller
    {
        private RhContext db = new RhContext();

        // GET: Medecins
        [AccessDeniedAuthorizeAttribute(Roles = "Admin,Medecin")]
        public ActionResult Index()
        {
            return View(db.Medecins.ToList());
        }

        // GET: Medecins/Details/5
        [AccessDeniedAuthorizeAttribute(Roles = "Admin")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Medecin medecin = db.Medecins.Find(id);
            if (medecin == null)
            {
                return HttpNotFound();
            }
            return View(medecin);
        }

        // GET: Medecins/Create
        [AccessDeniedAuthorizeAttribute(Roles = "Admin")]
        public ActionResult Create()
        {
            ViewBag.services = new SelectList(db.Services.AsQueryable(), "Id", "Libelle");
            ViewBag.specialites = new MultiSelectList(db.Specialites.AsQueryable(), "Id", "Libelle");
            return View();
        }

        // POST: Medecins/Create
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AccessDeniedAuthorizeAttribute(Roles = "Admin")]
        public ActionResult Create([Bind(Include = "Id,Matricule,Prenom,Nom,DateNaiss,Salaire,ServiceId")] Medecin medecin, int[] specialites)
        {
            if (ModelState.IsValid)
            {
                medecin.Specialites = new List<Specialite>();
                foreach(int sp in specialites)
                {
                    medecin.Specialites.Add(db.Specialites.Find(sp));
                }
                db.Medecins.Add(medecin);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(medecin);
        }

        // GET: Medecins/Edit/5
        [AccessDeniedAuthorizeAttribute(Roles = "Admin")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Medecin medecin = db.Medecins.Find(id);
            if (medecin == null)
            {
                return HttpNotFound();
            }
            return View(medecin);
        }

        // POST: Medecins/Edit/5
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AccessDeniedAuthorizeAttribute(Roles = "Admin")]
        public ActionResult Edit([Bind(Include = "Id,Matricule,Prenom,Nom,DateNaiss,Salaire")] Medecin medecin)
        {
            if (ModelState.IsValid)
            {
                db.Entry(medecin).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(medecin);
        }

        // GET: Medecins/Delete/5
        [AccessDeniedAuthorizeAttribute(Roles = "Admin")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Medecin medecin = db.Medecins.Find(id);
            if (medecin == null)
            {
                return HttpNotFound();
            }
            return View(medecin);
        }

        // POST: Medecins/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [AccessDeniedAuthorizeAttribute(Roles = "Admin")]
        public ActionResult DeleteConfirmed(int id)
        {
            Medecin medecin = db.Medecins.Find(id);
            db.Medecins.Remove(medecin);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        [AccessDeniedAuthorizeAttribute(Roles = "Admin")]
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        [AccessDeniedAuthorizeAttribute(Roles ="Admin")]
        public ActionResult Affecter(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //ViewBag.services = new SelectList(db.Services.AsQueryable(), "Id", "Libelle");
            //ViewBag.specialites = new MultiSelectList(db.Specialites.AsQueryable(), "Id", "Libelle");
            Medecin medecin = db.Medecins.Find(id);

            ViewBag.services = new SelectList(db.Services.AsQueryable(), "Id", "Libelle");
            if (medecin == null)
            {
                return HttpNotFound();
            }
            return View(medecin);
        }

        [HttpPost]
        [AccessDeniedAuthorizeAttribute(Roles ="Admin")]
        public ActionResult Affecter(int Id, int ServiceId)
        {
            using(var db = new RhContext())
            {
                IQueryable<Medecin> queryable = db.Medecins.Where(m => m.Id == Id);
                var medecin = queryable.Include(r => r.Specialites).FirstOrDefault();
                medecin.ServiceId = ServiceId;
                foreach (Specialite specialite in medecin.Specialites.ToList())
                {
                    medecin.Specialites.Remove(specialite);
                }
                db.SaveChanges();
            }
            


            return RedirectToAction("Index");
        }

        [AccessDeniedAuthorizeAttribute(Roles ="Admin")]
        public ActionResult Specialite(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Medecin medecin = db.Medecins.Find(id);

            /*ServiceRepository serviceRepository = new ServiceRepository();
            Service service = serviceRepository.FindServiceByMedecinId(id);*/

            SpecialiteRepository specialiteRepository = new SpecialiteRepository();
            List<Specialite> specialites = new List<Specialite>();
            specialiteRepository.FindSpecialiteByServiceId(medecin.ServiceId).ForEach(
                sp => specialites.Add(new Specialite { Id = sp.Id, Libelle = sp.Libelle }));

            ViewBag.specialite = new MultiSelectList(specialites, "Id", "Libelle");
            //ViewBag.service = service.Libelle;
            if (medecin == null)
            {
                return HttpNotFound();
            }
            return View(medecin);
        }

        [HttpPost]
        [AccessDeniedAuthorizeAttribute(Roles ="Admin")]
        public ActionResult Specialite(int Id, int[] specialites)
        {

            using (var db = new RhContext())
            {
                Medecin medecin = db.Medecins.Find(Id);
                medecin.Specialites = new List<Specialite>();
                foreach (int sp in specialites)
                {
                    medecin.Specialites.Add(db.Specialites.Find(sp));
                }
                db.SaveChanges();
            }

            return RedirectToAction("Index");
        }

        // GET: Medecins/Services/5
        [AllowAnonymous]
        public JsonResult FindSpecialitesByServiceId(int id)
        {
            SpecialiteRepository specialiteRepository = new SpecialiteRepository();
            List<Specialite> specialites = new List<Specialite>();
            specialiteRepository.FindSpecialiteByServiceId(id).ForEach(
                sp => specialites.Add(new Specialite { Id = sp.Id, Libelle = sp.Libelle }));
            return Json(specialites, JsonRequestBehavior.AllowGet);
        }

    }
}
