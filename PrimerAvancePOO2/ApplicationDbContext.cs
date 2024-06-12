
using Microsoft.EntityFrameworkCore;
using PrimerAvancePOO2.Entities;

namespace PrimerAvancePOO2
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<Componentes> Componentes { get; set; }   

       public DbSet<Proveedor> Proveedor { get; set; }
        
        
    }
}