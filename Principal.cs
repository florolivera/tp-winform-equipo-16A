using Dominio.Objetos;
using Negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace TPWinForm_Equipo16A
{
    public partial class Principal : Form
    {
        private List<Articulo> listaArticulos = new List<Articulo>();
        private List<Imagen> imagenesDelSeleccionado = new List<Imagen>();
        private int idxImg = 0;

        public Principal()
        {
            InitializeComponent();
            Shown += (s, e) => Init();
        }

        private void Init()
        {
            CargarListado();
            ConfigurarGrilla();
            CargarFiltros();
            AplicarFiltro(); // inicial
        }

        private void CargarListado()
        {
            try
            {
                var neg = new ArticuloNegocio();
                listaArticulos = neg.Listar();
                dgvArticulos.DataSource = listaArticulos;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void ConfigurarGrilla()
        {
            dgvArticulos.AutoGenerateColumns = true;
            dgvArticulos.ReadOnly = true;
            dgvArticulos.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvArticulos.MultiSelect = false;
        }

        private void CargarFiltros()
        {
            var mNeg = new MarcaNegocio();
            var cNeg = new CategoriaNegocio();

            var marcas = mNeg.Listar();
            var cats = cNeg.Listar();

            cmbMarca.DataSource = new List<Marca> { new Marca { Id = 0, Descripcion = "(Todas)" } }
                                  .Concat(marcas).ToList();
            cmbMarca.DisplayMember = "Descripcion";
            cmbMarca.ValueMember = "Id";

            cmbCategoria.DataSource = new List<Categoria> { new Categoria { Id = 0, Descripcion = "(Todas)" } }
                                     .Concat(cats).ToList();
            cmbCategoria.DisplayMember = "Descripcion";
            cmbCategoria.ValueMember = "Id";
        }

        private void AplicarFiltro()
        {
            var q = txtBuscar.Text?.Trim().ToLower() ?? "";
            int idMarca = (int)(cmbMarca.SelectedValue ?? 0);
            int idCat = (int)(cmbCategoria.SelectedValue ?? 0);

            var data = listaArticulos.Where(a =>
                (string.IsNullOrEmpty(q) || (a.Codigo + " " + a.Nombre + " " + a.Descripcion).ToLower().Contains(q)) &&
                (idMarca == 0 || (a.Marca != null && a.Marca.Id == idMarca)) &&
                (idCat == 0 || (a.Categoria != null && a.Categoria.Id == idCat))
            ).ToList();

            dgvArticulos.DataSource = null;
            dgvArticulos.DataSource = data;
        }

        private void dgvArticulos_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvArticulos.CurrentRow == null) return;
            var art = dgvArticulos.CurrentRow.DataBoundItem as Articulo;
            if (art == null) return;
            CargarImagenes(art.Id);
        }

        private void CargarImagenes(int idArticulo)
        {
            var neg = new ImagenNegocio();
            imagenesDelSeleccionado = neg.ListarPorArticulo(idArticulo);
            idxImg = 0;
            MostrarImagenActual();
        }

        private void MostrarImagenActual()
        {
            if (imagenesDelSeleccionado == null || imagenesDelSeleccionado.Count == 0)
            {
                pbxArticulo.ImageLocation = null;
                pbxArticulo.Image = null;
                return;
            }
            if (idxImg < 0) idxImg = imagenesDelSeleccionado.Count - 1;
            if (idxImg >= imagenesDelSeleccionado.Count) idxImg = 0;

            pbxArticulo.SizeMode = PictureBoxSizeMode.Zoom;
            pbxArticulo.LoadAsync(imagenesDelSeleccionado[idxImg].ImagenUrl);
        }

        private Articulo ArticuloSeleccionado()
        {
            return dgvArticulos.CurrentRow?.DataBoundItem as Articulo;
        }

        private void btnPrevImg_Click(object sender, EventArgs e)
        {
            idxImg--;
            MostrarImagenActual();
        }

        private void btnNextImg_Click(object sender, EventArgs e)
        {
            idxImg++;
            MostrarImagenActual();
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            var f = new AltaArticulo();
            if (f.ShowDialog() == DialogResult.OK)
            {
                CargarListado();
                AplicarFiltro();
            }
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            var artSel = ArticuloSeleccionado();
            if (artSel == null) return;

            var f = new AltaArticulo(artSel);
            if (f.ShowDialog() == DialogResult.OK)
            {
                CargarListado();
                AplicarFiltro();
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            var artSel = ArticuloSeleccionado();
            if (artSel == null) return;

            if (MessageBox.Show("Eliminar el articulo seleccionado?", "Confirmar",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                new ArticuloNegocio().Eliminar(artSel.Id);
                CargarListado();
                AplicarFiltro();
            }
        }

        private void btnDetalle_Click(object sender, EventArgs e)
        {
            var a = ArticuloSeleccionado();
            if (a == null) return;

            MessageBox.Show(
                $"Codigo: {a.Codigo}\nNombre: {a.Nombre}\nMarca: {a?.Marca?.Descripcion}\nCategoria: {a?.Categoria?.Descripcion}\nPrecio: {a.Precio:C}\n\n{a.Descripcion}",
                "Detalle");
        }

        private void txtBuscar_TextChanged(object sender, EventArgs e) => AplicarFiltro();
        private void cmbMarca_SelectionChangeCommitted(object sender, EventArgs e) => AplicarFiltro();
        private void cmbCategoria_SelectionChangeCommitted(object sender, EventArgs e) => AplicarFiltro();
    }
}
