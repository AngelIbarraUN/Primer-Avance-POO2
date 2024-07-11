
using Microsoft.EntityFrameworkCore;
using PrimerAvancePOO2.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace PrimerAvancePOO2
{
    public class ApplicationDbContext : IdentityDbContext 
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<Componentes> Componentes { get; set; }   

        public DbSet<Proveedor> Proveedor { get; set; }
        
        
    }
}