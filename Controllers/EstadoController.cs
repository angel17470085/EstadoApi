using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AppEstados.Data;
using AppEstados.Models;
using AppEstados.DTOs;
using Microsoft.Build.Framework;

namespace AppEstados.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EstadoController : ControllerBase
    {
        private readonly MiContext _context;

        public EstadoController(MiContext context)
        {
            _context = context;
        }

        // GET: api/Estado
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EstadoDto>>> GetEstados()
        {
            var listaEstados = await _context.Estados.Select(x => EstadoADto(x)).ToListAsync();
            return listaEstados;
        }

        // GET: api/Estado/5
        [HttpGet("{id}")]
        public async Task<ActionResult<EstadoDto>> GetEstado(int id)
        {
            var estado = await _context.Estados.FindAsync(id);

            if (estado == null)
            {
                return NotFound();
            }

            var estadoDto = EstadoADto(estado);
            return estadoDto;
        }

        // PUT: api/Estado/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEstado(int id, EstadoDto estadoDto)
        {
            if (id != estadoDto.Id)
            {
                return BadRequest();
            }

            var estadoAmodificar = await _context.Estados.FindAsync(id);

            if (estadoAmodificar == null)
            {
                return BadRequest();
            }

            estadoAmodificar.Nombre = estadoDto.Nombre;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException) when (!EstadoExists(id))
            {
                return NotFound();
            }

            return NoContent();
        }

        // POST: api/Estado
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<EstadoDto>> PostEstado(EstadoDto estadoDto)
        {

            var estado = new Estado()
            {
                Nombre = estadoDto.Nombre
            };

            _context.Estados.AddRange(estado);

            await _context.SaveChangesAsync();

            return CreatedAtAction("GetEstado", new { id = estado.Id }, estado);
        }

    

        // DELETE: api/Estado/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEstado(int id)
        {
            var estadoAborrar = await _context.Estados.FindAsync(id);
            if (estadoAborrar == null)
            {
                return NotFound();
            }

            _context.Estados.Remove(estadoAborrar);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private static EstadoDto EstadoADto(Estado estado)
        {
            return new EstadoDto
            {
                Id = estado.Id,
                Nombre = estado.Nombre
            };
        }


        private bool EstadoExists(int id)
        {
            return _context.Estados.Any(e => e.Id == id);
        }


    }
}
