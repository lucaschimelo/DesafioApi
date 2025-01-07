﻿
using System.Collections.Generic;

namespace DesafioApi.Application.Models.DataTransfer
{
    public class LivroDataTransfer
    {
        public int Codl { get; set; }
        public string Titulo { get; set; }
        public string Editora { get; set; }
        public int Edicao { get; set; }
        public string AnoPublicacao { get; set; }
        public List<int> Autores { get; set; }
        public List<int> Assuntos { get; set; }
        public List<LivroFormaCompraDataTransfer> LivroFormaCompra { get; set; }       
    }
}
