
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System;
using Microsoft.AspNetCore.Http;
using Desafio.Domain.Interfaces.Services;
using Desafio.Domain.Entities;
using System.Threading;
using Desafio.Domain.Validators;
using DesafioApi.Application.Models;
using FluentValidation.Results;
using System.Linq;
using System.Collections.Generic;
using Desafio.Service.Services;

namespace DesafioApi.Application.Controllers
{
    [ApiController]
    [Route("v1/[controller]")]
    [Consumes("application/json")]
    [Produces("application/json")]
    public class FormaCompraController : ControllerBase
    {
        private readonly IFormaCompraService _formaCompraService;
        public FormaCompraController(IFormaCompraService formaCompraService)
        {
            _formaCompraService = formaCompraService;
        }


        /// <summary>
        /// Retorna uma forma de compra
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
                var obj = await _formaCompraService.GetAsync(id);
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
        /// Retorna lista de formas de compra
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
                var list = await _formaCompraService.GetAsync();
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
        /// Cadastra uma nova forma de compra
        /// </summary>
        /// <param name="formaCompra"></param>
        /// <param name="ct"></param>
        /// <returns></returns>
        /// <response code="201">Sucesso</response>   
        /// <response code="422">Operação não atende os critérios estabelecidos</response>
        /// <response code="500">Ocorreu um erro interno</response> 
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpPost("")]
        public async Task<IActionResult> Post([FromBody] FormaCompra formaCompra, CancellationToken ct = default)
        {
            try
            {
                ValidationResult result = new FormaCompraValidator().Validate(formaCompra);
                if (!result.IsValid && result.Errors.Count > 0)
                {
                    return StatusCode(StatusCodes.Status422UnprocessableEntity, new ResultDataTransfer { ErrorMessages = result.Errors.Select(x => x.ErrorMessage).ToList() });
                }

                var newObj = await _formaCompraService.AddAsync(formaCompra, ct);

                return StatusCode(StatusCodes.Status201Created, newObj);
            }

            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ResultDataTransfer { ErrorMessages = new List<string> { ex.Message } });
            }

        }



        /// <summary>
        /// Atualiza uma forma de compra
        /// </summary>
        /// <param name="id"></param>
        /// <param name="formaCompra"></param>
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
        public async Task<IActionResult> Put(int id, [FromBody] FormaCompra formaCompra, CancellationToken ct = default)
        {
            try
            {
                var obj = await _formaCompraService.GetAsync(id);
                if (obj == null)
                    return NotFound();

                ValidationResult result = new FormaCompraValidator().Validate(formaCompra);
                if (!result.IsValid && result.Errors.Count > 0)
                {
                    return StatusCode(StatusCodes.Status422UnprocessableEntity, new ResultDataTransfer { ErrorMessages = result.Errors.Select(x => x.ErrorMessage).ToList() });
                }

                formaCompra.CodFor = id;
                var updateObj = await _formaCompraService.UpdateAsync(formaCompra, ct);

                return StatusCode(StatusCodes.Status202Accepted, updateObj);
            }

            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ResultDataTransfer { ErrorMessages = new List<string> { ex.Message } });
            }
        }

        /// <summary>
        /// Remove uma forma de compra
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
                var obj = _formaCompraService.Get(id);
                if (obj == null)
                    return NotFound();

                _formaCompraService.Remove(obj);

                return StatusCode(StatusCodes.Status202Accepted);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ResultDataTransfer { ErrorMessages = new List<string> { ex.Message } });
            }
        }

    }
}




