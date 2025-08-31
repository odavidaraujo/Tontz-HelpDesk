using Tontz.Domain.Entities;
using Tontz.Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Tontz.API.Controllers {
    [ApiController]
    [Route("api/[controller]")]
    public class TicketHistoryController : ControllerBase {
        private readonly TontzDbContext _context;

        public TicketHistoryController(TontzDbContext context) {
            _context = context;
        }

        // GET: api/TicketHistory
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TB_HISTORICO_CHAMADO>>> GetAll() {
            var history = await _context.TB_HISTORICO_CHAMADO
                .Include(h => h.RESPONSAVEL)
                .Include(h => h.CHAMADO)
                    .ThenInclude(c => c.EMPRESA)
                .Include(h => h.CHAMADO)
                    .ThenInclude(c => c.SOLICITANTE)
                .ToListAsync();

            return Ok(history);
        }

        // GET: api/TicketHistory/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TB_HISTORICO_CHAMADO>> Get(int id) {
            var entry = await _context.TB_HISTORICO_CHAMADO
                .Include(h => h.RESPONSAVEL)
                .Include(h => h.CHAMADO)
                    .ThenInclude(c => c.EMPRESA)
                .Include(h => h.CHAMADO)
                    .ThenInclude(c => c.SOLICITANTE)
                .FirstOrDefaultAsync(h => h.COD == id);

            if (entry == null)
                return NotFound(new { message = "Histórico não encontrado!" });

            return Ok(entry);
        }

        // POST: api/TicketHistory
        [HttpPost]
        public async Task<ActionResult<TB_HISTORICO_CHAMADO>> Create(TB_HISTORICO_CHAMADO entity) {
            try {
                entity.DATA_ALTERACAO = DateTime.Now;
                _context.TB_HISTORICO_CHAMADO.Add(entity);
                await _context.SaveChangesAsync();

                return CreatedAtAction(nameof(Get), new { id = entity.COD }, entity);
            } catch (Exception ex) {
                return StatusCode(500, new { message = "Erro ao criar histórico", error = ex.Message });
            }
        }

        // PUT: api/TicketHistory/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, TB_HISTORICO_CHAMADO entity) {
            if (id != entity.COD)
                return BadRequest(new { message = "ID não confere!" });

            _context.Entry(entity).State = EntityState.Modified;

            try {
                await _context.SaveChangesAsync();
                return Ok(entity);
            } catch (DbUpdateConcurrencyException) {
                if (!_context.TB_HISTORICO_CHAMADO.Any(h => h.COD == id))
                    return NotFound(new { message = "Histórico não encontrado!" });
                throw;
            }
        }

        // DELETE: api/TicketHistory/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id) {
            var entity = await _context.TB_HISTORICO_CHAMADO.FindAsync(id);
            if (entity == null)
                return NotFound(new { message = "Histórico não encontrado!" });

            _context.TB_HISTORICO_CHAMADO.Remove(entity);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
