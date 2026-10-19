using Entidades;
using Repositorio;

namespace Veterinaria1;

public class FrmGestionClientes : Form
{
    private readonly GestionMascotasRepository _repo = new();
    private readonly TabControl _tabs = new() { Dock = DockStyle.Fill };

    private readonly DataGridView _dgvPersonas = CrearGrid();
    private readonly TextBox _txtPersonaNombre = new();
    private readonly TextBox _txtPersonaTelefono = new();
    private readonly TextBox _txtPersonaCorreo = new();
    private readonly TextBox _txtPersonaDireccion = new();
    private readonly CheckBox _chkPersonaActivo = new() { Text = "Activo", Checked = true };
    private int _idPersona;

    private readonly DataGridView _dgvMascotas = CrearGrid();
    private readonly TextBox _txtMascotaNombre = new();
    private readonly DateTimePicker _dtpNacimiento = new() { Format = DateTimePickerFormat.Short, ShowCheckBox = true };
    private readonly ComboBox _cmbSexo = new() { DropDownStyle = ComboBoxStyle.DropDownList };
    private readonly TextBox _txtPeso = new();
    private readonly ComboBox _cmbRaza = new() { DropDownStyle = ComboBoxStyle.DropDownList };
    private readonly ComboBox _cmbDuenoPrincipal = new() { DropDownStyle = ComboBoxStyle.DropDownList };
    private readonly CheckBox _chkMascotaActiva = new() { Text = "Activa", Checked = true };
    private int _idMascota;

    private readonly DataGridView _dgvDuenos = CrearGrid();
    private readonly ComboBox _cmbDueno = new() { DropDownStyle = ComboBoxStyle.DropDownList };
    private readonly ComboBox _cmbMascotaDueno = new() { DropDownStyle = ComboBoxStyle.DropDownList };
    private readonly CheckBox _chkPrincipal = new() { Text = "Dueño principal" };

    public FrmGestionClientes(int pestanaInicial = 0)
    {
        Text = "Gestión de personas y mascotas";
        StartPosition = FormStartPosition.CenterScreen;
        MinimumSize = new Size(950, 650);
        Controls.Add(_tabs);

        CrearPestanaPersonas();
        CrearPestanaMascotas();
        CrearPestanaDuenos();
        Load += (_, _) => CargarTodo();
        _tabs.SelectedIndex = Math.Clamp(pestanaInicial, 0, 2);
    }

    private static DataGridView CrearGrid() => new()
    {
        Dock = DockStyle.Fill,
        ReadOnly = true,
        AllowUserToAddRows = false,
        AllowUserToDeleteRows = false,
        SelectionMode = DataGridViewSelectionMode.FullRowSelect,
        MultiSelect = false,
        AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
        BackgroundColor = Color.White
    };

    private static Label Etiqueta(string texto) => new() { Text = texto, AutoSize = true, Margin = new Padding(4, 10, 4, 2) };

    private static void AgregarCampo(FlowLayoutPanel panel, string etiqueta, Control control)
    {
        panel.Controls.Add(Etiqueta(etiqueta));
        control.Width = 230;
        panel.Controls.Add(control);
    }

    private static Button Boton(string texto, EventHandler accion)
    {
        var boton = new Button { Text = texto, AutoSize = true, Margin = new Padding(4, 12, 4, 4) };
        boton.Click += accion;
        return boton;
    }

    private void CrearPestanaPersonas()
    {
        var tab = new TabPage("Personas");
        var division = new SplitContainer { Dock = DockStyle.Fill, SplitterDistance = 320 };
        var formulario = new FlowLayoutPanel { Dock = DockStyle.Fill, FlowDirection = FlowDirection.TopDown, Padding = new Padding(14), AutoScroll = true };
        formulario.Controls.Add(new Label { Text = "Datos de la persona", Font = new Font(Font, FontStyle.Bold), AutoSize = true });
        AgregarCampo(formulario, "Nombre *", _txtPersonaNombre);
        AgregarCampo(formulario, "Teléfono", _txtPersonaTelefono);
        AgregarCampo(formulario, "Correo", _txtPersonaCorreo);
        AgregarCampo(formulario, "Dirección", _txtPersonaDireccion);
        formulario.Controls.Add(_chkPersonaActivo);
        formulario.Controls.Add(Boton("Guardar", GuardarPersona));
        formulario.Controls.Add(Boton("Nuevo", (_, _) => LimpiarPersona()));
        formulario.Controls.Add(Boton("Desactivar", DesactivarPersona));
        division.Panel1.Controls.Add(formulario);
        division.Panel2.Controls.Add(_dgvPersonas);
        _dgvPersonas.SelectionChanged += (_, _) => CargarPersonaSeleccionada();
        tab.Controls.Add(division);
        _tabs.TabPages.Add(tab);
    }

