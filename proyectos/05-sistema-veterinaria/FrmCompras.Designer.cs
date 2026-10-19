namespace Veterinaria1
{
    partial class FrmCompras
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
            DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle3 = new DataGridViewCellStyle();
            panel1 = new Panel();
            label1 = new Label();
            panel2 = new Panel();
            lblSucursal = new Label();
            label2 = new Label();
            dateTimePicker1 = new DateTimePicker();
            Proveedor = new Label();
            cmbProveedor = new ComboBox();
            panel3 = new Panel();
            label3 = new Label();
            cmbProducto = new ComboBox();
            label4 = new Label();
            numericUpDown1 = new NumericUpDown();
            label5 = new Label();
            txtPrecioUnitario = new TextBox();
            btnAgregarLinea = new Button();
            btnQuitarLinea = new Button();
            dgvLineas = new DataGridView();
            panel4 = new Panel();
            label6 = new Label();
            lblTotal = new Label();
            btnRegistrar = new Button();
            btnLimpiar = new Button();
            panel2.SuspendLayout();
            panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numericUpDown1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dgvLineas).BeginInit();
            panel4.SuspendLayout();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.BackColor = SystemColors.GradientInactiveCaption;
            panel1.Location = new Point(2, -1);
            panel1.Name = "panel1";
            panel1.Size = new Size(1321, 49);
            panel1.TabIndex = 1;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.Location = new Point(23, 14);
            label1.Name = "label1";
            label1.Size = new Size(71, 20);
            label1.TabIndex = 2;
            label1.Text = "Sucursal:";
            // 
            // panel2
            // 
            panel2.BackColor = SystemColors.ControlLightLight;
            panel2.Controls.Add(cmbProveedor);
            panel2.Controls.Add(Proveedor);
            panel2.Controls.Add(dateTimePicker1);
            panel2.Controls.Add(label2);
            panel2.Controls.Add(lblSucursal);
            panel2.Controls.Add(label1);
            panel2.Location = new Point(12, 70);
            panel2.Name = "panel2";
            panel2.Size = new Size(672, 136);
            panel2.TabIndex = 3;
            // 
            // lblSucursal
            // 
            lblSucursal.AutoSize = true;
            lblSucursal.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblSucursal.Location = new Point(117, 14);
            lblSucursal.Name = "lblSucursal";
            lblSucursal.Size = new Size(0, 20);
            lblSucursal.TabIndex = 3;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label2.Location = new Point(328, 19);
            label2.Name = "label2";
            label2.Size = new Size(53, 20);
            label2.TabIndex = 4;
            label2.Text = "Fecha:";
            // 
            // dateTimePicker1
            // 
            dateTimePicker1.Location = new Point(397, 14);
            dateTimePicker1.Name = "dateTimePicker1";
            dateTimePicker1.Size = new Size(272, 27);
            dateTimePicker1.TabIndex = 6;
            // 
            // Proveedor
            // 
            Proveedor.AutoSize = true;
            Proveedor.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            Proveedor.Location = new Point(23, 74);
            Proveedor.Name = "Proveedor";
            Proveedor.Size = new Size(86, 20);
            Proveedor.TabIndex = 7;
            Proveedor.Text = "Proveedor:";
            // 
            // cmbProveedor
            // 
            cmbProveedor.FormattingEnabled = true;
            cmbProveedor.Location = new Point(117, 71);
            cmbProveedor.Name = "cmbProveedor";
            cmbProveedor.Size = new Size(248, 28);
            cmbProveedor.TabIndex = 8;
            // 
            // panel3
            // 
            panel3.BackColor = SystemColors.ControlLightLight;
            panel3.Controls.Add(btnQuitarLinea);
            panel3.Controls.Add(btnAgregarLinea);
            panel3.Controls.Add(txtPrecioUnitario);
            panel3.Controls.Add(label5);
            panel3.Controls.Add(numericUpDown1);
            panel3.Controls.Add(label4);
            panel3.Controls.Add(cmbProducto);
            panel3.Controls.Add(label3);
            panel3.Location = new Point(12, 253);
            panel3.Name = "panel3";
            panel3.Size = new Size(672, 136);
            panel3.TabIndex = 9;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label3.Location = new Point(23, 19);
            label3.Name = "label3";
            label3.Size = new Size(73, 20);
            label3.TabIndex = 9;
            label3.Text = "Producto";
            // 
            // cmbProducto
            // 
            cmbProducto.FormattingEnabled = true;
            cmbProducto.Location = new Point(102, 11);
            cmbProducto.Name = "cmbProducto";
            cmbProducto.Size = new Size(248, 28);
            cmbProducto.TabIndex = 10;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label4.Location = new Point(397, 19);
            label4.Name = "label4";
            label4.Size = new Size(71, 20);
            label4.TabIndex = 11;
            label4.Text = "Cantidad";
            // 
            // numericUpDown1
            // 
            numericUpDown1.Location = new Point(486, 17);
            numericUpDown1.Name = "numericUpDown1";
            numericUpDown1.Size = new Size(165, 27);
            numericUpDown1.TabIndex = 12;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label5.Location = new Point(23, 65);
            label5.Name = "label5";
            label5.Size = new Size(113, 20);
            label5.TabIndex = 13;
            label5.Text = "Precio Unitario";
            // 
            // txtPrecioUnitario
            // 
            txtPrecioUnitario.Location = new Point(144, 58);
            txtPrecioUnitario.Name = "txtPrecioUnitario";
            txtPrecioUnitario.Size = new Size(179, 27);
            txtPrecioUnitario.TabIndex = 14;
            // 
            // btnAgregarLinea
            // 
            btnAgregarLinea.Font = new Font("Segoe UI", 13.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnAgregarLinea.Location = new Point(377, 63);
            btnAgregarLinea.Name = "btnAgregarLinea";
            btnAgregarLinea.Size = new Size(87, 38);
            btnAgregarLinea.TabIndex = 15;
            btnAgregarLinea.Text = "+";
            btnAgregarLinea.UseVisualStyleBackColor = true;
            // 
            // btnQuitarLinea
            // 
            btnQuitarLinea.Font = new Font("Segoe UI", 13.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnQuitarLinea.Location = new Point(486, 63);
            btnQuitarLinea.Name = "btnQuitarLinea";
            btnQuitarLinea.Size = new Size(87, 38);
            btnQuitarLinea.TabIndex = 16;
            btnQuitarLinea.Text = "-";
            btnQuitarLinea.UseVisualStyleBackColor = true;
            // 
            // dgvLineas
            // 
            dataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = SystemColors.Control;
            dataGridViewCellStyle1.Font = new Font("Segoe UI", 9F);
            dataGridViewCellStyle1.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = DataGridViewTriState.True;
            dgvLineas.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            dgvLineas.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = SystemColors.Window;
            dataGridViewCellStyle2.Font = new Font("Segoe UI", 9F);
            dataGridViewCellStyle2.ForeColor = SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = DataGridViewTriState.False;
            dgvLineas.DefaultCellStyle = dataGridViewCellStyle2;
            dgvLineas.Location = new Point(720, 70);
            dgvLineas.Name = "dgvLineas";
            dataGridViewCellStyle3.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = SystemColors.Control;
            dataGridViewCellStyle3.Font = new Font("Segoe UI", 9F);
            dataGridViewCellStyle3.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = DataGridViewTriState.True;
            dgvLineas.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            dgvLineas.RowHeadersWidth = 51;
            dgvLineas.Size = new Size(577, 326);
            dgvLineas.TabIndex = 10;
            // 
            // panel4
            // 
            panel4.BackColor = SystemColors.ControlLightLight;
            panel4.Controls.Add(btnLimpiar);
            panel4.Controls.Add(btnRegistrar);
            panel4.Controls.Add(lblTotal);
            panel4.Controls.Add(label6);
            panel4.Location = new Point(720, 426);
            panel4.Name = "panel4";
            panel4.Size = new Size(577, 97);
            panel4.TabIndex = 11;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label6.Location = new Point(17, 10);
            label6.Name = "label6";
            label6.Size = new Size(48, 20);
            label6.TabIndex = 17;
            label6.Text = "Total:";
            // 
            // lblTotal
            // 
            lblTotal.AutoSize = true;
            lblTotal.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblTotal.Location = new Point(97, 10);
            lblTotal.Name = "lblTotal";
            lblTotal.Size = new Size(44, 20);
            lblTotal.TabIndex = 18;
            lblTotal.Text = "Total";
            // 
            // btnRegistrar
            // 
            btnRegistrar.Location = new Point(194, 54);
            btnRegistrar.Name = "btnRegistrar";
            btnRegistrar.Size = new Size(106, 30);
            btnRegistrar.TabIndex = 19;
            btnRegistrar.Text = "Registrar";
            btnRegistrar.UseVisualStyleBackColor = true;
            // 
            // btnLimpiar
            // 
            btnLimpiar.Location = new Point(367, 54);
            btnLimpiar.Name = "btnLimpiar";
            btnLimpiar.Size = new Size(106, 30);
            btnLimpiar.TabIndex = 20;
            btnLimpiar.Text = "Limpiar";
            btnLimpiar.UseVisualStyleBackColor = true;
            // 
            // FrmCompras
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1324, 590);
            Controls.Add(panel4);
            Controls.Add(dgvLineas);
            Controls.Add(panel3);
            Controls.Add(panel2);
            Controls.Add(panel1);
            Name = "FrmCompras";
            Text = "FrmCompras";
            panel2.ResumeLayout(false);
            panel2.PerformLayout();
            panel3.ResumeLayout(false);
            panel3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)numericUpDown1).EndInit();
            ((System.ComponentModel.ISupportInitialize)dgvLineas).EndInit();
            panel4.ResumeLayout(false);
            panel4.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Panel panel1;
        private Label label1;
        private Panel panel2;
        private Label lblSucursal;
        private ComboBox cmbProveedor;
        private Label Proveedor;
        private DateTimePicker dateTimePicker1;
        private Label label2;
        private Panel panel3;
        private ComboBox cmbProducto;
        private Label label3;
        private Button btnQuitarLinea;
        private Button btnAgregarLinea;
        private TextBox txtPrecioUnitario;
        private Label label5;
        private NumericUpDown numericUpDown1;
        private Label label4;
        private DataGridView dgvLineas;
        private Panel panel4;
        private Button btnRegistrar;
        private Label lblTotal;
        private Label label6;
        private Button btnLimpiar;
    }
}