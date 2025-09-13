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
            this.SuspendLayout();
            // labels
            this.lblCodigo.Text = "Codigo";
            this.lblCodigo.Location = new System.Drawing.Point(12, 15);
            this.lblCodigo.AutoSize = true;

            this.lblNombre.Text = "Nombre";
            this.lblNombre.Location = new System.Drawing.Point(12, 45);
            this.lblNombre.AutoSize = true;

            this.lblDescripcion.Text = "Descripcion";
            this.lblDescripcion.Location = new System.Drawing.Point(12, 75);
            this.lblDescripcion.AutoSize = true;

            this.lblPrecio.Text = "Precio";
            this.lblPrecio.Location = new System.Drawing.Point(12, 175);
            this.lblPrecio.AutoSize = true;

            this.lblMarca.Text = "Marca";
            this.lblMarca.Location = new System.Drawing.Point(12, 205);
            this.lblMarca.AutoSize = true;

            this.lblCategoria.Text = "Categoria";
            this.lblCategoria.Location = new System.Drawing.Point(12, 235);
            this.lblCategoria.AutoSize = true;

            this.lblImagenes.Text = "Imagenes (una URL por linea)";
            this.lblImagenes.Location = new System.Drawing.Point(12, 265);
            this.lblImagenes.AutoSize = true;

            // inputs
            this.txtCodigo.Location = new System.Drawing.Point(120, 12);
            this.txtCodigo.Size = new System.Drawing.Size(220, 22);

            this.txtNombre.Location = new System.Drawing.Point(120, 42);
            this.txtNombre.Size = new System.Drawing.Size(360, 22);

            this.txtDescripcion.Location = new System.Drawing.Point(120, 72);
            this.txtDescripcion.Size = new System.Drawing.Size(360, 90);
            this.txtDescripcion.Multiline = true;

            this.txtPrecio.Location = new System.Drawing.Point(120, 172);
            this.txtPrecio.Size = new System.Drawing.Size(120, 22);

            this.cmbMarca.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbMarca.Location = new System.Drawing.Point(120, 202);
            this.cmbMarca.Size = new System.Drawing.Size(220, 24);

            this.btnAddMarca.Text = "+";
            this.btnAddMarca.Location = new System.Drawing.Point(346, 201);
            this.btnAddMarca.Size = new System.Drawing.Size(30, 24);

            this.cmbCategoria.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCategoria.Location = new System.Drawing.Point(120, 232);
            this.cmbCategoria.Size = new System.Drawing.Size(220, 24);

            this.btnAddCategoria.Text = "+";
            this.btnAddCategoria.Location = new System.Drawing.Point(346, 231);
            this.btnAddCategoria.Size = new System.Drawing.Size(30, 24);

            this.txtImagenes.Location = new System.Drawing.Point(15, 285);
            this.txtImagenes.Size = new System.Drawing.Size(465, 120);
            this.txtImagenes.Multiline = true;
            this.txtImagenes.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;

            this.btnGuardar.Text = "Guardar";
            this.btnGuardar.Location = new System.Drawing.Point(294, 420);
            this.btnGuardar.Size = new System.Drawing.Size(90, 27);

            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.Location = new System.Drawing.Point(390, 420);
            this.btnCancelar.Size = new System.Drawing.Size(90, 27);

            // form
            this.ClientSize = new System.Drawing.Size(494, 461);
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
            this.Name = "AltaArticulo";
            this.Text = "Articulo";
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}
