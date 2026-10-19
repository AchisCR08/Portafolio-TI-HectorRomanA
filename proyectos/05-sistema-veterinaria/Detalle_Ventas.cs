using Entidades;
using iText.IO.Font.Constants;
using iText.Kernel.Colors;
using iText.Kernel.Font;
using iText.Kernel.Geom;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Properties;
using System;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace Veterinaria1
{
    public partial class Detalle_Ventas : Form
    {
        private readonly DetalleVentasRepository _repo;
        private FacturaVista? _facturaSeleccionada;

        public Detalle_Ventas()
        {
            InitializeComponent();
            _repo = new DetalleVentasRepository();

            // Configurar grillas antes del Load
            ConfigurarGrillaFacturas();
            ConfigurarGrillaDetalle();

            // Valores por defecto de los date pickers
            dtpFechaInicio.Value = DateTime.Today.AddMonths(-1);
            dtpFechaFin.Value    = DateTime.Today;

            // Texto del botón PDF
            btnDescargarPDF.Text    = "📄 Descargar PDF";
            btnDescargarPDF.Enabled = false;

            // Eventos
            this.Load                 += Detalle_Ventas_Load;
            btnBuscar.Click           += BtnBuscar_Click;
            btnLimpiar.Click          += BtnLimpiar_Click;
            dgvFacturas.SelectionChanged += DgvFacturas_SelectionChanged;
            btnDescargarPDF.Click     += BtnDescargarPDF_Click;
        }

        // ─────────────────────────────────────────────────────────────────
        // CARGA INICIAL
        // ─────────────────────────────────────────────────────────────────

        private void Detalle_Ventas_Load(object sender, EventArgs e)
        {
            BuscarFacturas();
        }

        // ─────────────────────────────────────────────────────────────────
        // CONFIGURACIÓN DE GRILLAS
        // ─────────────────────────────────────────────────────────────────

        private void ConfigurarGrillaFacturas()
        {
            dgvFacturas.ReadOnly             = true;
            dgvFacturas.SelectionMode        = DataGridViewSelectionMode.FullRowSelect;
            dgvFacturas.MultiSelect          = false;
            dgvFacturas.AllowUserToAddRows   = false;
            dgvFacturas.AutoGenerateColumns  = false;
            dgvFacturas.RowHeadersVisible    = false;
            dgvFacturas.BackgroundColor      = System.Drawing.Color.White;
            dgvFacturas.BorderStyle          = BorderStyle.None;
            dgvFacturas.CellBorderStyle      = DataGridViewCellBorderStyle.SingleHorizontal;
            dgvFacturas.GridColor            = System.Drawing.Color.FromArgb(230, 230, 230);

            dgvFacturas.Columns.Add(new DataGridViewTextBoxColumn { Name = "ColId",      HeaderText = "#",        Width = 55,  DataPropertyName = "IdFactura"     });
            dgvFacturas.Columns.Add(new DataGridViewTextBoxColumn { Name = "ColFecha",   HeaderText = "Fecha",    Width = 95,  DataPropertyName = "Fecha"          });
            dgvFacturas.Columns.Add(new DataGridViewTextBoxColumn { Name = "ColCliente", HeaderText = "Cliente",  AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill, DataPropertyName = "PersonaNombre" });
            dgvFacturas.Columns.Add(new DataGridViewTextBoxColumn { Name = "ColTotal",   HeaderText = "Total ₡",  Width = 100, DataPropertyName = "Total"          });

            // Formato de columnas
            dgvFacturas.Columns["ColTotal"]!.DefaultCellStyle.Format     = "N2";
            dgvFacturas.Columns["ColTotal"]!.DefaultCellStyle.Alignment  = DataGridViewContentAlignment.MiddleRight;
            dgvFacturas.Columns["ColFecha"]!.DefaultCellStyle.Alignment  = DataGridViewContentAlignment.MiddleCenter;
            dgvFacturas.Columns["ColId"]!.DefaultCellStyle.Alignment     = DataGridViewContentAlignment.MiddleCenter;

            EstilizarEncabezado(dgvFacturas);
        }

        private void ConfigurarGrillaDetalle()
        {
            dgvDetalle.ReadOnly             = true;
            dgvDetalle.SelectionMode        = DataGridViewSelectionMode.FullRowSelect;
            dgvDetalle.AllowUserToAddRows   = false;
            dgvDetalle.AutoGenerateColumns  = false;
            dgvDetalle.RowHeadersVisible    = false;
            dgvDetalle.BackgroundColor      = System.Drawing.Color.White;
            dgvDetalle.BorderStyle          = BorderStyle.None;
            dgvDetalle.CellBorderStyle      = DataGridViewCellBorderStyle.SingleHorizontal;
            dgvDetalle.GridColor            = System.Drawing.Color.FromArgb(230, 230, 230);

            dgvDetalle.Columns.Add(new DataGridViewTextBoxColumn { Name = "DColTipo",    HeaderText = "Tipo",        Width = 130, DataPropertyName = "TipoLinea"     });
            dgvDetalle.Columns.Add(new DataGridViewTextBoxColumn { Name = "DColDesc",    HeaderText = "Descripción", AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill, DataPropertyName = "Descripcion" });
            dgvDetalle.Columns.Add(new DataGridViewTextBoxColumn { Name = "DColCant",    HeaderText = "Cant.",       Width = 55,  DataPropertyName = "Cantidad"      });
            dgvDetalle.Columns.Add(new DataGridViewTextBoxColumn { Name = "DColPrecio",  HeaderText = "Precio ₡",    Width = 100, DataPropertyName = "PrecioUnitario" });
            dgvDetalle.Columns.Add(new DataGridViewTextBoxColumn { Name = "DColSubtotal",HeaderText = "Subtotal ₡",  Width = 110, DataPropertyName = "Subtotal"      });

            dgvDetalle.Columns["DColPrecio"]!.DefaultCellStyle.Format    = "N2";
            dgvDetalle.Columns["DColSubtotal"]!.DefaultCellStyle.Format  = "N2";
            dgvDetalle.Columns["DColCant"]!.DefaultCellStyle.Alignment   = DataGridViewContentAlignment.MiddleCenter;
            dgvDetalle.Columns["DColPrecio"]!.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvDetalle.Columns["DColSubtotal"]!.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            EstilizarEncabezado(dgvDetalle);
        }

        private static void EstilizarEncabezado(DataGridView dgv)
        {
            dgv.ColumnHeadersDefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(37, 99, 235);
            dgv.ColumnHeadersDefaultCellStyle.ForeColor = System.Drawing.Color.White;
            dgv.ColumnHeadersDefaultCellStyle.Font      = new System.Drawing.Font("Segoe UI", 9f, System.Drawing.FontStyle.Bold);
            dgv.EnableHeadersVisualStyles               = false;
            dgv.ColumnHeadersHeight                     = 32;
        }

        // ─────────────────────────────────────────────────────────────────
        // BÚSQUEDA DE FACTURAS
        // ─────────────────────────────────────────────────────────────────

        private void BuscarFacturas()
        {
            try
            {
                var facturas = _repo.ListarFacturas(
                    SesionActual.IdSucursal,
                    dtpFechaInicio.Value.Date,
                    dtpFechaFin.Value.Date).ToList();

                dgvFacturas.DataSource = facturas;
                LimpiarDetalle();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al buscar facturas:\n{ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void LimpiarDetalle()
        {
            _facturaSeleccionada    = null;
            lblNumFactura.Text      = "Factura N°: —";
            lblFecha.Text           = "Fecha: —";
            lblCliente.Text         = "Cliente: —";
            lblSucursal.Text        = "Sucursal: —";
            lblTotalValue.Text      = "Total: ₡0.00";
            dgvDetalle.DataSource   = null;
            btnDescargarPDF.Enabled = false;
        }

        // ─────────────────────────────────────────────────────────────────
        // SELECCIÓN DE FACTURA → CARGAR DETALLE
        // ─────────────────────────────────────────────────────────────────

        private void DgvFacturas_SelectionChanged(object? sender, EventArgs e)
        {
            if (dgvFacturas.CurrentRow?.DataBoundItem is not FacturaVista factura)
                return;

            _facturaSeleccionada = factura;

            try
            {
                // Encabezado
                lblNumFactura.Text = $"Factura N°: {factura.IdFactura:D6}";
                lblFecha.Text      = $"Fecha: {factura.Fecha:dd/MM/yyyy}";
                lblCliente.Text    = $"Cliente: {factura.PersonaNombre}";
                lblSucursal.Text   = $"Sucursal: {factura.SucursalNombre}";
                lblTotalValue.Text = $"Total: ₡{factura.Total:N2}";

                // Líneas de detalle
                var lineas = _repo.ObtenerDetalle(factura.IdFactura).ToList();
                dgvDetalle.DataSource = lineas;

                // Pagos — mostrar en lblTotalValue con el método de pago
                var pagos = _repo.ListarPagos(factura.IdFactura).ToList();
                if (pagos.Any())
                {
                    var pago = pagos.First();
                    lblTotalValue.Text = $"Total: ₡{factura.Total:N2}   |   Pago: {pago.TipoPagoNombre}";
                }

                btnDescargarPDF.Enabled = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar detalle:\n{ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        // ─────────────────────────────────────────────────────────────────
        // BOTONES
        // ─────────────────────────────────────────────────────────────────

        private void BtnBuscar_Click(object? sender, EventArgs e) => BuscarFacturas();

        private void BtnLimpiar_Click(object? sender, EventArgs e)
        {
            dtpFechaInicio.Value = DateTime.Today.AddMonths(-1);
            dtpFechaFin.Value    = DateTime.Today;
            BuscarFacturas();
        }

        private void Desde_Click(object sender, EventArgs e) { }

        // ─────────────────────────────────────────────────────────────────
        // GENERAR PDF
        // ─────────────────────────────────────────────────────────────────

        private void BtnDescargarPDF_Click(object? sender, EventArgs e)
        {
            if (_facturaSeleccionada == null) return;

            string rutaArchivo = null;

            try
            {
                var lineas = _repo.ObtenerDetalle(_facturaSeleccionada.IdFactura).ToList();
                var pagos  = _repo.ListarPagos(_facturaSeleccionada.IdFactura).ToList();
                var pago   = pagos.FirstOrDefault();

                using var dialog = new SaveFileDialog
                {
                    Title      = "Guardar comprobante",
                    Filter     = "PDF (*.pdf)|*.pdf",
                    FileName   = $"Factura_{_facturaSeleccionada.IdFactura:D6}_{_facturaSeleccionada.Fecha:yyyyMMdd}.pdf",
                    DefaultExt = "pdf"
                };

                if (dialog.ShowDialog() != DialogResult.OK) return;

                rutaArchivo = dialog.FileName;
                GenerarPDF(rutaArchivo, _facturaSeleccionada, lineas, pago);

                MessageBox.Show("✅ Comprobante guardado correctamente.", "PDF generado",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Abrir el archivo automáticamente
                System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo
                {
                    FileName        = rutaArchivo,
                    UseShellExecute = true
                });
            }
            catch (Exception ex)
            {
                if (rutaArchivo != null && File.Exists(rutaArchivo))
                {
                    try { File.Delete(rutaArchivo); }
                    catch { }
                }

                var detalle = ex.InnerException != null
                    ? $"{ex.Message} -> {ex.InnerException.Message}"
                    : ex.Message;

                MessageBox.Show($"Error al generar PDF:\n{detalle}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private static void GenerarPDF(string rutaArchivo, FacturaVista factura,
            System.Collections.Generic.List<DetalleFacturaVista> lineas, PagoVista? pago)
        {
            using var writer   = new PdfWriter(rutaArchivo);
            using var pdf      = new PdfDocument(writer);
            using var document = new Document(pdf, PageSize.A4);

            document.SetMargins(40, 50, 40, 50);

            var fontBold   = PdfFontFactory.CreateFont(StandardFonts.HELVETICA_BOLD);
            var fontNormal = PdfFontFactory.CreateFont(StandardFonts.HELVETICA);
            var azul       = new DeviceRgb(37, 99, 235);
            var grisClaro  = new DeviceRgb(245, 247, 250);
            var noB        = iText.Layout.Borders.Border.NO_BORDER;

            // ── Encabezado ─────────────────────────────────────────────────
            var headerTable = new iText.Layout.Element.Table(
                    UnitValue.CreatePercentArray(new float[] { 70, 30 }))
                .UseAllAvailableWidth();

            headerTable.AddCell(new Cell()
                .Add(new Paragraph("VETERINARIA")
                    .SetFont(fontBold).SetFontSize(20).SetFontColor(azul))
                .Add(new Paragraph("Comprobante de Venta")
                    .SetFont(fontNormal).SetFontSize(11).SetFontColor(ColorConstants.GRAY))
                .SetBorder(noB));

            headerTable.AddCell(new Cell()
                .Add(new Paragraph("Factura N°")
                    .SetFont(fontNormal).SetFontSize(9).SetFontColor(ColorConstants.GRAY)
                    .SetTextAlignment(TextAlignment.RIGHT))
                .Add(new Paragraph($"{factura.IdFactura:D6}")
                    .SetFont(fontBold).SetFontSize(16).SetFontColor(azul)
                    .SetTextAlignment(TextAlignment.RIGHT))
                .SetBorder(noB));

            document.Add(headerTable);

            // Línea separadora usando SolidLine (DashedLine fue removido en iText7 v8)
            document.Add(new LineSeparator(new iText.Kernel.Pdf.Canvas.Draw.SolidLine(1.5f))
                .SetMarginTop(6).SetMarginBottom(14));

            // ── Info factura ────────────────────────────────────────────────
            var infoTable = new iText.Layout.Element.Table(
                    UnitValue.CreatePercentArray(new float[] { 50, 50 }))
                .UseAllAvailableWidth()
                .SetMarginBottom(16);

            infoTable.AddCell(InfoCell(fontBold, fontNormal, noB, "Fecha de emisión", factura.Fecha.ToString("dd/MM/yyyy")));
            infoTable.AddCell(InfoCell(fontBold, fontNormal, noB, "Sucursal",         factura.SucursalNombre));
            infoTable.AddCell(InfoCell(fontBold, fontNormal, noB, "Cliente",          factura.PersonaNombre));
            infoTable.AddCell(InfoCell(fontBold, fontNormal, noB, "Método de pago",   pago?.TipoPagoNombre ?? "—"));

            document.Add(infoTable);

            // ── Tabla de detalle ────────────────────────────────────────────
            var detTable = new iText.Layout.Element.Table(
                    UnitValue.CreatePercentArray(new float[] { 18, 42, 10, 15, 15 }))
                .UseAllAvailableWidth()
                .SetMarginBottom(10);

            // Encabezados
            foreach (var h in new[] { "Tipo", "Descripción", "Cant.", "Precio ₡", "Subtotal ₡" })
            {
                detTable.AddHeaderCell(new Cell()
                    .Add(new Paragraph(h).SetFont(fontBold).SetFontSize(9)
                        .SetFontColor(ColorConstants.WHITE))
                    .SetBackgroundColor(azul)
                    .SetPadding(6)
                    .SetBorder(noB));
            }

            // Filas alternadas
            bool altRow = false;
            foreach (var l in lineas)
            {
                var bg = altRow ? (iText.Kernel.Colors.Color)grisClaro : ColorConstants.WHITE;
                detTable.AddCell(FilaCell(l.TipoLinea,                fontNormal, 8, bg, noB, TextAlignment.LEFT));
                detTable.AddCell(FilaCell(l.Descripcion,              fontNormal, 8, bg, noB, TextAlignment.LEFT));
                detTable.AddCell(FilaCell(l.Cantidad.ToString(),       fontNormal, 8, bg, noB, TextAlignment.CENTER));
                detTable.AddCell(FilaCell($"₡{l.PrecioUnitario:N2}",  fontNormal, 8, bg, noB, TextAlignment.RIGHT));
                detTable.AddCell(FilaCell($"₡{l.Subtotal:N2}",        fontNormal, 8, bg, noB, TextAlignment.RIGHT));
                altRow = !altRow;
            }

            document.Add(detTable);

            // ── Total ───────────────────────────────────────────────────────
            // Usamos una tabla de 2 columnas; la celda derecha lleva el total
            var totalTable = new iText.Layout.Element.Table(
                    UnitValue.CreatePercentArray(new float[] { 60, 40 }))
                .UseAllAvailableWidth()
                .SetMarginBottom(20);

            // Celda vacía izquierda
            totalTable.AddCell(new Cell().SetBorder(noB));

            // Celda con línea superior y total — se logra con SolidBorder sólo en top
            totalTable.AddCell(new Cell()
                .Add(new Paragraph($"TOTAL: ₡{factura.Total:N2}")
                    .SetFont(fontBold).SetFontSize(13).SetFontColor(azul)
                    .SetTextAlignment(TextAlignment.RIGHT))
                .SetBorderTop(new iText.Layout.Borders.SolidBorder(azul, 1.5f))
                .SetBorderBottom(noB)
                .SetBorderLeft(noB)
                .SetBorderRight(noB)
                .SetPaddingTop(6));

            document.Add(totalTable);

            // ── Pie de página ───────────────────────────────────────────────
            document.Add(new LineSeparator(new iText.Kernel.Pdf.Canvas.Draw.SolidLine(0.5f))
                .SetMarginBottom(8));
            document.Add(new Paragraph("Gracias por su preferencia. Este documento es un comprobante de su compra.")
                .SetFont(fontNormal).SetFontSize(8).SetFontColor(ColorConstants.GRAY)
                .SetTextAlignment(TextAlignment.CENTER));
            document.Add(new Paragraph($"Generado el {DateTime.Now:dd/MM/yyyy HH:mm}")
                .SetFont(fontNormal).SetFontSize(7).SetFontColor(ColorConstants.GRAY)
                .SetTextAlignment(TextAlignment.CENTER));
        }

        // ── Helpers para celdas PDF ─────────────────────────────────────────

        private static Cell InfoCell(PdfFont fontBold, PdfFont fontNormal,
            iText.Layout.Borders.Border noB, string label, string value)
            => new Cell()
                .Add(new Paragraph(label).SetFont(fontBold).SetFontSize(8).SetFontColor(ColorConstants.GRAY))
                .Add(new Paragraph(value).SetFont(fontNormal).SetFontSize(10))
                .SetBorder(noB)
                .SetPaddingBottom(6);

        private static Cell FilaCell(string text, PdfFont font, float size,
            iText.Kernel.Colors.Color bg, iText.Layout.Borders.Border noB, TextAlignment align)
            => new Cell()
                .Add(new Paragraph(text).SetFont(font).SetFontSize(size).SetTextAlignment(align))
                .SetBackgroundColor(bg)
                .SetPadding(5)
                .SetBorder(noB);
    }
}
