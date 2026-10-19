using Repositorio;

namespace Veterinaria1;

public class FrmReportes : Form
{
    private readonly ReportesRepository _repo = new();
    private readonly TabControl _tabs = new() { Dock = DockStyle.Fill };

    public FrmReportes()
    {
        Text = "Reportes";
        StartPosition = FormStartPosition.CenterScreen;
        MinimumSize = new Size(950, 650);
        Controls.Add(_tabs);

        AgregarPestana("Clientes activos", () => _repo.ClientesActivos());
        AgregarPestana("Mascotas", () => _repo.MascotasDetalle());
        AgregarPestana("Dueños - Mascotas", () => _repo.DuenosMascotas());
        AgregarPestana("Citas veterinarias", () => _repo.CitasVeterinarias());
        AgregarPestana("Citas grooming", () => _repo.CitasGrooming());
        AgregarPestana("Vacunas pendientes", () => _repo.VacunasPendientes());
        AgregarPestana("Inventario", () => _repo.InventarioActual());
        AgregarPestana("Catalogo de productos", () => _repo.CatalogoProductos());
        AgregarPestana("Facturacion", () => _repo.FacturacionDetalle());
        AgregarPestana("Facturacion por sucursal", () => _repo.FacturacionPorSucursal());

        // Cargar la primera pestana al abrir; el resto se cargan al seleccionarlas
        if (_tabs.TabPages.Count > 0)
            CargarPestana(_tabs.TabPages[0]);
    }

    private void AgregarPestana(string titulo, Func<object> cargarDatos)
    {
        var grid = new DataGridView
        {
            Dock = DockStyle.Fill,
            ReadOnly = true,
            AllowUserToAddRows = false,
            AllowUserToDeleteRows = false,
            SelectionMode = DataGridViewSelectionMode.FullRowSelect,
            AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
            BackgroundColor = Color.White
        };

        var tab = new TabPage(titulo) { Tag = new PestanaReporte(grid, cargarDatos, cargada: false) };
        tab.Controls.Add(grid);
        _tabs.TabPages.Add(tab);
    }

    protected override void OnLoad(EventArgs e)
    {
        base.OnLoad(e);
        _tabs.SelectedIndexChanged += (_, _) =>
        {
            if (_tabs.SelectedTab is { } tab)
                CargarPestana(tab);
        };
    }

    private void CargarPestana(TabPage tab)
    {
        if (tab.Tag is not PestanaReporte info || info.Cargada) return;

        try
        {
            info.Grid.DataSource = info.CargarDatos();
            info.Cargada = true;
        }
        catch (Exception ex)
        {
            MessageBox.Show($"No se pudo cargar el reporte '{tab.Text}': {ex.Message}");
        }
    }

    private class PestanaReporte
    {
        public DataGridView Grid { get; }
        public Func<object> CargarDatos { get; }
        public bool Cargada { get; set; }

        public PestanaReporte(DataGridView grid, Func<object> cargarDatos, bool cargada)
        {
            Grid = grid;
            CargarDatos = cargarDatos;
            Cargada = cargada;
        }
    }
}
