namespace Veterinaria1
{
    partial class FrmLogin
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
            label1 = new Label();
            btnIngresar = new Button();
            txtUsuario = new TextBox();
            label2 = new Label();
            txtContrasenia = new TextBox();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.Location = new Point(26, 173);
            label1.Name = "label1";
            label1.Size = new Size(70, 23);
            label1.TabIndex = 0;
            label1.Text = "Usuario";
            // 
            // btnIngresar
            // 
            btnIngresar.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnIngresar.Location = new Point(209, 356);
            btnIngresar.Name = "btnIngresar";
            btnIngresar.Size = new Size(150, 48);
            btnIngresar.TabIndex = 2;
            btnIngresar.Text = "Ingresar";
            btnIngresar.UseVisualStyleBackColor = true;
            // 
            // txtUsuario
            // 
            txtUsuario.Location = new Point(209, 180);
            txtUsuario.Name = "txtUsuario";
            txtUsuario.Size = new Size(231, 27);
            txtUsuario.TabIndex = 3;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label2.Location = new Point(26, 265);
            label2.Name = "label2";
            label2.Size = new Size(99, 23);
            label2.TabIndex = 4;
            label2.Text = "Contraseña";
            // 
            // txtContrasenia
            // 
            txtContrasenia.Location = new Point(209, 261);
            txtContrasenia.Name = "txtContrasenia";
            txtContrasenia.Size = new Size(231, 27);
            txtContrasenia.TabIndex = 5;
            // 
            // FrmLogin
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.ControlLightLight;
            ClientSize = new Size(531, 579);
            Controls.Add(txtContrasenia);
            Controls.Add(label2);
            Controls.Add(txtUsuario);
            Controls.Add(btnIngresar);
            Controls.Add(label1);
            Name = "FrmLogin";
            Text = "FrmLogin";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private Button btnIngresar;
        private TextBox txtUsuario;
        private Label label2;
        private TextBox txtContrasenia;
    }
}