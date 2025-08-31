using Tontz.Domain.Entities;
using Tontz.Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Tontz.API.Controllers {
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeesController : ControllerBase {
        private readonly TontzDbContext _context;

        public EmployeesController(TontzDbContext context) {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TB_FUNCIONARIO>>> GetAll() {
            return Ok(await _context.TB_FUNCIONARIO
                .Include(f => f.EMPRESA)
                .ToListAsync());
        }

        [HttpGet("{cpf}")]
        public async Task<ActionResult<TB_FUNCIONARIO>> Get(string cpf) {
            var entity = await _context.TB_FUNCIONARIO
                .Include(f => f.EMPRESA)
                .FirstOrDefaultAsync(f => f.CPF == cpf);

            if (entity == null) return NotFound(new { message = "Funcionário não encontrado!" });
            return Ok(entity);
        }

        [HttpPost]
        public async Task<ActionResult<TB_FUNCIONARIO>> Create(TB_FUNCIONARIO entity) {
            _context.TB_FUNCIONARIO.Add(entity);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(Get), new { cpf = entity.CPF }, entity);
        }

        [HttpPut("{cpf}")]
        public async Task<IActionResult> Update(string cpf, TB_FUNCIONARIO entity) {
            if (cpf != entity.CPF) return BadRequest(new { message = "CPF não confere!" });
            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return Ok(entity);
        }

        [HttpDelete("{cpf}")]
        public async Task<IActionResult> Delete(string cpf) {
            var entity = await _context.TB_FUNCIONARIO.FindAsync(cpf);
            if (entity == null) return NotFound(new { message = "Funcionário não encontrado!" });
            _context.TB_FUNCIONARIO.Remove(entity);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
