using System.Drawing;
using System.Windows.Forms;

namespace Veterinaria1
{
    internal sealed class PlaceholderView : UserControl
    {
        public PlaceholderView(string title, string subtitle)
        {
            BackColor = Color.FromArgb(248, 250, 252);
            Dock = DockStyle.Fill;
            Padding = new Padding(28);

            var titleLabel = new Label
            {
                AutoSize = true,
                Dock = DockStyle.Top,
                Font = new Font("Segoe UI", 20F, FontStyle.Bold),
                ForeColor = Color.FromArgb(15, 23, 42),
                Padding = new Padding(0, 0, 0, 8),
                Text = title
            };

            var subtitleLabel = new Label
            {
                AutoSize = true,
                Dock = DockStyle.Top,
                Font = new Font("Segoe UI", 10F),
                ForeColor = Color.FromArgb(100, 116, 139),
                Padding = new Padding(0, 0, 0, 24),
                Text = subtitle
            };

            var messagePanel = new Panel
            {
                BackColor = Color.White,
                BorderStyle = BorderStyle.FixedSingle,
                Dock = DockStyle.Top,
                Height = 110,
                Padding = new Padding(20)
            };

            var messageLabel = new Label
            {
                Dock = DockStyle.Fill,
                Font = new Font("Segoe UI", 10F),
                ForeColor = Color.FromArgb(51, 65, 85),
                Text = "Esta vista se esta mostrando dentro de frmMain. Aqui puede reemplazarse por el formulario o UserControl definitivo del modulo.",
                TextAlign = ContentAlignment.MiddleLeft
            };

            messagePanel.Controls.Add(messageLabel);
            Controls.Add(messagePanel);
            Controls.Add(subtitleLabel);
            Controls.Add(titleLabel);
        }
    }
}
