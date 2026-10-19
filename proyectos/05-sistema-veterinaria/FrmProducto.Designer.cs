namespace Veterinaria1
{
    partial class FrmProducto
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
            cmbProveedor = new ComboBox();
            cmbCategoria = new ComboBox();
            txtPrecio = new TextBox();
            txtDescripcion = new TextBox();
            txtNombre = new TextBox();
            checkBox1 = new CheckBox();
            label5 = new Label();
            label4 = new Label();
            label3 = new Label();
            label2 = new Label();
            label1 = new Label();
            panel3 = new Panel();
            btnDesactivar = new Button();
            button3 = new Button();
            btnNuevo = new Button();
            btnGuardar = new Button();
            dgvProductos = new DataGridView();
            panel2.SuspendLayout();
            panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvProductos).BeginInit();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.BackColor = SystemColors.GradientInactiveCaption;
            panel1.Location = new Point(-1, -2);
            panel1.Name = "panel1";
            panel1.Size = new Size(1226, 45);
            panel1.TabIndex = 0;
            // 
            // panel2
            // 
            panel2.Controls.Add(cmbProveedor);
            panel2.Controls.Add(cmbCategoria);
            panel2.Controls.Add(txtPrecio);
            panel2.Controls.Add(txtDescripcion);
            panel2.Controls.Add(txtNombre);
            panel2.Controls.Add(checkBox1);
            panel2.Controls.Add(label5);
            panel2.Controls.Add(label4);
            panel2.Controls.Add(label3);
            panel2.Controls.Add(label2);
            panel2.Controls.Add(label1);
            panel2.Location = new Point(17, 76);
            panel2.Name = "panel2";
            panel2.Size = new Size(483, 291);
            panel2.TabIndex = 1;
            // 
            // cmbProveedor
            // 
            cmbProveedor.FormattingEnabled = true;
            cmbProveedor.Location = new Point(218, 213);
            cmbProveedor.Name = "cmbProveedor";
            cmbProveedor.Size = new Size(239, 28);
            cmbProveedor.TabIndex = 10;
            cmbProveedor.SelectedIndexChanged += cmbProveedor_SelectedIndexChanged;
            // 
            // cmbCategoria
            // 
            cmbCategoria.FormattingEnabled = true;
            cmbCategoria.Location = new Point(218, 166);
            cmbCategoria.Name = "cmbCategoria";
            cmbCategoria.Size = new Size(239, 28);
            cmbCategoria.TabIndex = 9;
            cmbCategoria.SelectedIndexChanged += cmbCategoria_SelectedIndexChanged;
            // 
            // txtPrecio
            // 
            txtPrecio.Location = new Point(215, 115);
            txtPrecio.Name = "txtPrecio";
            txtPrecio.Size = new Size(242, 27);
            txtPrecio.TabIndex = 8;
            txtPrecio.TextChanged += txtPrecio_TextChanged;
            // 
            // txtDescripcion
            // 
            txtDescripcion.Location = new Point(215, 67);
            txtDescripcion.Name = "txtDescripcion";
            txtDescripcion.Size = new Size(242, 27);
            txtDescripcion.TabIndex = 7;
            txtDescripcion.TextChanged += txtDescripcion_TextChanged;
            // 
            // txtNombre
            // 
            txtNombre.Location = new Point(215, 21);
            txtNombre.Name = "txtNombre";
            txtNombre.Size = new Size(242, 27);
            txtNombre.TabIndex = 6;
            txtNombre.TextChanged += txtNombre_TextChanged;
            // 
            // checkBox1
            // 
            checkBox1.AutoSize = true;
            checkBox1.Location = new Point(246, 247);
            checkBox1.Name = "checkBox1";
            checkBox1.Size = new Size(73, 24);
            checkBox1.TabIndex = 5;
            checkBox1.Text = "Activo";
            checkBox1.UseVisualStyleBackColor = true;
            checkBox1.CheckedChanged += checkBox1_CheckedChanged;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label5.Location = new Point(16, 213);
            label5.Name = "label5";
            label5.Size = new Size(92, 23);
            label5.TabIndex = 4;
            label5.Text = "Proveedor";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label4.Location = new Point(16, 160);
            label4.Name = "label4";
            label4.Size = new Size(88, 23);
            label4.TabIndex = 3;
            label4.Text = "Categoria";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label3.Location = new Point(16, 115);
            label3.Name = "label3";
            label3.Size = new Size(59, 23);
            label3.TabIndex = 2;
            label3.Text = "Precio";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label2.Location = new Point(16, 67);
            label2.Name = "label2";
            label2.Size = new Size(103, 23);
            label2.TabIndex = 1;
            label2.Text = "Descripcion";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.Location = new Point(16, 21);
            label1.Name = "label1";
            label1.Size = new Size(76, 23);
            label1.TabIndex = 0;
            label1.Text = "Nombre";
            // 
            // panel3
            // 
            panel3.BackColor = SystemColors.GradientInactiveCaption;
            panel3.Controls.Add(btnDesactivar);
            panel3.Controls.Add(button3);
            panel3.Controls.Add(btnNuevo);
            panel3.Controls.Add(btnGuardar);
            panel3.Location = new Point(17, 400);
            panel3.Name = "panel3";
            panel3.Size = new Size(483, 235);
            panel3.TabIndex = 2;
            // 
            // btnDesactivar
            // 
            btnDesactivar.Location = new Point(30, 100);
            btnDesactivar.Name = "btnDesactivar";
            btnDesactivar.Size = new Size(105, 43);
            btnDesactivar.TabIndex = 3;
            btnDesactivar.Text = "Desactivar";
            btnDesactivar.UseVisualStyleBackColor = true;
            btnDesactivar.Click += btnDesactivar_Click;
            // 
            // button3
            // 
            button3.Location = new Point(186, 91);
            button3.Name = "button3";
            button3.Size = new Size(105, 43);
            button3.TabIndex = 2;
            button3.Text = "btnCancelar";
            button3.UseVisualStyleBackColor = true;
            button3.Click += button3_Click;
            // 
            // btnNuevo
            // 
            btnNuevo.Location = new Point(186, 29);
            btnNuevo.Name = "btnNuevo";
            btnNuevo.Size = new Size(105, 43);
            btnNuevo.TabIndex = 1;
            btnNuevo.Text = "Nuevo";
            btnNuevo.UseVisualStyleBackColor = true;
            btnNuevo.Click += btnNuevo_Click;
            // 
            // btnGuardar
            // 
            btnGuardar.Location = new Point(30, 29);
            btnGuardar.Name = "btnGuardar";
            btnGuardar.Size = new Size(105, 43);
            btnGuardar.TabIndex = 0;
            btnGuardar.Text = "Guardar";
            btnGuardar.UseVisualStyleBackColor = true;
            btnGuardar.Click += btnGuardar_Click;
            // 
            // dgvProductos
            // 
            dgvProductos.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvProductos.Location = new Point(558, 79);
            dgvProductos.Name = "dgvProductos";
            dgvProductos.RowHeadersWidth = 51;
            dgvProductos.Size = new Size(641, 560);
            dgvProductos.TabIndex = 3;
            dgvProductos.CellContentClick += dgvProductos_CellContentClick;
            // 
            // FrmProducto
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.ControlLightLight;
            ClientSize = new Size(1224, 690);
            Controls.Add(dgvProductos);
            Controls.Add(panel3);
            Controls.Add(panel2);
            Controls.Add(panel1);
            Name = "FrmProducto";
            Text = "FrmProducto";
            Load += FrmProducto_Load;
            panel2.ResumeLayout(false);
            panel2.PerformLayout();
            panel3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvProductos).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Panel panel1;
        private Panel panel2;
        private Label label3;
        private Label label2;
        private Label label1;
        private Panel panel3;
        private ComboBox cmbProveedor;
        private ComboBox cmbCategoria;
        private TextBox txtPrecio;
        private TextBox txtDescripcion;
        private TextBox txtNombre;
        private CheckBox checkBox1;
        private Label label5;
        private Label label4;
        private Button btnDesactivar;
        private Button button3;
        private Button btnNuevo;
        private Button btnGuardar;
        private DataGridView dgvProductos;
    }
}