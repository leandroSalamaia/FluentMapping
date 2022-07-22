using ApiCliente.Attributes;
using ApiCliente.Data;
using ApiCliente.Enums;
using ApiCliente.Extensions;
using ApiCliente.Models;
using ApiCliente.ViewModels;
using ApiCliente.ViewModels.Cliente;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections;

namespace ApiCliente.Controllers
{
    [ApiController]
    public class ClienteController : ControllerBase
    {
        private AppDbContext context;

        public ClienteController(AppDbContext context)
        {
            this.context = context;
        }

        [HttpGet("v1/clientes")]
        [ApiKey]
        public async Task<IActionResult> GetAsync()
        {
            try
            {
                var clientes = await context.Clientes
                    .AsNoTracking()
                    .Include(x => x.PessoaFisica)
                    .Include(x => x.PessoaJuridica)
                    .Include(x => x.Endereco)
                    .Include(x => x.Contatos)
                    .ToListAsync();
                return Ok(new ResultViewModel<List<Cliente>>(clientes));
            }
            catch (Exception e)
            {
                return StatusCode(500, new ResultViewModel<List<Cliente>>("Erro Interno, código: EX-CL-G01"));
            }
        }


        [HttpGet("v1/clientes/{id:Guid}")]
        [ApiKey]
        public async Task<IActionResult> GetByIdAsync([FromRoute] Guid id)
        {
            try
            {
                var cliente = await context.Clientes
                    .Include(x => x.PessoaFisica)
                    .Include(x => x.PessoaJuridica)
                    .Include(x => x.Endereco)
                    .Include(x => x.Contatos)
                    .FirstOrDefaultAsync(x => x.Id == id);

                if(cliente is null)
                    return NotFound(new ResultViewModel<Cliente>("Nem um Cliente foi encontrado!"));
                return Ok(new ResultViewModel<Cliente>(cliente));
            }
            catch (DbUpdateException e)
            {
                return StatusCode(500, new ResultViewModel<List<Cliente>>("Erro Interno, código: EX-CL-GB01"));
            }
            catch (Exception e)
            {
                return StatusCode(500, new ResultViewModel<List<Cliente>>("Erro Interno, código: EX-CL-GB02"));
            }
        }

        [HttpPost("v1/clientes")]
        public async Task<IActionResult> PostAsync([FromBody] CreateOrEditClienteViewModel clienteViewModel)
        {
            if(!ModelState.IsValid)
                return BadRequest(new ResultViewModel<Cliente>(ModelState.GetErros()));

            try
            {
                var cliente = new Cliente();
                cliente.TipoCliente = (int)TipoPessoa.PessoaFisica;

                if (clienteViewModel.TipoCliente is (int)TipoPessoa.PessoaFisica)
                {
                    var PessoaFisica = new PessoaFisica()
                    {
                        Nome = clienteViewModel.PessoaFisica.Nome,
                        CPF = clienteViewModel.PessoaFisica.CPF,
                        DataNascimento = clienteViewModel.PessoaFisica.DataNascimento
                    };

                    cliente.PessoaFisica = PessoaFisica;
                }
                else
                {
                    var pessoaJuridica = new PessoaJuridica()
                    {
                        Nome = clienteViewModel.PessoaJuridica.Nome,
                        CNPJ = clienteViewModel.PessoaJuridica.CNPJ
                    };

                    cliente.PessoaJuridica = pessoaJuridica;
                }

                var endereco = new Endereco()
                {
                    Cidade = clienteViewModel.Endereco.Cidade,
                    Logradouro = clienteViewModel.Endereco.Logradouro,
                    numero = clienteViewModel.Endereco.numero,
                    Complemento = clienteViewModel.Endereco.Complemento,
                    Estado = clienteViewModel.Endereco.Estado,
                    Bairro = clienteViewModel.Endereco.Bairro
                };

                cliente.Endereco = endereco;

                var contatos = new List<Contato>();
                foreach (var item in clienteViewModel.Contatos)
                {
                    var contato = new Contato() { 
                        Email = item.Email,
                        Telefone = item.Telefone
                    };
                    contatos.Add(contato);
                }
                cliente.Contatos = contatos;
                await context.Clientes.AddAsync(cliente);
                await context.SaveChangesAsync();

                return Created($"v1/clientes/{cliente.Id}", new ResultViewModel<Cliente>(cliente));
            }
            catch (DbUpdateException e)
            {
                return StatusCode(500, new ResultViewModel<List<Cliente>>("Erro Interno, código: EX-CL-PO01"));
            }
            catch (Exception e)
            {
                return StatusCode(500, new ResultViewModel<List<Cliente>>("Erro Interno, código: EX-CL-PO02"));
            }

        }

        [HttpPut("v1/clientes/{id:Guid}")]
        public async Task<IActionResult> PutAsync([FromRoute] Guid id, [FromBody] CreateOrEditClienteViewModel clienteViewModel)
        {
            var cliente = await context.Clientes.FirstOrDefaultAsync(x => x.Id == id);
            if (cliente is not null)
            {
                if (clienteViewModel.TipoCliente is (int)TipoPessoa.PessoaFisica)
                {
                    var pessoaFisica = await context.PessoasFisicas.FirstOrDefaultAsync(x => x.ClienteId == id);
                    pessoaFisica.Nome = clienteViewModel.PessoaFisica.Nome;
                    pessoaFisica.CPF = clienteViewModel.PessoaFisica.CPF;
                    pessoaFisica.DataNascimento = clienteViewModel.PessoaFisica.DataNascimento;

                    cliente.PessoaFisica = pessoaFisica;
                }
                else
                {
                    var pessoaJuridica = await context.PessoasJuridicas.FirstOrDefaultAsync(x => x.ClienteId == id);
                    pessoaJuridica.Nome = clienteViewModel.PessoaJuridica.Nome;
                    pessoaJuridica.CNPJ = clienteViewModel.PessoaJuridica.CNPJ;

                    cliente.PessoaJuridica = pessoaJuridica;
                }

                var endereco = await context.Enderecos.FirstOrDefaultAsync(x => x.ClienteId == id);
                endereco.Cidade = clienteViewModel.Endereco.Cidade;
                endereco.Logradouro = clienteViewModel.Endereco.Logradouro;
                endereco.numero = clienteViewModel.Endereco.numero;
                endereco.Complemento = clienteViewModel.Endereco.Complemento;
                endereco.Estado = clienteViewModel.Endereco.Estado;
                endereco.Bairro = clienteViewModel.Endereco.Bairro;
                endereco.Cep = clienteViewModel.Endereco.Cep;

                cliente.Endereco = endereco;

                context.Clientes.Update(cliente);
                await context.SaveChangesAsync();               
            }

            return Ok();
        }

        [HttpPost("v1/clientes/{id:Guid}")]
        public async Task<IActionResult> DeleteAsync([FromRoute] Guid id)
        {
            var clientes = await context.Clientes.FirstOrDefaultAsync(x => x.Id == id);

            if (clientes is null)
                return NotFound();

            context.Clientes.Remove(clientes);
            await context.SaveChangesAsync();

            return Ok();
        }
    }
}
