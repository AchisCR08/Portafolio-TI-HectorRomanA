namespace Veterinaria1
{
    partial class Detalle_Compras
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
            btnLimpiar = new Button();
            btnBuscar = new Button();
            dtpFechaFin = new DateTimePicker();
            lbhasta = new Label();
            Desde = new Label();
            dtpFechaInicio = new DateTimePicker();
            panel1 = new Panel();
            panel2 = new Panel();
            label1 = new Label();
            dgvCompras = new DataGridView();
            panel3 = new Panel();
            dgvDetalle = new DataGridView();
            lblTotal = new Label();
            lblSucursal = new Label();
            lblProveedor = new Label();
            lblFecha = new Label();
            lblNumCompra = new Label();
            panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvCompras).BeginInit();
            panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvDetalle).BeginInit();
            SuspendLayout();
            // 
            // btnLimpiar
            // 
            btnLimpiar.Location = new Point(573, 46);
            btnLimpiar.Name = "btnLimpiar";
            btnLimpiar.Size = new Size(112, 29);
            btnLimpiar.TabIndex = 13;
            btnLimpiar.Text = "Limpiar";
            btnLimpiar.UseVisualStyleBackColor = true;
            // 
            // btnBuscar
            // 
            btnBuscar.Location = new Point(430, 47);
            btnBuscar.Name = "btnBuscar";
            btnBuscar.Size = new Size(112, 29);
            btnBuscar.TabIndex = 12;
            btnBuscar.Text = "Buscar";
            btnBuscar.UseVisualStyleBackColor = true;
            // 
            // dtpFechaFin
            // 
            dtpFechaFin.Location = new Point(285, 46);
            dtpFechaFin.Name = "dtpFechaFin";
            dtpFechaFin.Size = new Size(102, 27);
            dtpFechaFin.TabIndex = 11;
            // 
            // lbhasta
            // 
            lbhasta.AutoSize = true;
            lbhasta.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lbhasta.Location = new Point(226, 51);
            lbhasta.Name = "lbhasta";
            lbhasta.Size = new Size(53, 20);
            lbhasta.TabIndex = 10;
            lbhasta.Text = "Hasta:";
            // 
            // Desde
            // 
            Desde.AutoSize = true;
            Desde.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            Desde.Location = new Point(12, 51);
            Desde.Name = "Desde";
            Desde.Size = new Size(56, 20);
            Desde.TabIndex = 9;
            Desde.Text = "Desde:";
            // 
            // dtpFechaInicio
            // 
            dtpFechaInicio.Location = new Point(92, 46);
            dtpFechaInicio.Name = "dtpFechaInicio";
            dtpFechaInicio.Size = new Size(102, 27);
            dtpFechaInicio.TabIndex = 8;
            // 
            // panel1
            // 
            panel1.BackColor = SystemColors.InactiveCaption;
            panel1.Location = new Point(0, 2);
            panel1.Name = "panel1";
            panel1.Size = new Size(1421, 35);
            panel1.TabIndex = 14;
            // 
            // panel2
            // 
            panel2.BackColor = SystemColors.ButtonHighlight;
            panel2.Controls.Add(label1);
            panel2.Controls.Add(dgvCompras);
            panel2.ForeColor = SystemColors.WindowText;
            panel2.Location = new Point(12, 112);
            panel2.Name = "panel2";
            panel2.Size = new Size(563, 646);
            panel2.TabIndex = 15;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.Location = new Point(208, 43);
            label1.Name = "label1";
            label1.Size = new Size(71, 20);
            label1.TabIndex = 1;
            label1.Text = "Compras";
            // 
            // dgvCompras
            // 
            dgvCompras.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvCompras.Location = new Point(10, 132);
            dgvCompras.Name = "dgvCompras";
            dgvCompras.RowHeadersWidth = 51;
            dgvCompras.Size = new Size(537, 342);
            dgvCompras.TabIndex = 0;
            // 
            // panel3
            // 
            panel3.BackColor = SystemColors.ButtonHighlight;
            panel3.Controls.Add(dgvDetalle);
            panel3.Controls.Add(lblTotal);
            panel3.Controls.Add(lblSucursal);
            panel3.Controls.Add(lblProveedor);
            panel3.Controls.Add(lblFecha);
            panel3.Controls.Add(lblNumCompra);
            panel3.ForeColor = SystemColors.WindowText;
            panel3.Location = new Point(810, 112);
            panel3.Name = "panel3";
            panel3.Size = new Size(563, 646);
            panel3.TabIndex = 16;
            // 
            // dgvDetalle
            // 
            dgvDetalle.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvDetalle.Location = new Point(26, 306);
            dgvDetalle.Name = "dgvDetalle";
            dgvDetalle.RowHeadersWidth = 51;
            dgvDetalle.Size = new Size(529, 261);
            dgvDetalle.TabIndex = 7;
            // 
            // lblTotal
            // 
            lblTotal.AutoSize = true;
            lblTotal.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblTotal.Location = new Point(28, 221);
            lblTotal.Name = "lblTotal";
            lblTotal.Size = new Size(18, 20);
            lblTotal.TabIndex = 6;
            lblTotal.Text = "d";
            // 
            // lblSucursal
            // 
            lblSucursal.AutoSize = true;
            lblSucursal.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblSucursal.Location = new Point(28, 174);
            lblSucursal.Name = "lblSucursal";
            lblSucursal.Size = new Size(18, 20);
            lblSucursal.TabIndex = 5;
            lblSucursal.Text = "d";
            // 
            // lblProveedor
            // 
            lblProveedor.AutoSize = true;
            lblProveedor.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblProveedor.Location = new Point(28, 132);
            lblProveedor.Name = "lblProveedor";
            lblProveedor.Size = new Size(18, 20);
            lblProveedor.TabIndex = 4;
            lblProveedor.Text = "d";
            // 
            // lblFecha
            // 
            lblFecha.AutoSize = true;
            lblFecha.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblFecha.Location = new Point(28, 96);
            lblFecha.Name = "lblFecha";
            lblFecha.Size = new Size(18, 20);
            lblFecha.TabIndex = 3;
            lblFecha.Text = "d";
            // 
            // lblNumCompra
            // 
            lblNumCompra.AutoSize = true;
            lblNumCompra.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblNumCompra.Location = new Point(28, 52);
            lblNumCompra.Name = "lblNumCompra";
            lblNumCompra.Size = new Size(18, 20);
            lblNumCompra.TabIndex = 2;
            lblNumCompra.Text = "d";
            // 
            // Detalle_Compras
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1424, 790);
            Controls.Add(panel3);
            Controls.Add(panel2);
            Controls.Add(panel1);
            Controls.Add(btnLimpiar);
            Controls.Add(btnBuscar);
            Controls.Add(dtpFechaFin);
            Controls.Add(lbhasta);
            Controls.Add(Desde);
            Controls.Add(dtpFechaInicio);
            Name = "Detalle_Compras";
            Text = "Detalle_Compras";
            panel2.ResumeLayout(false);
            panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvCompras).EndInit();
            panel3.ResumeLayout(false);
            panel3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvDetalle).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button btnLimpiar;
        private Button btnBuscar;
        private DateTimePicker dtpFechaFin;
        private Label lbhasta;
        private Label Desde;
        private DateTimePicker dtpFechaInicio;
        private Panel panel1;
        private Panel panel2;
        private Label label1;
        private DataGridView dgvCompras;
        private Panel panel3;
        private DataGridView dgvDetalle;
        private Label lblTotal;
        private Label lblSucursal;
        private Label lblProveedor;
        private Label lblFecha;
        private Label lblNumCompra;
    }
}