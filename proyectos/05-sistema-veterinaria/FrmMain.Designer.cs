using System;
using System.Drawing;
using System.Windows.Forms;

namespace Veterinaria1
{
    partial class FrmMain
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            menuStripMain = new MenuStrip();
            inicioToolStripMenuItem = new ToolStripMenuItem();
            OperacionesToolStripMenuItem = new ToolStripMenuItem();
            personasToolStripMenuItem = new ToolStripMenuItem();
            mascotasToolStripMenuItem = new ToolStripMenuItem();
            dueñosToolStripMenuItem = new ToolStripMenuItem();
            ClinicaToolStripMenuItem = new ToolStripMenuItem();
            citasVeterinariaToolStripMenuItem = new ToolStripMenuItem();
            citasGroomingToolStripMenuItem = new ToolStripMenuItem();
            vacunasToolStripMenuItem = new ToolStripMenuItem();
            historialMedicoToolStripMenuItem = new ToolStripMenuItem();
            clinicaToolStripMenuItem1 = new ToolStripMenuItem();
            invToolStripMenuItem = new ToolStripMenuItem();
            productosToolStripMenuItem = new ToolStripMenuItem();
            categoriasToolStripMenuItem = new ToolStripMenuItem();
            proveedoresToolStripMenuItem = new ToolStripMenuItem();
            inventarioSucursalToolStripMenuItem = new ToolStripMenuItem();
            FinanzasToolStripMenuItem = new ToolStripMenuItem();
            facturacionToolStripMenuItem = new ToolStripMenuItem();
            detalleFacturaToolStripMenuItem = new ToolStripMenuItem();
            comprasToolStripMenuItem = new ToolStripMenuItem();
            detalleComprasToolStripMenuItem = new ToolStripMenuItem();
            OpcionToolStripMenuItem = new ToolStripMenuItem();
            usuariosToolStripMenuItem = new ToolStripMenuItem();
            configuracionToolStripMenuItem = new ToolStripMenuItem();
            sidebarPanel = new Panel();
            flowNavPanel = new FlowLayoutPanel();
            dashboardButton = new Button();
            mascotasButton = new Button();
            citasButton = new Button();
            facturacionButton = new Button();
            inventarioButton = new Button();
            comprasButton = new Button();
            reportesButton = new Button();
            panelLogoSeparator = new Panel();
            logoPanel = new Panel();
            lblLogoSubtitle = new Label();
            lblLogo = new Label();
            mainPanel = new Panel();
            tableLayoutPanelContent = new TableLayoutPanel();
            panelCitasDetail = new Panel();
            dgvCitas = new DataGridView();
            lblCitasSubtitle = new Label();
            lblCitasTitle = new Label();
            panelRightLayout = new Panel();
            panelSystemSummary = new Panel();
            tableLayoutPanelMetrics = new TableLayoutPanel();
            lblVetTitle = new Label();
            lblVetValue = new Label();
            lblHospTitle = new Label();
            lblHospValue = new Label();
            lblStockTitle = new Label();
            lblSummarySubtitle = new Label();
            lblSummaryTitle = new Label();
            panelSpacer = new Panel();
            panelActions = new Panel();
            tableLayoutPanelButtons = new TableLayoutPanel();
            btnNewCita = new Button();
            btnNewPet = new Button();
            btnFacturar = new Button();
            btnCompra = new Button();
            lblActionsSubtitle = new Label();
            lblActionsTitle = new Label();
            tableLayoutPanelKPIs = new TableLayoutPanel();
            panelCardIngresos = new Panel();
            lblIngresosIcon = new Label();
            lblIngresosValue = new Label();
            lblIngresosTitle = new Label();
            panelCardIngresosAccent = new Panel();
            panelCardCitas = new Panel();
            lblCitasIcon = new Label();
            lblCitasValue = new Label();
            lblCitasCardTitle = new Label();
            panelCardCitasAccent = new Panel();
            panelCardCompras = new Panel();
            lblComprasIcon = new Label();
            lblComprasValue = new Label();
            lblComprasTitle = new Label();
            panelCardComprasAccent = new Panel();
            panelCardVentas = new Panel();
            lblVentasIcon = new Label();
            lblVentasValue = new Label();
            lblVentasTitle = new Label();
            panelCardVentasAccent = new Panel();
            panelHeader = new Panel();
            lblDate = new Label();
            lblSubtitle = new Label();
            lblTitle = new Label();
            sucursalesToolStripMenuItem1 = new ToolStripMenuItem();
            menuStripMain.SuspendLayout();
            sidebarPanel.SuspendLayout();
            flowNavPanel.SuspendLayout();
            logoPanel.SuspendLayout();
            mainPanel.SuspendLayout();
            tableLayoutPanelContent.SuspendLayout();
            panelCitasDetail.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvCitas).BeginInit();
            panelRightLayout.SuspendLayout();
            panelSystemSummary.SuspendLayout();
            tableLayoutPanelMetrics.SuspendLayout();
            panelActions.SuspendLayout();
            tableLayoutPanelButtons.SuspendLayout();
            tableLayoutPanelKPIs.SuspendLayout();
            panelCardIngresos.SuspendLayout();
            panelCardCitas.SuspendLayout();
            panelCardCompras.SuspendLayout();
            panelCardVentas.SuspendLayout();
            panelHeader.SuspendLayout();
            SuspendLayout();
            // 
            // menuStripMain
            // 
            menuStripMain.BackColor = Color.White;
            menuStripMain.Font = new Font("Segoe UI", 9.75F);
            menuStripMain.ImageScalingSize = new Size(20, 20);
            menuStripMain.Items.AddRange(new ToolStripItem[] { inicioToolStripMenuItem, OperacionesToolStripMenuItem, ClinicaToolStripMenuItem, invToolStripMenuItem, FinanzasToolStripMenuItem, OpcionToolStripMenuItem });
            menuStripMain.Location = new Point(0, 0);
            menuStripMain.Name = "menuStripMain";
            menuStripMain.Padding = new Padding(7, 5, 0, 5);
            menuStripMain.RenderMode = ToolStripRenderMode.Professional;
            menuStripMain.Size = new Size(1371, 46);
            menuStripMain.TabIndex = 0;
            menuStripMain.Text = "menuStripMain";
            menuStripMain.ItemClicked += menuStripMain_ItemClicked;
            // 
            // inicioToolStripMenuItem
            // 
            inicioToolStripMenuItem.ForeColor = Color.FromArgb(51, 65, 85);
            inicioToolStripMenuItem.Image = Properties.Resources.opcion_de_carpeta;
            inicioToolStripMenuItem.ImageScaling = ToolStripItemImageScaling.None;
            inicioToolStripMenuItem.Name = "inicioToolStripMenuItem";
            inicioToolStripMenuItem.Size = new Size(97, 36);
            inicioToolStripMenuItem.Text = "Inicio";
            // 
            // OperacionesToolStripMenuItem
            // 
            OperacionesToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { personasToolStripMenuItem, mascotasToolStripMenuItem, dueñosToolStripMenuItem });
            OperacionesToolStripMenuItem.ForeColor = Color.FromArgb(51, 65, 85);
            OperacionesToolStripMenuItem.Image = Properties.Resources.sumision;
            OperacionesToolStripMenuItem.ImageScaling = ToolStripItemImageScaling.None;
            OperacionesToolStripMenuItem.Name = "OperacionesToolStripMenuItem";
            OperacionesToolStripMenuItem.Size = new Size(151, 36);
            OperacionesToolStripMenuItem.Text = "Operaciones";
            OperacionesToolStripMenuItem.Click += sucursalesToolStripMenuItem_Click;
            // 
            // personasToolStripMenuItem
            // 
            personasToolStripMenuItem.Name = "personasToolStripMenuItem";
            personasToolStripMenuItem.Size = new Size(165, 28);
            personasToolStripMenuItem.Text = "Personas";
            // 
            // mascotasToolStripMenuItem
            // 
            mascotasToolStripMenuItem.Name = "mascotasToolStripMenuItem";
            mascotasToolStripMenuItem.Size = new Size(165, 28);
            mascotasToolStripMenuItem.Text = "Mascotas";
            // 
            // dueñosToolStripMenuItem
            // 
            dueñosToolStripMenuItem.Name = "dueñosToolStripMenuItem";
            dueñosToolStripMenuItem.Size = new Size(165, 28);
            dueñosToolStripMenuItem.Text = "Dueños";
            // 
            // ClinicaToolStripMenuItem
            // 
            ClinicaToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { citasVeterinariaToolStripMenuItem, citasGroomingToolStripMenuItem, vacunasToolStripMenuItem, historialMedicoToolStripMenuItem, clinicaToolStripMenuItem1 });
            ClinicaToolStripMenuItem.ForeColor = Color.FromArgb(51, 65, 85);
            ClinicaToolStripMenuItem.Image = Properties.Resources.huella;
            ClinicaToolStripMenuItem.ImageScaling = ToolStripItemImageScaling.None;
            ClinicaToolStripMenuItem.Name = "ClinicaToolStripMenuItem";
            ClinicaToolStripMenuItem.Size = new Size(106, 36);
            ClinicaToolStripMenuItem.Text = "Clinica";
            ClinicaToolStripMenuItem.Click += personasToolStripMenuItem_Click;
            // 
            // citasVeterinariaToolStripMenuItem
            // 
            citasVeterinariaToolStripMenuItem.Name = "citasVeterinariaToolStripMenuItem";
            citasVeterinariaToolStripMenuItem.Size = new Size(218, 28);
            citasVeterinariaToolStripMenuItem.Text = "Citas Veterinaria";
            // 
            // citasGroomingToolStripMenuItem
            // 
            citasGroomingToolStripMenuItem.Name = "citasGroomingToolStripMenuItem";
            citasGroomingToolStripMenuItem.Size = new Size(218, 28);
            citasGroomingToolStripMenuItem.Text = "Citas Grooming";
            // 
            // vacunasToolStripMenuItem
            // 
            vacunasToolStripMenuItem.Name = "vacunasToolStripMenuItem";
            vacunasToolStripMenuItem.Size = new Size(218, 28);
            vacunasToolStripMenuItem.Text = "Vacunas";
            // 
            // historialMedicoToolStripMenuItem
            // 
            historialMedicoToolStripMenuItem.Name = "historialMedicoToolStripMenuItem";
            historialMedicoToolStripMenuItem.Size = new Size(218, 28);
            historialMedicoToolStripMenuItem.Text = "Historial Medico";
            // 
            // clinicaToolStripMenuItem1
            // 
            clinicaToolStripMenuItem1.Name = "clinicaToolStripMenuItem1";
            clinicaToolStripMenuItem1.Size = new Size(218, 28);
            clinicaToolStripMenuItem1.Text = "Clinica";
            // 
            // invToolStripMenuItem
            // 
            invToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { productosToolStripMenuItem, categoriasToolStripMenuItem, proveedoresToolStripMenuItem, inventarioSucursalToolStripMenuItem });
            invToolStripMenuItem.Font = new Font("Segoe UI", 10.2F, FontStyle.Regular, GraphicsUnit.Point, 0);
            invToolStripMenuItem.ForeColor = Color.FromArgb(51, 65, 85);
            invToolStripMenuItem.Image = Properties.Resources.inventario;
            invToolStripMenuItem.ImageScaling = ToolStripItemImageScaling.None;
            invToolStripMenuItem.Name = "invToolStripMenuItem";
            invToolStripMenuItem.Size = new Size(133, 36);
            invToolStripMenuItem.Text = "Inventario";
            invToolStripMenuItem.Click += mascotasToolStripMenuItem_Click;
            // 
            // productosToolStripMenuItem
            // 
            productosToolStripMenuItem.Name = "productosToolStripMenuItem";
            productosToolStripMenuItem.Size = new Size(239, 28);
            productosToolStripMenuItem.Text = "Productos";
            productosToolStripMenuItem.Click += productosToolStripMenuItem_Click;
            // 
            // categoriasToolStripMenuItem
            // 
            categoriasToolStripMenuItem.Name = "categoriasToolStripMenuItem";
            categoriasToolStripMenuItem.Size = new Size(239, 28);
            categoriasToolStripMenuItem.Text = "Categorias";
            categoriasToolStripMenuItem.Click += categoriasToolStripMenuItem_Click;
            // 
            // proveedoresToolStripMenuItem
            // 
            proveedoresToolStripMenuItem.Name = "proveedoresToolStripMenuItem";
            proveedoresToolStripMenuItem.Size = new Size(239, 28);
            proveedoresToolStripMenuItem.Text = "Proveedores";
            proveedoresToolStripMenuItem.Click += proveedoresToolStripMenuItem_Click;
            // 
            // inventarioSucursalToolStripMenuItem
            // 
            inventarioSucursalToolStripMenuItem.Name = "inventarioSucursalToolStripMenuItem";
            inventarioSucursalToolStripMenuItem.Size = new Size(239, 28);
            inventarioSucursalToolStripMenuItem.Text = "Inventario Sucursal";
            // 
            // FinanzasToolStripMenuItem
            // 
            FinanzasToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { facturacionToolStripMenuItem, detalleFacturaToolStripMenuItem, comprasToolStripMenuItem, detalleComprasToolStripMenuItem });
            FinanzasToolStripMenuItem.ForeColor = Color.FromArgb(51, 65, 85);
            FinanzasToolStripMenuItem.Image = Properties.Resources.contabilidad;
            FinanzasToolStripMenuItem.ImageScaling = ToolStripItemImageScaling.None;
            FinanzasToolStripMenuItem.Name = "FinanzasToolStripMenuItem";
            FinanzasToolStripMenuItem.Size = new Size(121, 36);
            FinanzasToolStripMenuItem.Text = "Finanzas";
            // 
            // facturacionToolStripMenuItem
            // 
            facturacionToolStripMenuItem.Name = "facturacionToolStripMenuItem";
            facturacionToolStripMenuItem.Size = new Size(220, 28);
            facturacionToolStripMenuItem.Text = "Facturacion";
            // 
            // detalleFacturaToolStripMenuItem
            // 
            detalleFacturaToolStripMenuItem.Name = "detalleFacturaToolStripMenuItem";
            detalleFacturaToolStripMenuItem.Size = new Size(220, 28);
            detalleFacturaToolStripMenuItem.Text = "Detalle factura";
            // 
            // comprasToolStripMenuItem
            // 
            comprasToolStripMenuItem.Name = "comprasToolStripMenuItem";
            comprasToolStripMenuItem.Size = new Size(220, 28);
            comprasToolStripMenuItem.Text = "Compras";
            // 
            // detalleComprasToolStripMenuItem
            // 
            detalleComprasToolStripMenuItem.Name = "detalleComprasToolStripMenuItem";
            detalleComprasToolStripMenuItem.Size = new Size(220, 28);
            detalleComprasToolStripMenuItem.Text = "Detalle Compras";
            // 
            // OpcionToolStripMenuItem
            // 
            OpcionToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { usuariosToolStripMenuItem, configuracionToolStripMenuItem });
            OpcionToolStripMenuItem.ForeColor = Color.FromArgb(51, 65, 85);
            OpcionToolStripMenuItem.Image = Properties.Resources.configuracion_de_preferencias_de_oficina_configuracion_de_rueda_dentada;
            OpcionToolStripMenuItem.ImageScaling = ToolStripItemImageScaling.None;
            OpcionToolStripMenuItem.Name = "OpcionToolStripMenuItem";
            OpcionToolStripMenuItem.Size = new Size(127, 36);
            OpcionToolStripMenuItem.Text = "Opciones";
            // 
            // usuariosToolStripMenuItem
            // 
            usuariosToolStripMenuItem.Name = "usuariosToolStripMenuItem";
            usuariosToolStripMenuItem.Size = new Size(159, 28);
            usuariosToolStripMenuItem.Text = "Usuarios";
            usuariosToolStripMenuItem.Click += usuariosToolStripMenuItem_Click;
            // 
            // configuracionToolStripMenuItem
            // 
            configuracionToolStripMenuItem.Name = "configuracionToolStripMenuItem";
            configuracionToolStripMenuItem.Size = new Size(159, 28);
            configuracionToolStripMenuItem.Text = "Salir";
            configuracionToolStripMenuItem.Click += configuracionToolStripMenuItem_Click;
            // 
            // sidebarPanel
            // 
            sidebarPanel.BackColor = Color.FromArgb(15, 23, 42);
            sidebarPanel.Controls.Add(flowNavPanel);
            sidebarPanel.Controls.Add(panelLogoSeparator);
            sidebarPanel.Controls.Add(logoPanel);
            sidebarPanel.Dock = DockStyle.Left;
            sidebarPanel.Location = new Point(0, 46);
            sidebarPanel.Margin = new Padding(3, 4, 3, 4);
            sidebarPanel.Name = "sidebarPanel";
            sidebarPanel.Size = new Size(263, 954);
            sidebarPanel.TabIndex = 1;
            // 
            // flowNavPanel
            // 
            flowNavPanel.Controls.Add(dashboardButton);
            flowNavPanel.Controls.Add(mascotasButton);
            flowNavPanel.Controls.Add(citasButton);
            flowNavPanel.Controls.Add(facturacionButton);
            flowNavPanel.Controls.Add(inventarioButton);
            flowNavPanel.Controls.Add(comprasButton);
            flowNavPanel.Controls.Add(reportesButton);
            flowNavPanel.Dock = DockStyle.Fill;
            flowNavPanel.FlowDirection = FlowDirection.TopDown;
            flowNavPanel.Location = new Point(0, 81);
            flowNavPanel.Margin = new Padding(3, 4, 3, 4);
            flowNavPanel.Name = "flowNavPanel";
            flowNavPanel.Padding = new Padding(11, 20, 11, 13);
            flowNavPanel.Size = new Size(263, 873);
            flowNavPanel.TabIndex = 2;
            flowNavPanel.WrapContents = false;
            // 
            // dashboardButton
            // 
            dashboardButton.BackColor = Color.FromArgb(14, 165, 233);
            dashboardButton.FlatAppearance.BorderSize = 0;
            dashboardButton.FlatAppearance.MouseDownBackColor = Color.FromArgb(14, 165, 233);
            dashboardButton.FlatAppearance.MouseOverBackColor = Color.FromArgb(14, 165, 233);
            dashboardButton.FlatStyle = FlatStyle.Flat;
            dashboardButton.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            dashboardButton.ForeColor = Color.White;
            dashboardButton.Location = new Point(14, 24);
            dashboardButton.Margin = new Padding(3, 4, 3, 11);
            dashboardButton.Name = "dashboardButton";
            dashboardButton.Padding = new Padding(11, 0, 0, 0);
            dashboardButton.Size = new Size(233, 53);
            dashboardButton.TabIndex = 0;
            dashboardButton.Text = "📊 Dashboard";
            dashboardButton.TextAlign = ContentAlignment.MiddleLeft;
            dashboardButton.UseVisualStyleBackColor = false;
            // 
            // mascotasButton
            // 
            mascotasButton.BackColor = Color.Transparent;
            mascotasButton.FlatAppearance.BorderSize = 0;
            mascotasButton.FlatAppearance.MouseDownBackColor = Color.FromArgb(51, 65, 85);
            mascotasButton.FlatAppearance.MouseOverBackColor = Color.FromArgb(30, 41, 59);
            mascotasButton.FlatStyle = FlatStyle.Flat;
            mascotasButton.Font = new Font("Segoe UI", 10F);
            mascotasButton.ForeColor = Color.FromArgb(241, 245, 249);
            mascotasButton.Location = new Point(14, 92);
            mascotasButton.Margin = new Padding(3, 4, 3, 11);
            mascotasButton.Name = "mascotasButton";
            mascotasButton.Padding = new Padding(11, 0, 0, 0);
            mascotasButton.Size = new Size(233, 53);
            mascotasButton.TabIndex = 1;
            mascotasButton.Text = "🐾 Mascotas";
            mascotasButton.TextAlign = ContentAlignment.MiddleLeft;
            mascotasButton.UseVisualStyleBackColor = false;
            mascotasButton.Click += mascotasButton_Click;
            // 
            // citasButton
            // 
            citasButton.BackColor = Color.Transparent;
            citasButton.FlatAppearance.BorderSize = 0;
            citasButton.FlatAppearance.MouseDownBackColor = Color.FromArgb(51, 65, 85);
            citasButton.FlatAppearance.MouseOverBackColor = Color.FromArgb(30, 41, 59);
            citasButton.FlatStyle = FlatStyle.Flat;
            citasButton.Font = new Font("Segoe UI", 10F);
            citasButton.ForeColor = Color.FromArgb(241, 245, 249);
            citasButton.Location = new Point(14, 160);
            citasButton.Margin = new Padding(3, 4, 3, 11);
            citasButton.Name = "citasButton";
            citasButton.Padding = new Padding(11, 0, 0, 0);
            citasButton.Size = new Size(233, 53);
            citasButton.TabIndex = 2;
            citasButton.Text = "📅 Citas";
            citasButton.TextAlign = ContentAlignment.MiddleLeft;
            citasButton.UseVisualStyleBackColor = false;
            // 
            // facturacionButton
            // 
            facturacionButton.BackColor = Color.Transparent;
            facturacionButton.FlatAppearance.BorderSize = 0;
            facturacionButton.FlatAppearance.MouseDownBackColor = Color.FromArgb(51, 65, 85);
            facturacionButton.FlatAppearance.MouseOverBackColor = Color.FromArgb(30, 41, 59);
            facturacionButton.FlatStyle = FlatStyle.Flat;
            facturacionButton.Font = new Font("Segoe UI", 10F);
            facturacionButton.ForeColor = Color.FromArgb(241, 245, 249);
            facturacionButton.Location = new Point(14, 228);
            facturacionButton.Margin = new Padding(3, 4, 3, 11);
            facturacionButton.Name = "facturacionButton";
            facturacionButton.Padding = new Padding(11, 0, 0, 0);
            facturacionButton.Size = new Size(233, 53);
            facturacionButton.TabIndex = 3;
            facturacionButton.Text = "\U0001f9fe Facturación";
            facturacionButton.TextAlign = ContentAlignment.MiddleLeft;
            facturacionButton.UseVisualStyleBackColor = false;
            // 
            // inventarioButton
            // 
            inventarioButton.BackColor = Color.Transparent;
            inventarioButton.FlatAppearance.BorderSize = 0;
            inventarioButton.FlatAppearance.MouseDownBackColor = Color.FromArgb(51, 65, 85);
            inventarioButton.FlatAppearance.MouseOverBackColor = Color.FromArgb(30, 41, 59);
            inventarioButton.FlatStyle = FlatStyle.Flat;
            inventarioButton.Font = new Font("Segoe UI", 10F);
            inventarioButton.ForeColor = Color.FromArgb(241, 245, 249);
            inventarioButton.Location = new Point(14, 296);
            inventarioButton.Margin = new Padding(3, 4, 3, 11);
            inventarioButton.Name = "inventarioButton";
            inventarioButton.Padding = new Padding(11, 0, 0, 0);
            inventarioButton.Size = new Size(233, 53);
            inventarioButton.TabIndex = 4;
            inventarioButton.Text = "📦 Inventario";
            inventarioButton.TextAlign = ContentAlignment.MiddleLeft;
            inventarioButton.UseVisualStyleBackColor = false;
            // 
            // comprasButton
            // 
            comprasButton.BackColor = Color.Transparent;
            comprasButton.FlatAppearance.BorderSize = 0;
            comprasButton.FlatAppearance.MouseDownBackColor = Color.FromArgb(51, 65, 85);
            comprasButton.FlatAppearance.MouseOverBackColor = Color.FromArgb(30, 41, 59);
            comprasButton.FlatStyle = FlatStyle.Flat;
            comprasButton.Font = new Font("Segoe UI", 10F);
            comprasButton.ForeColor = Color.FromArgb(241, 245, 249);
            comprasButton.Location = new Point(14, 364);
            comprasButton.Margin = new Padding(3, 4, 3, 11);
            comprasButton.Name = "comprasButton";
            comprasButton.Padding = new Padding(11, 0, 0, 0);
            comprasButton.Size = new Size(233, 53);
            comprasButton.TabIndex = 5;
            comprasButton.Text = "\U0001f6d2 Compras";
            comprasButton.TextAlign = ContentAlignment.MiddleLeft;
            comprasButton.UseVisualStyleBackColor = false;
            // 
            // reportesButton
            // 
            reportesButton.BackColor = Color.Transparent;
            reportesButton.FlatAppearance.BorderSize = 0;
            reportesButton.FlatAppearance.MouseDownBackColor = Color.FromArgb(51, 65, 85);
            reportesButton.FlatAppearance.MouseOverBackColor = Color.FromArgb(30, 41, 59);
            reportesButton.FlatStyle = FlatStyle.Flat;
            reportesButton.Font = new Font("Segoe UI", 10F);
            reportesButton.ForeColor = Color.FromArgb(241, 245, 249);
            reportesButton.Location = new Point(14, 432);
            reportesButton.Margin = new Padding(3, 4, 3, 11);
            reportesButton.Name = "reportesButton";
            reportesButton.Padding = new Padding(11, 0, 0, 0);
            reportesButton.Size = new Size(233, 53);
            reportesButton.TabIndex = 6;
            reportesButton.Text = "📊 Reportes";
            reportesButton.TextAlign = ContentAlignment.MiddleLeft;
            reportesButton.UseVisualStyleBackColor = false;
            // 
            // panelLogoSeparator
            // 
            panelLogoSeparator.BackColor = Color.FromArgb(51, 65, 85);
            panelLogoSeparator.Dock = DockStyle.Top;
            panelLogoSeparator.Location = new Point(0, 80);
            panelLogoSeparator.Margin = new Padding(3, 4, 3, 4);
            panelLogoSeparator.Name = "panelLogoSeparator";
            panelLogoSeparator.Size = new Size(263, 1);
            panelLogoSeparator.TabIndex = 1;
            // 
            // logoPanel
            // 
            logoPanel.Controls.Add(lblLogoSubtitle);
            logoPanel.Controls.Add(lblLogo);
            logoPanel.Dock = DockStyle.Top;
            logoPanel.Location = new Point(0, 0);
            logoPanel.Margin = new Padding(3, 4, 3, 4);
            logoPanel.Name = "logoPanel";
            logoPanel.Size = new Size(263, 80);
            logoPanel.TabIndex = 0;
            // 
            // lblLogoSubtitle
            // 
            lblLogoSubtitle.AutoSize = true;
            lblLogoSubtitle.Font = new Font("Segoe UI", 8.25F, FontStyle.Bold);
            lblLogoSubtitle.ForeColor = Color.FromArgb(148, 163, 184);
            lblLogoSubtitle.Location = new Point(17, 52);
            lblLogoSubtitle.Name = "lblLogoSubtitle";
            lblLogoSubtitle.Size = new Size(187, 19);
            lblLogoSubtitle.TabIndex = 1;
            lblLogoSubtitle.Text = "Gestión Clínica Profesional";
            // 
            // lblLogo
            // 
            lblLogo.AutoSize = true;
            lblLogo.Font = new Font("Segoe UI", 15.75F, FontStyle.Bold);
            lblLogo.ForeColor = Color.White;
            lblLogo.Location = new Point(14, 12);
            lblLogo.Name = "lblLogo";
            lblLogo.Size = new Size(177, 37);
            lblLogo.TabIndex = 0;
            lblLogo.Text = "🐾 VETCARE";
            // 
            // mainPanel
            // 
            mainPanel.BackColor = Color.FromArgb(248, 250, 252);
            mainPanel.Controls.Add(tableLayoutPanelContent);
            mainPanel.Controls.Add(tableLayoutPanelKPIs);
            mainPanel.Controls.Add(panelHeader);
            mainPanel.Dock = DockStyle.Fill;
            mainPanel.Location = new Point(263, 46);
            mainPanel.Margin = new Padding(3, 4, 3, 4);
            mainPanel.Name = "mainPanel";
            mainPanel.Size = new Size(1108, 954);
            mainPanel.TabIndex = 2;
            // 
            // tableLayoutPanelContent
            // 
            tableLayoutPanelContent.ColumnCount = 2;
            tableLayoutPanelContent.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 62F));
            tableLayoutPanelContent.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 38F));
            tableLayoutPanelContent.Controls.Add(panelCitasDetail, 0, 0);
            tableLayoutPanelContent.Controls.Add(panelRightLayout, 1, 0);
            tableLayoutPanelContent.Dock = DockStyle.Fill;
            tableLayoutPanelContent.Location = new Point(0, 273);
            tableLayoutPanelContent.Margin = new Padding(3, 4, 3, 4);
            tableLayoutPanelContent.Name = "tableLayoutPanelContent";
            tableLayoutPanelContent.Padding = new Padding(11, 0, 11, 13);
            tableLayoutPanelContent.RowCount = 1;
            tableLayoutPanelContent.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanelContent.Size = new Size(1108, 681);
            tableLayoutPanelContent.TabIndex = 2;
            // 
            // panelCitasDetail
            // 
            panelCitasDetail.BackColor = Color.White;
            panelCitasDetail.BorderStyle = BorderStyle.FixedSingle;
            panelCitasDetail.Controls.Add(dgvCitas);
            panelCitasDetail.Controls.Add(lblCitasSubtitle);
            panelCitasDetail.Controls.Add(lblCitasTitle);
            panelCitasDetail.Dock = DockStyle.Fill;
            panelCitasDetail.Location = new Point(18, 8);
            panelCitasDetail.Margin = new Padding(7, 8, 7, 8);
            panelCitasDetail.Name = "panelCitasDetail";
            panelCitasDetail.Padding = new Padding(17, 20, 17, 20);
            panelCitasDetail.Size = new Size(659, 652);
            panelCitasDetail.TabIndex = 0;
            // 
            // dgvCitas
            // 
            dgvCitas.AllowUserToAddRows = false;
            dgvCitas.AllowUserToDeleteRows = false;
            dgvCitas.AllowUserToResizeRows = false;
            dgvCitas.BackgroundColor = Color.White;
            dgvCitas.BorderStyle = BorderStyle.None;
            dgvCitas.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dgvCitas.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dgvCitas.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvCitas.Dock = DockStyle.Fill;
            dgvCitas.EnableHeadersVisualStyles = false;
            dgvCitas.GridColor = Color.FromArgb(241, 245, 249);
            dgvCitas.Location = new Point(17, 87);
            dgvCitas.Margin = new Padding(3, 4, 3, 4);
            dgvCitas.MultiSelect = false;
            dgvCitas.Name = "dgvCitas";
            dgvCitas.ReadOnly = true;
            dgvCitas.RowHeadersVisible = false;
            dgvCitas.RowHeadersWidth = 51;
            dgvCitas.RowTemplate.Height = 32;
            dgvCitas.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvCitas.Size = new Size(623, 543);
            dgvCitas.TabIndex = 2;
            dgvCitas.CellContentClick += dgvCitas_CellContentClick;
            // 
            // lblCitasSubtitle
            // 
            lblCitasSubtitle.AutoSize = true;
            lblCitasSubtitle.Dock = DockStyle.Top;
            lblCitasSubtitle.Font = new Font("Segoe UI", 8.25F);
            lblCitasSubtitle.ForeColor = Color.FromArgb(100, 116, 139);
            lblCitasSubtitle.Location = new Point(17, 48);
            lblCitasSubtitle.Name = "lblCitasSubtitle";
            lblCitasSubtitle.Padding = new Padding(0, 0, 0, 20);
            lblCitasSubtitle.Size = new Size(332, 39);
            lblCitasSubtitle.TabIndex = 1;
            lblCitasSubtitle.Text = "Listado de pacientes programados para consulta hoy";
            // 
            // lblCitasTitle
            // 
            lblCitasTitle.AutoSize = true;
            lblCitasTitle.Dock = DockStyle.Top;
            lblCitasTitle.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            lblCitasTitle.ForeColor = Color.FromArgb(15, 23, 42);
            lblCitasTitle.Location = new Point(17, 20);
            lblCitasTitle.Name = "lblCitasTitle";
            lblCitasTitle.Size = new Size(269, 28);
            lblCitasTitle.TabIndex = 0;
            lblCitasTitle.Text = "📅 Próximas Citas Médicas";
            // 
            // panelRightLayout
            // 
            panelRightLayout.Controls.Add(panelSystemSummary);
            panelRightLayout.Controls.Add(panelSpacer);
            panelRightLayout.Controls.Add(panelActions);
            panelRightLayout.Dock = DockStyle.Fill;
            panelRightLayout.Location = new Point(687, 8);
            panelRightLayout.Margin = new Padding(3, 8, 7, 8);
            panelRightLayout.Name = "panelRightLayout";
            panelRightLayout.Size = new Size(403, 652);
            panelRightLayout.TabIndex = 1;
            // 
            // panelSystemSummary
            // 
            panelSystemSummary.BackColor = Color.White;
            panelSystemSummary.BorderStyle = BorderStyle.FixedSingle;
            panelSystemSummary.Controls.Add(tableLayoutPanelMetrics);
            panelSystemSummary.Controls.Add(lblSummarySubtitle);
            panelSystemSummary.Controls.Add(lblSummaryTitle);
            panelSystemSummary.Dock = DockStyle.Fill;
            panelSystemSummary.Location = new Point(0, 306);
            panelSystemSummary.Margin = new Padding(3, 4, 3, 4);
            panelSystemSummary.Name = "panelSystemSummary";
            panelSystemSummary.Padding = new Padding(17, 20, 17, 20);
            panelSystemSummary.Size = new Size(403, 346);
            panelSystemSummary.TabIndex = 2;
            // 
            // tableLayoutPanelMetrics
            // 
            tableLayoutPanelMetrics.ColumnCount = 2;
            tableLayoutPanelMetrics.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 65F));
            tableLayoutPanelMetrics.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 35F));
            tableLayoutPanelMetrics.Controls.Add(lblVetTitle, 0, 0);
            tableLayoutPanelMetrics.Controls.Add(lblVetValue, 1, 0);
            tableLayoutPanelMetrics.Controls.Add(lblHospTitle, 0, 1);
            tableLayoutPanelMetrics.Controls.Add(lblHospValue, 1, 1);
            tableLayoutPanelMetrics.Controls.Add(lblStockTitle, 0, 2);
            tableLayoutPanelMetrics.Dock = DockStyle.Fill;
            tableLayoutPanelMetrics.Location = new Point(17, 87);
            tableLayoutPanelMetrics.Margin = new Padding(3, 4, 3, 4);
            tableLayoutPanelMetrics.Name = "tableLayoutPanelMetrics";
            tableLayoutPanelMetrics.RowCount = 4;
            tableLayoutPanelMetrics.RowStyles.Add(new RowStyle(SizeType.Percent, 25F));
            tableLayoutPanelMetrics.RowStyles.Add(new RowStyle(SizeType.Percent, 26.58228F));
            tableLayoutPanelMetrics.RowStyles.Add(new RowStyle(SizeType.Percent, 5.485232F));
            tableLayoutPanelMetrics.RowStyles.Add(new RowStyle(SizeType.Percent, 43.881855F));
            tableLayoutPanelMetrics.Size = new Size(367, 237);
            tableLayoutPanelMetrics.TabIndex = 2;
            // 
            // lblVetTitle
            // 
            lblVetTitle.AutoSize = true;
            lblVetTitle.Dock = DockStyle.Fill;
            lblVetTitle.Font = new Font("Segoe UI", 9F);
            lblVetTitle.ForeColor = Color.FromArgb(71, 85, 105);
            lblVetTitle.Location = new Point(3, 0);
            lblVetTitle.Name = "lblVetTitle";
            lblVetTitle.Size = new Size(232, 58);
            lblVetTitle.TabIndex = 0;
            lblVetTitle.Text = "Veterinarios totales";
            lblVetTitle.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lblVetValue
            // 
            lblVetValue.AutoSize = true;
            lblVetValue.Dock = DockStyle.Fill;
            lblVetValue.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblVetValue.ForeColor = Color.FromArgb(15, 23, 42);
            lblVetValue.Location = new Point(241, 0);
            lblVetValue.Name = "lblVetValue";
            lblVetValue.Size = new Size(123, 58);
            lblVetValue.TabIndex = 1;
            lblVetValue.Text = "4 ";
            lblVetValue.TextAlign = ContentAlignment.MiddleRight;
            // 
            // lblHospTitle
            // 
            lblHospTitle.AutoSize = true;
            lblHospTitle.Dock = DockStyle.Fill;
            lblHospTitle.Font = new Font("Segoe UI", 9F);
            lblHospTitle.ForeColor = Color.FromArgb(71, 85, 105);
            lblHospTitle.Location = new Point(3, 58);
            lblHospTitle.Name = "lblHospTitle";
            lblHospTitle.Size = new Size(232, 62);
            lblHospTitle.TabIndex = 2;
            lblHospTitle.Text = "Mascotas totales";
            lblHospTitle.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lblHospValue
            // 
            lblHospValue.AutoSize = true;
            lblHospValue.Dock = DockStyle.Fill;
            lblHospValue.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblHospValue.ForeColor = Color.FromArgb(124, 58, 237);
            lblHospValue.Location = new Point(241, 58);
            lblHospValue.Name = "lblHospValue";
            lblHospValue.Size = new Size(123, 62);
            lblHospValue.TabIndex = 3;
            lblHospValue.Text = "23";
            lblHospValue.TextAlign = ContentAlignment.MiddleRight;
            // 
            // lblStockTitle
            // 
            lblStockTitle.AutoSize = true;
            lblStockTitle.Dock = DockStyle.Fill;
            lblStockTitle.Font = new Font("Segoe UI", 9F);
            lblStockTitle.ForeColor = Color.FromArgb(71, 85, 105);
            lblStockTitle.Location = new Point(3, 120);
            lblStockTitle.Name = "lblStockTitle";
            lblStockTitle.Size = new Size(232, 12);
            lblStockTitle.TabIndex = 4;
            lblStockTitle.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lblSummarySubtitle
            // 
            lblSummarySubtitle.AutoSize = true;
            lblSummarySubtitle.Dock = DockStyle.Top;
            lblSummarySubtitle.Font = new Font("Segoe UI", 8.25F);
            lblSummarySubtitle.ForeColor = Color.FromArgb(100, 116, 139);
            lblSummarySubtitle.Location = new Point(17, 48);
            lblSummarySubtitle.Name = "lblSummarySubtitle";
            lblSummarySubtitle.Padding = new Padding(0, 0, 0, 20);
            lblSummarySubtitle.Size = new Size(88, 39);
            lblSummarySubtitle.TabIndex = 1;
            lblSummarySubtitle.Text = "Einformacion";
            // 
            // lblSummaryTitle
            // 
            lblSummaryTitle.AutoSize = true;
            lblSummaryTitle.Dock = DockStyle.Top;
            lblSummaryTitle.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            lblSummaryTitle.ForeColor = Color.FromArgb(15, 23, 42);
            lblSummaryTitle.Location = new Point(17, 20);
            lblSummaryTitle.Name = "lblSummaryTitle";
            lblSummaryTitle.Size = new Size(132, 28);
            lblSummaryTitle.TabIndex = 0;
            lblSummaryTitle.Text = "⚙️ Resumen";
            lblSummaryTitle.Click += lblSummaryTitle_Click;
            // 
            // panelSpacer
            // 
            panelSpacer.Dock = DockStyle.Top;
            panelSpacer.Location = new Point(0, 286);
            panelSpacer.Margin = new Padding(3, 4, 3, 4);
            panelSpacer.Name = "panelSpacer";
            panelSpacer.Size = new Size(403, 20);
            panelSpacer.TabIndex = 1;
            // 
            // panelActions
            // 
            panelActions.BackColor = Color.White;
            panelActions.BorderStyle = BorderStyle.FixedSingle;
            panelActions.Controls.Add(tableLayoutPanelButtons);
            panelActions.Controls.Add(lblActionsSubtitle);
            panelActions.Controls.Add(lblActionsTitle);
            panelActions.Dock = DockStyle.Top;
            panelActions.Location = new Point(0, 0);
            panelActions.Margin = new Padding(3, 4, 3, 4);
            panelActions.Name = "panelActions";
            panelActions.Padding = new Padding(17, 20, 17, 20);
            panelActions.Size = new Size(403, 286);
            panelActions.TabIndex = 0;
            // 
            // tableLayoutPanelButtons
            // 
            tableLayoutPanelButtons.ColumnCount = 2;
            tableLayoutPanelButtons.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanelButtons.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanelButtons.Controls.Add(btnNewCita, 0, 0);
            tableLayoutPanelButtons.Controls.Add(btnNewPet, 1, 0);
            tableLayoutPanelButtons.Controls.Add(btnFacturar, 0, 1);
            tableLayoutPanelButtons.Controls.Add(btnCompra, 1, 1);
            tableLayoutPanelButtons.Dock = DockStyle.Fill;
            tableLayoutPanelButtons.Location = new Point(17, 87);
            tableLayoutPanelButtons.Margin = new Padding(3, 4, 3, 4);
            tableLayoutPanelButtons.Name = "tableLayoutPanelButtons";
            tableLayoutPanelButtons.RowCount = 2;
            tableLayoutPanelButtons.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanelButtons.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanelButtons.Size = new Size(367, 177);
            tableLayoutPanelButtons.TabIndex = 2;
            // 
            // btnNewCita
            // 
            btnNewCita.BackColor = Color.White;
            btnNewCita.Dock = DockStyle.Fill;
            btnNewCita.FlatAppearance.BorderColor = Color.FromArgb(226, 232, 240);
            btnNewCita.FlatAppearance.MouseDownBackColor = Color.FromArgb(241, 245, 249);
            btnNewCita.FlatAppearance.MouseOverBackColor = Color.FromArgb(248, 250, 252);
            btnNewCita.FlatStyle = FlatStyle.Flat;
            btnNewCita.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnNewCita.ForeColor = Color.FromArgb(51, 65, 85);
            btnNewCita.Location = new Point(5, 5);
            btnNewCita.Margin = new Padding(5);
            btnNewCita.Name = "btnNewCita";
            btnNewCita.Size = new Size(173, 78);
            btnNewCita.TabIndex = 0;
            btnNewCita.Text = "📅 Nueva Cita";
            btnNewCita.UseVisualStyleBackColor = false;
            // 
            // btnNewPet
            // 
            btnNewPet.BackColor = Color.White;
            btnNewPet.Dock = DockStyle.Fill;
            btnNewPet.FlatAppearance.BorderColor = Color.FromArgb(226, 232, 240);
            btnNewPet.FlatAppearance.MouseDownBackColor = Color.FromArgb(241, 245, 249);
            btnNewPet.FlatAppearance.MouseOverBackColor = Color.FromArgb(248, 250, 252);
            btnNewPet.FlatStyle = FlatStyle.Flat;
            btnNewPet.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnNewPet.ForeColor = Color.FromArgb(51, 65, 85);
            btnNewPet.Location = new Point(188, 5);
            btnNewPet.Margin = new Padding(5);
            btnNewPet.Name = "btnNewPet";
            btnNewPet.Size = new Size(174, 78);
            btnNewPet.TabIndex = 1;
            btnNewPet.Text = "🐾 Reg. Mascota";
            btnNewPet.UseVisualStyleBackColor = false;
            // 
            // btnFacturar
            // 
            btnFacturar.BackColor = Color.White;
            btnFacturar.Dock = DockStyle.Fill;
            btnFacturar.FlatAppearance.BorderColor = Color.FromArgb(226, 232, 240);
            btnFacturar.FlatAppearance.MouseDownBackColor = Color.FromArgb(241, 245, 249);
            btnFacturar.FlatAppearance.MouseOverBackColor = Color.FromArgb(248, 250, 252);
            btnFacturar.FlatStyle = FlatStyle.Flat;
            btnFacturar.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnFacturar.ForeColor = Color.FromArgb(51, 65, 85);
            btnFacturar.Location = new Point(5, 93);
            btnFacturar.Margin = new Padding(5);
            btnFacturar.Name = "btnFacturar";
            btnFacturar.Size = new Size(173, 79);
            btnFacturar.TabIndex = 2;
            btnFacturar.Text = "\U0001f9fe Facturar";
            btnFacturar.UseVisualStyleBackColor = false;
            btnFacturar.Click += btnNewInvoice_Click;
            // 
            // btnCompra
            // 
            btnCompra.BackColor = Color.White;
            btnCompra.Dock = DockStyle.Fill;
            btnCompra.FlatAppearance.BorderColor = Color.FromArgb(226, 232, 240);
            btnCompra.FlatAppearance.MouseDownBackColor = Color.FromArgb(241, 245, 249);
            btnCompra.FlatAppearance.MouseOverBackColor = Color.FromArgb(248, 250, 252);
            btnCompra.FlatStyle = FlatStyle.Flat;
            btnCompra.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnCompra.ForeColor = Color.FromArgb(51, 65, 85);
            btnCompra.Location = new Point(188, 93);
            btnCompra.Margin = new Padding(5);
            btnCompra.Name = "btnCompra";
            btnCompra.Size = new Size(174, 79);
            btnCompra.TabIndex = 3;
            btnCompra.Text = "\U0001f6d2 Reg. Compra";
            btnCompra.UseVisualStyleBackColor = false;
            // 
            // lblActionsSubtitle
            // 
            lblActionsSubtitle.AutoSize = true;
            lblActionsSubtitle.Dock = DockStyle.Top;
            lblActionsSubtitle.Font = new Font("Segoe UI", 8.25F);
            lblActionsSubtitle.ForeColor = Color.FromArgb(100, 116, 139);
            lblActionsSubtitle.Location = new Point(17, 48);
            lblActionsSubtitle.Name = "lblActionsSubtitle";
            lblActionsSubtitle.Padding = new Padding(0, 0, 0, 20);
            lblActionsSubtitle.Size = new Size(244, 39);
            lblActionsSubtitle.TabIndex = 1;
            lblActionsSubtitle.Text = "Acceso directo a operaciones comunes";
            // 
            // lblActionsTitle
            // 
            lblActionsTitle.AutoSize = true;
            lblActionsTitle.Dock = DockStyle.Top;
            lblActionsTitle.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            lblActionsTitle.ForeColor = Color.FromArgb(15, 23, 42);
            lblActionsTitle.Location = new Point(17, 20);
            lblActionsTitle.Name = "lblActionsTitle";
            lblActionsTitle.Size = new Size(202, 28);
            lblActionsTitle.TabIndex = 0;
            lblActionsTitle.Text = "⚡ Accesos Rápidos";
            // 
            // tableLayoutPanelKPIs
            // 
            tableLayoutPanelKPIs.ColumnCount = 4;
            tableLayoutPanelKPIs.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            tableLayoutPanelKPIs.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            tableLayoutPanelKPIs.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            tableLayoutPanelKPIs.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            tableLayoutPanelKPIs.Controls.Add(panelCardIngresos, 3, 0);
            tableLayoutPanelKPIs.Controls.Add(panelCardCitas, 2, 0);
            tableLayoutPanelKPIs.Controls.Add(panelCardCompras, 1, 0);
            tableLayoutPanelKPIs.Controls.Add(panelCardVentas, 0, 0);
            tableLayoutPanelKPIs.Dock = DockStyle.Top;
            tableLayoutPanelKPIs.Location = new Point(0, 93);
            tableLayoutPanelKPIs.Margin = new Padding(3, 4, 3, 4);
            tableLayoutPanelKPIs.Name = "tableLayoutPanelKPIs";
            tableLayoutPanelKPIs.Padding = new Padding(11, 7, 11, 20);
            tableLayoutPanelKPIs.RowCount = 1;
            tableLayoutPanelKPIs.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanelKPIs.Size = new Size(1108, 180);
            tableLayoutPanelKPIs.TabIndex = 1;
            tableLayoutPanelKPIs.Paint += tableLayoutPanelKPIs_Paint;
            // 
            // panelCardIngresos
            // 
            panelCardIngresos.BackColor = Color.White;
            panelCardIngresos.BorderStyle = BorderStyle.FixedSingle;
            panelCardIngresos.Controls.Add(lblIngresosIcon);
            panelCardIngresos.Controls.Add(lblIngresosValue);
            panelCardIngresos.Controls.Add(lblIngresosTitle);
            panelCardIngresos.Controls.Add(panelCardIngresosAccent);
            panelCardIngresos.Dock = DockStyle.Fill;
            panelCardIngresos.Location = new Point(831, 15);
            panelCardIngresos.Margin = new Padding(7, 8, 7, 8);
            panelCardIngresos.Name = "panelCardIngresos";
            panelCardIngresos.Size = new Size(259, 137);
            panelCardIngresos.TabIndex = 3;
            // 
            // lblIngresosIcon
            // 
            lblIngresosIcon.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            lblIngresosIcon.AutoSize = true;
            lblIngresosIcon.Font = new Font("Segoe UI", 26F);
            lblIngresosIcon.ForeColor = Color.FromArgb(203, 213, 225);
            lblIngresosIcon.Location = new Point(105, 16);
            lblIngresosIcon.Name = "lblIngresosIcon";
            lblIngresosIcon.Size = new Size(85, 60);
            lblIngresosIcon.TabIndex = 4;
            lblIngresosIcon.Text = "📈";
            // 
            // lblIngresosValue
            // 
            lblIngresosValue.AutoSize = true;
            lblIngresosValue.Font = new Font("Segoe UI", 18F, FontStyle.Bold);
            lblIngresosValue.ForeColor = Color.FromArgb(15, 23, 42);
            lblIngresosValue.Location = new Point(11, 43);
            lblIngresosValue.Name = "lblIngresosValue";
            lblIngresosValue.Size = new Size(186, 41);
            lblIngresosValue.TabIndex = 2;
            lblIngresosValue.Text = "3 productos";
            // 
            // lblIngresosTitle
            // 
            lblIngresosTitle.AutoSize = true;
            lblIngresosTitle.Font = new Font("Segoe UI", 8.25F, FontStyle.Bold);
            lblIngresosTitle.ForeColor = Color.FromArgb(100, 116, 139);
            lblIngresosTitle.Location = new Point(11, 16);
            lblIngresosTitle.Name = "lblIngresosTitle";
            lblIngresosTitle.Size = new Size(80, 19);
            lblIngresosTitle.TabIndex = 1;
            lblIngresosTitle.Text = "Stock bajo";
            lblIngresosTitle.Click += lblIngresosTitle_Click;
            // 
            // panelCardIngresosAccent
            // 
            panelCardIngresosAccent.BackColor = Color.FromArgb(225, 29, 72);
            panelCardIngresosAccent.Dock = DockStyle.Left;
            panelCardIngresosAccent.Location = new Point(0, 0);
            panelCardIngresosAccent.Margin = new Padding(3, 4, 3, 4);
            panelCardIngresosAccent.Name = "panelCardIngresosAccent";
            panelCardIngresosAccent.Size = new Size(5, 135);
            panelCardIngresosAccent.TabIndex = 0;
            // 
            // panelCardCitas
            // 
            panelCardCitas.BackColor = Color.White;
            panelCardCitas.BorderStyle = BorderStyle.FixedSingle;
            panelCardCitas.Controls.Add(lblCitasIcon);
            panelCardCitas.Controls.Add(lblCitasValue);
            panelCardCitas.Controls.Add(lblCitasCardTitle);
            panelCardCitas.Controls.Add(panelCardCitasAccent);
            panelCardCitas.Dock = DockStyle.Fill;
            panelCardCitas.Location = new Point(560, 15);
            panelCardCitas.Margin = new Padding(7, 8, 7, 8);
            panelCardCitas.Name = "panelCardCitas";
            panelCardCitas.Size = new Size(257, 137);
            panelCardCitas.TabIndex = 2;
            // 
            // lblCitasIcon
            // 
            lblCitasIcon.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            lblCitasIcon.AutoSize = true;
            lblCitasIcon.Font = new Font("Segoe UI", 26F);
            lblCitasIcon.ForeColor = Color.FromArgb(203, 213, 225);
            lblCitasIcon.Location = new Point(187, 16);
            lblCitasIcon.Name = "lblCitasIcon";
            lblCitasIcon.Size = new Size(85, 60);
            lblCitasIcon.TabIndex = 4;
            lblCitasIcon.Text = "📅";
            // 
            // lblCitasValue
            // 
            lblCitasValue.AutoSize = true;
            lblCitasValue.Font = new Font("Segoe UI", 18F, FontStyle.Bold);
            lblCitasValue.ForeColor = Color.FromArgb(15, 23, 42);
            lblCitasValue.Location = new Point(11, 43);
            lblCitasValue.Name = "lblCitasValue";
            lblCitasValue.Size = new Size(129, 41);
            lblCitasValue.TabIndex = 2;
            lblCitasValue.Text = "18 Citas";
            // 
            // lblCitasCardTitle
            // 
            lblCitasCardTitle.AutoSize = true;
            lblCitasCardTitle.Font = new Font("Segoe UI", 8.25F, FontStyle.Bold);
            lblCitasCardTitle.ForeColor = Color.FromArgb(100, 116, 139);
            lblCitasCardTitle.Location = new Point(11, 16);
            lblCitasCardTitle.Name = "lblCitasCardTitle";
            lblCitasCardTitle.Size = new Size(103, 19);
            lblCitasCardTitle.TabIndex = 1;
            lblCitasCardTitle.Text = "CITAS DE HOY";
            // 
            // panelCardCitasAccent
            // 
            panelCardCitasAccent.BackColor = Color.FromArgb(37, 99, 235);
            panelCardCitasAccent.Dock = DockStyle.Left;
            panelCardCitasAccent.Location = new Point(0, 0);
            panelCardCitasAccent.Margin = new Padding(3, 4, 3, 4);
            panelCardCitasAccent.Name = "panelCardCitasAccent";
            panelCardCitasAccent.Size = new Size(5, 135);
            panelCardCitasAccent.TabIndex = 0;
            // 
            // panelCardCompras
            // 
            panelCardCompras.BackColor = Color.White;
            panelCardCompras.BorderStyle = BorderStyle.FixedSingle;
            panelCardCompras.Controls.Add(lblComprasIcon);
            panelCardCompras.Controls.Add(lblComprasValue);
            panelCardCompras.Controls.Add(lblComprasTitle);
            panelCardCompras.Controls.Add(panelCardComprasAccent);
            panelCardCompras.Dock = DockStyle.Fill;
            panelCardCompras.Location = new Point(289, 15);
            panelCardCompras.Margin = new Padding(7, 8, 7, 8);
            panelCardCompras.Name = "panelCardCompras";
            panelCardCompras.Size = new Size(257, 137);
            panelCardCompras.TabIndex = 1;
            // 
            // lblComprasIcon
            // 
            lblComprasIcon.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            lblComprasIcon.AutoSize = true;
            lblComprasIcon.Font = new Font("Segoe UI", 26F);
            lblComprasIcon.ForeColor = Color.FromArgb(203, 213, 225);
            lblComprasIcon.Location = new Point(187, 16);
            lblComprasIcon.Name = "lblComprasIcon";
            lblComprasIcon.Size = new Size(85, 60);
            lblComprasIcon.TabIndex = 4;
            lblComprasIcon.Text = "\U0001f6d2";
            // 
            // lblComprasValue
            // 
            lblComprasValue.AutoSize = true;
            lblComprasValue.Font = new Font("Segoe UI", 18F, FontStyle.Bold);
            lblComprasValue.ForeColor = Color.FromArgb(15, 23, 42);
            lblComprasValue.Location = new Point(11, 43);
            lblComprasValue.Name = "lblComprasValue";
            lblComprasValue.Size = new Size(34, 41);
            lblComprasValue.TabIndex = 2;
            lblComprasValue.Text = "₡";
            lblComprasValue.Click += lblComprasValue_Click;
            // 
            // lblComprasTitle
            // 
            lblComprasTitle.AutoSize = true;
            lblComprasTitle.Font = new Font("Segoe UI", 8.25F, FontStyle.Bold);
            lblComprasTitle.ForeColor = Color.FromArgb(100, 116, 139);
            lblComprasTitle.Location = new Point(11, 16);
            lblComprasTitle.Name = "lblComprasTitle";
            lblComprasTitle.Size = new Size(134, 19);
            lblComprasTitle.TabIndex = 1;
            lblComprasTitle.Text = "COMPRAS DEL DÍA";
            // 
            // panelCardComprasAccent
            // 
            panelCardComprasAccent.BackColor = Color.FromArgb(124, 58, 237);
            panelCardComprasAccent.Dock = DockStyle.Left;
            panelCardComprasAccent.Location = new Point(0, 0);
            panelCardComprasAccent.Margin = new Padding(3, 4, 3, 4);
            panelCardComprasAccent.Name = "panelCardComprasAccent";
            panelCardComprasAccent.Size = new Size(5, 135);
            panelCardComprasAccent.TabIndex = 0;
            // 
            // panelCardVentas
            // 
            panelCardVentas.BackColor = Color.White;
            panelCardVentas.BorderStyle = BorderStyle.FixedSingle;
            panelCardVentas.Controls.Add(lblVentasIcon);
            panelCardVentas.Controls.Add(lblVentasValue);
            panelCardVentas.Controls.Add(lblVentasTitle);
            panelCardVentas.Controls.Add(panelCardVentasAccent);
            panelCardVentas.Dock = DockStyle.Fill;
            panelCardVentas.Location = new Point(18, 15);
            panelCardVentas.Margin = new Padding(7, 8, 7, 8);
            panelCardVentas.Name = "panelCardVentas";
            panelCardVentas.Size = new Size(257, 137);
            panelCardVentas.TabIndex = 0;
            panelCardVentas.Paint += panelCardVentas_Paint;
            // 
            // lblVentasIcon
            // 
            lblVentasIcon.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            lblVentasIcon.AutoSize = true;
            lblVentasIcon.Font = new Font("Segoe UI", 26F);
            lblVentasIcon.ForeColor = Color.FromArgb(203, 213, 225);
            lblVentasIcon.Location = new Point(187, 16);
            lblVentasIcon.Name = "lblVentasIcon";
            lblVentasIcon.Size = new Size(85, 60);
            lblVentasIcon.TabIndex = 4;
            lblVentasIcon.Text = "💰";
            // 
            // lblVentasValue
            // 
            lblVentasValue.AutoSize = true;
            lblVentasValue.Font = new Font("Segoe UI", 18F, FontStyle.Bold);
            lblVentasValue.ForeColor = Color.FromArgb(15, 23, 42);
            lblVentasValue.Location = new Point(11, 43);
            lblVentasValue.Name = "lblVentasValue";
            lblVentasValue.Size = new Size(34, 41);
            lblVentasValue.TabIndex = 2;
            lblVentasValue.Text = "₡";
            // 
            // lblVentasTitle
            // 
            lblVentasTitle.AutoSize = true;
            lblVentasTitle.Font = new Font("Segoe UI", 8.25F, FontStyle.Bold);
            lblVentasTitle.ForeColor = Color.FromArgb(100, 116, 139);
            lblVentasTitle.Location = new Point(11, 16);
            lblVentasTitle.Name = "lblVentasTitle";
            lblVentasTitle.Size = new Size(117, 19);
            lblVentasTitle.TabIndex = 1;
            lblVentasTitle.Text = "VENTAS DEL DÍA";
            // 
            // panelCardVentasAccent
            // 
            panelCardVentasAccent.BackColor = Color.FromArgb(13, 148, 136);
            panelCardVentasAccent.Dock = DockStyle.Left;
            panelCardVentasAccent.Location = new Point(0, 0);
            panelCardVentasAccent.Margin = new Padding(3, 4, 3, 4);
            panelCardVentasAccent.Name = "panelCardVentasAccent";
            panelCardVentasAccent.Size = new Size(5, 135);
            panelCardVentasAccent.TabIndex = 0;
            panelCardVentasAccent.Paint += panelCardVentasAccent_Paint;
            // 
            // panelHeader
            // 
            panelHeader.Controls.Add(lblDate);
            panelHeader.Controls.Add(lblSubtitle);
            panelHeader.Controls.Add(lblTitle);
            panelHeader.Dock = DockStyle.Top;
            panelHeader.Location = new Point(0, 0);
            panelHeader.Margin = new Padding(3, 4, 3, 4);
            panelHeader.Name = "panelHeader";
            panelHeader.Size = new Size(1108, 93);
            panelHeader.TabIndex = 0;
            panelHeader.Paint += panelHeader_Paint;
            // 
            // lblDate
            // 
            lblDate.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            lblDate.AutoSize = true;
            lblDate.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold);
            lblDate.ForeColor = Color.FromArgb(71, 85, 105);
            lblDate.Location = new Point(831, 29);
            lblDate.Name = "lblDate";
            lblDate.Size = new Size(235, 23);
            lblDate.TabIndex = 2;
            lblDate.Text = "Jueves, 11 de Junio de 2026";
            lblDate.TextAlign = ContentAlignment.TopRight;
            // 
            // lblSubtitle
            // 
            lblSubtitle.AutoSize = true;
            lblSubtitle.Font = new Font("Segoe UI", 9.5F);
            lblSubtitle.ForeColor = Color.FromArgb(100, 116, 139);
            lblSubtitle.Location = new Point(19, 56);
            lblSubtitle.Name = "lblSubtitle";
            lblSubtitle.Size = new Size(288, 21);
            lblSubtitle.TabIndex = 1;
            lblSubtitle.Text = "Resumen operativo del día - Veterinaria ";
            lblSubtitle.Click += lblSubtitle_Click;
            // 
            // lblTitle
            // 
            lblTitle.AutoSize = true;
            lblTitle.Font = new Font("Segoe UI", 18F, FontStyle.Bold);
            lblTitle.ForeColor = Color.FromArgb(15, 23, 42);
            lblTitle.Location = new Point(14, 16);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(337, 41);
            lblTitle.TabIndex = 0;
            lblTitle.Text = "📊 Dashboard General";
            lblTitle.Click += lblTitle_Click;
            // 
            // sucursalesToolStripMenuItem1
            // 
            sucursalesToolStripMenuItem1.Name = "sucursalesToolStripMenuItem1";
            sucursalesToolStripMenuItem1.Size = new Size(32, 19);
            sucursalesToolStripMenuItem1.Text = "📍 Sucursales";
            // 
            // FrmMain
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(248, 250, 252);
            ClientSize = new Size(1371, 1000);
            Controls.Add(mainPanel);
            Controls.Add(sidebarPanel);
            Controls.Add(menuStripMain);
            Font = new Font("Segoe UI", 9F);
            MainMenuStrip = menuStripMain;
            Margin = new Padding(3, 4, 3, 4);
            MinimumSize = new Size(1140, 851);
            Name = "FrmMain";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "VetCare - Sistema de Gestión de Clínica Veterinaria";
            menuStripMain.ResumeLayout(false);
            menuStripMain.PerformLayout();
            sidebarPanel.ResumeLayout(false);
            flowNavPanel.ResumeLayout(false);
            logoPanel.ResumeLayout(false);
            logoPanel.PerformLayout();
            mainPanel.ResumeLayout(false);
            tableLayoutPanelContent.ResumeLayout(false);
            panelCitasDetail.ResumeLayout(false);
            panelCitasDetail.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvCitas).EndInit();
            panelRightLayout.ResumeLayout(false);
            panelSystemSummary.ResumeLayout(false);
            panelSystemSummary.PerformLayout();
            tableLayoutPanelMetrics.ResumeLayout(false);
            tableLayoutPanelMetrics.PerformLayout();
            panelActions.ResumeLayout(false);
            panelActions.PerformLayout();
            tableLayoutPanelButtons.ResumeLayout(false);
            tableLayoutPanelKPIs.ResumeLayout(false);
            panelCardIngresos.ResumeLayout(false);
            panelCardIngresos.PerformLayout();
            panelCardCitas.ResumeLayout(false);
            panelCardCitas.PerformLayout();
            panelCardCompras.ResumeLayout(false);
            panelCardCompras.PerformLayout();
            panelCardVentas.ResumeLayout(false);
            panelCardVentas.PerformLayout();
            panelHeader.ResumeLayout(false);
            panelHeader.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStripMain;
        private System.Windows.Forms.ToolStripMenuItem inicioToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem OperacionesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ClinicaToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem invToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem FinanzasToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem OpcionToolStripMenuItem;
        
        private System.Windows.Forms.Panel sidebarPanel;
        private System.Windows.Forms.Panel logoPanel;
        private System.Windows.Forms.Label lblLogo;
        private System.Windows.Forms.Label lblLogoSubtitle;
        private System.Windows.Forms.Panel panelLogoSeparator;
        private System.Windows.Forms.FlowLayoutPanel flowNavPanel;
        private System.Windows.Forms.Button dashboardButton;
        private System.Windows.Forms.Button mascotasButton;
        private System.Windows.Forms.Button citasButton;
        private System.Windows.Forms.Button facturacionButton;
        private System.Windows.Forms.Button inventarioButton;
        private System.Windows.Forms.Button comprasButton;
        private System.Windows.Forms.Button reportesButton;
        
        private System.Windows.Forms.Panel mainPanel;
        private System.Windows.Forms.Panel panelHeader;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblSubtitle;
        private System.Windows.Forms.Label lblDate;
        
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanelKPIs;
        private System.Windows.Forms.Panel panelCardVentas;
        private System.Windows.Forms.Panel panelCardVentasAccent;
        private System.Windows.Forms.Label lblVentasTitle;
        private System.Windows.Forms.Label lblVentasValue;
        private System.Windows.Forms.Label lblVentasIcon;
        
        private System.Windows.Forms.Panel panelCardCompras;
        private System.Windows.Forms.Panel panelCardComprasAccent;
        private System.Windows.Forms.Label lblComprasTitle;
        private System.Windows.Forms.Label lblComprasValue;
        private System.Windows.Forms.Label lblComprasIcon;
        
        private System.Windows.Forms.Panel panelCardCitas;
        private System.Windows.Forms.Panel panelCardCitasAccent;
        private System.Windows.Forms.Label lblCitasCardTitle;
        private System.Windows.Forms.Label lblCitasValue;
        private System.Windows.Forms.Label lblCitasIcon;
        
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanelContent;
        private System.Windows.Forms.Panel panelCitasDetail;
        private System.Windows.Forms.Label lblCitasTitle;
        private System.Windows.Forms.Label lblCitasSubtitle;
        private System.Windows.Forms.DataGridView dgvCitas;
        
        private System.Windows.Forms.Panel panelRightLayout;
        private System.Windows.Forms.Panel panelActions;
        private System.Windows.Forms.Label lblActionsTitle;
        private System.Windows.Forms.Label lblActionsSubtitle;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanelButtons;
        private System.Windows.Forms.Button btnNewCita;
        private System.Windows.Forms.Button btnNewPet;
        private System.Windows.Forms.Button btnFacturar;
        private System.Windows.Forms.Button btnCompra;
        
        private System.Windows.Forms.Panel panelSpacer;
        
        private System.Windows.Forms.Panel panelSystemSummary;
        private System.Windows.Forms.Label lblSummaryTitle;
        private System.Windows.Forms.Label lblSummarySubtitle;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanelMetrics;
        private System.Windows.Forms.Label lblVetTitle;
        private System.Windows.Forms.Label lblVetValue;
        private System.Windows.Forms.Label lblHospTitle;
        private System.Windows.Forms.Label lblHospValue;
        private System.Windows.Forms.Label lblStockTitle;
        private ToolStripMenuItem sucursalesToolStripMenuItem1;
        private ToolStripMenuItem personasToolStripMenuItem;
        private ToolStripMenuItem mascotasToolStripMenuItem;
        private ToolStripMenuItem dueñosToolStripMenuItem;
        private ToolStripMenuItem citasVeterinariaToolStripMenuItem;
        private ToolStripMenuItem citasGroomingToolStripMenuItem;
        private ToolStripMenuItem vacunasToolStripMenuItem;
        private ToolStripMenuItem historialMedicoToolStripMenuItem;
        private ToolStripMenuItem productosToolStripMenuItem;
        private Label lblIngresosIcon;
        private Label lblIngresosValue;
        private Label lblIngresosTitle;
        private Panel panelCardIngresosAccent;
        private ToolStripMenuItem categoriasToolStripMenuItem;
        private ToolStripMenuItem proveedoresToolStripMenuItem;
        private ToolStripMenuItem inventarioSucursalToolStripMenuItem;
        private ToolStripMenuItem facturacionToolStripMenuItem;
        private ToolStripMenuItem detalleFacturaToolStripMenuItem;
        private ToolStripMenuItem comprasToolStripMenuItem;
        private ToolStripMenuItem detalleComprasToolStripMenuItem;
        private ToolStripMenuItem usuariosToolStripMenuItem;
        private ToolStripMenuItem configuracionToolStripMenuItem;
        private ToolStripMenuItem clinicaToolStripMenuItem1;
        private Panel panelCardIngresos;
    }
}
