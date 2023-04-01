using Microsoft.Ajax.Utilities;
using PokeJuego.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;
using System.Web.Services.Description;
using System.Web.WebPages;

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

            if (idPok == MiPokemon)
            {
                idPok = ramon.Next(min, max + 1);
            }

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
            ViewBag.idPokemon = Mipokemon;
            ViewBag.MiPokemon = db.C_tbPokemon.Find(Mipokemon).strNombrePokemon;
            ViewBag.ImagenMiPokemon = db.C_tbPokemon.Find(Mipokemon).strImagen;

            ViewBag.idContrincante = Contrincante;
            ViewBag.Contrincante = db.C_tbPokemon.Find(Contrincante).strNombrePokemon;
            ViewBag.ImagenContrincante = db.C_tbPokemon.Find(Contrincante).strImagen;

            return View();
        }





        public ActionResult Huir(int PokemonCobarde)
        {
            var Cobarde = db.C_tbPokemon.Where(x => x.intPokemon == PokemonCobarde).First();
            return View(Cobarde);

        }

        public ActionResult FinalPelea(string Mensaje, int PokemonGanador)
        {
            ViewBag.MensajeGanador = Mensaje;
            var Ganador = db.C_tbPokemon.Where(x => x.intPokemon == PokemonGanador).First();
            return View(Ganador);

        }

        //[HttpPost]
        public ActionResult Batalla(int Mipokemon, int Contrincante, int turno, int PS, int PSEnemigo, string accion)
        {
            //Mipokemon, EL ID DE MI POKEMON
            //Contrincante, EL ID DE MI ENEMIGO

            //turno, 1 2 3 4
            //accion, ATACAR - HUIR

            //PS, VIDA ACTUAL DE MI PERSONAJE
            //PSEnemigo VIDA ACTUAL DEL ENEMIGO


            System.Threading.Thread.Sleep(1000);

            var Personaje = db.C_tbPokemon.Where(x => x.intPokemon == Mipokemon).First();
            var Enemigo = db.C_tbPokemon.Where(x => x.intPokemon == Contrincante).First();

            var vidarestante = PS;
            var vidaRestanteEnemigo = PSEnemigo;

            var MiAtaque = 0;
            var AtaqueEnemigo = 0;

            var MiAtaqueEspecial = 0;
            var AtaqueEnemigoEspecial = 0;

            ViewBag.MiPokemon = Personaje;
            ViewBag.Contrincante = Enemigo;

            if (turno == 5)
            {

                if (PS < PSEnemigo)
                {
                    var MensajeRes = "Haz perdido esta batalla";
                    return RedirectToAction("FinalPelea", "Batallas", new { Mensaje = MensajeRes, PokemonGanador = Contrincante });

                }
                else
                {
                    var MensajeRes = "Haz ganado esta batalla";
                    return RedirectToAction("FinalPelea", "Batallas", new { Mensaje = MensajeRes, PokemonGanador = Mipokemon });

                }
            }


            if (accion == "Atacar" || accion == "Comenzar_Batalla")
            {
                switch (turno)
                {
                    case 0:
                        var vidaPersonaje = Personaje.tnyVida;
                        var vidaEnemigo = Enemigo.tnyVida;

                        ViewBag.MisPS = vidaPersonaje;
                        ViewBag.PSenemigo = vidaEnemigo;

                        ViewBag.accion = "Ataque Basico";
                        ViewBag.Turno = 1;

                        break;




                    case 1:

                        //CUADO DE CLIC EN EL BOTON DE ATACAR ENTRARE EN EL TURNO 2
                        // AHORA A LOS PERSONAJES SE LES RESTO UN PORCENTAJE DE VIDA

                        MiAtaque = DañoAleatorio(Personaje.C_tbAtaques.intDaño);
                        vidaRestanteEnemigo = PSEnemigo - MiAtaque;
                        ViewBag.PSenemigo = vidaRestanteEnemigo;

                        AtaqueEnemigo = DañoAleatorio(Enemigo.C_tbAtaques.intDaño);
                        vidarestante = PS - AtaqueEnemigo;
                        ViewBag.MisPS = vidarestante;

                        if (vidaRestanteEnemigo < 0 || vidarestante < 0)
                        {

                            if (vidaRestanteEnemigo < vidarestante)
                            {
                                var MensajeRes = "Haz ganado esta batalla";
                                return RedirectToAction("FinalPelea", "Batallas", new { Mensaje = MensajeRes, PokemonGanador = Mipokemon });
                            }
                            else
                            {
                                var MensajeRes = "Haz perdido esta batalla";
                                return RedirectToAction("FinalPelea", "Batallas", new { Mensaje = MensajeRes, PokemonGanador = Contrincante });
                            }
                        }



                        ViewBag.Turno = 2;
                        ViewBag.accion = "Ataque Basico";
                        ViewBag.MensajeCombate = "Resultados del primer enfrentamiento";
                        ViewBag.mensajeTurnoJugador = Personaje.strNombrePokemon + " ha utilizado " + Personaje.C_tbAtaques.strNombreAtaque + ", el enemigo ha recibido " + MiAtaque + " puntos de daño";
                        ViewBag.mensajeTurnoEnemigo = Enemigo.strNombrePokemon + "  ha utilizado " + Enemigo.C_tbAtaques.strNombreAtaque + ", haz perdido " + AtaqueEnemigo + " puntos de salud";

                        break;







                    case 2:

                        //CUADO DE CLIC EN EL BOTON DE ATACAR ENTRARE EN EL TURNO 3
                        // AHORA A LOS PERSONAJES SE LES RESTO UN PORCENTAJE DE VIDA

                        MiAtaque = DañoAleatorio(Personaje.C_tbAtaques.intDaño);
                        vidaRestanteEnemigo = PSEnemigo - MiAtaque;
                        ViewBag.PSenemigo = vidaRestanteEnemigo;

                        AtaqueEnemigo = DañoAleatorio(Enemigo.C_tbAtaques.intDaño);
                        vidarestante = PS - AtaqueEnemigo;
                        ViewBag.MisPS = vidarestante;

                        if (vidaRestanteEnemigo < 0 || vidarestante < 0)
                        {

                            if (vidaRestanteEnemigo < vidarestante)
                            {
                                var MensajeRes = "Haz ganado esta batalla";
                                return RedirectToAction("FinalPelea", "Batallas", new { Mensaje = MensajeRes, PokemonGanador = Mipokemon });
                            }
                            else
                            {
                                var MensajeRes = "Haz perdido esta batalla";
                                return RedirectToAction("FinalPelea", "Batallas", new { Mensaje = MensajeRes , PokemonGanador = Contrincante });
                            }
                        }

                        ViewBag.Turno = 3;
                        ViewBag.accion = "Ataque Basico";
                        ViewBag.MensajeCombate = "Resultados del segundo enfrentamiento";
                        ViewBag.mensajeTurnoJugador = Personaje.strNombrePokemon + " ha utilizado " + Personaje.C_tbAtaques.strNombreAtaque + ", el enemigo ha recibido " + MiAtaque + " puntos de daño";
                        ViewBag.mensajeTurnoEnemigo = Enemigo.strNombrePokemon + "  ha utilizado " + Enemigo.C_tbAtaques.strNombreAtaque + ", haz perdido " + AtaqueEnemigo + " puntos de salud";

                        break;









                    case 3:

                        //CUADO DE CLIC EN EL BOTON DE ATACAR ENTRARE EN EL TURNO 4
                        // AHORA A LOS PERSONAJES SE LES RESTO UN PORCENTAJE DE VIDA UTILIZANDO SU ATAQUE ESPECIAL

                        MiAtaque = DañoAleatorio(Personaje.C_tbAtaques.intDaño);
                        vidaRestanteEnemigo = PSEnemigo - MiAtaque;
                        ViewBag.PSenemigo = vidaRestanteEnemigo;

                        AtaqueEnemigo = DañoAleatorio(Enemigo.C_tbAtaques.intDaño);
                        vidarestante = PS - AtaqueEnemigo;
                        ViewBag.MisPS = vidarestante;

                        if (vidaRestanteEnemigo < 0 || vidarestante < 0)
                        {

                            if (vidaRestanteEnemigo < vidarestante)
                            {
                                var MensajeRes = "Haz ganado esta batalla";
                                return RedirectToAction("FinalPelea", "Batallas", new { Mensaje = MensajeRes, PokemonGanador = Mipokemon });
                            }
                            else
                            {
                                var MensajeRes = "Haz perdido esta batalla";
                                return RedirectToAction("FinalPelea", "Batallas", new { Mensaje = MensajeRes , PokemonGanador = Contrincante });
                            }
                        }

                        ViewBag.Turno = 4;
                        ViewBag.accion = "Ataque Especial";
                        ViewBag.MensajeCombate = "Resultados del tercer enfrentamiento";
                        ViewBag.mensajeTurnoJugador = Personaje.strNombrePokemon + " ha utilizado " + Personaje.C_tbAtaques.strNombreAtaque + ", el enemigo ha recibido " + MiAtaque + " puntos de daño";
                        ViewBag.mensajeTurnoEnemigo = Enemigo.strNombrePokemon + "  ha utilizado " + Enemigo.C_tbAtaques.strNombreAtaque + ", haz perdido " + AtaqueEnemigo + " puntos de salud";

                        break;










                    case 4:

                        //CUADO DE CLIC EN EL BOTON DE ATACAR ENTRARE EN EL TURNO 4
                        // AHORA A LOS PERSONAJES SE LES RESTO UN PORCENTAJE DE VIDA UTILIZANDO SU ATAQUE ESPECIAL

                        MiAtaqueEspecial = DañoAleatorio(Personaje.C_tbAtaqueEspecial.intDañoEsp);
                        vidaRestanteEnemigo = PSEnemigo - MiAtaqueEspecial;
                        ViewBag.PSenemigo = vidaRestanteEnemigo;

                        AtaqueEnemigoEspecial = DañoAleatorio(Enemigo.C_tbAtaqueEspecial.intDañoEsp);
                        vidarestante = PS - AtaqueEnemigoEspecial;
                        ViewBag.MisPS = vidarestante;

                        if (vidaRestanteEnemigo < 0 || vidarestante < 0)
                        {

                            if (vidaRestanteEnemigo < vidarestante)
                            {
                                var MensajeRes = "Haz ganado esta batalla";
                                return RedirectToAction("FinalPelea", "Batallas", new { Mensaje = MensajeRes , PokemonGanador = Mipokemon });
                            }
                            else
                            {
                                var MensajeRes = "Haz perdido esta batalla";
                                return RedirectToAction("FinalPelea", "Batallas", new { Mensaje = MensajeRes , PokemonGanador = Contrincante });
                            }
                        }

                        ViewBag.Turno = 5;
                        ViewBag.accion = "Ver Resultados";
                        ViewBag.MensajeCombate = "Resultados del cuarto enfrentamiento";
                        ViewBag.mensajeTurnoJugador = Personaje.strNombrePokemon + " ha utilizado " + Personaje.C_tbAtaqueEspecial.strNombreAtaqueEsp + ", el enemigo ha recibido " + MiAtaqueEspecial + " puntos de daño";
                        ViewBag.mensajeTurnoEnemigo = Enemigo.strNombrePokemon + "  ha utilizado " + Enemigo.C_tbAtaqueEspecial.strNombreAtaqueEsp + ", haz perdido " + AtaqueEnemigoEspecial + " puntos de salud";
                        break;






                }
                return View();
            }
            else
            {

                return RedirectToAction("Huir", "Batallas", new { PokemonCobarde = Mipokemon});

            }

        }


        public int DañoAleatorio(int D)
        {
            int min = 1;
            int max = 10;
            var porcentaje = 0;
            double Valordec = 0;
            var damage = 0.0;
            Random ramon = new Random();

            porcentaje = ramon.Next(min, max + 1);
            Valordec = porcentaje * 0.2;
            damage = D * Valordec;

            if (damage < 2)
            {
                porcentaje = ramon.Next(min, max + 1);
                Valordec = porcentaje * 0.2;
                damage = D * Valordec;
            }

            return (int)damage;
        }

    }
}