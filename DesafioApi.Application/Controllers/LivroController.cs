
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System;
using Microsoft.AspNetCore.Http;
using Desafio.Domain.Interfaces.Services;
using Desafio.Domain.Entities;
using System.Threading;
using DesafioApi.Application.Models.DataTransfer;
using System.Collections.Generic;
using System.Linq;
using Desafio.Domain.Validators;
using DesafioApi.Application.Models;
using FluentValidation.Results;


namespace DesafioApi.Application.Controllers
{
    [ApiController]
    [Route("v1/[controller]")]
    [Consumes("application/json")]
    [Produces("application/json")]
    public class LivroController : ControllerBase
    {
        private readonly ILivroService _livroService;
        public LivroController(ILivroService livroService)
        {
            _livroService = livroService;
        }


        /// <summary>
        /// Retorna um livro
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <response code="200">Sucesso</response>
        /// <response code="404">Não encontrado</response>
        /// <response code="500">Ocorreu um erro interno</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                var obj = await _livroService.GetAsync(id);
                if (obj == null)
                    return NotFound();

                List<int> assuntos = obj.Assuntos.Select(x => x.Assunto_CodAs).ToList();
                List<int> autores = obj.Autores.Select(x => x.Autor_CodAu).ToList();
                List<LivroFormaCompraDataTransfer> formaCompras = obj.LivroFormaComprasDTO.Select(x => new LivroFormaCompraDataTransfer
                {
                    Valor = x.Valor,
                    Descricao = x.Descricao,
                    CodFor = x.CodFor,
                    Codl = x.Codl
                }).ToList();

                LivroDataTransfer dto = new LivroDataTransfer
                {
                    AnoPublicacao = obj.AnoPublicacao,
                    Codl = obj.Codl,
                    Edicao = obj.Edicao,
                    Editora = obj.Editora,
                    Titulo = obj.Titulo,
                    Assuntos = assuntos,
                    Autores = autores,
                    LivroFormaCompra = formaCompras
                };

                return StatusCode(StatusCodes.Status200OK, dto);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ResultDataTransfer { ErrorMessages = new List<string> { ex.Message } });
            }
        }




        /// <summary>
        /// Retorna lista de Livros
        /// </summary>
        /// <returns></returns>
        /// <response code="200">Sucesso</response>    
        /// <response code="404">Não encontrado</response>
        /// <response code="500">Ocorreu um erro interno</response> 
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpGet("")]

        public async Task<IActionResult> Get()
        {
            try
            {
                var list = await _livroService.GetAsync();
                if (list == null || list.Count == 0)
                    return NotFound();

                return Ok(list);
            }

            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ResultDataTransfer { ErrorMessages = new List<string> { ex.Message } });
            }
        }

        /// <summary>
        /// Cadastra um novo livro
        /// </summary>
        /// <param name="livro"></param>
        /// <returns></returns>
        /// <response code="201">Sucesso</response>   
        /// <response code="422">Operação não atende os critérios estabelecidos</response>
        /// <response code="500">Ocorreu um erro interno</response> 
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpPost("")]
        public IActionResult Post([FromBody] LivroDataTransfer livro, CancellationToken ct = default)
        {
            try
            {
                Livro objLivro = new Livro
                {
                    AnoPublicacao = livro.AnoPublicacao,
                    Edicao = livro.Edicao,
                    Editora = livro.Editora,
                    Titulo = livro.Titulo
                };

                objLivro.Assuntos = livro?.Assuntos?.Select(assuntoCodAs => new LivroAssunto
                {
                    Assunto_CodAs = assuntoCodAs
                })?.ToList();

                objLivro.Autores = livro?.Autores?.Select(autorCodAu => new LivroAutor
                {
                    Autor_CodAu = autorCodAu
                })?.ToList();

                objLivro.FormaCompras = livro?.LivroFormaCompra?.Select(item => new LivroFormaCompra
                {
                    FormaCompra_CodFor = item.CodFor,
                    Valor = item.Valor
                })?.ToList();

                ValidationResult result = new LivroValidator().Validate(objLivro);
                if (!result.IsValid && result.Errors.Count > 0)
                {
                    return StatusCode(StatusCodes.Status422UnprocessableEntity, new ResultDataTransfer { ErrorMessages = result.Errors.Select(x => x.ErrorMessage).ToList() });
                }

                int codl = _livroService.Insert(objLivro);
                if (codl > 0)
                    return StatusCode(StatusCodes.Status201Created, _livroService.Get(codl));
                else
                    return StatusCode(StatusCodes.Status500InternalServerError);
            }

            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ResultDataTransfer { ErrorMessages = new List<string> { ex.Message } });
            }

        }


        /// <summary>
        /// Atualiza um livro
        /// </summary>
        /// <param name="id"></param>
        /// <param name="livro"></param>
        /// <param name="ct"></param>
        /// <returns></returns>
        /// <response code="202">Sucesso</response>
        /// <response code="404">Não encontrado</response>
        /// <response code="422">Operação não atende os critérios estabelecidos</response>
        /// <response code="500">Ocorreu um erro interno</response>
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpPut("{id}")]

        public IActionResult Put(int id, [FromBody] LivroDataTransfer livro, CancellationToken ct = default)
        {
            try
            {
                var obj = _livroService.Get(id);
                if (obj == null)
                    return NotFound();

                Livro objLivro = new Livro
                {
                    Codl = id,
                    AnoPublicacao = livro.AnoPublicacao,
                    Edicao = livro.Edicao,
                    Editora = livro.Editora,
                    Titulo = livro.Titulo
                };

                objLivro.Assuntos = livro?.Assuntos?.Select(assuntoCodAs => new LivroAssunto
                {
                    Assunto_CodAs = assuntoCodAs,
                    Livro_Codl = id
                })?.ToList();

                objLivro.Autores = livro?.Autores?.Select(autorCodAu => new LivroAutor
                {
                    Autor_CodAu = autorCodAu,
                    Livro_Codl = id
                })?.ToList();

                objLivro.FormaCompras = livro?.LivroFormaCompra?.Select(item => new LivroFormaCompra
                {
                    FormaCompra_CodFor = item.CodFor,
                    Valor = item.Valor,
                    Livro_Codl = id
                })?.ToList();

                ValidationResult result = new LivroValidator().Validate(objLivro);
                if (!result.IsValid && result.Errors.Count > 0)
                {
                    return StatusCode(StatusCodes.Status422UnprocessableEntity, new ResultDataTransfer { ErrorMessages = result.Errors.Select(x => x.ErrorMessage).ToList() });
                }

                int codl = _livroService.Update(objLivro);
                if (codl > 0)
                    return StatusCode(StatusCodes.Status202Accepted, _livroService.Get(codl));
                else
                    return StatusCode(StatusCodes.Status500InternalServerError);

            }

            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ResultDataTransfer { ErrorMessages = new List<string> { ex.Message } });
            }
        }

        /// <summary>
        /// Remove um livro
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <response code="202">Sucesso</response>
        /// <response code="404">Não encontrado</response>
        /// <response code="500">Ocorreu um erro interno</response>
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                var obj = _livroService.Get(id);
                if (obj == null)
                    return NotFound();

                bool success = _livroService.Delete(id);
                if (success)
                    return StatusCode(StatusCodes.Status202Accepted);
                else
                    return StatusCode(StatusCodes.Status500InternalServerError);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ResultDataTransfer { ErrorMessages = new List<string> { ex.Message } });
            }
        }

    }
}




