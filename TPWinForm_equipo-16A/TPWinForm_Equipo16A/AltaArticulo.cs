using Dominio.Objetos;
using Negocio;
using System;
using System.Linq;
using System.Windows.Forms;

namespace TPWinForm_Equipo16A
{
    public partial class AltaArticulo : Form
    {
        private Articulo _edit; // null = alta

        public AltaArticulo(Articulo a = null)
        {
            InitializeComponent();
            _edit = a;

            Shown += (s, e) => Init();
            btnGuardar.Click += (s, e) => Guardar();
            btnCancelar.Click += (s, e) => { DialogResult = DialogResult.Cancel; Close(); };
            btnAddMarca.Click += (s, e) => AgregarMarca();
            btnAddCategoria.Click += (s, e) => AgregarCategoria();
        }

        private void Init()
        {
            var mNeg = new MarcaNegocio();
            var cNeg = new CategoriaNegocio();

            cmbMarca.DataSource = mNeg.Listar();
            cmbMarca.DisplayMember = "Descripcion";
            cmbMarca.ValueMember = "Id";

            cmbCategoria.DataSource = cNeg.Listar();
            cmbCategoria.DisplayMember = "Descripcion";
            cmbCategoria.ValueMember = "Id";

            if (_edit != null)
            {
                Text = "Modificar Articulo";
                txtCodigo.Text = _edit.Codigo;
                txtNombre.Text = _edit.Nombre;
                txtDescripcion.Text = _edit.Descripcion;
                txtPrecio.Text = _edit.Precio.ToString("0.##");

                if (_edit.Marca != null) cmbMarca.SelectedValue = _edit.Marca.Id;
                if (_edit.Categoria != null) cmbCategoria.SelectedValue = _edit.Categoria.Id;

                var imgs = new ImagenNegocio().ListarPorArticulo(_edit.Id);
                if (imgs != null && imgs.Count > 0)
                    txtImagenes.Lines = imgs.Select(i => i.ImagenUrl).ToArray();
            }
            else Text = "Agregar Articulo";
        }

        private bool Validar(out string msj)
        {
            msj = "";
            if (string.IsNullOrWhiteSpace(txtCodigo.Text)) msj = "Codigo requerido";
            else if (string.IsNullOrWhiteSpace(txtNombre.Text)) msj = "Nombre requerido";
            else if (!decimal.TryParse(txtPrecio.Text, out _)) msj = "Precio invalido";
            return msj == "";
        }

        private void Guardar()
        {
            if (!Validar(out string msj)) { MessageBox.Show(msj); return; }

            var art = _edit ?? new Articulo();
            art.Codigo = txtCodigo.Text.Trim();
            art.Nombre = txtNombre.Text.Trim();
            art.Descripcion = txtDescripcion.Text.Trim();
            art.Precio = decimal.Parse(txtPrecio.Text.Trim());
            art.Marca = cmbMarca.SelectedItem as Marca;
            art.Categoria = cmbCategoria.SelectedItem as Categoria;

            var aNeg = new ArticuloNegocio();
            if (_edit == null)
            {
                int id = aNeg.Agregar(art);
                GuardarImagenes(id);
            }
            else
            {
                art.Id = _edit.Id;
                aNeg.Modificar(art); // tu firma real
                new ImagenNegocio().EliminarPorArticulo(_edit.Id);
                GuardarImagenes(_edit.Id);
            }

            DialogResult = DialogResult.OK;
            Close();
        }

        private void GuardarImagenes(int idArticulo)
        {
            var lineas = (txtImagenes.Text ?? "")
                .Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);

            var iNeg = new ImagenNegocio();
            foreach (var url in lineas)
                iNeg.Agregar(new Imagen { IdArticulo = idArticulo, ImagenUrl = url.Trim() });
        }

        private void AgregarMarca()
        {
            var d = Prompt("Nueva Marca:");
            if (string.IsNullOrWhiteSpace(d)) return;
            var neg = new MarcaNegocio();
            neg.Agregar(new Marca { Descripcion = d.Trim() });
            cmbMarca.DataSource = neg.Listar();
        }

        private void AgregarCategoria()
        {
            var d = Prompt("Nueva Categoria:");
            if (string.IsNullOrWhiteSpace(d)) return;
            var neg = new CategoriaNegocio();
            neg.Agregar(new Categoria { Descripcion = d.Trim() });
            cmbCategoria.DataSource = neg.Listar();
        }

        private string Prompt(string titulo)
        {
            var f = new Form()
            {
                Width = 300,
                Height = 140,
                Text = titulo,
                FormBorderStyle = FormBorderStyle.FixedDialog,
                StartPosition = FormStartPosition.CenterParent,
                MinimizeBox = false,
                MaximizeBox = false
            };
            var txt = new TextBox() { Left = 10, Top = 10, Width = 260 };
            var ok = new Button() { Text = "OK", Left = 110, Width = 70, Top = 40, DialogResult = DialogResult.OK };
            var cancel = new Button() { Text = "Cancelar", Left = 190, Width = 80, Top = 40, DialogResult = DialogResult.Cancel };
            f.Controls.Add(txt); f.Controls.Add(ok); f.Controls.Add(cancel);
            f.AcceptButton = ok; f.CancelButton = cancel;
            return f.ShowDialog(this) == DialogResult.OK ? txt.Text : null;
        }

        private void btnImgLocal_Click(object sender, EventArgs e)
        {
            OpenFileDialog archivo = new OpenFileDialog();
            archivo.Filter = "imagenes|*.jpg;*.jpeg;*.png;";

            if (archivo.ShowDialog() != DialogResult.OK)
                return;

            if (string.IsNullOrWhiteSpace(txtImagenes.Text))
                txtImagenes.Text = archivo.FileName;
            else
                txtImagenes.AppendText(Environment.NewLine + archivo.FileName);
        }

    }
}
