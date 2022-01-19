using BootstrapBlazor.Components;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.EntityFrameworkCore;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

#nullable disable

namespace IncomeCalculator.Data
{
    public partial class BenefitsContext : DbContext
    {
        public BenefitsContext()
        {
        }

        public BenefitsContext(DbContextOptions<BenefitsContext> options)
            : base(options)
        {
        }

        public virtual DbSet<MinWage> MinWages { get; set; }
        public virtual DbSet<ChildTaxCredit> ChildTaxCredits { get; set; }
        public virtual DbSet<WorkingTaxCredit> WorkingTaxCredits { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                 optionsBuilder.UseSqlServer("Server=.\\SQLEXPRESS; Database=Benefits; Trusted_Connection=True; MultipleActiveResultSets=true;");
                //Azure connection string is in secrets.json
             }
}
protected override void OnModelCreating(ModelBuilder modelBuilder)
{
            modelBuilder.HasAnnotation("Relational:Collation", "Latin1_General_CI_AS");

            modelBuilder.Entity<MinWage>(entity =>
            {
                entity.Property(e => e.TaxYear).HasColumnType("date");
            });

            modelBuilder.Entity<ChildTaxCredit>(entity =>
            {
                entity.Property(ctc => ctc.TaxYear).HasColumnType("date");
            });

            modelBuilder.Entity<WorkingTaxCredit>(entity =>
            {
                entity.Property(wtc => wtc.TaxYear).HasColumnType("date");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
