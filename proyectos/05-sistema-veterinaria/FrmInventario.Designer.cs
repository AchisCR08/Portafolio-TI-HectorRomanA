namespace Veterinaria1
{
    partial class FrmInventario
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
            panel1 = new Panel();
            panel2 = new Panel();
            txtStock = new TextBox();
            label3 = new Label();
            cmbProducto = new ComboBox();
            label2 = new Label();
            label1 = new Label();
            lblSucursalNombre = new Label();
            panel3 = new Panel();
            btnLimpiar = new Button();
            btnEliminar = new Button();
            btnGuardar = new Button();
            dgvInventario = new DataGridView();
            contextMenuStrip1 = new ContextMenuStrip(components);
            panel2.SuspendLayout();
            panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvInventario).BeginInit();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.BackColor = SystemColors.GradientInactiveCaption;
            panel1.Location = new Point(-1, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(1227, 42);
            panel1.TabIndex = 0;
            // 
            // panel2
            // 
            panel2.Controls.Add(txtStock);
            panel2.Controls.Add(label3);
            panel2.Controls.Add(cmbProducto);
            panel2.Controls.Add(label2);
            panel2.Controls.Add(label1);
            panel2.Controls.Add(lblSucursalNombre);
            panel2.Location = new Point(12, 61);
            panel2.Name = "panel2";
            panel2.Size = new Size(421, 224);
            panel2.TabIndex = 1;
            panel2.Paint += panel2_Paint;
            // 
            // txtStock
            // 
            txtStock.Location = new Point(185, 145);
            txtStock.Name = "txtStock";
            txtStock.Size = new Size(210, 27);
            txtStock.TabIndex = 5;
            txtStock.TextChanged += txtStock_TextChanged;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label3.Location = new Point(11, 146);
            label3.Name = "label3";
            label3.Size = new Size(55, 23);
            label3.TabIndex = 4;
            label3.Text = "Stock";
            // 
            // cmbProducto
            // 
            cmbProducto.FormattingEnabled = true;
            cmbProducto.Location = new Point(185, 88);
            cmbProducto.Name = "cmbProducto";
            cmbProducto.Size = new Size(208, 28);
            cmbProducto.TabIndex = 3;
            cmbProducto.SelectedIndexChanged += cmbProducto_SelectedIndexChanged;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label2.Location = new Point(11, 90);
            label2.Name = "label2";
            label2.Size = new Size(83, 23);
            label2.TabIndex = 2;
            label2.Text = "Producto";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.Location = new Point(11, 32);
            label1.Name = "label1";
            label1.Size = new Size(76, 23);
            label1.TabIndex = 1;
            label1.Text = "Sucursal";
            // 
            // lblSucursalNombre
            // 
            lblSucursalNombre.AutoSize = true;
            lblSucursalNombre.Location = new Point(195, 25);
            lblSucursalNombre.Name = "lblSucursalNombre";
            lblSucursalNombre.Size = new Size(0, 20);
            lblSucursalNombre.TabIndex = 0;
            // 
            // panel3
            // 
            panel3.BackColor = SystemColors.GradientInactiveCaption;
            panel3.Controls.Add(btnLimpiar);
            panel3.Controls.Add(btnEliminar);
            panel3.Controls.Add(btnGuardar);
            panel3.Location = new Point(19, 350);
            panel3.Name = "panel3";
            panel3.Size = new Size(415, 219);
            panel3.TabIndex = 2;
            // 
            // btnLimpiar
            // 
            btnLimpiar.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnLimpiar.Location = new Point(23, 113);
            btnLimpiar.Name = "btnLimpiar";
            btnLimpiar.Size = new Size(129, 43);
            btnLimpiar.TabIndex = 2;
            btnLimpiar.Text = "LIMPIAR";
            btnLimpiar.UseVisualStyleBackColor = true;
            btnLimpiar.Click += btnLimpiar_Click;
            // 
            // btnEliminar
            // 
            btnEliminar.Font = new Font("Segoe UI Black", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnEliminar.Location = new Point(209, 33);
            btnEliminar.Name = "btnEliminar";
            btnEliminar.Size = new Size(129, 43);
            btnEliminar.TabIndex = 1;
            btnEliminar.Text = "ELIMINAR";
            btnEliminar.UseVisualStyleBackColor = true;
            btnEliminar.Click += btnEliminar_Click;
            // 
            // btnGuardar
            // 
            btnGuardar.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnGuardar.Location = new Point(23, 33);
            btnGuardar.Name = "btnGuardar";
            btnGuardar.Size = new Size(129, 43);
            btnGuardar.TabIndex = 0;
            btnGuardar.Text = "GUARDAR";
            btnGuardar.UseVisualStyleBackColor = true;
            btnGuardar.Click += btnGuardar_Click;
            // 
            // dgvInventario
            // 
            dgvInventario.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvInventario.Location = new Point(490, 64);
            dgvInventario.Name = "dgvInventario";
            dgvInventario.RowHeadersWidth = 51;
            dgvInventario.Size = new Size(718, 528);
            dgvInventario.TabIndex = 3;
            dgvInventario.CellContentClick += dgvInventario_CellContentClick;
            // 
            // contextMenuStrip1
            // 
            contextMenuStrip1.ImageScalingSize = new Size(20, 20);
            contextMenuStrip1.Name = "contextMenuStrip1";
            contextMenuStrip1.Size = new Size(61, 4);
            // 
            // FrmInventario
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1224, 690);
            Controls.Add(dgvInventario);
            Controls.Add(panel3);
            Controls.Add(panel2);
            Controls.Add(panel1);
            Name = "FrmInventario";
            Text = "FrmInventario";
            panel2.ResumeLayout(false);
            panel2.PerformLayout();
            panel3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvInventario).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Panel panel1;
        private Panel panel2;
        private Label lblSucursalNombre;
        private Label label2;
        private Label label1;
        private TextBox txtStock;
        private Label label3;
        private ComboBox cmbProducto;
        private Panel panel3;
        private Button btnLimpiar;
        private Button btnEliminar;
        private Button btnGuardar;
        private DataGridView dgvInventario;
        private ContextMenuStrip contextMenuStrip1;
    }
}