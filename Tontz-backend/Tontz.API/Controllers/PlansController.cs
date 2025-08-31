using Tontz.Domain.Entities;
using Tontz.Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Tontz.API.Controllers {
    [ApiController]
    [Route("api/[controller]")]
    public class PlansController : ControllerBase {
        private readonly TontzDbContext _context;

        public PlansController(TontzDbContext context) {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TB_PLANO>>> GetAll() {
            return Ok(await _context.TB_PLANO.ToListAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TB_PLANO>> Get(int id) {
            var entity = await _context.TB_PLANO.FindAsync(id);
            if (entity == null) return NotFound(new { message = "Plano não encontrado!" });
            return Ok(entity);
        }

        [HttpPost]
        public async Task<ActionResult<TB_PLANO>> Create(TB_PLANO entity) {
            _context.TB_PLANO.Add(entity);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(Get), new { id = entity.COD }, entity);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, TB_PLANO entity) {
            if (id != entity.COD) return BadRequest(new { message = "ID não confere!" });
            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return Ok(entity);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id) {
            var entity = await _context.TB_PLANO.FindAsync(id);
            if (entity == null) return NotFound(new { message = "Plano não encontrado!" });
            _context.TB_PLANO.Remove(entity);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
