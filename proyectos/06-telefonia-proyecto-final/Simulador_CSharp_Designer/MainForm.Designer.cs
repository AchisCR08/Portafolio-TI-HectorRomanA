using System.Drawing;
using System.Windows.Forms;

namespace SimuladorTelefoniaDesigner;

public partial class MainForm
{
    private System.ComponentModel.IContainer components = null!;
    private SplitContainer splitMain;
    private TabControl tabsPhones;
    private TabPage tabPhone1;
    private TabPage tabPhone2;
    private GroupBox groupConnection;
    private System.Windows.Forms.Timer uiTimer;

    private Label lblClock1;
    private Label lblTitle1;
    private Label lblPhoneTitle1;
    private Label lblDeviceTitle1;
    private Label lblCardTitle1;
    private Label lblLocationTitle1;
    private TextBox txtPhone1;
    private TextBox txtDeviceId1;
    private TextBox txtCardId1;
    private TextBox txtLocation1;
    private TextBox txtDisplay1;
    private Button btnKey1_1;
    private Button btnKey2_1;
    private Button btnKey3_1;
    private Button btnKey4_1;
    private Button btnKey5_1;
    private Button btnKey6_1;
    private Button btnKey7_1;
    private Button btnKey8_1;
    private Button btnKey9_1;
    private Button btnKeyStar1;
    private Button btnKey0_1;
    private Button btnKeyHash1;
    private Button btnBackspace1;
    private Button btnClear1;
    private Button btnBalance1;
    private Button btnCall1;
    private Button btnHang1;
    private Label lblTimerTitle1;
    private Label lblTimer1;
    private Label lblDestinationTitle1;
    private Label lblTimeTitle1;
    private Label lblTariffTitle1;
    private TextBox txtDestination1;
    private TextBox txtTime1;
    private TextBox txtTariff1;
    private Label lblStatus1;

    private Label lblClock2;
    private Label lblTitle2;
    private TextBox txtPhone2;
    private TextBox txtDeviceId2;
    private TextBox txtCardId2;
    private TextBox txtLocation2;
    private Label lblPhoneTitle2;
    private Label lblDeviceTitle2;
    private Label lblCardTitle2;
    private Label lblLocationTitle2;
    private TextBox txtDisplay2;
    private Button btnKey1_2;
    private Button btnKey2_2;
    private Button btnKey3_2;
    private Button btnKey4_2;
    private Button btnKey5_2;
    private Button btnKey6_2;
    private Button btnKey7_2;
    private Button btnKey8_2;
    private Button btnKey9_2;
    private Button btnKeyStar2;
    private Button btnKey0_2;
    private Button btnKeyHash2;
    private Button btnBackspace2;
    private Button btnClear2;
    private Button btnBalance2;
    private Button btnCall2;
    private Button btnHang2;
    private Label lblTimerTitle2;
    private Label lblTimer2;
    private Label lblDestinationTitle2;
    private Label lblTimeTitle2;
    private Label lblTariffTitle2;
    private TextBox txtDestination2;
    private TextBox txtTime2;
    private TextBox txtTariff2;
    private Label lblStatus2;

    private Label lblHost;
    private TextBox txtHost;
    private Label lblPort;
    private NumericUpDown numPort;
    private CheckBox chkEncrypt;
    private Button btnTestConnection;
    private Label lblRaw;
    private TextBox txtRaw;
    private Label lblResponse;
    private TextBox txtResponse;

