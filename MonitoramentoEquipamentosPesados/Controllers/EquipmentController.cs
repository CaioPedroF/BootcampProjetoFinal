using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MonitoramentoEquipamentosPesados.Data;
using MonitoramentoEquipamentosPesados.Models;

namespace MonitoramentoEquipamentosPesados.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EquipamentoController : ControllerBase
    {
        private readonly AppDbContext _db;

        public EquipamentoController(AppDbContext db)
        {
            _db = db;
        }

        // GET: api/equipamento
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Equipamento>>> GetAll()
        {
            return await _db.Equipamentos.ToListAsync();
        }

        // GET: api/equipamento/5
        [HttpGet("{id:int}")]
        public async Task<ActionResult<Equipamento>> Get(int id)
        {
            var equipamento = await _db.Equipamentos.FindAsync(id);
            if (equipamento == null)
                return NotFound();
            return equipamento;
        }

        // POST: api/equipamento
        [HttpPost]
        public async Task<ActionResult<Equipamento>> Create(Equipamento equipamento)
        {
            _db.Equipamentos.Add(equipamento);
            await _db.SaveChangesAsync();
            return CreatedAtAction(nameof(Get), new { id = equipamento.Id }, equipamento);
        }

        // PUT: api/equipamento/5
        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, Equipamento input)
        {
            var equipamento = await _db.Equipamentos.FindAsync(id);
            if (equipamento == null) return NotFound();

            // Atualiza todas as propriedades do model
            equipamento.Codigo = input.Codigo;
            equipamento.Tipo = input.Tipo;
            equipamento.Modelo = input.Modelo;
            equipamento.Horimetro = input.Horimetro;
            equipamento.StatusOperacional = input.StatusOperacional;
            equipamento.DataAquisicao = input.DataAquisicao;
            equipamento.LocalizacaoAtual = input.LocalizacaoAtual;

            await _db.SaveChangesAsync();
            return NoContent();
        }

        
        // DELETE: api/equipment/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
        var equipamento = await _db.Equipamentos.FindAsync(id);
         if (equipamento == null)
        return NotFound($"Equipamento com id {id} não encontrado.");

       _db.Equipamentos.Remove(equipamento);
       await _db.SaveChangesAsync();

      return Ok($"Equipamento '{equipamento.Codigo}' deletado com sucesso.");
    }


    // GET: api/equipamento/list?page=1&pageSize=10&tipo=Caminhao&statusOperacional=Operacional&codigo=CAT-793F
      // GET: api/equipamentos/list
[HttpGet("list")]
public async Task<ActionResult<IEnumerable<Equipamento>>> GetList(
    [FromQuery] int page = 1,
    [FromQuery] int pageSize = 10,
    [FromQuery] string? tipo = null,
    [FromQuery] string? statusOperacional = null,
    [FromQuery] string? codigo = null)
{
    var query = _db.Equipamentos.AsQueryable();

    if (!string.IsNullOrEmpty(tipo))
        query = query.Where(e => e.Tipo.ToLower() == tipo.ToLower());

    if (!string.IsNullOrEmpty(statusOperacional))
        query = query.Where(e => e.StatusOperacional.ToLower() == statusOperacional.ToLower());

    if (!string.IsNullOrEmpty(codigo))
        query = query.Where(e => e.Codigo.ToLower().Contains(codigo.ToLower()));

    var totalItems = await query.CountAsync();
    var totalPages = (int)Math.Ceiling(totalItems / (double)pageSize);

    var equipamentos = await query
        .Skip((page - 1) * pageSize)
        .Take(pageSize)
        .ToListAsync();

    return Ok(new
    {
        Page = page,
        PageSize = pageSize,
        TotalItems = totalItems,
        TotalPages = totalPages,
        Data = equipamentos
    });


}


    }
}