    private void CrearPestanaMascotas()
    {
        var tab = new TabPage("Mascotas");
        var division = new SplitContainer { Dock = DockStyle.Fill, SplitterDistance = 320 };
        var formulario = new FlowLayoutPanel { Dock = DockStyle.Fill, FlowDirection = FlowDirection.TopDown, Padding = new Padding(14), AutoScroll = true };
        formulario.Controls.Add(new Label { Text = "Datos de la mascota", Font = new Font(Font, FontStyle.Bold), AutoSize = true });
        AgregarCampo(formulario, "Nombre *", _txtMascotaNombre);
        AgregarCampo(formulario, "Nacimiento", _dtpNacimiento);
        AgregarCampo(formulario, "Sexo", _cmbSexo);
        AgregarCampo(formulario, "Peso (kg)", _txtPeso);
        AgregarCampo(formulario, "Raza *", _cmbRaza);
        AgregarCampo(formulario, "Dueño principal", _cmbDuenoPrincipal);
        formulario.Controls.Add(_chkMascotaActiva);
        formulario.Controls.Add(Boton("Guardar", GuardarMascota));
        formulario.Controls.Add(Boton("Nuevo", (_, _) => LimpiarMascota()));
        formulario.Controls.Add(Boton("Desactivar", DesactivarMascota));
        division.Panel1.Controls.Add(formulario);
        division.Panel2.Controls.Add(_dgvMascotas);
        _dgvMascotas.SelectionChanged += (_, _) => CargarMascotaSeleccionada();
        tab.Controls.Add(division);
        _tabs.TabPages.Add(tab);
    }

    private void CrearPestanaDuenos()
    {
        var tab = new TabPage("Dueños – Mascotas");
        var division = new SplitContainer { Dock = DockStyle.Fill, SplitterDistance = 320 };
        var formulario = new FlowLayoutPanel { Dock = DockStyle.Fill, FlowDirection = FlowDirection.TopDown, Padding = new Padding(14), AutoScroll = true };
        formulario.Controls.Add(new Label { Text = "Asignar dueño a mascota", Font = new Font(Font, FontStyle.Bold), AutoSize = true });
        AgregarCampo(formulario, "Persona", _cmbDueno);
        AgregarCampo(formulario, "Mascota", _cmbMascotaDueno);
        formulario.Controls.Add(_chkPrincipal);
        formulario.Controls.Add(Boton("Asignar / actualizar", AsignarDueno));
        formulario.Controls.Add(Boton("Quitar relación", QuitarDueno));
        formulario.Controls.Add(Boton("Limpiar", (_, _) => LimpiarDueno()));
        division.Panel1.Controls.Add(formulario);
        division.Panel2.Controls.Add(_dgvDuenos);
        _dgvDuenos.SelectionChanged += (_, _) => CargarDuenoSeleccionado();
        tab.Controls.Add(division);
        _tabs.TabPages.Add(tab);
    }

    private void CargarTodo()
    {
        _cmbSexo.DataSource = new[] { "M", "H" };
        CargarPersonas();
        CargarMascotas();
        CargarDuenos();
        LimpiarPersona();
        LimpiarMascota();
        LimpiarDueno();
    }

