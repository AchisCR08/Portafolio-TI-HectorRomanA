namespace Veterinaria1
{
    partial class FrmVacunas1
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
            panel3 = new Panel();
            btnGuardar = new Button();
            btnModificar = new Button();
            btnCancelar = new Button();
            btnNuevo = new Button();
            dgvVacunas = new DataGridView();
            lblMascota = new Label();
            lblVacuna = new Label();
            lblFecha = new Label();
            lblProximas = new Label();
            cmbMascotas = new ComboBox();
            cmbVacunas = new ComboBox();
            dtpAplicacion = new DateTimePicker();
            dtpProxima = new DateTimePicker();
            panel1.SuspendLayout();
            panel2.SuspendLayout();
            panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvVacunas).BeginInit();
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
            label1.Location = new Point(332, 10);
            label1.Name = "label1";
            label1.Size = new Size(62, 20);
            label1.TabIndex = 0;
            label1.Text = "Vacunas";
            // 
            // panel2
            // 
            panel2.BackColor = SystemColors.ControlLightLight;
            panel2.BorderStyle = BorderStyle.FixedSingle;
            panel2.Controls.Add(dtpProxima);
            panel2.Controls.Add(dtpAplicacion);
            panel2.Controls.Add(cmbVacunas);
            panel2.Controls.Add(cmbMascotas);
            panel2.Controls.Add(lblProximas);
            panel2.Controls.Add(lblFecha);
            panel2.Controls.Add(lblVacuna);
            panel2.Controls.Add(lblMascota);
            panel2.Location = new Point(18, 70);
            panel2.Name = "panel2";
            panel2.Size = new Size(445, 355);
            panel2.TabIndex = 1;
            // 
            // panel3
            // 
            panel3.BackColor = SystemColors.GradientInactiveCaption;
            panel3.Controls.Add(btnGuardar);
            panel3.Controls.Add(btnModificar);
            panel3.Controls.Add(btnCancelar);
            panel3.Controls.Add(btnNuevo);
            panel3.Location = new Point(18, 455);
            panel3.Name = "panel3";
            panel3.Size = new Size(445, 180);
            panel3.TabIndex = 2;
            // 
            // btnGuardar
            // 
            btnGuardar.Location = new Point(251, 115);
            btnGuardar.Name = "btnGuardar";
            btnGuardar.Size = new Size(125, 43);
            btnGuardar.TabIndex = 3;
            btnGuardar.Text = "Guardar";
            btnGuardar.UseVisualStyleBackColor = true;
            btnGuardar.Click += btnGuardar_Click;
            // 
            // btnModificar
            // 
            btnModificar.Location = new Point(18, 115);
            btnModificar.Name = "btnModificar";
            btnModificar.Size = new Size(125, 43);
            btnModificar.TabIndex = 2;
            btnModificar.Text = "Modificar";
            btnModificar.UseVisualStyleBackColor = true;
            btnModificar.Click += btnModificar_Click;
            // 
            // btnCancelar
            // 
            btnCancelar.Location = new Point(251, 28);
            btnCancelar.Name = "btnCancelar";
            btnCancelar.Size = new Size(125, 43);
            btnCancelar.TabIndex = 1;
            btnCancelar.Text = "Cancelar";
            btnCancelar.UseVisualStyleBackColor = true;
            btnCancelar.Click += btnCancelar_Click;
            // 
            // btnNuevo
            // 
            btnNuevo.Location = new Point(18, 28);
            btnNuevo.Name = "btnNuevo";
            btnNuevo.Size = new Size(125, 43);
            btnNuevo.TabIndex = 0;
            btnNuevo.Text = "Nuevo";
            btnNuevo.UseVisualStyleBackColor = true;
            btnNuevo.Click += btnNuevo_Click;
            // 
            // dgvVacunas
            // 
            dgvVacunas.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvVacunas.Location = new Point(495, 70);
            dgvVacunas.Name = "dgvVacunas";
            dgvVacunas.RowHeadersWidth = 51;
            dgvVacunas.Size = new Size(700, 565);
            dgvVacunas.TabIndex = 3;
            dgvVacunas.CellContentClick += dgvVacunas_CellContentClick;
            dgvVacunas.SelectionChanged += dgvVacunas_SelectionChanged;
            // 
            // lblMascota
            // 
            lblMascota.AutoSize = true;
            lblMascota.Location = new Point(16, 28);
            lblMascota.Name = "lblMascota";
            lblMascota.Size = new Size(68, 20);
            lblMascota.TabIndex = 0;
            lblMascota.Text = "Mascota:";
            lblMascota.Click += label2_Click;
            // 
            // lblVacuna
            // 
            lblVacuna.AutoSize = true;
            lblVacuna.Location = new Point(16, 75);
            lblVacuna.Name = "lblVacuna";
            lblVacuna.Size = new Size(59, 20);
            lblVacuna.TabIndex = 1;
            lblVacuna.Text = "Vacuna:";
            // 
            // lblFecha
            // 
            lblFecha.AutoSize = true;
            lblFecha.Location = new Point(17, 126);
            lblFecha.Name = "lblFecha";
            lblFecha.Size = new Size(143, 20);
            lblFecha.TabIndex = 2;
            lblFecha.Text = "Fecha de aplicacion:";
            // 
            // lblProximas
            // 
            lblProximas.AutoSize = true;
            lblProximas.Location = new Point(17, 188);
            lblProximas.Name = "lblProximas";
            lblProximas.Size = new Size(110, 20);
            lblProximas.TabIndex = 3;
            lblProximas.Text = "Proximas dosis:";
            // 
            // cmbMascotas
            // 
            cmbMascotas.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbMascotas.FormattingEnabled = true;
            cmbMascotas.Location = new Point(175, 25);
            cmbMascotas.Name = "cmbMascotas";
            cmbMascotas.Size = new Size(240, 28);
            cmbMascotas.TabIndex = 4;
            // 
            // cmbVacunas
            // 
            cmbVacunas.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbVacunas.FormattingEnabled = true;
            cmbVacunas.Location = new Point(175, 75);
            cmbVacunas.Name = "cmbVacunas";
            cmbVacunas.Size = new Size(240, 28);
            cmbVacunas.TabIndex = 5;
            // 
            // dtpAplicacion
            // 
            dtpAplicacion.Format = DateTimePickerFormat.Short;
            dtpAplicacion.Location = new Point(175, 126);
            dtpAplicacion.Name = "dtpAplicacion";
            dtpAplicacion.Size = new Size(240, 27);
            dtpAplicacion.TabIndex = 6;
            // 
            // dtpProxima
            // 
            dtpProxima.Format = DateTimePickerFormat.Short;
            dtpProxima.Location = new Point(175, 181);
            dtpProxima.Name = "dtpProxima";
            dtpProxima.ShowCheckBox = true;
            dtpProxima.Size = new Size(240, 27);
            dtpProxima.TabIndex = 7;
            // 
            // FrmVacunas1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1206, 643);
            Controls.Add(dgvVacunas);
            Controls.Add(panel3);
            Controls.Add(panel2);
            Controls.Add(panel1);
            Name = "FrmVacunas1";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "FrmVacunas1";
            Load += FrmVacunas1_Load;
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            panel2.ResumeLayout(false);
            panel2.PerformLayout();
            panel3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvVacunas).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Panel panel1;
        private Label label1;
        private Panel panel2;
        private Panel panel3;
        private Button btnGuardar;
        private Button btnModificar;
        private Button btnCancelar;
        private Button btnNuevo;
        private DataGridView dgvVacunas;
        private Label lblProximas;
        private Label lblFecha;
        private Label lblVacuna;
        private Label lblMascota;
        private DateTimePicker dtpProxima;
        private DateTimePicker dtpAplicacion;
        private ComboBox cmbVacunas;
        private ComboBox cmbMascotas;
    }
}