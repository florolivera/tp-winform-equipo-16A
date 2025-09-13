﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Objetos
{
    public class Imagen
    {
        public int Id { get; set; }
        public int IdArticulo { get; set; }
        public string ImagenUrl { get; set; }

        public Imagen() { }
        public Imagen(int id, int idArticulo, string imagenUrl)
        {
            this.Id = id;
            this.IdArticulo = idArticulo;
            this.ImagenUrl = imagenUrl;
        }
    }
}