    private void CargarPersonas()
    {
        var personas = _repo.ListarPersonas().ToList();
        _dgvPersonas.DataSource = personas;
        _cmbDuenoPrincipal.DataSource = personas.Where(p => p.Activo).ToList();
        _cmbDuenoPrincipal.DisplayMember = "Nombre";
        _cmbDuenoPrincipal.ValueMember = "IdPersona";
        _cmbDueno.DataSource = personas.Where(p => p.Activo).ToList();
        _cmbDueno.DisplayMember = "Nombre";
        _cmbDueno.ValueMember = "IdPersona";
    }

    private void CargarMascotas()
    {
        var mascotas = _repo.ListarMascotas().ToList();
        _dgvMascotas.DataSource = mascotas;
        _cmbMascotaDueno.DataSource = mascotas.Where(m => m.Activo).ToList();
        _cmbMascotaDueno.DisplayMember = "Nombre";
        _cmbMascotaDueno.ValueMember = "IdMascota";
        _cmbRaza.DataSource = _repo.ListarRazas().ToList();
        _cmbRaza.DisplayMember = "Nombre";
        _cmbRaza.ValueMember = "Id";
    }

    private void CargarDuenos() => _dgvDuenos.DataSource = _repo.ListarDuenosMascotas().ToList();

    private void GuardarPersona(object? sender, EventArgs e)
    {
        if (string.IsNullOrWhiteSpace(_txtPersonaNombre.Text)) { MessageBox.Show("El nombre es obligatorio."); return; }
        try
        {
            _repo.GuardarPersona(new Persona { IdPersona = _idPersona, Nombre = _txtPersonaNombre.Text.Trim(), Telefono = _txtPersonaTelefono.Text.Trim(), Correo = _txtPersonaCorreo.Text.Trim(), Direccion = _txtPersonaDireccion.Text.Trim(), IdSucursal = SesionActual.IdSucursal, Activo = _chkPersonaActivo.Checked });
            CargarPersonas(); LimpiarPersona(); MessageBox.Show("Persona guardada correctamente.");
        }
        catch (Exception ex) { MessageBox.Show($"Error: {ex.Message}"); }
    }

    private void DesactivarPersona(object? sender, EventArgs e)
    {
        if (_idPersona == 0) { MessageBox.Show("Seleccione una persona."); return; }
        if (MessageBox.Show("¿Desactivar esta persona?", "Confirmar", MessageBoxButtons.YesNo) != DialogResult.Yes) return;
        try { _repo.DesactivarPersona(_idPersona); CargarPersonas(); LimpiarPersona(); MessageBox.Show("Persona desactivada."); }
        catch (Exception ex) { MessageBox.Show($"Error: {ex.Message}"); }
    }

    private void GuardarMascota(object? sender, EventArgs e)
    {
        if (string.IsNullOrWhiteSpace(_txtMascotaNombre.Text) || _cmbRaza.SelectedValue is not int idRaza) { MessageBox.Show("Nombre y raza son obligatorios."); return; }
        if (!string.IsNullOrWhiteSpace(_txtPeso.Text) && (!decimal.TryParse(_txtPeso.Text, out var peso) || peso <= 0)) { MessageBox.Show("El peso debe ser un número mayor a cero."); return; }
        decimal? pesoActual = string.IsNullOrWhiteSpace(_txtPeso.Text) ? null : decimal.Parse(_txtPeso.Text);
        try
        {
            _repo.GuardarMascota(new MascotaGestion { IdMascota = _idMascota, Nombre = _txtMascotaNombre.Text.Trim(), FechaNacimiento = _dtpNacimiento.Checked ? _dtpNacimiento.Value.Date : null, Sexo = _cmbSexo.SelectedItem?.ToString(), PesoActual = pesoActual, IdRaza = idRaza, Activo = _chkMascotaActiva.Checked }, _idMascota == 0 && _cmbDuenoPrincipal.SelectedValue is int idDueno ? idDueno : null);
            CargarMascotas(); CargarDuenos(); LimpiarMascota(); MessageBox.Show("Mascota guardada correctamente.");
        }
        catch (Exception ex) { MessageBox.Show($"Error: {ex.Message}"); }
    }

