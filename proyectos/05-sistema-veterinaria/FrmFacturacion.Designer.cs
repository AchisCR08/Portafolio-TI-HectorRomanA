namespace Veterinaria1
{
    partial class FrmFacturacion
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
            btnAgregarLinea = new Button();
            txtPrecioUnitario = new TextBox();
            label5 = new Label();
            txtCantidad = new TextBox();
            lb5 = new Label();
            label4 = new Label();
            cmbItem = new ComboBox();
            rbCitaGrooming = new RadioButton();
            rbCitaVet = new RadioButton();
            rbProducto = new RadioButton();
            lblFecha = new Label();
            label3 = new Label();
            cmbCliente = new ComboBox();
            label2 = new Label();
            lblSucursalFactura = new Label();
            label1 = new Label();
            sqlCommand1 = new Microsoft.Data.SqlClient.SqlCommand();
            dgvDetalle = new DataGridView();
            btnQuitarLinea = new Button();
            label6 = new Label();
            lblTotal = new Label();
            panel3 = new Panel();
            btnLimpiar = new Button();
            btnEmitir = new Button();
            cmbTipoPago = new ComboBox();
            LBTIpopago = new Label();
            panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvDetalle).BeginInit();
            panel3.SuspendLayout();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.BackColor = SystemColors.InactiveCaption;
            panel1.Location = new Point(-1, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(1226, 50);
            panel1.TabIndex = 0;
            // 
            // panel2
            // 
            panel2.Controls.Add(btnAgregarLinea);
            panel2.Controls.Add(txtPrecioUnitario);
            panel2.Controls.Add(label5);
            panel2.Controls.Add(txtCantidad);
            panel2.Controls.Add(lb5);
            panel2.Controls.Add(label4);
            panel2.Controls.Add(cmbItem);
            panel2.Controls.Add(rbCitaGrooming);
            panel2.Controls.Add(rbCitaVet);
            panel2.Controls.Add(rbProducto);
            panel2.Controls.Add(lblFecha);
            panel2.Controls.Add(label3);
            panel2.Controls.Add(cmbCliente);
            panel2.Controls.Add(label2);
            panel2.Controls.Add(lblSucursalFactura);
            panel2.Controls.Add(label1);
            panel2.Location = new Point(12, 74);
            panel2.Name = "panel2";
            panel2.Size = new Size(505, 460);
            panel2.TabIndex = 1;
            // 
            // btnAgregarLinea
            // 
            btnAgregarLinea.Location = new Point(211, 406);
            btnAgregarLinea.Name = "btnAgregarLinea";
            btnAgregarLinea.Size = new Size(214, 36);
            btnAgregarLinea.TabIndex = 15;
            btnAgregarLinea.Text = "Agregar";
            btnAgregarLinea.UseVisualStyleBackColor = true;
            // 
            // txtPrecioUnitario
            // 
            txtPrecioUnitario.Location = new Point(211, 361);
            txtPrecioUnitario.Name = "txtPrecioUnitario";
            txtPrecioUnitario.Size = new Size(258, 27);
            txtPrecioUnitario.TabIndex = 14;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label5.Location = new Point(25, 362);
            label5.Name = "label5";
            label5.Size = new Size(129, 23);
            label5.TabIndex = 13;
            label5.Text = "Precio Unitario";
            // 
            // txtCantidad
            // 
            txtCantidad.Location = new Point(211, 305);
            txtCantidad.Name = "txtCantidad";
            txtCantidad.Size = new Size(258, 27);
            txtCantidad.TabIndex = 12;
            // 
            // lb5
            // 
            lb5.AutoSize = true;
            lb5.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lb5.Location = new Point(25, 309);
            lb5.Name = "lb5";
            lb5.Size = new Size(83, 23);
            lb5.TabIndex = 11;
            lb5.Text = "Cantidad";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label4.Location = new Point(25, 248);
            label4.Name = "label4";
            label4.Size = new Size(47, 23);
            label4.TabIndex = 10;
            label4.Text = "Item";
            // 
            // cmbItem
            // 
            cmbItem.FormattingEnabled = true;
            cmbItem.Location = new Point(215, 248);
            cmbItem.Name = "cmbItem";
            cmbItem.Size = new Size(254, 28);
            cmbItem.TabIndex = 9;
            // 
            // rbCitaGrooming
            // 
            rbCitaGrooming.AutoSize = true;
            rbCitaGrooming.Location = new Point(221, 208);
            rbCitaGrooming.Name = "rbCitaGrooming";
            rbCitaGrooming.Size = new Size(124, 24);
            rbCitaGrooming.TabIndex = 8;
            rbCitaGrooming.TabStop = true;
            rbCitaGrooming.Text = "cita grooming";
            rbCitaGrooming.UseVisualStyleBackColor = true;
            // 
            // rbCitaVet
            // 
            rbCitaVet.AutoSize = true;
            rbCitaVet.Location = new Point(221, 178);
            rbCitaVet.Name = "rbCitaVet";
            rbCitaVet.Size = new Size(128, 24);
            rbCitaVet.TabIndex = 7;
            rbCitaVet.TabStop = true;
            rbCitaVet.Text = "cita veterinaria";
            rbCitaVet.UseVisualStyleBackColor = true;
            // 
            // rbProducto
            // 
            rbProducto.AutoSize = true;
            rbProducto.Location = new Point(221, 148);
            rbProducto.Name = "rbProducto";
            rbProducto.Size = new Size(90, 24);
            rbProducto.TabIndex = 6;
            rbProducto.TabStop = true;
            rbProducto.Text = "Producto";
            rbProducto.UseVisualStyleBackColor = true;
            // 
            // lblFecha
            // 
            lblFecha.AutoSize = true;
            lblFecha.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblFecha.Location = new Point(221, 110);
            lblFecha.Name = "lblFecha";
            lblFecha.Size = new Size(55, 23);
            lblFecha.TabIndex = 5;
            lblFecha.Text = "Fecha";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label3.Location = new Point(14, 110);
            label3.Name = "label3";
            label3.Size = new Size(55, 23);
            label3.TabIndex = 4;
            label3.Text = "Fecha";
            // 
            // cmbCliente
            // 
            cmbCliente.FormattingEnabled = true;
            cmbCliente.Location = new Point(221, 61);
            cmbCliente.Name = "cmbCliente";
            cmbCliente.Size = new Size(255, 28);
            cmbCliente.TabIndex = 3;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label2.Location = new Point(14, 66);
            label2.Name = "label2";
            label2.Size = new Size(66, 23);
            label2.TabIndex = 2;
            label2.Text = "Cliente";
            // 
            // lblSucursalFactura
            // 
            lblSucursalFactura.AutoSize = true;
            lblSucursalFactura.Location = new Point(270, 22);
            lblSucursalFactura.Name = "lblSucursalFactura";
            lblSucursalFactura.Size = new Size(30, 20);
            lblSucursalFactura.TabIndex = 1;
            lblSucursalFactura.Text = "sds";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.Location = new Point(14, 20);
            label1.Name = "label1";
            label1.Size = new Size(76, 23);
            label1.TabIndex = 0;
            label1.Text = "Sucursal";
            // 
            // sqlCommand1
            // 
            sqlCommand1.CommandTimeout = 30;
            sqlCommand1.EnableOptimizedParameterBinding = false;
            // 
            // dgvDetalle
            // 
            dgvDetalle.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvDetalle.Location = new Point(546, 83);
            dgvDetalle.Name = "dgvDetalle";
            dgvDetalle.RowHeadersWidth = 51;
            dgvDetalle.Size = new Size(666, 269);
            dgvDetalle.TabIndex = 2;
            // 
            // btnQuitarLinea
            // 
            btnQuitarLinea.Location = new Point(555, 366);
            btnQuitarLinea.Name = "btnQuitarLinea";
            btnQuitarLinea.Size = new Size(139, 40);
            btnQuitarLinea.TabIndex = 3;
            btnQuitarLinea.Text = "Quitar linea";
            btnQuitarLinea.UseVisualStyleBackColor = true;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label6.Location = new Point(802, 375);
            label6.Name = "label6";
            label6.Size = new Size(71, 23);
            label6.TabIndex = 4;
            label6.Text = "TOTAL: ";
            // 
            // lblTotal
            // 
            lblTotal.AutoSize = true;
            lblTotal.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblTotal.Location = new Point(899, 375);
            lblTotal.Name = "lblTotal";
            lblTotal.Size = new Size(0, 23);
            lblTotal.TabIndex = 5;
            // 
            // panel3
            // 
            panel3.Controls.Add(btnLimpiar);
            panel3.Controls.Add(btnEmitir);
            panel3.Controls.Add(cmbTipoPago);
            panel3.Controls.Add(LBTIpopago);
            panel3.Location = new Point(555, 450);
            panel3.Name = "panel3";
            panel3.Size = new Size(657, 203);
            panel3.TabIndex = 6;
            // 
            // btnLimpiar
            // 
            btnLimpiar.Location = new Point(408, 165);
            btnLimpiar.Name = "btnLimpiar";
            btnLimpiar.Size = new Size(246, 35);
            btnLimpiar.TabIndex = 18;
            btnLimpiar.Text = "Limpiar";
            btnLimpiar.UseVisualStyleBackColor = true;
            // 
            // btnEmitir
            // 
            btnEmitir.Location = new Point(128, 70);
            btnEmitir.Name = "btnEmitir";
            btnEmitir.Size = new Size(246, 35);
            btnEmitir.TabIndex = 17;
            btnEmitir.Text = "Emitir";
            btnEmitir.UseVisualStyleBackColor = true;
            // 
            // cmbTipoPago
            // 
            cmbTipoPago.FormattingEnabled = true;
            cmbTipoPago.Location = new Point(127, 9);
            cmbTipoPago.Name = "cmbTipoPago";
            cmbTipoPago.Size = new Size(254, 28);
            cmbTipoPago.TabIndex = 16;
            // 
            // LBTIpopago
            // 
            LBTIpopago.AutoSize = true;
            LBTIpopago.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            LBTIpopago.Location = new Point(13, 14);
            LBTIpopago.Name = "LBTIpopago";
            LBTIpopago.Size = new Size(92, 23);
            LBTIpopago.TabIndex = 16;
            LBTIpopago.Text = "Tipo pago";
            // 
            // FrmFacturacion
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1224, 690);
            Controls.Add(panel3);
            Controls.Add(lblTotal);
            Controls.Add(label6);
            Controls.Add(btnQuitarLinea);
            Controls.Add(dgvDetalle);
            Controls.Add(panel2);
            Controls.Add(panel1);
            Name = "FrmFacturacion";
            Text = "FrmFacturacion";
            Load += FrmFacturacion_Load_1;
            panel2.ResumeLayout(false);
            panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvDetalle).EndInit();
            panel3.ResumeLayout(false);
            panel3.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Panel panel1;
        private Panel panel2;
        private Label lblSucursalFactura;
        private Label label1;
        private Microsoft.Data.SqlClient.SqlCommand sqlCommand1;
        private RadioButton rbCitaVet;
        private RadioButton rbProducto;
        private Label lblFecha;
        private Label label3;
        private ComboBox cmbCliente;
        private Label label2;
        private TextBox txtPrecioUnitario;
        private Label label5;
        private TextBox txtCantidad;
        private Label lb5;
        private Label label4;
        private ComboBox cmbItem;
        private RadioButton rbCitaGrooming;
        private Button btnAgregarLinea;
        private DataGridView dgvDetalle;
        private Button btnQuitarLinea;
        private Label label6;
        private Label lblTotal;
        private Panel panel3;
        private Label LBTIpopago;
        private Button btnLimpiar;
        private Button btnEmitir;
        private ComboBox cmbTipoPago;
    }
}