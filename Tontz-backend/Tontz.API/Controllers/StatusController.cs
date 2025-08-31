using Tontz.Domain.Entities;
using Tontz.Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Tontz.API.Controllers {
    [ApiController]
    [Route("api/[controller]")]
    public class StatusesController : ControllerBase {
        private readonly TontzDbContext _context;

        public StatusesController(TontzDbContext context) {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TB_STATUS>>> GetAll() {
            return Ok(await _context.TB_STATUS.ToListAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TB_STATUS>> Get(byte id) {
            var entity = await _context.TB_STATUS.FindAsync(id);
            if (entity == null) return NotFound(new { message = "Status não encontrado!" });
            return Ok(entity);
        }

        [HttpPost]
        public async Task<ActionResult<TB_STATUS>> Create(TB_STATUS entity) {
            _context.TB_STATUS.Add(entity);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(Get), new { id = entity.COD }, entity);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(byte id, TB_STATUS entity) {
            if (id != entity.COD) return BadRequest(new { message = "ID não confere!" });
            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return Ok(entity);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(byte id) {
            var entity = await _context.TB_STATUS.FindAsync(id);
            if (entity == null) return NotFound(new { message = "Status não encontrado!" });
            _context.TB_STATUS.Remove(entity);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
