namespace Veterinaria1
{
    partial class Detalle_Ventas
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
            dtpFechaInicio = new DateTimePicker();
            Desde = new Label();
            lbhasta = new Label();
            dtpFechaFin = new DateTimePicker();
            btnBuscar = new Button();
            btnLimpiar = new Button();
            panel2 = new Panel();
            label1 = new Label();
            dgvFacturas = new DataGridView();
            panel3 = new Panel();
            btnDescargarPDF = new Button();
            dgvDetalle = new DataGridView();
            lblTotalValue = new Label();
            lblSucursal = new Label();
            lblCliente = new Label();
            lblFecha = new Label();
            lblNumFactura = new Label();
            panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvFacturas).BeginInit();
            panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvDetalle).BeginInit();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.BackColor = SystemColors.InactiveCaption;
            panel1.Location = new Point(1, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(1421, 35);
            panel1.TabIndex = 1;
            // 
            // dtpFechaInicio
            // 
            dtpFechaInicio.Location = new Point(102, 41);
            dtpFechaInicio.Name = "dtpFechaInicio";
            dtpFechaInicio.Size = new Size(102, 27);
            dtpFechaInicio.TabIndex = 2;
            // 
            // Desde
            // 
            Desde.AutoSize = true;
            Desde.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            Desde.Location = new Point(22, 46);
            Desde.Name = "Desde";
            Desde.Size = new Size(56, 20);
            Desde.TabIndex = 3;
            Desde.Text = "Desde:";
            Desde.Click += Desde_Click;
            // 
            // lbhasta
            // 
            lbhasta.AutoSize = true;
            lbhasta.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lbhasta.Location = new Point(236, 46);
            lbhasta.Name = "lbhasta";
            lbhasta.Size = new Size(53, 20);
            lbhasta.TabIndex = 4;
            lbhasta.Text = "Hasta:";
            // 
            // dtpFechaFin
            // 
            dtpFechaFin.Location = new Point(295, 41);
            dtpFechaFin.Name = "dtpFechaFin";
            dtpFechaFin.Size = new Size(102, 27);
            dtpFechaFin.TabIndex = 5;
            // 
            // btnBuscar
            // 
            btnBuscar.Location = new Point(440, 42);
            btnBuscar.Name = "btnBuscar";
            btnBuscar.Size = new Size(112, 29);
            btnBuscar.TabIndex = 6;
            btnBuscar.Text = "Buscar";
            btnBuscar.UseVisualStyleBackColor = true;
            // 
            // btnLimpiar
            // 
            btnLimpiar.Location = new Point(583, 41);
            btnLimpiar.Name = "btnLimpiar";
            btnLimpiar.Size = new Size(112, 29);
            btnLimpiar.TabIndex = 7;
            btnLimpiar.Text = "Limpiar";
            btnLimpiar.UseVisualStyleBackColor = true;
            // 
            // panel2
            // 
            panel2.BackColor = SystemColors.ButtonHighlight;
            panel2.Controls.Add(label1);
            panel2.Controls.Add(dgvFacturas);
            panel2.ForeColor = SystemColors.WindowText;
            panel2.Location = new Point(12, 112);
            panel2.Name = "panel2";
            panel2.Size = new Size(563, 646);
            panel2.TabIndex = 8;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.Location = new Point(208, 43);
            label1.Name = "label1";
            label1.Size = new Size(132, 20);
            label1.TabIndex = 1;
            label1.Text = "Facturas emitidas";
            // 
            // dgvFacturas
            // 
            dgvFacturas.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvFacturas.Location = new Point(10, 132);
            dgvFacturas.Name = "dgvFacturas";
            dgvFacturas.RowHeadersWidth = 51;
            dgvFacturas.Size = new Size(537, 342);
            dgvFacturas.TabIndex = 0;
            // 
            // panel3
            // 
            panel3.BackColor = SystemColors.ButtonHighlight;
            panel3.Controls.Add(btnDescargarPDF);
            panel3.Controls.Add(dgvDetalle);
            panel3.Controls.Add(lblTotalValue);
            panel3.Controls.Add(lblSucursal);
            panel3.Controls.Add(lblCliente);
            panel3.Controls.Add(lblFecha);
            panel3.Controls.Add(lblNumFactura);
            panel3.ForeColor = SystemColors.WindowText;
            panel3.Location = new Point(750, 112);
            panel3.Name = "panel3";
            panel3.Size = new Size(563, 646);
            panel3.TabIndex = 9;
            // 
            // btnDescargarPDF
            // 
            btnDescargarPDF.Location = new Point(46, 588);
            btnDescargarPDF.Name = "btnDescargarPDF";
            btnDescargarPDF.Size = new Size(141, 40);
            btnDescargarPDF.TabIndex = 8;
            btnDescargarPDF.Text = "button1";
            btnDescargarPDF.UseVisualStyleBackColor = true;
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
            // lblTotalValue
            // 
            lblTotalValue.AutoSize = true;
            lblTotalValue.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblTotalValue.Location = new Point(28, 221);
            lblTotalValue.Name = "lblTotalValue";
            lblTotalValue.Size = new Size(18, 20);
            lblTotalValue.TabIndex = 6;
            lblTotalValue.Text = "d";
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
            // lblCliente
            // 
            lblCliente.AutoSize = true;
            lblCliente.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblCliente.Location = new Point(28, 132);
            lblCliente.Name = "lblCliente";
            lblCliente.Size = new Size(18, 20);
            lblCliente.TabIndex = 4;
            lblCliente.Text = "d";
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
            // lblNumFactura
            // 
            lblNumFactura.AutoSize = true;
            lblNumFactura.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblNumFactura.Location = new Point(28, 52);
            lblNumFactura.Name = "lblNumFactura";
            lblNumFactura.Size = new Size(18, 20);
            lblNumFactura.TabIndex = 2;
            lblNumFactura.Text = "d";
            // 
            // Detalle_Ventas
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1424, 790);
            Controls.Add(panel3);
            Controls.Add(panel2);
            Controls.Add(btnLimpiar);
            Controls.Add(btnBuscar);
            Controls.Add(dtpFechaFin);
            Controls.Add(lbhasta);
            Controls.Add(Desde);
            Controls.Add(panel1);
            Controls.Add(dtpFechaInicio);
            Name = "Detalle_Ventas";
            Text = "Detalle_Ventas";
            panel2.ResumeLayout(false);
            panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvFacturas).EndInit();
            panel3.ResumeLayout(false);
            panel3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvDetalle).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Panel panel1;
        private DateTimePicker dtpFechaInicio;
        private Label Desde;
        private Label lbhasta;
        private DateTimePicker dtpFechaFin;
        private Button btnBuscar;
        private Button btnLimpiar;
        private Panel panel2;
        private Label label1;
        private DataGridView dgvFacturas;
        private Panel panel3;
        private Label lblCliente;
        private Label lblFecha;
        private Label lblNumFactura;
        private DataGridView dgvDetalle;
        private Label lblTotalValue;
        private Label lblSucursal;
        private Button btnDescargarPDF;
    }
}