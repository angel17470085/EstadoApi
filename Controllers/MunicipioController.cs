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

namespace AppEstados.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MunicipioController : ControllerBase
    {
        private readonly MiContext _context;

        public MunicipioController(MiContext context)
        {
            _context = context;
        }

        // GET: api/Municipio
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MunicipioDto>>> GetMunicipios()
        {
            return await _context.Municipios.Select(x=> MunicipioAdto(x)).ToListAsync();
        }

        // GET: api/Municipio/5
        [HttpGet("{id}")]
        public async Task<ActionResult<MunicipioDto>> GetMunicipio(int id)
        {
            var municipio = await _context.Municipios.FindAsync(id);

            if (municipio == null)
            {
                return NotFound();
            }
            var municipioDto = MunicipioAdto(municipio);
            return municipioDto;
        }

        // PUT: api/Municipio/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMunicipio(int id, MunicipioDto municipioDto)
        {
            if (id != municipioDto.Id)
            {
                return BadRequest();
            }

            var municipioAmodificar = await _context.Municipios.FindAsync(id);
           

            if (municipioAmodificar == null)
            {
                return NotFound();
            }

            municipioAmodificar.Nombre = municipioDto.Nombre;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException) when (!MunicipioExists(id))
            {             
                 return NotFound();        
            }

            return NoContent();
        }

        // POST: api/Municipio
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<MunicipioDto>> PostMunicipio(MunicipioDto municipioDto)
        {
            var municipio = new Municipio()
            {
                Nombre = municipioDto.Nombre
            };

            _context.Municipios.Add(municipio);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetMunicipio", new { id = municipio.Id }, municipio);
        }

        // DELETE: api/Municipio/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMunicipio(int id)
        {
            var municipio = await _context.Municipios.FindAsync(id);
            if (municipio == null)
            {
                return NotFound();
            }

            _context.Municipios.Remove(municipio);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        public static MunicipioDto MunicipioAdto(Municipio municipio)
        {
            var municipioDto = new MunicipioDto()
            {
                Id = municipio.Id,
                Nombre =municipio.Nombre
            };
            return municipioDto;
        }
        private bool MunicipioExists(int id)
        {
            return _context.Municipios.Any(e => e.Id == id);
        }
    }
}
