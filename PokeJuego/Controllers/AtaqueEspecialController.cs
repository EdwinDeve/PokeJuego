using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PokeJuego.Models;

namespace PokeJuego.Controllers
{
    public class AtaqueEspecialController : Controller
    {
        private dbPokemonEntities db = new dbPokemonEntities();

        // GET: AtaqueEspecial
        public ActionResult Index()
        {
            return View(db.C_tbAtaqueEspecial.ToList());
        }

        // GET: AtaqueEspecial/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            C_tbAtaqueEspecial c_tbAtaqueEspecial = db.C_tbAtaqueEspecial.Find(id);
            if (c_tbAtaqueEspecial == null)
            {
                return HttpNotFound();
            }
            return View(c_tbAtaqueEspecial);
        }

        // GET: AtaqueEspecial/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: AtaqueEspecial/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "intAtaqueEsp,strNombreAtaqueEsp,intDañoEsp")] C_tbAtaqueEspecial c_tbAtaqueEspecial)
        {
            if (ModelState.IsValid)
            {
                db.C_tbAtaqueEspecial.Add(c_tbAtaqueEspecial);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(c_tbAtaqueEspecial);
        }

        // GET: AtaqueEspecial/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            C_tbAtaqueEspecial c_tbAtaqueEspecial = db.C_tbAtaqueEspecial.Find(id);
            if (c_tbAtaqueEspecial == null)
            {
                return HttpNotFound();
            }
            return View(c_tbAtaqueEspecial);
        }

        // POST: AtaqueEspecial/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "intAtaqueEsp,strNombreAtaqueEsp,intDañoEsp")] C_tbAtaqueEspecial c_tbAtaqueEspecial)
        {
            if (ModelState.IsValid)
            {
                db.Entry(c_tbAtaqueEspecial).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(c_tbAtaqueEspecial);
        }

        // GET: AtaqueEspecial/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            C_tbAtaqueEspecial c_tbAtaqueEspecial = db.C_tbAtaqueEspecial.Find(id);
            if (c_tbAtaqueEspecial == null)
            {
                return HttpNotFound();
            }
            return View(c_tbAtaqueEspecial);
        }

        // POST: AtaqueEspecial/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            C_tbAtaqueEspecial c_tbAtaqueEspecial = db.C_tbAtaqueEspecial.Find(id);
            db.C_tbAtaqueEspecial.Remove(c_tbAtaqueEspecial);
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
