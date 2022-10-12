﻿using System;
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

            return EstadoADto(estado);
        }

        // PUT: api/Estado/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEstado(int id, Estado estado)
        {
            if (id != estado.Id)
            {
                return BadRequest();
            }

            _context.Entry(estado).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EstadoExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Estado
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<EstadoDto>> PostEstado(EstadoDto estadoDto)
        {

            var estado = new Estado
            {
                Nombre = estadoDto.Nombre
            };

            _context.Estados.Add(estado);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetEstado", new { id = estado.Id }, estado);
        }

        // DELETE: api/Estado/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEstado(int id)
        {
            var estado = await _context.Estados.FindAsync(id);
            if (estado == null)
            {
                return NotFound();
            }

            _context.Estados.Remove(estado);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private static EstadoDto EstadoADto(Estado estado)
        {
            return new EstadoDto {
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
