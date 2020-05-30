using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KhaalminateCore.Models
{
#pragma warning disable KApp
    public partial class Invoice
    {
        public Invoice()
        {

        }
        public Invoice(int? invid)
        {
            Invid = invid;
        }
        public int? Invid { get; set; }
        public string Invno { get; set; }
        public string POno { get; set; }
        public DateTime? PODate { get; set; }
        public DateTime? InvDate { get; set; }
        public decimal? InvAmt { get; set; }
        public string FileNo { get; set; }
        public string FromAddr { get; set; }
        public string ToAddr { get; set; }
        public string ShipAddr { get; set; }
        public decimal? InvAmtBT { get; set; }
        public decimal? CGST { get; set; }
        public decimal? SGST { get; set; }
        public decimal? TGST { get; set; }
        public string AcctDet { get; set; }
        public string Declare { get; set; }
        public string Note { get; set; }
        public string SearchDetails { get; set; }
        public string DCNO { get; set; }
        public string DCDate { get; set; }
        public DateTime? InsDate { get; set; }
        public string InsAddr { get; set; }
        public int? BudEsti { get; set; }
        public string QuotNo { get; set; }

    }
    public class InvoiceItemConfiguration : IEntityTypeConfiguration<Invoice>
    {
        public void Configure(EntityTypeBuilder<Invoice> builder)
        {
            //throw new NotImplementedException();
            // Set configuration for entity.
            builder.ToTable("Invoice");

            //set key for entity.
            builder.HasKey(v => v.Invno);

            //Configuration of Table columns
            builder.Property(v => v.Invno).HasColumnType("nVarchar(25)").IsRequired();
            builder.Property(v => v.InvAmt).HasColumnType("decimal(20,2)").IsRequired();
            builder.Property(v => v.InvAmtBT).HasColumnType("decimal(20,2)").IsRequired();
            builder.Property(v => v.CGST).HasColumnType("decimal(20,2)").IsRequired();
            builder.Property(v => v.SGST).HasColumnType("decimal(20,2)").IsRequired();
            builder.Property(v => v.TGST).HasColumnType("decimal(20,2)").IsRequired();
            builder.Property(v => v.POno).HasColumnType("nVarchar(25)").IsRequired();
            builder.Property(v => v.AcctDet).HasColumnType("nVarchar(max)").IsRequired();
            builder.Property(v => v.Declare).HasColumnType("nVarchar(max)").IsRequired();
            builder.Property(v => v.Note).HasColumnType("nVarchar(max)").IsRequired();
            builder.Property(v => v.FileNo).HasColumnType("nVarchar(30)").IsRequired();
            builder.Property(v => v.FromAddr).HasColumnType("nVarchar(MAX)").IsRequired();
            builder.Property(v => v.ToAddr).HasColumnType("nVarchar(MAX)").IsRequired();
            builder.Property(v => v.ShipAddr).HasColumnType("nVarchar(MAX)").IsRequired();
            builder.Property(v => v.DCNO).HasColumnType("nVarchar(25)").IsRequired();
            builder.Property(v => v.QuotNo).HasColumnType("nVarchar(25)").IsRequired();
            builder.Property(v => v.InsAddr).HasColumnType("nVarchar(MAX)").IsRequired();
            builder.Property(v => v.BudEsti).HasColumnType("decimal(20,2)").IsRequired();
            
            //Default values in the column to be entered.
            builder
                .Property(v => v.Invid)
                .HasColumnType("int")
                .IsRequired()
                .HasDefaultValueSql("NEXT VALUE FOR [Sequences].[Invid]");

            // Columns with generated value on add or update
            builder
                .Property(p => p.PODate)
                .HasColumnType("datetime2")
                .IsRequired()
                .ValueGeneratedOnAddOrUpdate();

            builder
                .Property(p => p.InvDate)
                .HasColumnType("datetime2")
                .IsRequired()
                .ValueGeneratedOnAddOrUpdate();

            builder
                .Property(p => p.InsDate)
                .HasColumnType("datetime2")
                .IsRequired()
                .ValueGeneratedOnAddOrUpdate();

            builder
                .Property(p => p.DCDate)
                .HasColumnType("datetime2")
                .IsRequired()
                .ValueGeneratedOnAddOrUpdate();
        }
    }
    public class KhaalminateDbContext : DbContext
    {
        public KhaalminateDbContext(DbContextOptions<KhaalminateDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Apply configurations for entity

            modelBuilder
                .ApplyConfiguration(new InvoiceItemConfiguration());

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Invoice> Invoices { get; set; }
    }
#pragma warning restore KApp
}
