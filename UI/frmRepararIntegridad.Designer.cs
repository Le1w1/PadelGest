namespace UI
{
    partial class frmRepararIntegridad
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

        #region Código generado por el Diseñador de Windows Forms

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmRepararIntegridad));
            lblTitulo = new Label();
            lblExplicacion = new Label();
            dgvInconsistencias = new DataGridView();
            lblAdvertenciaGeneral = new Label();
            lblAdvertenciaRecalcular = new Label();
            btnRestaurar = new Button();
            btnRecalcular = new Button();
            btnCancelar = new Button();
            ((System.ComponentModel.ISupportInitialize)dgvInconsistencias).BeginInit();
            SuspendLayout();
            // 
            // lblTitulo
            // 
            lblTitulo.AutoSize = true;
            lblTitulo.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            lblTitulo.ForeColor = SystemColors.ActiveCaptionText;
            lblTitulo.Location = new Point(20, 15);
            lblTitulo.Name = "lblTitulo";
            // AirPadel style preview BEGIN
            lblTitulo.ForeColor = Color.FromArgb(214, 246, 36);
            lblTitulo.Font = new Font("Segoe UI", 18F, FontStyle.Bold);
            // AirPadel style preview END
            lblTitulo.Size = new Size(238, 25);
            lblTitulo.TabIndex = 0;
            lblTitulo.Text = "Reparación de Integridad";
            // 
            // lblExplicacion
            // 
            lblExplicacion.Font = new Font("Segoe UI", 9F);
            lblExplicacion.Location = new Point(20, 50);
            lblExplicacion.Name = "lblExplicacion";
            // AirPadel style preview BEGIN
            lblExplicacion.ForeColor = Color.White;
            // AirPadel style preview END
            lblExplicacion.Size = new Size(700, 40);
            lblExplicacion.TabIndex = 1;
            lblExplicacion.Text = "El sistema detectó las siguientes inconsistencias en la base de datos. Debe reparar el sistema antes de continuar operando.";
            // 
            // dgvInconsistencias
            // 
            dgvInconsistencias.AllowUserToAddRows = false;
            dgvInconsistencias.AllowUserToDeleteRows = false;
            dgvInconsistencias.AllowUserToResizeRows = false;
            dgvInconsistencias.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvInconsistencias.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvInconsistencias.EditMode = DataGridViewEditMode.EditProgrammatically;
            dgvInconsistencias.Font = new Font("Segoe UI", 9F);
            dgvInconsistencias.Location = new Point(20, 95);
            dgvInconsistencias.MultiSelect = false;
            dgvInconsistencias.Name = "dgvInconsistencias";
            // AirPadel style preview BEGIN
            dgvInconsistencias.BackgroundColor = Color.White;
            dgvInconsistencias.BorderStyle = BorderStyle.None;
            dgvInconsistencias.GridColor = Color.FromArgb(64, 103, 166);
            dgvInconsistencias.EnableHeadersVisualStyles = false;
            dgvInconsistencias.RowHeadersVisible = false;
            dgvInconsistencias.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dgvInconsistencias.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dgvInconsistencias.ColumnHeadersHeight = 36;
            dgvInconsistencias.RowTemplate.Height = 30;
            dgvInconsistencias.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(214, 246, 36);
            dgvInconsistencias.ColumnHeadersDefaultCellStyle.ForeColor = Color.FromArgb(18, 18, 18);
            dgvInconsistencias.DefaultCellStyle.SelectionBackColor = Color.FromArgb(211, 230, 248);
            dgvInconsistencias.DefaultCellStyle.SelectionForeColor = Color.FromArgb(18, 18, 18);
            // AirPadel style preview END
            dgvInconsistencias.ReadOnly = true;
            dgvInconsistencias.RowHeadersVisible = false;
            dgvInconsistencias.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvInconsistencias.Size = new Size(700, 200);
            dgvInconsistencias.TabIndex = 2;
            // 
            // lblAdvertenciaGeneral
            // 
            lblAdvertenciaGeneral.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblAdvertenciaGeneral.Location = new Point(20, 310);
            lblAdvertenciaGeneral.Name = "lblAdvertenciaGeneral";
            // AirPadel style preview BEGIN
            lblAdvertenciaGeneral.ForeColor = Color.White;
            // AirPadel style preview END
            lblAdvertenciaGeneral.Size = new Size(700, 40);
            lblAdvertenciaGeneral.TabIndex = 3;
            lblAdvertenciaGeneral.Text = "Elija UNA opción de reparación. La aplicación se reiniciará al finalizar. Los cambios no realizados a través del sistema no serán recuperables.";
            // 
            // lblAdvertenciaRecalcular
            // 
            lblAdvertenciaRecalcular.Font = new Font("Segoe UI", 9F, FontStyle.Italic);
            lblAdvertenciaRecalcular.ForeColor = Color.Firebrick;
            lblAdvertenciaRecalcular.Location = new Point(20, 360);
            lblAdvertenciaRecalcular.Name = "lblAdvertenciaRecalcular";
            // AirPadel style preview BEGIN
            lblAdvertenciaRecalcular.ForeColor = Color.White;
            // AirPadel style preview END
            lblAdvertenciaRecalcular.Size = new Size(700, 55);
            lblAdvertenciaRecalcular.TabIndex = 4;
            lblAdvertenciaRecalcular.Text = resources.GetString("lblAdvertenciaRecalcular.Text");
            // 
            // btnRestaurar
            // 
            btnRestaurar.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnRestaurar.Location = new Point(20, 430);
            btnRestaurar.Name = "btnRestaurar";
            // AirPadel style preview BEGIN
            btnRestaurar.FlatStyle = FlatStyle.Flat;
            btnRestaurar.FlatAppearance.BorderSize = 0;
            btnRestaurar.BackColor = Color.FromArgb(214, 246, 36);
            btnRestaurar.ForeColor = Color.FromArgb(18, 18, 18);
            btnRestaurar.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            btnRestaurar.UseVisualStyleBackColor = false;
            // AirPadel style preview END
            btnRestaurar.Size = new Size(220, 45);
            btnRestaurar.TabIndex = 5;
            btnRestaurar.Text = "Restaurar Backup";
            btnRestaurar.UseVisualStyleBackColor = false;
            btnRestaurar.Click += btnRestaurar_Click;
            // 
            // btnRecalcular
            // 
            btnRecalcular.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnRecalcular.ForeColor = Color.Firebrick;
            btnRecalcular.Location = new Point(260, 430);
            btnRecalcular.Name = "btnRecalcular";
            // AirPadel style preview BEGIN
            btnRecalcular.FlatStyle = FlatStyle.Flat;
            btnRecalcular.FlatAppearance.BorderSize = 1;
            btnRecalcular.FlatAppearance.BorderColor = Color.FromArgb(64, 103, 166);
            btnRecalcular.BackColor = Color.FromArgb(24, 70, 138);
            btnRecalcular.ForeColor = Color.White;
            btnRecalcular.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            btnRecalcular.UseVisualStyleBackColor = false;
            // AirPadel style preview END
            btnRecalcular.Size = new Size(280, 45);
            btnRecalcular.TabIndex = 6;
            btnRecalcular.Text = "Recalcular Dígitos Verificadores";
            btnRecalcular.UseVisualStyleBackColor = false;
            btnRecalcular.Click += btnRecalcular_Click;
            // 
            // btnCancelar
            // 
            btnCancelar.Font = new Font("Segoe UI", 10F);
            btnCancelar.Location = new Point(620, 430);
            btnCancelar.Name = "btnCancelar";
            // AirPadel style preview BEGIN
            btnCancelar.FlatStyle = FlatStyle.Flat;
            btnCancelar.FlatAppearance.BorderSize = 1;
            btnCancelar.FlatAppearance.BorderColor = Color.FromArgb(64, 103, 166);
            btnCancelar.BackColor = Color.FromArgb(24, 70, 138);
            btnCancelar.ForeColor = Color.White;
            btnCancelar.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            btnCancelar.UseVisualStyleBackColor = false;
            // AirPadel style preview END
            btnCancelar.Size = new Size(100, 45);
            btnCancelar.TabIndex = 7;
            btnCancelar.Text = "Cancelar";
            btnCancelar.UseVisualStyleBackColor = false;
            btnCancelar.Click += btnCancelar_Click;
            // 
            // frmRepararIntegridad
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.PowderBlue;
            ClientSize = new Size(740, 500);
            ControlBox = false;
            Controls.Add(lblTitulo);
            Controls.Add(lblExplicacion);
            Controls.Add(dgvInconsistencias);
            Controls.Add(lblAdvertenciaGeneral);
            Controls.Add(lblAdvertenciaRecalcular);
            Controls.Add(btnRestaurar);
            Controls.Add(btnRecalcular);
            Controls.Add(btnCancelar);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "frmRepararIntegridad";
            // AirPadel style preview BEGIN
            BackColor = Color.FromArgb(14, 43, 92);
            ForeColor = Color.White;
            Font = new Font("Segoe UI", 9.5F);
            // AirPadel style preview END
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Reparación de Integridad";
            ((System.ComponentModel.ISupportInitialize)dgvInconsistencias).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Label lblTitulo;
        private System.Windows.Forms.Label lblExplicacion;
        private System.Windows.Forms.DataGridView dgvInconsistencias;
        private System.Windows.Forms.Label lblAdvertenciaGeneral;
        private System.Windows.Forms.Label lblAdvertenciaRecalcular;
        private System.Windows.Forms.Button btnRestaurar;
        private System.Windows.Forms.Button btnRecalcular;
        private System.Windows.Forms.Button btnCancelar;
    }
}