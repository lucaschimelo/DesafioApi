
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System;
using Microsoft.AspNetCore.Http;
using Desafio.Domain.Interfaces.Services;
using Desafio.Domain.Entities;
using System.Threading;
using Desafio.Domain.Validators;
using FluentValidation.Results;
using DesafioApi.Application.Models;
using System.Linq;
using System.Collections.Generic;

namespace DesafioApi.Application.Controllers
{
    [ApiController]
    [Route("v1/[controller]")]
    [Consumes("application/json")]
    [Produces("application/json")]
    public class AssuntoController : ControllerBase
    {
        private readonly IAssuntoService _assuntoService;
      
        public AssuntoController(IAssuntoService assuntoService)
        {
            _assuntoService = assuntoService;          
        }


        /// <summary>
        /// Retorna um assunto
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
                var obj = await _assuntoService.GetAsync(id);
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
        /// Retorna lista de Assuntos
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
                var list = await _assuntoService.GetAsync();
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
        /// Cadastra um novo assunto
        /// </summary>
        /// <param name="assunto"></param>
        /// <param name="ct"></param>
        /// <returns></returns>
        /// <response code="201">Sucesso</response>   
        /// <response code="422">Operação não atende os critérios estabelecidos</response>
        /// <response code="500">Ocorreu um erro interno</response> 
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpPost("")]
        public async Task<IActionResult> Post([FromBody] Assunto assunto, CancellationToken ct = default)
        {
            try
            {
                ValidationResult result = new AssuntoValidator().Validate(assunto);
                if (!result.IsValid && result.Errors.Count > 0)
                {
                    return StatusCode(StatusCodes.Status422UnprocessableEntity, new ResultDataTransfer {ErrorMessages = result.Errors.Select(x => x.ErrorMessage).ToList() });
                }

                var newObj = await _assuntoService.AddAsync(assunto, ct);

                return StatusCode(StatusCodes.Status201Created, newObj);
            }

            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ResultDataTransfer { ErrorMessages = new List<string> { ex.Message } });
            }

        }



        /// <summary>
        /// Atualiza um assunto
        /// </summary>
        /// <param name="id"></param>
        /// <param name="assunto"></param>
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
        public async Task<IActionResult> Put(int id, [FromBody] Assunto assunto, CancellationToken ct = default)
        {
            try
            {
                var obj = await _assuntoService.GetAsync(id);
                if (obj == null)
                    return NotFound();

                ValidationResult result = new AssuntoValidator().Validate(assunto);
                if (!result.IsValid && result.Errors.Count > 0)
                {
                    return StatusCode(StatusCodes.Status422UnprocessableEntity, new ResultDataTransfer { ErrorMessages = result.Errors.Select(x => x.ErrorMessage).ToList() });
                }

                assunto.CodAs = id;
                var updateObj = await _assuntoService.UpdateAsync(assunto, ct);

                return StatusCode(StatusCodes.Status202Accepted, updateObj);
            }

            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ResultDataTransfer { ErrorMessages = new List<string> { ex.Message } });
            }
        }

        /// <summary>
        /// Remove um assunto
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
                var obj = _assuntoService.Get(id);
                if (obj == null)
                    return NotFound();

                _assuntoService.Remove(obj);

                return StatusCode(StatusCodes.Status202Accepted);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ResultDataTransfer { ErrorMessages = new List<string> { ex.Message } });
            }
        }

    }
}




