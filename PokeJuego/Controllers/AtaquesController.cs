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
    public class AtaquesController : Controller
    {
        private dbPokemonEntities db = new dbPokemonEntities();

        // GET: Ataques
        public ActionResult Index()
        {
            return View(db.C_tbAtaques.ToList());
        }

        // GET: Ataques/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            C_tbAtaques c_tbAtaques = db.C_tbAtaques.Find(id);
            if (c_tbAtaques == null)
            {
                return HttpNotFound();
            }
            return View(c_tbAtaques);
        }

        // GET: Ataques/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Ataques/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "intAtaque,strNombreAtaque,intDaño")] C_tbAtaques c_tbAtaques)
        {
            if (ModelState.IsValid)
            {
                db.C_tbAtaques.Add(c_tbAtaques);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(c_tbAtaques);
        }

        // GET: Ataques/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            C_tbAtaques c_tbAtaques = db.C_tbAtaques.Find(id);
            if (c_tbAtaques == null)
            {
                return HttpNotFound();
            }
            return View(c_tbAtaques);
        }

        // POST: Ataques/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "intAtaque,strNombreAtaque,intDaño")] C_tbAtaques c_tbAtaques)
        {
            if (ModelState.IsValid)
            {
                db.Entry(c_tbAtaques).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(c_tbAtaques);
        }

        // GET: Ataques/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            C_tbAtaques c_tbAtaques = db.C_tbAtaques.Find(id);
            if (c_tbAtaques == null)
            {
                return HttpNotFound();
            }
            return View(c_tbAtaques);
        }

        // POST: Ataques/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            C_tbAtaques c_tbAtaques = db.C_tbAtaques.Find(id);
            db.C_tbAtaques.Remove(c_tbAtaques);
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
