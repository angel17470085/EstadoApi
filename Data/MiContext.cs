using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using AppEstados.Models;

namespace AppEstados.Data
{
    public class MiContext : DbContext
    {
        public MiContext (DbContextOptions<MiContext> options)
            : base(options)
        {
        }

        public DbSet<Estado> Estados { get; set; }

        public DbSet<Municipio> Municipios { get; set; }
    }
}
