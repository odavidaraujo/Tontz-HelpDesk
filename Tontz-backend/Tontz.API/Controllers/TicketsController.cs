using Tontz.Domain.Entities;
using Tontz.Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Tontz.API.Controllers {
    [ApiController]
    [Route("api/[controller]")]
    public class TicketsController : ControllerBase {
        private readonly TontzDbContext _context;

        public TicketsController(TontzDbContext context) {
            _context = context;
        }

        // GET: api/Tickets
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TB_CHAMADO>>> GetAll() {
            var tickets = await _context.TB_CHAMADO
                .Include(t => t.EMPRESA)
                .Include(t => t.SOLICITANTE)
                .Include(t => t.CATEGORIA)
                .Include(t => t.PRIORIDADE)
                .Include(t => t.STATUS)
                .ToListAsync();

            return Ok(tickets);
        }

        // GET: api/Tickets/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TB_CHAMADO>> Get(int id) {
            var ticket = await _context.TB_CHAMADO
                .Include(t => t.EMPRESA)
                .Include(t => t.SOLICITANTE)
                .Include(t => t.CATEGORIA)
                .Include(t => t.PRIORIDADE)
                .Include(t => t.STATUS)
                .FirstOrDefaultAsync(t => t.COD == id);

            if (ticket == null)
                return NotFound(new { message = "Chamado não encontrado!" });

            return Ok(ticket);
        }

        // POST: api/Tickets
        [HttpPost]
        public async Task<ActionResult<TB_CHAMADO>> Create(TB_CHAMADO entity) {
            try {
                entity.CREATE_AT = DateTime.Now;
                _context.TB_CHAMADO.Add(entity);
                await _context.SaveChangesAsync();

                return CreatedAtAction(nameof(Get), new { id = entity.COD }, entity);
            } catch (Exception ex) {
                return StatusCode(500, new { message = "Erro ao criar chamado", error = ex.Message });
            }
        }

        // PUT: api/Tickets/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, TB_CHAMADO entity) {
            if (id != entity.COD)
                return BadRequest(new { message = "ID não confere!" });

            entity.UPDATE_AT = DateTime.Now;
            _context.Entry(entity).State = EntityState.Modified;

            try {
                await _context.SaveChangesAsync();
                return Ok(entity);
            } catch (DbUpdateConcurrencyException) {
                if (!_context.TB_CHAMADO.Any(t => t.COD == id))
                    return NotFound(new { message = "Chamado não encontrado!" });
                throw;
            }
        }

        // DELETE: api/Tickets/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id) {
            var ticket = await _context.TB_CHAMADO.FindAsync(id);
            if (ticket == null)
                return NotFound(new { message = "Chamado não encontrado!" });

            _context.TB_CHAMADO.Remove(ticket);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
