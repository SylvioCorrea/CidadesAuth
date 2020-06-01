using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using CidadesAuth.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using CidadesAuth.Models;
using Microsoft.AspNetCore.Authorization;

namespace CidadesAuth.Controllers
{
    //Classe controladora padrao. Cada metodo corresponde a uma das operacoes CRUD.
    //As operacoes assincronas sao intermediadas pelo objeto da classe que serve de
    //repositorio 'cidadesRepository'.

    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CidadesController : ControllerBase
    {
        private readonly ICidadesRepository cidadesRepository;

        public CidadesController(ICidadesRepository cidadesRepository)
        {
            this.cidadesRepository = cidadesRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Read()
        {
            return Ok(await cidadesRepository.GetAll());
        }

        [HttpGet("{codigo}")]
        public async Task<IActionResult> Read(int codigo)
        {
            var cidade = await cidadesRepository.GetOne(codigo);
            if (cidade != null)
            {
                return Ok(cidade);
            }
            return NotFound($"Nao ha cidade com o codigo {codigo} no banco de dados");
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Cidade cidade)
        {
            int result = await cidadesRepository.Add(cidade);
            if (result > 0)
            {
                //Insere a chave primaria gerada pelo banco de dados no objeto antes de retorna-lo
                cidade.Codigo = result;
                return StatusCode(201, cidade);
            }
            return StatusCode(500, "Erro: a inserção não foi efetuada");
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] Cidade cidade)
        {
            int result = await cidadesRepository.Update(cidade);
            if (result > 0)
            {
                return Ok(cidade);
            }
            return StatusCode(500, "Erro: a modificação não foi efetuada");
        }

        [HttpDelete("{codigo}")]
        public async Task<IActionResult> Delete(int codigo)
        {
            int result = await cidadesRepository.Delete(codigo);
            if (result > 0)
            {
                return Ok();
            }
            return StatusCode(500, "Erro: nenhuma modificacao foi efetuada");
        }

        [HttpGet("cidades-por-uf")]
        public async Task<IActionResult> ReadCidadesPorUf()
        {
            return Ok(await cidadesRepository.GetCidadesPorUF());
        }
    }
}
