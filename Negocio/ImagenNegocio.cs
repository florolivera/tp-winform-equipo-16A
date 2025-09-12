using Dominio.Objetos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public class ImagenNegocio
    {
        string consultaPrincipal = "SELECT Id, IdArticulo, ImagenUrl FROM IMAGENES";
        string consultaPorId = "SELECT Id, IdArticulo, ImagenUrl FROM IMAGENES WHERE Id=@id";
        string consultaPorArticulo = "SELECT Id, IdArticulo, ImagenUrl FROM IMAGENES WHERE IdArticulo=@id";
        string consultaAgregar = "INSERT INTO IMAGENES (IdArticulo, ImagenUrl) VALUES (@a, @u)";
        string consultaEliminar = "DELETE FROM IMAGENES WHERE Id=@id";
        string consultaEliminarPorArticulo = "DELETE FROM IMAGENES WHERE IdArticulo=@id";
        public List<Imagen> ListarPorArticulo(int idArticulo)
        {
            var lista = new List<Imagen>();
            var datos = new AccesoDB();
            try
            {
                datos.setearConsulta("SELECT Id, IdArticulo, ImagenUrl FROM IMAGENES WHERE IdArticulo=@id");
                datos.setearParametro("@id", idArticulo);
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    lista.Add(new Imagen
                    {
                        Id = (int)datos.Lector["Id"],
                        IdArticulo = (int)datos.Lector["IdArticulo"],
                        ImagenUrl = datos.Lector["ImagenUrl"] as string
                    });
                }
                return lista;
            }
            finally { datos.cerrarConexion(); }
        }

        public void Agregar(Imagen img)
        {
            var datos = new AccesoDB();
            try
            {
                datos.setearConsulta("INSERT INTO IMAGENES (IdArticulo, ImagenUrl) VALUES (@a, @u)");
                datos.setearParametro("@a", img.IdArticulo);
                datos.setearParametro("@u", img.ImagenUrl);
                datos.ejecutarAccion();
            }
            finally { datos.cerrarConexion(); }
        }

        public void Eliminar(int idImagen)
        {
            var datos = new AccesoDB();
            try
            {
                datos.setearConsulta(consultaEliminar);
                datos.setearParametro("@id", idImagen);
                datos.ejecutarAccion();
            }
            finally { datos.cerrarConexion(); }
        }

        public void EliminarPorArticulo(int idArticulo)
        {
            var datos = new AccesoDB();
            try
            {
                datos.setearConsulta(consultaEliminarPorArticulo);
                datos.setearParametro("@id", idArticulo);
                datos.ejecutarAccion();
            }
            finally { datos.cerrarConexion(); }
        }
    }
}
