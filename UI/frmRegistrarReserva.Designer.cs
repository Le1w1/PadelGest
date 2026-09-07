namespace UI
{
    partial class frmRegistrarReserva
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }

            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            lblTitulo = new Label();
            gbDatos = new GroupBox();
            lblImporteValor = new Label();
            lblImporteTitulo = new Label();
            lblEquipamientoValor = new Label();
            lblEquipamientoTitulo = new Label();
            lblTarifaValor = new Label();
            lblTarifaTitulo = new Label();
            lblCanchaValor = new Label();
            lblCanchaTitulo = new Label();
            lblHorarioValor = new Label();
            lblHorarioTitulo = new Label();
            lblFechaValor = new Label();
            lblFechaTitulo = new Label();
            lblClienteValor = new Label();
            lblClienteTitulo = new Label();
            lblMensaje = new Label();
            btnRegistrar = new Button();
            btnVolver = new Button();
            gbDatos.SuspendLayout();
            SuspendLayout();
            // lblTitulo
            lblTitulo.Font = new Font("Segoe UI", 20F, FontStyle.Bold);
            lblTitulo.Location = new Point(20, 18);
            lblTitulo.Name = "lblTitulo";
            lblTitulo.Size = new Size(700, 45);
            lblTitulo.Text = "Registrar Reserva";
            lblTitulo.TextAlign = ContentAlignment.MiddleCenter;
            // gbDatos
            gbDatos.BackColor = Color.LightSkyBlue;
            gbDatos.Controls.Add(lblImporteValor);
            gbDatos.Controls.Add(lblImporteTitulo);
            gbDatos.Controls.Add(lblEquipamientoValor);
            gbDatos.Controls.Add(lblEquipamientoTitulo);
            gbDatos.Controls.Add(lblTarifaValor);
            gbDatos.Controls.Add(lblTarifaTitulo);
            gbDatos.Controls.Add(lblCanchaValor);
            gbDatos.Controls.Add(lblCanchaTitulo);
            gbDatos.Controls.Add(lblHorarioValor);
            gbDatos.Controls.Add(lblHorarioTitulo);
            gbDatos.Controls.Add(lblFechaValor);
            gbDatos.Controls.Add(lblFechaTitulo);
            gbDatos.Controls.Add(lblClienteValor);
            gbDatos.Controls.Add(lblClienteTitulo);
            gbDatos.Location = new Point(20, 80);
            gbDatos.Name = "gbDatos";
            gbDatos.Size = new Size(700, 275);
            gbDatos.TabStop = false;
            gbDatos.Text = "Datos de la Reserva";
            // Cliente
            lblClienteTitulo.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblClienteTitulo.Location = new Point(30, 35);
            lblClienteTitulo.Size = new Size(100, 23);
            lblClienteTitulo.Text = "Cliente:";
            lblClienteValor.Location = new Point(140, 35);
            lblClienteValor.Size = new Size(520, 23);
            lblClienteValor.Text = "-";
            // Fecha
            lblFechaTitulo.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblFechaTitulo.Location = new Point(30, 75);
            lblFechaTitulo.Size = new Size(100, 23);
            lblFechaTitulo.Text = "Fecha:";
            lblFechaValor.Location = new Point(140, 75);
            lblFechaValor.Size = new Size(180, 23);
            lblFechaValor.Text = "-";
            // Horario
            lblHorarioTitulo.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblHorarioTitulo.Location = new Point(360, 75);
            lblHorarioTitulo.Size = new Size(90, 23);
            lblHorarioTitulo.Text = "Horario:";
            lblHorarioValor.Location = new Point(460, 75);
            lblHorarioValor.Size = new Size(200, 23);
            lblHorarioValor.Text = "-";
            // Cancha
            lblCanchaTitulo.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblCanchaTitulo.Location = new Point(30, 115);
            lblCanchaTitulo.Size = new Size(100, 23);
            lblCanchaTitulo.Text = "Cancha:";
            lblCanchaValor.Location = new Point(140, 115);
            lblCanchaValor.Size = new Size(180, 23);
            lblCanchaValor.Text = "-";
            // Tarifa
            lblTarifaTitulo.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblTarifaTitulo.Location = new Point(360, 115);
            lblTarifaTitulo.Size = new Size(90, 23);
            lblTarifaTitulo.Text = "Tarifa:";
            lblTarifaValor.Location = new Point(460, 115);
            lblTarifaValor.Size = new Size(200, 23);
            lblTarifaValor.Text = "-";
            // Equipamiento
            lblEquipamientoTitulo.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblEquipamientoTitulo.Location = new Point(30, 155);
            lblEquipamientoTitulo.Size = new Size(105, 23);
            lblEquipamientoTitulo.Text = "Equipamiento:";
            lblEquipamientoValor.Location = new Point(140, 155);
            lblEquipamientoValor.Size = new Size(520, 23);
            lblEquipamientoValor.Text = "-";
            // Importe
            lblImporteTitulo.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            lblImporteTitulo.Location = new Point(420, 210);
            lblImporteTitulo.Size = new Size(110, 28);
            lblImporteTitulo.Text = "Abonado:";
            lblImporteValor.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            lblImporteValor.Location = new Point(535, 210);
            lblImporteValor.Size = new Size(125, 28);
            lblImporteValor.Text = "-";
            lblImporteValor.TextAlign = ContentAlignment.MiddleRight;
            // Mensaje
            lblMensaje.ForeColor = Color.DarkRed;
            lblMensaje.Location = new Point(20, 370);
            lblMensaje.Name = "lblMensaje";
            lblMensaje.Size = new Size(700, 45);
            lblMensaje.TextAlign = ContentAlignment.MiddleLeft;
            // Registrar
            btnRegistrar.Location = new Point(560, 430);
            btnRegistrar.Name = "btnRegistrar";
            btnRegistrar.Size = new Size(160, 34);
            btnRegistrar.Text = "Registrar Reserva";
            btnRegistrar.UseVisualStyleBackColor = true;
            btnRegistrar.Click += btnRegistrar_Click;
            // Volver
            btnVolver.Location = new Point(440, 430);
            btnVolver.Name = "btnVolver";
            btnVolver.Size = new Size(100, 34);
            btnVolver.Text = "Volver";
            btnVolver.UseVisualStyleBackColor = true;
            btnVolver.Click += btnVolver_Click;
            // Form
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.WhiteSmoke;
            ClientSize = new Size(740, 485);
            Controls.Add(btnVolver);
            Controls.Add(btnRegistrar);
            Controls.Add(lblMensaje);
            Controls.Add(gbDatos);
            Controls.Add(lblTitulo);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "frmRegistrarReserva";
            StartPosition = FormStartPosition.CenterParent;
            Text = "PadelGest - Registrar Reserva";
            FormClosed += frmRegistrarReserva_FormClosed;
            Load += frmRegistrarReserva_Load;
            gbDatos.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Label lblTitulo;
        private GroupBox gbDatos;
        private Label lblClienteTitulo;
        private Label lblClienteValor;
        private Label lblFechaTitulo;
        private Label lblFechaValor;
        private Label lblHorarioTitulo;
        private Label lblHorarioValor;
        private Label lblCanchaTitulo;
        private Label lblCanchaValor;
        private Label lblTarifaTitulo;
        private Label lblTarifaValor;
        private Label lblEquipamientoTitulo;
        private Label lblEquipamientoValor;
        private Label lblImporteTitulo;
        private Label lblImporteValor;
        private Label lblMensaje;
        private Button btnRegistrar;
        private Button btnVolver;
    }
}
