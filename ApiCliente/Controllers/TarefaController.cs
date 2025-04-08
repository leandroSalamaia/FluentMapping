using ApiCliente.Data;
using ApiCliente.Extensions;
using ApiCliente.Models.Tarefa;
using ApiCliente.ViewModels;
using ApiCliente.ViewModels.Tarefa;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApiCliente.Controllers
{
    [ApiController]
    public class TarefaController : ControllerBase
    {
        private readonly AppDbContext _context;

        public TarefaController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet("v1/tarefas")]
        public async Task<IActionResult> GetAsync()
        {
            try
            {
                var tarefas = await _context.Tarefas
                    .Include(x => x.Cliente)
                    .Select(t => new TarefaViewModel
                    {
                        Id = t.Id,
                        StatusTarefa = t.StatusTarefa,
                        TipoTarefa = t.TipoTarefa,
                        ClienteId = t.ClienteId,
                        Descricao = t.Descricao
                    })
                    .ToListAsync();

                return Ok(new ResultViewModel<List<TarefaViewModel>>(tarefas));
            }
            catch (Exception)
            {
                return StatusCode(500, new ResultViewModel<List<TarefaViewModel>>("Erro Interno, código: EX-TA-G01"));
            }
        }

        [HttpGet("v1/tarefas/{id:Guid}")]
        public async Task<IActionResult> GetByIdAsync([FromRoute] Guid id)
        {
            try
            {
                var tarefa = await _context.Tarefas
                    .Include(x => x.Cliente)
                    .Select(t => new TarefaViewModel
                    {
                        Id = t.Id,
                        StatusTarefa = t.StatusTarefa,
                        TipoTarefa = t.TipoTarefa,
                        ClienteId = t.ClienteId,
                        Descricao = t.Descricao
                    })
                    .FirstOrDefaultAsync(t => t.Id == id);

                if (tarefa is null)
                    return NotFound(new ResultViewModel<TarefaViewModel>("Nenhuma tarefa foi encontrada!"));

                return Ok(new ResultViewModel<TarefaViewModel>(tarefa));
            }
            catch (Exception)
            {
                return StatusCode(500, new ResultViewModel<TarefaViewModel>("Erro Interno, código: EX-TA-GB01"));
            }
        }

        [HttpPost("v1/tarefas")]
        public async Task<IActionResult> PostAsync([FromBody] CreateOrEditTarefaViewModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(new ResultViewModel<CreateOrEditTarefaViewModel>(ModelState.GetErros()));

            try
            {
                var tarefa = new Tarefa
                {
                    StatusTarefa = model.StatusTarefa,
                    TipoTarefa = model.TipoTarefa,
                    ClienteId = model.ClienteId,
                    Descricao = model.Descricao
                };

                await _context.Tarefas.AddAsync(tarefa);
                await _context.SaveChangesAsync();

                return Created($"v1/tarefas/{tarefa.Id}", new ResultViewModel<Tarefa>(tarefa));
            }
            catch (Exception)
            {
                return StatusCode(500, new ResultViewModel<CreateOrEditTarefaViewModel>("Erro Interno, código: EX-TA-PO01"));
            }
        }

        [HttpPut("v1/tarefas/{id:Guid}")]
        public async Task<IActionResult> PutAsync([FromRoute] Guid id, [FromBody] CreateOrEditTarefaViewModel model)
        {
            var tarefa = await _context.Tarefas.FirstOrDefaultAsync(x => x.Id == id);

            if (tarefa is null)
                return NotFound(new ResultViewModel<Tarefa>("Tarefa não encontrada!"));

            try
            {
                tarefa.StatusTarefa = model.StatusTarefa;
                tarefa.TipoTarefa = model.TipoTarefa;
                tarefa.Descricao = model.Descricao;
                tarefa.ClienteId = model.ClienteId;

                _context.Tarefas.Update(tarefa);
                await _context.SaveChangesAsync();

                return Ok(new ResultViewModel<Tarefa>(tarefa));
            }
            catch (Exception)
            {
                return StatusCode(500, new ResultViewModel<CreateOrEditTarefaViewModel>("Erro Interno, código: EX-TA-PU01"));
            }
        }

        [HttpDelete("v1/tarefas/{id:Guid}")]
        public async Task<IActionResult> DeleteAsync([FromRoute] Guid id)
        {
            var tarefa = await _context.Tarefas.FirstOrDefaultAsync(x => x.Id == id);

            if (tarefa is null)
                return NotFound(new ResultViewModel<Tarefa>("Tarefa não encontrada!"));

            try
            {
                _context.Tarefas.Remove(tarefa);
                await _context.SaveChangesAsync();

                return Ok(new ResultViewModel<Tarefa>(tarefa));
            }
            catch (Exception)
            {
                return StatusCode(500, new ResultViewModel<Tarefa>("Erro Interno, código: EX-TA-DE01"));
            }
        }
    }
}