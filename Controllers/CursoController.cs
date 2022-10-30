using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Universidade_Api;

namespace Universidade_Api.Controllers
{
    [Route("api/[controller]s")]
    [ApiController]
    public class CursoController : ControllerBase
    {
        private readonly UniversidadeContext _context;

        public CursoController(UniversidadeContext context)
        {
            _context = context;
        }

        // get with query for name in url
        

        // GET: api/cursos/dispose
        /// Used to dispose of the database context
        [HttpGet("dispose")]
        public void Dispose()
        {
            _context.Dispose();
        }

        // GET: api/cursos/
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Curso>>> GetCurso()
        {

            if (_context.Cursos == null)
            {
                return NotFound("Não existem cursos");
            }
            return await _context.Cursos.ToListAsync();
        }

        // GET: api/cursos/5
        [HttpGet("{id:int}")]
        public async Task<ActionResult<Curso>> GetCurso(long id)
        {
            if (_context.Cursos == null)
            {
                return NotFound("Não existem cursos");
            }
            var curso = await _context.Cursos.FindAsync(id);

            if (curso == null)
            {
                return NotFound("Não existe curso com esse id");
            }

            return curso;
        }

        // GET: api/cursos/LES
        [HttpGet("{sigla}")]
        public async Task<ActionResult<Curso>> GetCurso(string sigla)
        {
            if (_context.Cursos == null)
            {
                return NotFound("Não existem cursos");
            }
            var curso = await _context.Cursos.Where(c => c.Sigla == sigla).FirstOrDefaultAsync();

            if (curso == null)
            {
                return NotFound("Não existe curso com essa sigla");
            }

            return curso;
        }


        // PUT: api/cursos/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCurso(long id, Curso curso)
        {
            if (id != curso.Id)
            {
                return BadRequest("Id do curso não corresponde ao id do curso a alterar");
            }

            _context.Entry(curso).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CursoExists(id))
                {
                    return NotFound("Não existe curso com esse id");
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/cursos/
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Curso>> PostCurso(Curso curso)
        {
            if (_context.Cursos == null)
            {
                return Problem("Entity set 'UniversidadeContext.Cursos'  is null.");
            }
            _context.Cursos.Add(curso);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetCurso), new { id = curso.Id }, curso);
        }

        // DELETE: api/cursos/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCurso(long id)
        {
            if (_context.Cursos == null)
            {
                return NotFound("Não existem cursos");
            }
            var curso = await _context.Cursos.FindAsync(id);
            if (curso == null)
            {
                return NotFound("Não existe curso com esse id");
            }

            _context.Cursos.Remove(curso);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CursoExists(long id)
        {
            return (_context.Cursos?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
