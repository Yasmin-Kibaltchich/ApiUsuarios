using Microsoft.EntityFrameworkCore;
using ApiUsuarios.Models;

namespace ApiUsuarios.Data
{
    public class UsuariosDbContext : DbContext
    {
        public UsuariosDbContext(DbContextOptions<UsuariosDbContext> options) : base(options) {}

        public DbSet<Usuarios> Usuarios { get; set; }        
        public DbSet<Escolaridade> Escolaridade { get; set; }
    }
    
    
}   

