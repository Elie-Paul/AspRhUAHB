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
    public class SpecialitesController : Controller
    {
        private RhContext db = new RhContext();

        // GET: Specialites
        public ActionResult Index()
        {
            return View(db.Specialites.ToList());
        }

        // GET: Specialites/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Specialite specialite = db.Specialites.Find(id);
            if (specialite == null)
            {
                return HttpNotFound();
            }
            return View(specialite);
        }

        // GET: Specialites/Create
        public ActionResult Create()
        {
            ViewBag.services = new SelectList(db.Services.AsQueryable(), "Id", "Libelle");
            return View();
        }

        // POST: Specialites/Create
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Libelle")] Specialite specialite, int Service)
        {
            if (ModelState.IsValid)
            {
                Service service = db.Services.Find(Service);
                specialite.Service = service;
                Specialite spExist = db.Specialites.SingleOrDefault(sp => sp.Libelle == (specialite.Libelle));
                if (spExist != null)
                {
                    return RedirectToAction("Create");
                }
                db.Specialites.Add(specialite);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(specialite);
        }

        // GET: Specialites/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Specialite specialite = db.Specialites.Find(id);
            if (specialite == null)
            {
                return HttpNotFound();
            }
            return View(specialite);
        }

        // POST: Specialites/Edit/5
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Libelle")] Specialite specialite)
        {
            if (ModelState.IsValid)
            {
                db.Entry(specialite).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(specialite);
        }

        // GET: Specialites/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Specialite specialite = db.Specialites.Find(id);
            if (specialite == null)
            {
                return HttpNotFound();
            }
            return View(specialite);
        }

        // POST: Specialites/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Specialite specialite = db.Specialites.Find(id);
            db.Specialites.Remove(specialite);
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
