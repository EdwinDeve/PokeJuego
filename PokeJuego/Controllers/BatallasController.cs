using PokeJuego.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PokeJuego.Controllers
{
    public class BatallasController : Controller
    {
        private dbPokemonEntities db = new dbPokemonEntities();

        // GET: Batallas
        public ActionResult SeleccionPersonaje(int idPokemon)
        {
            var poke = db.C_tbPokemon.Find(idPokemon);

            return View(poke);
        }



        public ActionResult SeleccionarContrincante(int MiPokemon)
        {
            int min = 1;
            int max = db.C_tbPokemon.Max(x => x.intPokemon);
            Random ramon = new Random();

            var idPok = ramon.Next(min, max + 1);

            try
            {
                ViewBag.Mipokemon = MiPokemon;
                var contrincante = db.C_tbPokemon.Find(idPok);
                return View(contrincante);  
            }
            catch (Exception)
            {

                return RedirectToAction("SeleccionPersonaje", MiPokemon);
            }

        }




        public ActionResult Resumen(int Mipokemon, int Contrincante)
        {

            ViewBag.MiPokemon = db.C_tbPokemon.Find(Mipokemon).strNombrePokemon;
            ViewBag.ImagenMiPokemon = db.C_tbPokemon.Find(Mipokemon).strImagen;

            ViewBag.Contrincante = db.C_tbPokemon.Find(Contrincante).strNombrePokemon;
            ViewBag.ImagenContrincante = db.C_tbPokemon.Find(Contrincante).strImagen;

            return View();
        }
    }
}