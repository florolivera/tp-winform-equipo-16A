using Dominio.Objetos;
using Negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.IO;
using Microsoft.VisualBasic;
using System.Net;
using System.Drawing;
using System.Threading.Tasks;

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
            AplicarFiltro(); 

            //dgvArticulos.Columns["Id"].Visible = false;
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

            // evita que el DGV explote 
            dgvArticulos.DataError += (s, e) => { e.ThrowException = false; };

            // convierte marca y categoria en texto 
            dgvArticulos.CellFormatting += (s, e) =>
            {
                if (e.Value == null) return;

                var colName = dgvArticulos.Columns[e.ColumnIndex].Name;

                if (colName == "Marca" && e.Value is Dominio.Objetos.Marca m)
                {
                    e.Value = m.Descripcion;   // mostrar texto
                    e.FormattingApplied = true;
                }
                else if (colName == "Categoria" && e.Value is Dominio.Objetos.Categoria c)
                {
                    e.Value = c.Descripcion;   // mostrar texto
                    e.FormattingApplied = true;
                }
            };
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

            dgvArticulos.Columns["Id"].Visible = false;
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


        //metodo auxiliar para descargar de internet ya que algunos ervidores bloquean las urls directas
        private async Task<Image> DescargarImagenConHeadersAsync(string url)
        {
            // chequea que la url sea valida y use http o https
            if (!Uri.TryCreate((url ?? "").Trim(), UriKind.Absolute, out var uri) ||
                (uri.Scheme != Uri.UriSchemeHttp && uri.Scheme != Uri.UriSchemeHttps))
                return null;

            // intenta descargar la imagen con un referer opcional / referer: indica a la web desde donde llega la solicitud
            async Task<Image> TryDownload(string referer)
            {
                using (var wc = new WebClient())
                {
                    //
                    wc.Headers.Add(HttpRequestHeader.UserAgent, "Mozilla/5.0");


                    if (!string.IsNullOrEmpty(referer))
                        wc.Headers.Add(HttpRequestHeader.Referer, referer);

                    //descarga de datos de la imagen
                    byte[] data = await wc.DownloadDataTaskAsync(uri);

                    //se crea la imagen de los datos bajados
                    using (var ms = new MemoryStream(data))
                    using (var img = Image.FromStream(ms))
                        return new Bitmap(img); 
                }
            }

            try
            {
                //  sin referer 
                return await TryDownload(null);
            }
            catch
            {
                try
                {
                    //  con referer del mismo sitio
                    return await TryDownload($"{uri.Scheme}://{uri.Host}/");
                }
                catch
                {
                    try
                    {
                        // con referer genérico (google)
                        return await TryDownload("https://www.google.com/");
                    }
                    catch
                    {
                        //si todo falla, no muuestra nada
                        return null;
                    }
                }
            }
        }



        private async void MostrarImagenActual()
        {

            if (imagenesDelSeleccionado == null || imagenesDelSeleccionado.Count == 0)
            {
                pbxArticulo.Image = null;
                pbxArticulo.ImageLocation = null;
                return;
            }

            
            if (idxImg < 0) idxImg = imagenesDelSeleccionado.Count - 1;
            if (idxImg >= imagenesDelSeleccionado.Count) idxImg = 0;

            string url = imagenesDelSeleccionado[idxImg].ImagenUrl;

            pbxArticulo.SizeMode = PictureBoxSizeMode.Zoom;
            pbxArticulo.Image = null;
            pbxArticulo.ImageLocation = null;

            // descarga la img desde internet usando el metodo auxiliar 
            var img = await DescargarImagenConHeadersAsync(url);
            // si logro descargar, muestra la imagen, si no, queda null
            pbxArticulo.Image = img; 
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

        private void btnAgregarImagen_Click(object sender, EventArgs e)
        {
            var articulo = ArticuloSeleccionado();
            if (articulo == null)
            {
                MessageBox.Show("Selecciona un articulo");
                return;
            }

            // Pide la URL al usuario
            string url = Interaction.InputBox(
                "Ingrese la URL de la imagen:",
                "Agregar imagen"
                
            );

            if (string.IsNullOrWhiteSpace(url))
                return; // cancelado o vacio

            // Mostrar en el PictureBox
            pbxArticulo.SizeMode = PictureBoxSizeMode.Zoom;
            pbxArticulo.ImageLocation = url;

            // Guardar en la BD
            var imgNeg = new ImagenNegocio();
            imgNeg.Agregar(new Imagen
            {
                IdArticulo = articulo.Id,
                ImagenUrl = url
            });

            MessageBox.Show("Imagen cargada correctamente");
        }

        
    }
}
