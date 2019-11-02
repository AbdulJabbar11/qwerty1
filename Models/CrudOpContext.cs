using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using qwerty1.Models;

namespace qwerty1.Models
{
    public partial class CrudOpContext : DbContext
    {
        public CrudOpContext()
        {
        }

        public CrudOpContext(DbContextOptions<CrudOpContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Airline> Airline { get; set; }
        public virtual DbSet<Flight> Flight { get; set; }
        public virtual DbSet<SystemUser> SystemUser { get; set; }

        //        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //        {
        //            if (!optionsBuilder.IsConfigured)
        //            {
        //#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
        //                optionsBuilder.UseSqlServer("Server=ABDULJABBARGUJJ\\MSSQLSERVR;Database=CrudOp;Trusted_Connection=True;User ID=sa; Password=jabbar786;");
        //            }
        //        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("ProductVersion", "2.2.6-servicing-10079");

            modelBuilder.Entity<Airline>(entity =>
            {
                

                entity.Property(e => e.Country)
                    .HasColumnName("country")
                    .HasMaxLength(50);

                entity.Property(e => e.Date)
                    .HasColumnName("date")
                    .HasColumnType("date");

                entity.Property(e => e.Name)
                    .HasColumnName("name")
                    .HasMaxLength(50);

                entity.Property(e => e.ShortCode)
                    .HasColumnName("short_code")
                    .HasMaxLength(50);

                entity.Property(e => e.Status)
                    .HasColumnName("status")
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Flight>(entity =>
            {
                

                entity.Property(e => e.AirlineId).HasColumnName("airline_id");

                entity.Property(e => e.ArrivalDateTime)
                    .HasColumnName("arrival_date_time")
                    .HasColumnType("datetime");

                entity.Property(e => e.DepartureDateTime)
                    .HasColumnName("departure_date_time")
                    .HasColumnType("datetime");

                entity.Property(e => e.Destination)
                    .HasColumnName("destination")
                    .HasMaxLength(50);

                entity.Property(e => e.Source)
                    .HasColumnName("source")
                    .HasMaxLength(50);
            });
            modelBuilder.Entity<SystemUser>(entity =>
            {


                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Username)
                    .HasColumnName("UserName")
                    .HasColumnType("nvarchar(50)");

                entity.Property(e => e.Password)
                    .HasColumnName("Password")
                    .HasColumnType("nvarchar(50)");

                entity.Property(e => e.Role)
                    .HasColumnName("Role")
                    .HasMaxLength(50);

                entity.Property(e => e.Status)
                    .HasColumnName("status")
                    .HasMaxLength(50);

                entity.Property(e => e.Email)
                    .HasColumnName("email")
                    .HasMaxLength(50);


            });
        }

//        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//        {
//            if (!optionsBuilder.IsConfigured)
//            {
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
//                optionsBuilder.UseSqlServer("Server=ABDULJABBARGUJJ\\MSSQLSERVR;Database=CrudOp;Trusted_Connection=True;User ID=sa; Password=jabbar786;");
//            }
//        }

        
    }
}
