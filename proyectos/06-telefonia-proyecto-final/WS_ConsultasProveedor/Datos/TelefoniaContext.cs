using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;

namespace WS_ConsultasProveedor.Datos
{
    public class TelefoniaContext : DbContext
    {
        public TelefoniaContext() : base("name=TelefoniaContext")
        {
        }

        public DbSet<Cuenta> Cuentas { get; set; }

        public DbSet<FacturaPostpago> FacturasPostpago { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Cuenta>().ToTable("Cuentas", "dbo");
            modelBuilder.Entity<Cuenta>().HasKey(c => c.NumeroTelefono);

            modelBuilder.Entity<FacturaPostpago>().ToTable("FacturasPostpago", "dbo");
            modelBuilder.Entity<FacturaPostpago>().HasKey(f => f.Id);
        }
    }

    [Table("Cuentas", Schema = "dbo")]
    public class Cuenta
    {
        [Key]
        [Column("numero_telefono")]
        public string NumeroTelefono { get; set; }

        [Column("tipo_servicio")]
        public string TipoServicio { get; set; }

        [Column("saldo")]
        public decimal Saldo { get; set; }

        [Column("proveedor")]
        public string Proveedor { get; set; }

        [Column("bono_mismo_proveedor")]
        public decimal BonoMismoProveedor { get; set; }

        [Column("numero_telefono_cifrado")]
        public string NumeroTelefonoCifrado { get; set; }

        [Column("identificador_telefono")]
        public string IdentificadorTelefono { get; set; }

        [Column("identificador_tarjeta")]
        public string IdentificadorTarjeta { get; set; }

        [Column("identificacion_duenno")]
        public string IdentificacionDuenno { get; set; }

        [Column("estado")]
        public string Estado { get; set; }

        [Column("fecha_activacion")]
        public DateTime? FechaActivacion { get; set; }

        [Column("fecha_desactivacion")]
        public DateTime? FechaDesactivacion { get; set; }

        [Column("saldo_postpago")]
        public decimal SaldoPostpago { get; set; }

        [Column("fecha_maxima_pago")]
        public DateTime? FechaMaximaPago { get; set; }
    }

    [Table("FacturasPostpago", Schema = "dbo")]
    public class FacturaPostpago
    {
        [Key]
        [Column("id")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Column("numero_telefono")]
        public string NumeroTelefono { get; set; }

        [Column("fecha_calculo")]
        public DateTime FechaCalculo { get; set; }

        [Column("fecha_maxima_pago")]
        public DateTime FechaMaximaPago { get; set; }

        [Column("monto")]
        public decimal Monto { get; set; }

        [Column("estado")]
        public string Estado { get; set; }

        [Column("fecha_registro")]
        public DateTime FechaRegistro { get; set; }
    }
}
