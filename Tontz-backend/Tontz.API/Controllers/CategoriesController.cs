using Tontz.Domain.Entities;
using Tontz.Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Tontz.Domain.Entities;

namespace Tontz.API.Controllers {
    [ApiController]
    [Route("api/[controller]")]
    public class CategoriesController : ControllerBase {
        private readonly TontzDbContext _context;

        public CategoriesController(TontzDbContext context) {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TB_CATEGORIA>>> GetAll() {
            return Ok(await _context.TB_CATEGORIA.ToListAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TB_CATEGORIA>> Get(int id) {
            var entity = await _context.TB_CATEGORIA.FindAsync(id);
            if (entity == null) return NotFound(new { message = "Categoria não encontrada!" });
            return Ok(entity);
        }

        [HttpPost]
        public async Task<ActionResult<TB_CATEGORIA>> Create(TB_CATEGORIA entity) {
            _context.TB_CATEGORIA.Add(entity);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(Get), new { id = entity.COD }, entity);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, TB_CATEGORIA entity) {
            if (id != entity.COD) return BadRequest(new { message = "ID não confere!" });
            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return Ok(entity);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id) {
            var entity = await _context.TB_CATEGORIA.FindAsync(id);
            if (entity == null) return NotFound(new { message = "Categoria não encontrada!" });
            _context.TB_CATEGORIA.Remove(entity);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
