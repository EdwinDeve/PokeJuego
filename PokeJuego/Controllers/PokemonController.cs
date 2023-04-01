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
    public class PokemonController : Controller
    {
        private dbPokemonEntities db = new dbPokemonEntities();

        // GET: Pokemon
        public ActionResult Index()
        {
            var c_tbPokemon = db.C_tbPokemon.Include(c => c.C_tbAtaqueEspecial).Include(c => c.C_tbAtaques).Include(c => c.C_tbTipoPokemon);
            return View(c_tbPokemon.ToList());
        }



        public ActionResult ListaPokemones()
        {
            var c_tbPokemon = db.C_tbPokemon.Include(c => c.C_tbAtaqueEspecial).Include(c => c.C_tbAtaques).Include(c => c.C_tbTipoPokemon);
            return View(c_tbPokemon.ToList());
        }

        // GET: Pokemon/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            C_tbPokemon c_tbPokemon = db.C_tbPokemon.Find(id);
            if (c_tbPokemon == null)
            {
                return HttpNotFound();
            }
            return View(c_tbPokemon);
        }

        // GET: Pokemon/Create
        public ActionResult Create()
        {
            ViewBag.intAtaqueEsp = new SelectList(db.C_tbAtaqueEspecial, "intAtaqueEsp", "strNombreAtaqueEsp");
            ViewBag.intAtaque = new SelectList(db.C_tbAtaques, "intAtaque", "strNombreAtaque");
            ViewBag.intTipoPokemon = new SelectList(db.C_tbTipoPokemon, "intTipoPokemon", "strTipoPokemon");
            return View();
        }

        // POST: Pokemon/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "intPokemon,strNombrePokemon,strImagen,tnyVida,intTipoPokemon,intAtaque,intAtaqueEsp")] C_tbPokemon c_tbPokemon)
        {
            if (ModelState.IsValid)
            {
                db.C_tbPokemon.Add(c_tbPokemon);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.intAtaqueEsp = new SelectList(db.C_tbAtaqueEspecial, "intAtaqueEsp", "strNombreAtaqueEsp", c_tbPokemon.intAtaqueEsp);
            ViewBag.intAtaque = new SelectList(db.C_tbAtaques, "intAtaque", "strNombreAtaque", c_tbPokemon.intAtaque);
            ViewBag.intTipoPokemon = new SelectList(db.C_tbTipoPokemon, "intTipoPokemon", "strTipoPokemon", c_tbPokemon.intTipoPokemon);
            return View(c_tbPokemon);
        }

        // GET: Pokemon/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            C_tbPokemon c_tbPokemon = db.C_tbPokemon.Find(id);
            if (c_tbPokemon == null)
            {
                return HttpNotFound();
            }
            ViewBag.intAtaqueEsp = new SelectList(db.C_tbAtaqueEspecial, "intAtaqueEsp", "strNombreAtaqueEsp", c_tbPokemon.intAtaqueEsp);
            ViewBag.intAtaque = new SelectList(db.C_tbAtaques, "intAtaque", "strNombreAtaque", c_tbPokemon.intAtaque);
            ViewBag.intTipoPokemon = new SelectList(db.C_tbTipoPokemon, "intTipoPokemon", "strTipoPokemon", c_tbPokemon.intTipoPokemon);
            return View(c_tbPokemon);
        }

        // POST: Pokemon/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "intPokemon,strNombrePokemon,strImagen,tnyVida,intTipoPokemon,intAtaque,intAtaqueEsp")] C_tbPokemon c_tbPokemon)
        {
            if (ModelState.IsValid)
            {
                db.Entry(c_tbPokemon).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.intAtaqueEsp = new SelectList(db.C_tbAtaqueEspecial, "intAtaqueEsp", "strNombreAtaqueEsp", c_tbPokemon.intAtaqueEsp);
            ViewBag.intAtaque = new SelectList(db.C_tbAtaques, "intAtaque", "strNombreAtaque", c_tbPokemon.intAtaque);
            ViewBag.intTipoPokemon = new SelectList(db.C_tbTipoPokemon, "intTipoPokemon", "strTipoPokemon", c_tbPokemon.intTipoPokemon);
            return View(c_tbPokemon);
        }

        // GET: Pokemon/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            C_tbPokemon c_tbPokemon = db.C_tbPokemon.Find(id);
            if (c_tbPokemon == null)
            {
                return HttpNotFound();
            }
            return View(c_tbPokemon);
        }

        // POST: Pokemon/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            C_tbPokemon c_tbPokemon = db.C_tbPokemon.Find(id);
            db.C_tbPokemon.Remove(c_tbPokemon);
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
