namespace UI
{
    partial class frmCambiarIdioma
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            lblIdioma = new Label();
            cboIdiomas = new ComboBox();
            btnGuardar = new Button();
            btnVolver = new Button();
            lblMensaje = new Label();
            SuspendLayout();
            // 
            // lblIdioma
            // 
            lblIdioma.AutoSize = true;
            lblIdioma.Font = new Font("Segoe UI", 10F);
            lblIdioma.Location = new Point(30, 35);
            lblIdioma.Name = "lblIdioma";
            // AirPadel style preview BEGIN
            lblIdioma.ForeColor = Color.White;
            // AirPadel style preview END
            lblIdioma.Size = new Size(54, 19);
            lblIdioma.TabIndex = 0;
            lblIdioma.Text = "Idioma:";
            // 
            // cboIdiomas
            // 
            cboIdiomas.DropDownStyle = ComboBoxStyle.DropDownList;
            cboIdiomas.Font = new Font("Segoe UI", 10F);
            cboIdiomas.FormattingEnabled = true;
            cboIdiomas.Location = new Point(110, 32);
            cboIdiomas.Name = "cboIdiomas";
            // AirPadel style preview BEGIN
            cboIdiomas.BackColor = Color.White;
            cboIdiomas.ForeColor = Color.FromArgb(18, 18, 18);
            cboIdiomas.FlatStyle = FlatStyle.Flat;
            // AirPadel style preview END
            cboIdiomas.Size = new Size(240, 25);
            cboIdiomas.TabIndex = 1;
            // 
            // btnGuardar
            // 
            btnGuardar.BackColor = SystemColors.ButtonHighlight;
            btnGuardar.Font = new Font("Segoe UI", 10F);
            btnGuardar.Location = new Point(110, 110);
            btnGuardar.Name = "btnGuardar";
            // AirPadel style preview BEGIN
            btnGuardar.FlatStyle = FlatStyle.Flat;
            btnGuardar.FlatAppearance.BorderSize = 0;
            btnGuardar.BackColor = Color.FromArgb(214, 246, 36);
            btnGuardar.ForeColor = Color.FromArgb(18, 18, 18);
            btnGuardar.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            btnGuardar.UseVisualStyleBackColor = false;
            // AirPadel style preview END
            btnGuardar.Size = new Size(110, 35);
            btnGuardar.TabIndex = 2;
            btnGuardar.Text = "Guardar";
            btnGuardar.UseVisualStyleBackColor = false;
            btnGuardar.Click += btnGuardar_Click;
            // 
            // btnVolver
            // 
            btnVolver.Font = new Font("Segoe UI", 10F);
            btnVolver.Location = new Point(240, 110);
            btnVolver.Name = "btnVolver";
            // AirPadel style preview BEGIN
            btnVolver.FlatStyle = FlatStyle.Flat;
            btnVolver.FlatAppearance.BorderSize = 1;
            btnVolver.FlatAppearance.BorderColor = Color.FromArgb(64, 103, 166);
            btnVolver.BackColor = Color.FromArgb(24, 70, 138);
            btnVolver.ForeColor = Color.White;
            btnVolver.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            btnVolver.UseVisualStyleBackColor = false;
            // AirPadel style preview END
            btnVolver.Size = new Size(110, 35);
            btnVolver.TabIndex = 3;
            btnVolver.Text = "Volver";
            btnVolver.UseVisualStyleBackColor = false;
            btnVolver.Click += btnVolver_Click;
            // 
            // lblMensaje
            // 
            lblMensaje.AutoSize = true;
            lblMensaje.Font = new Font("Segoe UI", 9F);
            lblMensaje.ForeColor = Color.DarkRed;
            lblMensaje.Location = new Point(30, 75);
            lblMensaje.Name = "lblMensaje";
            // AirPadel style preview BEGIN
            lblMensaje.ForeColor = Color.FromArgb(214, 246, 36);
            // AirPadel style preview END
            lblMensaje.Size = new Size(0, 15);
            lblMensaje.TabIndex = 4;
            // 
            // frmCambiarIdioma
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.PowderBlue;
            ClientSize = new Size(400, 180);
            Controls.Add(lblMensaje);
            Controls.Add(btnVolver);
            Controls.Add(btnGuardar);
            Controls.Add(cboIdiomas);
            Controls.Add(lblIdioma);
            ForeColor = SystemColors.ControlText;
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "frmCambiarIdioma";
            // AirPadel style preview BEGIN
            BackColor = Color.FromArgb(14, 43, 92);
            ForeColor = Color.White;
            Font = new Font("Segoe UI", 9.5F);
            // AirPadel style preview END
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Cambiar Idioma";
            FormClosed += frmCambiarIdioma_FormClosed;
            Load += frmCambiarIdioma_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblIdioma;
        private ComboBox cboIdiomas;
        private Button btnGuardar;
        private Button btnVolver;
        private Label lblMensaje;
    }
}
