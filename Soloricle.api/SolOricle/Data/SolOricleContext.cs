using Microsoft.EntityFrameworkCore;
using SolOricle.Core.Models;
using SolOricle.Api.Models;

namespace SolOricle.Api.Data
{
    public class SolOricleContext : DbContext
    {
        public SolOricleContext(DbContextOptions<SolOricleContext> options)
            : base(options) { }

   
        public DbSet<JournalEntry> JournalEntries { get; set; }

        public DbSet<Literature> Literature { get; set; }
        public DbSet<AnalysisResult> AnalysisResults { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AnalysisResult>()
                .HasOne(a => a.Literature)
                .WithMany()
                .HasForeignKey(a => a.LiteratureId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
