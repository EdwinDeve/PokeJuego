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
    public class TipoPokemonController : Controller
    {
        private dbPokemonEntities db = new dbPokemonEntities();

        // GET: TipoPokemon
        public ActionResult Index()
        {
            return View(db.C_tbTipoPokemon.ToList());
        }



        // GET: TipoPokemon/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            C_tbTipoPokemon c_tbTipoPokemon = db.C_tbTipoPokemon.Find(id);
            if (c_tbTipoPokemon == null)
            {
                return HttpNotFound();
            }
            return View(c_tbTipoPokemon);
        }




        // GET: TipoPokemon/Create
        public ActionResult Create()
        {
            return View();
        }




        // POST: TipoPokemon/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "intTipoPokemon,strTipoPokemon")] C_tbTipoPokemon c_tbTipoPokemon)
        {
            if (ModelState.IsValid)
            {
                db.C_tbTipoPokemon.Add(c_tbTipoPokemon);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(c_tbTipoPokemon);
        }




        // GET: TipoPokemon/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            C_tbTipoPokemon c_tbTipoPokemon = db.C_tbTipoPokemon.Find(id);
            if (c_tbTipoPokemon == null)
            {
                return HttpNotFound();
            }
            return View(c_tbTipoPokemon);
        }




        // POST: TipoPokemon/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "intTipoPokemon,strTipoPokemon")] C_tbTipoPokemon c_tbTipoPokemon)
        {
            if (ModelState.IsValid)
            {
                db.Entry(c_tbTipoPokemon).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(c_tbTipoPokemon);
        }





        // GET: TipoPokemon/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            C_tbTipoPokemon c_tbTipoPokemon = db.C_tbTipoPokemon.Find(id);
            if (c_tbTipoPokemon == null)
            {
                return HttpNotFound();
            }
            return View(c_tbTipoPokemon);
        }





        // POST: TipoPokemon/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            C_tbTipoPokemon c_tbTipoPokemon = db.C_tbTipoPokemon.Find(id);
            db.C_tbTipoPokemon.Remove(c_tbTipoPokemon);
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
