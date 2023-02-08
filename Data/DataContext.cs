using System;
using NOVAteste.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using static NOVAteste.Models.Arquivo;

namespace NOVAteste.Data
{
    public partial class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        public DbSet<Arquivo> Arquivo { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
           => options.UseSqlServer("Server=localhost,1433;Database=testeNOVA;User ID=sa;Password=269545");

        DataContext _dataContext;

        public static SqlConnection Connection;

        // Aqui vamos informar que temos arquivos de mapeamento
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            /*modelBuilder.ApplyConfiguration(new ArquivoMap());*/
        }
    }
}
