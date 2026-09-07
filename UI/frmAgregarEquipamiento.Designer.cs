namespace UI
{
    partial class frmAgregarEquipamiento
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
            components = new System.ComponentModel.Container();
            lblTitulo = new Label();
            gbEquipamiento = new GroupBox();
            lblTotalValor = new Label();
            lblTotalTitulo = new Label();
            lblImportePelotasValor = new Label();
            lblImportePelotasTitulo = new Label();
            lblStockPelotasValor = new Label();
            lblStockPelotasTitulo = new Label();
            lblCantidadPelotas = new Label();
            nudPelotas = new NumericUpDown();
            lblPelotas = new Label();
            lblImportePaletasValor = new Label();
            lblImportePaletasTitulo = new Label();
            lblStockPaletasValor = new Label();
            lblStockPaletasTitulo = new Label();
            lblCantidadPaletas = new Label();
            nudPaletas = new NumericUpDown();
            lblPaletas = new Label();
            lblMensaje = new Label();
            btnConfirmar = new Button();
            btnVolver = new Button();
            errorProvider = new ErrorProvider(components);
            gbEquipamiento.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)nudPelotas).BeginInit();
            ((System.ComponentModel.ISupportInitialize)nudPaletas).BeginInit();
            ((System.ComponentModel.ISupportInitialize)errorProvider).BeginInit();
            SuspendLayout();
            // lblTitulo
            lblTitulo.Font = new Font("Segoe UI", 20F, FontStyle.Bold);
            lblTitulo.Location = new Point(20, 18);
            lblTitulo.Name = "lblTitulo";
            lblTitulo.Size = new Size(620, 45);
            lblTitulo.TabIndex = 0;
            lblTitulo.Text = "Agregar Equipamiento";
            lblTitulo.TextAlign = ContentAlignment.MiddleCenter;
            // gbEquipamiento
            gbEquipamiento.BackColor = Color.LightSkyBlue;
            gbEquipamiento.Controls.Add(lblTotalValor);
            gbEquipamiento.Controls.Add(lblTotalTitulo);
            gbEquipamiento.Controls.Add(lblImportePelotasValor);
            gbEquipamiento.Controls.Add(lblImportePelotasTitulo);
            gbEquipamiento.Controls.Add(lblStockPelotasValor);
            gbEquipamiento.Controls.Add(lblStockPelotasTitulo);
            gbEquipamiento.Controls.Add(lblCantidadPelotas);
            gbEquipamiento.Controls.Add(nudPelotas);
            gbEquipamiento.Controls.Add(lblPelotas);
            gbEquipamiento.Controls.Add(lblImportePaletasValor);
            gbEquipamiento.Controls.Add(lblImportePaletasTitulo);
            gbEquipamiento.Controls.Add(lblStockPaletasValor);
            gbEquipamiento.Controls.Add(lblStockPaletasTitulo);
            gbEquipamiento.Controls.Add(lblCantidadPaletas);
            gbEquipamiento.Controls.Add(nudPaletas);
            gbEquipamiento.Controls.Add(lblPaletas);
            gbEquipamiento.Location = new Point(20, 80);
            gbEquipamiento.Name = "gbEquipamiento";
            gbEquipamiento.Size = new Size(620, 250);
            gbEquipamiento.TabIndex = 1;
            gbEquipamiento.TabStop = false;
            gbEquipamiento.Text = "Equipamiento disponible";
            // lblTotalValor
            lblTotalValor.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            lblTotalValor.Location = new Point(420, 205);
            lblTotalValor.Name = "lblTotalValor";
            lblTotalValor.Size = new Size(165, 28);
            lblTotalValor.TabIndex = 15;
            lblTotalValor.Text = "$0,00";
            lblTotalValor.TextAlign = ContentAlignment.MiddleRight;
            // lblTotalTitulo
            lblTotalTitulo.AutoSize = true;
            lblTotalTitulo.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            lblTotalTitulo.Location = new Point(325, 209);
            lblTotalTitulo.Name = "lblTotalTitulo";
            lblTotalTitulo.Size = new Size(48, 20);
            lblTotalTitulo.TabIndex = 14;
            lblTotalTitulo.Text = "Total:";
            // Pelotas
            lblPelotas.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            lblPelotas.Location = new Point(25, 120);
            lblPelotas.Name = "lblPelotas";
            lblPelotas.Size = new Size(110, 25);
            lblPelotas.TabIndex = 8;
            lblPelotas.Text = "Pelotas";
            nudPelotas.Location = new Point(190, 122);
            nudPelotas.Name = "nudPelotas";
            nudPelotas.Size = new Size(75, 23);
            nudPelotas.TabIndex = 9;
            nudPelotas.ValueChanged += nudCantidad_ValueChanged;
            lblCantidadPelotas.AutoSize = true;
            lblCantidadPelotas.Location = new Point(140, 125);
            lblCantidadPelotas.Name = "lblCantidadPelotas";
            lblCantidadPelotas.Size = new Size(39, 15);
            lblCantidadPelotas.TabIndex = 10;
            lblCantidadPelotas.Text = "Cant.:";
            lblStockPelotasTitulo.AutoSize = true;
            lblStockPelotasTitulo.Location = new Point(300, 125);
            lblStockPelotasTitulo.Name = "lblStockPelotasTitulo";
            lblStockPelotasTitulo.Size = new Size(39, 15);
            lblStockPelotasTitulo.TabIndex = 11;
            lblStockPelotasTitulo.Text = "Stock:";
            lblStockPelotasValor.Location = new Point(350, 122);
            lblStockPelotasValor.Name = "lblStockPelotasValor";
            lblStockPelotasValor.Size = new Size(50, 23);
            lblStockPelotasValor.TabIndex = 12;
            lblStockPelotasValor.Text = "0";
            lblImportePelotasTitulo.AutoSize = true;
            lblImportePelotasTitulo.Location = new Point(420, 125);
            lblImportePelotasTitulo.Name = "lblImportePelotasTitulo";
            lblImportePelotasTitulo.Size = new Size(75, 15);
            lblImportePelotasTitulo.TabIndex = 13;
            lblImportePelotasTitulo.Text = "Importe unit.:";
            lblImportePelotasValor.Location = new Point(505, 122);
            lblImportePelotasValor.Name = "lblImportePelotasValor";
            lblImportePelotasValor.Size = new Size(80, 23);
            lblImportePelotasValor.TabIndex = 14;
            lblImportePelotasValor.Text = "-";
            // Paletas
            lblPaletas.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            lblPaletas.Location = new Point(25, 55);
            lblPaletas.Name = "lblPaletas";
            lblPaletas.Size = new Size(110, 25);
            lblPaletas.TabIndex = 0;
            lblPaletas.Text = "Paletas";
            nudPaletas.Location = new Point(190, 57);
            nudPaletas.Name = "nudPaletas";
            nudPaletas.Size = new Size(75, 23);
            nudPaletas.TabIndex = 1;
            nudPaletas.ValueChanged += nudCantidad_ValueChanged;
            lblCantidadPaletas.AutoSize = true;
            lblCantidadPaletas.Location = new Point(140, 60);
            lblCantidadPaletas.Name = "lblCantidadPaletas";
            lblCantidadPaletas.Size = new Size(39, 15);
            lblCantidadPaletas.TabIndex = 2;
            lblCantidadPaletas.Text = "Cant.:";
            lblStockPaletasTitulo.AutoSize = true;
            lblStockPaletasTitulo.Location = new Point(300, 60);
            lblStockPaletasTitulo.Name = "lblStockPaletasTitulo";
            lblStockPaletasTitulo.Size = new Size(39, 15);
            lblStockPaletasTitulo.TabIndex = 3;
            lblStockPaletasTitulo.Text = "Stock:";
            lblStockPaletasValor.Location = new Point(350, 57);
            lblStockPaletasValor.Name = "lblStockPaletasValor";
            lblStockPaletasValor.Size = new Size(50, 23);
            lblStockPaletasValor.TabIndex = 4;
            lblStockPaletasValor.Text = "0";
            lblImportePaletasTitulo.AutoSize = true;
            lblImportePaletasTitulo.Location = new Point(420, 60);
            lblImportePaletasTitulo.Name = "lblImportePaletasTitulo";
            lblImportePaletasTitulo.Size = new Size(75, 15);
            lblImportePaletasTitulo.TabIndex = 5;
            lblImportePaletasTitulo.Text = "Importe unit.:";
            lblImportePaletasValor.Location = new Point(505, 57);
            lblImportePaletasValor.Name = "lblImportePaletasValor";
            lblImportePaletasValor.Size = new Size(80, 23);
            lblImportePaletasValor.TabIndex = 6;
            lblImportePaletasValor.Text = "-";
            // lblMensaje
            lblMensaje.ForeColor = Color.DarkRed;
            lblMensaje.Location = new Point(20, 345);
            lblMensaje.Name = "lblMensaje";
            lblMensaje.Size = new Size(620, 40);
            lblMensaje.TabIndex = 2;
            lblMensaje.TextAlign = ContentAlignment.MiddleLeft;
            // btnConfirmar
            btnConfirmar.Location = new Point(480, 400);
            btnConfirmar.Name = "btnConfirmar";
            btnConfirmar.Size = new Size(160, 34);
            btnConfirmar.TabIndex = 3;
            btnConfirmar.Text = "Confirmar";
            btnConfirmar.UseVisualStyleBackColor = true;
            btnConfirmar.Click += btnConfirmar_Click;
            // btnVolver
            btnVolver.Location = new Point(360, 400);
            btnVolver.Name = "btnVolver";
            btnVolver.Size = new Size(100, 34);
            btnVolver.TabIndex = 4;
            btnVolver.Text = "Volver";
            btnVolver.UseVisualStyleBackColor = true;
            btnVolver.Click += btnVolver_Click;
            // errorProvider
            errorProvider.ContainerControl = this;
            // frmAgregarEquipamiento
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.WhiteSmoke;
            ClientSize = new Size(660, 455);
            Controls.Add(btnVolver);
            Controls.Add(btnConfirmar);
            Controls.Add(lblMensaje);
            Controls.Add(gbEquipamiento);
            Controls.Add(lblTitulo);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "frmAgregarEquipamiento";
            StartPosition = FormStartPosition.CenterParent;
            Text = "PadelGest - Agregar Equipamiento";
            FormClosed += frmAgregarEquipamiento_FormClosed;
            Load += frmAgregarEquipamiento_Load;
            gbEquipamiento.ResumeLayout(false);
            gbEquipamiento.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)nudPelotas).EndInit();
            ((System.ComponentModel.ISupportInitialize)nudPaletas).EndInit();
            ((System.ComponentModel.ISupportInitialize)errorProvider).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Label lblTitulo;
        private GroupBox gbEquipamiento;
        private Label lblPaletas;
        private NumericUpDown nudPaletas;
        private Label lblCantidadPaletas;
        private Label lblStockPaletasTitulo;
        private Label lblStockPaletasValor;
        private Label lblImportePaletasTitulo;
        private Label lblImportePaletasValor;
        private Label lblPelotas;
        private NumericUpDown nudPelotas;
        private Label lblCantidadPelotas;
        private Label lblStockPelotasTitulo;
        private Label lblStockPelotasValor;
        private Label lblImportePelotasTitulo;
        private Label lblImportePelotasValor;
        private Label lblTotalTitulo;
        private Label lblTotalValor;
        private Label lblMensaje;
        private Button btnConfirmar;
        private Button btnVolver;
        private ErrorProvider errorProvider;
    }
}
