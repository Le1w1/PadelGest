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
            DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
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
            lblTitulo.Font = new Font("Segoe UI", 18F, FontStyle.Bold);
            lblTitulo.ForeColor = Color.FromArgb(214, 246, 36);
            lblTitulo.Location = new Point(20, 15);
            lblTitulo.Name = "lblTitulo";
            lblTitulo.Size = new Size(304, 32);
            lblTitulo.TabIndex = 0;
            lblTitulo.Text = "Reparación de Integridad";
            // 
            // lblExplicacion
            // 
            lblExplicacion.Font = new Font("Segoe UI", 9F);
            lblExplicacion.ForeColor = Color.White;
            lblExplicacion.Location = new Point(20, 50);
            lblExplicacion.Name = "lblExplicacion";
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
            dgvInconsistencias.BackgroundColor = Color.White;
            dgvInconsistencias.BorderStyle = BorderStyle.None;
            dgvInconsistencias.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dgvInconsistencias.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = Color.FromArgb(214, 246, 36);
            dataGridViewCellStyle1.Font = new Font("Segoe UI", 9F);
            dataGridViewCellStyle1.ForeColor = Color.FromArgb(18, 18, 18);
            dataGridViewCellStyle1.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = DataGridViewTriState.True;
            dgvInconsistencias.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            dgvInconsistencias.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = SystemColors.Window;
            dataGridViewCellStyle2.Font = new Font("Segoe UI", 9F);
            dataGridViewCellStyle2.ForeColor = Color.White;
            dataGridViewCellStyle2.SelectionBackColor = Color.FromArgb(211, 230, 248);
            dataGridViewCellStyle2.SelectionForeColor = Color.FromArgb(18, 18, 18);
            dataGridViewCellStyle2.WrapMode = DataGridViewTriState.False;
            dgvInconsistencias.DefaultCellStyle = dataGridViewCellStyle2;
            dgvInconsistencias.EditMode = DataGridViewEditMode.EditProgrammatically;
            dgvInconsistencias.EnableHeadersVisualStyles = false;
            dgvInconsistencias.Font = new Font("Segoe UI", 9F);
            dgvInconsistencias.GridColor = Color.FromArgb(64, 103, 166);
            dgvInconsistencias.Location = new Point(20, 95);
            dgvInconsistencias.MultiSelect = false;
            dgvInconsistencias.Name = "dgvInconsistencias";
            dgvInconsistencias.ReadOnly = true;
            dgvInconsistencias.RowHeadersVisible = false;
            dgvInconsistencias.RowTemplate.Height = 30;
            dgvInconsistencias.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvInconsistencias.Size = new Size(700, 200);
            dgvInconsistencias.TabIndex = 2;
            // 
            // lblAdvertenciaGeneral
            // 
            lblAdvertenciaGeneral.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblAdvertenciaGeneral.ForeColor = Color.White;
            lblAdvertenciaGeneral.Location = new Point(20, 310);
            lblAdvertenciaGeneral.Name = "lblAdvertenciaGeneral";
            lblAdvertenciaGeneral.Size = new Size(700, 40);
            lblAdvertenciaGeneral.TabIndex = 3;
            lblAdvertenciaGeneral.Text = "Elija UNA opción de reparación. La aplicación se reiniciará al finalizar. Los cambios no realizados a través del sistema no serán recuperables.";
            // 
            // lblAdvertenciaRecalcular
            // 
            lblAdvertenciaRecalcular.Font = new Font("Segoe UI", 9F, FontStyle.Italic);
            lblAdvertenciaRecalcular.ForeColor = Color.White;
            lblAdvertenciaRecalcular.Location = new Point(20, 360);
            lblAdvertenciaRecalcular.Name = "lblAdvertenciaRecalcular";
            lblAdvertenciaRecalcular.Size = new Size(700, 55);
            lblAdvertenciaRecalcular.TabIndex = 4;
            lblAdvertenciaRecalcular.Text = resources.GetString("lblAdvertenciaRecalcular.Text");
            // 
            // btnRestaurar
            // 
            btnRestaurar.BackColor = Color.FromArgb(214, 246, 36);
            btnRestaurar.FlatAppearance.BorderSize = 0;
            btnRestaurar.FlatStyle = FlatStyle.Flat;
            btnRestaurar.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            btnRestaurar.ForeColor = Color.FromArgb(18, 18, 18);
            btnRestaurar.Location = new Point(20, 430);
            btnRestaurar.Name = "btnRestaurar";
            btnRestaurar.Size = new Size(220, 45);
            btnRestaurar.TabIndex = 5;
            btnRestaurar.Text = "Restaurar Backup";
            btnRestaurar.UseVisualStyleBackColor = false;
            btnRestaurar.Click += btnRestaurar_Click;
            // 
            // btnRecalcular
            // 
            btnRecalcular.BackColor = Color.FromArgb(214, 246, 36);
            btnRecalcular.FlatAppearance.BorderColor = Color.FromArgb(64, 103, 166);
            btnRecalcular.FlatStyle = FlatStyle.Flat;
            btnRecalcular.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            btnRecalcular.ForeColor = Color.Black;
            btnRecalcular.Location = new Point(260, 430);
            btnRecalcular.Name = "btnRecalcular";
            btnRecalcular.Size = new Size(280, 45);
            btnRecalcular.TabIndex = 6;
            btnRecalcular.Text = "Recalcular Dígitos Verificadores";
            btnRecalcular.UseVisualStyleBackColor = false;
            btnRecalcular.Click += btnRecalcular_Click;
            // 
            // btnCancelar
            // 
            btnCancelar.BackColor = Color.FromArgb(24, 70, 138);
            btnCancelar.FlatAppearance.BorderColor = Color.FromArgb(64, 103, 166);
            btnCancelar.FlatStyle = FlatStyle.Flat;
            btnCancelar.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            btnCancelar.ForeColor = Color.White;
            btnCancelar.Location = new Point(620, 430);
            btnCancelar.Name = "btnCancelar";
            btnCancelar.Size = new Size(100, 45);
            btnCancelar.TabIndex = 7;
            btnCancelar.Text = "Cancelar";
            btnCancelar.UseVisualStyleBackColor = false;
            btnCancelar.Click += btnCancelar_Click;
            // 
            // frmRepararIntegridad
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(14, 43, 92);
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
            Font = new Font("Segoe UI", 9.5F);
            ForeColor = Color.White;
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "frmRepararIntegridad";
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