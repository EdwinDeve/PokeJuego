﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace PokeJuego.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class dbPokemonEntities : DbContext
    {
        public dbPokemonEntities()
            : base("name=dbPokemonEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<C_tbAtaqueEspecial> C_tbAtaqueEspecial { get; set; }
        public virtual DbSet<C_tbAtaques> C_tbAtaques { get; set; }
        public virtual DbSet<C_tbPokemon> C_tbPokemon { get; set; }
        public virtual DbSet<C_tbTipoPokemon> C_tbTipoPokemon { get; set; }
    }
}
