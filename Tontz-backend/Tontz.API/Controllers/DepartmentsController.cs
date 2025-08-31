using Tontz.Domain.Entities;
using Tontz.Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Tontz.API.Controllers {
    [ApiController]
    [Route("api/[controller]")]
    public class DepartmentsController : ControllerBase {
        private readonly TontzDbContext _context;

        public DepartmentsController(TontzDbContext context) {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TB_SETOR>>> GetAll() {
            return Ok(await _context.TB_SETOR.ToListAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TB_SETOR>> Get(int id) {
            var entity = await _context.TB_SETOR.FindAsync(id);
            if (entity == null) return NotFound(new { message = "Setor não encontrado!" });
            return Ok(entity);
        }

        [HttpPost]
        public async Task<ActionResult<TB_SETOR>> Create(TB_SETOR entity) {
            _context.TB_SETOR.Add(entity);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(Get), new { id = entity.COD }, entity);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, TB_SETOR entity) {
            if (id != entity.COD) return BadRequest(new { message = "ID não confere!" });
            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return Ok(entity);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id) {
            var entity = await _context.TB_SETOR.FindAsync(id);
            if (entity == null) return NotFound(new { message = "Setor não encontrado!" });
            _context.TB_SETOR.Remove(entity);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