    private void DesactivarMascota(object? sender, EventArgs e)
    {
        if (_idMascota == 0) { MessageBox.Show("Seleccione una mascota."); return; }
        if (MessageBox.Show("¿Desactivar esta mascota?", "Confirmar", MessageBoxButtons.YesNo) != DialogResult.Yes) return;
        try { _repo.DesactivarMascota(_idMascota); CargarMascotas(); LimpiarMascota(); MessageBox.Show("Mascota desactivada."); }
        catch (Exception ex) { MessageBox.Show($"Error: {ex.Message}"); }
    }

    private void AsignarDueno(object? sender, EventArgs e)
    {
        if (_cmbDueno.SelectedValue is not int idPersona || _cmbMascotaDueno.SelectedValue is not int idMascota) { MessageBox.Show("Seleccione persona y mascota."); return; }
        try { _repo.AsignarDueno(idPersona, idMascota, _chkPrincipal.Checked); CargarDuenos(); LimpiarDueno(); MessageBox.Show("Relación guardada correctamente."); }
        catch (Exception ex) { MessageBox.Show($"Error: {ex.Message}"); }
    }

    private void QuitarDueno(object? sender, EventArgs e)
    {
        if (_dgvDuenos.CurrentRow?.DataBoundItem is not DuenoMascotaRegistro relacion) { MessageBox.Show("Seleccione una relación."); return; }
        if (MessageBox.Show("¿Quitar esta relación?", "Confirmar", MessageBoxButtons.YesNo) != DialogResult.Yes) return;
        try { _repo.QuitarDueno(relacion.IdPersona, relacion.IdMascota); CargarDuenos(); LimpiarDueno(); MessageBox.Show("Relación eliminada."); }
        catch (Exception ex) { MessageBox.Show($"Error: {ex.Message}"); }
    }

    private void CargarPersonaSeleccionada()
    {
        if (_dgvPersonas.CurrentRow?.DataBoundItem is not Persona persona) return;
        _idPersona = persona.IdPersona; _txtPersonaNombre.Text = persona.Nombre; _txtPersonaTelefono.Text = persona.Telefono; _txtPersonaCorreo.Text = persona.Correo; _txtPersonaDireccion.Text = persona.Direccion; _chkPersonaActivo.Checked = persona.Activo;
    }

    private void CargarMascotaSeleccionada()
    {
        if (_dgvMascotas.CurrentRow?.DataBoundItem is not MascotaGestion mascota) return;
        _idMascota = mascota.IdMascota; _txtMascotaNombre.Text = mascota.Nombre; _dtpNacimiento.Checked = mascota.FechaNacimiento.HasValue; if (mascota.FechaNacimiento.HasValue) _dtpNacimiento.Value = mascota.FechaNacimiento.Value; _cmbSexo.SelectedItem = mascota.Sexo; _txtPeso.Text = mascota.PesoActual?.ToString("0.##") ?? ""; _cmbRaza.SelectedValue = mascota.IdRaza; _chkMascotaActiva.Checked = mascota.Activo;
    }

    private void CargarDuenoSeleccionado()
    {
        if (_dgvDuenos.CurrentRow?.DataBoundItem is not DuenoMascotaRegistro relacion) return;
        _cmbDueno.SelectedValue = relacion.IdPersona; _cmbMascotaDueno.SelectedValue = relacion.IdMascota; _chkPrincipal.Checked = relacion.EsPrincipal;
    }

    private void LimpiarPersona() { _idPersona = 0; _txtPersonaNombre.Clear(); _txtPersonaTelefono.Clear(); _txtPersonaCorreo.Clear(); _txtPersonaDireccion.Clear(); _chkPersonaActivo.Checked = true; _dgvPersonas.ClearSelection(); }
    private void LimpiarMascota() { _idMascota = 0; _txtMascotaNombre.Clear(); _dtpNacimiento.Checked = false; _cmbSexo.SelectedIndex = -1; _txtPeso.Clear(); _cmbRaza.SelectedIndex = -1; _cmbDuenoPrincipal.SelectedIndex = -1; _chkMascotaActiva.Checked = true; _dgvMascotas.ClearSelection(); }
    private void LimpiarDueno() { _cmbDueno.SelectedIndex = -1; _cmbMascotaDueno.SelectedIndex = -1; _chkPrincipal.Checked = false; _dgvDuenos.ClearSelection(); }
}