    protected override void Dispose(bool disposing)
    {
        if (disposing && components != null)
        {
            components.Dispose();
        }

        base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
        components = new System.ComponentModel.Container();
        splitMain = new SplitContainer();
        tabsPhones = new TabControl();
        tabPhone1 = new TabPage();
        lblStatus1 = new Label();
        txtTariff1 = new TextBox();
        txtTime1 = new TextBox();
        txtDestination1 = new TextBox();
        lblTariffTitle1 = new Label();
        lblTimeTitle1 = new Label();
        lblDestinationTitle1 = new Label();
        lblTimer1 = new Label();
        lblTimerTitle1 = new Label();
        btnHang1 = new Button();
        btnCall1 = new Button();
        btnBalance1 = new Button();
        btnClear1 = new Button();
        btnBackspace1 = new Button();
        btnKeyHash1 = new Button();
        btnKey0_1 = new Button();
        btnKeyStar1 = new Button();
        btnKey9_1 = new Button();
        btnKey8_1 = new Button();
        btnKey7_1 = new Button();
        btnKey6_1 = new Button();
        btnKey5_1 = new Button();
        btnKey4_1 = new Button();
        btnKey3_1 = new Button();
        btnKey2_1 = new Button();
        btnKey1_1 = new Button();
        txtDisplay1 = new TextBox();
        lblTitle1 = new Label();
        lblClock1 = new Label();
        tabPhone2 = new TabPage();
        lblStatus2 = new Label();
        txtTariff2 = new TextBox();
        txtTime2 = new TextBox();
        txtDestination2 = new TextBox();
        lblTariffTitle2 = new Label();
        lblTimeTitle2 = new Label();
        lblDestinationTitle2 = new Label();
        lblTimer2 = new Label();
        lblTimerTitle2 = new Label();
        btnHang2 = new Button();
        btnCall2 = new Button();
        btnBalance2 = new Button();
        btnClear2 = new Button();
        btnBackspace2 = new Button();
        btnKeyHash2 = new Button();
        btnKey0_2 = new Button();
        btnKeyStar2 = new Button();
        btnKey9_2 = new Button();
        btnKey8_2 = new Button();
        btnKey7_2 = new Button();
        btnKey6_2 = new Button();
        btnKey5_2 = new Button();
        btnKey4_2 = new Button();
        btnKey3_2 = new Button();
        btnKey2_2 = new Button();
        btnKey1_2 = new Button();
        txtDisplay2 = new TextBox();
        lblTitle2 = new Label();
        lblClock2 = new Label();
        groupConnection = new GroupBox();
        txtResponse = new TextBox();
        lblResponse = new Label();
        txtRaw = new TextBox();
        lblRaw = new Label();
        btnTestConnection = new Button();
        chkEncrypt = new CheckBox();
        numPort = new NumericUpDown();
        lblPort = new Label();
        txtHost = new TextBox();
        lblHost = new Label();
        txtCardId1 = new TextBox();
        lblPhoneTitle1 = new Label();
        lblCardTitle1 = new Label();
        txtPhone1 = new TextBox();
        txtLocation1 = new TextBox();
        lblDeviceTitle1 = new Label();
        lblLocationTitle1 = new Label();
        txtDeviceId1 = new TextBox();
        txtPhone2 = new TextBox();
        lblPhoneTitle2 = new Label();
        txtDeviceId2 = new TextBox();
        lblDeviceTitle2 = new Label();
        txtCardId2 = new TextBox();
        lblCardTitle2 = new Label();
        txtLocation2 = new TextBox();
        lblLocationTitle2 = new Label();
        uiTimer = new System.Windows.Forms.Timer(components);
        ((System.ComponentModel.ISupportInitialize)splitMain).BeginInit();
        splitMain.Panel1.SuspendLayout();
        splitMain.Panel2.SuspendLayout();
        splitMain.SuspendLayout();
        tabsPhones.SuspendLayout();
        tabPhone1.SuspendLayout();
        tabPhone2.SuspendLayout();
        groupConnection.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)numPort).BeginInit();
        SuspendLayout();
        // 
        // splitMain
        // 
        splitMain.Dock = DockStyle.Fill;
        splitMain.FixedPanel = FixedPanel.Panel2;
        splitMain.Location = new Point(0, 0);
        splitMain.Margin = new Padding(3, 4, 3, 4);
        splitMain.Name = "splitMain";
        // 
        // splitMain.Panel1
        // 
        splitMain.Panel1.Controls.Add(tabsPhones);
        // 
        // splitMain.Panel2
        // 
        splitMain.Panel2.Controls.Add(groupConnection);
        splitMain.Size = new Size(1463, 1055);
        splitMain.SplitterDistance = 977;
        splitMain.SplitterWidth = 5;
        splitMain.TabIndex = 0;
        // 
        // tabsPhones
        // 
        tabsPhones.Controls.Add(tabPhone1);
        tabsPhones.Controls.Add(tabPhone2);
        tabsPhones.Dock = DockStyle.Fill;
        tabsPhones.Font = new Font("Segoe UI", 10F);
        tabsPhones.Location = new Point(0, 0);
        tabsPhones.Margin = new Padding(3, 4, 3, 4);
        tabsPhones.Name = "tabsPhones";
        tabsPhones.SelectedIndex = 0;
        tabsPhones.Size = new Size(977, 1055);
        tabsPhones.TabIndex = 0;
        // 
        // tabPhone1
        // 
        tabPhone1.BackColor = Color.White;
        tabPhone1.Controls.Add(lblStatus1);
        tabPhone1.Controls.Add(txtTariff1);
        tabPhone1.Controls.Add(txtTime1);
        tabPhone1.Controls.Add(txtDestination1);
        tabPhone1.Controls.Add(lblTariffTitle1);
        tabPhone1.Controls.Add(lblTimeTitle1);
        tabPhone1.Controls.Add(lblDestinationTitle1);
        tabPhone1.Controls.Add(lblTimer1);
        tabPhone1.Controls.Add(lblTimerTitle1);
        tabPhone1.Controls.Add(btnHang1);
        tabPhone1.Controls.Add(btnCall1);
        tabPhone1.Controls.Add(btnBalance1);
        tabPhone1.Controls.Add(btnClear1);
        tabPhone1.Controls.Add(btnBackspace1);
        tabPhone1.Controls.Add(btnKeyHash1);
        tabPhone1.Controls.Add(btnKey0_1);
        tabPhone1.Controls.Add(btnKeyStar1);
        tabPhone1.Controls.Add(btnKey9_1);
        tabPhone1.Controls.Add(btnKey8_1);
        tabPhone1.Controls.Add(btnKey7_1);
        tabPhone1.Controls.Add(btnKey6_1);
        tabPhone1.Controls.Add(btnKey5_1);
        tabPhone1.Controls.Add(btnKey4_1);
        tabPhone1.Controls.Add(btnKey3_1);
        tabPhone1.Controls.Add(btnKey2_1);
        tabPhone1.Controls.Add(btnKey1_1);
        tabPhone1.Controls.Add(txtDisplay1);
        tabPhone1.Controls.Add(lblTitle1);
        tabPhone1.Controls.Add(lblClock1);
        tabPhone1.Location = new Point(4, 32);
        tabPhone1.Margin = new Padding(3, 4, 3, 4);
        tabPhone1.Name = "tabPhone1";
        tabPhone1.Padding = new Padding(21, 24, 21, 24);
        tabPhone1.Size = new Size(969, 1019);
        tabPhone1.TabIndex = 0;
        tabPhone1.Text = "Telefono 1";
        // 
        // lblStatus1
        // 
        lblStatus1.BorderStyle = BorderStyle.FixedSingle;
        lblStatus1.Location = new Point(584, 598);
        lblStatus1.Name = "lblStatus1";
        lblStatus1.Size = new Size(343, 173);
        lblStatus1.TabIndex = 36;
        lblStatus1.Text = "Ver el saldo";
        lblStatus1.TextAlign = ContentAlignment.MiddleCenter;
        // 
        // txtTariff1
        // 
        txtTariff1.Location = new Point(746, 505);
        txtTariff1.Margin = new Padding(3, 4, 3, 4);
        txtTariff1.Name = "txtTariff1";
        txtTariff1.ReadOnly = true;
        txtTariff1.Size = new Size(205, 30);
        txtTariff1.TabIndex = 35;
        // 
        // txtTime1
        // 
        txtTime1.Location = new Point(746, 445);
        txtTime1.Margin = new Padding(3, 4, 3, 4);
        txtTime1.Name = "txtTime1";
        txtTime1.ReadOnly = true;
        txtTime1.Size = new Size(205, 30);
        txtTime1.TabIndex = 33;
        // 
        // txtDestination1
        // 
        txtDestination1.Location = new Point(746, 385);
        txtDestination1.Margin = new Padding(3, 4, 3, 4);
        txtDestination1.Name = "txtDestination1";
        txtDestination1.ReadOnly = true;
        txtDestination1.Size = new Size(205, 30);
        txtDestination1.TabIndex = 31;
        txtDestination1.TextChanged += txtDestination1_TextChanged;
        // 
        // lblTariffTitle1
        // 
        lblTariffTitle1.AutoSize = true;
        lblTariffTitle1.Location = new Point(590, 505);
        lblTariffTitle1.Name = "lblTariffTitle1";
        lblTariffTitle1.Size = new Size(50, 23);
        lblTariffTitle1.TabIndex = 34;
        lblTariffTitle1.Text = "Tarifa";
        // 
        // lblTimeTitle1
        // 
        lblTimeTitle1.AutoSize = true;
        lblTimeTitle1.Location = new Point(590, 445);
        lblTimeTitle1.Name = "lblTimeTitle1";
        lblTimeTitle1.Size = new Size(133, 23);
        lblTimeTitle1.TabIndex = 32;
        lblTimeTitle1.Text = "Tiempo maximo";
        // 
        // lblDestinationTitle1
        // 
        lblDestinationTitle1.AutoSize = true;
        lblDestinationTitle1.Location = new Point(590, 385);
        lblDestinationTitle1.Name = "lblDestinationTitle1";
        lblDestinationTitle1.Size = new Size(68, 23);
        lblDestinationTitle1.TabIndex = 30;
        lblDestinationTitle1.Text = "Destino";
        // 
        // lblTimer1
        // 
        lblTimer1.Font = new Font("Segoe UI Semibold", 32F, FontStyle.Bold);
        lblTimer1.Location = new Point(595, 227);
        lblTimer1.Name = "lblTimer1";
        lblTimer1.Size = new Size(274, 107);
        lblTimer1.TabIndex = 29;
        lblTimer1.Text = "00:00";
        lblTimer1.TextAlign = ContentAlignment.MiddleCenter;
        // 
        // lblTimerTitle1
        // 
        lblTimerTitle1.AutoSize = true;
        lblTimerTitle1.Location = new Point(670, 199);
        lblTimerTitle1.Name = "lblTimerTitle1";
        lblTimerTitle1.Size = new Size(118, 23);
        lblTimerTitle1.TabIndex = 28;
        lblTimerTitle1.Text = "Tiempo actual";
        // 
        // btnHang1
        // 
        btnHang1.BackColor = Color.FromArgb(224, 49, 49);
        btnHang1.FlatStyle = FlatStyle.Flat;
        btnHang1.ForeColor = Color.White;
        btnHang1.Location = new Point(212, 777);
        btnHang1.Margin = new Padding(3, 4, 3, 4);
        btnHang1.Name = "btnHang1";
        btnHang1.Size = new Size(177, 69);
        btnHang1.TabIndex = 27;
        btnHang1.Text = "Colgar";
        btnHang1.UseVisualStyleBackColor = false;
        btnHang1.Visible = false;
        btnHang1.Click += Phone1_Hang_Click;
        // 
        // btnCall1
        // 
        btnCall1.BackColor = Color.FromArgb(35, 173, 96);
        btnCall1.FlatStyle = FlatStyle.Flat;
        btnCall1.ForeColor = Color.White;
        btnCall1.Location = new Point(212, 777);
        btnCall1.Margin = new Padding(3, 4, 3, 4);
        btnCall1.Name = "btnCall1";
        btnCall1.Size = new Size(177, 69);
        btnCall1.TabIndex = 26;
        btnCall1.Text = "Llamar";
        btnCall1.UseVisualStyleBackColor = false;
        btnCall1.Click += Phone1_Call_Click;
        // 
        // btnBalance1
        // 
        btnBalance1.Location = new Point(395, 677);
        btnBalance1.Margin = new Padding(3, 4, 3, 4);
        btnBalance1.Name = "btnBalance1";
        btnBalance1.Size = new Size(98, 56);
        btnBalance1.TabIndex = 25;
        btnBalance1.Text = "Saldo";
        btnBalance1.UseVisualStyleBackColor = true;
        btnBalance1.Click += Phone1_Balance_Click;
        // 
        // btnClear1
        // 
        btnClear1.Location = new Point(252, 677);
        btnClear1.Margin = new Padding(3, 4, 3, 4);
        btnClear1.Name = "btnClear1";
        btnClear1.Size = new Size(98, 56);
        btnClear1.TabIndex = 24;
        btnClear1.Text = "Limpiar";
        btnClear1.UseVisualStyleBackColor = true;
        btnClear1.Click += Phone1_Clear_Click;
        // 
        // btnBackspace1
        // 
        btnBackspace1.Location = new Point(112, 677);
        btnBackspace1.Margin = new Padding(3, 4, 3, 4);
        btnBackspace1.Name = "btnBackspace1";
        btnBackspace1.Size = new Size(96, 56);
        btnBackspace1.TabIndex = 23;
        btnBackspace1.Text = "Borrar";
        btnBackspace1.UseVisualStyleBackColor = true;
        btnBackspace1.Click += Phone1_Backspace_Click;
        // 
        // btnKeyHash1
        // 
        btnKeyHash1.Font = new Font("Segoe UI Semibold", 20F, FontStyle.Bold);
        btnKeyHash1.Location = new Point(395, 578);
        btnKeyHash1.Margin = new Padding(3, 4, 3, 4);
        btnKeyHash1.Name = "btnKeyHash1";
        btnKeyHash1.Size = new Size(98, 77);
        btnKeyHash1.TabIndex = 22;
        btnKeyHash1.Text = "#";
        btnKeyHash1.UseVisualStyleBackColor = true;
        btnKeyHash1.Click += Phone1_Digit_Click;
        // 
        // btnKey0_1
        // 
        btnKey0_1.Font = new Font("Segoe UI Semibold", 20F, FontStyle.Bold);
        btnKey0_1.Location = new Point(252, 578);
        btnKey0_1.Margin = new Padding(3, 4, 3, 4);
        btnKey0_1.Name = "btnKey0_1";
        btnKey0_1.Size = new Size(98, 77);
        btnKey0_1.TabIndex = 21;
        btnKey0_1.Text = "0";
        btnKey0_1.UseVisualStyleBackColor = true;
        btnKey0_1.Click += Phone1_Digit_Click;
        // 
        // btnKeyStar1
        // 
        btnKeyStar1.Font = new Font("Segoe UI Semibold", 20F, FontStyle.Bold);
        btnKeyStar1.Location = new Point(110, 578);
        btnKeyStar1.Margin = new Padding(3, 4, 3, 4);
        btnKeyStar1.Name = "btnKeyStar1";
        btnKeyStar1.Size = new Size(98, 77);
        btnKeyStar1.TabIndex = 20;
        btnKeyStar1.Text = "*";
        btnKeyStar1.UseVisualStyleBackColor = true;
        btnKeyStar1.Click += Phone1_Digit_Click;
        // 
        // btnKey9_1
        // 
        btnKey9_1.Font = new Font("Segoe UI Semibold", 20F, FontStyle.Bold);
        btnKey9_1.Location = new Point(395, 478);
        btnKey9_1.Margin = new Padding(3, 4, 3, 4);
        btnKey9_1.Name = "btnKey9_1";
        btnKey9_1.Size = new Size(98, 77);
        btnKey9_1.TabIndex = 19;
        btnKey9_1.Text = "9";
        btnKey9_1.UseVisualStyleBackColor = true;
        btnKey9_1.Click += Phone1_Digit_Click;
        // 
        // btnKey8_1
        // 
        btnKey8_1.Font = new Font("Segoe UI Semibold", 20F, FontStyle.Bold);
        btnKey8_1.Location = new Point(252, 478);
        btnKey8_1.Margin = new Padding(3, 4, 3, 4);
        btnKey8_1.Name = "btnKey8_1";
        btnKey8_1.Size = new Size(98, 77);
        btnKey8_1.TabIndex = 18;
        btnKey8_1.Text = "8";
        btnKey8_1.UseVisualStyleBackColor = true;
        btnKey8_1.Click += Phone1_Digit_Click;
        // 
        // btnKey7_1
        // 
        btnKey7_1.Font = new Font("Segoe UI Semibold", 20F, FontStyle.Bold);
        btnKey7_1.Location = new Point(110, 478);
        btnKey7_1.Margin = new Padding(3, 4, 3, 4);
        btnKey7_1.Name = "btnKey7_1";
        btnKey7_1.Size = new Size(98, 77);
        btnKey7_1.TabIndex = 17;
        btnKey7_1.Text = "7";
        btnKey7_1.UseVisualStyleBackColor = true;
        btnKey7_1.Click += Phone1_Digit_Click;
        // 
        // btnKey6_1
        // 
        btnKey6_1.Font = new Font("Segoe UI Semibold", 20F, FontStyle.Bold);
        btnKey6_1.Location = new Point(395, 378);
        btnKey6_1.Margin = new Padding(3, 4, 3, 4);
        btnKey6_1.Name = "btnKey6_1";
        btnKey6_1.Size = new Size(98, 77);
        btnKey6_1.TabIndex = 16;
        btnKey6_1.Text = "6";
        btnKey6_1.UseVisualStyleBackColor = true;
        btnKey6_1.Click += Phone1_Digit_Click;
        // 
        // btnKey5_1
        // 
        btnKey5_1.Font = new Font("Segoe UI Semibold", 20F, FontStyle.Bold);
        btnKey5_1.Location = new Point(252, 378);
        btnKey5_1.Margin = new Padding(3, 4, 3, 4);
        btnKey5_1.Name = "btnKey5_1";
        btnKey5_1.Size = new Size(98, 77);
        btnKey5_1.TabIndex = 15;
        btnKey5_1.Text = "5";
        btnKey5_1.UseVisualStyleBackColor = true;
        btnKey5_1.Click += Phone1_Digit_Click;
        // 
        // btnKey4_1
        // 
        btnKey4_1.Font = new Font("Segoe UI Semibold", 20F, FontStyle.Bold);
        btnKey4_1.Location = new Point(110, 378);
        btnKey4_1.Margin = new Padding(3, 4, 3, 4);
        btnKey4_1.Name = "btnKey4_1";
        btnKey4_1.Size = new Size(98, 77);
        btnKey4_1.TabIndex = 14;
        btnKey4_1.Text = "4";
        btnKey4_1.UseVisualStyleBackColor = true;
        btnKey4_1.Click += Phone1_Digit_Click;
        // 
        // btnKey3_1
        // 
        btnKey3_1.Font = new Font("Segoe UI Semibold", 20F, FontStyle.Bold);
        btnKey3_1.Location = new Point(395, 278);
        btnKey3_1.Margin = new Padding(3, 4, 3, 4);
        btnKey3_1.Name = "btnKey3_1";
        btnKey3_1.Size = new Size(98, 77);
        btnKey3_1.TabIndex = 13;
        btnKey3_1.Text = "3";
        btnKey3_1.UseVisualStyleBackColor = true;
        btnKey3_1.Click += Phone1_Digit_Click;
        // 
        // btnKey2_1
        // 
        btnKey2_1.Font = new Font("Segoe UI Semibold", 20F, FontStyle.Bold);
        btnKey2_1.Location = new Point(252, 278);
        btnKey2_1.Margin = new Padding(3, 4, 3, 4);
        btnKey2_1.Name = "btnKey2_1";
        btnKey2_1.Size = new Size(98, 77);
        btnKey2_1.TabIndex = 12;
        btnKey2_1.Text = "2";
        btnKey2_1.UseVisualStyleBackColor = true;
        btnKey2_1.Click += Phone1_Digit_Click;
        // 
        // btnKey1_1
        // 
        btnKey1_1.Font = new Font("Segoe UI Semibold", 20F, FontStyle.Bold);
        btnKey1_1.Location = new Point(110, 278);
        btnKey1_1.Margin = new Padding(3, 4, 3, 4);
        btnKey1_1.Name = "btnKey1_1";
        btnKey1_1.Size = new Size(98, 77);
        btnKey1_1.TabIndex = 11;
        btnKey1_1.Text = "1";
        btnKey1_1.UseVisualStyleBackColor = true;
        btnKey1_1.Click += Phone1_Digit_Click;
        // 
        // txtDisplay1
        // 
        txtDisplay1.Font = new Font("Segoe UI Semibold", 24F, FontStyle.Bold);
        txtDisplay1.Location = new Point(81, 171);
        txtDisplay1.Margin = new Padding(3, 4, 3, 4);
        txtDisplay1.Name = "txtDisplay1";
        txtDisplay1.ReadOnly = true;
        txtDisplay1.Size = new Size(445, 61);
        txtDisplay1.TabIndex = 10;
        txtDisplay1.TextAlign = HorizontalAlignment.Center;
        // 
        // lblTitle1
        // 
        lblTitle1.AutoSize = true;
        lblTitle1.Font = new Font("Segoe UI Semibold", 22F, FontStyle.Bold);
        lblTitle1.Location = new Point(347, 37);
        lblTitle1.Name = "lblTitle1";
        lblTitle1.Size = new Size(192, 50);
        lblTitle1.TabIndex = 1;
        lblTitle1.Text = "Telefono 1";
        // 
        // lblClock1
        // 
        lblClock1.Font = new Font("Segoe UI Semibold", 11F, FontStyle.Bold);
        lblClock1.Location = new Point(262, 113);
        lblClock1.Name = "lblClock1";
        lblClock1.Size = new Size(88, 28);
        lblClock1.TabIndex = 0;
        lblClock1.Text = "00:00:00";
        // 
        // tabPhone2
        // 
        tabPhone2.BackColor = Color.White;
        tabPhone2.Controls.Add(lblStatus2);
        tabPhone2.Controls.Add(txtTariff2);
        tabPhone2.Controls.Add(txtTime2);
        tabPhone2.Controls.Add(txtDestination2);
        tabPhone2.Controls.Add(lblTariffTitle2);
        tabPhone2.Controls.Add(lblTimeTitle2);
        tabPhone2.Controls.Add(lblDestinationTitle2);
        tabPhone2.Controls.Add(lblTimer2);
        tabPhone2.Controls.Add(lblTimerTitle2);
        tabPhone2.Controls.Add(btnHang2);
        tabPhone2.Controls.Add(btnCall2);
        tabPhone2.Controls.Add(btnBalance2);
        tabPhone2.Controls.Add(btnClear2);
        tabPhone2.Controls.Add(btnBackspace2);
        tabPhone2.Controls.Add(btnKeyHash2);
        tabPhone2.Controls.Add(btnKey0_2);
        tabPhone2.Controls.Add(btnKeyStar2);
        tabPhone2.Controls.Add(btnKey9_2);
        tabPhone2.Controls.Add(btnKey8_2);
        tabPhone2.Controls.Add(btnKey7_2);
        tabPhone2.Controls.Add(btnKey6_2);
        tabPhone2.Controls.Add(btnKey5_2);
        tabPhone2.Controls.Add(btnKey4_2);
        tabPhone2.Controls.Add(btnKey3_2);
        tabPhone2.Controls.Add(btnKey2_2);
        tabPhone2.Controls.Add(btnKey1_2);
        tabPhone2.Controls.Add(txtDisplay2);
        tabPhone2.Controls.Add(lblTitle2);
        tabPhone2.Controls.Add(lblClock2);
        tabPhone2.Location = new Point(4, 32);
        tabPhone2.Margin = new Padding(3, 4, 3, 4);
        tabPhone2.Name = "tabPhone2";
        tabPhone2.Padding = new Padding(21, 24, 21, 24);
        tabPhone2.Size = new Size(970, 1019);
        tabPhone2.TabIndex = 1;
        tabPhone2.Text = "Telefono 2";
        // 
        // lblStatus2
        // 
        lblStatus2.BorderStyle = BorderStyle.FixedSingle;
        lblStatus2.Location = new Point(586, 592);
        lblStatus2.Name = "lblStatus2";
        lblStatus2.Size = new Size(343, 173);
        lblStatus2.TabIndex = 36;
        lblStatus2.Text = "Ver saldo";
        lblStatus2.TextAlign = ContentAlignment.MiddleCenter;
        // 
        // txtTariff2
        // 
        txtTariff2.Location = new Point(724, 492);
        txtTariff2.Margin = new Padding(3, 4, 3, 4);
        txtTariff2.Name = "txtTariff2";
        txtTariff2.ReadOnly = true;
        txtTariff2.Size = new Size(205, 30);
        txtTariff2.TabIndex = 35;
        // 
        // txtTime2
        // 
        txtTime2.Location = new Point(724, 432);
        txtTime2.Margin = new Padding(3, 4, 3, 4);
        txtTime2.Name = "txtTime2";
        txtTime2.ReadOnly = true;
        txtTime2.Size = new Size(205, 30);
        txtTime2.TabIndex = 33;
        // 
        // txtDestination2
        // 
        txtDestination2.Location = new Point(724, 372);
        txtDestination2.Margin = new Padding(3, 4, 3, 4);
        txtDestination2.Name = "txtDestination2";
        txtDestination2.ReadOnly = true;
        txtDestination2.Size = new Size(205, 30);
        txtDestination2.TabIndex = 31;
        // 
        // lblTariffTitle2
        // 
        lblTariffTitle2.AutoSize = true;
        lblTariffTitle2.Location = new Point(592, 499);
        lblTariffTitle2.Name = "lblTariffTitle2";
        lblTariffTitle2.Size = new Size(50, 23);
        lblTariffTitle2.TabIndex = 34;
        lblTariffTitle2.Text = "Tarifa";
        // 
        // lblTimeTitle2
        // 
        lblTimeTitle2.AutoSize = true;
        lblTimeTitle2.Location = new Point(592, 439);
        lblTimeTitle2.Name = "lblTimeTitle2";
        lblTimeTitle2.Size = new Size(133, 23);
        lblTimeTitle2.TabIndex = 32;
        lblTimeTitle2.Text = "Tiempo maximo";
        // 
        // lblDestinationTitle2
        // 
        lblDestinationTitle2.AutoSize = true;
        lblDestinationTitle2.Location = new Point(592, 379);
        lblDestinationTitle2.Name = "lblDestinationTitle2";
        lblDestinationTitle2.Size = new Size(68, 23);
        lblDestinationTitle2.TabIndex = 30;
        lblDestinationTitle2.Text = "Destino";
        // 
        // lblTimer2
        // 
        lblTimer2.Font = new Font("Segoe UI Semibold", 32F, FontStyle.Bold);
        lblTimer2.Location = new Point(597, 221);
        lblTimer2.Name = "lblTimer2";
        lblTimer2.Size = new Size(274, 107);
        lblTimer2.TabIndex = 29;
        lblTimer2.Text = "00:00";
        lblTimer2.TextAlign = ContentAlignment.MiddleCenter;
        // 
        // lblTimerTitle2
        // 
        lblTimerTitle2.AutoSize = true;
        lblTimerTitle2.Location = new Point(670, 203);
        lblTimerTitle2.Name = "lblTimerTitle2";
        lblTimerTitle2.Size = new Size(118, 23);
        lblTimerTitle2.TabIndex = 28;
        lblTimerTitle2.Text = "Tiempo actual";
        // 
        // btnHang2
        // 
        btnHang2.BackColor = Color.FromArgb(224, 49, 49);
        btnHang2.FlatStyle = FlatStyle.Flat;
        btnHang2.ForeColor = Color.White;
        btnHang2.Location = new Point(226, 769);
        btnHang2.Margin = new Padding(3, 4, 3, 4);
        btnHang2.Name = "btnHang2";
        btnHang2.Size = new Size(177, 69);
        btnHang2.TabIndex = 27;
        btnHang2.Text = "Colgar";
        btnHang2.UseVisualStyleBackColor = false;
        btnHang2.Visible = false;
        btnHang2.Click += Phone2_Hang_Click;
        // 
        // btnCall2
        // 
        btnCall2.BackColor = Color.FromArgb(35, 173, 96);
        btnCall2.FlatStyle = FlatStyle.Flat;
        btnCall2.ForeColor = Color.White;
        btnCall2.Location = new Point(227, 769);
        btnCall2.Margin = new Padding(3, 4, 3, 4);
        btnCall2.Name = "btnCall2";
        btnCall2.Size = new Size(177, 69);
        btnCall2.TabIndex = 26;
        btnCall2.Text = "Llamar";
        btnCall2.UseVisualStyleBackColor = false;
        btnCall2.Click += Phone2_Call_Click;
        // 
        // btnBalance2
        // 
        btnBalance2.Location = new Point(397, 685);
        btnBalance2.Margin = new Padding(3, 4, 3, 4);
        btnBalance2.Name = "btnBalance2";
        btnBalance2.Size = new Size(98, 56);
        btnBalance2.TabIndex = 25;
        btnBalance2.Text = "Saldo";
        btnBalance2.UseVisualStyleBackColor = true;
        btnBalance2.Click += Phone2_Balance_Click;
        // 
        // btnClear2
        // 
        btnClear2.Location = new Point(254, 685);
        btnClear2.Margin = new Padding(3, 4, 3, 4);
        btnClear2.Name = "btnClear2";
        btnClear2.Size = new Size(98, 56);
        btnClear2.TabIndex = 24;
        btnClear2.Text = "Limpiar";
        btnClear2.UseVisualStyleBackColor = true;
        btnClear2.Click += Phone2_Clear_Click;
        // 
        // btnBackspace2
        // 
        btnBackspace2.Location = new Point(112, 685);
        btnBackspace2.Margin = new Padding(3, 4, 3, 4);
        btnBackspace2.Name = "btnBackspace2";
        btnBackspace2.Size = new Size(98, 56);
        btnBackspace2.TabIndex = 23;
        btnBackspace2.Text = "Borrar";
        btnBackspace2.UseVisualStyleBackColor = true;
        btnBackspace2.Click += Phone2_Backspace_Click;
        // 
        // btnKeyHash2
        // 
        btnKeyHash2.Font = new Font("Segoe UI Semibold", 20F, FontStyle.Bold);
        btnKeyHash2.Location = new Point(397, 572);
        btnKeyHash2.Margin = new Padding(3, 4, 3, 4);
        btnKeyHash2.Name = "btnKeyHash2";
        btnKeyHash2.Size = new Size(98, 77);
        btnKeyHash2.TabIndex = 22;
        btnKeyHash2.Text = "#";
        btnKeyHash2.UseVisualStyleBackColor = true;
        btnKeyHash2.Click += Phone2_Digit_Click;
        // 
        // btnKey0_2
        // 
        btnKey0_2.Font = new Font("Segoe UI Semibold", 20F, FontStyle.Bold);
        btnKey0_2.Location = new Point(254, 572);
        btnKey0_2.Margin = new Padding(3, 4, 3, 4);
        btnKey0_2.Name = "btnKey0_2";
        btnKey0_2.Size = new Size(98, 77);
        btnKey0_2.TabIndex = 21;
        btnKey0_2.Text = "0";
        btnKey0_2.UseVisualStyleBackColor = true;
        btnKey0_2.Click += Phone2_Digit_Click;
        // 
        // btnKeyStar2
        // 
        btnKeyStar2.Font = new Font("Segoe UI Semibold", 20F, FontStyle.Bold);
        btnKeyStar2.Location = new Point(112, 572);
        btnKeyStar2.Margin = new Padding(3, 4, 3, 4);
        btnKeyStar2.Name = "btnKeyStar2";
        btnKeyStar2.Size = new Size(98, 77);
        btnKeyStar2.TabIndex = 20;
        btnKeyStar2.Text = "*";
        btnKeyStar2.UseVisualStyleBackColor = true;
        btnKeyStar2.Click += Phone2_Digit_Click;
        // 
        // btnKey9_2
        // 
        btnKey9_2.Font = new Font("Segoe UI Semibold", 20F, FontStyle.Bold);
        btnKey9_2.Location = new Point(397, 472);
        btnKey9_2.Margin = new Padding(3, 4, 3, 4);
        btnKey9_2.Name = "btnKey9_2";
        btnKey9_2.Size = new Size(98, 77);
        btnKey9_2.TabIndex = 19;
        btnKey9_2.Text = "9";
        btnKey9_2.UseVisualStyleBackColor = true;
        btnKey9_2.Click += Phone2_Digit_Click;
        // 
        // btnKey8_2
        // 
        btnKey8_2.Font = new Font("Segoe UI Semibold", 20F, FontStyle.Bold);
        btnKey8_2.Location = new Point(254, 472);
        btnKey8_2.Margin = new Padding(3, 4, 3, 4);
        btnKey8_2.Name = "btnKey8_2";
        btnKey8_2.Size = new Size(98, 77);
        btnKey8_2.TabIndex = 18;
        btnKey8_2.Text = "8";
        btnKey8_2.UseVisualStyleBackColor = true;
        btnKey8_2.Click += Phone2_Digit_Click;
        // 
        // btnKey7_2
        // 
        btnKey7_2.Font = new Font("Segoe UI Semibold", 20F, FontStyle.Bold);
        btnKey7_2.Location = new Point(112, 472);
        btnKey7_2.Margin = new Padding(3, 4, 3, 4);
        btnKey7_2.Name = "btnKey7_2";
        btnKey7_2.Size = new Size(98, 77);
        btnKey7_2.TabIndex = 17;
        btnKey7_2.Text = "7";
        btnKey7_2.UseVisualStyleBackColor = true;
        btnKey7_2.Click += Phone2_Digit_Click;
        // 
        // btnKey6_2
        // 
        btnKey6_2.Font = new Font("Segoe UI Semibold", 20F, FontStyle.Bold);
        btnKey6_2.Location = new Point(397, 372);
        btnKey6_2.Margin = new Padding(3, 4, 3, 4);
        btnKey6_2.Name = "btnKey6_2";
        btnKey6_2.Size = new Size(98, 77);
        btnKey6_2.TabIndex = 16;
        btnKey6_2.Text = "6";
        btnKey6_2.UseVisualStyleBackColor = true;
        btnKey6_2.Click += Phone2_Digit_Click;
        // 
        // btnKey5_2
        // 
        btnKey5_2.Font = new Font("Segoe UI Semibold", 20F, FontStyle.Bold);
        btnKey5_2.Location = new Point(254, 372);
        btnKey5_2.Margin = new Padding(3, 4, 3, 4);
        btnKey5_2.Name = "btnKey5_2";
        btnKey5_2.Size = new Size(98, 77);
        btnKey5_2.TabIndex = 15;
        btnKey5_2.Text = "5";
        btnKey5_2.UseVisualStyleBackColor = true;
        btnKey5_2.Click += Phone2_Digit_Click;
        // 
        // btnKey4_2
        // 
        btnKey4_2.Font = new Font("Segoe UI Semibold", 20F, FontStyle.Bold);
        btnKey4_2.Location = new Point(112, 372);
        btnKey4_2.Margin = new Padding(3, 4, 3, 4);
        btnKey4_2.Name = "btnKey4_2";
        btnKey4_2.Size = new Size(98, 77);
        btnKey4_2.TabIndex = 14;
        btnKey4_2.Text = "4";
        btnKey4_2.UseVisualStyleBackColor = true;
        btnKey4_2.Click += Phone2_Digit_Click;
        // 
        // btnKey3_2
        // 
        btnKey3_2.Font = new Font("Segoe UI Semibold", 20F, FontStyle.Bold);
        btnKey3_2.Location = new Point(397, 272);
        btnKey3_2.Margin = new Padding(3, 4, 3, 4);
        btnKey3_2.Name = "btnKey3_2";
        btnKey3_2.Size = new Size(98, 77);
        btnKey3_2.TabIndex = 13;
        btnKey3_2.Text = "3";
        btnKey3_2.UseVisualStyleBackColor = true;
        btnKey3_2.Click += Phone2_Digit_Click;
        // 
        // btnKey2_2
        // 
        btnKey2_2.Font = new Font("Segoe UI Semibold", 20F, FontStyle.Bold);
        btnKey2_2.Location = new Point(254, 272);
        btnKey2_2.Margin = new Padding(3, 4, 3, 4);
        btnKey2_2.Name = "btnKey2_2";
        btnKey2_2.Size = new Size(98, 77);
        btnKey2_2.TabIndex = 12;
        btnKey2_2.Text = "2";
        btnKey2_2.UseVisualStyleBackColor = true;
        btnKey2_2.Click += Phone2_Digit_Click;
        // 
        // btnKey1_2
        // 
        btnKey1_2.Font = new Font("Segoe UI Semibold", 20F, FontStyle.Bold);
        btnKey1_2.Location = new Point(112, 272);
        btnKey1_2.Margin = new Padding(3, 4, 3, 4);
        btnKey1_2.Name = "btnKey1_2";
        btnKey1_2.Size = new Size(98, 77);
        btnKey1_2.TabIndex = 11;
        btnKey1_2.Text = "1";
        btnKey1_2.UseVisualStyleBackColor = true;
        btnKey1_2.Click += Phone2_Digit_Click;
        // 
        // txtDisplay2
        // 
        txtDisplay2.Font = new Font("Segoe UI Semibold", 24F, FontStyle.Bold);
        txtDisplay2.Location = new Point(83, 165);
        txtDisplay2.Margin = new Padding(3, 4, 3, 4);
        txtDisplay2.Name = "txtDisplay2";
        txtDisplay2.ReadOnly = true;
        txtDisplay2.Size = new Size(445, 61);
        txtDisplay2.TabIndex = 10;
        txtDisplay2.TextAlign = HorizontalAlignment.Center;
        // 
        // lblTitle2
        // 
        lblTitle2.AutoSize = true;
        lblTitle2.Font = new Font("Segoe UI Semibold", 22F, FontStyle.Bold);
        lblTitle2.Location = new Point(347, 37);
        lblTitle2.Name = "lblTitle2";
        lblTitle2.Size = new Size(198, 50);
        lblTitle2.TabIndex = 1;
        lblTitle2.Text = "Telefono 2";
        // 
        // lblClock2
        // 
        lblClock2.AutoSize = true;
        lblClock2.Font = new Font("Segoe UI Semibold", 11F, FontStyle.Bold);
        lblClock2.Location = new Point(264, 107);
        lblClock2.Name = "lblClock2";
        lblClock2.Size = new Size(88, 25);
        lblClock2.TabIndex = 0;
        lblClock2.Text = "00:00:00";
        // 
        // groupConnection
        // 
        groupConnection.Controls.Add(txtResponse);
        groupConnection.Controls.Add(lblResponse);
        groupConnection.Controls.Add(txtRaw);
        groupConnection.Controls.Add(lblRaw);
        groupConnection.Controls.Add(btnTestConnection);
        groupConnection.Controls.Add(chkEncrypt);
        groupConnection.Controls.Add(numPort);
        groupConnection.Controls.Add(lblPort);
        groupConnection.Controls.Add(txtHost);
        groupConnection.Controls.Add(lblHost);
        groupConnection.Controls.Add(txtCardId1);
        groupConnection.Controls.Add(lblPhoneTitle1);
        groupConnection.Controls.Add(lblCardTitle1);
        groupConnection.Controls.Add(txtPhone1);
        groupConnection.Controls.Add(txtLocation1);
        groupConnection.Controls.Add(lblDeviceTitle1);
        groupConnection.Controls.Add(lblLocationTitle1);
        groupConnection.Controls.Add(txtDeviceId1);
        groupConnection.Controls.Add(txtPhone2);
        groupConnection.Controls.Add(lblPhoneTitle2);
        groupConnection.Controls.Add(txtDeviceId2);
        groupConnection.Controls.Add(lblDeviceTitle2);
        groupConnection.Controls.Add(txtCardId2);
        groupConnection.Controls.Add(lblCardTitle2);
        groupConnection.Controls.Add(txtLocation2);
        groupConnection.Controls.Add(lblLocationTitle2);
        groupConnection.Dock = DockStyle.Fill;
        groupConnection.Font = new Font("Segoe UI", 10F);
        groupConnection.Location = new Point(0, 0);
        groupConnection.Margin = new Padding(3, 4, 3, 4);
        groupConnection.Name = "groupConnection";
        groupConnection.Padding = new Padding(21, 24, 21, 24);
        groupConnection.Size = new Size(481, 1055);
        groupConnection.TabIndex = 0;
        groupConnection.TabStop = false;
        groupConnection.Text = "Conexion y tramas";
        groupConnection.Enter += groupConnection_Enter;
        // 
        // txtResponse
        // 
        txtResponse.Font = new Font("Consolas", 9F);
        txtResponse.Location = new Point(24, 761);
        txtResponse.Margin = new Padding(3, 4, 3, 4);
        txtResponse.Multiline = true;
        txtResponse.Name = "txtResponse";
        txtResponse.ReadOnly = true;
        txtResponse.ScrollBars = ScrollBars.Vertical;
        txtResponse.Size = new Size(394, 275);
        txtResponse.TabIndex = 9;
        // 
        // lblResponse
        // 
        lblResponse.AutoSize = true;
        lblResponse.Location = new Point(24, 734);
        lblResponse.Name = "lblResponse";
        lblResponse.Size = new Size(152, 23);
        lblResponse.TabIndex = 8;
        lblResponse.Text = "Respuesta recibida";
        // 
        // txtRaw
        // 
        txtRaw.Font = new Font("Consolas", 9F);
        txtRaw.Location = new Point(24, 502);
        txtRaw.Margin = new Padding(3, 4, 3, 4);
        txtRaw.Multiline = true;
        txtRaw.Name = "txtRaw";
        txtRaw.ReadOnly = true;
        txtRaw.ScrollBars = ScrollBars.Vertical;
        txtRaw.Size = new Size(394, 228);
        txtRaw.TabIndex = 7;
        // 
        // lblRaw
        // 
        lblRaw.AutoSize = true;
        lblRaw.Location = new Point(24, 469);
        lblRaw.Name = "lblRaw";
        lblRaw.Size = new Size(121, 23);
        lblRaw.TabIndex = 6;
        lblRaw.Text = "Trama enviada";
        // 
        // btnTestConnection
        // 
        btnTestConnection.Location = new Point(34, 227);
        btnTestConnection.Margin = new Padding(3, 4, 3, 4);
        btnTestConnection.Name = "btnTestConnection";
        btnTestConnection.Size = new Size(394, 53);
        btnTestConnection.TabIndex = 5;
        btnTestConnection.Text = "Probar con saldo del telefono 1";
        btnTestConnection.UseVisualStyleBackColor = true;
        btnTestConnection.Click += btnTestConnection_Click;
        // 
        // chkEncrypt
        // 
        chkEncrypt.AutoSize = true;
        chkEncrypt.Checked = true;
        chkEncrypt.CheckState = CheckState.Checked;
        chkEncrypt.Location = new Point(34, 167);
        chkEncrypt.Margin = new Padding(3, 4, 3, 4);
        chkEncrypt.Name = "chkEncrypt";
        chkEncrypt.Size = new Size(369, 27);
        chkEncrypt.TabIndex = 4;
        chkEncrypt.Text = "Enviar telefono, dispositivo y tarjeta con AES";
        chkEncrypt.UseVisualStyleBackColor = true;
        // 
        // numPort
        // 
        numPort.Location = new Point(166, 105);
        numPort.Margin = new Padding(3, 4, 3, 4);
        numPort.Maximum = new decimal(new int[] { 65535, 0, 0, 0 });
        numPort.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
        numPort.Name = "numPort";
        numPort.Size = new Size(263, 30);
        numPort.TabIndex = 3;
        numPort.Value = new decimal(new int[] { 8000, 0, 0, 0 });
        // 
        // lblPort
        // 
        lblPort.AutoSize = true;
        lblPort.Location = new Point(34, 109);
        lblPort.Name = "lblPort";
        lblPort.Size = new Size(120, 23);
        lblPort.TabIndex = 2;
        lblPort.Text = "Puerto Python";
        // 
        // txtHost
        // 
        txtHost.Location = new Point(166, 52);
        txtHost.Margin = new Padding(3, 4, 3, 4);
        txtHost.Name = "txtHost";
        txtHost.Size = new Size(262, 30);
        txtHost.TabIndex = 1;
        txtHost.Text = "localhost";
        // 
        // lblHost
        // 
        lblHost.AutoSize = true;
        lblHost.Location = new Point(34, 56);
        lblHost.Name = "lblHost";
        lblHost.Size = new Size(104, 23);
        lblHost.TabIndex = 0;
        lblHost.Text = "Host Python";
        // 
        // txtCardId1
        // 
        txtCardId1.Location = new Point(142, 344);
        txtCardId1.Margin = new Padding(3, 4, 3, 4);
        txtCardId1.Name = "txtCardId1";
        txtCardId1.Size = new Size(182, 30);
        txtCardId1.TabIndex = 7;
        txtCardId1.Text = "1234567890123456789";
        txtCardId1.TextChanged += txtCardId1_TextChanged;
        // 
        // lblPhoneTitle1
        // 
        lblPhoneTitle1.AutoSize = true;
        lblPhoneTitle1.Location = new Point(34, 307);
        lblPhoneTitle1.Name = "lblPhoneTitle1";
        lblPhoneTitle1.Size = new Size(74, 23);
        lblPhoneTitle1.TabIndex = 2;
        lblPhoneTitle1.Text = "Telefono";
        // 
        // lblCardTitle1
        // 
        lblCardTitle1.AutoSize = true;
        lblCardTitle1.Location = new Point(34, 351);
        lblCardTitle1.Name = "lblCardTitle1";
        lblCardTitle1.Size = new Size(81, 23);
        lblCardTitle1.TabIndex = 6;
        lblCardTitle1.Text = "ID tarjeta";
        // 
        // txtPhone1
        // 
        txtPhone1.Location = new Point(142, 304);
        txtPhone1.Margin = new Padding(3, 4, 3, 4);
        txtPhone1.Name = "txtPhone1";
        txtPhone1.Size = new Size(182, 30);
        txtPhone1.TabIndex = 3;
        txtPhone1.Text = "88889999";
        txtPhone1.TextChanged += txtPhone1_TextChanged;
        // 
        // txtLocation1
        // 
        txtLocation1.Location = new Point(142, 429);
        txtLocation1.Margin = new Padding(3, 4, 3, 4);
        txtLocation1.Name = "txtLocation1";
        txtLocation1.Size = new Size(182, 30);
        txtLocation1.TabIndex = 9;
        txtLocation1.Text = "9.935,-84.091";
        // 
        // lblDeviceTitle1
        // 
        lblDeviceTitle1.AutoSize = true;
        lblDeviceTitle1.Location = new Point(34, 394);
        lblDeviceTitle1.Name = "lblDeviceTitle1";
        lblDeviceTitle1.Size = new Size(95, 23);
        lblDeviceTitle1.TabIndex = 4;
        lblDeviceTitle1.Text = "ID telefono";
        // 
        // lblLocationTitle1
        // 
        lblLocationTitle1.AutoSize = true;
        lblLocationTitle1.Location = new Point(34, 432);
        lblLocationTitle1.Name = "lblLocationTitle1";
        lblLocationTitle1.Size = new Size(85, 23);
        lblLocationTitle1.TabIndex = 8;
        lblLocationTitle1.Text = "Ubicacion";
        // 
        // txtDeviceId1
        // 
        txtDeviceId1.Location = new Point(142, 387);
        txtDeviceId1.Margin = new Padding(3, 4, 3, 4);
        txtDeviceId1.Name = "txtDeviceId1";
        txtDeviceId1.Size = new Size(182, 30);
        txtDeviceId1.TabIndex = 5;
        txtDeviceId1.Text = "1234567890123456";
        // 
        // txtPhone2
        // 
        txtPhone2.Location = new Point(142, 544);
        txtPhone2.Margin = new Padding(3, 4, 3, 4);
        txtPhone2.Name = "txtPhone2";
        txtPhone2.Size = new Size(182, 30);
        txtPhone2.TabIndex = 10;
        txtPhone2.Text = "25743715";
        // 
        // lblPhoneTitle2
        // 
        lblPhoneTitle2.AutoSize = true;
        lblPhoneTitle2.Location = new Point(34, 547);
        lblPhoneTitle2.Name = "lblPhoneTitle2";
        lblPhoneTitle2.Size = new Size(88, 23);
        lblPhoneTitle2.TabIndex = 11;
        lblPhoneTitle2.Text = "Telefono 2";
        // 
        // txtDeviceId2
        // 
        txtDeviceId2.Location = new Point(142, 627);
        txtDeviceId2.Margin = new Padding(3, 4, 3, 4);
        txtDeviceId2.Name = "txtDeviceId2";
        txtDeviceId2.Size = new Size(182, 30);
        txtDeviceId2.TabIndex = 14;
        txtDeviceId2.Text = "3333333333333333";
        // 
        // lblDeviceTitle2
        // 
        lblDeviceTitle2.AutoSize = true;
        lblDeviceTitle2.Location = new Point(34, 634);
        lblDeviceTitle2.Name = "lblDeviceTitle2";
        lblDeviceTitle2.Size = new Size(95, 23);
        lblDeviceTitle2.TabIndex = 15;
        lblDeviceTitle2.Text = "ID telefono";
        // 
        // txtCardId2
        // 
        txtCardId2.Location = new Point(142, 584);
        txtCardId2.Margin = new Padding(3, 4, 3, 4);
        txtCardId2.Name = "txtCardId2";
        txtCardId2.Size = new Size(182, 30);
        txtCardId2.TabIndex = 12;
        txtCardId2.Text = "3333333333333333333";
        // 
        // lblCardTitle2
        // 
        lblCardTitle2.AutoSize = true;
        lblCardTitle2.Location = new Point(34, 591);
        lblCardTitle2.Name = "lblCardTitle2";
        lblCardTitle2.Size = new Size(81, 23);
        lblCardTitle2.TabIndex = 13;
        lblCardTitle2.Text = "ID tarjeta";
        // 
        // txtLocation2
        // 
        txtLocation2.Location = new Point(142, 669);
        txtLocation2.Margin = new Padding(3, 4, 3, 4);
        txtLocation2.Name = "txtLocation2";
        txtLocation2.Size = new Size(182, 30);
        txtLocation2.TabIndex = 16;
        txtLocation2.Text = "9.935,-84.091";
        // 
        // lblLocationTitle2
        // 
        lblLocationTitle2.AutoSize = true;
        lblLocationTitle2.Location = new Point(34, 672);
        lblLocationTitle2.Name = "lblLocationTitle2";
        lblLocationTitle2.Size = new Size(85, 23);
        lblLocationTitle2.TabIndex = 17;
        lblLocationTitle2.Text = "Ubicacion";
        // 
        // uiTimer
        // 
        uiTimer.Interval = 500;
        uiTimer.Tick += uiTimer_Tick;
        // 
        // MainForm
        // 
        AutoScaleDimensions = new SizeF(8F, 20F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(1463, 1055);
        Controls.Add(splitMain);
        Margin = new Padding(3, 4, 3, 4);
        MinimumSize = new Size(1346, 998);
        Name = "MainForm";
        StartPosition = FormStartPosition.CenterScreen;
        Text = "Simulador de llamadas - SIM1";
        splitMain.Panel1.ResumeLayout(false);
        splitMain.Panel2.ResumeLayout(false);
        ((System.ComponentModel.ISupportInitialize)splitMain).EndInit();
        splitMain.ResumeLayout(false);
        tabsPhones.ResumeLayout(false);
        tabPhone1.ResumeLayout(false);
        tabPhone1.PerformLayout();
        tabPhone2.ResumeLayout(false);
        tabPhone2.PerformLayout();
        groupConnection.ResumeLayout(false);
        groupConnection.PerformLayout();
        ((System.ComponentModel.ISupportInitialize)numPort).EndInit();
        ResumeLayout(false);
    }
}

