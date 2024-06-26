using Microsoft.EntityFrameworkCore;
using Servicos;


namespace TP_MS_VENDA
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }

        public DbSet<EntidadeVenda> Venda { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<EntidadeVenda>().HasKey(p => p.Id);

            base.OnModelCreating(modelBuilder);
        }
    }
}
