using System.Diagnostics;
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
    public class AlunoController : ControllerBase
    {
        private readonly UniversidadeContext _context;

        public AlunoController(UniversidadeContext context)
        {
            _context = context;
        }

        private static AlunoDTO AlunoToDTO(Aluno aluno)
        {
            ICollection<string?>? siglasucs = aluno.UnidadesCurriculares?.Select(uc => uc.Sigla).ToList();
            var siglacurso = aluno.Curso?.Sigla;
            return new AlunoDTO
            {
                Id = aluno.Id,
                Nome = aluno.Nome,
                SiglaCurso = siglacurso,
                Saldo = aluno.Saldo,
                Email = aluno.Email,
                SiglasUcs = siglasucs
            };
        }

        // GET: api/Aluno
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AlunoDTO>>> GetAluno()
        {
            if (_context.Alunos == null)
            {
                return NotFound("Alunos not found");
            }
            return await _context.Alunos.Include(x => x.Curso).Include(x => x.UnidadesCurriculares).Select(x => AlunoToDTO(x)).ToListAsync();
        }

        // GET: api/Aluno/5
        [HttpGet("{id:int}")]
        public async Task<ActionResult<AlunoDTO>> GetAluno(long id)
        {
            if (_context.Alunos == null)
            {
                return NotFound("Alunos is null");
            }
            var aluno = await _context.Alunos.Include(x => x.Curso).Include(x => x.UnidadesCurriculares).FirstOrDefaultAsync(x => x.Id == id);

            if (aluno == null)
            {
                return NotFound("Aluno não encontrado");
            }
            _context.Entry(aluno);

            return AlunoToDTO(aluno);
        }

        // GET: api/Aluno/LES
        [HttpGet("{sigla}")]
        public async Task<ActionResult<IEnumerable<AlunoDTO>>> GetCurso(string sigla)
        {
            if (_context.Alunos == null)
            {
                return NotFound("Alunos not found");
            }

            return await _context.Alunos.Where(x => x.Curso != null && x.Curso.Sigla == sigla).Include(x => x.Curso).Include(x => x.UnidadesCurriculares).Select(x => AlunoToDTO(x)).ToListAsync();
        }

        // PUT: api/Alunos/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAluno(long id, AlunoDTO alunoDTO)
        {
            if (id != alunoDTO.Id)
            {
                return BadRequest("Id do aluno não corresponde ao id do aluno a alterar");
            }

            var aluno = await _context.Alunos.Include(x => x.Curso).Include(x => x.UnidadesCurriculares).FirstOrDefaultAsync(x => x.Id == id);
            if (aluno == null)
            {
                return NotFound("Aluno não encontrado");
            }

            aluno.Nome = alunoDTO.Nome;
            aluno.Curso = await _context.Cursos.Where(c => c.Sigla == alunoDTO.SiglaCurso).FirstOrDefaultAsync();
            aluno.Saldo = alunoDTO.Saldo;
            aluno.Email = alunoDTO.Email;
            if (alunoDTO.SiglasUcs != null)
            {
                aluno.UnidadesCurriculares = addAlunoToUc(alunoDTO);
            }
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException) when (!AlunoExists(id))
            {
                return NotFound("Aluno não encontrado");
            }

            return NoContent();
        }

        // PUT: api/Alunos/5/saldo/debitar
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}/saldo/debitar")]
        public async Task<ActionResult<AlunoDTO>> PutAlunoSaldoDebito(long id, Double valor)
        {
            var aluno = await _context.Alunos.Include(x => x.Curso).Include(x => x.UnidadesCurriculares).FirstOrDefaultAsync(x => x.Id == id);
            if (aluno == null)
            {
                return NotFound("Aluno não encontrado");
            }
            if (aluno.Saldo < 0)
            {
                return BadRequest("Saldo negativo");
            }
            if (aluno.Saldo < valor)
            {
                return BadRequest("Saldo insuficiente. Saldo: " + aluno.Saldo + " valor: " + valor);
            }
            aluno.Saldo -= valor;
            await _context.SaveChangesAsync();
            return AlunoToDTO(aluno);
        }

        // PUT: api/Alunos/5/saldo/creditar
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}/saldo/creditar")]
        public async Task<ActionResult<AlunoDTO>> PutAlunoSaldoCredito(long id, Double valor)
        {
            var aluno = await _context.Alunos.Include(x => x.Curso).Include(x => x.UnidadesCurriculares).FirstOrDefaultAsync(x => x.Id == id);
            if (aluno == null)
            {
                return NotFound("Aluno não encontrado");
            }
            aluno.Saldo += valor;
            await _context.SaveChangesAsync();
            return AlunoToDTO(aluno);
        }



        // POST: api/Aluno
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<AlunoDTO>> PostAluno(AlunoDTO alunoDTO)
        {
            if (alunoDTO == null)
            {
                return BadRequest("Aluno não pode ser nulo");
            }

            var aluno = new Aluno
            {
                Nome = alunoDTO.Nome,
                Curso = await _context.Cursos.Where(c => c.Sigla == alunoDTO.SiglaCurso).FirstOrDefaultAsync(),
                Saldo = alunoDTO.Saldo,
                Email = alunoDTO.Email,
                UnidadesCurriculares = addAlunoToUc(alunoDTO)
            };

            if (aluno.Saldo < 0)
            {
                return BadRequest("Saldo não pode ser negativo");
            }
            if(aluno.Saldo == null)
            {
                aluno.Saldo = 100;
            }

            _context.Alunos.Add(aluno);
            await _context.SaveChangesAsync();

            return CreatedAtAction(
                nameof(GetAluno),
                new { id = aluno.Id },
                AlunoToDTO(aluno));
        }

        private ICollection<UnidadeCurricular>? addAlunoToUc(AlunoDTO alunoDTO)
        {
            ICollection<UnidadeCurricular> ucs = new List<UnidadeCurricular>();
            if (alunoDTO == null)
            {
                return null;
            }
            if (alunoDTO.SiglasUcs != null)
            {
                ucs = _context.UnidadesCurriculares.Where(u => alunoDTO.SiglasUcs.Contains(u.Sigla)).ToList();
            }
            return ucs;
        }

        // DELETE: api/Aluno/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAluno(long id)
        {
            var aluno = await _context.Alunos.FindAsync(id);

            if (aluno == null)
            {
                return NotFound("Aluno não encontrado");
            }

            _context.Alunos.Remove(aluno);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool AlunoExists(long id)
        {
            return (_context.Alunos?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
