namespace TPWinForm_Equipo16A
{
    partial class AltaArticulo
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.TextBox txtCodigo;
        private System.Windows.Forms.TextBox txtNombre;
        private System.Windows.Forms.TextBox txtDescripcion;
        private System.Windows.Forms.TextBox txtPrecio;
        private System.Windows.Forms.ComboBox cmbMarca;
        private System.Windows.Forms.ComboBox cmbCategoria;
        private System.Windows.Forms.Button btnAddMarca;
        private System.Windows.Forms.Button btnAddCategoria;
        private System.Windows.Forms.TextBox txtImagenes;
        private System.Windows.Forms.Button btnGuardar;
        private System.Windows.Forms.Button btnCancelar;
        private System.Windows.Forms.Label lblCodigo;
        private System.Windows.Forms.Label lblNombre;
        private System.Windows.Forms.Label lblDescripcion;
        private System.Windows.Forms.Label lblPrecio;
        private System.Windows.Forms.Label lblMarca;
        private System.Windows.Forms.Label lblCategoria;
        private System.Windows.Forms.Label lblImagenes;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.txtCodigo = new System.Windows.Forms.TextBox();
            this.txtNombre = new System.Windows.Forms.TextBox();
            this.txtDescripcion = new System.Windows.Forms.TextBox();
            this.txtPrecio = new System.Windows.Forms.TextBox();
            this.cmbMarca = new System.Windows.Forms.ComboBox();
            this.cmbCategoria = new System.Windows.Forms.ComboBox();
            this.btnAddMarca = new System.Windows.Forms.Button();
            this.btnAddCategoria = new System.Windows.Forms.Button();
            this.txtImagenes = new System.Windows.Forms.TextBox();
            this.btnGuardar = new System.Windows.Forms.Button();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.lblCodigo = new System.Windows.Forms.Label();
            this.lblNombre = new System.Windows.Forms.Label();
            this.lblDescripcion = new System.Windows.Forms.Label();
            this.lblPrecio = new System.Windows.Forms.Label();
            this.lblMarca = new System.Windows.Forms.Label();
            this.lblCategoria = new System.Windows.Forms.Label();
            this.lblImagenes = new System.Windows.Forms.Label();
            this.btnImgLocal = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // txtCodigo
            // 
            this.txtCodigo.Location = new System.Drawing.Point(120, 12);
            this.txtCodigo.Name = "txtCodigo";
            this.txtCodigo.Size = new System.Drawing.Size(220, 22);
            this.txtCodigo.TabIndex = 7;
            // 
            // txtNombre
            // 
            this.txtNombre.Location = new System.Drawing.Point(120, 42);
            this.txtNombre.Name = "txtNombre";
            this.txtNombre.Size = new System.Drawing.Size(360, 22);
            this.txtNombre.TabIndex = 8;
            // 
            // txtDescripcion
            // 
            this.txtDescripcion.Location = new System.Drawing.Point(120, 72);
            this.txtDescripcion.Multiline = true;
            this.txtDescripcion.Name = "txtDescripcion";
            this.txtDescripcion.Size = new System.Drawing.Size(360, 90);
            this.txtDescripcion.TabIndex = 9;
            // 
            // txtPrecio
            // 
            this.txtPrecio.Location = new System.Drawing.Point(120, 172);
            this.txtPrecio.Name = "txtPrecio";
            this.txtPrecio.Size = new System.Drawing.Size(120, 22);
            this.txtPrecio.TabIndex = 10;
            // 
            // cmbMarca
            // 
            this.cmbMarca.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbMarca.Location = new System.Drawing.Point(120, 202);
            this.cmbMarca.Name = "cmbMarca";
            this.cmbMarca.Size = new System.Drawing.Size(220, 24);
            this.cmbMarca.TabIndex = 11;
            // 
            // cmbCategoria
            // 
            this.cmbCategoria.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCategoria.Location = new System.Drawing.Point(120, 232);
            this.cmbCategoria.Name = "cmbCategoria";
            this.cmbCategoria.Size = new System.Drawing.Size(220, 24);
            this.cmbCategoria.TabIndex = 13;
            // 
            // btnAddMarca
            // 
            this.btnAddMarca.Location = new System.Drawing.Point(346, 201);
            this.btnAddMarca.Name = "btnAddMarca";
            this.btnAddMarca.Size = new System.Drawing.Size(30, 24);
            this.btnAddMarca.TabIndex = 12;
            this.btnAddMarca.Text = "+";
            // 
            // btnAddCategoria
            // 
            this.btnAddCategoria.Location = new System.Drawing.Point(346, 231);
            this.btnAddCategoria.Name = "btnAddCategoria";
            this.btnAddCategoria.Size = new System.Drawing.Size(30, 24);
            this.btnAddCategoria.TabIndex = 14;
            this.btnAddCategoria.Text = "+";
            // 
            // txtImagenes
            // 
            this.txtImagenes.Location = new System.Drawing.Point(15, 285);
            this.txtImagenes.Multiline = true;
            this.txtImagenes.Name = "txtImagenes";
            this.txtImagenes.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtImagenes.Size = new System.Drawing.Size(465, 90);
            this.txtImagenes.TabIndex = 15;
            // 
            // btnGuardar
            // 
            this.btnGuardar.Location = new System.Drawing.Point(294, 420);
            this.btnGuardar.Name = "btnGuardar";
            this.btnGuardar.Size = new System.Drawing.Size(90, 27);
            this.btnGuardar.TabIndex = 16;
            this.btnGuardar.Text = "Guardar";
            // 
            // btnCancelar
            // 
            this.btnCancelar.Location = new System.Drawing.Point(390, 420);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(90, 27);
            this.btnCancelar.TabIndex = 17;
            this.btnCancelar.Text = "Cancelar";
            // 
            // lblCodigo
            // 
            this.lblCodigo.AutoSize = true;
            this.lblCodigo.Location = new System.Drawing.Point(12, 15);
            this.lblCodigo.Name = "lblCodigo";
            this.lblCodigo.Size = new System.Drawing.Size(51, 16);
            this.lblCodigo.TabIndex = 0;
            this.lblCodigo.Text = "Codigo";
            // 
            // lblNombre
            // 
            this.lblNombre.AutoSize = true;
            this.lblNombre.Location = new System.Drawing.Point(12, 45);
            this.lblNombre.Name = "lblNombre";
            this.lblNombre.Size = new System.Drawing.Size(56, 16);
            this.lblNombre.TabIndex = 1;
            this.lblNombre.Text = "Nombre";
            // 
            // lblDescripcion
            // 
            this.lblDescripcion.AutoSize = true;
            this.lblDescripcion.Location = new System.Drawing.Point(12, 75);
            this.lblDescripcion.Name = "lblDescripcion";
            this.lblDescripcion.Size = new System.Drawing.Size(79, 16);
            this.lblDescripcion.TabIndex = 2;
            this.lblDescripcion.Text = "Descripcion";
            // 
            // lblPrecio
            // 
            this.lblPrecio.AutoSize = true;
            this.lblPrecio.Location = new System.Drawing.Point(12, 175);
            this.lblPrecio.Name = "lblPrecio";
            this.lblPrecio.Size = new System.Drawing.Size(46, 16);
            this.lblPrecio.TabIndex = 3;
            this.lblPrecio.Text = "Precio";
            // 
            // lblMarca
            // 
            this.lblMarca.AutoSize = true;
            this.lblMarca.Location = new System.Drawing.Point(12, 205);
            this.lblMarca.Name = "lblMarca";
            this.lblMarca.Size = new System.Drawing.Size(45, 16);
            this.lblMarca.TabIndex = 4;
            this.lblMarca.Text = "Marca";
            // 
            // lblCategoria
            // 
            this.lblCategoria.AutoSize = true;
            this.lblCategoria.Location = new System.Drawing.Point(12, 235);
            this.lblCategoria.Name = "lblCategoria";
            this.lblCategoria.Size = new System.Drawing.Size(66, 16);
            this.lblCategoria.TabIndex = 5;
            this.lblCategoria.Text = "Categoria";
            // 
            // lblImagenes
            // 
            this.lblImagenes.AutoSize = true;
            this.lblImagenes.Location = new System.Drawing.Point(12, 265);
            this.lblImagenes.Name = "lblImagenes";
            this.lblImagenes.Size = new System.Drawing.Size(185, 16);
            this.lblImagenes.TabIndex = 6;
            this.lblImagenes.Text = "Imagenes (una URL por linea)";
            // 
            // btnImgLocal
            // 
            this.btnImgLocal.Location = new System.Drawing.Point(139, 394);
            this.btnImgLocal.Name = "btnImgLocal";
            this.btnImgLocal.Size = new System.Drawing.Size(75, 23);
            this.btnImgLocal.TabIndex = 18;
            this.btnImgLocal.Text = "+";
            this.btnImgLocal.UseVisualStyleBackColor = true;
            this.btnImgLocal.Click += new System.EventHandler(this.btnImgLocal_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 394);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(111, 16);
            this.label1.TabIndex = 19;
            this.label1.Text = "Imagenes (Local)";
            // 
            // AltaArticulo
            // 
            this.ClientSize = new System.Drawing.Size(494, 461);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnImgLocal);
            this.Controls.Add(this.lblCodigo);
            this.Controls.Add(this.lblNombre);
            this.Controls.Add(this.lblDescripcion);
            this.Controls.Add(this.lblPrecio);
            this.Controls.Add(this.lblMarca);
            this.Controls.Add(this.lblCategoria);
            this.Controls.Add(this.lblImagenes);
            this.Controls.Add(this.txtCodigo);
            this.Controls.Add(this.txtNombre);
            this.Controls.Add(this.txtDescripcion);
            this.Controls.Add(this.txtPrecio);
            this.Controls.Add(this.cmbMarca);
            this.Controls.Add(this.btnAddMarca);
            this.Controls.Add(this.cmbCategoria);
            this.Controls.Add(this.btnAddCategoria);
            this.Controls.Add(this.txtImagenes);
            this.Controls.Add(this.btnGuardar);
            this.Controls.Add(this.btnCancelar);
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(512, 508);
            this.MinimumSize = new System.Drawing.Size(512, 508);
            this.Name = "AltaArticulo";
            this.Text = "Articulo";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private System.Windows.Forms.Button btnImgLocal;
        private System.Windows.Forms.Label label1;
    }
}
