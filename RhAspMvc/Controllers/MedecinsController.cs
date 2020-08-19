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

namespace RhAspMvc.Controllers
{
    public class MedecinsController : Controller
    {
        private RhContext db = new RhContext();

        // GET: Medecins
        public ActionResult Index()
        {
            return View(db.Medecins.ToList());
        }

        // GET: Medecins/Details/5
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
        public ActionResult Create()
        {
            return View();
        }

        // POST: Medecins/Create
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Matricule,Prenom,Nom,DateNaiss,Salaire")] Medecin medecin)
        {
            if (ModelState.IsValid)
            {
                db.Medecins.Add(medecin);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(medecin);
        }

        // GET: Medecins/Edit/5
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
        public ActionResult DeleteConfirmed(int id)
        {
            Medecin medecin = db.Medecins.Find(id);
            db.Medecins.Remove(medecin);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
