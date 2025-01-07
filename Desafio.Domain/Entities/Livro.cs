
using Desafio.Domain.DTOs;
using System.Collections.Generic;

namespace Desafio.Domain.Entities
{
    public class Livro
    {
        public int Codl { get; set; }
        public string Titulo { get; set; }
        public string Editora { get; set; }
        public int Edicao { get; set; }
        public string AnoPublicacao { get; set; }      
        public IEnumerable<LivroAssunto> Assuntos { get; set; } = new List<LivroAssunto>(); 
        public IEnumerable<LivroAutor> Autores { get; set; } = new List<LivroAutor>();
        public IEnumerable<LivroFormaCompra> FormaCompras { get; set; } = new List<LivroFormaCompra>();
        public IEnumerable<LivroFormaComprasDTO> LivroFormaComprasDTO { get; set; } = new List<LivroFormaComprasDTO>();
    }
}
