namespace Veterinaria1
{
    partial class FrmUsuarios
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
            components = new System.ComponentModel.Container();
            dgvUsuarios = new DataGridView();
            txtUsername = new TextBox();
            txtPassword = new TextBox();
            chkActivo = new CheckBox();
            btnGuardar = new Button();
            btnEliminar = new Button();
            btnCancelar = new Button();
            btnNuevo = new Button();
            btnEditar = new Button();
            panel1 = new Panel();
            panel2 = new Panel();
            cmbRolUsuario = new ComboBox();
            label3 = new Label();
            lblPersonaSeleccionada = new Label();
            txtConfirmPassword = new TextBox();
            txtcontra2 = new Label();
            label2 = new Label();
            label1 = new Label();
            cmbPersona = new ComboBox();
            Usuario = new Label();
            panel3 = new Panel();
            contextMenuStrip1 = new ContextMenuStrip(components);
            ((System.ComponentModel.ISupportInitialize)dgvUsuarios).BeginInit();
            panel2.SuspendLayout();
            panel3.SuspendLayout();
            SuspendLayout();
            // 
            // dgvUsuarios
            // 
            dgvUsuarios.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvUsuarios.Location = new Point(552, 138);
            dgvUsuarios.Name = "dgvUsuarios";
            dgvUsuarios.RowHeadersWidth = 51;
            dgvUsuarios.Size = new Size(663, 471);
            dgvUsuarios.TabIndex = 0;
            dgvUsuarios.CellContentClick += dgvUsuarios_CellContentClick;
            // 
            // txtUsername
            // 
            txtUsername.Location = new Point(231, 132);
            txtUsername.Name = "txtUsername";
            txtUsername.Size = new Size(264, 27);
            txtUsername.TabIndex = 5;
            // 
            // txtPassword
            // 
            txtPassword.Location = new Point(231, 205);
            txtPassword.Name = "txtPassword";
            txtPassword.PasswordChar = '*';
            txtPassword.Size = new Size(264, 27);
            txtPassword.TabIndex = 6;
            // 
            // chkActivo
            // 
            chkActivo.AutoSize = true;
            chkActivo.Location = new Point(231, 359);
            chkActivo.Name = "chkActivo";
            chkActivo.Size = new Size(73, 24);
            chkActivo.TabIndex = 12;
            chkActivo.Text = "Activo";
            chkActivo.UseVisualStyleBackColor = true;
            chkActivo.CheckedChanged += chkActivo_CheckedChanged;
            // 
            // btnGuardar
            // 
            btnGuardar.Location = new Point(18, 21);
            btnGuardar.Name = "btnGuardar";
            btnGuardar.Size = new Size(163, 41);
            btnGuardar.TabIndex = 13;
            btnGuardar.Text = "GUARDAR";
            btnGuardar.UseVisualStyleBackColor = true;
            btnGuardar.Click += btnGuardar_Click;
            // 
            // btnEliminar
            // 
            btnEliminar.Location = new Point(18, 97);
            btnEliminar.Name = "btnEliminar";
            btnEliminar.Size = new Size(163, 41);
            btnEliminar.TabIndex = 14;
            btnEliminar.Text = "ELIMINAR";
            btnEliminar.UseVisualStyleBackColor = true;
            btnEliminar.Click += btnEliminar_Click;
            // 
            // btnCancelar
            // 
            btnCancelar.Location = new Point(203, 97);
            btnCancelar.Name = "btnCancelar";
            btnCancelar.Size = new Size(163, 41);
            btnCancelar.TabIndex = 15;
            btnCancelar.Text = "Cancelar";
            btnCancelar.UseVisualStyleBackColor = true;
            btnCancelar.Click += btnCancelar_Click;
            // 
            // btnNuevo
            // 
            btnNuevo.Location = new Point(18, 163);
            btnNuevo.Name = "btnNuevo";
            btnNuevo.Size = new Size(163, 41);
            btnNuevo.TabIndex = 16;
            btnNuevo.Text = "Nuevo";
            btnNuevo.UseVisualStyleBackColor = true;
            btnNuevo.Click += btnNuevo_Click;
            // 
            // btnEditar
            // 
            btnEditar.Location = new Point(203, 21);
            btnEditar.Name = "btnEditar";
            btnEditar.Size = new Size(163, 41);
            btnEditar.TabIndex = 17;
            btnEditar.Text = "editar";
            btnEditar.UseVisualStyleBackColor = true;
            // 
            // panel1
            // 
            panel1.BackColor = SystemColors.GradientActiveCaption;
            panel1.ImeMode = ImeMode.On;
            panel1.Location = new Point(-3, 1);
            panel1.Name = "panel1";
            panel1.Size = new Size(1231, 35);
            panel1.TabIndex = 18;
            // 
            // panel2
            // 
            panel2.BackColor = SystemColors.Window;
            panel2.Controls.Add(cmbRolUsuario);
            panel2.Controls.Add(label3);
            panel2.Controls.Add(lblPersonaSeleccionada);
            panel2.Controls.Add(txtConfirmPassword);
            panel2.Controls.Add(txtcontra2);
            panel2.Controls.Add(chkActivo);
            panel2.Controls.Add(label2);
            panel2.Controls.Add(label1);
            panel2.Controls.Add(cmbPersona);
            panel2.Controls.Add(Usuario);
            panel2.Controls.Add(txtUsername);
            panel2.Controls.Add(txtPassword);
            panel2.Location = new Point(21, 64);
            panel2.Name = "panel2";
            panel2.Size = new Size(505, 386);
            panel2.TabIndex = 19;
            // 
            // cmbRolUsuario
            // 
            cmbRolUsuario.FormattingEnabled = true;
            cmbRolUsuario.Location = new Point(231, 319);
            cmbRolUsuario.Name = "cmbRolUsuario";
            cmbRolUsuario.Size = new Size(259, 28);
            cmbRolUsuario.TabIndex = 11;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label3.Location = new Point(15, 319);
            label3.Name = "label3";
            label3.Size = new Size(131, 23);
            label3.TabIndex = 10;
            label3.Text = "Seleccionar Rol";
            // 
            // lblPersonaSeleccionada
            // 
            lblPersonaSeleccionada.AutoSize = true;
            lblPersonaSeleccionada.Location = new Point(239, 78);
            lblPersonaSeleccionada.Name = "lblPersonaSeleccionada";
            lblPersonaSeleccionada.Size = new Size(0, 20);
            lblPersonaSeleccionada.TabIndex = 9;
            // 
            // txtConfirmPassword
            // 
            txtConfirmPassword.Location = new Point(231, 269);
            txtConfirmPassword.Name = "txtConfirmPassword";
            txtConfirmPassword.PasswordChar = '*';
            txtConfirmPassword.Size = new Size(264, 27);
            txtConfirmPassword.TabIndex = 8;
            // 
            // txtcontra2
            // 
            txtcontra2.AutoSize = true;
            txtcontra2.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            txtcontra2.Location = new Point(14, 269);
            txtcontra2.Name = "txtcontra2";
            txtcontra2.Size = new Size(186, 23);
            txtcontra2.TabIndex = 7;
            txtcontra2.Text = "Confirmar Contraseña";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label2.Location = new Point(21, 194);
            label2.Name = "label2";
            label2.Size = new Size(92, 23);
            label2.TabIndex = 6;
            label2.Text = "Contaseña";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.Location = new Point(15, 136);
            label1.Name = "label1";
            label1.Size = new Size(70, 23);
            label1.TabIndex = 2;
            label1.Text = "Usuario";
            // 
            // cmbPersona
            // 
            cmbPersona.FormattingEnabled = true;
            cmbPersona.Location = new Point(231, 26);
            cmbPersona.Name = "cmbPersona";
            cmbPersona.Size = new Size(264, 28);
            cmbPersona.TabIndex = 1;
            cmbPersona.SelectedIndexChanged += comboBox1_SelectedIndexChanged;
            // 
            // Usuario
            // 
            Usuario.AutoSize = true;
            Usuario.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            Usuario.Location = new Point(14, 27);
            Usuario.Name = "Usuario";
            Usuario.Size = new Size(71, 23);
            Usuario.TabIndex = 0;
            Usuario.Text = "Persona";
            // 
            // panel3
            // 
            panel3.BackColor = SystemColors.GradientInactiveCaption;
            panel3.Controls.Add(btnCancelar);
            panel3.Controls.Add(btnGuardar);
            panel3.Controls.Add(btnNuevo);
            panel3.Controls.Add(btnEliminar);
            panel3.Controls.Add(btnEditar);
            panel3.Location = new Point(21, 456);
            panel3.Name = "panel3";
            panel3.Size = new Size(505, 224);
            panel3.TabIndex = 0;
            // 
            // contextMenuStrip1
            // 
            contextMenuStrip1.ImageScalingSize = new Size(20, 20);
            contextMenuStrip1.Name = "contextMenuStrip1";
            contextMenuStrip1.Size = new Size(61, 4);
            // 
            // FrmUsuarios
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.Window;
            ClientSize = new Size(1227, 690);
            Controls.Add(panel3);
            Controls.Add(panel2);
            Controls.Add(panel1);
            Controls.Add(dgvUsuarios);
            Name = "FrmUsuarios";
            Text = "FrmUsuarios";
            Load += FrmUsuarios_Load;
            ((System.ComponentModel.ISupportInitialize)dgvUsuarios).EndInit();
            panel2.ResumeLayout(false);
            panel2.PerformLayout();
            panel3.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private DataGridView dgvUsuarios;
        private TextBox txtUsername;
        private TextBox txtPassword;
        private CheckBox chkActivo;
        private Button btnGuardar;
        private Button btnEliminar;
        private Button btnCancelar;
        private Button btnNuevo;
        private Button btnEditar;
        private Panel panel1;
        private Panel panel2;
        private Panel panel3;
        private Label txtcontra2;
        private Label label2;
        private Label label1;
        private ComboBox cmbPersona;
        private Label Usuario;
        private ContextMenuStrip contextMenuStrip1;
        private TextBox txtConfirmPassword;
        private Label lblPersonaSeleccionada;
        private ComboBox cmbRolUsuario;
        private Label label3;
    }
}