namespace Veterinaria1
{
    partial class FrmCitasVeterinaria
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
            lblTitulo = new Label();
            panel2 = new Panel();
            txtMotivo = new TextBox();
            cmbEstado = new ComboBox();
            cmbVeterinario = new ComboBox();
            cobMascota = new ComboBox();
            dtpHora = new DateTimePicker();
            dtpFecha = new DateTimePicker();
            label6 = new Label();
            label5 = new Label();
            label4 = new Label();
            label3 = new Label();
            label2 = new Label();
            label1 = new Label();
            panel3 = new Panel();
            btnModificar = new Button();
            btnCancelar = new Button();
            btnNuevo = new Button();
            btnGuardar = new Button();
            dgvCitas = new DataGridView();
            panel1.SuspendLayout();
            panel2.SuspendLayout();
            panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvCitas).BeginInit();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.BackColor = SystemColors.GradientInactiveCaption;
            panel1.Controls.Add(lblTitulo);
            panel1.Dock = DockStyle.Top;
            panel1.Location = new Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(1206, 45);
            panel1.TabIndex = 0;
            // 
            // lblTitulo
            // 
            lblTitulo.AutoSize = true;
            lblTitulo.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            lblTitulo.Location = new Point(18, 11);
            lblTitulo.Name = "lblTitulo";
            lblTitulo.Size = new Size(335, 28);
            lblTitulo.TabIndex = 0;
            lblTitulo.Text = "GESTIÓN DE CITAS VETERINARIAS";
            // 
            // panel2
            // 
            panel2.BorderStyle = BorderStyle.FixedSingle;
            panel2.Controls.Add(txtMotivo);
            panel2.Controls.Add(cmbEstado);
            panel2.Controls.Add(cmbVeterinario);
            panel2.Controls.Add(cobMascota);
            panel2.Controls.Add(dtpHora);
            panel2.Controls.Add(dtpFecha);
            panel2.Controls.Add(label6);
            panel2.Controls.Add(label5);
            panel2.Controls.Add(label4);
            panel2.Controls.Add(label3);
            panel2.Controls.Add(label2);
            panel2.Controls.Add(label1);
            panel2.Location = new Point(18, 70);
            panel2.Name = "panel2";
            panel2.Size = new Size(445, 355);
            panel2.TabIndex = 1;
            // 
            // txtMotivo
            // 
            txtMotivo.Location = new Point(175, 275);
            txtMotivo.Name = "txtMotivo";
            txtMotivo.Size = new Size(151, 27);
            txtMotivo.TabIndex = 11;
            // 
            // cmbEstado
            // 
            cmbEstado.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbEstado.FormattingEnabled = true;
            cmbEstado.Location = new Point(175, 221);
            cmbEstado.Name = "cmbEstado";
            cmbEstado.Size = new Size(151, 28);
            cmbEstado.TabIndex = 10;
            // 
            // cmbVeterinario
            // 
            cmbVeterinario.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbVeterinario.FormattingEnabled = true;
            cmbVeterinario.Location = new Point(175, 163);
            cmbVeterinario.Name = "cmbVeterinario";
            cmbVeterinario.Size = new Size(151, 28);
            cmbVeterinario.TabIndex = 9;
            // 
            // cobMascota
            // 
            cobMascota.DropDownStyle = ComboBoxStyle.DropDownList;
            cobMascota.FormattingEnabled = true;
            cobMascota.Location = new Point(175, 116);
            cobMascota.Name = "cobMascota";
            cobMascota.Size = new Size(151, 28);
            cobMascota.TabIndex = 8;
            // 
            // dtpHora
            // 
            dtpHora.Format = DateTimePickerFormat.Time;
            dtpHora.Location = new Point(175, 68);
            dtpHora.Name = "dtpHora";
            dtpHora.Size = new Size(250, 27);
            dtpHora.TabIndex = 7;
            // 
            // dtpFecha
            // 
            dtpFecha.Format = DateTimePickerFormat.Short;
            dtpFecha.Location = new Point(175, 20);
            dtpFecha.Name = "dtpFecha";
            dtpFecha.Size = new Size(240, 27);
            dtpFecha.TabIndex = 6;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(16, 278);
            label6.Name = "label6";
            label6.Size = new Size(67, 20);
            label6.TabIndex = 5;
            label6.Text = "MOTIVO:";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(16, 221);
            label5.Name = "label5";
            label5.Size = new Size(67, 20);
            label5.TabIndex = 4;
            label5.Text = "ESTADO:";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(16, 171);
            label4.Name = "label4";
            label4.Size = new Size(103, 20);
            label4.TabIndex = 3;
            label4.Text = "VETERINARIO:";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(16, 119);
            label3.Name = "label3";
            label3.Size = new Size(79, 20);
            label3.TabIndex = 2;
            label3.Text = "MASCOTA:";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(16, 71);
            label2.Name = "label2";
            label2.Size = new Size(53, 20);
            label2.TabIndex = 1;
            label2.Text = "HORA:";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(16, 23);
            label1.Name = "label1";
            label1.Size = new Size(57, 20);
            label1.TabIndex = 0;
            label1.Text = "FECHA:";
            // 
            // panel3
            // 
            panel3.BackColor = SystemColors.GradientInactiveCaption;
            panel3.Controls.Add(btnModificar);
            panel3.Controls.Add(btnCancelar);
            panel3.Controls.Add(btnNuevo);
            panel3.Controls.Add(btnGuardar);
            panel3.Location = new Point(18, 455);
            panel3.Name = "panel3";
            panel3.Size = new Size(445, 180);
            panel3.TabIndex = 2;
            // 
            // btnModificar
            // 
            btnModificar.Location = new Point(17, 109);
            btnModificar.Name = "btnModificar";
            btnModificar.Size = new Size(125, 43);
            btnModificar.TabIndex = 3;
            btnModificar.Text = "Modificar";
            btnModificar.UseVisualStyleBackColor = true;
            btnModificar.Click += btnModificar_Click;
            // 
            // btnCancelar
            // 
            btnCancelar.Location = new Point(167, 109);
            btnCancelar.Name = "btnCancelar";
            btnCancelar.Size = new Size(125, 43);
            btnCancelar.TabIndex = 2;
            btnCancelar.Text = "Cancelar";
            btnCancelar.UseVisualStyleBackColor = true;
            btnCancelar.Click += btnCancelar_Click;
            // 
            // btnNuevo
            // 
            btnNuevo.Location = new Point(167, 25);
            btnNuevo.Name = "btnNuevo";
            btnNuevo.Size = new Size(125, 43);
            btnNuevo.TabIndex = 1;
            btnNuevo.Text = "Nuevo";
            btnNuevo.UseVisualStyleBackColor = true;
            btnNuevo.Click += btnNuevo_Click;
            // 
            // btnGuardar
            // 
            btnGuardar.Location = new Point(17, 25);
            btnGuardar.Name = "btnGuardar";
            btnGuardar.Size = new Size(125, 43);
            btnGuardar.TabIndex = 0;
            btnGuardar.Text = "Guardar";
            btnGuardar.UseVisualStyleBackColor = true;
            btnGuardar.Click += btnGuardar_Click;
            // 
            // dgvCitas
            // 
            dgvCitas.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvCitas.Location = new Point(484, 70);
            dgvCitas.Name = "dgvCitas";
            dgvCitas.RowHeadersWidth = 51;
            dgvCitas.Size = new Size(710, 565);
            dgvCitas.TabIndex = 3;
            dgvCitas.SelectionChanged += dgvCitas_SelectionChanged;
            // 
            // FrmCitasVeterinaria
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.ControlLightLight;
            ClientSize = new Size(1206, 643);
            Controls.Add(dgvCitas);
            Controls.Add(panel3);
            Controls.Add(panel2);
            Controls.Add(panel1);
            Name = "FrmCitasVeterinaria";
            Text = "Citas Veterinaria";
            Load += FrmCitasVeterinaria_Load;
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            panel2.ResumeLayout(false);
            panel2.PerformLayout();
            panel3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvCitas).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Panel panel1;
        private Label lblTitulo;
        private Panel panel2;
        private Label label6;
        private Label label5;
        private Label label4;
        private Label label3;
        private Label label2;
        private Label label1;
        private TextBox txtMotivo;
        private ComboBox cmbEstado;
        private ComboBox cmbVeterinario;
        private ComboBox cobMascota;
        private DateTimePicker dtpHora;
        private DateTimePicker dtpFecha;
        private Panel panel3;
        private Button btnCancelar;
        private Button btnNuevo;
        private Button btnGuardar;
        private DataGridView dgvCitas;
        private Button btnModificar;
    }
}