using Tontz.Domain.Entities;
using Tontz.Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Tontz.API.Controllers {
    [ApiController]
    [Route("api/[controller]")]
    public class PrioritiesController : ControllerBase {
        private readonly TontzDbContext _context;

        public PrioritiesController(TontzDbContext context) {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TB_PRIORIDADE>>> GetAll() {
            return Ok(await _context.TB_PRIORIDADE.ToListAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TB_PRIORIDADE>> Get(byte id) {
            var entity = await _context.TB_PRIORIDADE.FindAsync(id);
            if (entity == null) return NotFound(new { message = "Prioridade não encontrada!" });
            return Ok(entity);
        }

        [HttpPost]
        public async Task<ActionResult<TB_PRIORIDADE>> Create(TB_PRIORIDADE entity) {
            _context.TB_PRIORIDADE.Add(entity);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(Get), new { id = entity.COD }, entity);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(byte id, TB_PRIORIDADE entity) {
            if (id != entity.COD) return BadRequest(new { message = "ID não confere!" });
            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return Ok(entity);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(byte id) {
            var entity = await _context.TB_PRIORIDADE.FindAsync(id);
            if (entity == null) return NotFound(new { message = "Prioridade não encontrada!" });
            _context.TB_PRIORIDADE.Remove(entity);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
 