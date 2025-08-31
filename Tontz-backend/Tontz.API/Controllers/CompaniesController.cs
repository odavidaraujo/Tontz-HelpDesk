using Tontz.Domain.Entities;
using Tontz.Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Tontz.API.Controllers {
    [ApiController]
    [Route("api/[controller]")]
    public class CompaniesController : ControllerBase {
        private readonly TontzDbContext _context;

        public CompaniesController(TontzDbContext context) {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TB_EMPRESA>>> GetAll() {
            return Ok(await _context.TB_EMPRESA
                .Include(e => e.EMPRESA_PLANO)
                .ToListAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TB_EMPRESA>> Get(int id) {
            var entity = await _context.TB_EMPRESA
                .Include(e => e.EMPRESA_PLANO)
                .FirstOrDefaultAsync(e => e.COD == id);

            if (entity == null) return NotFound(new { message = "Empresa não encontrada!" });
            return Ok(entity);
        }

        [HttpPost]
        public async Task<ActionResult<TB_EMPRESA>> Create(TB_EMPRESA entity) {
            _context.TB_EMPRESA.Add(entity);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(Get), new { id = entity.COD }, entity);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, TB_EMPRESA entity) {
            if (id != entity.COD) return BadRequest(new { message = "ID não confere!" });
            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return Ok(entity);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id) {
            var entity = await _context.TB_EMPRESA.FindAsync(id);
            if (entity == null) return NotFound(new { message = "Empresa não encontrada!" });
            _context.TB_EMPRESA.Remove(entity);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
