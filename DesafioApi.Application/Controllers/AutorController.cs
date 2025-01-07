
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System;
using Microsoft.AspNetCore.Http;
using Desafio.Domain.Interfaces.Services;
using Desafio.Domain.Entities;
using System.Threading;
using DesafioApi.Application.Models;
using System.Collections.Generic;
using Desafio.Domain.Validators;
using FluentValidation.Results;
using System.Linq;

namespace DesafioApi.Application.Controllers
{
    [ApiController]
    [Route("v1/[controller]")]
    [Consumes("application/json")]
    [Produces("application/json")]
    public class AutorController : ControllerBase
    {
        private readonly IAutorService _autorService;
        public AutorController(IAutorService autorService)
        {
            _autorService = autorService;
        }

        /// <summary>
        /// Download do relatorio Autor x Livro
        /// </summary>
        /// <returns></returns>
        /// <response code="200">Sucesso</response>
        /// <response code="404">Não encontrado</response>
        /// <response code="500">Ocorreu um erro interno</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpGet("downloadAutorLivro")]

        public async Task<IActionResult> DownloadReportAutorLivro()
        {
            try
            {
                var obj = await _autorService.ReportAutorLivroAsync();
                if (obj == null)
                    return NotFound();

                return Ok(obj);
            }

            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ResultDataTransfer { ErrorMessages = new List<string> { ex.Message } });
            }
        }


        /// <summary>
        /// Retorna um autor
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
                var obj = await _autorService.GetAsync(id);
                if (obj == null)
                    return NotFound();              

                return StatusCode(StatusCodes.Status200OK, obj);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ResultDataTransfer { ErrorMessages = new List<string> { ex.Message } });
            }
        }




        /// <summary>
        /// Retorna lista de Autores
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
                var list = await _autorService.GetAsync();
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
        /// Cadastra um novo autor
        /// </summary>
        /// <param name="autor"></param>
        /// <param name="ct"></param>
        /// <returns></returns>
        /// <response code="201">Sucesso</response>   
        /// <response code="422">Operação não atende os critérios estabelecidos</response>
        /// <response code="500">Ocorreu um erro interno</response> 
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpPost("")]
        public async Task<IActionResult> Post([FromBody] Autor autor, CancellationToken ct = default)
        {
            try
            {
                ValidationResult result = new AutorValidator().Validate(autor);
                if (!result.IsValid && result.Errors.Count > 0)
                {
                    return StatusCode(StatusCodes.Status422UnprocessableEntity, new ResultDataTransfer { ErrorMessages = result.Errors.Select(x => x.ErrorMessage).ToList() });
                }

                var newObj = await _autorService.AddAsync(autor, ct);

                return StatusCode(StatusCodes.Status201Created, newObj);
            }

            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ResultDataTransfer { ErrorMessages = new List<string> { ex.Message } });
            }

        }



        /// <summary>
        /// Atualiza um autor
        /// </summary>
        /// <param name="id"></param>
        /// <param name="autor"></param>
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
        public async Task<IActionResult> Put(int id, [FromBody] Autor autor, CancellationToken ct = default)
        {
            try
            {
                var obj = await _autorService.GetAsync(id);
                if (obj == null)
                    return NotFound();

                ValidationResult result = new AutorValidator().Validate(autor);
                if (!result.IsValid && result.Errors.Count > 0)
                {
                    return StatusCode(StatusCodes.Status422UnprocessableEntity, new ResultDataTransfer { ErrorMessages = result.Errors.Select(x => x.ErrorMessage).ToList() });
                }

                autor.CodAu = id;
                var updateObj = await _autorService.UpdateAsync(autor, ct);

                return StatusCode(StatusCodes.Status202Accepted, updateObj);
            }

            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ResultDataTransfer { ErrorMessages = new List<string> { ex.Message } });
            }
        }

        /// <summary>
        /// Remove um autor
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
                var obj =  _autorService.Get(id);
                if (obj == null)
                    return NotFound();

                _autorService.Remove(obj);

                return StatusCode(StatusCodes.Status202Accepted);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ResultDataTransfer { ErrorMessages = new List<string> { ex.Message } });
            }
        }

    }
}




