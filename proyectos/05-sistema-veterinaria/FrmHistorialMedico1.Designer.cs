namespace Veterinaria1
{
    partial class FrmHistorialMedico1
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
            panel2 = new Panel();
            panel3 = new Panel();
            btnNuevo = new Button();
            btnGuardar = new Button();
            dgvHistorial = new DataGridView();
            lbltratamiento = new Label();
            lblTratamiento1 = new Label();
            lblOpservaciones = new Label();
            txtDiagnostico = new TextBox();
            textBox2 = new TextBox();
            textBox3 = new TextBox();
            lblInstruccion = new Label();
            panel2.SuspendLayout();
            panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvHistorial).BeginInit();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.BackColor = SystemColors.GradientInactiveCaption;
            panel1.Dock = DockStyle.Top;
            panel1.Location = new Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(1206, 45);
            panel1.TabIndex = 0;
            // 
            // panel2
            // 
            panel2.BorderStyle = BorderStyle.FixedSingle;
            panel2.Controls.Add(textBox3);
            panel2.Controls.Add(textBox2);
            panel2.Controls.Add(txtDiagnostico);
            panel2.Controls.Add(lblOpservaciones);
            panel2.Controls.Add(lblTratamiento1);
            panel2.Controls.Add(lbltratamiento);
            panel2.Location = new Point(18, 71);
            panel2.Name = "panel2";
            panel2.Size = new Size(445, 395);
            panel2.TabIndex = 1;
            // 
            // panel3
            // 
            panel3.BackColor = SystemColors.GradientInactiveCaption;
            panel3.Controls.Add(btnNuevo);
            panel3.Controls.Add(btnGuardar);
            panel3.Location = new Point(18, 471);
            panel3.Name = "panel3";
            panel3.Size = new Size(445, 180);
            panel3.TabIndex = 2;
            // 
            // btnNuevo
            // 
            btnNuevo.Location = new Point(210, 25);
            btnNuevo.Name = "btnNuevo";
            btnNuevo.Size = new Size(125, 43);
            btnNuevo.TabIndex = 1;
            btnNuevo.Text = "Nuevo";
            btnNuevo.UseVisualStyleBackColor = true;
            btnNuevo.Click += btnNuevo_Click;
            // 
            // btnGuardar
            // 
            btnGuardar.Location = new Point(25, 25);
            btnGuardar.Name = "btnGuardar";
            btnGuardar.Size = new Size(165, 43);
            btnGuardar.TabIndex = 0;
            btnGuardar.Text = "Guardar";
            btnGuardar.UseVisualStyleBackColor = true;
            btnGuardar.Click += btnGuardar_Click;
            // 
            // dgvHistorial
            // 
            dgvHistorial.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvHistorial.BackgroundColor = Color.White;
            dgvHistorial.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvHistorial.Location = new Point(495, 70);
            dgvHistorial.Name = "dgvHistorial";
            dgvHistorial.ReadOnly = true;
            dgvHistorial.RowHeadersWidth = 51;
            dgvHistorial.Size = new Size(700, 565);
            dgvHistorial.TabIndex = 3;
            dgvHistorial.CellContentClick += dgvHistorial_CellContentClick;
            dgvHistorial.SelectionChanged += dgvHistorial_SelectionChanged;
            // 
            // lbltratamiento
            // 
            lbltratamiento.AutoSize = true;
            lbltratamiento.Location = new Point(16, 18);
            lbltratamiento.Name = "lbltratamiento";
            lbltratamiento.Size = new Size(92, 20);
            lbltratamiento.TabIndex = 0;
            lbltratamiento.Text = "Diagnostico:";
            // 
            // lblTratamiento1
            // 
            lblTratamiento1.AutoSize = true;
            lblTratamiento1.Location = new Point(16, 142);
            lblTratamiento1.Name = "lblTratamiento1";
            lblTratamiento1.Size = new Size(92, 20);
            lblTratamiento1.TabIndex = 1;
            lblTratamiento1.Text = "Tratamiento:";
            // 
            // lblOpservaciones
            // 
            lblOpservaciones.AutoSize = true;
            lblOpservaciones.Location = new Point(16, 266);
            lblOpservaciones.Name = "lblOpservaciones";
            lblOpservaciones.Size = new Size(108, 20);
            lblOpservaciones.TabIndex = 2;
            lblOpservaciones.Text = "Observaciones:";
            // 
            // txtDiagnostico
            // 
            txtDiagnostico.Location = new Point(16, 45);
            txtDiagnostico.Name = "txtDiagnostico";
            txtDiagnostico.Size = new Size(125, 27);
            txtDiagnostico.TabIndex = 3;
            // 
            // textBox2
            // 
            textBox2.Location = new Point(16, 169);
            textBox2.Name = "textBox2";
            textBox2.Size = new Size(125, 27);
            textBox2.TabIndex = 4;
            // 
            // textBox3
            // 
            textBox3.Location = new Point(16, 289);
            textBox3.Name = "textBox3";
            textBox3.Size = new Size(125, 27);
            textBox3.TabIndex = 5;
            // 
            // lblInstruccion
            // 
            lblInstruccion.AutoSize = true;
            lblInstruccion.Location = new Point(12, 48);
            lblInstruccion.Name = "lblInstruccion";
            lblInstruccion.Size = new Size(387, 20);
            lblInstruccion.TabIndex = 4;
            lblInstruccion.Text = "Seleccione una cita para registrar o actualizar su consulta";
            // 
            // FrmHistorialMedico1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.ControlLightLight;
            ClientSize = new Size(1206, 643);
            Controls.Add(lblInstruccion);
            Controls.Add(dgvHistorial);
            Controls.Add(panel3);
            Controls.Add(panel2);
            Controls.Add(panel1);
            Name = "FrmHistorialMedico1";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "FrmHistorialMedico1";
            Load += FrmHistorialMedico1_Load;
            panel2.ResumeLayout(false);
            panel2.PerformLayout();
            panel3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvHistorial).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Panel panel1;
        private Panel panel2;
        private Panel panel3;
        private DataGridView dgvHistorial;
        private Button btnNuevo;
        private Button btnGuardar;
        private Label lblOpservaciones;
        private Label lblTratamiento1;
        private Label lbltratamiento;
        private TextBox textBox3;
        private TextBox textBox2;
        private TextBox txtDiagnostico;
        private Label lblInstruccion;
    }
}