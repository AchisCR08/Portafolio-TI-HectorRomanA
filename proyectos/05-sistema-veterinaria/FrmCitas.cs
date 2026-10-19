using Entidades;
using Repositorio;

namespace Veterinaria1;

public partial class FrmCitas : Form
{
    private readonly bool _grooming;
    private readonly AtencionRepository _repo = new();
    public FrmCitas(bool grooming) { _grooming=grooming; InitializeComponent(); ConfigurarPantalla(); CargarDatos(); }
    private void ConfigurarPantalla() { Text=_grooming ? "Citas Grooming" : "Citas Veterinaria"; lblTitulo.Text=_grooming ? "GESTIÓN DE CITAS GROOMING" : "GESTIÓN DE CITAS VETERINARIAS"; lblResponsable.Text=_grooming ? "Groomer" : "Veterinario"; lblDetalle.Text=_grooming ? "Servicio" : "Motivo"; cmbServicio.Visible=_grooming; txtMotivo.Visible=!_grooming; }
    private void CargarDatos() { Enlazar(cmbMascota,_repo.Mascotas()); Enlazar(cmbResponsable,_repo.Personas()); Enlazar(cmbEstado,_repo.Estados()); if(_grooming) Enlazar(cmbServicio,_repo.Servicios()); Refrescar(); Limpiar(); }
    private static void Enlazar(ComboBox combo,IEnumerable<OpcionCatalogo> datos) { combo.DataSource=datos.ToList();combo.DisplayMember="Nombre";combo.ValueMember="Id"; }
    private void Refrescar() { dgvCitas.DataSource=_repo.ListarCitas(_grooming,SesionActual.IdSucursal).ToList(); foreach(DataGridViewColumn c in dgvCitas.Columns) c.HeaderText=c.HeaderText switch { "Id"=>"Código", "Servicio"=>_grooming ? "Servicio" : "Motivo", _=>c.HeaderText }; }
    private void btnGuardar_Click(object sender,EventArgs e) { if(cmbMascota.SelectedValue is not int mascota || cmbResponsable.SelectedValue is not int responsable || cmbEstado.SelectedValue is not int estado || (_grooming && cmbServicio.SelectedValue is not int)) { MessageBox.Show("Complete todos los campos obligatorios.");return; } try { _repo.CrearCita(_grooming,dtpFecha.Value.Date,dtpHora.Value.TimeOfDay,mascota,responsable,estado,SesionActual.IdSucursal,txtMotivo.Text.Trim(),_grooming?(int)cmbServicio.SelectedValue:null); Refrescar(); Limpiar(); MessageBox.Show("Cita guardada correctamente."); } catch(Exception ex) { MessageBox.Show(ex.Message,"Error"); } }
    private void btnNuevo_Click(object sender,EventArgs e) => Limpiar();
    private void btnCancelar_Click(object sender,EventArgs e) => Limpiar();
    private void Limpiar() { dtpFecha.Value=DateTime.Today;dtpHora.Value=DateTime.Now;txtMotivo.Clear();cmbMascota.SelectedIndex=-1;cmbResponsable.SelectedIndex=-1;cmbEstado.SelectedIndex=-1;cmbServicio.SelectedIndex=-1;dgvCitas.ClearSelection(); }
}
