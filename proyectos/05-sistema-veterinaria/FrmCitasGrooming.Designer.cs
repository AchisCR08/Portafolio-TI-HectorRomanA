namespace Veterinaria1
{
    partial class FrmCitasGrooming
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            panel1 = new Panel();
            label1 = new Label();
            panel2 = new Panel();
            cmbServicio = new ComboBox();
            cmbEstado = new ComboBox();
            cmbGroomer = new ComboBox();
            cmbMascota = new ComboBox();
            dtpHora = new DateTimePicker();
            dptFecha = new DateTimePicker();
            lblServicio = new Label();
            lblEstado = new Label();
            lblGroomer = new Label();
            lblMascota = new Label();
            lblHora = new Label();
            lblFecha = new Label();
            panel3 = new Panel();
            btnModificar = new Button();
            btnCancelar = new Button();
            btnGuardar = new Button();
            btnNuevo = new Button();
            dgvCitasGrooming = new DataGridView();
            panel1.SuspendLayout();
            panel2.SuspendLayout();
            panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvCitasGrooming).BeginInit();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.BackColor = SystemColors.GradientInactiveCaption;
            panel1.Controls.Add(label1);
            panel1.Dock = DockStyle.Top;
            panel1.Location = new Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(1206, 45);
            panel1.TabIndex = 0;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(449, 9);
            label1.Name = "label1";
            label1.Size = new Size(217, 20);
            label1.TabIndex = 0;
            label1.Text = "GESTIÓN DE CITAS GROOMING";
            // 
            // panel2
            // 
            panel2.BorderStyle = BorderStyle.FixedSingle;
            panel2.Controls.Add(cmbServicio);
            panel2.Controls.Add(cmbEstado);
            panel2.Controls.Add(cmbGroomer);
            panel2.Controls.Add(cmbMascota);
            panel2.Controls.Add(dtpHora);
            panel2.Controls.Add(dptFecha);
            panel2.Controls.Add(lblServicio);
            panel2.Controls.Add(lblEstado);
            panel2.Controls.Add(lblGroomer);
            panel2.Controls.Add(lblMascota);
            panel2.Controls.Add(lblHora);
            panel2.Controls.Add(lblFecha);
            panel2.Location = new Point(18, 70);
            panel2.Name = "panel2";
            panel2.Size = new Size(445, 355);
            panel2.TabIndex = 1;
            // 
            // cmbServicio
            // 
            cmbServicio.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbServicio.FormattingEnabled = true;
            cmbServicio.Location = new Point(151, 249);
            cmbServicio.Name = "cmbServicio";
            cmbServicio.Size = new Size(151, 28);
            cmbServicio.TabIndex = 11;
            // 
            // cmbEstado
            // 
            cmbEstado.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbEstado.FormattingEnabled = true;
            cmbEstado.Location = new Point(150, 201);
            cmbEstado.Name = "cmbEstado";
            cmbEstado.Size = new Size(151, 28);
            cmbEstado.TabIndex = 10;
            // 
            // cmbGroomer
            // 
            cmbGroomer.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbGroomer.FormattingEnabled = true;
            cmbGroomer.Location = new Point(151, 164);
            cmbGroomer.Name = "cmbGroomer";
            cmbGroomer.Size = new Size(151, 28);
            cmbGroomer.TabIndex = 9;
            // 
            // cmbMascota
            // 
            cmbMascota.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbMascota.FormattingEnabled = true;
            cmbMascota.Location = new Point(149, 119);
            cmbMascota.Name = "cmbMascota";
            cmbMascota.Size = new Size(151, 28);
            cmbMascota.TabIndex = 8;
            // 
            // dtpHora
            // 
            dtpHora.Format = DateTimePickerFormat.Time;
            dtpHora.Location = new Point(149, 73);
            dtpHora.Name = "dtpHora";
            dtpHora.ShowUpDown = true;
            dtpHora.Size = new Size(250, 27);
            dtpHora.TabIndex = 7;
            // 
            // dptFecha
            // 
            dptFecha.Format = DateTimePickerFormat.Short;
            dptFecha.Location = new Point(147, 27);
            dptFecha.Name = "dptFecha";
            dptFecha.Size = new Size(250, 27);
            dptFecha.TabIndex = 6;
            // 
            // lblServicio
            // 
            lblServicio.AutoSize = true;
            lblServicio.Location = new Point(25, 257);
            lblServicio.Name = "lblServicio";
            lblServicio.Size = new Size(64, 20);
            lblServicio.TabIndex = 5;
            lblServicio.Text = "Servicio:";
            // 
            // lblEstado
            // 
            lblEstado.AutoSize = true;
            lblEstado.Location = new Point(25, 204);
            lblEstado.Name = "lblEstado";
            lblEstado.Size = new Size(57, 20);
            lblEstado.TabIndex = 4;
            lblEstado.Text = "Estado:";
            // 
            // lblGroomer
            // 
            lblGroomer.AutoSize = true;
            lblGroomer.Location = new Point(25, 162);
            lblGroomer.Name = "lblGroomer";
            lblGroomer.Size = new Size(62, 20);
            lblGroomer.TabIndex = 3;
            lblGroomer.Text = "Gromer:";
            // 
            // lblMascota
            // 
            lblMascota.AutoSize = true;
            lblMascota.Location = new Point(25, 108);
            lblMascota.Name = "lblMascota";
            lblMascota.Size = new Size(62, 20);
            lblMascota.TabIndex = 2;
            lblMascota.Text = "Macota:";
            // 
            // lblHora
            // 
            lblHora.AutoSize = true;
            lblHora.Location = new Point(25, 68);
            lblHora.Name = "lblHora";
            lblHora.Size = new Size(45, 20);
            lblHora.TabIndex = 1;
            lblHora.Text = "Hora:";
            // 
            // lblFecha
            // 
            lblFecha.AutoSize = true;
            lblFecha.Location = new Point(25, 20);
            lblFecha.Name = "lblFecha";
            lblFecha.Size = new Size(50, 20);
            lblFecha.TabIndex = 0;
            lblFecha.Text = "Fecha:";
            // 
            // panel3
            // 
            panel3.Controls.Add(btnModificar);
            panel3.Controls.Add(btnCancelar);
            panel3.Controls.Add(btnGuardar);
            panel3.Controls.Add(btnNuevo);
            panel3.Location = new Point(18, 455);
            panel3.Name = "panel3";
            panel3.Size = new Size(445, 180);
            panel3.TabIndex = 2;
            // 
            // btnModificar
            // 
            btnModificar.Location = new Point(25, 117);
            btnModificar.Name = "btnModificar";
            btnModificar.Size = new Size(125, 43);
            btnModificar.TabIndex = 3;
            btnModificar.Text = "Modificar";
            btnModificar.UseVisualStyleBackColor = true;
            btnModificar.Click += btnModificar_Click;
            // 
            // btnCancelar
            // 
            btnCancelar.Location = new Point(264, 117);
            btnCancelar.Name = "btnCancelar";
            btnCancelar.Size = new Size(125, 43);
            btnCancelar.TabIndex = 2;
            btnCancelar.Text = "Cancelar";
            btnCancelar.UseVisualStyleBackColor = true;
            btnCancelar.Click += btnCancelar_Click;
            // 
            // btnGuardar
            // 
            btnGuardar.Location = new Point(264, 34);
            btnGuardar.Name = "btnGuardar";
            btnGuardar.Size = new Size(125, 43);
            btnGuardar.TabIndex = 1;
            btnGuardar.Text = "Guardar";
            btnGuardar.UseVisualStyleBackColor = true;
            btnGuardar.Click += btnGuardar_Click;
            // 
            // btnNuevo
            // 
            btnNuevo.Location = new Point(25, 34);
            btnNuevo.Name = "btnNuevo";
            btnNuevo.Size = new Size(125, 43);
            btnNuevo.TabIndex = 0;
            btnNuevo.Text = "Nuevo";
            btnNuevo.UseVisualStyleBackColor = true;
            btnNuevo.Click += btnNuevo_Click;
            // 
            // dgvCitasGrooming
            // 
            dgvCitasGrooming.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvCitasGrooming.Location = new Point(495, 70);
            dgvCitasGrooming.Name = "dgvCitasGrooming";
            dgvCitasGrooming.RowHeadersWidth = 51;
            dgvCitasGrooming.Size = new Size(700, 565);
            dgvCitasGrooming.TabIndex = 3;
            dgvCitasGrooming.CellContentClick += dgvCitasGrooming_CellContentClick;
            dgvCitasGrooming.SelectionChanged += dgvCitasGrooming_SelectionChanged;
            // 
            // FrmCitasGrooming
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1206, 643);
            Controls.Add(dgvCitasGrooming);
            Controls.Add(panel3);
            Controls.Add(panel2);
            Controls.Add(panel1);
            Name = "FrmCitasGrooming";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Citas grooming";
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            panel2.ResumeLayout(false);
            panel2.PerformLayout();
            panel3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvCitasGrooming).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Panel panel1;
        private Label label1;
        private Panel panel2;
        private Panel panel3;
        private Button btnModificar;
        private Button btnCancelar;
        private Button btnGuardar;
        private Button btnNuevo;
        private DataGridView dgvCitasGrooming;
        private Label lblServicio;
        private Label lblEstado;
        private Label lblGroomer;
        private Label lblMascota;
        private Label lblHora;
        private Label lblFecha;
        private ComboBox cmbServicio;
        private ComboBox cmbEstado;
        private ComboBox cmbGroomer;
        private ComboBox cmbMascota;
        private DateTimePicker dtpHora;
        private DateTimePicker dptFecha;
    }
}