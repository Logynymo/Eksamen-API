using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Eksamen_API.Models
{
    /// <summary>
    /// ZipCityContext which handles Database handling/creation.
    /// ZipCityContext inherits DbContext.
    /// </summary>
    public partial class ZipCityContext : DbContext
    {
        /// <summary>
        /// Default Constructor.
        /// </summary>
        public ZipCityContext()
        {
        }

        /// <summary>
        /// Constructor makes a DbContextOptions called options containing options for ZipCityContext.
        /// "Base" allows further configuration of the options.
        /// </summary>
        /// <param name="options"></param>
        public ZipCityContext(DbContextOptions<ZipCityContext> options)
            : base(options)
        {
        }

        /// <summary>
        /// public Virtual DbSet which is used to translate queries into the database, in this case it is for MigrationHistory,
        /// that is also why its get;set;.
        /// public Virtual DbSet which is used to translate queries into the database, in this case it is for ZipCity,
        /// that is also why its get;set;.
        /// </summary>
        public virtual DbSet<MigrationHistory> MigrationHistory { get; set; }
        public virtual DbSet<ZipCity> ZipCity { get; set; }

        /// <summary>
        /// Protected override called Onconfiguring.
        /// The if checks if the optionsBuilder is configured in this case, if it isn't it will set the connection string with UseSqlServer.
        /// </summary>
        /// <param name="optionsBuilder">Contains a simple API surface for configuring DbContextOptionsBuilder.</param>
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=CV-BB-5852;Database=ZipCity;Trusted_Connection=True;");
            }
        }

        /// <summary>
        /// Protected override called OnModelCreating.
        /// modelBuilder.HasAnnotation adds or updates an annotation of the model.
        /// Then it creates the MigrationHistory for the database with the proper properties.
        /// Then it creates ZipCity for the database with its proper properties.
        /// </summary>
        /// <param name="modelBuilder">instance of Modelbuilder called modelBuilder. It helps define the shapes of the entities,
        /// and relationships betweeen eachother and how they map in the Database.</param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("ProductVersion", "2.2.4-servicing-10062");

            modelBuilder.Entity<MigrationHistory>(entity =>
            {
                entity.HasKey(e => new { e.MigrationId, e.ContextKey })
                    .HasName("PK_dbo.__MigrationHistory");

                entity.ToTable("__MigrationHistory");

                entity.Property(e => e.MigrationId).HasMaxLength(150);

                entity.Property(e => e.ContextKey).HasMaxLength(300);

                entity.Property(e => e.Model).IsRequired();

                entity.Property(e => e.ProductVersion)
                    .IsRequired()
                    .HasMaxLength(32);
            });

            modelBuilder.Entity<ZipCity>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasMaxLength(15)
                    .ValueGeneratedNever();

                entity.Property(e => e.CityName)
                    .IsRequired()
                    .HasMaxLength(100);
            });
        }
    }
}
