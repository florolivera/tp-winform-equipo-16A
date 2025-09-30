using Dominio.Objetos;
using Negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.IO;
using Microsoft.VisualBasic;

namespace TPWinForm_Equipo16A
{
    public partial class Principal : Form
    {
        private List<Articulo> listaArticulos = new List<Articulo>();
        private List<Imagen> imagenesDelSeleccionado = new List<Imagen>();
        private int idxImg = 0;

        private ContextMenuStrip menuMarca = new ContextMenuStrip();
        private ContextMenuStrip menuCategoria = new ContextMenuStrip();

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
            ConfigurarMenusGestion();  
            AplicarFiltro();

        }

        private void CargarListado()
        {
            try
            {
                var neg = new ArticuloNegocio();
                listaArticulos = neg.Listar();

                var cNeg = new CategoriaNegocio();
                var mNeg = new MarcaNegocio();
                var catIds = new HashSet<int>(cNeg.Listar().Select(c => c.Id));
                var marIds = new HashSet<int>(mNeg.Listar().Select(m => m.Id));
                listaArticulos = listaArticulos
                    .Where(a => a.Categoria != null && catIds.Contains(a.Categoria.Id)
                             && a.Marca != null && marIds.Contains(a.Marca.Id))
                    .ToList();


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

            dgvArticulos.DataError += (s, e) => { e.ThrowException = false; };


            dgvArticulos.CellFormatting += (s, e) =>
            {
                if (e.Value == null) return;

                var colName = dgvArticulos.Columns[e.ColumnIndex].Name;

                if (colName == "Marca" && e.Value is Dominio.Objetos.Marca m)
                {
                    e.Value = m.Descripcion; 
                    e.FormattingApplied = true;
                }
                else if (colName == "Categoria" && e.Value is Dominio.Objetos.Categoria c)
                {
                    e.Value = c.Descripcion;  
                    e.FormattingApplied = true;
                }
            };


            pbxArticulo.SizeMode = PictureBoxSizeMode.Zoom;
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

            if (dgvArticulos.Columns.Contains("Id"))
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

        private void btnAgregarImagen_Click(object sender, EventArgs e)
        {
            var articulo = ArticuloSeleccionado();
            if (articulo == null)
            {
                MessageBox.Show("Selecciona un articulo");
                return;
            }

            string url = Interaction.InputBox(
                "Ingrese la URL de la imagen:",
                "Agregar imagen"
            );

            if (string.IsNullOrWhiteSpace(url))
                return;

            pbxArticulo.SizeMode = PictureBoxSizeMode.Zoom;
            pbxArticulo.ImageLocation = url;

            var imgNeg = new ImagenNegocio();
            imgNeg.Agregar(new Imagen
            {
                IdArticulo = articulo.Id,
                ImagenUrl = url
            });

            MessageBox.Show("Imagen cargada correctamente");
        }

        private void ConfigurarMenusGestion()
        {
            // === MARCA ===
            menuMarca.Items.Clear();
            menuMarca.Items.Add("Agregar…", null, (s, e) => AgregarMarca());
            menuMarca.Items.Add("Renombrar…", null, (s, e) => RenombrarMarca());
            menuMarca.Items.Add("Eliminar", null, (s, e) => EliminarMarca());
            cmbMarca.ContextMenuStrip = menuMarca;

            menuCategoria.Items.Clear();
            menuCategoria.Items.Add("Agregar…", null, (s, e) => AgregarCategoria());
            menuCategoria.Items.Add("Renombrar…", null, (s, e) => RenombrarCategoria());
            menuCategoria.Items.Add("Eliminar", null, (s, e) => EliminarCategoria());
            cmbCategoria.ContextMenuStrip = menuCategoria;
        }

        private string Prompt(string titulo, string valorInicial = "")
        {
            var f = new Form
            {
                Width = 320,
                Height = 140,
                Text = titulo,
                FormBorderStyle = FormBorderStyle.FixedDialog,
                StartPosition = FormStartPosition.CenterParent,
                MinimizeBox = false,
                MaximizeBox = false
            };
            var txt = new TextBox { Left = 10, Top = 10, Width = 280, Text = valorInicial };
            var ok = new Button { Text = "OK", Left = 140, Width = 70, Top = 40, DialogResult = DialogResult.OK };
            var cancel = new Button { Text = "Cancelar", Left = 220, Width = 70, Top = 40, DialogResult = DialogResult.Cancel };
            f.Controls.Add(txt); f.Controls.Add(ok); f.Controls.Add(cancel);
            f.AcceptButton = ok; f.CancelButton = cancel;
            return f.ShowDialog(this) == DialogResult.OK ? txt.Text : null;
        }

        private void RefrescarMarcas(int? seleccionarId = null)
        {
            var mNeg = new MarcaNegocio();
            var marcas = mNeg.Listar();
            var lista = new List<Marca> { new Marca { Id = 0, Descripcion = "(Todas)" } };
            lista.AddRange(marcas);
            cmbMarca.DataSource = null;
            cmbMarca.DataSource = lista;
            cmbMarca.DisplayMember = "Descripcion";
            cmbMarca.ValueMember = "Id";
            if (seleccionarId.HasValue) cmbMarca.SelectedValue = seleccionarId.Value;
        }

        private void RefrescarCategorias(int? seleccionarId = null)
        {
            var cNeg = new CategoriaNegocio();
            var cats = cNeg.Listar();
            var lista = new List<Categoria> { new Categoria { Id = 0, Descripcion = "(Todas)" } };
            lista.AddRange(cats);
            cmbCategoria.DataSource = null;
            cmbCategoria.DataSource = lista;
            cmbCategoria.DisplayMember = "Descripcion";
            cmbCategoria.ValueMember = "Id";
            if (seleccionarId.HasValue) cmbCategoria.SelectedValue = seleccionarId.Value;
        }

        private void AgregarMarca()
        {
            var desc = Prompt("Nueva Marca:");
            if (string.IsNullOrWhiteSpace(desc)) return;

            try
            {
                var neg = new MarcaNegocio();

                if (neg.Listar().Any(x => string.Equals(x.Descripcion ?? "", desc.Trim(), StringComparison.OrdinalIgnoreCase)))
                {
                    MessageBox.Show("La marca ya existe.");
                    return;
                }
                neg.Agregar(new Marca { Descripcion = desc.Trim() });

                var recien = neg.Listar().FirstOrDefault(x => string.Equals(x.Descripcion ?? "", desc.Trim(), StringComparison.OrdinalIgnoreCase));
                RefrescarMarcas(recien?.Id);
                CargarListado(); 
                AplicarFiltro();
            }
            catch (Exception ex) { MessageBox.Show("No se pudo agregar la marca.\n" + ex.Message); }
        }

        private void RenombrarMarca()
        {
            var m = cmbMarca.SelectedItem as Marca;
            if (m == null || m.Id == 0) { MessageBox.Show("Seleccioná una marca (no '(Todas)')"); return; }
            var nuevo = Prompt($"Renombrar '{m.Descripcion}':", m.Descripcion ?? "");
            if (string.IsNullOrWhiteSpace(nuevo)) return;

            try
            {
                var neg = new MarcaNegocio();
                m.Descripcion = nuevo.Trim();
                neg.Modificar(m);
                RefrescarMarcas(m.Id);
                CargarListado();
                AplicarFiltro();
            }
            catch (Exception ex) { MessageBox.Show("No se pudo renombrar la marca.\n" + ex.Message); }
        }

        private void EliminarMarca()
        {
            var m = cmbMarca.SelectedItem as Marca;
            if (m == null || m.Id == 0) { MessageBox.Show("Seleccioná una marca (no '(Todas)')"); return; }
            if (MessageBox.Show($"¿Eliminar la marca '{m.Descripcion}'?", "Confirmación",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes) return;

            try
            {
                var neg = new MarcaNegocio();
                neg.Eliminar(m.Id); // si hay artículos referenciando, tirará error por FK
                RefrescarMarcas(0);
                CargarListado();
                AplicarFiltro();
            }
            catch (Exception ex)
            {
                MessageBox.Show("No se pudo eliminar (probablemente está en uso por algún artículo).\n" + ex.Message);
            }
        }

        private void AgregarCategoria()
        {
            var desc = Prompt("Nueva Categoría:");
            if (string.IsNullOrWhiteSpace(desc)) return;

            try
            {
                var neg = new CategoriaNegocio();
                if (neg.Listar().Any(x => string.Equals(x.Descripcion ?? "", desc.Trim(), StringComparison.OrdinalIgnoreCase)))
                { MessageBox.Show("La categoría ya existe."); return; }

                neg.Agregar(new Categoria { Descripcion = desc.Trim() });

                var recien = neg.Listar().FirstOrDefault(x => string.Equals(x.Descripcion ?? "", desc.Trim(), StringComparison.OrdinalIgnoreCase));
                RefrescarCategorias(recien?.Id);
                CargarListado();
                AplicarFiltro();
            }
            catch (Exception ex) { MessageBox.Show("No se pudo agregar la categoría.\n" + ex.Message); }
        }

        private void RenombrarCategoria()
        {
            var c = cmbCategoria.SelectedItem as Categoria;
            if (c == null || c.Id == 0) { MessageBox.Show("Seleccioná una categoría (no '(Todas)')"); return; }
            var nuevo = Prompt($"Renombrar '{c.Descripcion}':", c.Descripcion ?? "");
            if (string.IsNullOrWhiteSpace(nuevo)) return;

            try
            {
                var neg = new CategoriaNegocio();
                c.Descripcion = nuevo.Trim();
                neg.Modificar(c);
                RefrescarCategorias(c.Id);
                CargarListado();
                AplicarFiltro();
            }
            catch (Exception ex) { MessageBox.Show("No se pudo renombrar la categoría.\n" + ex.Message); }
        }

        private void EliminarCategoria()
        {
            var c = cmbCategoria.SelectedItem as Categoria;
            if (c == null || c.Id == 0) { MessageBox.Show("Seleccioná una categoría (no '(Todas)')"); return; }
            if (MessageBox.Show($"¿Eliminar la categoría '{c.Descripcion}'?", "Confirmación",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes) return;

            try
            {
                var neg = new CategoriaNegocio();
                neg.Eliminar(c.Id);
                RefrescarCategorias(0);
                CargarListado();
                AplicarFiltro();
            }
            catch (Exception ex)
            {
                MessageBox.Show("No se pudo eliminar (probablemente está en uso por algún artículo).\n" + ex.Message);
            }
        }
    }
}
