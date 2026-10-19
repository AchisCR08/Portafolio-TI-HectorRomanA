using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Veterinaria1
{
    public partial class FrmMain : Form
    {
        private readonly Panel dashboardViewPanel = new();
        private Control? currentView;
        private readonly DashboardRepository _dashboardRepo;

        public FrmMain()
        {
            InitializeComponent();
            _dashboardRepo = new DashboardRepository();
            ConfigureDashboardView();
            WireNavigation();
            this.Load += FrmMain_Load;
        }

        private void FrmMain_Load(object? sender, EventArgs e)
        {
            if (SesionActual.EstaAutenticado)
            {
                Text = $"Veterinaria - {SesionActual.NombrePersona} - {SesionActual.NombreSucursal}";
            }

            // Actualizar encabezado con fecha real y sucursal
            lblDate.Text = DateTime.Now.ToString("dddd, dd 'de' MMMM 'de' yyyy",
                new System.Globalization.CultureInfo("es-CR"));
            lblSubtitle.Text = $"Resumen operativo del día — {SesionActual.NombreSucursal}";

            // Ocultar card de "Stock bajo"
            panelCardIngresos.Visible = false;

            // Ocultar fila de stock del resumen (lblStockTitle no tiene valor, se deja vacío)
            lblStockTitle.Visible = false;

            AplicarPermisosMenu();
            CargarDashboard();
            ShowDashboard();
        }

        private void AplicarPermisosMenu()
        {
            bool esAdministrador = string.Equals(
                SesionActual.NombreRol.Trim(),
                "Administrador",
                StringComparison.OrdinalIgnoreCase);

            OpcionToolStripMenuItem.Visible = esAdministrador;
        }

        private void CargarDashboard()
        {
            try
            {
                int idSucursal = SesionActual.IdSucursal;

                // ── KPIs ──────────────────────────────────────────────
                var kpi = _dashboardRepo.ObtenerKPI(idSucursal);

                lblVentasValue.Text = $"₡{kpi.TotalVentas:N2}";
                lblComprasValue.Text = $"₡{kpi.TotalCompras:N2}";
                lblCitasValue.Text = $"{kpi.CitasHoy} Citas";

                lblVetValue.Text = kpi.TotalVeterinarios.ToString();
                lblHospValue.Text = kpi.TotalMascotas.ToString();

                // ── Grid de citas de hoy ──────────────────────────────
                CargarGrillaCitas(idSucursal);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar el dashboard: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void CargarGrillaCitas(int idSucursal)
        {
            dgvCitas.Columns.Clear();

            var citas = _dashboardRepo.ObtenerCitasHoy(idSucursal).ToList();

            // Configurar columnas
            dgvCitas.Columns.Add(new DataGridViewTextBoxColumn { Name = "Tipo", HeaderText = "Tipo", Width = 100 });
            dgvCitas.Columns.Add(new DataGridViewTextBoxColumn { Name = "Hora", HeaderText = "Hora", Width = 80 });
            dgvCitas.Columns.Add(new DataGridViewTextBoxColumn { Name = "Paciente", HeaderText = "Paciente", Width = 140 });
            dgvCitas.Columns.Add(new DataGridViewTextBoxColumn { Name = "Veterinario", HeaderText = "Responsable", Width = 160 });
            dgvCitas.Columns.Add(new DataGridViewTextBoxColumn { Name = "Motivo", HeaderText = "Motivo / Servicio", AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill });
            dgvCitas.Columns.Add(new DataGridViewTextBoxColumn { Name = "Estado", HeaderText = "Estado", Width = 110 });

            // Estilo encabezado
            dgvCitas.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(248, 250, 252);
            dgvCitas.ColumnHeadersDefaultCellStyle.ForeColor = Color.FromArgb(71, 85, 105);
            dgvCitas.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 8.5f, FontStyle.Bold);
            dgvCitas.EnableHeadersVisualStyles = false;

            if (citas.Count == 0)
            {
                // Mostrar mensaje cuando no hay citas
                dgvCitas.Rows.Add("—", "—", "Sin citas programadas hoy", "", "", "");
            }
            else
            {
                foreach (var c in citas)
                {
                    string horaStr = DateTime.Today.Add(c.Hora).ToString("hh:mm tt");
                    dgvCitas.Rows.Add(c.TipoCita, horaStr, c.Paciente, c.Veterinario, c.Motivo ?? "—", c.Estado);
                }
            }

            // Colorear columna Estado
            dgvCitas.CellFormatting += DgvCitas_CellFormatting;
        }

        private void ConfigureDashboardView()
        {
            dashboardViewPanel.Name = "dashboardViewPanel";
            dashboardViewPanel.BackColor = mainPanel.BackColor;
            dashboardViewPanel.Dock = DockStyle.Fill;

            Control[] dashboardControls =
            [
                tableLayoutPanelContent,
                tableLayoutPanelKPIs,
                panelHeader
            ];

            foreach (Control control in dashboardControls)
            {
                mainPanel.Controls.Remove(control);
                dashboardViewPanel.Controls.Add(control);
            }
        }

        private void WireNavigation()
        {
            inicioToolStripMenuItem.Click += (_, _) => ShowDashboard();
            dashboardButton.Click += (_, _) => ShowDashboard();

            usuariosToolStripMenuItem.Click += (_, _) => ShowFormView(new FrmUsuarios());

            personasToolStripMenuItem.Click += (_, _) => ShowFormView(new FrmGestionClientes(0));
            mascotasToolStripMenuItem.Click += (_, _) => ShowFormView(new FrmGestionClientes(1));
            dueñosToolStripMenuItem.Click += (_, _) => ShowFormView(new FrmGestionClientes(2));
            citasVeterinariaToolStripMenuItem.Click += (_, _) => ShowFormView(new FrmCitasVeterinaria());

            citasGroomingToolStripMenuItem.Click += (_, _) => ShowFormView(new FrmCitasGrooming());
            vacunasToolStripMenuItem.Click += (_, _) => ShowFormView(new FrmVacunas1());
            historialMedicoToolStripMenuItem.Click += (_, _) => ShowFormView(new FrmHistorialMedico1());
            productosToolStripMenuItem.Click += (_, _) => ShowFormView(new FrmProducto());
            categoriasToolStripMenuItem.Click += (_, _) => ShowFormView(new FrmCategoria());
            proveedoresToolStripMenuItem.Click += (_, _) => ShowFormView(new frmProveedores());
            inventarioSucursalToolStripMenuItem.Click += (_, _) => ShowFormView(new FrmInventario());
            facturacionToolStripMenuItem.Click += (_, _) => ShowFormView(new FrmFacturacion());
            detalleFacturaToolStripMenuItem.Click += (_, _) => ShowFormView(new Detalle_Ventas());
            comprasToolStripMenuItem.Click += (_, _) => ShowFormView(new FrmCompras());
            detalleComprasToolStripMenuItem.Click += (_, _) => ShowFormView(new Detalle_Compras());
            
            configuracionToolStripMenuItem.Click += (_, _) => ShowPlaceholderView("Configuracion", "Parametros del sistema");

            mascotasButton.Click += (_, _) => ShowFormView(new FrmGestionClientes(1));
            citasButton.Click += (_, _) => ShowPlaceholderView("Citas", "Agenda de citas");
            facturacionButton.Click += (_, _) => ShowFormView(new FrmFacturacion());
            inventarioButton.Click += (_, _) => ShowFormView(new FrmInventario());
            comprasButton.Click += (_, _) => ShowFormView(new FrmCompras());
            reportesButton.Click += (_, _) => ShowFormView(new FrmReportes());

            btnCompra.Click += (_, _) => ShowFormView(new FrmCompras());
        }

        private void ShowDashboard()
        {
            ShowControlView(dashboardViewPanel, disposePreviousView: true);
            SetActiveNavigationButton(dashboardButton);
            CargarDashboard();   // refrescar datos cada vez que se vuelve al dashboard
        }

        private void ShowFormView(Form form)
        {
            form.TopLevel = false;
            form.FormBorderStyle = FormBorderStyle.None;
            form.Dock = DockStyle.Fill;
            ShowControlView(form, disposePreviousView: true);
            form.Show();
            SetActiveNavigationButton(null);
        }

        private void ShowPlaceholderView(string title, string subtitle)
        {
            ShowControlView(new PlaceholderView(title, subtitle), disposePreviousView: true);
            SetActiveNavigationButton(GetNavigationButton(title));
        }

        private void ShowControlView(Control view, bool disposePreviousView)
        {
            if (currentView == view)
                return;

            if (currentView != null)
            {
                mainPanel.Controls.Remove(currentView);
                if (disposePreviousView && currentView != dashboardViewPanel)
                    currentView.Dispose();
            }

            currentView = view;
            view.Dock = DockStyle.Fill;
            mainPanel.Controls.Clear();
            mainPanel.Controls.Add(view);
            view.BringToFront();
        }

        private Button? GetNavigationButton(string title)
        {
            return title switch
            {
                "Mascotas" => mascotasButton,
                "Citas" or "Citas Veterinaria" => citasButton,
                "Facturacion" => facturacionButton,
                "Inventario" or "Productos" or "Categorias" or "Proveedores" or "Inventario Sucursal" => inventarioButton,
                "Compras" => comprasButton,
                "Reportes" => reportesButton,
                _ => null
            };
        }

        private void SetActiveNavigationButton(Button? activeButton)
        {
            foreach (Control control in flowNavPanel.Controls)
            {
                if (control is not Button button) continue;
                bool isActive = button == activeButton;
                button.BackColor = isActive ? Color.FromArgb(14, 165, 233) : Color.Transparent;
                button.Font = new Font("Segoe UI", 10F, isActive ? FontStyle.Bold : FontStyle.Regular);
            }
        }

        private void DgvCitas_CellFormatting(object? sender, DataGridViewCellFormattingEventArgs e)
        {
            var estadoCol = dgvCitas.Columns["Estado"];
            if (estadoCol == null || e.ColumnIndex != estadoCol.Index || e.Value == null || e.CellStyle == null)
                return;

            string status = e.Value.ToString() ?? "";
            (Color fg, Color sel) = status switch
            {
                var s when s.Contains("Complet", StringComparison.OrdinalIgnoreCase)
                    => (Color.FromArgb(13, 148, 136), Color.FromArgb(13, 148, 136)),
                var s when s.Contains("Progreso", StringComparison.OrdinalIgnoreCase) || s.Contains("Proceso", StringComparison.OrdinalIgnoreCase)
                    => (Color.FromArgb(217, 119, 6), Color.FromArgb(217, 119, 6)),
                var s when s.Contains("Pendiente", StringComparison.OrdinalIgnoreCase)
                    => (Color.FromArgb(37, 99, 235), Color.FromArgb(37, 99, 235)),
                _ => (Color.FromArgb(71, 85, 105), Color.FromArgb(71, 85, 105))
            };

            e.CellStyle.ForeColor = fg;
            e.CellStyle.SelectionForeColor = sel;
        }

        // ── Handlers vacíos del Designer ─────────────────────────────
        private void panelCardVentas_Paint(object sender, PaintEventArgs e) { }
        private void panelCardVentasAccent_Paint(object sender, PaintEventArgs e) { }
        private void mascotasToolStripMenuItem_Click(object sender, EventArgs e) { }
        private void personasToolStripMenuItem_Click(object sender, EventArgs e) { }
        private void lblTitle_Click(object sender, EventArgs e) { }
        private void sucursalesToolStripMenuItem_Click(object sender, EventArgs e) { }
        private void lblIngresosTitle_Click(object sender, EventArgs e) { }
        private void lblVentasTrend_Click(object sender, EventArgs e) { }
        private void lblSubtitle_Click(object sender, EventArgs e) { }
        private void lblComprasValue_Click(object sender, EventArgs e) { }
        private void dgvCitas_CellContentClick(object sender, DataGridViewCellEventArgs e) { }
        private void mascotasButton_Click(object sender, EventArgs e) { }
        private void usuariosToolStripMenuItem_Click(object sender, EventArgs e) => ShowFormView(new FrmUsuarios());
        private void tableLayoutPanelKPIs_Paint(object sender, PaintEventArgs e) { }
        private void productosToolStripMenuItem_Click(object sender, EventArgs e) { }

        private void lblSummaryTitle_Click(object sender, EventArgs e)
        {

        }

        private void proveedoresToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void btnNewInvoice_Click(object sender, EventArgs e)
        {
            ShowFormView(new FrmFacturacion());
        }

        private void panelHeader_Paint(object sender, PaintEventArgs e)
        {

        }

        private void configuracionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void sucursaToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void categoriasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowFormView(new FrmCategoria());
        }

        private void menuStripMain_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
        }
    }
}
