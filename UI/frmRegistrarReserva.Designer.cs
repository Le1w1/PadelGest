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
            // 
            // lblTitulo
            // 
            lblTitulo.Font = new Font("Segoe UI", 18F, FontStyle.Bold);
            lblTitulo.ForeColor = Color.FromArgb(214, 246, 36);
            lblTitulo.Location = new Point(20, 18);
            lblTitulo.Name = "lblTitulo";
            lblTitulo.Size = new Size(700, 45);
            lblTitulo.TabIndex = 4;
            lblTitulo.Text = "Registrar Reserva";
            lblTitulo.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // gbDatos
            // 
            gbDatos.BackColor = Color.FromArgb(24, 70, 138);
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
            gbDatos.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            gbDatos.ForeColor = Color.White;
            gbDatos.Location = new Point(20, 80);
            gbDatos.Name = "gbDatos";
            gbDatos.Size = new Size(700, 275);
            gbDatos.TabIndex = 3;
            gbDatos.TabStop = false;
            gbDatos.Text = "Datos de la Reserva";
            // 
            // lblImporteValor
            // 
            lblImporteValor.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            lblImporteValor.Location = new Point(535, 210);
            lblImporteValor.Name = "lblImporteValor";
            lblImporteValor.Size = new Size(125, 28);
            lblImporteValor.TabIndex = 0;
            lblImporteValor.Text = "-";
            lblImporteValor.TextAlign = ContentAlignment.MiddleRight;
            // 
            // lblImporteTitulo
            // 
            lblImporteTitulo.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            lblImporteTitulo.Location = new Point(420, 210);
            lblImporteTitulo.Name = "lblImporteTitulo";
            lblImporteTitulo.Size = new Size(110, 28);
            lblImporteTitulo.TabIndex = 1;
            lblImporteTitulo.Text = "Abonado:";
            // 
            // lblEquipamientoValor
            // 
            lblEquipamientoValor.Location = new Point(140, 155);
            lblEquipamientoValor.Name = "lblEquipamientoValor";
            lblEquipamientoValor.Size = new Size(520, 23);
            lblEquipamientoValor.TabIndex = 2;
            lblEquipamientoValor.Text = "-";
            // 
            // lblEquipamientoTitulo
            // 
            lblEquipamientoTitulo.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblEquipamientoTitulo.Location = new Point(30, 155);
            lblEquipamientoTitulo.Name = "lblEquipamientoTitulo";
            lblEquipamientoTitulo.Size = new Size(105, 23);
            lblEquipamientoTitulo.TabIndex = 3;
            lblEquipamientoTitulo.Text = "Equipamiento:";
            // 
            // lblTarifaValor
            // 
            lblTarifaValor.Location = new Point(460, 115);
            lblTarifaValor.Name = "lblTarifaValor";
            lblTarifaValor.Size = new Size(200, 23);
            lblTarifaValor.TabIndex = 4;
            lblTarifaValor.Text = "-";
            // 
            // lblTarifaTitulo
            // 
            lblTarifaTitulo.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblTarifaTitulo.Location = new Point(360, 115);
            lblTarifaTitulo.Name = "lblTarifaTitulo";
            lblTarifaTitulo.Size = new Size(90, 23);
            lblTarifaTitulo.TabIndex = 5;
            lblTarifaTitulo.Text = "Tarifa:";
            // 
            // lblCanchaValor
            // 
            lblCanchaValor.Location = new Point(140, 115);
            lblCanchaValor.Name = "lblCanchaValor";
            lblCanchaValor.Size = new Size(180, 23);
            lblCanchaValor.TabIndex = 6;
            lblCanchaValor.Text = "-";
            // 
            // lblCanchaTitulo
            // 
            lblCanchaTitulo.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblCanchaTitulo.Location = new Point(30, 115);
            lblCanchaTitulo.Name = "lblCanchaTitulo";
            lblCanchaTitulo.Size = new Size(100, 23);
            lblCanchaTitulo.TabIndex = 7;
            lblCanchaTitulo.Text = "Cancha:";
            // 
            // lblHorarioValor
            // 
            lblHorarioValor.Location = new Point(460, 75);
            lblHorarioValor.Name = "lblHorarioValor";
            lblHorarioValor.Size = new Size(200, 23);
            lblHorarioValor.TabIndex = 8;
            lblHorarioValor.Text = "-";
            // 
            // lblHorarioTitulo
            // 
            lblHorarioTitulo.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblHorarioTitulo.Location = new Point(360, 75);
            lblHorarioTitulo.Name = "lblHorarioTitulo";
            lblHorarioTitulo.Size = new Size(90, 23);
            lblHorarioTitulo.TabIndex = 9;
            lblHorarioTitulo.Text = "Horario:";
            // 
            // lblFechaValor
            // 
            lblFechaValor.Location = new Point(140, 75);
            lblFechaValor.Name = "lblFechaValor";
            lblFechaValor.Size = new Size(180, 23);
            lblFechaValor.TabIndex = 10;
            lblFechaValor.Text = "-";
            // 
            // lblFechaTitulo
            // 
            lblFechaTitulo.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblFechaTitulo.Location = new Point(30, 75);
            lblFechaTitulo.Name = "lblFechaTitulo";
            lblFechaTitulo.Size = new Size(100, 23);
            lblFechaTitulo.TabIndex = 11;
            lblFechaTitulo.Text = "Fecha:";
            // 
            // lblClienteValor
            // 
            lblClienteValor.Location = new Point(140, 35);
            lblClienteValor.Name = "lblClienteValor";
            lblClienteValor.Size = new Size(520, 23);
            lblClienteValor.TabIndex = 12;
            lblClienteValor.Text = "-";
            // 
            // lblClienteTitulo
            // 
            lblClienteTitulo.BackColor = Color.FromArgb(24, 70, 138);
            lblClienteTitulo.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblClienteTitulo.Location = new Point(30, 35);
            lblClienteTitulo.Name = "lblClienteTitulo";
            lblClienteTitulo.Size = new Size(100, 23);
            lblClienteTitulo.TabIndex = 13;
            lblClienteTitulo.Text = "Cliente:";
            // 
            // lblMensaje
            // 
            lblMensaje.ForeColor = Color.FromArgb(214, 246, 36);
            lblMensaje.Location = new Point(20, 370);
            lblMensaje.Name = "lblMensaje";
            lblMensaje.Size = new Size(700, 45);
            lblMensaje.TabIndex = 2;
            lblMensaje.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // btnRegistrar
            // 
            btnRegistrar.BackColor = Color.FromArgb(214, 246, 36);
            btnRegistrar.FlatAppearance.BorderSize = 0;
            btnRegistrar.FlatStyle = FlatStyle.Flat;
            btnRegistrar.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            btnRegistrar.ForeColor = Color.FromArgb(18, 18, 18);
            btnRegistrar.Location = new Point(560, 430);
            btnRegistrar.Name = "btnRegistrar";
            btnRegistrar.Size = new Size(160, 34);
            btnRegistrar.TabIndex = 1;
            btnRegistrar.Text = "Registrar Reserva";
            btnRegistrar.UseVisualStyleBackColor = false;
            btnRegistrar.Click += btnRegistrar_Click;
            // 
            // btnVolver
            // 
            btnVolver.BackColor = Color.FromArgb(24, 70, 138);
            btnVolver.FlatAppearance.BorderColor = Color.FromArgb(64, 103, 166);
            btnVolver.FlatStyle = FlatStyle.Flat;
            btnVolver.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            btnVolver.ForeColor = Color.White;
            btnVolver.Location = new Point(440, 430);
            btnVolver.Name = "btnVolver";
            btnVolver.Size = new Size(100, 34);
            btnVolver.TabIndex = 0;
            btnVolver.Text = "Volver";
            btnVolver.UseVisualStyleBackColor = false;
            btnVolver.Click += btnVolver_Click;
            // 
            // frmRegistrarReserva
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(14, 43, 92);
            ClientSize = new Size(740, 485);
            Controls.Add(btnVolver);
            Controls.Add(btnRegistrar);
            Controls.Add(lblMensaje);
            Controls.Add(gbDatos);
            Controls.Add(lblTitulo);
            Font = new Font("Segoe UI", 9.5F);
            ForeColor = Color.White;
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
